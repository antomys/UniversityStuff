using System;
using System.Collections.Generic;
using Pacman.Base;
using Pacman.Base.Point;

namespace Pacman.Game
{
    public class Renderer
    {
        private const int OriginalX = Constant.WindowXSize;
        private const int OriginalY = Constant.WindowYSize;
        private const int ScaleX = Constant.WindowXScale;
        private const int ScaleY = Constant.WindowYScale;

        public Renderer()
        {
            const int ySize = OriginalY * ScaleY + 1;
            const int xSize = OriginalX * ScaleX + 1;

            CustomConsole.Initialize(xSize, ySize);
            CustomConsole.CursorVisible = false;
            CustomConsole.Clear();
        }

        public static void Render(LinkedList<EntityBase>[,] grid)
        {
            CustomConsole.SetCursorPosition(0, 0);

            for (var y = 0; y < OriginalY; y++)
            {
                for (var x = 0; x < OriginalX; x++)
                {
                    var entities = grid[y, x];
                    var item = entities.First.Value; // Here this shit!
                    CustomConsole.ForegroundColor = item.Pixel.ForegroundColor;
                    CustomConsole.BackgroundColor = item.Pixel.BackgroundColor;
                    CustomConsole.Write(item.Character);

                    //Scale X
                    if (x >= OriginalX - 1) continue;
                    var nextItem = grid[y, x + 1].First?.Value;
                    if (!(item.SmoothRender && nextItem.SmoothRender && item.Character == nextItem.Character))
                    {
                        CustomConsole.ForegroundColor = ConsoleColor.Black;
                        CustomConsole.BackgroundColor = ConsoleColor.Black;
                    }
                    for (var s = 0; s < ScaleX - 1; s++)
                        CustomConsole.Write(' ');
                }

                //Finish line
                CustomConsole.WriteLine();

                //Scale Y
                if (y >= OriginalY - 1) continue;
                {
                    for (var s = 0; s < ScaleY - 1; s++)
                    {
                        for (var x = 0; x < OriginalX; x++)
                        {
                            var entities = grid[y, x];
                            var item = entities.First?.Value;
                            var nextItem = grid[y + 1, x].First?.Value;

                            //1 color pixel + (scaleX -1) black pixels
                            if (item.SmoothRender && nextItem.SmoothRender && item.Character == nextItem.Character)
                            {
                                CustomConsole.ForegroundColor = item.Pixel.ForegroundColor;
                                CustomConsole.BackgroundColor = item.Pixel.BackgroundColor;
                                CustomConsole.Write(' ');
                                CustomConsole.ForegroundColor = ConsoleColor.Black;
                                CustomConsole.BackgroundColor = ConsoleColor.Black;
                                CustomConsole.Write(new string(' ', ScaleX - 1));
                            }
                            else
                            {
                                //(scaleX) black pixels only
                                CustomConsole.ForegroundColor = ConsoleColor.Black;
                                CustomConsole.BackgroundColor = ConsoleColor.Black;
                                CustomConsole.Write(new string(' ', ScaleX));
                            }
                        }
                        CustomConsole.WriteLine();
                    }
                }

            }
        }

        public static void Clear()
        {
            CustomConsole.Clear();
        }

    }
}
