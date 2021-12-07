using Mining_Tool_3.Model;
using Mining_Tool_3.mvvm;
using Mining_Tool_3.ViewModel;
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
    /// Interaction logic for RefinerieControl.xaml
    /// </summary>
    public partial class RefinerieControl : UserControl
    {
        public RefinerieControl()
        {
            DataContext = new RefinerieVM();
            InitializeComponent();
        }

        public static readonly DependencyProperty SetCargoProperty = DependencyProperty.Register(
         "Cargo", typeof(CargoVM), typeof(RefinerieControl),
         new FrameworkPropertyMetadata(null, new PropertyChangedCallback(ChangeCargo)));

        public CargoVM Cargo { get { return (CargoVM)GetValue(SetCargoProperty); } set { SetValue(SetCargoProperty, value); } }

        private static void ChangeCargo(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            RefinerieControl control = sender as RefinerieControl;
            RefinerieVM refinerieVM = control.DataContext as RefinerieVM;
            refinerieVM.Cargo = e.NewValue as CargoVM;
        }

        private void DataGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RefinerieVM viewModel = DataContext as RefinerieVM;
            if (viewModel.RowDetailsVisible == DataGridRowDetailsVisibilityMode.Collapsed)
            {
                viewModel.RowDetailsVisible = DataGridRowDetailsVisibilityMode.VisibleWhenSelected;
            }
            else
            {
                viewModel.RowDetailsVisible = DataGridRowDetailsVisibilityMode.Collapsed;
            }
        }

        private void MyDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefinerieVM viewModel = DataContext as RefinerieVM;
            if (viewModel.RowDetailsVisible == DataGridRowDetailsVisibilityMode.Collapsed)
            {
                viewModel.RowDetailsVisible = DataGridRowDetailsVisibilityMode.VisibleWhenSelected;
            }
        }
    }
}
