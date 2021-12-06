using Mining_Tool_3.mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mining_Tool_3.ViewModel
{
    public class MainVM : BaseVM
    {
        public MainVM()
        {
            Messenger.Instance.Register<String>(this, "SpeechRecognition", NotifySpeech);
        }

        private string _speech = "";
        public String Speech
        { get { return _speech; } }

        private void NotifySpeech(string name)
        {
            if (name == "Meining")
            {
                name = "Mining";            
            }
            _speech = "";
            OnPropertyChanged("Speech");
            _speech = name;
            OnPropertyChanged("Speech");
        }
    }
}
