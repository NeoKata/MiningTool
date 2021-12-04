using Mining_Tool_3.Model;
using Mining_Tool_3.mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;

namespace Mining_Tool_3.ViewModel
{
    public class RefinerieVM : BaseVM
    {

        public RefinerieVM()
        {
            Messenger.Instance.Register<CargoItemVM>(this, "CargoItemVM", ReceiveCargoItem);
        }

        private void ReceiveCargoItem(CargoItemVM cargoItem)
        {
            UpdateGrid();
        }

        public IEnumerable<RefinementMethode> RefinementMethodes
        {
            get
            {
                return RefinementMethode.RefinementMethodes;
            }
        }

        private DataGridRowDetailsVisibilityMode _rowDetailsVisible;
        public DataGridRowDetailsVisibilityMode RowDetailsVisible
        {
            get { return _rowDetailsVisible; }
            set
            {
                _rowDetailsVisible = value;
                OnPropertyChanged("RowDetailsVisible");
            }
        }


        private CargoVM _cargo;
        public CargoVM Cargo
        {
            get { return _cargo; }
            set
            {
                if (_cargo != null)
                {
                    _cargo.CargoItems.CollectionChanged -= CargoItems_CollectionChanged;
                }
                _cargo = value;
                _cargo.CargoItems.CollectionChanged += CargoItems_CollectionChanged;
            }
        }

        private void CargoItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
           UpdateGrid();
        }

        private void UpdateGrid()
        {
            Refinery.MinValue = Double.MaxValue;
            Refinery.MaxValue = 0.0;
           
            foreach (RefinementMethode refinementMethode in RefinementMethodes)
            {
                foreach (string refineryId in Refinery.Refineries)
                {
                    refinementMethode.ById(refineryId).Cargo.Clear();
                    double sum = 0.0;
                    foreach (CargoItemVM cargo in Cargo.CargoItems)
                    {                       
                        double yield = refinementMethode.ById(refineryId).GetYield(cargo.Element);
                        double refineryBonus = 1.0 + yield;
                        double cargoSCU = cargo.cSCU * (1.0 - refinementMethode.Loss) * refineryBonus;
                        double result = cargoSCU * cargo.Element.ValueRefined;
                        sum += result;
                        refinementMethode.ById(refineryId).Cargo.Add(new KeyValuePair<Element, ElementValue>(cargo.Element, new ElementValue(yield, result)));

                    }
                    refinementMethode.ById(refineryId).Value = sum;
                }
            }
            OnPropertyChanged("RefinementMethodes");
        }
    }
}
