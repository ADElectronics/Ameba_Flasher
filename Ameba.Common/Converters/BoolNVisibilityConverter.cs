using System;
using System.Windows;
using System.Windows.Data;

namespace Ameba.Common.Converters
{
    public class BoolNVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool)value == true)
                return Visibility.Collapsed;
            else
                return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((Visibility)value == Visibility.Collapsed)
                return true;
            else
                return false;
        }
    }
}
