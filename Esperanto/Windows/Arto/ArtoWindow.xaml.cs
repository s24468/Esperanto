using System;
using System.IO;
using System.Windows;

namespace Esperanto.Arto;

public partial class ArtoWindow
{
    private string CurrentCorrectText { get; set; }
    private ComboBoxHelper ComboBoxHelper { get; set; } = new(@"C:\Users\Jarek\RiderProjects\Esperanto\Esperanto\Resources\Arto");

    public ArtoWindow()
    {
        InitializeComponent();

        ComboBox.ItemsSource = ComboBoxHelper.GetOptionsForTable();
        LoadTextFromFile(ComboBoxHelper.GetCorrectPath("Rimo_Semajntagoj",".txt"));
    }


    private void ChangeRandomWords_Click(object sender, RoutedEventArgs e)
    {
        var text = TextInput.Text;
        var words = text.Split(new[] { ' ', '\t', '\n', '\r', '.', ',', ';', '!', '?' },
            StringSplitOptions.RemoveEmptyEntries);
        var random = new Random();

        foreach (var word in words)
        {
            if (char.IsLetter(word[0]) && random.Next(2) == 0)
            {
                text = text.Replace(word, "---");
            }
        }

        TextInput.Text = text;
    }


    private void CheckCorrectness_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show(TextInput.Text == CurrentCorrectText ? "Correct" : "This is not correct!");
    }

    private void BackToMainPage_Click(object sender, RoutedEventArgs e)
    {
        WindowHelper.NavigateToWindow<MainWindow>(this);
    }

    private void ChangeCurrentText_Click(object sender, RoutedEventArgs e)
    {
        var selectedItem = (ComboBoxHelper.ComboBoxItemModel)ComboBox.SelectedItem;
        LoadTextFromFile(ComboBoxHelper.GetCorrectPath(selectedItem.Text, ".txt"));
    }


    private void LoadTextFromFile(string filePath)
    {
        CurrentCorrectText = File.ReadAllText(filePath);
        TextInput.Text = CurrentCorrectText;
    }
}