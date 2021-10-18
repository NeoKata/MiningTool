using Mining_Tool_3.mvvm;
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
    /// Interaction logic for NumberCargoControl.xaml
    /// </summary>
    public partial class NumberCargoControl : UserControl
    {
        private double _numberCargoContent = 0;


        public NumberCargoControl()
        {
            InitializeComponent();
            WriteLablewithLeadingZero();
        }

        public static readonly DependencyProperty SetTitelProperty = DependencyProperty.Register(
            "Titel", typeof(string), typeof(NumberCargoControl),
            new FrameworkPropertyMetadata("Title", new PropertyChangedCallback(ChangeTitel)));

        public string Titel { get { return (string)GetValue(SetTitelProperty); } set { SetValue(SetTitelProperty, value); } }

        private static void ChangeTitel(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            NumberCargoControl numberCargoControl = sender as NumberCargoControl;
            numberCargoControl.TitelLabel.Content = (string)e.NewValue;
        }

        public static readonly DependencyProperty SetNumberContentProperty =
      DependencyProperty.Register("NumberContent", typeof(double), typeof(NumberCargoControl), new
         FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnNumberContentPropertyChanged));

        public double NumberContent
        {
            get { return (double)GetValue(SetNumberContentProperty); }
            set { SetValue(SetNumberContentProperty, value); }
        }

        private static void OnNumberContentPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            NumberCargoControl control = sender as NumberCargoControl;
            if (control != null)
            {
                control.WriteLablewithLeadingZero();
            }
        }

        private void WriteLablewithLeadingZero()
        {
            _numberCargoContent = NumberContent;
            Mass_Input.Content = string.Format("{0,2:00.00}", _numberCargoContent);
        }

        private void SubLastMass()
        {
            _numberCargoContent = Math.Truncate(_numberCargoContent * 10);
        }

        private void AddToMass(int number)
        {
            if (_numberCargoContent >= 32)
            {
                _numberCargoContent = 0;
            }
            _numberCargoContent *= 1000;
            _numberCargoContent += number;
            _numberCargoContent = Math.Clamp(_numberCargoContent, 0, 3200);
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
                _numberCargoContent = 0;
            }
          
            NumberContent = _numberCargoContent/100.0;
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            NumberContent = 0;
            Keyboard.Focus(Mass_Input);
        }


    }
}

