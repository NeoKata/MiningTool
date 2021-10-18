using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Mining_Tool_3
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            EventManager.RegisterClassHandler(typeof(Button), Button.KeyDownEvent, new KeyEventHandler(TextBox_KeyDown));
        }


        void TextBox_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                MoveToNextUIElement(e);
            }
        }

        void MoveToNextUIElement(KeyEventArgs e)
        {

            TraversalRequest next = new TraversalRequest(FocusNavigationDirection.Next);
            UIElement nextElement = null;
            Button nextButton = null;
            Button currentElement = Keyboard.FocusedElement as Button;
            currentElement.MoveFocus(next);
            while (true)
            {
                nextElement = Keyboard.FocusedElement as UIElement;
                nextButton = nextElement as Button;
                if (nextButton != null && nextButton.Tag != null && nextButton.Tag.ToString() == "Input_Control")
                {
                    break;
                }
                nextElement.MoveFocus(next);
            }

            if (nextButton.Focus())
            {
                e.Handled = true;
            }
        }
    }
}
