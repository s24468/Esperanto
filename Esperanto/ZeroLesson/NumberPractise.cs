using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Esperanto
{
    public class NumberPractise
    {
        Dictionary<string, string> numbersDictionary = new Dictionary<string, string>();
        public bool isVisible = false;
        public NumberPractise(string filePath)
        {
            loadToDictionary(filePath);
        }

        private void loadToDictionary(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(' ');
                    if (parts.Length == 2)
                    {
                        string key = parts[0].Trim();
                        string value = parts[1].Trim();
                        numbersDictionary[key] = value;
                    }
                }
            }
        }


        public void showNewNumbers(TextBlock textBlock, TextBox textBox)
        {
            Random random = new Random();
            int x = random.Next(0, 4999);
            int y = random.Next(0, 5000);
            textBlock.Text = x + " + " + y;
            textBlock.Visibility = Visibility.Visible;
            textBox.Visibility = Visibility.Visible;
            isVisible = true;
        }

        public void hide(TextBlock textBlock, TextBox textBox)
        {
            textBlock.Visibility = Visibility.Hidden;
            textBox.Visibility = Visibility.Hidden;
        }
    }
}