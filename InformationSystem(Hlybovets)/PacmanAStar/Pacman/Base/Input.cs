using System;

namespace Pacman.Base
{
    public class Input
    {
        public static InputStatus GetInput()
        {
            var status = new InputStatus();
            var key = Console.ReadKey();
            status.Direction = key.Key switch
            {
                //if (Keyboard.IsKeyDown(Key.Up))
                ConsoleKey.UpArrow => Direction.Up,
                //if (Keyboard.IsKeyDown(Key.Down))
                ConsoleKey.DownArrow => Direction.Down,
                //if (Keyboard.IsKeyDown(Key.Left))
                ConsoleKey.LeftArrow => Direction.Left,
                //if (Keyboard.IsKeyDown(Key.Right))
                ConsoleKey.RightArrow => Direction.Right,
                _ => status.Direction
            };
            return status;
        }
        //todo:add A* algo
    }

    public class InputStatus
    {
        public Direction Direction { get; set; }
    }

    public enum Direction
    {
        None,
        Up,
        Down,
        Left,
        Right
    }
}
