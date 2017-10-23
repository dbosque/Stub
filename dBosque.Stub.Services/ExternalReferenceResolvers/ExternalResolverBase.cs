using dBosque.Stub.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace dBosque.Stub.Services.ExternalReferenceResolvers
{
    public abstract class ExternalResolverBase
    {
        /// <summary>
        /// Retrieve the required data from the resource
        /// </summary>
        /// <param name="data">The data to resolve</param>
        /// <returns></returns>
        public IEnumerable<ExternalMessageType> Execute(string data)
        {
            try
            {
                var v = new UriBuilder(data);
                return FromUri(data);
            }
            catch (UriFormatException)
            {
                return FromContent(data);
            }
        }

        /// <summary>
        /// Retrieve the required data from the resource and crossreference it with the given repository
        /// </summary>
        /// <param name="data">The data to resolve</param>
        /// <param name="repository">The repository to crossreference with</param>
        /// <returns></returns>
        public IEnumerable<InternalMessageType> Execute(string data, IStubDataRepository repository)
        {
            return Crossreference(Execute(data), repository);
        }

        /// <summary>
        /// Crossreference the messagetype with the repository 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="repository"></param>
        /// <returns></returns>
        private IEnumerable<InternalMessageType> Crossreference(IEnumerable<ExternalMessageType> data, IStubDataRepository repository)
        {
            var types = repository.GetMessageTypes();
            return data
                .Select(d => new InternalMessageType(d))
                .Select(a => a.UpdateMessageType(types.FirstOrDefault(m => m.Namespace == a.Namespace && m.Rootnode == a.RootNode)));
        }

        /// <summary>
        /// Extract the messageTypes from the given uri
        /// </summary>
        /// <param name="uri">The url to retrieve</param>
        /// <returns></returns>
        protected abstract IEnumerable<ExternalMessageType> FromUri(string uri);

        /// <summary>
        /// Extract the messageTypes from the given content
        /// </summary>
        /// <param name="content">The content to parse</param>
        /// <returns></returns>
        protected abstract IEnumerable<ExternalMessageType> FromContent(string content);
    }
}
