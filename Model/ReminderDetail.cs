using System;
using System.Collections.Generic;
using System.Text;

namespace Mining_Tool_3.Model
{
    public class ReminderDetail
    {
        public DateTime Timer { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }
        public bool TimerStart { get; set; }
        public double  UEC { get; set; }
    }
}
