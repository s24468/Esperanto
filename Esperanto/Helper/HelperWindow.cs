using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Esperanto.VocabularyExercises;

namespace Esperanto
{
    public class HelperWindow
    {
        public static void NavigateToWindow<T>(Window currentWindow) where T : Window, new()
        {
            T targetWindow = new T();
            targetWindow.Show();
            currentWindow.Close();
        }
        public static void SetupEnterKeyToClick(Window window, Button targetButton)
        {
            window.KeyDown += (sender, e) => 
            {
                if (e.Key == Key.Enter)
                {
                    targetButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                }
            };
        }
    }
}