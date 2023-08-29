using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Esperanto.VocabularyExercises.WordTranslate;


namespace Esperanto.VocabularyExercises.FillTheGaps
{
    public partial class FillTheGapsWindow : Window
    {
        private Helper _helper;

        private List<CsvData> csvDataList;

        private int score;

        private int index;

        public FillTheGapsWindow()
        {
            InitializeComponent();
            _helper = new Helper();

            csvDataList = _helper.ReadCsv(ChoosenPath("Pronomoj1Sentences"), values => new CsvData(values));
            giveNewWord();
            KeyDown += CheckEnter_KeyDown;
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
                csvDataList[index].ProbabilityWeight = csvDataList[index].ProbabilityWeight == 1
                    ? 1
                    : csvDataList[index].ProbabilityWeight - 1;
            }
            else
            {
                csvDataList[index].ProbabilityWeight++;

                MessageBox.Show("correct answer: " + csvDataList[index].Esperanto);
            }

            giveNewWord();
        }

        private void giveNewWord()
        {
            Random random = new Random();

            List<string> newList = new List<string>();

            foreach (var obj in csvDataList)
            {
                for (int i = 0; i < Math.Pow(2,obj.ProbabilityWeight-1); i++)
                {
                    newList.Add(obj.English);
                }
            }

            int randomIndex = random.Next(0, newList.Count);

            index = csvDataList.FindIndex(data => data.English == newList[randomIndex]); //index which word

            BlockWord.Text = csvDataList[index].English;
            BlockWord.Foreground = csvDataList[index].Color;
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
            string path = clickedButton.Content.ToString();
            csvDataList = _helper.ReadCsv(ChoosenPath(path), values => new CsvData(values));

            showMessageBoxWithCsvData(path);

            giveNewWord();
            InputBox.Text = "ĈĉĜĝĤĥĴĵŜŝŬŭ";
        }

        private void showMessageBoxWithCsvData(string path)
        {
            StringBuilder tableBuilder = new StringBuilder();
            tableBuilder.AppendLine("Esperanto\tEnglish"); // Table headers

            foreach (var data in csvDataList)
            {
                tableBuilder.AppendLine($"{data.Esperanto}\t{data.English}");
            }

            string formattedTable = tableBuilder.ToString();

            MessageBox.Show(formattedTable, path); //, MessageBoxButtons.OK, MessageBoxIcon.Information
        }

        private void CheckEnter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CheckButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }

    }
}