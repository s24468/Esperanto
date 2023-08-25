using System.Windows;
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
            WordTranslateWindow main = new WordTranslateWindow();
            main.Show();
            this.Close();
        }
    }
}