using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace StudyOrganizer.WPF.Converters
{
    [ValueConversion(typeof(bool),typeof(Visibility))]
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool visibility = value is bool ? (bool) value : false;
            if (visibility)
            {
                return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}