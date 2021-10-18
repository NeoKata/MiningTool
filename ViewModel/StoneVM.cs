using Mining_Tool_3.Model;
using Mining_Tool_3.mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Input;

namespace Mining_Tool_3.ViewModel
{
    public class StoneVM : BaseVM
    {
        private Stone _stone;

        public string Header { get { return _stone.BestMineral + ": " + _stone.Mass; } }

        public double SumValue { get { return _stone.Value; } }

        public double Mass
        {
            get { return _stone.Mass; }
            set
            {
                _stone.Mass = value;
                OnPropertyChanged("SumValue");
                OnPropertyChanged("Header");
                Messenger.Instance.Send(_stone.Mass, "Stone_Mass");
            }
        }

        MineralVM _inert;
        ObservableCollection<MineralVM> _inerts;
        public ObservableCollection<MineralVM> Inert
        {
            get
            {
                if (_inerts == null)
                {
                    _inerts = new ObservableCollection<MineralVM>();
                    _inerts.Add(_inert);
                }
                return _inerts;
            }
        }

        ObservableCollection<MineralVM> _minerals;
        public ObservableCollection<MineralVM> Minerals
        {
            get
            {
                if (_minerals == null)
                {
                    _minerals = new ObservableCollection<MineralVM>();
                    foreach (Mineral mineral in _stone.Minerals)
                    {
                        _minerals.Add(new MineralVM(mineral));
                    }

                }
                return _minerals;
            }
        }

        public StoneVM(Stone stone, Element element)
        {
            _stone = stone;
            _inert = new MineralVM(_stone.Inert);
            Messenger.Instance.Register<double>(this, "Mineral_Percentage", ReceivePercentage);
            if (element != null)
            {
                _stone.AddMineral(element);
            }
        }

        private void ReceivePercentage(double percentage)
        {
            _inert.Percentage = 12;
            OnPropertyChanged("SumValue");
            OnPropertyChanged("Header");
        }

        public void AddMineral(Element element)
        {
            MineralVM foundMineral = null;
            foreach (MineralVM mineralVM in Minerals)
            {
                if (mineralVM.Element == element)
                {
                    foundMineral = mineralVM;
                    break;
                }
            }
            if (foundMineral != null)
            {
                Minerals.Remove(foundMineral);
                _stone.Minerals.Remove(foundMineral.Mineral);
            }
            else
            {
                Minerals.Add(new MineralVM(_stone.AddMineral(element)));
            }
        }
    }
}
