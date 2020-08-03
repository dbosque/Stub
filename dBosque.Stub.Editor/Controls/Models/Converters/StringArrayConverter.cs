using System;
using System.ComponentModel;
using System.Globalization;

namespace dBosque.Stub.Editor.Controls.Models.Converters
{
    public class StringArrayConverter : ArrayConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destType)
        {
            if (destType == typeof(string))
            {
               return string.Join(",",(string[])value);
            }
            return base.ConvertTo(context, culture, value, destType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string s = value as string;

            if (!string.IsNullOrEmpty(s))
            {
                return ((string)value).Split(',');
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}
