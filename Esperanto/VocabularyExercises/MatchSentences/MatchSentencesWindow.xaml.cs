using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Esperanto.VocabularyExercises.WordTranslate;

namespace Esperanto.VocabularyExercises.MatchSentences
{
    public partial class MatchSentencesWindow : Window
    {
        private Helper _helper;
        private int numberOfItems = 8;

        private List<CsvData> csvDataList;
        public ObservableCollection<CsvData> Questions { get; set; }

        public ObservableCollection<CsvData> RandomQuestions { get; set; }

        public ObservableCollection<CsvData> RandomAnswers { get; set; }

        public MatchSentencesWindow()
        {
            InitializeComponent();
            _helper = new Helper();

            csvDataList = _helper.ReadCsv(ChoosenPath("SentencesMatch1"), values => new CsvData(values));
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
                    // Change the color for incorrect answers
                    RandomQuestions[i].Color = new SolidColorBrush(Colors.Red);
                    RandomAnswers[i].Color = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    // Reset the color for correct answers (optional)
                    RandomQuestions[i].Color = new SolidColorBrush(Colors.Green);
                    RandomAnswers[i].Color = new SolidColorBrush(Colors.Green);
                }
            }

            QuestionsList.Items.Refresh();
            AnswersList.Items.Refresh();

            if (areItemsInCorrectOrder)
            {
                MessageBox.Show("CORRECT!");
            }
            else
            {
                MessageBox.Show("It is incorrect :(");
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
                ChoosenPath(selectedItem);
            }
        }

        private void ChoosePath_Click(object sender, RoutedEventArgs e)
        {
            if (PathOptions.SelectedItem != null)
            {
                var selectedItem = (PathOptions.SelectedItem as ComboBoxItem).Content as string;
                var newPath = ChoosenPath(selectedItem);

                csvDataList = _helper.ReadCsv(newPath, values => new CsvData(values));
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
    }
}