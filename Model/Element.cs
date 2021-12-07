using System;
using System.Collections.Generic;
using System.Text;
using static Mining_Tool_3.Model.RefinementMethode;

namespace Mining_Tool_3.Model
{
    public class Element
    {
        public static readonly Element QUANTANIUMN = new Element("Quantainium", "QUAN", 44.03, 88.0, 0, 0, "Quantanium", 2.4, 4.8, 14.4);
        public static readonly Element BEXALITE = new Element("Bexalite", "BEXA", 22.01, 43.91, 1, 0, "Bexaliet", 1.0, 2.0, 6.0);
        public static readonly Element TARANITE = new Element("Taranite", "TARA", 17.60, 35.13, 2, 0, "Taraniet", 0.8, 1.6, 4.8);
        public static readonly Element BORASE = new Element("Borase", "BORA", 16.82, 35.21, 3, 0, "Borase", 0.88, 1.76, 5.28);
        public static readonly Element LARANITE = new Element("Laranite", "LARA", 15.41, 30.67, 0, 1, "Laraniet", 0.7, 1.4, 4.2);
        public static readonly Element AGRICIUM = new Element("Agricium", "AGRI", 13.65, 27.47, 1, 1, "Akriecieum", 0.9, 1.8, 5.4);
        public static readonly Element HEPHAESTANITE = new Element("Hephaestanite", "HEPH", 7.92, 15.74, 2, 1, "Hefstaniet", 0.35, 0.7, 1.4);
        public static readonly Element TITANIUM = new Element("Titanium", "TITA", 4.25, 8.88, 3, 1, "Titanium", 0.2, 0.4, 1.2);
        public static readonly Element DIAMOND = new Element("Diamond", "DIAM", 3.63, 7.3, 0, 2, "Diamand", 0.16, 0.32, 0.96);
        public static readonly Element GOLD = new Element("Gold", "GOLD", 2.99, 6.41, 1, 2, "Gold", 0.15, 0.3, 0.9);
        public static readonly Element COPPER = new Element("Copper", "COPP", 2.94, 6.15, 2, 2, "Kupfer", 0.14, 0.28, 0.84);
        public static readonly Element BERYL = new Element("Beryl", "BERY", 2.32, 4.35, 3, 2, "Beryl", 0.1, 0.2, 0.6);
        public static readonly Element TUNGSTEN = new Element("Tungsten", "TUNG", 1.99, 4.01, 0, 3, "Tungsten", 0.1, 0.2, 0.6);
        public static readonly Element CORUNDUM = new Element("Corundum", "CORU", 1.38, 2.7, 1, 3, "Korundum", 0.06, 0.12, 0.36);
        public static readonly Element QUARTZ = new Element("Quartz", "QUAR", 0.77, 1.55, 2, 3, "Quartz", 0.03, 0.06, 0.18);
        public static readonly Element ALUMINUM = new Element("Aluminum", "ALUM", 0.63, 1.29, 3, 3, "Aluminium", 0.03, 0.06, 0.18);
        public static readonly Element INERT = new Element("Inert Material", "Inert Material", 0.02, 0, 0, 0, "none", 0.0, 0.0, 0.0);

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
        public string GermanSpeech { get; private set; }

        private List<KeyValuePair<Cost, double>> _costForMethode;

        public double CostForMethode(Cost cost)
        {
            foreach(var value in _costForMethode)
            {
                if (value.Key == cost)
                {
                    return value.Value;
                }
            }
            return 0.0;
        }

        Element(string name,
            string symbol,
            double value,
            double valueRefined,
            int row,
            int column,
            string germanSpeech,
            double costLow,
            double costMid,
            double costHigh)
        {


            Name = name;
            Symbol = symbol;
            Value = value;
            ValueRefined = valueRefined;
            Row = row;
            Column = column;
            GermanSpeech = germanSpeech;

            _costForMethode = new List<KeyValuePair<Cost, double>>{
            new KeyValuePair<Cost, double>(Cost.XtraLow, costLow),
            new KeyValuePair<Cost, double>(Cost.Low, costLow),
            new KeyValuePair<Cost, double>(Cost.Mid, costMid),
            new KeyValuePair<Cost, double>(Cost.High, costHigh)};
        }
    }
}
