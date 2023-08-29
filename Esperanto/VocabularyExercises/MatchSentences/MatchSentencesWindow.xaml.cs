using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Esperanto.VocabularyExercises.WordTranslate;

namespace Esperanto.VocabularyExercises.MatchSentences
{
    public partial class MatchSentencesWindow : Window
    {
        QuizViewModel _viewModel;
        private Helper _helper;

        private List<CsvData> csvDataList;

        public MatchSentencesWindow()
        {
            InitializeComponent();
            // Populate _viewModel.Questions and _viewModel.Answers
            _helper = new Helper();

            csvDataList = _helper.ReadCsv(ChoosenPath("Pronomoj1Sentences"), values => new CsvData(values));
            _viewModel = new QuizViewModel(csvDataList);

            this.DataContext = _viewModel;

            // Make ObservableCollection update when individual items are modified
            BindingOperations.EnableCollectionSynchronization(_viewModel.Questions, new object());
            BindingOperations.EnableCollectionSynchronization(_viewModel.Answers, new object());
        }


        private string ChoosenPath(string buttonName)
        {
            return @"C:\Users\Jarek\RiderProjects\Esperanto\Esperanto\VocabularyExercises\Resources\" + buttonName +
                   ".csv";
        }

        private void OnDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("dragItem"))
            {
                e.Effects = DragDropEffects.Move;
            }
        }

        private void OnDrop(object sender, DragEventArgs e)
        {
            if (sender is ListBox listBox)
            {
                CsvData draggedItem = e.Data.GetData(typeof(CsvData)) as CsvData;
                if (listBox.Name == "QuestionsList")
                {
                    _viewModel.Answers.Remove(draggedItem);
                    _viewModel.Questions.Add(draggedItem);
                }
                else if (listBox.Name == "AnswersList")
                {
                    _viewModel.Questions.Remove(draggedItem);
                    _viewModel.Answers.Add(draggedItem);
                }
            }
        }

        private void OnMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is ListBox listBox)
            {
                var selectedItem = listBox.SelectedItem as CsvData;
                if (selectedItem != null)
                {
                    DragDrop.DoDragDrop(listBox, selectedItem, DragDropEffects.Move);
                }
            }
        }


        private void OnCheckAnswersClicked(object sender, RoutedEventArgs e)
        {
            _viewModel.CheckAnswers();
            // Show a MessageBox or update the UI based on the check result
        }
    }
}

public class QuizViewModel : INotifyPropertyChanged
{
    public ObservableCollection<CsvData> Questions { get; set; }
    public ObservableCollection<CsvData> Answers { get; set; }

    public QuizViewModel(List<CsvData> csvDataList)
    {
        Questions = new ObservableCollection<CsvData>(csvDataList);
        Answers = new ObservableCollection<CsvData>(
            new List<CsvData>(csvDataList)); // Clone or Shuffle list here if you want
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void CheckAnswers()
    {
        bool allCorrect = true;
        for (int i = 0; i < Questions.Count; i++)
        {
            if (Questions[i].Esperanto != Answers[i].Esperanto)
            {
                allCorrect = false;
            }
        }

        // Show a MessageBox based on the check result
        if (allCorrect)
        {
            MessageBox.Show("All answers are correct!");
        }
        else
        {
            MessageBox.Show("Some answers are incorrect. Please try again.");
        }
    }
}
// MessageBox.Show($"{Questions[i].Esperanto}"+"  "+$"{Answers[i].English}");