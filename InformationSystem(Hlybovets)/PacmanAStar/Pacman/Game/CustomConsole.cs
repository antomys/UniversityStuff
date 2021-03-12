using System;

namespace Pacman.Game
{
    internal static class CustomConsole
    {
        public static bool CursorVisible {
            set => Console.CursorVisible = value; }
        public static ConsoleColor ForegroundColor { get; set; }
        public static ConsoleColor BackgroundColor { get; set; }
        //public static int WindowHeight { get => Console.WindowHeight; set => Console.WindowHeight = value; }
        //public static int WindowWidth { get => Console.WindowWidth; set => Console.WindowWidth = value; }

        private static ConsolePixel[,] _grid;
        private static int _cursorLeft = 0;
        private static int _cursorTop = 0;

        internal static void Initialize(int xSize, int ySize)
        {
            //Console.SetWindowSize(xSize, ySize);
            //Console.SetBufferSize(xSize, ySize);
            _grid = new ConsolePixel[xSize, ySize];
        }

        internal static void Clear()
        {
            Console.ResetColor();
            Console.SetCursorPosition(0, 0);
            for (var y = 0; y < Console.WindowHeight; y++)
            {
                Console.Write(new string(' ', Console.WindowWidth));
            }
            Console.SetCursorPosition(0, 0);

            Console.Clear();
            Initialize(Console.WindowWidth, Console.WindowHeight);
            _cursorLeft = 0;
            _cursorTop = 0;
        }

        internal static void SetCursorPosition(int left, int top)
        {
            _cursorLeft = left;
            _cursorTop = top;
        }

        internal static void Write(char character)
        {
            var current = _grid[_cursorLeft, _cursorTop];
            if (current == null || current.Character != character)
            {
                _grid[_cursorLeft, _cursorTop] = new ConsolePixel()
                {
                    Character = character,
                    BackgroundColor = BackgroundColor,
                    ForegroundColor = ForegroundColor
                };
                Console.SetCursorPosition(_cursorLeft, _cursorTop);
                Console.BackgroundColor = BackgroundColor;
                Console.ForegroundColor = ForegroundColor;
                Console.Write(character);
            }
            _cursorLeft++;
        }

        internal static void WriteLine()
        {
            _cursorLeft = 0;
            _cursorTop++;
        }

        internal static void Write(string v)
        {
            foreach (var t in v)
            {
                Write(t);
            }
        }

        internal static void ResetColor()
        {
            Console.ResetColor();
        }
    }

    public class ConsolePixel
    {
        public ConsoleColor ForegroundColor { get; set; }
        public ConsoleColor BackgroundColor { get; set; }
        public char Character { get; set; }
    }
}
