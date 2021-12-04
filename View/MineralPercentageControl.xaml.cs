using Mining_Tool_3.mvvm;
using Mining_Tool_3.ViewModel;
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
    /// Interaction logic for MineralPercentageControl.xaml
    /// </summary>
    public partial class MineralPercentageControl : UserControl
    {

        private double _percentage = 0;

        public MineralPercentageControl()
        {
            InitializeComponent();
            writeLablewithLeadingZero();
        }

        public static readonly DependencyProperty SetInputProperty = DependencyProperty.Register(
          "Input", typeof(bool), typeof(MineralPercentageControl),
          new FrameworkPropertyMetadata(true, new PropertyChangedCallback(ChangeInput)));

        public bool Input { get { return (bool)GetValue(SetInputProperty); } set { SetValue(SetInputProperty, value); } }

        private static void ChangeInput(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            MineralPercentageControl control = sender as MineralPercentageControl;
            if (!(bool)e.NewValue)
            {
                control.Percent_Input.Tag = "";
                control.Percent_Input.IsEnabled = false;
                control.Percent_Input.Style = control.FindResource("DisableButton") as Style;
                control.Delete.IsEnabled = false;
                control.Delete.Content = "";                
                control.Delete.Style = control.FindResource("DisableButton2") as Style;
            }
        }

        public static readonly DependencyProperty SetPercentageProperty =
    DependencyProperty.Register("Percentage", typeof(double), typeof(MineralPercentageControl), new FrameworkPropertyMetadata(0.0,FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPercentagePropertyChanged));

        public double Percentage
        {
            get { return (double)GetValue(SetPercentageProperty); }
            set {SetValue(SetPercentageProperty, value);}
        }

        private static void OnPercentagePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            MineralPercentageControl control = sender as MineralPercentageControl;
            if (control != null)
            {
               control.writeLablewithLeadingZero();
            }
        }


        private void writeLablewithLeadingZero()
        {
            _percentage = Percentage*100.0;
            Percent_Input.Content = string.Format("{0,3:000.00}", _percentage) + "%";
        }

        private void subFromPercent()
        {
            _percentage = Math.Truncate(_percentage * 10);
        }

        private void addToPercent(int number)
        {
            _percentage = Math.Round(Percentage * 100.0,4);
            if (_percentage >= 100)
            {
                _percentage = 0;
            }
            _percentage *= 1000.0;
            _percentage += number;
            _percentage = Math.Clamp(_percentage, 0, 10000);
        }

        private void Button_KeyDown(object sender, KeyEventArgs e)
        {           
            if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                addToPercent(e.Key - Key.D0);
            }
            else if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                addToPercent(e.Key - Key.NumPad0);
            }
            else if (e.Key == Key.Back)
            {
                subFromPercent();
            }
            else if (e.Key == Key.Delete)
            {
                _percentage = 0;
            }
            Percentage = _percentage/10000.0;
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            Percentage = 0;        
            Keyboard.Focus(Percent_Input);
        }

    }
}
