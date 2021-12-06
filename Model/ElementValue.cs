using Mining_Tool_3.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Mining_Tool_3.Model
{
    public class ElementValue
    {
        public double Yield { get; set; }
        public double Value { get; set; }

        public ElementValue(double yield, double value) => (Yield, Value) = (yield, value);
    }
}
