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
        }

        private void SetMiningView(object sender, RoutedEventArgs e)
        {
            MiningViewButton.Style = FindResource("SelectedButton") as Style;
            RefinerieButton.Style = FindResource("DefaultButton") as Style;

            Element.Visibility = Visibility.Visible;
            ElementTab.Visibility = Visibility.Visible;

            Grid.SetColumn(Cargo, 1);
            Grid.SetColumnSpan(Cargo, 1);

            Refinerie.Visibility = Visibility.Collapsed;
        }
        private void SetRefinerieView(object sender, RoutedEventArgs e)
        {
            RefinerieButton.Style = FindResource("SelectedButton") as Style;
            MiningViewButton.Style = FindResource("DefaultButton") as Style;
            
            Element.Visibility = Visibility.Collapsed;
            ElementTab.Visibility = Visibility.Collapsed;
            
            Grid.SetColumn(Cargo, 0);
            Grid.SetColumnSpan(Cargo, 2);

            Refinerie.Visibility = Visibility.Visible;
        }
    }
}
