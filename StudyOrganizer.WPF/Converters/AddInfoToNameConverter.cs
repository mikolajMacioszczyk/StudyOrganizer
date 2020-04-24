using System;
using System.Globalization;
using System.Windows.Data;

namespace StudyOrganizer.WPF.Converters
{
    [ValueConversion(typeof(string),typeof(string))]
    public class AddInfoToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return parameter.ToString() + value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}