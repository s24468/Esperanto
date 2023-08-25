using System.Windows.Media;

namespace Esperanto.VocabularyExercises.WordTranslate
{
    public class CsvData
    {
        public string Esperanto { get; set; }

        public string English { get; set; }

        public Brush CellBackgroundColor { get; set; } // Background color property

        public CsvData(string[] array)
        {
            Esperanto = array[0];
            English = array[1];
        }
    }
}