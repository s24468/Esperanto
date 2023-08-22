using System.Windows;
using System.Threading.Tasks;

namespace Esperanto
{
    public partial class ZeroLesson : Window
    {
        private readonly MethodHelper _methodHelper;

        public ZeroLesson()
        {
            InitializeComponent();
            _methodHelper = new MethodHelper();
        }

        private void Alfabeto_Click(object sender, RoutedEventArgs e)
        {
            _methodHelper.showPhoto(
                @"C:\Users\Jarek\RiderProjects\Esperanto\Esperanto\ZeroLesson\Resources\AlfabetoPhoto.jpg",
                popupImage);
        }

        private void PrononcoNotes_Click(object sender, RoutedEventArgs e)
        {
        }

        private async void PrononcoRealExamples_Click(object sender, RoutedEventArgs e)
        {
            _methodHelper.showPhoto(@"C:\Users\Jarek\RiderProjects\Esperanto\Esperanto\ZeroLesson\Resources\story.jpg",
                popupImage);
            await Task.Delay(1500); // 3000 milliseconds = 3 seconds
            _methodHelper.doSound(@"C:\Users\Jarek\RiderProjects\Esperanto\Esperanto\ZeroLesson\Resources\story.mp3");
        }

        private void Nombroj_Click(object sender, RoutedEventArgs e)
        {
            _methodHelper.showPhoto(
                @"C:\Users\Jarek\RiderProjects\Esperanto\Esperanto\ZeroLesson\Resources\numbers.jpg", popupImage);
        }

        private void PraktikoNombroj_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
// MessageBox.Show($"Error: ");