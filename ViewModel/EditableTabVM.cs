using Mining_Tool_3.Model;
using Mining_Tool_3.mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace Mining_Tool_3.ViewModel
{

    public class EditableTabVM : BaseVM
    {
        ObservableCollection<StoneVM> _items;

        Ship _ship = Ship.PROSPECTOR;

        public ObservableCollection<StoneVM> Items
        {
            get
            {
                if (_items == null)
                {
                    _items = new ObservableCollection<StoneVM>();
                }
                return _items;
            }
        }

        private ICommand _newStoneCommand;

        public ICommand NewStoneCommand
        {
            get
            {
                return _newStoneCommand ?? (_newStoneCommand = new CommandHandler((sender) =>
                {
                    NewStone(null);
                }, () => true));
            }
        }
        private ICommand _deleteStoneCommand;

        public ICommand DeleteStoneCommand
        {
            get
            {
                return _deleteStoneCommand ?? (_deleteStoneCommand = new CommandHandler((sender) =>
                {
                    Items.Remove(sender as StoneVM);
                }, () => true));
            }
        }
        private StoneVM _selectedItem;
        public StoneVM SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        public EditableTabVM()
        {
            Messenger.Instance.Register<Element>(this, "ElementVM", ElementNotify);
            Messenger.Instance.Register<Ship>(this, "ChangeShip", ChangeShip);
            Messenger.Instance.Register<String>(this, "SpeechRecognition", NotifySpeech);
        }

        ~EditableTabVM()
        {
            Messenger.Instance.Unregister<Element>(this, "ElementVM");
            Messenger.Instance.Unregister<Ship>(this, "ChangeShip");
            Messenger.Instance.Unregister<String>(this, "SpeechRecognition");
        }

        public void ElementNotify(Element element)
        {
            if (SelectedItem != null)
            {
                SelectedItem.ManageMineral(element);
            }
            else
            {
                NewStone(element);
            }
        }

        public void ChangeShip(Ship ship)
        {
            _ship = ship;
        }

        public void NotifySpeech(String name)
        {
            if (name == "Neuer Stein")
            {
                NewStone(null);
            }
            foreach (Element element in Element.ByValues)
            {
                if (name == element.Name)
                {
                    if (SelectedItem != null)
                    {
                        SelectedItem.AddMineral(element);
                    }
                    else
                    {
                        NewStone(element);
                    }
                    return;
                }
                if (name.Contains(element.Name) && name.Contains("Löschen"))
                {
                    if (SelectedItem != null)
                    {
                        SelectedItem.RemoveMineral(element);
                    }
                    return;
                }

                if (name.Contains(element.Name))
                {
                    var names = name.Split(" ");
                    if (names.Length > 1)
                    {
                        var number = new int[names.Length];                       
                        for (int i = 1; i < names.Length; i++)
                        {
                            if (names[i] == "Komma")                          
                            {
                                if(i + 1 < names.Length && names[i+1] != "Prozent" && int.Parse(names[i + 1]) < 10)
                                {
                                    names[i + 1] = "" + int.Parse(names[i + 1]) * 10;
                                }
                                continue;
                            }
                            if(names[i] == "Prozent")
                            {                               
                                continue;
                            }
                  
                            if (i == 1)
                            {
                                number[i] = int.Parse(names[i]) * 100;
                                continue;
                            }
                            number[i] = int.Parse(names[i]);
                        }
                        var percent = 0.0;
                        for (int i = 0; i < number.Length; i++)
                        {
                            percent += number[i];
                        }
                        percent /= 10000.0;
                        if (names[0] == element.Name)
                        {
                            if (SelectedItem == null)
                            {
                                NewStone(element);
                            }
                            SelectedItem.AddMineral(element, percent);
                            return;
                        }
                    }
                }


            }
        }

        private void NewStone(Element element)
        {
            StoneVM stone = new StoneVM(new Stone(_ship), element);
            Items.Add(stone);
            SelectedItem = stone;
            OnPropertyChanged("Items");
        }
    }
}
