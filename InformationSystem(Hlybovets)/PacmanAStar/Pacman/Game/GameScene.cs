using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Pacman.Base;
using Pacman.Base.Point;
using Pacman.Entity;
using Point = Pacman.Base.Point.Point;

namespace Pacman.Game
{
    public class GameScene
    {
        public readonly LinkedList<EntityBase>[,] Grid;
        public readonly int XSize;
        public readonly int YSize;
        //private Renderer _renderer;
        //private Input _input;
        public TransitionType Transition = TransitionType.None;

        private int _scoreInMap = 0;


        [Obsolete ("Changed to method without renderer")]
        public GameScene(int x, int y, Renderer renderer)
        {
            Grid = new LinkedList<EntityBase>[y, x];
            XSize = x;
            YSize = y;

            //_renderer = renderer;
            Renderer.Clear();
            //_input = new Input();
        }
        
        public GameScene(int x, int y)
        {
            Grid = new LinkedList<EntityBase>[y, x];
            XSize = x;
            YSize = y;

            //_renderer = renderer;
            Renderer.Clear();
            //_input = new Input();
        }

        public void Load(string filePath)
        {
            var text = File.ReadAllText(filePath);
            //Remove \r to fix OS compatibility
            text = text.Replace("\r", "");
            var list = text.Split('\n');
            
            
            for (var y = 0; y < YSize; y++)
            {
                var line = list[y];
                for (var x = 0; x < XSize; x++)
                {
                    var character = line[x];
                    var linkedList = new LinkedList<EntityBase>();
                    Grid[y, x] = linkedList;

                    linkedList.AddFirst(new Space());

                    var point = new Point(x, y);
                    switch (character)
                    {
                        case Constant.PlayerChar:
                        {
                            var p = new Player();
                            p.Start(this, point);
                            linkedList.AddFirst(p);
                            break;
                        }
                        case Constant.GhostChar:
                        {
                            var p = new Ghost();
                            p.Start(this, point);
                            linkedList.AddFirst(p);
                            break;
                        }
                        case Constant.WallChar:
                        {
                            var p = new Wall();
                            p.Start(this, point);
                            linkedList.AddFirst(p);
                            break;
                        }
                        case Constant.ScoreChar:
                        {
                            var p = new Score();
                            p.Start(this, point);
                            linkedList.AddFirst(p);
                            _scoreInMap++;
                            break;
                        }
                    }

                }
            }
        }

        public void Tick()
        {
            //We use a queue to first get all jobs to be executed and then execute every single job just once
            //If we execute immediately without a queue, the object position can change within that execution and it's possible that the same object could be executed multiple times during same tick
            var executionQueue = new Queue<EntityBase>();
            for (var y = 0; y < YSize; y++)
            {
                for (var x = 0; x < XSize; x++)
                {
                    var entities = Grid[y, x];
                    var item = entities.First;
                    while (item != null)
                    {
                        executionQueue.Enqueue(item.Value);
                        item = item.Next;
                    }
                }
            }

            while (executionQueue.Count > 0 && Transition == TransitionType.None)
            {
                var item = executionQueue.Dequeue();
                if (!item.IsDestroyed)
                    item.Update();
            }

            Renderer.Render(Grid);
        }

        public void DestroyEntity(EntityBase e)
        {
            for (var y = 0; y < YSize; y++)
            {
                for (var x = 0; x < XSize; x++)
                {
                    var entities = Grid[y, x];
                    var item = entities.First;
                    while (item != null)
                    {
                        if (item.Value == e)
                        {
                            entities.Remove(item);

                            if(item.Value is Score)
                            {
                                _scoreInMap--;
                                if (_scoreInMap <= 0)
                                    StartTransition(TransitionType.Finish);
                            }
                        }
                        item = item.Next;
                    }
                }
            }
        }

        public void StartTransition(TransitionType type)
        {
            Transition = type;
        }


    }
}
