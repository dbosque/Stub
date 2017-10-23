using dBosque.Stub.Editor.Models;
using System.ComponentModel;

namespace dBosque.Stub.Editor.Controls.Models.Converters
{

    /// <summary>
    /// Convert contentype to a supported dropdown list
    /// </summary>
    public class ContentTypeConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(
                    ITypeDescriptorContext context)
        {
            return true;
        }

        ///<summary>Returns a collection of standard values for the data type this type converter is designed for when provided with a format context.</summary>
        ///<returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> that holds a standard set of valid values, or null if the data type does not support a standard set of values.</returns>
        ///<param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context that can be used to extract additional information about the environment from which this converter is invoked. This parameter or properties of this parameter can be null. </param>
        public override StandardValuesCollection
                 GetStandardValues(ITypeDescriptorContext context)
        {
            // Get the supported contenttype from the configuration
            return new StandardValuesCollection(GlobalSettings.Instance.SupportedContentTypes.ToArray());
        }
    }
}
