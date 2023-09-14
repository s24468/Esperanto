using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Esperanto.VocabularyExercises.WordTranslate;

namespace Esperanto.VocabularyExercises.Tables
{
    public class HelperTables
    {
        public string ChoosenPath(string buttonName)
        {
            return @"C:\Users\Jarek\RiderProjects\Esperanto\Esperanto\VocabularyExercises\TeachYourselfResources\" +
                   buttonName +
                   ".csv";
        }

        public List<ComboBoxItemModel> getOptionsForTable()
        {
            var itemList = new List<ComboBoxItemModel>
            {
                new ComboBoxItemModel(""),
                new ComboBoxItemModel("Left"),
                new ComboBoxItemModel("Right"),
                new ComboBoxItemModel("First10")
            };
            return itemList;
        }
        

        // Bind the list to the ComboBox
        public class ComboBoxItemModel
        {
            public string Text { get; set; }
        
            public ComboBoxItemModel(string text)
            {
                Text = text;
            }
        }

        
    }
}