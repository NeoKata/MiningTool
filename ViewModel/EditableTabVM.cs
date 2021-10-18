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
            set { _selectedItem = value; 
                OnPropertyChanged("SelectedItem");            
            }
        }

        public EditableTabVM()
        {
            Messenger.Instance.Register<Element>(this, "ElementVM", ElementNotify);
        }

        public void ElementNotify(Element element)
        {
            if (SelectedItem != null)
            {
                SelectedItem.AddMineral(element);
            }
            else
            {
                NewStone(element);
            }           
        }

        private void NewStone(Element element)
        {
            StoneVM stone = new StoneVM(new Stone(Ship.PROSPECTOR), element);
            Items.Add(stone);
            SelectedItem = stone;
            OnPropertyChanged("Items");
        }



    }
}
