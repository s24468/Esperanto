using System.Windows.Media;

namespace Esperanto
{
    public class CsvDataFamilio
    {
        public class CsvData
        {
            public string Esperanto { get; set; }

            public string English { get; set; }
            // Add other properties as needed

            public Brush CellBackgroundColor { get; set; } // Background color property

            public CsvData(string[] array)
            {
                Esperanto = array[0];
                English = array[1];
            }
        }

        public SolidColorBrush CellBackgroundColor { get; set; }
    }
}