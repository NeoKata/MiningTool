using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace Mining_Tool_3.Model
{
    public class Refinery
    {
        public static readonly string ARC_L1 = "ARC-L1";
        public static readonly string CRU_L1 = "CRU-L1";
        public static readonly string HUR_L1 = "HUR-L1";
        public static readonly string HUR_L2 = "HUR-L2";
        public static readonly string MIC_L1 = "MIC-L1";

        public static IEnumerable<string> Refineries
        {
            get
            {
                yield return ARC_L1;
                yield return CRU_L1;
                yield return HUR_L1;
                yield return HUR_L2;
                yield return MIC_L1;
            }
        }

        public string Name { get; private set; }
        public string Symbol { get; private set; }


        ObservableCollection<KeyValuePair<Element, ElementValue>> _cargo;
        public ObservableCollection<KeyValuePair<Element, ElementValue>> Cargo
        {
            get
            {
                if (_cargo == null)
                {
                    _cargo = new ObservableCollection<KeyValuePair<Element, ElementValue>>();
                }
                return _cargo;
            }
        }

        private double value = 0.0;
        public double Value {
            get { return value; }
            set { 
                this.value = value;
                if(this.value > MaxValue)
                {
                    MaxValue = this.value;
                }
                if(this.value < MinValue)
                {
                    MinValue = this.value;
                }
            }
        }
        public static double MinValue = double.MaxValue;
        public static double MaxValue = 0.0;
        public HashSet<KeyValuePair<Element, double>> YieldModifier { get; private set; }
        public Refinery(string name, string symbol, HashSet<KeyValuePair<Element, double>> yield) => (Name, Symbol, YieldModifier) = (name, symbol, yield);

        public double GetYield(Element element)
        {
          
            foreach (KeyValuePair<Element, double> pair in YieldModifier)
            {
                if (pair.Key == element)
                {                   
                    return pair.Value;
                }
            }
            return 0.0;
        }

    }
}
