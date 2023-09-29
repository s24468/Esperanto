using System.Windows;

namespace Esperanto.Windows.RegulojWindow.Afiksoj;

public partial class AfiksojWindow : Window
{
    public AfiksojWindow()
    {
        InitializeComponent();
    }
    private void BackToMainPage_Click(object sender, RoutedEventArgs e)
    {
        WindowHelper.NavigateToWindow<RegulojWindow>(this);
    }
}