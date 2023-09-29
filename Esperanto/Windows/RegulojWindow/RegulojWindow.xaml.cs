using System.Windows;
using Esperanto.Windows.RegulojWindow.Afiksoj;

namespace Esperanto.Windows.RegulojWindow;

public partial class RegulojWindow : Window
{
    public RegulojWindow()
    {
        InitializeComponent();
    }

    private void AfiksojWindow_Click(object sender, RoutedEventArgs e)
    {
        var newWindow = new AfiksojWindow();
        newWindow.Show();
        this.Close();
    }

    private void BackToMainPage_Click(object sender, RoutedEventArgs e)
    {
        WindowHelper.NavigateToWindow<MainWindow>(this);
    }
}