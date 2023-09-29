using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Esperanto.VocabularyExercises.WordTranslate;

namespace Esperanto;

public class MessageBoxHelper
{
    public static void ShowTable(string path, List<CsvData> _csvDataList )
    {
        var table = string.Join("\n", _csvDataList.Select(data => $"{data.Esperanto}\t{data.English}"));
        MessageBox.Show($"Esperanto\tEnglish\n{table}", path);
    }
}