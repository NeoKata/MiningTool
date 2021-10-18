using System;
using System.Collections.Generic;
using System.Text;

namespace Mining_Tool_3.Model
{
    public class Mineral
    {
        private double _percentage;

        public Element Element { get; set; }
        public Stone Stone { get; set; }

        public double Percentage
        {
            get
            {
                if (Element == Element.INERT)
                {
                    double sum = 1.0;
                    foreach (Mineral mineral in Stone.Minerals)
                    {
                        sum -= mineral.Percentage;
                    }
                    return sum;
                }
                return _percentage;
            }
            set { _percentage = value; }
        }

        public double Mass { get { return Stone.Mass * 2 * Percentage; } }
        public double Value { get { return (Math.Truncate(Mass) * Element.Value); } }
        public double CargoValue { get { return Cargo * 100.0 * Element.Value; } }
        public double Cargo
        {            
            get { return Math.Truncate(((Stone.Ship.CScu / 100.0) * CargoPercent) * 100.0) / 100.0; }
        }
        public double CargoPercent { get { return Math.Round((Stone.Ship.CScu <= Mass ? Stone.Ship.CScu : Mass) / Stone.Ship.CScu,4) ; } }

        public Mineral(Element element, Stone stone) => (Element, Stone, Percentage) = (element, stone, 0);

        public Mineral(Mineral mineral)
        {
            _percentage = mineral.Percentage;
            Element = mineral.Element;
            Stone = mineral.Stone;
        }

    }
}
