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
        private HelperData _helperData;

        public FirstLesson()
        {
            InitializeComponent();
            _helperData = new HelperData();
        }


        private void FamilioVortlisto_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.ItemsSource == null)
            {
                ReadFamilioCsv(@"C:\Users\Jarek\RiderProjects\Esperanto\Esperanto\OldStuff\Familio.csv");
                dataGrid.ItemsSource = dataArray;
            }

            if (!_helperData.isTableVisible)
            {
                _helperData.isTableVisible = true;
                dataGrid.Visibility = Visibility.Visible;
            }
            else
            {
                _helperData.isTableVisible = false;
                dataGrid.Visibility = Visibility.Hidden;
            }
        }

        private void ReadFamilioCsv(string path)
        {
            List<CsvDataFamilio> csvDataList = _helperData.ReadCsv(path, values => new CsvDataFamilio());//values

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