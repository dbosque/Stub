using dBosque.Stub.Repository.StubDb.Entities;
using dBosque.Stub.Editor.Controls.Models.Converters;
using dBosque.Stub.Editor.Models;
using System.ComponentModel;
using System.Drawing.Design;
using dBosque.Stub.Editor.Controls.Models.Editors;

namespace dBosque.Stub.Editor.Controls.Models
{
    /// <summary>
    /// A messagetype property to be able to edit the stub in a propertygrid
    /// </summary>
    public class MessageTypeProperty : MessageTypeItem, IPropertyBase
    { 

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="type"></param>
        public MessageTypeProperty(MessageType type) : base(type)
        { }


        /// <summary>
        /// The collection of matches (xpath/regexgroup) that will trigger this response
        /// </summary>
        [Editor(typeof(RegexEditor), typeof(UITypeEditor)),
         Description("Enter a value to test the RegEx"),
         Category("Testing")]

        /// <summary>
        /// Regex tester
        /// </summary>
      //  [DefaultValue(""), TypeConverter(typeof(MessageTypeRegExConverter)), Description(""), Category("Testing")]
        public string RegExTest { get; set; }

        /// <summary>
        /// The content of the sample message (to be edited in the messageEditor
        /// </summary>
        string IPropertyBase.Message { get { return Sample; } set { Sample = value; } }
    }
}
