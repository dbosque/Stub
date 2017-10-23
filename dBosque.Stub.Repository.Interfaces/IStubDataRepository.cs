using dBosque.Stub.Interfaces;
using System;
using System.Collections.Generic;
using dBosque.Stub.Repository.StubDb.Entities;

namespace dBosque.Stub.Repository.Interfaces
{
    public interface IStubDataRepository
    {
        /// <summary>
        /// The provider used by this repository
        /// </summary>
        string Provider { get; }    

        /// <summary>
        /// Update the message with the found matche(s)
        /// </summary>
        /// <param name="message"></param>
        void GetStubForMessage<T>(IStubMessage<T> message);

        /// <summary>
        /// Log a message to the database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <param name="tenant"></param>
        /// <param name="uri"></param>
        /// <param name="messageTypeId"></param>
        void Log(long? id, string request, long? tenant, string uri, long? messageTypeId);

        /// <summary>
        /// Flush the database connection and create a new one.
        /// </summary>
        void Flush();

        /// <summary>
        /// The current connection
        /// </summary>
        string Connection { get; }

        /// <summary>
        /// Return all messagetypes
        /// </summary>
        /// <returns></returns>
        IEnumerable<MessageType> GetMessageTypes();

        /// <summary>
        /// Return the messageType with the given id
        /// </summary>
        /// <param name="messageTypeId"></param>
        /// <returns></returns>
        MessageType GetMessageType(long messageTypeId);

        /// <summary>
        /// Get all templated for a specific messagetype
        /// </summary>
        /// <param name="messageTypeId"></param>
        /// <returns></returns>
        IEnumerable<Template> GetTemplates(long messageTypeId);

        /// <summary>
        /// Get all templates
        /// </summary>
        /// <returns></returns>
        IEnumerable<Template> GetTemplates();

        /// <summary>
        /// Get a specific template for the given Id
        /// </summary>
        /// <param name="templateId"></param>
        /// <returns></returns>
        Template GetTemplate(long templateId);

        /// <summary>
        /// Get a specific combination for the given id
        /// </summary>
        /// <param name="combId"></param>
        /// <returns></returns>
        Combination GetCombination(long combId);

        /// <summary>
        /// Get all combinations.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Combination> GetCombinations();

        /// <summary>
        /// Get an xpath for the given id
        /// </summary>
        /// <param name="comboXpathId"></param>
        /// <returns></returns>
        CombinationXpath GetCombinationXpath(long comboXpathId);

        /// <summary>
        /// Return the response with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Response GetResponse(long id);

        /// <summary>
        /// Get all available xpaths
        /// </summary>
        /// <returns></returns>
        IEnumerable<Xpath> GetXpaths();

        /// <summary>
        /// Get a specific xpath
        /// </summary>
        /// <param name="xpathId"></param>
        /// <returns></returns>
        Xpath GetXpath(long xpathId);

        /// <summary>
        /// Update a template
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateAction"></param>
        /// <returns></returns>
        Template UpdateTemplate(long? id, Action<Template> updateAction);

        /// <summary>
        /// Update a response
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateAction"></param>
        /// <returns></returns>
        Response UpdateResponse(long? id, Action<Response> updateAction);

        /// <summary>
        /// Update a specific Xpath combo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateAction"></param>
        /// <returns></returns>
        CombinationXpath UpdateCombinationXpath(long? id, Action<CombinationXpath> updateAction);

        /// <summary>
        /// Update a messagetype
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateAction"></param>
        /// <returns></returns>
        MessageType UpdateMessageType(long? id, Action<MessageType> updateAction);

        /// <summary>
        /// Update a combination
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateAction"></param>
        /// <returns></returns>
        Combination UpdateCombination(long? id, Action<Combination> updateAction);

        /// <summary>
        /// Update an xpath
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateAction"></param>
        /// <returns></returns>
        Xpath UpdateXpath(long? id, Action<Xpath> updateAction);

        /// <summary>
        /// Clone a complete tree item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        long CloneResponseTree(long id);

        /// <summary>
        /// Check if a given xpath is in use by a combination
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        bool IsInUse(Xpath path);

        /// <summary>
        /// Check if a given template Id has shared combination
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool HasSharedCombinationsWithOther(long id);

        /// <summary>
        /// Delete the specific xpath
        /// </summary>
        /// <param name="path"></param>
        void DeleteXpath(Xpath path);

        /// <summary>
        /// Delete a combination and the complete tree below it.
        /// </summary>
        /// <param name="id"></param>
        void DeleteCombinationDown(long id);

        /// <summary>
        /// Delete a messagetype
        /// </summary>
        /// <param name="id"></param>
        void DeleteMessageType(long id);

        /// <summary>
        /// Delete a template and the complete tree of combinations and response below it.
        /// </summary>
        /// <param name="id"></param>
        void DeleteTemplateDown(long id);

        /// <summary>
        /// Save the current settings.
        /// </summary>
        void Save();

        /// <summary>
        /// Get all logging
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IEnumerable<StubLog> GetLogs(System.Linq.Expressions.Expression<Func<StubLog, bool>> filter);

        /// <summary>
        /// Get all logging
        /// </summary>
        /// <param name="size"></param>
        /// <param name="prevValue"></param>
        /// <param name="negative"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable<stp_selectLog_Result> GetLogs(int size, int prevValue, bool negative, string query);
    }
}
