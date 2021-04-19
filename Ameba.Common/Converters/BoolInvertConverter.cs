using System;
using System.Windows.Data;

namespace Ameba.Common.Converters
{
	public class BoolInvertConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if ((bool)value == true)
				return false;
			else
				return true;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if ((bool)value == true)
				return false;
			else
				return true;
		}
	}
}
