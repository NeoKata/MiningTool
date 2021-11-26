using System;
using System.Collections.Generic;
using System.Text;

namespace Mining_Tool_3.Model
{
    public class Element
    {
        public static readonly Element QUANTANIUMN = new Element("Quantainium", "QUAN", 44.03, 88.0, 0, 0);
        public static readonly Element BEXALITE = new Element("Bexalite", "BEXA", 22.01, 43.91, 1, 0);
        public static readonly Element TARANITE = new Element("Taranite", "TARA", 17.60, 35.13, 2, 0);
        public static readonly Element BORASE = new Element("Borase", "BORA", 16.82, 35.21, 3, 0);
        public static readonly Element LARANITE = new Element("Laranite", "LARA", 15.41, 30.67, 0, 1);
        public static readonly Element AGRICIUM = new Element("Agricium", "AGRI", 13.65, 27.47, 1, 1);
        public static readonly Element HEPHAESTANITE = new Element("Hephaestanite", "HEPH", 7.92, 15.74, 2, 1);
        public static readonly Element TITANIUM = new Element("Titanium", "TITA", 4.25, 8.88, 3, 1);
        public static readonly Element DIAMOND = new Element("Diamond", "DIAM", 3.63, 7.3, 0, 2);
        public static readonly Element GOLD = new Element("Gold", "GOLD", 2.99, 6.41, 1, 2);
        public static readonly Element COPPER = new Element("Copper", "COPP", 2.94, 6.15, 2, 2);
        public static readonly Element BERYL = new Element("Beryl", "BERY", 2.32, 4.35, 3, 2);
        public static readonly Element TUNGSTEN = new Element("Tungsten", "TUNG", 1.99, 4.01, 0, 3);
        public static readonly Element CORUNDUM = new Element("Corundum", "CORU", 1.38, 2.7, 1, 3);
        public static readonly Element QUARTZ = new Element("Quartz", "QUAR", 0.77, 1.55, 2, 3);
        public static readonly Element ALUMINUM = new Element("Aluminum", "ALUM", 0.63, 1.29, 3, 3);
        public static readonly Element INERT = new Element("Inert Material", "Inert Material", 0.02, 0, 0, 0);

        public static IEnumerable<Element> ByValues
        {
            get
            {
                yield return QUANTANIUMN;
                yield return BEXALITE;
                yield return TARANITE;
                yield return BORASE;
                yield return LARANITE;
                yield return AGRICIUM;
                yield return HEPHAESTANITE;
                yield return TITANIUM;
                yield return DIAMOND;
                yield return GOLD;
                yield return COPPER;
                yield return BERYL;
                yield return TUNGSTEN;
                yield return CORUNDUM;
                yield return QUARTZ;
                yield return ALUMINUM;
            }
        }

        public string Name { get; private set; }
        public string Symbol { get; private set; }
        public double Value { get; private set; }
        public double ValueRefined { get; private set; }
        public int Row { get; set; }
        public int Column { get; set; }

        Element(string name, string symbol, double value, double valueRefined, int row, int column) => (Name, Symbol, Value, ValueRefined, Row, Column) = (name, symbol, value, valueRefined, row, column);
    }
}
