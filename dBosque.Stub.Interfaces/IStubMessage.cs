using System.Collections.Generic;

namespace dBosque.Stub.Interfaces
{
    public interface IStubMessage<T>
    {
        /// <summary>
        /// The (log) id of this message
        /// </summary>
        MessageId Id { get; }
        /// <summary>
        /// The error description of the message
        /// </summary>
        string Description { get; }

        /// <summary>
        /// The original sender of the message
        /// </summary>
        string Sender { get; set; }

        /// <summary>
        /// The actual request received
        /// </summary>
        string Request              { get; set; }

        /// <summary>
        /// The configured response to return (if any)
        /// </summary>
        string Response             { get; set; }

        /// <summary>
        /// The rootnode extracted from the request
        /// </summary>
        string RootNode             { get; set; }

        /// <summary>
        /// The root namespace extracted from the request.
        /// </summary>
        string RootNameSpace        { get; set; }

        /// <summary>
        /// A list of found matches
        /// </summary>
        StubMatchList Matches       { get; set; }

        /// <summary>
        /// The statuscode to return
        /// </summary>
        int HttpStatusCode          { get; set; }

        /// <summary>
        /// The Raw request
        /// </summary>
        string RawRequest           { get; set; }

        /// <summary>
        /// The contenttype to return
        /// </summary>
        string ContentType          { get; set; }

        /// <summary>
        /// Indicator if for this message(type) a passthrough is configured
        /// </summary>
        bool IsPassTrough { get; set; }

        /// <summary>
        /// The URL to pass the request forward to.
        /// </summary>
        string PassthroughUri { get; set; }

        /// <summary>
        /// The tenant on which the request was received
        /// </summary>
        string Tenant { get; set; }

        /// <summary>
        /// The ID of the tenant on which the request was received.
        /// </summary>
        long? TenantId { get; set; }

        /// <summary>
        /// The URI on which the request was received
        /// </summary>
        string Uri { get; set; }

        /// <summary>
        /// Indicator to signal if any match was found or not.
        /// </summary>
        bool HasMatch { get; }


        /// <summary>
        /// Indicator to signal if there are multiple matches found.
        /// </summary>
        bool HasMultipleMatches { get; }

        /// <summary>
        /// Localize a xpath
        /// </summary>
        /// <param name="xpath"></param>
        /// <returns></returns>
        string LocalizeXpath(string xpath);

        /// <summary>
        /// Relay the message to the specific uri.
        /// </summary>
        /// <param name="uri"></param>
        void Relay(string uri);

        /// <summary>
        /// Return a valid result message
        /// </summary>
        /// <returns></returns>
        T AsResult();

        /// <summary>
        /// Return a valid UnAuthorized message
        /// </summary>
        /// <returns></returns>
        T AsUnauthorized();

        /// <summary>
        /// Should any postprocessing be done.
        /// </summary>
        bool hasPostProcessing { get; }

        /// <summary>
        /// Possible headers to return to client
        /// </summary>
        Dictionary<string, string[]> ResponseHeaders { get; set; }
    }

   

  

   
}
