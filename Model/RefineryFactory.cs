using System;
using System.Collections.Generic;
using System.Text;

namespace Mining_Tool_3.Model
{
    public class RefineryFactory
    {
        public static Refinery getRefinery(string name)
        {
            switch (name)
            {
                case "ARC-L1":
                    return new Refinery("Wide Forest Station", "ARC-L1",
     new HashSet<KeyValuePair<Element, double>>{
                new KeyValuePair<Element, double>(Element.GOLD,-0.06),
                new KeyValuePair<Element, double>(Element.AGRICIUM,0.02),
                new KeyValuePair<Element, double>(Element.BERYL,-0.09),
                new KeyValuePair<Element, double>(Element.CORUNDUM,0.02),
                new KeyValuePair<Element, double>(Element.DIAMOND,0.04),
                new KeyValuePair<Element, double>(Element.HEPHAESTANITE,-0.02),
                new KeyValuePair<Element, double>(Element.LARANITE,0.04),
                new KeyValuePair<Element, double>(Element.TARANITE,0.03),
                new KeyValuePair<Element, double>(Element.ALUMINUM,0.04),
                new KeyValuePair<Element, double>(Element.BORASE,-0.03)
     });
                case "CRU-L1":
                    return new Refinery("Ambitious Dream Station", "CRU-L1",
           new HashSet<KeyValuePair<Element, double>>{
                new KeyValuePair<Element, double>(Element.GOLD,0.04),
                new KeyValuePair<Element, double>(Element.TITANIUM,0.04),
                new KeyValuePair<Element, double>(Element.QUANTANIUMN,0.03),
                new KeyValuePair<Element, double>(Element.BEXALITE,0.05),
                new KeyValuePair<Element, double>(Element.CORUNDUM,-0.04),
                new KeyValuePair<Element, double>(Element.TUNGSTEN,0.02)
           });
                case "HUR-L1":
                    return new Refinery("Green Glade Station", "HUR-L1",
                        new HashSet<KeyValuePair<Element, double>>{
                new KeyValuePair<Element, double>(Element.TITANIUM,0.02),
                new KeyValuePair<Element, double>(Element.QUANTANIUMN,-0.06),
                new KeyValuePair<Element, double>(Element.QUARTZ,-0.07),
                new KeyValuePair<Element, double>(Element.AGRICIUM,0.03),
                new KeyValuePair<Element, double>(Element.BERYL,0.03),
                new KeyValuePair<Element, double>(Element.LARANITE,0.04),
                new KeyValuePair<Element, double>(Element.COPPER,0.1)
                        });
                case "HUR-L2":
                    return new Refinery("Faithful Dream Station", "HUR-L2",
                        new HashSet<KeyValuePair<Element, double>>{
                new KeyValuePair<Element, double>(Element.TITANIUM,-0.03),
                new KeyValuePair<Element, double>(Element.BEXALITE,-0.03),
                new KeyValuePair<Element, double>(Element.HEPHAESTANITE,0.02),
                new KeyValuePair<Element, double>(Element.TARANITE,-0.04),
                new KeyValuePair<Element, double>(Element.ALUMINUM,0.02),
                new KeyValuePair<Element, double>(Element.BORASE,0.04),
                new KeyValuePair<Element, double>(Element.COPPER,0.03)
                        });
                case "MIC-L1":
                    return new Refinery("Shallow Frontier Station", "MIC-L1",
                       new HashSet<KeyValuePair<Element, double>>{
                new KeyValuePair<Element, double>(Element.QUANTANIUMN,0.02),
                new KeyValuePair<Element, double>(Element.QUARTZ,0.03),
                new KeyValuePair<Element, double>(Element.BERYL,0.03),
                new KeyValuePair<Element, double>(Element.BEXALITE,0.04),
                new KeyValuePair<Element, double>(Element.DIAMOND,0.05),
                new KeyValuePair<Element, double>(Element.HEPHAESTANITE,0.05),
                new KeyValuePair<Element, double>(Element.LARANITE,-0.06),
                new KeyValuePair<Element, double>(Element.ALUMINUM,-0.08),
                new KeyValuePair<Element, double>(Element.BORASE,0.04),
                new KeyValuePair<Element, double>(Element.TUNGSTEN,-0.03)
                       });
            }
            return null;
        }

    }
}
