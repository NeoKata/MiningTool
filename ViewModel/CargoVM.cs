using Mining_Tool_3.Model;
using Mining_Tool_3.mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace Mining_Tool_3.ViewModel
{


    public class CargoVM : BaseVM
    {
        ObservableCollection<CargoItemVM> _cargoItems;
        public ObservableCollection<CargoItemVM> CargoItems
        {
            get
            {
                if (_cargoItems == null)
                {
                    _cargoItems = new ObservableCollection<CargoItemVM>();
                }
                return _cargoItems;
            }
        }

        public double SumValue { get {
                double sum = 0;    
                foreach(CargoItemVM item in _cargoItems)
                    {
                    sum += item.Value;
                }return sum; } }

        public CargoVM()
        {
            Messenger.Instance.Register<Mineral>(this, "MineralVM_Mineral_To_Cargo", ReceiveMineral);
        }

        private ICommand _deleteCargoCommand;

        public ICommand DeleteCargoCommand
        {
            get
            {
                return _deleteCargoCommand ?? (_deleteCargoCommand = new CommandHandler((cargo) =>
                {
                    var item = cargo as CargoItemVM;
                    item.PropertyChanged -= Cargo_PropertyChanged;
                    _cargoItems.Remove(item);
                    OnPropertyChanged("SumValue");
                }, () => true));
            }
        }

        private void ReceiveMineral(Mineral mineral)
        {
            foreach (CargoItemVM cargoModel in _cargoItems)
            {
                if (cargoModel.Element == mineral.Element)
                {
                    cargoModel.SCU += mineral.Cargo;
                    return;
                }
            }
            CargoItemVM cargo = new CargoItemVM(mineral.Element, mineral.Cargo);
            cargo.PropertyChanged += Cargo_PropertyChanged;
            CargoItems.Add(cargo);
            OnPropertyChanged("SumValue");

        }

        private void Cargo_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged("SumValue");
        }
    }
}
