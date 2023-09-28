using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Esperanto.VocabularyExercises;
using Esperanto.VocabularyExercises.WordTranslate;

namespace Esperanto.Tabloj;

public partial class TablojWindow 
{
    private readonly DataHelper _dataHelper;

    private List<CsvData> _csvDataList;

    private int _score;
    private int _index;


    private ComboBoxHelper ComboBoxHelper { get; set; } =
        new(@"C:\Users\Jarek\RiderProjects\Esperanto\Esperanto\Resources\Tabloj");


    public TablojWindow()
    {
        InitializeComponent();
        DataContext = this;

        _dataHelper = new DataHelper();

        ComboBox.ItemsSource = ComboBoxHelper.GetOptionsForTable();

        _csvDataList = _dataHelper.ReadCsv(ComboBoxHelper.GetCorrectPath("Konjunkcioj", ".csv"),
            values => new CsvData(values));
      
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

    private void ChangeCurrentText_Click(object sender, RoutedEventArgs e)
    {
        ComboBoxHelper.ComboBoxItemModel selectedItem = (ComboBoxHelper.ComboBoxItemModel)ComboBox.SelectedItem;
        string path = ComboBoxHelper.GetCorrectPath(selectedItem.Text, ".csv");

        _csvDataList = _dataHelper.ReadCsv(path, values => new CsvData(values));

        MessageBoxHelper.ShowTable(path,_csvDataList);
        GiveNewWord();
        InputBox.Text = "ĈĉĜĝĤĥĴĵŜŝŬŭ";
    }

    
    private void BackToMainPage_Click(object sender, RoutedEventArgs e)
    {
        WindowHelper.NavigateToWindow<VocabularyExercisesWindow>(this);
    }
    private void GiveNewWord()
    {
        var random = new Random();
        var newList = _csvDataList
            .SelectMany(obj => Enumerable.Repeat(obj.English, (int)Math.Pow(2, obj.ProbabilityWeight - 1)))
            .ToList();
        var randomIndex = random.Next(0, newList.Count);
        
        var index =_csvDataList.FindIndex(data => data.English == newList[randomIndex]);
        BlockWord.Text = _csvDataList[index].English;
        BlockWord.Foreground = _csvDataList[index].Color;
    }
}