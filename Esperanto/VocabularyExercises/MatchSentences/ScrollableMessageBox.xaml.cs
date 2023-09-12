using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Esperanto.VocabularyExercises.MatchSentences
{
    public partial class ScrollableMessageBox : Window
    {
        public ObservableCollection<string> YourWordList { get; set; } // Ensure this is filled with your words
        private ObservableCollection<string> FilteredWordList { get; set; }
        private string path;
        public ScrollableMessageBox(string message,string path)
        {
            InitializeComponent();
            this.DataContext = this;
            this.path = path;

            // Split the message string into an array of words based on the newline character
            string[] words = message.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            // Populate the ObservableCollection with the words from the message
            YourWordList = new ObservableCollection<string>(words);
            FilteredWordList = new ObservableCollection<string>(YourWordList);
        }

        private void ComboBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var comboBox = sender as ComboBox;
            string typedText = comboBox.Text + e.Text;
            FilteredWordList.Clear();

            foreach (var item in YourWordList)
            {
                if (item.StartsWith(typedText, StringComparison.InvariantCultureIgnoreCase))
                {
                    FilteredWordList.Add(item);
                }
            }

            comboBox.ItemsSource = FilteredWordList;
        }

        private void ComboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            comboBox.IsDropDownOpen = true;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            string inputText = textBoxInput.Text;

            if (string.IsNullOrEmpty(inputText))
            {
                MessageBox.Show("\n"+"Please enter some text.");
                return;
            }

            // Append to existing CSV file
            try
            {

                File.AppendAllText(path, inputText);

                MessageBox.Show("Text saved to CSV file.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            
        }
    }
}