using System.Windows;
using System.Threading.Tasks;

namespace Esperanto
{
    public partial class ZeroLesson : Window
    {
        private readonly HelperData _helperData;
        private readonly NumberPractise numberPractise;

        public ZeroLesson()
        {
            InitializeComponent();
            _helperData = new HelperData();
            numberPractise =
                new NumberPractise(
                    @"C:\Users\Jarek\RiderProjects\Esperanto\Esperanto\ZeroLesson\Resources\numbers.txt");
        }

        private void Alfabeto_Click(object sender, RoutedEventArgs e)
        {
            _helperData.showPhoto(
                @"C:\Users\Jarek\RiderProjects\Esperanto\Esperanto\ZeroLesson\Resources\AlfabetoPhoto.jpg",
                popupImage);
        }

        private void PrononcoNotes_Click(object sender, RoutedEventArgs e)
        {
        }

        private async void PrononcoRealExamples_Click(object sender, RoutedEventArgs e)
        {
            _helperData.showPhoto(@"C:\Users\Jarek\RiderProjects\Esperanto\Esperanto\ZeroLesson\Resources\story.jpg",
                popupImage);
            await Task.Delay(1500); // 3000 milliseconds = 3 seconds
            _helperData.doSound(@"C:\Users\Jarek\RiderProjects\Esperanto\Esperanto\ZeroLesson\Resources\story.mp3");
        }

        private void Nombroj_Click(object sender, RoutedEventArgs e)
        {
            _helperData.showPhoto(
                @"C:\Users\Jarek\RiderProjects\Esperanto\Esperanto\ZeroLesson\Resources\numbers.jpg", popupImage);
        }

        private void PraktikoNombroj_Click(object sender, RoutedEventArgs e)
        {
            if (numberPractise.isVisible == false)
            {
                numberPractise.showNewNumbers(textBlock,textBox);
                
            }
            else
            {
                numberPractise.hide(textBlock,textBox);
                numberPractise.isVisible = false;
            }
        }

        private void BackToMainPage_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}