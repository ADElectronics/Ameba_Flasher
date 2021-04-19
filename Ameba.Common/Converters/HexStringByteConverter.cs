using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Ameba.Common.Converters
{
    public class HexStringByteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string ret = null;

            if (value != null && value is Byte)
            {
                ret = String.Format("0x{0:X2}", (Byte)value);
            }

            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Byte ret = 0x0;
            string str;

            if (value != null && value is string)
            {
                str = (string)value;
                str = str.ToLower();
                str = str.Replace("0x", "");
                if (Byte.TryParse(str, NumberStyles.HexNumber, culture, out ret) != true)
                {
                    return DependencyProperty.UnsetValue;
                }
            }

            return ret;
        }
    }
}
