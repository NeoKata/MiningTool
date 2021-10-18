using Mining_Tool_3.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mining_Tool_3.ViewModel
{
    public class CargoItemVM : BaseVM
    {
        private double _scu;

        public Element Element { get; set; }
        public double SCU { get { return _scu; } set { _scu = value; OnPropertyChanged("Value"); OnPropertyChanged("SCU"); } }
        public double Value { get { return SCU*100*Element.Value;}}

        public CargoItemVM(Element element, double scu) => (Element, SCU) = (element, scu);
    }
}
