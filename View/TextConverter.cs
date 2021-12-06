using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace Mining_Tool_3.View
{
    public class TextConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           string input = (string)value;
           return !String.IsNullOrEmpty(input);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
