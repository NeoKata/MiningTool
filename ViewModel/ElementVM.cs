using Mining_Tool_3.Model;
using Mining_Tool_3.mvvm;
using Mining_Tool_3.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Mining_Tool_3.View
{
    internal class ElementVM : BaseVM
    {
        private ICommand _clickMiningCommand;

        public ICommand ClickMiningCommand
        {
            get
            {
                return _clickMiningCommand ?? (_clickMiningCommand = new CommandHandler((element) => {
                Messenger.Instance.Send(element as Element, "ElementVM");
                }, () => true));
            }
        }
        public IEnumerable<Element> Nodes { get; private set; }

        public ElementVM()
        {
            Nodes = Element.ByValues;
        }
    }
}