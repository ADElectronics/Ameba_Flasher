using System;
using System.Linq;
using System.Windows.Data;

namespace Ameba.Common.Converter
{
    public class HexStringConverter : IValueConverter
    {
        string lastValidValue;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string ret = null;

            if (value != null && value is string)
            {
                char[] chars = ((string)value).ToCharArray();
                var formatted = chars.Select((p, i) => (++i) % 2 == 0 ? String.Concat(p.ToString(), " ") : p.ToString());
                ret = String.Join(String.Empty, formatted).Trim();
            }

            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            object ret = null;

            if (value != null && value is string)
            {
                var valueAsString = ((string)value).Replace(" ", String.Empty).ToUpper();
                ret = lastValidValue = IsHex(valueAsString) ? valueAsString : lastValidValue;
            }

            return ret;
        }

        bool IsHex(string text)
        {
            var reg = new System.Text.RegularExpressions.Regex("^[0-9A-Fa-f]*$");
            return reg.IsMatch(text);
        }
    }
}
