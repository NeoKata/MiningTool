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
    /// Interaction logic for NumberControl.xaml
    /// </summary>
    public partial class NumberTimeControl : UserControl
    {
        private double _numberContent = 0;

        public NumberTimeControl()
        {
            InitializeComponent();
            WriteLablewithLeadingZero();
         
        }

        public static readonly DependencyProperty SetTitelProperty = DependencyProperty.Register(
            "Titel", typeof(string), typeof(NumberTimeControl), 
            new FrameworkPropertyMetadata("Title", new PropertyChangedCallback(ChangeTitel)));
        
        public string Titel { get { return (string)GetValue(SetTitelProperty); } set { SetValue(SetTitelProperty, value); } }

        private static void ChangeTitel(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            NumberTimeControl numberControl = sender as NumberTimeControl;
            numberControl.TitelLabel.Content = (string)e.NewValue;
        }


        public static readonly DependencyProperty SetMaxValueProperty = DependencyProperty.Register(
          "MaxValue", typeof(int), typeof(NumberTimeControl),
          new FrameworkPropertyMetadata(0, new PropertyChangedCallback(ChangeMaxValue)));

        public int MaxValue { get { return (int)GetValue(SetMaxValueProperty); } set { SetValue(SetMaxValueProperty, value); } }

        private static void ChangeMaxValue(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            NumberTimeControl control = sender as NumberTimeControl;
            if (control.MaxValue != (int)e.NewValue)
            {
                control.MaxValue = (int)e.NewValue;
            }
        }

        public static readonly DependencyProperty SetNumberContentProperty =
      DependencyProperty.Register("NumberContent", typeof(double), typeof(NumberTimeControl), new
         FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnNumberContentPropertyChanged));
        
        public double NumberContent
        {
            get { return (double)GetValue(SetNumberContentProperty); }
            set { SetValue(SetNumberContentProperty, value);}
        }
        
        private static void OnNumberContentPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            NumberTimeControl control = sender as NumberTimeControl;
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
            if (_numberContent >= MaxValue)
            {
                _numberContent = 0;
            }
            _numberContent *= 10;
            _numberContent += number;
            _numberContent = Math.Clamp(_numberContent, 0, MaxValue);
        }

        private void WriteLablewithLeadingZero()
        {
            _numberContent = (int)NumberContent;
            String label = "";

            if (_numberContent < 10)
            {
                label += "0";
            }
            label += _numberContent;       
            Mass_Input.Content = label;           
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

