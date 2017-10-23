using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;


namespace dBosque.Stub.Sockets.Models
{
    /// <summary>
    /// List of open connections
    /// </summary>
#pragma warning disable S3925 // "ISerializable" should be implemented correctly
    public class ClientConnectionList : Dictionary<string, ClientConnection>
#pragma warning restore S3925 // "ISerializable" should be implemented correctly
    {
        public void CloseAll()
        {
            Values.ToList().ForEach(a => a.Dispose());
            Clear();
        }
        public void Disconnect(ClientConnection s)
        {
            if (s == null)
                return;           
            if (ContainsKey(s.Id))
                this[s.Id].Dispose();
            Remove(s.Id);
        }
        //public ClientConnection GetOrAdd(Socket s)
        //{
        //    if (!ContainsKey(s.RemoteEndPoint.ToString()))
        //        AddConnection(s);
        //    return this[s.RemoteEndPoint.ToString()];
        //}
        public void AddConnection(ClientConnection s)
        {            
            Add(s.Id, s);
        }
    }
}
