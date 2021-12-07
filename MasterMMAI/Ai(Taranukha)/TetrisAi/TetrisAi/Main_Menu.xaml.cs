using System;
using System.Windows;
using System.Windows.Controls;

namespace TetrisAi
{
    public partial class Menu : Page
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Uri("/Start_Menu.xaml", UriKind.Relative));
        }

        private void Option_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}