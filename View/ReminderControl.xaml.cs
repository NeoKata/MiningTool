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
    /// Interaction logic for ReminderControl.xaml
    /// </summary>
    public partial class ReminderControl : UserControl
    {
        public ReminderControl()
        {
            InitializeComponent();
            DataContext = new ReminderVM();
        }
    }
}
