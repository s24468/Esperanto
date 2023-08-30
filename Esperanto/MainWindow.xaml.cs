using System.Windows;
using Esperanto.VocabularyExercises;

namespace Esperanto
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ZeroLesson_Click(object sender, RoutedEventArgs e)
        {
            ZeroLesson newWindow = new ZeroLesson();
            newWindow.Show();
            this.Close();
        }

        private void FirstLesson_Click(object sender, RoutedEventArgs e)
        {
            FirstLesson newWindow = new FirstLesson();
            newWindow.Show();
            this.Close();
        }

        private void VocabularyExercises_Click(object sender, RoutedEventArgs e)
        {
            VocabularyExercisesWindow newWindow = new VocabularyExercisesWindow();
            newWindow.Show();
            this.Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
} // MessageBox.Show($"Error: ");