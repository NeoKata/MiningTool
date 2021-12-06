using System;
using System.Collections.Generic;
using System.Text;

namespace Mining_Tool_3.Model
{
    public class Reminder
    {
        public string RafineryKey { get; set; }
        public List<ReminderDetail> Data {get;set;}

        public Reminder(string Key)
        {
            RafineryKey = Key;
            Data = new List<ReminderDetail>();
        }
        public Reminder()
        {

        }
    }
}
