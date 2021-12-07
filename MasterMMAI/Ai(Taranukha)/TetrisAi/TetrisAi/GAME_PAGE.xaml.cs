using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace TetrisAi
{
    public partial class GamePage : Page
    {
        public static int Mode = 1;
        public static bool Guide = true;
        public static string MainControl = null;
        private readonly double[] AI = {0, -0.4865, 0.7735, -0.9865, -0.1865};
        public double[,] AI_Good = new double[5, 5];
        private readonly double[] AI2 = new double[5];
        public DispatcherTimer AItimer = new();
        private int count;
        private bool EyeMode = true;
        private int gamecount = 1;
        public double GameSpeed = 500;
        public DispatcherTimer gamespeedtimer = new();
        public string OldControl;
        private int score;
        public Tetris Tetris = new(10, 22);
        public Tetris Tetris2 = new(10, 22);
        public DispatcherTimer timer = new();

        public GamePage()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromMilliseconds(GameSpeed / 500);
            timer.Tick += timer_Tick;
            timer.Start();

            gamespeedtimer.Interval = TimeSpan.FromMilliseconds(GameSpeed);
            gamespeedtimer.Tick += GameTimer_Tick;
            gamespeedtimer.Start();

            AItimer.Interval = TimeSpan.FromMilliseconds(2000);
            AItimer.Tick += AiTimer_Tick;
            AItimer.Start();

            Tetris.NewBlock();
            if (Mode != 1)
                Tetris2.NewBlock();

            if (Mode == 5)
            {
                AI_Good[0, 0] = 1;

                for (var i = 1; i < 5; i++) AI_Good[0, i] = AI[i];

                while (true)
                {
                    for (var i = 0; i < 5; i++)
                    {
                        var rnd = new Random();
                        AI2[i] = AI_Good[0, i] + rnd.NextDouble() / 5 * 2 - 0.2;
                    }

                    if (AI2[2] <= 1 && AI2[2] >= 0 && AI2[1] >= -1 && AI2[1] <= 0 && AI2[3] >= -1 && AI2[3] <= 0 &&
                        AI2[4] >= -1 && AI2[4] <= 0) break;
                }

                Gamecount.Text = gamecount.ToString();
                _1p_a.Text = AI[1].ToString("F4");
                _1p_b.Text = AI[2].ToString("F4");
                _1p_c.Text = AI[3].ToString("F4");
                _1p_d.Text = AI[4].ToString("F4");
                _2p_a.Text = AI2[1].ToString("F4");
                _2p_b.Text = AI2[2].ToString("F4");
                _2p_c.Text = AI2[3].ToString("F4");
                _2p_d.Text = AI2[4].ToString("F4");
                one0.Text = AI_Good[0, 0].ToString();
                one1.Text = AI_Good[0, 1].ToString("F4");
                one2.Text = AI_Good[0, 2].ToString("F4");
                one3.Text = AI_Good[0, 3].ToString("F4");
                one4.Text = AI_Good[0, 4].ToString("F4");
            }

            level1.Text = Tetris.Level.ToString();
            level2.Text = Tetris2.Level.ToString();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (OldControl != MainControl)
            {
                OldControl = MainControl;
                switch (MainControl)
                {
                    case "Up":
                        Tetris.Rotation();
                        break;
                    case "Down":
                        Tetris.MoveDown();
                        break;
                    case "Left":
                        Tetris.MoveLeft();
                        break;
                    case "Right":
                        Tetris.MoveRight();
                        break;
                    case "M":
                        Tetris.MoveGround();
                        break;
                    case "H":
                        Tetris.AIVirtualMove(AI[1], AI[2], AI[3], AI[4]);
                        break;
                    case "W":
                        Tetris2.Rotation();
                        break;
                    case "S":
                        Tetris2.MoveDown();
                        break;
                    case "A":
                        Tetris2.MoveLeft();
                        break;
                    case "D":
                        Tetris2.MoveRight();
                        break;
                    case "G":
                        Tetris2.MoveGround();
                        break;
                }
            }

            score++;

            if (score / 1000 == 1 && Mode != 5)
            {
                score = Tetris.Score;
                level1.Text = Tetris.Level++.ToString();
                level2.Text = Tetris2.Level++.ToString();
                GameSpeed = GameSpeed / 10 * 9;
                gamespeedtimer.Interval = TimeSpan.FromMilliseconds(GameSpeed);
                score = 0;
            }

            if (count >= 15)
            {
                Canvas.Children.Clear();
                Tetris.CanvasClear = true;
            }

            if (EyeMode)
            {
                if (Mode != 1) Tetris2.DrawBoard(this, true, Guide);
                Tetris.DrawBoard(this, false, Guide);
            }

            if (count >= 15)
            {
                count = 0;
                Tetris.CanvasClear = false;
            }


            if (Mode == 5)
            {
                Tetris.AIVirtualMove(AI[1], AI[2], AI[3], AI[4]);
                Tetris2.AIVirtualMove(AI2[1], AI2[2], AI2[3], AI2[4]);
            }

            if (Tetris.IsGameOver() || Tetris2.IsGameOver())
            {
                if (Mode == 1)
                {
                    MessageBox.Show("게임 오버");
                    timer.Stop();
                    gamespeedtimer.Stop();
                }
                else if (Mode == 2)
                {
                    if (Tetris.IsGameOver())
                    {
                        Tetris = new Tetris(10, 22);
                        Tetris.NewBlock();
                    }
                    else if (Tetris2.IsGameOver())
                    {
                        Tetris2 = new Tetris(10, 22);
                        Tetris2.NewBlock();
                    }
                }
                else if (Mode == 3)
                {
                    if (Tetris.IsGameOver()) MessageBox.Show("2P Win!");
                    else
                        MessageBox.Show("1P Win!");

                    timer.Stop();
                    gamespeedtimer.Stop();
                }
                else if (Mode == 4)
                {
                    if (Tetris.IsGameOver()) MessageBox.Show("You Lose..");
                    else
                        MessageBox.Show("1P Win!");

                    timer.Stop();
                    gamespeedtimer.Stop();
                }
                else if (Mode == 5)
                {
                    if (Tetris.IsGameOver())
                    {
                        AI[0] = Tetris.Score;
                        for (var j = 4; j >= 0; j--)
                            if (AI_Good[j, 0] <= Tetris.Score)
                                for (var i = 0; i < 5; i++)
                                {
                                    if (j + 1 < 5)
                                        AI_Good[j + 1, i] = AI_Good[j, i];
                                    AI_Good[j, i] = AI[i];
                                }

                        while (true)
                        {
                            for (var i = 0; i < 5; i++)
                            {
                                var rnd = new Random();
                                AI[i] = AI_Good[0, i] + rnd.NextDouble() / 5 * 2 - 0.2;
                            }

                            if (AI[2] <= 1 && AI[2] >= 0 && AI[1] >= -1 && AI[1] <= 0 && AI[3] >= -1 && AI[3] <= 0 &&
                                AI[4] >= -1 && AI[4] <= 0) break;
                        }

                        Tetris = new Tetris(10, 22);
                        Tetris.NewBlock();
                    }
                    else if (Tetris2.IsGameOver())
                    {
                        AI2[0] = Tetris2.Score;
                        for (var j = 4; j >= 0; j--)
                            if (AI_Good[j, 0] <= Tetris2.Score)
                                for (var i = 0; i < 5; i++)
                                {
                                    if (j + 1 < 5)
                                        AI_Good[j + 1, i] = AI_Good[j, i];
                                    AI_Good[j, i] = AI2[i];
                                }

                        while (true)
                        {
                            for (var i = 0; i < 5; i++)
                            {
                                var rnd = new Random();
                                AI2[i] = AI_Good[0, i] + rnd.NextDouble() / 5 * 2 - 0.2;
                            }

                            if (AI2[2] <= 1 && AI2[2] >= 0 && AI2[1] >= -1 && AI2[1] <= 0 && AI2[3] >= -1 &&
                                AI2[3] <= 0 && AI2[4] >= -1 && AI2[4] <= 0) break;
                        }

                        Tetris2 = new Tetris(10, 22);
                        Tetris2.NewBlock();
                    }


                    gamecount++;
                    Gamecount.Text = gamecount.ToString();
                    _1p_a.Text = AI[1].ToString("F4");
                    _1p_b.Text = AI[2].ToString("F4");
                    _1p_c.Text = AI[3].ToString("F4");
                    _1p_d.Text = AI[4].ToString("F4");
                    _2p_a.Text = AI2[1].ToString("F4");
                    _2p_b.Text = AI2[2].ToString("F4");
                    _2p_c.Text = AI2[3].ToString("F4");
                    _2p_d.Text = AI2[4].ToString("F4");
                    one0.Text = AI_Good[0, 0].ToString();
                    one1.Text = AI_Good[0, 1].ToString("F4");
                    one2.Text = AI_Good[0, 2].ToString("F4");
                    one3.Text = AI_Good[0, 3].ToString("F4");
                    one4.Text = AI_Good[0, 4].ToString("F4");
                    two0.Text = AI_Good[1, 0].ToString();
                    two1.Text = AI_Good[1, 1].ToString("F4");
                    two2.Text = AI_Good[1, 2].ToString("F4");
                    two3.Text = AI_Good[1, 3].ToString("F4");
                    two4.Text = AI_Good[1, 4].ToString("F4");
                    three0.Text = AI_Good[2, 0].ToString();
                    three1.Text = AI_Good[2, 1].ToString("F4");
                    three2.Text = AI_Good[2, 2].ToString("F4");
                    three3.Text = AI_Good[2, 3].ToString("F4");
                    three4.Text = AI_Good[2, 4].ToString("F4");
                    four0.Text = AI_Good[3, 0].ToString();
                    four1.Text = AI_Good[3, 1].ToString("F4");
                    four2.Text = AI_Good[3, 2].ToString("F4");
                    four3.Text = AI_Good[3, 3].ToString("F4");
                    four4.Text = AI_Good[3, 4].ToString("F4");
                    five0.Text = AI_Good[4, 0].ToString();
                    five1.Text = AI_Good[4, 1].ToString("F4");
                    five2.Text = AI_Good[4, 2].ToString("F4");
                    five3.Text = AI_Good[4, 3].ToString("F4");
                    five4.Text = AI_Good[4, 4].ToString("F4");
                }
            }

            Score.Text = Tetris.Score.ToString();
            Score2.Text = Tetris2.Score.ToString();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            Tetris.MoveDown();
            if (Mode != 1)
                Tetris2.MoveDown();

            count++;
        }

        private void AiTimer_Tick(object sender, EventArgs e)
        {
            if (Mode == 4) Tetris2.AIVirtualMove(AI[1], AI[2], AI[3], AI[4]);
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
            Application.Current.MainWindow.Width = 400;
            Application.Current.MainWindow.Height = 500;
            timer.Stop();
            gamespeedtimer.Stop();
            Keyboard.ClearFocus();
        }

        private void Sound_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.SoundMode)
            {
                MainWindow.SoundMode = false;
                SoundSource.Source = new BitmapImage(new Uri(@"\Source\mute_button.png", UriKind.Relative));
            }
            else
            {
                MainWindow.SoundMode = true;
                SoundSource.Source = new BitmapImage(new Uri(@"\Source\sound_button.png", UriKind.Relative));
            }

            Keyboard.ClearFocus();
        }

        private void Eye_Click(object sender, RoutedEventArgs e)
        {
            if (EyeMode)
            {
                EyeMode = false;
                EyeImage.Source = new BitmapImage(new Uri(@"\Source\CloseEye.png", UriKind.Relative));
                count = 20;
                GameSpeed = 1;
                timer.Interval = TimeSpan.FromMilliseconds(GameSpeed / 500);
            }
            else
            {
                EyeMode = true;
                EyeImage.Source = new BitmapImage(new Uri(@"\Source\OpenEye.png", UriKind.Relative));
                count = 20;
                GameSpeed = 500;
                timer.Interval = TimeSpan.FromMilliseconds(GameSpeed / 30);
            }
        }

        private void Guide_Click(object sender, RoutedEventArgs e)
        {
            if (Guide)
            {
                count = 20;
                Guide = false;
            }
            else
            {
                Guide = true;
            }
        }
    }
}