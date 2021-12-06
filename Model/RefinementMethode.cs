using Mining_Tool_3.mvvm;
using Mining_Tool_3.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Windows.Media;

namespace Mining_Tool_3.Model
{
    public class RefinementMethode
    {
        public enum Speed
        {
            XtraFast,
            VeryFast,
            Mid,
            Slow,
            VerySlow,
            XtraSlow
        }
        public enum Cost
        {
            High,
            Mid,
            Low,
            XtraLow
        }


        public static readonly RefinementMethode DINYX = new RefinementMethode("Dinyx Solvenation", 0.05, Speed.XtraSlow, Cost.Low);
        public static readonly RefinementMethode FERRON = new RefinementMethode("Ferron Exchange", 0.05, Speed.VerySlow, Cost.Mid);
        public static readonly RefinementMethode PYROMETRIC = new RefinementMethode("Pyrometric Chromalysis", 0.05, Speed.Slow, Cost.High);
        public static readonly RefinementMethode THERMONATIC = new RefinementMethode("Thermonatic Deposition", 0.1925, Speed.Slow, Cost.Low);
        public static readonly RefinementMethode ELECTROSTAROLYSIS = new RefinementMethode("Electrostarolysis", 0.1925, Speed.Mid, Cost.Mid);
        public static readonly RefinementMethode GASKIN = new RefinementMethode("Gaskin Process", 0.1925, Speed.Mid, Cost.High);
        public static readonly RefinementMethode KAZEN = new RefinementMethode("Kazen Winnowing", 0.335, Speed.Mid, Cost.Low);
        public static readonly RefinementMethode CORMACK = new RefinementMethode("Cormack Method", 0.335, Speed.VeryFast, Cost.Mid);
        public static readonly RefinementMethode XCR = new RefinementMethode("XCR Reaction", 0.335, Speed.XtraFast, Cost.High);

        public static IEnumerable<RefinementMethode> RefinementMethodes
        {
            get
            {
                yield return DINYX;
                yield return FERRON;
                yield return PYROMETRIC;
                yield return THERMONATIC;
                yield return ELECTROSTAROLYSIS;
                yield return GASKIN;
                yield return KAZEN;
                yield return CORMACK;
                yield return XCR;
            }
        }

        private static int _count = 0;
        private static int _lastId = 0;
        private static int _elementCount = 0;

        public int ID { get; private set; }
        public string Name { get; private set; }
        public double Loss { get; private set; }
        public Speed MethodSpeed { get; private set; }
        public Cost MethodCost { get; private set; }
        public double SumCost { get; set; }
        public Refinery ARC_L1 { get; private set; }
        public Refinery CRU_L1 { get; private set; }
        public Refinery HUR_L1 { get; private set; }
        public Refinery HUR_L2 { get; private set; }
        public Refinery MIC_L1 { get; private set; }

        private byte[] Range(double value)
        {
            byte[] bytes = new byte[2];
            double colorValue = 510 / (Refinery.MaxValue - Refinery.MinValue) * (value - Refinery.MinValue);
            if (double.IsNaN(colorValue))
            {
                bytes[0] = 255;
                bytes[1] = 255;
                return bytes;
            }
            double green = colorValue / 255;
            double red = 2.0 - green;
            if (green > 1)
            {
                green = 1;
            }
            if (red > 1)
            {
                red = 1;
            }
            green *= 255;
            red *= 255;
            bytes[0] = Convert.ToByte(green);
            bytes[1] = Convert.ToByte(red);
            return bytes;
        }

        public Brush Color
        {
            get
            {
                if (_lastId != ID)
                {
                    _lastId = ID;
                    _elementCount = 1;
                }
                else
                {
                    _elementCount++;
                }

                switch (_elementCount)
                {
                    case 4:
                        return new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, Range(ARC_L1.Value)[1], Range(ARC_L1.Value)[0], 0));
                    case 6:
                        return new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, Range(CRU_L1.Value)[1], Range(CRU_L1.Value)[0], 0));
                    case 8:
                        return new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, Range(HUR_L1.Value)[1], Range(HUR_L1.Value)[0], 0));
                    case 10:
                        return new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, Range(HUR_L2.Value)[1], Range(HUR_L2.Value)[0], 0));
                    case 12:
                        return new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, Range(MIC_L1.Value)[1], Range(MIC_L1.Value)[0], 0));
                    default:
                        return Brushes.Aquamarine;
                }
            }
        }

        private ICommand _arctoReminderCommand;
        public ICommand ARC_L1ToReminderCommand
        {
            get
            {
                return _arctoReminderCommand ?? (_arctoReminderCommand = new CommandHandler((sender) =>
                {
                    Messenger.Instance.Send(sender as RefinementMethode, "Reminder_ARC1");
                }, () => true));
            }
        }

        private ICommand _crutoReminderCommand;
        public ICommand CRU_L1ToReminderCommand
        {
            get
            {
                return _crutoReminderCommand ?? (_crutoReminderCommand = new CommandHandler((sender) =>
                {
                    Messenger.Instance.Send(sender as RefinementMethode, "Reminder_CRU1");
                }, () => true));
            }
        }

        private ICommand _hur1toReminderCommand;
        public ICommand HUR_L1ToReminderCommand
        {
            get
            {
                return _hur1toReminderCommand ?? (_hur1toReminderCommand = new CommandHandler((sender) =>
                {
                    Messenger.Instance.Send(sender as RefinementMethode, "Reminder_HUR1");
                }, () => true));
            }
        }

        private ICommand _hur2toReminderCommand;
        public ICommand HUR_L2ToReminderCommand
        {
            get
            {
                return _hur2toReminderCommand ?? (_hur2toReminderCommand = new CommandHandler((sender) =>
                {
                    Messenger.Instance.Send(sender as RefinementMethode, "Reminder_HUR2");
                }, () => true));
            }
        }

        private ICommand _mictoReminderCommand;
        public ICommand MIC_L1ToReminderCommand
        {
            get
            {
                return _mictoReminderCommand ?? (_mictoReminderCommand = new CommandHandler((sender) =>
                {
                    Messenger.Instance.Send(sender as RefinementMethode, "Reminder_MIC1");
                }, () => true));
            }
        }


        public Refinery ById(string id)
        {
            switch (id)
            {
                case "ARC-L1":
                    return ARC_L1;
                case "CRU-L1":
                    return CRU_L1;
                case "HUR-L1":
                    return HUR_L1;
                case "HUR-L2":
                    return HUR_L2;
                case "MIC-L1":
                    return MIC_L1;
            }
            return null;
        }

        RefinementMethode(string name, double loss, Speed speed, Cost cost)
        {
            ID = _count++;
            Name = name;
            Loss = loss;
            MethodSpeed = speed;
            MethodCost = cost;
            ARC_L1 = RefineryFactory.getRefinery(Refinery.ARC_L1);
            CRU_L1 = RefineryFactory.getRefinery(Refinery.CRU_L1);
            HUR_L1 = RefineryFactory.getRefinery(Refinery.HUR_L1);
            HUR_L2 = RefineryFactory.getRefinery(Refinery.HUR_L2);
            MIC_L1 = RefineryFactory.getRefinery(Refinery.MIC_L1);
        }
    }
}
