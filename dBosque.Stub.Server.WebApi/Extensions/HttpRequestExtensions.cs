using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace dBosque.Stub.Server.WebApi.Extensions
{
    public static class HttpRequestExtensions
    {

        /// <summary>
        /// Get the body of the request in a save way
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetBody(this HttpRequest request)
        {
            var bodyStr = "";

            // Allows using several time the stream in ASP.Net Core
            request.EnableRewind();

            // Arguments: Stream, Encoding, detect encoding, buffer size 
            // AND, the most important: keep stream opened
            using (StreamReader reader
                      = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
            {
                bodyStr = reader.ReadToEnd();
            }

            // Rewind, so the core is not lost when it looks the body for the request
            request.Body.Position = 0;
            return bodyStr;
        }
    }
}
