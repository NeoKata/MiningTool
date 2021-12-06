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
        private ReminderItemVM reminderItemARC1;
        private ReminderItemVM reminderItemCRU1;
        private ReminderItemVM reminderItemHUR1;
        private ReminderItemVM reminderItemHUR2;
        private ReminderItemVM reminderItemMIC1;

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

            if (reminderItemARC1 != null && reminderItemVM.Refinery.Symbol == reminderItemARC1.Refinery.Symbol)
            {
                reminderItemARC1 = null;
                return;
            }
            if (reminderItemCRU1 != null && reminderItemVM.Refinery.Symbol == reminderItemCRU1.Refinery.Symbol)
            {
                reminderItemCRU1 = null;
                return;
            }
            if (reminderItemHUR1 != null && reminderItemVM.Refinery.Symbol == reminderItemHUR1.Refinery.Symbol)
            {
                reminderItemHUR1 = null;
                return;
            }
            if (reminderItemHUR2 != null && reminderItemVM.Refinery.Symbol == reminderItemHUR2.Refinery.Symbol)
            {
                reminderItemHUR2 = null;
                return;
            }
            if (reminderItemMIC1 != null && reminderItemVM.Refinery.Symbol == reminderItemMIC1.Refinery.Symbol)
            {
                reminderItemMIC1 = null;
                return;
            }         
        }

        public ReminderVM()
        {
            foreach(var reminder in _json.readJson())
            {
                var reminderItemVM = new ReminderItemVM(reminder);
                ReminderItems.Add(new ReminderItemVM(reminder));
                if (reminder.RafineryKey == Refinery.ARC_L1)
                {
                    reminderItemARC1 = reminderItemVM;
                    continue;
                }
                if (reminder.RafineryKey == Refinery.CRU_L1)
                {
                    reminderItemCRU1 = reminderItemVM;
                    continue;
                }
                if (reminder.RafineryKey == Refinery.HUR_L1)
                {
                    reminderItemHUR1 = reminderItemVM;
                    continue;
                }
                if (reminder.RafineryKey == Refinery.HUR_L2)
                {
                    reminderItemHUR2 = reminderItemVM;
                    continue;
                }
                if (reminder.RafineryKey == Refinery.MIC_L1)
                {
                    reminderItemMIC1 = reminderItemVM;
                    continue;
                }
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
            if (reminderItemMIC1 == null)
            {
                reminderItemMIC1 = new ReminderItemVM(RefineryFactory.getRefinery(Refinery.MIC_L1));
                ReminderItems.Add(reminderItemMIC1);            
            }
            reminderItemMIC1.ReminderDetails.Add(new ReminderItemDetailVM()
            {
                Value = obj.MIC_L1.Value,
                ReminderItemVM = reminderItemMIC1
            });
            save();
        }

        private void ReceiveHUR2(RefinementMethode obj)
        {
            if (reminderItemHUR2 == null)
            {
                reminderItemHUR2 = new ReminderItemVM(RefineryFactory.getRefinery(Refinery.HUR_L2));
                ReminderItems.Add(reminderItemHUR2);
            }            
            reminderItemHUR2.ReminderDetails.Add(new ReminderItemDetailVM()
            {
                Value = obj.HUR_L2.Value,
                ReminderItemVM = reminderItemHUR2
            });
            save();
        }

        private void ReceiveHUR1(RefinementMethode obj)
        {
            if (reminderItemHUR1 == null)
            {
                reminderItemHUR1 = new ReminderItemVM(RefineryFactory.getRefinery(Refinery.HUR_L1));
                ReminderItems.Add(reminderItemHUR1);
            }
            reminderItemHUR1.ReminderDetails.Add(new ReminderItemDetailVM()
            {
                Value = obj.HUR_L1.Value,
                ReminderItemVM = reminderItemHUR1
            });
            save();
        }

        private void ReceiveCRU1(RefinementMethode obj)
        {
            if (reminderItemCRU1 == null)
            {
                reminderItemCRU1 = new ReminderItemVM(RefineryFactory.getRefinery(Refinery.CRU_L1));
                ReminderItems.Add(reminderItemCRU1);
            }
            reminderItemCRU1.ReminderDetails.Add(new ReminderItemDetailVM()
            {
                Value = obj.CRU_L1.Value,
                ReminderItemVM = reminderItemCRU1
            });
            save();
        }

        private void ReceiveARC1(RefinementMethode obj)
        {
            if (reminderItemARC1 == null)
            {
                reminderItemARC1 = new ReminderItemVM(RefineryFactory.getRefinery(Refinery.ARC_L1));
                ReminderItems.Add(reminderItemARC1);
            }
            reminderItemARC1.ReminderDetails.Add(new ReminderItemDetailVM()
            {
                Value = obj.ARC_L1.Value,
                ReminderItemVM = reminderItemARC1
            });
            save();
        }

        private void save()
        {
            List<Reminder> reminder = new List<Reminder>();
            if (reminderItemARC1 != null)
            {
                reminder.Add(reminderItemARC1.Model);
            }
            if (reminderItemCRU1 != null)
            {
                reminder.Add(reminderItemCRU1.Model);
            }
            if (reminderItemHUR1 != null)
            {
                reminder.Add(reminderItemHUR1.Model);
            }
            if (reminderItemHUR2 != null)
            {
                reminder.Add(reminderItemHUR2.Model);
            }
            if (reminderItemMIC1 != null)
            {
                reminder.Add(reminderItemMIC1.Model);
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
