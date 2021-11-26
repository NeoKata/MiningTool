using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace Mining_Tool_3.View
{
    public class PercentValueConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double input = (double)value;
           if (input > 0)
            {
                return 1;
            }
            if (input < 0)
            {
                return -1;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public double UpperLimit { get; set; }
        public double LowerLimit { get; set; }
    }
}
