using dBosque.Stub.Socket.Client.Exception;
using dBosque.Stub.Socket.Client.Parsing;
using System;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Xml.Serialization;

namespace dBosque.Stub.Socket.Client.Process
{    

    /// <summary>
    /// Proxy for all external calls to the actual stub
    /// </summary>
    public sealed class StubProxy
    {
        private readonly string _server;
        private readonly int _port;

        /// <summary>
        /// Create a new proxy
        /// </summary>
        /// <param name="server">The server to call.</param>
        /// <param name="port">The port to call the server on.</param>
        /// <returns></returns>
        public static StubProxy Create(string server, int port) => new StubProxy(server, port);

        /// <summary>
        /// Create a new proxy
        /// </summary>
        /// <param name="server">The server:port to use</param>
        /// <returns></returns>
        public static StubProxy Create(string server)
        {
            var con = server.Split(':');
            if (con.Length != 2)
                throw new StubNotAvailableException(server);
            return new StubProxy(con[0], int.Parse(con[1]));
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="server"></param>
        /// <param name="port"></param>
        private StubProxy(string server, int port)
        {
            _server = server;
            _port = port;
        }

        /// <summary>
        /// Retrieve a serialized object
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="command">The command to execute</param>
        /// <returns></returns>
        public StubProxyResult<TResult> ExecuteClass<TResult>(DbCommand command) where TResult : class, new()
        {
            var msg = DbCommandParser.Parse<TResult>(command);
            int? errorCode = null;
            using (var connection = new StubServerConnection(_server, _port))
            {
                var res = connection.Send(msg, out errorCode);
                if (errorCode.HasValue)
                    return new StubProxyResult<TResult>(null, res, errorCode);

                // Serialize to the expected result class
                var serializer = new XmlSerializer(typeof(TResult));
                return new StubProxyResult<TResult>((TResult)serializer.Deserialize(new StringReader(msg)));
            }
        }

        /// <summary>
        /// Return a DbDataReader
        /// </summary>
        /// <param name="command">The command to execute</param>
        /// <returns></returns>
        public StubProxyResult<DbDataReader> ExecuteReader(DbCommand command)    
        {
            var msg = DbCommandParser.Parse<DbDataReader>(command);
            int? errorCode = null;
            using (var connection = new StubServerConnection(_server, _port))
            {
                var res = connection.Send(msg, out errorCode);
                if (errorCode.HasValue)
                    return new StubProxyResult<DbDataReader>(null, res, errorCode);

                // Create a datareader from the return data.
                // The returned data should contain an inline xmlschema
                using (var reader = new StringReader(res))
                {
                    var data = new DataSet("DS");
                    data.ReadXml(reader, XmlReadMode.ReadSchema);
                    return new StubProxyResult<DbDataReader>(data.CreateDataReader());
                }
            }
        }

        /// <summary>
        /// Return a simple result type
        /// </summary>
        /// <typeparam name="TResult">The type of data to return</typeparam>
        /// <param name="command">The command to execute</param>
        /// <returns></returns>
        public StubProxyResult<TResult> Execute<TResult>(DbCommand command) 
        {
            var msg = DbCommandParser.Parse<TResult>(command);
            int? errorCode = null;
            var typecode = Type.GetTypeCode(typeof(TResult));
            using (var connection = new StubServerConnection(_server, _port))
            {
                var res = connection.Send(msg, out errorCode);
                if (errorCode.HasValue)
                    return new StubProxyResult<TResult>(default(TResult), res, errorCode);

                // Convert to the actual expected type
                if (!typeof(TResult).IsClass || typeof(TResult) == typeof(string))
                {
                    object response;
                    switch (typecode)
                    {
                        case TypeCode.Int16:
                        case TypeCode.Int32:
                        case TypeCode.Int64:
                            response = int.Parse(res);
                            break;
                        case TypeCode.Boolean:
                            response = Convert.ToBoolean(res);
                            break;
                        case TypeCode.String:
                            response = msg;
                            break;
                        default:
                            response = default(TResult);
                            break;
                    }
                    return new StubProxyResult<TResult>((TResult)response);
                }
             
                else
                {
                    // Try to serialize to an object
                    var serializer = new XmlSerializer(typeof(TResult));
                    return new StubProxyResult<TResult>((TResult)serializer.Deserialize(new StringReader(msg)));
                }
            }
        }          
    }
}

