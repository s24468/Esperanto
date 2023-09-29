using System.Collections.Generic;
using System.IO;

namespace Esperanto;

public class ComboBoxHelper
{

    private string CorrectPath { get; set; }
    public ComboBoxHelper(string correctPath)
    {
        CorrectPath = correctPath;
    }
    public string GetCorrectPath(string text, string format)
    {
        return CorrectPath + @"\" + text + format;
    }
    public IEnumerable<ComboBoxItemModel> GetOptionsForTable()
    {
        var fileNames = Directory.GetFiles(CorrectPath);
        var itemList = new List<ComboBoxItemModel>();

        foreach (var fileName in fileNames)
        {
            var nameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            itemList.Add(new ComboBoxItemModel(nameWithoutExtension));
        }
        return itemList;
    }


    public class ComboBoxItemModel
    {
        public string Text { get; set; }

        public ComboBoxItemModel(string text)
        {
            Text = text;
        }
    }
}