using dBosque.Stub.Interfaces;
using dBosque.Stub.Repository.Extensions;
using dBosque.Stub.Repository.Interfaces;
using dBosque.Stub.Repository.StubDb.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.XPath;

namespace dBosque.Stub.Repository
{
    public class LinqStubRepository :  IStubDataRepository
    {

        private readonly IDbContextBuilder _builder;
        private StubDbEntities database;
        private readonly ConnectionStringSetting _connection;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="config"></param>
        public LinqStubRepository(IEnumerable<IDbContextBuilder> builders, IConfigurationRepository config, ILogger logger)
        {
            _connection = config.Current;

            _builder = builders.FirstOrDefault(b => b.CanHandle(_connection.ProviderName));
            database = _builder.CreateDbContext<StubDbEntities>(_connection.ConnectionString);
            logger.LogTrace($"Created datarepository from {_connection.ConnectionString}");             
        }

        ///<summary>
        ///The provider used by this repository
        ///</summary>
        string IStubDataRepository.Provider => _connection.ProviderName;

        ///<summary>
        ///Get all logging
        ///</summary>
        ///<param name="filter"></param>
        ///<returns></returns>
        IEnumerable<StubLog> IStubDataRepository.GetLogs(System.Linq.Expressions.Expression<Func<StubLog, bool>> filter)
        {
            return database.StubLog.Where(filter).AsEnumerable();
        }
        ///<summary>
        ///Get all logging
        ///</summary>
        ///<param name="size"></param>
        ///<param name="prevValue"></param>
        ///<param name="negative"></param>
        ///<param name="query"></param>
        ///<returns></returns>
        IEnumerable<stp_selectLog_Result> IStubDataRepository.GetLogs(int size, int prevValue, bool negative, string query)
        {            
            query = query.Replace("@pagesize", size.ToString()).Replace("@prevVal", prevValue.ToString()).Replace("@compare", negative ? "<" : ">");
            return database.stp_log(query);
        }


        private string AsExactRegEx(string pattern)
        {
            if (!pattern.EndsWith("$", StringComparison.Ordinal))
                pattern += "$";
            if (!pattern.StartsWith("^", StringComparison.Ordinal))
                pattern = "^" + pattern;
            return pattern;
        }

        private IEnumerable<MessageType> GetTypesForMessage<T>(IStubMessage<T> message)
        {
            //1. Check if MessageType is supported
            var msgTypes = database.MessageType.Where(mt => mt.Namespace == message.RootNameSpace &&
                                                                        mt.Rootnode == message.RootNode).ToList();           

            // If no messagetype exists, and the uri is not null, try with xpath.
            if (!msgTypes.Any() && !string.IsNullOrEmpty(message.Uri))
            {
                msgTypes = database.MessageType
                    //.Where(mt => mt.Rootnode == message.RootNode)
                    .ToList() // An extra tolist, to ensure the regex is not handled in the database. (which is impossible)
                    .Where(mt => Regex.IsMatch(message.Uri, AsExactRegEx(mt.Namespace) ,RegexOptions.IgnoreCase))
                    .ToList();
            }

            return msgTypes;
        }

        private void UpdateMessageBasedOnResult<T>(IStubMessage<T> message, MessageType first, bool found, bool hasCombo)
        {

            if (!found && first == null)
                message.Matches.Error = $"Message with {message.Description} is not supported.";
            else if (!found)
            {
                if (hasCombo)
                    message.Matches.Error = "No match found.";
                else
                    message.Matches.Error = $"No templates found for message with {message.Description}.";

                // Get the passthrough info from the first messagetype.
                message.IsPassTrough = first.PassthroughEnabled;
                message.PassthroughUri = first.PassthroughUrl;
            }
            
        }
        private bool FindValidCombination<T>(IStubMessage<T> message, IEnumerable<MessageType> msgTypes)
        {
            bool found = false;
            bool hasCombo = false;

            foreach (var msgType in msgTypes)
            {
                if (msgType.Combination?.Count > 0)
                {
                    hasCombo = true;
                    // 2. Find a combo
                
                    foreach (var combination in msgType.Combination)
                    {
                        if (IsMatch(combination, message.Uri, msgType.Namespace, message))
                        {
                            UpdateMessage(message, combination);
                            LogMatch(message);
                            found = true;
                          
                        }
                    }
                }
            }

            UpdateMessageBasedOnResult(message, msgTypes.FirstOrDefault(), found, hasCombo);
            return found;
        }

        /// <summary>
        /// Try to find a stub for the given message
        /// </summary>
        /// <param name="message"></param>
        void IStubDataRepository.GetStubForMessage<T>(IStubMessage<T> message)
        {
            var msgTypes = GetTypesForMessage(message);
            msgTypes = database.MessageType
                .Include(m => m.Combination)
                    .ThenInclude(c => c.CombinationXpath)
                    .ThenInclude(cx => cx.Xpath)
                    .ThenInclude(x => x.TemplateXpath)
                .Include(m => m.Combination)
                    .ThenInclude(c => c.Response)
                .Include(m => m.Template)
                .Where(m => msgTypes.Select(a => a.MessageTypeId).Contains(m.MessageTypeId)).ToList();
            if (!FindValidCombination(message, msgTypes))
                (this as IStubDataRepository).Log(null, message.Request, message.TenantId, message.Uri, msgTypes?.FirstOrDefault()?.MessageTypeId);
        }

        ///<summary>
        ///Log a message to the database
        ///</summary>
        ///<param name="id"></param>
        ///<param name="request"></param>
        ///<param name="tenant"></param>
        ///<param name="uri"></param>
        ///<param name="messageTypeId"></param>
        void IStubDataRepository.Log(long? id, string request, long? tenant, string uri, long? typeId)
        {
            database.StubLog.Add(new StubLog() { CombinationId = id, ResponseDatumTijd = DateTime.Now, Request = request, TenantId = tenant, Uri = uri, MessageTypeId = typeId });
            database.SaveChanges();
        }
        ///<summary>
        ///Flush the database connection and create a new one.
        ///</summary>
        void IStubDataRepository.Flush()
        {
            database.Dispose();
            database = _builder.CreateDbContext<StubDbEntities>(_connection.ConnectionString);
        }
        ///<summary>
        ///The current connection
        ///</summary>
        string IStubDataRepository.Connection => _connection.Name;

        ///<summary>
        ///Return the messageType with the given id
        ///</summary>
        ///<param name="messageTypeId"></param>
        ///<returns></returns>
        MessageType IStubDataRepository.GetMessageType(long messageTypeId)
            => database.MessageType
                 .Include(m => m.Combination)
                    .ThenInclude(c => c.CombinationXpath)
                    .ThenInclude(cx => cx.Xpath)
                    .ThenInclude(x => x.TemplateXpath)
                .Include(m => m.Combination)
                    .ThenInclude(c => c.Response)
                .Include(m => m.Template)
                .Distinct().FirstOrDefault(a => a.MessageTypeId == messageTypeId);

        ///<summary>
        ///Return all messagetypes
        ///</summary>
        ///<returns></returns>
        IEnumerable<MessageType> IStubDataRepository.GetMessageTypes() 
            => database.MessageType
               .Include(m => m.Combination)
                    .ThenInclude(c => c.CombinationXpath)
                    .ThenInclude(cx => cx.Xpath)
                    .ThenInclude(x => x.TemplateXpath)
                .Include(m => m.Combination)
                    .ThenInclude(c => c.Response)
                .Include(m => m.Template)
                .Distinct().AsEnumerable();

        ///<summary>
        ///Get an xpath for the given id
        ///</summary>
        ///<param name="comboXpathId"></param>
        ///<returns></returns>
        CombinationXpath IStubDataRepository.GetCombinationXpath(long comboXpathId)
            => database.CombinationXpath
                .Include(c => c.Xpath)
                .Where(a => a.CombinationXpathId == comboXpathId).FirstOrDefault();

        ///<summary>
        ///Get a specific combination for the given id
        ///</summary>
        ///<param name="combId"></param>
        ///<returns></returns>
        Combination IStubDataRepository.GetCombination(long combId)
           => database.Combination
                        .Include( c => c.Response)
                        .Include(c => c.CombinationXpath)
                            .ThenInclude(cx => cx.Xpath)
                       .Where(temp => temp.CombinationId == combId)
                       .FirstOrDefault();

        ///<summary>
        ///Get all combinations.
        ///</summary>
        ///<returns></returns>
        IEnumerable<Combination> IStubDataRepository.GetCombinations()
            => database.Combination.Distinct().AsEnumerable();

       ///<summary>
       ///Get a specific template for the given Id
       ///</summary>
       ///<param name="templateId"></param>
       ///<returns></returns>
       Template IStubDataRepository.GetTemplate(long templateId)
            => database.Template
                        .Include( t=> t.Combination)
                        .Include( t => t.TemplateXpath)
                        .ThenInclude(x => x.Xpath)
                        .Where(temp => temp.TemplateId == templateId)
                        .FirstOrDefault();

        ///<summary>
        ///Get all templates
        ///</summary>
        ///<returns></returns>
        IEnumerable<Template> IStubDataRepository.GetTemplates()
         => database.Template.Distinct().AsEnumerable();

        ///<summary>
        ///Get all templated for a specific messagetype
        ///</summary>
        ///<param name="messageTypeId"></param>
        ///<returns></returns>
        IEnumerable<Template> IStubDataRepository.GetTemplates(long messageTypeId)
            => database.Template
                        .Include(t => t.Combination)
                            .ThenInclude(c => c.CombinationXpath)
                            .ThenInclude(cx => cx.Xpath)
                        .Where(temp => temp.MessageTypeId == messageTypeId)
                        .OrderBy(temp => temp.Description);

        ///<summary>
        ///Get all available xpaths
        ///</summary>
        ///<returns></returns>
        IEnumerable<Xpath> IStubDataRepository.GetXpaths() 
            => database.Xpath
                    .Include(x => x.TemplateXpath)
                    .AsEnumerable();
        ///<summary>
        ///Get a specific xpath
        ///</summary>
        ///<param name="xpathId"></param>
        ///<returns></returns>
        Xpath IStubDataRepository.GetXpath(long xpathId)
           => database.Xpath.FirstOrDefault(a => a.XpathId == xpathId);
        ///<summary>
        ///Return the response with the given id
        ///</summary>
        ///<param name="id"></param>
        ///<returns></returns>
        Response IStubDataRepository.GetResponse(long id) 
            => database.Response
            .Include(r => r.Combination)
                .ThenInclude(c => c.CombinationXpath)
                    .ThenInclude(x => x.Xpath)
            .FirstOrDefault(r => r.ResponseId == id);

        ///<summary>
        ///Update a template
        ///</summary>
        ///<param name="id"></param>
        ///<param name="updateAction"></param>
        ///<returns></returns>
        Template IStubDataRepository.UpdateTemplate(long? id, Action<Template> updateAction)
        {
            var item = new Template();
            if (id.HasValue)
                item = database.Template.FirstOrDefault(r => r.TemplateId == id);
            else
                database.Template.Add(item);

            updateAction(item);
            database.SaveChanges();
            return item;
        }


        ///<summary>
        ///Update a combination
        ///</summary>
        ///<param name="id"></param>
        ///<param name="updateAction"></param>
        ///<returns></returns>
        Combination IStubDataRepository.UpdateCombination(long? id, Action<Combination> updateAction)
        {
            var item = new Combination();
            if (id.HasValue)
                item = database.Combination
                    .Include(c => c.CombinationXpath)
                    .FirstOrDefault(r => r.CombinationId == id);
            else
                database.Combination.Add(item);

            updateAction(item);
            database.SaveChanges();
            return item;
        }
        ///<summary>
        ///Update a response
        ///</summary>
        ///<param name="id"></param>
        ///<param name="updateAction"></param>
        ///<returns></returns>
        Response IStubDataRepository.UpdateResponse(long? id, Action<Response> updateAction)
        {
            var item = new Response();
            if (id.HasValue)
                item = database.Response
                      .Include(r => r.Combination)
                        .ThenInclude(c => c.CombinationXpath)
                            .ThenInclude(x => x.Xpath)
                      .FirstOrDefault(r => r.ResponseId == id);
            else
                database.Response.Add(item);

            updateAction(item);
            database.SaveChanges();
            return item;
        }
        ///<summary>
        ///Update a specific Xpath combo
        ///</summary>
        ///<param name="id"></param>
        ///<param name="updateAction"></param>
        ///<returns></returns>
        CombinationXpath IStubDataRepository.UpdateCombinationXpath(long? id, Action<CombinationXpath> updateAction)
        {
            var item = new CombinationXpath();
            if (id.HasValue)
                item = database.CombinationXpath.FirstOrDefault(r => r.CombinationXpathId == id);
            else
                database.CombinationXpath.Add(item);

            updateAction(item);
            database.SaveChanges();
            return item;
        }

        ///<summary>
        ///Update an xpath
        ///</summary>
        ///<param name="id"></param>
        ///<param name="updateAction"></param>
        ///<returns></returns>
        Xpath IStubDataRepository.UpdateXpath(long? id, Action<Xpath> updateAction)
        {
            var item = new Xpath();
            if (id.HasValue)
                item = database.Xpath.FirstOrDefault(r => r.XpathId == id);
            else
                database.Xpath.Add(item);

            updateAction(item);
            database.SaveChanges();
            return item;        
        }

        ///<summary>
        ///Check if a given template Id has shared combination
        ///</summary>
        ///<param name="id"></param>
        ///<returns></returns>
        bool IStubDataRepository.HasSharedCombinationsWithOther(long id)
        {
            var template = database.Template.FirstOrDefault(t => t.TemplateId == id);
            return database.Combination.Any(zc => zc.MessageTypeId != template.MessageTypeId && zc.TemplateId == template.TemplateId);
        }
        ///<summary>
        ///Check if a given xpath is in use by a combination
        ///</summary>
        ///<param name="path"></param>
        ///<returns></returns>
        bool IStubDataRepository.IsInUse(Xpath path)
        {
            return database.TemplateXpath.Any(x => x.XpathId == path.XpathId) ||
                   database.CombinationXpath.Any(x => x.XpathId == path.XpathId);         
        }

        ///<summary>
        ///Update a messagetype
        ///</summary>
        ///<param name="id"></param>
        ///<param name="updateAction"></param>
        ///<returns></returns>
        MessageType IStubDataRepository.UpdateMessageType(long? id, Action<MessageType> updateAction)
        {
            var item = new MessageType();
            if (id.HasValue)
                item = database.MessageType
                    .Include(m => m.Combination)
                        .ThenInclude(c => c.CombinationXpath)
                        .ThenInclude(cx => cx.Xpath)
                        .ThenInclude(x => x.TemplateXpath)
                    .Include(m => m.Combination)
                        .ThenInclude(c => c.Response)
                    .Include(m => m.Template)
                    .FirstOrDefault(r => r.MessageTypeId == id);
            else
                database.MessageType.Add(item);

            updateAction(item);
            database.SaveChanges();
            return item;
        }
        ///<summary>
        ///Clone a complete tree item
        ///</summary>
        ///<param name="id"></param>
        ///<returns></returns>
        long IStubDataRepository.CloneResponseTree(long id)
        {
            // Copy from?
            var def = database.Combination
                .Include(c => c.Response)
                .Include(c => c.CombinationXpath)
                .FirstOrDefault(zc => zc.Response.ResponseId == id);

            // new Combinatie
            var copy = new Combination()
            {
                Description = "Copy " + def.Description,
                MessageTypeId = def.MessageTypeId,
                TemplateId = def.TemplateId,
                Response = new Response()
                {
                    Description = def.Response.Description,
                    ResponseText = "<EMPTY_RESPONSE/>",
                    ContentType = def.Response.ContentType,
                    Headers = def.Response.Headers,
                    StatusCode = def.Response.StatusCode
                },
            };
            // new xpaths
            foreach (var x in def.CombinationXpath)
            {
                // Default new xpath value
                string newXpathValue = "Copy " + x.XpathValue;
             

                copy.CombinationXpath.Add(new CombinationXpath()
                {
                    XpathId = x.XpathId,
                    XpathValue = newXpathValue
                });
            }

            database.Combination.Add(copy);
           
            database.SaveChanges();
            return copy.CombinationId;
        }

        ///<summary>
        ///Delete a messagetype
        ///</summary>
        ///<param name="id"></param>
        void IStubDataRepository.DeleteMessageType(long id)
        {
            var mt = database.MessageType.FirstOrDefault(a => a.MessageTypeId == id);
            if (mt != null)
            {
                database.MessageType.Remove(mt);
                database.SaveChanges();
            }

        }
        ///<summary>
        ///Delete the specific xpath
        ///</summary>
        ///<param name="path"></param>
        void IStubDataRepository.DeleteXpath(Xpath path)
        {
            database.Xpath.Remove(path);
            database.SaveChanges();
        }
        ///<summary>
        ///Delete a template and the complete tree of combinations and response below it.
        ///</summary>
        ///<param name="id"></param>
        void IStubDataRepository.DeleteTemplateDown(long id)
        {
            var template = database.Template
                .Include(t => t.Combination)
                .Include(t => t.TemplateXpath)
                .FirstOrDefault(t => t.TemplateId == id);
            template.Combination.ToList().ForEach(c => (this as IStubDataRepository).DeleteCombinationDown(c.CombinationId));

            // Delete templateXpaths
            database.TemplateXpath.RemoveRange(template.TemplateXpath);
            database.Template.Remove(template);
            database.SaveChanges();
        }
        ///<summary>
        ///Delete a combination and the complete tree below it.
        ///</summary>
        ///<param name="id"></param>
        void IStubDataRepository.DeleteCombinationDown(long id)
        {
            var def = database.Combination.FirstOrDefault(r => r.ResponseId == id);
            if (def != null)
            {
                database.Response.Remove(def.Response);
                // Delete CombinatieXpath Valuen
                database.CombinationXpath.RemoveRange(def.CombinationXpath);
                // Delete combinatie
                database.Combination.Remove(def);
                database.SaveChanges();
            }
        }

        ///<summary>
        ///Save the current settings.
        ///</summary>
        void IStubDataRepository.Save()
        {
            database.SaveChanges();
        }

        /// <summary>
        /// Create a navigator for the xml document
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private XPathNavigator GetNavigator<T>(IStubMessage<T> message)
        {
            var doc = new XPathDocument(new StringReader(message.Request.IfEmpty("<root/>")));
            return doc.CreateNavigator();
        }

        private void LogMatch<T>(IStubMessage<T> message)
        {       
        }

        /// <summary>
        /// Update the message with a found combinatie
        /// </summary>
        /// <param name="message"></param>
        /// <param name="combo"></param>
        private void UpdateMessage<T>(IStubMessage<T> message, Combination combo)
        {
            var match = new MatchCombination(combo.Description);

            // Copy matches
            match.Items.AddRange(combo.CombinationXpath.Select(xpath => new MatchItem(xpath.Xpath.CleanExpression, xpath.XpathValue)));

            // Update message
            message.Response            = combo.Response.ResponseText;            
            message.HttpStatusCode      = (int)(combo.Response.StatusCode ?? 200);
            message.ContentType         = combo.Response.ContentType.IfEmpty(message.ContentType);
            message.ResponseHeaders     = combo.Response.FromHeaders();
           
            message.IsPassTrough        = message.HttpStatusCode == -1;
            // If there was a match, but it is marked as passthrough, the passthrough uri is in the responsetext
            if (message.IsPassTrough)
                message.PassthroughUri = message.Response;

            message.Matches.Add(match);
            (this as IStubDataRepository).Log(combo.CombinationId, message.Request, message.TenantId, message.Uri, combo.MessageTypeId);
        }

        private Match IsValidRegexMatch(string uri, string uriPattern, out Regex pattern)
        {
            uriPattern = AsExactRegEx(uriPattern);
            pattern = new Regex(uriPattern, RegexOptions.IgnoreCase);
            if (uri != null && pattern.IsMatch(uri) && pattern.GetGroupNames().Length > 0)
                return pattern.Match(uri);
            return null;
        }

        private Regex AsSaveRegex(string value)
        {
            try
            {
                return new Regex(AsExactRegEx(value), RegexOptions.IgnoreCase);
            }
            catch (Exception)
            {
                // catch all
            }
            return null;
        }
        /// <summary>
        /// Check to see if a specific combinatie is a match
        /// </summary>
        /// <param name="combo"></param>
        /// <param name="navigator"></param>
        /// <returns></returns>
        private bool IsMatch<T>(Combination combo, string uri, string uriPattern, IStubMessage<T> message)
        {
            bool isMatch = false;

            Match match = IsValidRegexMatch(uri, uriPattern, out Regex pattern);
            var navigator = GetNavigator(message);
            foreach (var xpath in combo.CombinationXpath)
            {
                // Content
                if (xpath.Xpath.Type == 0)
                {
                    
                    Regex xpathRegex = AsSaveRegex(xpath.XpathValue);
                    var iterator = navigator.SelectSingleNode(message.LocalizeXpath(xpath.Xpath.Expression));
                    if (iterator != null)
                        isMatch = (iterator.Value == xpath.XpathValue) || (xpathRegex?.IsMatch(iterator.Value)??false);
                }
                // Uri
                else if (xpath.Xpath.Type == 1)
                {
                    isMatch = match != null && pattern.GetGroupNames().Contains(xpath.Xpath.Expression) && match.Groups[xpath.Xpath.Expression].Value == xpath.XpathValue;
                }
                else
                {
                    isMatch = false;
                }
                if (!isMatch)
                    continue;
            }
            return isMatch;            
        }
    }
}
