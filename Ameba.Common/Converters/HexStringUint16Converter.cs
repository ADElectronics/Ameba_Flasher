using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Ameba.Common.Converters
{
    public class HexStringUint16Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string ret = null;

            if (value != null && value is UInt16)
            {
                ret = String.Format("0x{0:X4}", (UInt16)value);
            }

            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            UInt16 ret = 0x0;
            string str;

            if (value != null && value is string)
            {
                str = (string)value;
                str = str.ToLower();
                str = str.Replace("0x", "");
                if (UInt16.TryParse(str, NumberStyles.HexNumber, culture, out ret) != true)
                {
                    return DependencyProperty.UnsetValue;
                }
            }

            return ret;
        }
    }
}
