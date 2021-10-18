using System;
using System.Collections.Generic;
using System.Text;

namespace Mining_Tool_3.mvvm
{
    interface IMessenger
    {
        void Register<TNotification>(object recipient, Action<TNotification> action);
        void Register<TNotification>(object recipient, string identCode, Action<TNotification> action);
        
        void Send<TNotification>(TNotification notification);
        void Send<TNotification>(TNotification notification, string identCode);
        
        void Unregister<TNotification>(object recipient);
        void Unregister<TNotification>(object recipient, string identCode);
    }
}
