using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Pacman.Base;
using Pacman.Base.Point;

namespace Pacman.Game
{
    public class GameController
    {
        private readonly Renderer _renderer = new();
        public void Run()
        {
            Console.Title = "PacMan Console";
            while (true)
            {
                Intro();
                Game(_renderer);
            }
        }

        private static void Intro()
        {
            IntroScene.Run();
        }

        private static void Game(Renderer renderer)
        {
            //var s = new GameScene(Constant.WindowXSize, Constant.WindowYSize, renderer);
            var s = new GameScene(Constant.WindowXSize, Constant.WindowYSize);
            s.Load("Resource/map.txt");

            var sw = new Stopwatch();
            while (s.Transition == TransitionType.None)
            {
                sw.Start();
                s.Tick();
                sw.Stop();
                var elapsed = sw.ElapsedMilliseconds;
                sw.Reset();
                var target = (elapsed > Constant.GameLoopDelay) ? 0 : Constant.GameLoopDelay - elapsed;
                Task.Delay(TimeSpan.FromMilliseconds(target)).Wait();
            }

            if (s.Transition == TransitionType.Finish)
                Finish();
            else
                Dead();
        }

        private static void Finish()
        {
            new FinishScene().Run();
        }

        private static void Dead()
        {
            DeadScene.Run();
        }
    }
}
