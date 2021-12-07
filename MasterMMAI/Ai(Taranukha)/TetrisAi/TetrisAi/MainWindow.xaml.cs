using System.Windows;
using System.Windows.Input;

namespace TetrisAi
{
    public partial class MainWindow : Window
    {
        public static bool SoundMode = true;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            GamePage.MainControl = e.Key.ToString();
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            GamePage.MainControl = null;
        }
    }
}