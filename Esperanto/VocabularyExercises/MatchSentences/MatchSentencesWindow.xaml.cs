using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Esperanto.VocabularyExercises.WordTranslate;

// VortListoSentencesMatch1
namespace Esperanto.VocabularyExercises.MatchSentences
{
    public partial class MatchSentencesWindow : Window
    {
        private HelperData _helperData;
        private int numberOfItems = 8;
        private string currentPath = "";

        private List<CsvData> csvDataList;
        public ObservableCollection<CsvData> Questions { get; set; }

        public ObservableCollection<CsvData> RandomQuestions { get; set; }

        public ObservableCollection<CsvData> RandomAnswers { get; set; }

        public MatchSentencesWindow()
        {
            InitializeComponent();
            _helperData = new HelperData();

            csvDataList = _helperData.ReadCsv(ChoosenPath("SentencesMatch1"), values => new CsvData(values));
            Questions = new ObservableCollection<CsvData>(csvDataList);

            var randomTenItems = Questions.OrderBy(x => Guid.NewGuid()).Take(numberOfItems).ToList();
            RandomQuestions = new ObservableCollection<CsvData>(randomTenItems.OrderBy(x => Guid.NewGuid()));
            RandomAnswers = new ObservableCollection<CsvData>(randomTenItems.OrderBy(x => Guid.NewGuid()));

            this.DataContext = this; // Set the data context
        }


        private string ChoosenPath(string buttonName)
        {
            return @"C:\Users\Jarek\RiderProjects\Esperanto\Esperanto\VocabularyExercises\Resources\" + buttonName +
                   ".csv";
        }

        private void Check_click(object sender, RoutedEventArgs e)
        {
            bool areItemsInCorrectOrder = true;

            for (int i = 0; i < RandomQuestions.Count; i++)
            {
                if (!RandomQuestions[i].Equals(RandomAnswers[i]))
                {
                    areItemsInCorrectOrder = false;
                    RandomQuestions[i].Color = new SolidColorBrush(Colors.Red);
                    RandomAnswers[i].Color = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    RandomQuestions[i].Color = new SolidColorBrush(Colors.Green);
                    RandomAnswers[i].Color = new SolidColorBrush(Colors.Green);
                }
            }

            CsvData.SortObservableCollectionByColour(RandomQuestions);
            CsvData.SortObservableCollectionByColour(RandomAnswers);

            QuestionsList.Items.Refresh();
            AnswersList.Items.Refresh();

            if (areItemsInCorrectOrder)
            {
                MessageBox.Show("CORRECT!");
            }
        }


        private void MoveUp_Click(object sender, RoutedEventArgs e)
        {
            MoveItem(-1);
        }

        private void MoveDown_Click(object sender, RoutedEventArgs e)
        {
            MoveItem(1);
        }

        private void MoveItem(int direction)
        {
            // Validate
            if (QuestionsList.SelectedItem == null || QuestionsList.SelectedIndex < 0)
                return;

            int newIndex = QuestionsList.SelectedIndex + direction;

            // Validate New Index
            if (newIndex < 0 || newIndex >= RandomQuestions.Count)
                return;

            // Remove and insert
            CsvData selected = (CsvData)QuestionsList.SelectedItem;
            RandomQuestions.RemoveAt(QuestionsList.SelectedIndex);
            RandomQuestions.Insert(newIndex, selected);

            // Reset selection
            QuestionsList.SelectedIndex = newIndex;
        }


        private void BackToMainPage_Click(object sender, RoutedEventArgs e)
        {
            VocabularyExercisesWindow main = new VocabularyExercisesWindow();
            main.Show();
            this.Close();
        }

        private void PathOptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PathOptions.SelectedItem != null)
            {
                var selectedItem = (PathOptions.SelectedItem as ComboBoxItem).Content as string;
                currentPath = selectedItem;

            }
        }

        private void ChoosePath_Click(object sender, RoutedEventArgs e)
        {
            if (PathOptions.SelectedItem != null)
            {
                var selectedItem = (PathOptions.SelectedItem as ComboBoxItem).Content as string;
                var newPath = ChoosenPath(selectedItem);

                csvDataList = _helperData.ReadCsv(newPath, values => new CsvData(values));
                Questions = new ObservableCollection<CsvData>(csvDataList);

                Application.Current.Dispatcher.Invoke(() =>
                {
                    RandomQuestions.Clear();
                    RandomAnswers.Clear();
                    var randomTenItems = Questions.OrderBy(x => Guid.NewGuid()).Take(numberOfItems).ToList();
                    RandomQuestions = new ObservableCollection<CsvData>(randomTenItems.OrderBy(x => Guid.NewGuid()));
                    RandomAnswers = new ObservableCollection<CsvData>(randomTenItems.OrderBy(x => Guid.NewGuid()));

                    QuestionsList.Items.Refresh();
                    AnswersList.Items.Refresh();
                });

                this.DataContext = null;
                this.DataContext = this;
            }
        }

        private void Show_Click(object sender, RoutedEventArgs e)
        {
            List<CsvData> csvDataListTemp =
                _helperData.ReadCsv(ChoosenPath("VortListo"+currentPath) , values => new CsvData(values));
          
            // Constructing the string to display
            string message = "";
            foreach (var item in csvDataListTemp)
            {
                message += $"{item.Esperanto} - {item.English}\n";
            }

            // Displaying in custom dialog
            var scrollableMessageBox = new ScrollableMessageBox(message,ChoosenPath("VortListo"+currentPath));
            scrollableMessageBox.ShowDialog();
            
            
        }
    }
    
}