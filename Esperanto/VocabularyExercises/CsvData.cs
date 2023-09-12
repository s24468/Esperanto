using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media;

namespace Esperanto.VocabularyExercises.WordTranslate
{
    public class CsvData
    {
        public string Esperanto { get; set; }

        public string English { get; set; }
        public int ProbabilityWeight { get; set; }

        private Brush color;
        public Brush Color
        {
            get { return color; }
            set
            {
                if (color != value)
                {
                    color = value;
                    OnPropertyChanged("Color");
                }
            }
        }

        public CsvData(string[] array)
        {
            Esperanto = array[0];
            English = array[1];
            ProbabilityWeight = 1;
            Color = ConvertValueToColor(ProbabilityWeight);
        }
        public static int CompareCsvDataByColor(CsvData a, CsvData b)
        {
            // Assuming SolidColorBrush, but the logic can be adapted for other brushes
            var colorA = (a.Color as SolidColorBrush)?.Color;
            var colorB = (b.Color as SolidColorBrush)?.Color;

            if (colorA == Colors.Red && colorB != Colors.Red) return -1; // Red comes first
            if (colorA != Colors.Red && colorB == Colors.Red) return 1;  // Green comes later
            return 0; // Equal, or both are red/green
        }
        public static void SortObservableCollectionByColour(ObservableCollection<CsvData> collection)
        {
            var sortedList = collection.OrderBy(x => x, new ComparisonComparer<CsvData>(CsvData.CompareCsvDataByColor))
                .ToList();

            for (int i = 0; i < sortedList.Count; i++)
            {
                collection.Move(collection.IndexOf(sortedList[i]), i);
            }
        }


        public static SolidColorBrush ConvertValueToColor(double value)
        {
            byte red, green, blue;

            if (value < 4)
            {
                // Original behavior: value affects both red and green
                red = (byte)(Math.Max(0, Math.Min(255, value * 90)));
                green = (byte)(Math.Max(0, Math.Min(255, value * 90 + 165)));
                blue = 128; // Constant blue
            }
            else
            {
                red = 255;
                green = (byte)(Math.Max(0, Math.Min(255, 510 - value * 90)));
                blue = (byte)(Math.Max(0, Math.Min(128, 256 - (value - 4) * 40)));
            }
            return new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, red, green, blue));
        }
        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}