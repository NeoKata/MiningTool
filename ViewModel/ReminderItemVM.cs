using Mining_Tool_3.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Mining_Tool_3.ViewModel
{
    public class ReminderItemVM
    {
        public Refinery Refinery { get; set; }

        public Reminder Model { get; set; }

        private ObservableCollection<ReminderItemDetailVM> _reminderDetail;

        public ObservableCollection<ReminderItemDetailVM> ReminderDetails
        {
            get
            {
                if (_reminderDetail == null)
                {
                    _reminderDetail = new ObservableCollection<ReminderItemDetailVM>();
                }
                return _reminderDetail;
            }
        }

        public ReminderItemVM(Refinery refinery)
        {
            Refinery = refinery;
            Model = new Reminder(refinery.Symbol);
            ReminderDetails.CollectionChanged += ReminderDetails_CollectionChanged;
        }

        public ReminderItemVM(Reminder reminder)
        {
            Model = reminder;
            Refinery = RefineryFactory.getRefinery(reminder.RafineryKey);
            foreach (var detail in reminder.Data)
            {
                ReminderDetails.Add(new ReminderItemDetailVM(detail,this));
            }
            ReminderDetails.CollectionChanged += ReminderDetails_CollectionChanged;
        }

        private void ReminderDetails_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Model.Data.Clear();
            foreach (var item in ReminderDetails)
            {
                Model.Data.Add(item.Model);
            }
        }
    }
}
