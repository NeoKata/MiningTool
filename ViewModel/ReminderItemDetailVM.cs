using Mining_Tool_3.Model;
using Mining_Tool_3.mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Windows.Input;

namespace Mining_Tool_3.ViewModel
{
    public class ReminderItemDetailVM : BaseVM
    {
        public ReminderDetail Model { get; set; }
        public bool TimeRun { get { return Model.TimerStart; } set { Model.TimerStart = value; } }

        private BackgroundWorker _clockWorker = new BackgroundWorker();


        public DateTime Timer { get { return Model.Timer; } set { Model.Timer = value; } }
        public double Value { get { return Model.UEC; } set { Model.UEC = value; } }
        public double SCU { get { return Model.SCU; } set { Model.SCU = value; } }
        public string TimeLeft { get; set; }
        public bool Ready { get; set; }


        public int Day { get { return Model.Day; } set { Model.Day = value; Messenger.Instance.Send(Timer, "ChangeTimer"); } }
        public int Hour
        {
            get { return Model.Hour; }
            set { Model.Hour = value; Messenger.Instance.Send(Timer, "ChangeTimer"); }
        }
        public int Minute
        {
            get { return Model.Minute; }
            set { Model.Minute = value; Messenger.Instance.Send(Timer, "ChangeTimer"); }
        }
        public int Second
        {
            get { return Model.Second; }
            set { Model.Second = value; Messenger.Instance.Send(Timer, "ChangeTimer"); }
        }

        public ReminderItemVM ReminderItemVM { get; set; }

        public ReminderItemDetailVM()
        {
            Model = new ReminderDetail();
            TimeRun = false;
            _clockWorker.WorkerSupportsCancellation = true;
            _clockWorker.DoWork += _clockWorker_DoWork;
        }

        public ReminderItemDetailVM(ReminderDetail detail, ReminderItemVM reminderItemVM)
        {
            Model = detail;
            ReminderItemVM = reminderItemVM;          
            _clockWorker.WorkerSupportsCancellation = true;
            _clockWorker.DoWork += _clockWorker_DoWork;
             restartTimer();
        }

        private ICommand _deleteReminderCommand;
        public ICommand DeleteReminderCommand
        {
            get
            {
                return _deleteReminderCommand ?? (_deleteReminderCommand = new CommandHandler((sender) =>
                {
                    ReminderItemVM.ReminderDetails.Remove(this);
                    if (ReminderItemVM.ReminderDetails.Count == 0)
                    {
                        Messenger.Instance.Send(ReminderItemVM, "Delete Item");
                    }
                }, () => true));
            }
        }

        private ICommand _playCommand;
        public ICommand PlayCommand
        {
            get
            {
                return _playCommand ?? (_playCommand = new CommandHandler((sender) =>
                {
                    if (!TimeRun)
                    {
                        Timer = DateTime.Now;
                        Timer = Timer.AddSeconds(Second);
                        Timer = Timer.AddMinutes(Minute);
                        Timer = Timer.AddHours(Hour);
                        Timer = Timer.AddDays(Day);

                        var time = DateTime.Now - Timer;
                        TimeLeft = time.ToString("dd\\.hh\\:mm\\:ss");
                     
                        TimeRun = true;
                        if (!_clockWorker.IsBusy)
                        {
                            _clockWorker.RunWorkerAsync();
                        }
                        OnPropertyChanged("TimeLeft");
                        OnPropertyChanged("TimeRun");
                        Messenger.Instance.Send(Timer, "ChangeTimer");
                        return;
                    }
                    if (TimeRun)
                    {
                        _clockWorker.CancelAsync();
                        TimeRun = false;
                        var time = Timer - DateTime.Now;
                        Day = time.Days;
                        Hour = time.Hours;
                        Minute = time.Minutes;
                        Second = time.Seconds;
                        OnPropertyChanged("Day");
                        OnPropertyChanged("Hour");
                        OnPropertyChanged("Minute");
                        OnPropertyChanged("Second");
                        OnPropertyChanged("TimeRun");
                        Messenger.Instance.Send(Timer, "ChangeTimer");
                    }

                }, () => true));
            }
        }

        private void restartTimer()
        {
            if (TimeRun && !_clockWorker.IsBusy)
            {
                _clockWorker.RunWorkerAsync();
                OnPropertyChanged("TimeRun");
            }
        }


        private void _clockWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!e.Cancel)
            {
                var timer = DateTime.Now - Timer;
                TimeLeft = timer.ToString("dd\\.hh\\:mm\\:ss");
                OnPropertyChanged("TimeLeft");
                if (Timer <= DateTime.Now)
                {
                    e.Cancel = true;
                    Ready = true;
                    TimeLeft = "00.00:00:00";
                    OnPropertyChanged("Ready");
                }
                Thread.Sleep(1000);
            }
        }
    }
}
