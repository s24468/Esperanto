using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Esperanto
{
    public partial class FirstLesson : Window
    {
        private List<CsvDataFamilio> dataArray = new List<CsvDataFamilio>();
        private Helper _helper;

        public FirstLesson()
        {
            InitializeComponent();
            _helper = new Helper();
        }


        private void FamilioVortlisto_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.ItemsSource == null)
            {
                ReadFamilioCsv(@"C:\Users\Jarek\RiderProjects\Esperanto\Esperanto\FirstLesson\CsvDataFamilio.cs");
                dataGrid.ItemsSource = dataArray;
            }

            if (!_helper.isTableVisible)
            {
                _helper.isTableVisible = true;
                dataGrid.Visibility = Visibility.Visible;
            }
            else
            {
                _helper.isTableVisible = false;
                dataGrid.Visibility = Visibility.Hidden;
            }
        }

        private void ReadFamilioCsv(string path)
        {
            List<CsvDataFamilio> csvDataList = _helper.ReadCsv(path, values => new CsvDataFamilio());//values

            foreach (var csvData in csvDataList)
            {
                dataArray.Add(csvData);
            }

            for (var i = 0; i < dataArray.Count; i++)
            {
                var d = dataArray[i];

                d.CellBackgroundColor = (i % 3 == 1) ? Brushes.Pink :
                    (i % 3 == 2) ? Brushes.Coral :
                    d.CellBackgroundColor;
            }
        }


        private void BackToMainPage_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}