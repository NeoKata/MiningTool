using Mining_Tool_3.Model;
using Mining_Tool_3.mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Mining_Tool_3.ViewModel
{
    public class MineralVM : BaseVM
    {
        private Mineral _mineral; 

        public MineralVM Self { get { return this; } }

        public Mineral Mineral { get { return _mineral; } }

        public Element Element { get { return _mineral.Element; } }

        public string CargoFormat { get { return _mineral.Stone.Ship.ScuLabel + " SCU"; } }

        private ICommand _toCargoCommand;

        public ICommand ToCargoCommand
        {
            get
            {
                return _toCargoCommand ?? (_toCargoCommand = new CommandHandler((element) =>
                {
                    SendToCargo();
                }, () => true));
            }
        }

        public void SendToCargo()
        {
            Messenger.Instance.Send(Mineral, "MineralVM_Mineral_To_Cargo");
        }

        public double Percentage
        {
            get { return _mineral.Percentage; }
            set
            {
                _mineral.Percentage = value;
                OnPropertyChanged("Percentage");
                OnPropertyChanged("Value");
                OnPropertyChanged("ValueCargo");
                OnPropertyChanged("Cargo");
                OnPropertyChanged("CargoPercent");
                if (Element != Element.INERT)
                {
                    Messenger.Instance.Send(_mineral.Percentage, "Mineral_Percentage");
                }
            }
        }
        public bool Input { get { return Element != Element.INERT; } }
        public double Value { get {return _mineral.Value; } }
        public double ValueCargo { get { return _mineral.CargoValue; } }
        public double Cargo { get { return _mineral.Cargo; } }
        public double CargoPercent { get { return _mineral.CargoPercent*100; } }

        public MineralVM(Mineral mineral)
        {
            _mineral = mineral;
            Messenger.Instance.Register<double>(this, "Stone_Mass", ReceiveMass);
            Messenger.Instance.Register<Ship>(this, "ChangeShip", ChangeShip);
        }

        ~MineralVM()  
        {
            Messenger.Instance.Unregister<double>(this,"Stone_Mass");
            Messenger.Instance.Unregister<Ship>(this, "ChangeShip");
        }

        private void ReceiveMass(double mass)
        {
            OnPropertyChanged("Value");
            OnPropertyChanged("ValueCargo");
            OnPropertyChanged("Cargo");
            OnPropertyChanged("CargoPercent");
        }

        public void ChangeShip(Ship ship)
        {
            _mineral.Stone.Ship = ship;

            OnPropertyChanged("Value");
            OnPropertyChanged("ValueCargo");
            OnPropertyChanged("Cargo");
            OnPropertyChanged("CargoPercent");
            OnPropertyChanged("CargoFormat");
        }
    }
}
