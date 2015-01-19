using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DesktopBackgroundChanger.ValueConverters
{
	public class InverseBooleanToVisibilityConverter : IValueConverter
	{
		private readonly IValueConverter _originalConverter;

		public InverseBooleanToVisibilityConverter()
		{
			_originalConverter = new BooleanToVisibilityConverter();
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ((Visibility)_originalConverter.Convert(value, targetType, parameter, culture)) == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return !((bool)_originalConverter.ConvertBack(value, targetType, parameter, culture));
		}
	}
}