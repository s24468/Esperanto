using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Esperanto.VocabularyExercises.WordTranslate
{
    public partial class WordTranslateWindow : Window
    {
        private Helper _helper;

        private List<CsvDataFamilio> csvDataList;

        private int score;

        private int index;

        public WordTranslateWindow()
        {
            InitializeComponent();
            _helper = new Helper();

            csvDataList = _helper.ReadCsv(ChoosenPath("Familio"), values => new CsvDataFamilio(values));
            giveNewWord();
        }

        private string ChoosenPath(string buttonName)
        {
            return @"C:\Users\Jarek\RiderProjects\Esperanto\Esperanto\VocabularyExercises\Resources\" + buttonName +
                   ".csv";
        }

        private void Check_Click(object sender, RoutedEventArgs e)
        {
            if (csvDataList[index].Esperanto == InputBox.Text)
            {
                score++;
                ScoreBoard.Text = "Score " + score;
            }

            giveNewWord();
        }

        private void giveNewWord()
        {
            Random random = new Random();
            index = random.Next(csvDataList.Count);
            BlockWord.Text = csvDataList[index].English;
        }

        private void BackToMainPage_Click(object sender, RoutedEventArgs e)
        {
            VocabularyExercisesWindow main = new VocabularyExercisesWindow();
            main.Show();
            this.Close();
        }

        private void ChoosePath_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button; 
            string x=  clickedButton.Content.ToString(); 
            csvDataList = _helper.ReadCsv(ChoosenPath(x), values => new CsvDataFamilio(values));
            giveNewWord();
        }
    }
}