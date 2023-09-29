using System;
using System.Windows;
using Esperanto.Arto;
using Esperanto.Rakonto;
using Esperanto.Tabloj;
using Esperanto.Traduko;
using Esperanto.VocabularyExercises;
using Esperanto.Windows.RegulojWindow;

namespace Esperanto
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void VortlistoWindow_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new VortlistoWindow();
            newWindow.Show();
            this.Close();
        }
        private void ArtoWindow_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new ArtoWindow();
            newWindow.Show();
            this.Close();
        }
        private void TablojWindow_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new TablojWindow();
            newWindow.Show();
            this.Close();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RakontoWindow_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new RakontoWindow();
            newWindow.Show();
            this.Close();
        }

        private void RegulojWindow_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new RegulojWindow();
            newWindow.Show();
            this.Close();
        }
    }
} 