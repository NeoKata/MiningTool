using System;
using System.Collections.Generic;
using System.Text;

namespace Mining_Tool_3.Model
{
    public class Stone
    {

        public Ship Ship { get; set; }

        public double Mass { get; set; }

        private Mineral _inert;
        public Mineral Inert { get { return _inert; } set { _inert = value; } }

        HashSet<Mineral> _minerals;
        public HashSet<Mineral> Minerals
        {
            get
            {
                if (_minerals == null)
                {
                    _minerals = new HashSet<Mineral>();
                }
                return _minerals;
            }
        }

        public double Value
        {
            get
            {
                double sum = Inert.Value;
                foreach (Mineral mineral in Minerals)
                {
                    sum += mineral.Value;
                }
                return sum;
            }
        }

        public Stone(Ship ship)
        {
            Ship = ship;
            _inert = new Mineral(Element.INERT, this);
        }

        public Stone(Mineral mineral)
        {           
            _inert = mineral;
        }

        public Mineral AddMineral(Element element)
        {
            Mineral mineral = new Mineral(element, this);
            Minerals.Add(mineral);
            return mineral;
        }

        public string BestMineral
        {
            get
            {
                double value = 0;
                string name = "Empty";
                foreach (Mineral mineral in Minerals)
                {
                    if (mineral.CargoValue > value)
                    {
                        value = mineral.CargoValue;
                        name = mineral.Element.Symbol;
                    }                    
                }
                return name;
            }
        }
    }
}