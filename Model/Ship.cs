using System;
using System.Collections.Generic;
using System.Text;

namespace Mining_Tool_3.Model
{
    public class Ship
    {
        public static readonly Ship PROSPECTOR = new Ship("Prospector", 3200, "32.00");
        public static readonly Ship MOLE = new Ship("Mole", 9600, "96.00");

        public static IEnumerable<Ship> Ships
        {
            get
            {
                yield return PROSPECTOR;
                yield return MOLE;
            }
        }

        public string Name { get; set; }
        public int CScu { get; set; }
        public int Scu { get { return CScu / 100; } }
        public String ScuLabel { get; set; }

        public Ship(string name, int cscu, String scuLabel) => (Name, CScu, ScuLabel) = (name, cscu, scuLabel);

    }
}
