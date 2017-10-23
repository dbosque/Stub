using dBosque.Stub.Editor.Controls.Models.Descriptors;
using System;
using System.ComponentModel;

namespace dBosque.Stub.Editor.Controls.Models.Converters
{
    /// <summary>
    /// Convertor to show a collection in the propertygrid
    /// </summary>
    internal class MatchPropertyCollectionConverter : ExpandableObjectConverter
    {
       ///<summary>Converts the given value object to the specified type, using the specified context and culture information.</summary>
       ///<param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
       ///<param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If null is passed, the current culture is assumed. </param>
       ///<param name="value">The <see cref="T:System.Object" /> to convert. </param>
       ///<param name="destinationType">The <see cref="T:System.Type" /> to convert the <paramref name="value" /> parameter to. </param>
       ///<returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
       ///<exception cref="T:System.ArgumentNullException">The <paramref name="destinationType" /> parameter is null. </exception>
       ///<exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
       
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destType)
        {
            if (destType == typeof(string) && value is MatchPropertyCollection)
            {                               
                return "Available matches.";
            }
            return base.ConvertTo(context, culture, value, destType);
        }
    }
}
