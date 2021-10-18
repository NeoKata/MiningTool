using System;
using System.Collections.Generic;
using System.Text;

namespace Mining_Tool_3.Model
{
    public class Ship
    {
        public static readonly Ship PROSPECTOR = new Ship("Prospector", 3200);
        public static readonly Ship MOLE = new Ship("Mole", 9600);

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

        /*
        public float CargoValue { get {
                float sum = 0;
                foreach (Stone stone in Cargo)
                {
                    sum += stone.Value;
                }
                return sum;
            } }

        public IEnumerable<Stone> Cargo { get; set; }
*/
        public Ship(string name, int cscu) => (Name, CScu) = (name, cscu);

    }
}
