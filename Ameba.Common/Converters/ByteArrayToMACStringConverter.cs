using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;

namespace Ameba.Common.Converters
{
    public class ByteArrayToMACStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            byte[] bytesValue = value as byte[];

            if (value == null || targetType != typeof(string))
                return DependencyProperty.UnsetValue;

            return string.Format(CultureInfo.InvariantCulture, 
                "{0:X2}:{1:X2}:{2:X2}:{3:X2}:{4:X2}:{5:X2}",
                                 bytesValue[0],
                                 bytesValue[1],
                                 bytesValue[2],
                                 bytesValue[3],
                                 bytesValue[4],
                                 bytesValue[5]);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string mac = value as string;
            string[] hexes = mac.Split(':');
            Regex r = new Regex("^[0-9a-fA-F]{2}(((:[0-9a-fA-F]{2}){5})|((-[0-9a-fA-F]{2}){5}))$");
            byte[] bytesValue = new byte[6];

            if (hexes.Length != 6)
                return DependencyProperty.UnsetValue;

            if (r.IsMatch(mac))
            {
                for (byte i = 0; i < hexes.Length; i++)
                {
                    bytesValue[i] = Byte.Parse(hexes[i], NumberStyles.HexNumber);
                }
                return bytesValue;
            }

            return DependencyProperty.UnsetValue;
        }
    }
}
