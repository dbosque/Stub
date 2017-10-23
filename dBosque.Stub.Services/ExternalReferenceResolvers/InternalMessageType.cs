using dBosque.Stub.Repository.StubDb.Entities;
using dBosque.Stub.Services.Extensions;

namespace dBosque.Stub.Services.ExternalReferenceResolvers
{
    /// <summary>
    /// An info object for the retrieved ExternalMessageType combined with a possible database reference
    /// </summary>
    public class InternalMessageType : ExternalMessageType
    {
        public InternalMessageType(ExternalMessageType msg)
            : base(msg)
        {
            var info = msg.GetDocumentInfo();
            RootNode = info.RootNode;
            Namespace = info.Namespace;
        }
        public bool? PassthroughEnabled { get; set; }
        public long? Id { get; set; }
        public string RootNode { get; set; }
        public string Namespace { get; set; }

        public InternalMessageType UpdateMessageType(MessageType type)
        {
            Id = type?.MessageTypeId;
            Uri = type?.PassthroughUrl ?? Uri;
            PassthroughEnabled = type?.PassthroughEnabled;
            Description = type?.Description ?? Description;
            return this;
        }


    }
}
