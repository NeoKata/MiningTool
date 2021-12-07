using Mining_Tool_3.Model;
using Mining_Tool_3.mvvm;
using Mining_Tool_3.Persitenz;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Mining_Tool_3.ViewModel
{
    public class ReminderVM : BaseVM
    {
        private Json _json = new Json();

        ObservableCollection<ReminderItemVM> _reminderItems;
        public ObservableCollection<ReminderItemVM> ReminderItems
        {
            get
            {
                if (_reminderItems == null)
                {
                    _reminderItems = new ObservableCollection<ReminderItemVM>();
                }
                return _reminderItems;
            }
        }

        private void Remove(ReminderItemVM reminderItemVM)
        {
            ReminderItems.Remove(reminderItemVM);
        }

        public ReminderVM()
        {
            foreach(var reminder in _json.readJson())
            {
                var reminderItemVM = new ReminderItemVM(reminder);
                ReminderItems.Add(new ReminderItemVM(reminder));               
            }

            Messenger.Instance.Register<RefinementMethode>(this, "Reminder_ARC1", ReceiveARC1);
            Messenger.Instance.Register<RefinementMethode>(this, "Reminder_CRU1", ReceiveCRU1);
            Messenger.Instance.Register<RefinementMethode>(this, "Reminder_HUR1", ReceiveHUR1);
            Messenger.Instance.Register<RefinementMethode>(this, "Reminder_HUR2", ReceiveHUR2);
            Messenger.Instance.Register<RefinementMethode>(this, "Reminder_MIC1", ReceiveMIC1);

            Messenger.Instance.Register<ReminderItemVM>(this, "Delete Item", DeleteInhalt);
            Messenger.Instance.Register<DateTime>(this, "ChangeTimer", TimerChanged);
        }

        private void TimerChanged(DateTime obj)
        {
            save();
        }

        private void DeleteInhalt(ReminderItemVM item)
        {
            Remove(item);
            save();
        }

        private void ReceiveMIC1(RefinementMethode obj)
        {

            CreateReminder(Refinery.MIC_L1, obj.MIC_L1.Value, obj.MIC_L1.CargoSCU);
        }

        private void ReceiveHUR2(RefinementMethode obj)
        {
            CreateReminder(Refinery.HUR_L2, obj.HUR_L2.Value, obj.HUR_L2.CargoSCU);
        }

        private void ReceiveHUR1(RefinementMethode obj)
        {
            CreateReminder(Refinery.HUR_L1, obj.HUR_L1.Value, obj.HUR_L1.CargoSCU);
        }

        private void ReceiveCRU1(RefinementMethode obj)
        {
            CreateReminder(Refinery.CRU_L1, obj.CRU_L1.Value, obj.CRU_L1.CargoSCU);
        }

        private void ReceiveARC1(RefinementMethode obj)
        {
            CreateReminder(Refinery.ARC_L1, obj.ARC_L1.Value, obj.ARC_L1.CargoSCU);
        }


        private void CreateReminder(String RefineryKey,double value, double scu)
        {
            ReminderItemVM found = null;
            foreach(var reminder in ReminderItems)
            {
                if(reminder.Refinery.Symbol.Equals(RefineryKey))
                {
                    found = reminder;
                    break;
                   
                }
            }
            if(found == null)
            {
                found = new ReminderItemVM(RefineryFactory.getRefinery(RefineryKey));
                ReminderItems.Add(found);
            }
            found.ReminderDetails.Add(new ReminderItemDetailVM()
            {
                Value = value,
                SCU = scu,
                ReminderItemVM = found
            });
            save();
        }

        private void save()
        {
            List<Reminder> reminder = new List<Reminder>();
            foreach(var reminderVM in ReminderItems)
            {
                reminder.Add(reminderVM.Model);
            }
            _json.writeJson(reminder);
        }

        ~ReminderVM()
        {
            Messenger.Instance.Unregister<RefinementMethode>(this, "Reminder_ARC1");
            Messenger.Instance.Unregister<RefinementMethode>(this, "Reminder_CRU1");
            Messenger.Instance.Unregister<RefinementMethode>(this, "Reminder_HUR1");
            Messenger.Instance.Unregister<RefinementMethode>(this, "Reminder_HUR2");
            Messenger.Instance.Unregister<RefinementMethode>(this, "Reminder_MIC1");
            Messenger.Instance.Unregister<DateTime>(this, "ChangeTimer");
        }
    }
}
