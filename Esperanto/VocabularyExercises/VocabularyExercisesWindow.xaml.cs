using System.Windows;
using System.Windows.Controls;
using Esperanto.VocabularyExercises.FillTheGaps;
using Esperanto.VocabularyExercises.MatchSentences;
using Esperanto.VocabularyExercises.WordTranslate;

namespace Esperanto.VocabularyExercises
{
    public partial class VocabularyExercisesWindow : Window
    {
        public VocabularyExercisesWindow()
        {
            InitializeComponent();
        }

        private void BackToMainPage_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void WordTranslate_Click(object sender, RoutedEventArgs e)
        {
            WordTranslateWindow main = null;
            if (sender is Button clickedButton)
            {
                string buttonText = clickedButton.Content as string;

                if (buttonText == "Translate the Word")
                {
                    main = new WordTranslateWindow("Casual");

                    // Button 1 was clicked, perform the corresponding action
                }
                else if (buttonText == "TY Translate the Word")
                {
                    main = new WordTranslateWindow("Teach Yourself"); 
                }
            }
            main.Show();
            this.Close();
        }
        private void FillTheGaps_Click(object sender, RoutedEventArgs e)
        {
            FillTheGapsWindow main = new FillTheGapsWindow();
            main.Show();
            this.Close();
        }

        private void MatchSentences_Click(object sender, RoutedEventArgs e)
        {
            MatchSentencesWindow main = new MatchSentencesWindow();
            main.Show();
            this.Close();
        }
        private void Tables_Click(object sender, RoutedEventArgs e)
        {
            MatchSentencesWindow main = new MatchSentencesWindow();
            main.Show();
            this.Close();
        }
    }
}