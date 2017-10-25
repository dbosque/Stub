using dBosque.Stub.Services.Extensions;
using System.Linq;

namespace dBosque.Stub.Server.WebApi.Configuration.Model
{
    /// <summary>
    /// Extensions class to transform object to a corresponding webApi Model
    /// </summary>
    public static class EntityToModelExtensions
    {
        /// <summary>
        /// Transform to its webapi Model
        /// </summary>
        /// <param name="a"></param>
        /// <param name="uri"></param>
        /// <param name="addTemplates"></param>
        /// <returns></returns>
        public static Match AsModel(this Repository.StubDb.Entities.Xpath a, string uri = null, bool addTemplates = false)
        {
            return new Match()
            {
                Id = a.XpathId,
                XPath = a.Type == 0 ? a.CleanExpression : null,
                GroupName = a.Type == 1 ? a.CleanExpression : null,
                Type = a.TypeToName(),
                Templates = addTemplates ? a.TemplateXpath.Select(b => new Template() { Id = b.TemplateId }.CreateLink(uri)) : null
            }.CreateLink(uri) as Match;
        }

        /// <summary>
        /// Transform to its webapi Model
        /// </summary>
        /// <param name="a"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static MatchInstance AsModel(this Repository.StubDb.Entities.CombinationXpath a, string uri = null)
        {
            return new MatchInstance()
            {
                Id = a.CombinationXpathId,
                XPath = a.Xpath.Type == 0 ? a.Xpath.CleanExpression : null,
                GroupName = a.Xpath.Type == 1 ? a.Xpath.CleanExpression : null,
                Value = a.XpathValue
            }.CreateLink(uri) as MatchInstance;
        }

        /// <summary>
        /// Transform to its webapi Model
        /// </summary>
        /// <param name="a"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static Instance AsModel(this Repository.StubDb.Entities.Combination a, string uri = null)
        {
            return new Instance()
            {
                Id = a.CombinationId,
                Name = a.Description,
                ContentType = a.Response.ContentType,
                HttpStatusCode = a.Response.StatusCode,
                Response = a.Response.ResponseText.Base64Encode(),
                Template = new Template() { Id = a.TemplateId }.CreateLink(uri),
                Matches = a.CombinationXpath.Select(c => c.AsModel(uri))
            }.CreateLink(uri) as Instance;
        }

        /// <summary>
        /// Transform to its webapi Model
        /// </summary>
        /// <param name="a"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static Template AsModel(this Repository.StubDb.Entities.Template a, string uri = null)
        {
            return new Template()
            {
                Id = a.TemplateId,
                Name = a.Description,
                Stub =  new Stub() { Id = a.MessageTypeId }.CreateLink(uri),
                Instances = a.Combination.Select(c => new Instance() { Id = c.CombinationId }.CreateLink(uri) ),
                Matches = a.TemplateXpath.Select(t => new Match() { Id = t.XpathId, XPath = t.Xpath.CleanExpression, Type = t.Xpath.TypeToName()  }.CreateLink(uri))
            }.CreateLink(uri) as Template;
        }

        /// <summary>
        /// Transform to its webapi Model
        /// </summary>
        /// <param name="a"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static Stub AsModel(this Services.ExternalReferenceResolvers.InternalMessageType a, string uri = null)
        {
            return new Stub()
            {
                Id = a.Id,
                Description = a.Description,
                RootNode = a.RootNode,
                Namespace = a.Namespace,
                UriRegex = a.RegEx,
                AllowPassthrough = a.PassthroughEnabled,
                PassthroughUri = a.Uri,  
                Sample = a.Request.Base64Encode(),
            }.CreateLink(uri) as Stub;
            
        }

        /// <summary>
        /// Transform to its webapi Model
        /// </summary>
        /// <param name="a"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static Stub AsModel(this Repository.StubDb.Entities.MessageType a, string uri = null)
        {
            return new Stub()
            {
                Id = a.MessageTypeId,
                Description = a.Description,
                RootNode = a.IsRegEx ? null : a.Rootnode,
                Namespace = a.IsRegEx ? null : (string.IsNullOrEmpty(a.Namespace)?null:a.Namespace),
                UriRegex = a.IsRegEx?a.Namespace:null,
                AllowPassthrough = a.PassthroughEnabled,
                PassthroughUri = a.PassthroughUrl,
                Sample = a.Sample.Base64Encode(),
                Templates = a.Template.Select(t => new Template() { Id = t.TemplateId }.CreateLink(uri) )
            }.CreateLink(uri) as Stub;
        }

        /// <summary>
        /// Transform to its webapi Model
        /// </summary>
        /// <param name="a"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static Trace AsModel(this Repository.StubDb.Entities.StubLog a, string uri = null)
        {
            return new Trace()
            {
                Id = a.StubLogId,
                Data = a.Request.Base64Encode(),
                Stub = a.MessageTypeId.HasValue? new Stub() { Id = a.MessageTypeId.Value}.CreateLink(uri) : null,
                Time = a.ResponseDatumTijd,
                Uri = a.Uri,
                Instance = a.CombinationId.HasValue ? new Template() { Id = a.CombinationId.Value }.CreateLink(uri):null
            }.CreateLink(uri) as Trace;
        }
    }
}
