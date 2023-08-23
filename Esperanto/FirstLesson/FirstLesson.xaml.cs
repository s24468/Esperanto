using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace Esperanto
{
    public partial class FirstLesson : Window
    {
        private List<CsvData> dataArray = new List<CsvData>();
        private MethodHelper _methodHelper;

        public FirstLesson()
        {
            InitializeComponent();
            _methodHelper = new MethodHelper();
        }


        private void DataDomoKajFamilioVortlisto_Click(object sender, RoutedEventArgs e)
        {
            ReadCsv();
            dataGrid.ItemsSource = dataArray;
        }

        private void ReadCsv()
        {
            using (var reader =
                   new StreamReader(
                       @"C:\Users\Jarek\RiderProjects\Esperanto\Esperanto\FirstLesson\Resources\FamilioKajDomo.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(' ');

                    var data = new CsvData
                    {
                        Esperanto = values[0],
                        English = values[1]
                    };
                    dataArray.Add(data);
                    int x = 1;
                    foreach (var d in dataArray)
                    {
                        if (x % 3 == 2)
                        {
                            d.CellBackgroundColor = Brushes.Pink; // You can set any Brush here
                        }

                        if (x % 3 == 0)
                        {
                            d.CellBackgroundColor = Brushes.Coral;
                        }

                        x++;
                    }
                }
            }
        }

        private void BackToMainPage_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }

    public class CsvData
    {
        public string Esperanto { get; set; }

        public string English { get; set; }
        // Add other properties as needed

        public Brush CellBackgroundColor { get; set; } // Background color property
    }
}