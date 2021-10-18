using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace Mining_Tool_3.View
{
    public class ElementValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double input = (double)value;
            return (input < UpperLimit && input > LowerLimit);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public double UpperLimit { get; set; }
        public double LowerLimit { get; set; }
    }
}
