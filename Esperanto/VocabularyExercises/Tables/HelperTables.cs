using System.Collections.Generic;
using System.Text;
using System.Windows;
using Esperanto.VocabularyExercises.WordTranslate;

namespace Esperanto.VocabularyExercises.Tables
{
    public class HelperTables
    {

        public void showMessageBoxWithCsvData(string path,List<CsvData> csvDataList)
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
    }
}