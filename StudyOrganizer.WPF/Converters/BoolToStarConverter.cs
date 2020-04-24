using System;
using System.Globalization;
using System.Windows.Data;

namespace StudyOrganizer.WPF.Converters
{
    [ValueConversion(typeof(bool), typeof(string))]
    public class BoolToStarConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isTrue = value is bool ? (bool) value : false;
            if (isTrue)
            {
                return "../Icones/gold_star.png";
            }

            return "../Icones/blank_star.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string text = value.ToString();
            if (text == null)
            {
                return true;
            }
            if (text.Equals(@"..\gold_star.png"))
            {
                return true;
            }

            return false;
        }
    }
}