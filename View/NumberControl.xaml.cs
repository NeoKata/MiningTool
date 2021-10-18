using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mining_Tool_3.View
{
    /// <summary>
    /// Interaction logic for NumberControl.xaml
    /// </summary>
    public partial class NumberControl : UserControl
    {
        private double _numberContent = 0;

        public NumberControl()
        {
            InitializeComponent();
            WriteLablewithLeadingZero();
        }

        public static readonly DependencyProperty SetTitelProperty = DependencyProperty.Register(
            "Titel", typeof(string), typeof(NumberControl), 
            new FrameworkPropertyMetadata("Title", new PropertyChangedCallback(ChangeTitel)));
        
        public string Titel { get { return (string)GetValue(SetTitelProperty); } set { SetValue(SetTitelProperty, value); } }
        
        private static void ChangeTitel(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            NumberControl numberControl = sender as NumberControl;
            numberControl.TitelLabel.Content = (string)e.NewValue;
        }

        public static readonly DependencyProperty SetNumberContentProperty =
      DependencyProperty.Register("NumberContent", typeof(double), typeof(NumberControl), new
         FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnNumberContentPropertyChanged));

        public double NumberContent
        {
            get { return (double)GetValue(SetNumberContentProperty); }
            set { SetValue(SetNumberContentProperty, value);}
        }

        private static void OnNumberContentPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            NumberControl control = sender as NumberControl;
            if (control != null)
            {
                control.WriteLablewithLeadingZero();
            }
        }

        private void SubLastMass()
        {
            _numberContent = Math.Floor(_numberContent / 10);
        }

        private void AddToMass(int number)
        {
            if (_numberContent >= 1000)
            {
                _numberContent %= 1000;

            }
            _numberContent *= 10;
            _numberContent += number;
        }

        private void WriteLablewithLeadingZero()
        {
            _numberContent = (int)NumberContent;
            String label = "";

            if (_numberContent < 1000)
            {
                label += "0";
            }
            if (_numberContent < 100)
            {
                label += "0";
            }
            if (_numberContent < 10)
            {
                label += "0";
            }
            label += _numberContent;
       
            Mass_Input.Content = label;
            MassSCU.Content = ((_numberContent * 2) / 100).ToString("n");
        }



        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            NumberContent = 0;
            Keyboard.Focus(Mass_Input);
        }

        private void Button_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                AddToMass(e.Key - Key.D0);
            }
            else if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                AddToMass(e.Key - Key.NumPad0);
            }
            else if (e.Key == Key.Back)
            {
                SubLastMass();
            }
            else if (e.Key == Key.Delete)
            {
                _numberContent = 0;
            }
            NumberContent = _numberContent;          
        }
    }
}

