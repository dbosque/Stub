using System.ComponentModel;
using dBosque.Stub.Repository.StubDb.Entities;

namespace dBosque.Stub.Editor.Models
{
    /// <summary>
    /// Item voor het displayen van de verschillende messagetypes
    /// </summary>
    public class MessageTypeItem
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public MessageTypeItem()
        { }

        /// <summary>
        /// Create a messagetypeItem from the database
        /// </summary>
        /// <param name="type"></param>
        public MessageTypeItem(MessageType type)
        {
            Org = type;
            NameSpace = type.Namespace;
            RootNode = type.Rootnode;
            Description = type.Description;
            Id = type.MessageTypeId;
            AllowPassthrough = type.PassthroughEnabled;
            PassthroughUri = type.PassthroughUrl;
            Sample = type.Sample;
        }

        /// <summary>
        /// Add the object as Tag so we can retrieve it.
        /// Het 'hoofd' item bevat de namespace (eerste column)
        /// Subitems bevat de Rootnode (tweede column)
        /// </summary>
        /// <returns></returns>

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public MessageType Org { get; set; }
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public long? Id { get; set; }

        [Description("The namespace or Regex to trigger upon."), DisplayName("Namespace/Regex"), DefaultValue(""), Category("Trigger")]
        public string NameSpace { get; set; }

        [Description("The rootnode to trigger upon."), DefaultValue(""), Category("Trigger")]
        public string RootNode { get; set; }

        [Description("A nice description."), DefaultValue("")]
        public string Description { get; set; }

        [Description("Indicator to allow passthrough in case a match has not been found."), Category("Passthrough"), DisplayName("Enabled"), DefaultValue(false)]
        public bool AllowPassthrough { get; set; }

        [DefaultValue(false), Description("The url to pass the message through in case no match could be found."), Category("Passthrough")]
        public string PassthroughUri { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public string Sample { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public string PassThroughLayout => string.Format("{0}{1}", AllowPassthrough ? "(enabled )" : "(disabled)", string.IsNullOrEmpty(PassthroughUri) ? "" : ": " + PassthroughUri);
    }
}
