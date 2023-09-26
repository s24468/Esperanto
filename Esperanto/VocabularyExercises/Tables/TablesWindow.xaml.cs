using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Esperanto.VocabularyExercises.WordTranslate;

namespace Esperanto.VocabularyExercises.Tables
{
    public partial class TablesWindow : Window
    {
        private readonly DataHelper _dataHelper;

        private List<CsvData> _csvDataList;
        public event PropertyChangedEventHandler PropertyChanged;

        private int _score;
        private int _index;
        public string currentChosenButtons;
        public string currentSentenceTranslation;

        private ObservableCollection<string> _buttonContentsLeft;
        private ObservableCollection<string> _buttonContentsRight;

        public ObservableCollection<string> ButtonContentsLeft
        {
            get => _buttonContentsLeft;
            set
            {
                _buttonContentsLeft = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> ButtonContentsRight
        {
            get => _buttonContentsRight;
            set
            {
                _buttonContentsRight = value;
                OnPropertyChanged();
            }
        }

        private readonly HelperTables _helperTables;


        public TablesWindow()
        {
            InitializeComponent();
            DataContext = this;
          
            ButtonContentsLeft = new ObservableCollection<string>
            {
                "Tabloj",
                "Rakonto"
            };
            ButtonContentsRight = new ObservableCollection<string>
            {
                "Initial",
                "Initial2",
            };
            _dataHelper = new DataHelper();

            _helperTables = new HelperTables();

            comboBox.ItemsSource = _helperTables.getOptionsForTable();

            _csvDataList = _dataHelper.ReadCsv(_helperTables.ChoosenPath(@"Tabloj\Korelativoj"),
                values => new CsvData(values));
            currentChosenButtons = @"Tabloj\";
            GiveNewWord();
            WindowHelper.SetupEnterKeyToClick(this, CheckButton);
        }


        private void Check_Click(object sender, RoutedEventArgs e)
        {
            var currentData = _csvDataList[_index];
            if (_csvDataList[_index].Esperanto == InputBox.Text)
            {
                ScoreBoard.Text = $"Score {++_score}";
                currentData.ProbabilityWeight = Math.Max(1, currentData.ProbabilityWeight - 1);
            }
            else
            {
                _csvDataList[_index].ProbabilityWeight++;
                MessageBox.Show("correct answer: " + _csvDataList[_index].Esperanto);
            }

            GiveNewWord();
        }

        private void ChoosePath_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            HelperTables.ComboBoxItemModel selectedItem = (HelperTables.ComboBoxItemModel)comboBox.SelectedItem;
            string path = currentChosenButtons + @"\" + clickedButton.Content ;
            if (selectedItem != null)
            {
                path += selectedItem.Text;
            }

            if (!File.Exists(_helperTables.ChoosenPath(path)))
            {
                return;
            }
            _csvDataList = _dataHelper.ReadCsv(_helperTables.ChoosenPath(path), values => new CsvData(values));

            ShowMessageBoxWithCsvData(path);

            GiveNewWord();
            InputBox.Text = "ĈĉĜĝĤĥĴĵŜŝŬŭ";
        }

        private void ChooseButtons_Click(object sender, RoutedEventArgs e)
        {
            var buttonContent = (string)((Button)sender).Content;
            currentChosenButtons = buttonContent;
            switch (buttonContent)
            {
                case "Tabloj":
                    ButtonContentsRight[0] = "Korelativoj";
                    ButtonContentsRight[1] = "Konjunkcioj";
                    break;
                case "Rakonto":
                    ButtonContentsRight[0] = "KorelativojEnRakonto";
                    ButtonContentsRight[1] = "KonjunkciojEnRakonto";
                    break;
            }
        }

        private void GiveNewWord()
        {
            var random = new Random();
            var newList = _csvDataList
                .SelectMany(obj => Enumerable.Repeat(obj.English, (int)Math.Pow(2, obj.ProbabilityWeight - 1)))
                .ToList();
            var randomIndex = random.Next(0, newList.Count);
            _index = _csvDataList.FindIndex(data => data.English == newList[randomIndex]); //index which word
            string[] parts = _csvDataList[_index].English.Split('@');
            if (parts.Length > 1)
            {
                currentSentenceTranslation = parts[1];
            }
            BlockWord.Text = parts[0];
            BlockWord.Foreground = _csvDataList[_index].Color;
        }

        private void ShowMessageBoxWithCsvData(string path)
        {
            var table = string.Join("\n", _csvDataList.Select(data => $"{data.Esperanto}\t{data.English}"));
            MessageBox.Show($"Esperanto\tEnglish\n{table}", path);
        }

        private void BackToMainPage_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.NavigateToWindow<VocabularyExercisesWindow>(this);
        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ShowMeAllNames_Click(object sender, RoutedEventArgs e)
        {
            string[] fileNames = Directory.GetFiles("C:\\Users\\Jarek\\RiderProjects\\Esperanto\\Esperanto\\VocabularyExercises\\TeachYourselfResources\\Tabloj");
            String x = "";
            foreach (string fileName in fileNames)
            {
                x+= Path.GetFileName(fileName) + Environment.NewLine;
            }
            string[] fileNames2 = Directory.GetFiles("C:\\Users\\Jarek\\RiderProjects\\Esperanto\\Esperanto\\VocabularyExercises\\TeachYourselfResources\\Rakonto");
            String y = "";
            foreach (string fileName in fileNames2)
            {
                y+= Path.GetFileName(fileName) + Environment.NewLine;
            }
            MessageBox.Show(x+"\n"+y);
        }

        private void ShowMeAllTranslationOfSentence_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(currentSentenceTranslation);
        }
    }
}