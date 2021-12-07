using System;
using System.Windows;
using System.Windows.Controls;

namespace TetrisAi
{
    public partial class StartMenu : Page
    {
        public StartMenu()
        {
            InitializeComponent();
        }

        private void Single_Play_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/GAME_PAGE.xaml", UriKind.Relative));
            GamePage.Mode = 1;
        }

        private void _2P_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/GAME_PAGE.xaml", UriKind.Relative));
            GamePage.Mode = 2;
            Application.Current.MainWindow.Width = 720;
        }

        private void vs2P_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/GAME_PAGE.xaml", UriKind.Relative));
            GamePage.Mode = 3;
            Application.Current.MainWindow.Width = 720;
        }

        private void vsAI_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/GAME_PAGE.xaml", UriKind.Relative));
            GamePage.Mode = 4;
            Application.Current.MainWindow.Width = 720;
        }

        private void AIGood_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/GAME_PAGE.xaml", UriKind.Relative));
            GamePage.Mode = 5;
            Application.Current.MainWindow.Width = 720;
            Application.Current.MainWindow.Height = 600;
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}