using dBosque.Stub.Editor.Controls.Models.Converters;
using dBosque.Stub.Editor.Controls.Models.Descriptors;
using dBosque.Stub.Editor.Controls.Models.Editors;
using System.ComponentModel;
using System.Drawing.Design;

namespace dBosque.Stub.Editor.Controls.Models
{
    /// <summary>
    /// A response property to be able to edit the template instance in a propertygrid
    /// </summary>
    public class ResponseProperty : IPropertyBase
    {
        /// <summary>
        /// The id in the database
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public long? Id { get; set; }

        /// <summary>
        /// The name of the template instance
        /// </summary>
        [Description("Instance name"), Category("Instance")]
        public string Name { get; set; }

        ///<summary>
        ///The message of the Propety to modify
        ///</summary>
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public string Message { get; set; }

        /// <summary>
        /// The contenttype to return
        /// </summary>
        [TypeConverter(typeof(ContentTypeConverter)), 
         Description("Contenttype to return"), 
         Category("Response"), 
         DefaultValue("application/json")]    
        public string ContentType { get; set; }

        /// <summary>
        /// The statuscode to return
        /// </summary>
        [TypeConverter(typeof(StatusCodeConverter)), 
         Description("Statuscode to return"), 
         Category("Response"),
         DefaultValue("OK (200)")]
        public string StatusCode { get; set; }

        /// <summary>
        /// The collection of matches (xpath/regexgroup) that will trigger this response
        /// </summary>
        [Editor(typeof(CustomEditor), typeof(UITypeEditor)),
         Description("Matches"),
         Category("Response"), 
         TypeConverter(typeof(MatchPropertyCollectionConverter))]
        public MatchPropertyCollection Matches { get; set; }
    }

   

   
}
