using Mining_Tool_3.Model;
using Mining_Tool_3.mvvm;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for CalculatorControl.xaml
    /// </summary>
    public partial class CalculatorControl : UserControl
    {
        public CalculatorControl()
        {
            InitializeComponent();
            Messenger.Instance.Register<String>(this, "SpeechRecognition", NotifySpeech);
            SpeechRecognition speechRecognition = new SpeechRecognition();
            speechRecognition.start();

           
        }

        private void NotifySpeech(string name)
        {
            if (name == "Meining")
            {
                ActivateMiningView();
                return;
            }
            if (name == "Raffinerie")
            {
                ActivateRafinerieView();
                return;
            }
        }

        private void SetMiningView(object sender, RoutedEventArgs e)
        {
            ActivateMiningView();
        }

        private void ActivateMiningView()
        {
            MiningViewButton.Style = FindResource("SelectedButton") as Style;
            RefinerieButton.Style = FindResource("DefaultButton") as Style;

            Element.Visibility = Visibility.Visible;
            ElementTab.Visibility = Visibility.Visible;

            ShipSelect.Visibility = Visibility.Visible;
            Grid.SetColumn(Cargo, 1);
            Grid.SetColumnSpan(Cargo, 1);

            Refinerie.Visibility = Visibility.Collapsed;
        }

        private void SetRefinerieView(object sender, RoutedEventArgs e)
        {
            ActivateRafinerieView();
        }

        private void ActivateRafinerieView()
        {
            RefinerieButton.Style = FindResource("SelectedButton") as Style;
            MiningViewButton.Style = FindResource("DefaultButton") as Style;

            Element.Visibility = Visibility.Collapsed;
            ElementTab.Visibility = Visibility.Collapsed;
            ShipSelect.Visibility = Visibility.Collapsed;

            Grid.SetColumn(Cargo, 0);
            Grid.SetColumnSpan(Cargo, 2);

            Refinerie.Visibility = Visibility.Visible;
        }

        private void ShipSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ShipSelect.SelectedIndex == 0)
            {
                Messenger.Instance.Send(Ship.PROSPECTOR, "ChangeShip");
            }
            if (ShipSelect.SelectedIndex == 1)
            {
                Messenger.Instance.Send(Ship.MOLE, "ChangeShip");
            }
        }
    }
}
