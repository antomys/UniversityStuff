using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TetrisAi
{
    public class Tetris
    {
        public static bool CanvasClear = false;
        private int CurrentBlock;
        private int CurrentDirection;
        private int CurrentX = 4;
        private int CurrentY;
        private int[,] Grid;
        private readonly int[,] GuideGrid;
        private readonly int Height;
        public int Level = 1;

        private readonly int[] MissBlock;
        private int NextBlock = -1;
        private readonly int[,] OldGrid;
        private int[,] OldGuideGrid;
        private readonly Random random = new((int) DateTime.Now.Ticks);
        public int Score;

        private readonly int Width;

        public Tetris(int Width, int Height)
        {
            this.Width = Width;
            this.Height = Height;
            MissBlock = new int[7];
            Grid = new int[Width, Height];
            OldGrid = new int[Width, Height];
            GuideGrid = new int[Width, Height];
            OldGuideGrid = new int[Width, Height];

            for (var i = 0; i < Width; i++)
            for (var j = 0; j < Height; j++)
                OldGrid[i, j] = 1;
        }

        public void NewBlock()
        {
            CurrentX = 3;
            CurrentY = 1;
            CurrentDirection = 3;

            CurrentBlock = NextBlock;
            if (NextBlock == -1) CurrentBlock = random.Next(0, 7);
            if (ForgetBlock() != -1)
            {
                var i = ForgetBlock();
                NextBlock = i;
                MissBlock[i] = 0;
            }
            else
            {
                NextBlock = random.Next(0, 7);
                for (var i = 0; i < 7; i++)
                    if (NextBlock != i)
                        MissBlock[i]++;
                MissBlock[NextBlock] = 0;
            }

            MergeCurrentBlockToBoard(Grid, CurrentX, CurrentY, CurrentDirection);
            DrawGuideGrid();
        }

        private int ForgetBlock()
        {
            for (var i = 0; i < 7; i++)
                if (MissBlock[i] >= 7)
                    return i;
            return -1;
        }

        private int[,] MergeCurrentBlockToBoard(int[,] Grid, int CurrentX, int CurrentY, int CurrentDirection)
        {
            var blockArray = GetBlockArray(CurrentBlock, CurrentDirection);

            var arrayLength = blockArray.Length;
            var size = 0;

            switch (arrayLength)
            {
                case 9:
                    size = 3;
                    break;
                case 16:
                    size = 4;
                    break;
            }

            for (var i = 0; i < size; i++)
            for (var j = 0; j < size; j++)
                if (blockArray[i, j] == 1)
                    Grid[CurrentX + i, CurrentY + j] = 10;
            return Grid;
        }

        private int[,] GetBlockArray(int block, int direction)
        {
            switch (block)
            {
                case 0: return new int[4, 4] {{0, 0, 0, 0}, {0, 1, 1, 0}, {0, 1, 1, 0}, {0, 0, 0, 0}};
                case 1:
                    switch (direction)
                    {
                        case 0:
                            return new int[4, 4] {{0, 0, 0, 0}, {1, 1, 1, 1}, {0, 0, 0, 0}, {0, 0, 0, 0}};
                        case 1:
                            return new int[4, 4] {{0, 0, 1, 0}, {0, 0, 1, 0}, {0, 0, 1, 0}, {0, 0, 1, 0}};
                        case 2:
                            return new int[4, 4] {{0, 0, 0, 0}, {0, 0, 0, 0}, {1, 1, 1, 1}, {0, 0, 0, 0}};
                        case 3:
                            return new int[4, 4] {{0, 1, 0, 0}, {0, 1, 0, 0}, {0, 1, 0, 0}, {0, 1, 0, 0}};
                    }

                    break;
                case 2:
                    switch (direction)
                    {
                        case 0:
                            return new int[3, 3] {{0, 1, 1}, {1, 1, 0}, {0, 0, 0}};
                        case 1:
                            return new int[3, 3] {{0, 1, 0}, {0, 1, 1}, {0, 0, 1}};
                        case 2:
                            return new int[3, 3] {{0, 0, 0}, {0, 1, 1}, {1, 1, 0}};
                        case 3:
                            return new int[3, 3] {{1, 0, 0}, {1, 1, 0}, {0, 1, 0}};
                    }

                    break;
                case 3:
                    switch (direction)
                    {
                        case 0:
                            return new int[3, 3] {{1, 1, 0}, {0, 1, 1}, {0, 0, 0}};
                        case 1:
                            return new int[3, 3] {{0, 0, 1}, {0, 1, 1}, {0, 1, 0}};
                        case 2:
                            return new int[3, 3] {{0, 0, 0}, {1, 1, 0}, {0, 1, 1}};
                        case 3:
                            return new int[3, 3] {{0, 1, 0}, {1, 1, 0}, {1, 0, 0}};
                    }

                    break;
                case 4:
                    switch (direction)
                    {
                        case 0:
                            return new int[3, 3] {{0, 0, 1}, {1, 1, 1}, {0, 0, 0}};
                        case 1:
                            return new int[3, 3] {{0, 1, 0}, {0, 1, 0}, {0, 1, 1}};
                        case 2:
                            return new int[3, 3] {{0, 0, 0}, {1, 1, 1}, {1, 0, 0}};
                        case 3:
                            return new int[3, 3] {{1, 1, 0}, {0, 1, 0}, {0, 1, 0}};
                    }

                    break;
                case 5:
                    switch (direction)
                    {
                        case 0:
                            return new int[3, 3] {{1, 0, 0}, {1, 1, 1}, {0, 0, 0}};
                        case 1:
                            return new int[3, 3] {{0, 1, 1}, {0, 1, 0}, {0, 1, 0}};
                        case 2:
                            return new int[3, 3] {{0, 0, 0}, {1, 1, 1}, {0, 0, 1}};
                        case 3:
                            return new int[3, 3] {{0, 1, 0}, {0, 1, 0}, {1, 1, 0}};
                    }

                    break;
                case 6:
                    switch (direction)
                    {
                        case 0:
                            return new int[3, 3] {{0, 1, 0}, {1, 1, 1}, {0, 0, 0}};
                        case 1:
                            return new int[3, 3] {{0, 1, 0}, {0, 1, 1}, {0, 1, 0}};
                        case 2:
                            return new int[3, 3] {{0, 0, 0}, {1, 1, 1}, {0, 1, 0}};
                        case 3:
                            return new int[3, 3] {{0, 1, 0}, {1, 1, 0}, {0, 1, 0}};
                    }

                    break;
            }

            return null;
        }

        private void FixBlock()
        {
            for (var i = 0; i < Width; i++)
            for (var j = 0; j < Height; j++)
                if (Grid[i, j] == 10)
                    switch (CurrentBlock)
                    {
                        case 0:
                            Grid[i, j] = 1;
                            break;
                        case 1:
                            Grid[i, j] = 2;
                            break;
                        case 2:
                            Grid[i, j] = 3;
                            break;
                        case 3:
                            Grid[i, j] = 4;
                            break;
                        case 4:
                            Grid[i, j] = 5;
                            break;
                        case 5:
                            Grid[i, j] = 6;
                            break;
                        case 6:
                            Grid[i, j] = 7;
                            break;
                    }

            DestroyBlock();
        }

        private void DestroyBlock()
        {
            for (var j = Height - 1; j >= 0; j--)
            for (var i = 0; i < Width; i++)
            {
                if (Grid[i, j] == 0) break;
                if (i == Width - 1)
                {
                    Score++;
                    for (var y = j; y > 0; y--)
                    for (i = 0; i < Width; i++)
                        Grid[i, y] = Grid[i, y - 1];
                    j = Height;
                    break;
                }
            }
        }

        public void Rotation()
        {
            var nextDirection = CurrentDirection;

            switch (CurrentDirection)
            {
                case 0:
                    nextDirection = 3;
                    break;
                case 1:
                    nextDirection = 0;
                    break;
                case 2:
                    nextDirection = 1;
                    break;
                case 3:
                    nextDirection = 2;
                    break;
            }

            RemoveCurrentBlock(Grid);

            if (CanAction(Grid, nextDirection, CurrentX, CurrentY))
            {
                CurrentDirection = nextDirection;
            }
            else
            {
                if (CanAction(Grid, nextDirection, CurrentX - 1, CurrentY))
                {
                    CurrentX--;
                    CurrentDirection = nextDirection;
                }
                else if (CanAction(Grid, nextDirection, CurrentX + 1, CurrentY))
                {
                    CurrentX++;
                    CurrentDirection = nextDirection;
                }
                else if (CanAction(Grid, nextDirection, CurrentX, CurrentY - 1))
                {
                    CurrentY--;
                    CurrentDirection = nextDirection;
                }
                else if (CurrentBlock == 1 && nextDirection == 1)
                {
                    CurrentX += 2;
                    CurrentDirection = nextDirection;
                }
            }

            MergeCurrentBlockToBoard(Grid, CurrentX, CurrentY, CurrentDirection);
            DrawGuideGrid();
        }

        public void MoveLeft()
        {
            RemoveCurrentBlock(Grid);

            if (CanAction(Grid, CurrentDirection, CurrentX - 1, CurrentY))
                CurrentX--;
            MergeCurrentBlockToBoard(Grid, CurrentX, CurrentY, CurrentDirection);
            DrawGuideGrid();
        }

        public void MoveRight()
        {
            RemoveCurrentBlock(Grid);

            if (CanAction(Grid, CurrentDirection, CurrentX + 1, CurrentY))
                CurrentX++;
            MergeCurrentBlockToBoard(Grid, CurrentX, CurrentY, CurrentDirection);
            DrawGuideGrid();
        }

        public void MoveDown()
        {
            RemoveCurrentBlock(Grid);

            if (CanAction(Grid, CurrentDirection, CurrentX, CurrentY + 1))
            {
                CurrentY++;
                MergeCurrentBlockToBoard(Grid, CurrentX, CurrentY, CurrentDirection);
            }
            else
            {
                MergeCurrentBlockToBoard(Grid, CurrentX, CurrentY, CurrentDirection);
                FixBlock();
                NewBlock();
            }
        }

        public void MoveGround()
        {
            RemoveCurrentBlock(Grid);
            var i = CurrentY;
            while (true)
                if (CanAction(Grid, CurrentDirection, CurrentX, i + 1))
                {
                    i++;
                }
                else
                {
                    CurrentY = i;
                    MergeCurrentBlockToBoard(Grid, CurrentX, CurrentY, CurrentDirection);
                    FixBlock();
                    NewBlock();
                    break;
                }
        }

        private bool CanAction(int[,] Grid, int nextDirection, int nextX, int nextY)
        {
            var blockArray = GetBlockArray(CurrentBlock, nextDirection);
            var arrayLength = blockArray.Length;
            var size = 0;

            switch (arrayLength)
            {
                case 9:
                    size = 3;
                    break;
                case 16:
                    size = 4;
                    break;
            }

            for (var i = 0; i < size; i++)
            for (var j = 0; j < size; j++)
                if (blockArray[i, j] == 1)
                {
                    if (nextY + j >= Height) return false;

                    if (nextX + i < 0) return false;

                    if (nextX + i >= Width) return false;

                    if (Grid[nextX + i, nextY + j] != 0) return false;
                }

            return true;
        }

        private void RemoveCurrentBlock(int[,] Grid)
        {
            for (var i = 0; i < Width; i++)
            for (var j = 0; j < Height; j++)
                if (Grid[i, j] == 10)
                    Grid[i, j] = 0;
        }

        public bool IsGameOver()
        {
            var y = 1;
            for (var x = 0; x < Width; x++)
                if (Grid[x, y] != 0 && Grid[x, y] != 10)
                    return true;
            return false;
        }

        public void DrawBoard(GamePage game_page, bool player2, bool GuideLine)
        {
            var unitSize = 20;
            var marginLeft = 10;
            var marginTop = 0;
            if (player2)
                marginLeft += (Width * 2 + 1) * unitSize;

            if (Grid[4, 2] != OldGrid[4, 2] || CanvasClear) DrawNext(game_page, marginLeft, marginTop, player2);

            for (var i = 0; i < Width; i++)
            for (var j = 2; j < Height; j++)
            {
                var x1 = marginLeft + i * unitSize;
                var y1 = marginTop + j * unitSize - unitSize;
                if (CanvasClear) PaintCell(game_page, unitSize, x1, y1, i, j, Grid);
                if (Grid[i, j] != OldGrid[i, j]) PaintCell(game_page, unitSize, x1, y1, i, j, Grid);
                if (OldGuideGrid[i, j] != 0 && GuideLine) PaintCell(game_page, unitSize, x1, y1, i, j, Grid);
                if (GuideGrid[i, j] != 0 && GuideLine) PaintCell(game_page, unitSize, x1, y1, i, j, GuideGrid);
                OldGrid[i, j] = Grid[i, j];
            }

            OldGuideGrid = new int[Width, Height];
        }

        private void DrawGuideGrid()
        {
            OldGuideGrid = (int[,]) GuideGrid.Clone();
            RemoveCurrentBlock(Grid);
            RemoveCurrentBlock(GuideGrid);
            var i = CurrentY;
            while (true)
                if (CanAction(Grid, CurrentDirection, CurrentX, i + 1))
                {
                    i++;
                }
                else
                {
                    MergeCurrentBlockToBoard(Grid, CurrentX, CurrentY, CurrentDirection);
                    MergeCurrentBlockToBoard(GuideGrid, CurrentX, i, CurrentDirection);
                    break;
                }
        }

        private void DrawNext(GamePage game_page, int marginLeft, int marginTop, bool player2)
        {
            var unitSize = 20;
            var size = 4;
            var blockArray = GetBlockArray(NextBlock, 3);

            switch (NextBlock)
            {
                case 0:
                    blockArray = new int[4, 4] {{0, 0, 0, 0}, {0, 1, 1, 0}, {0, 1, 1, 0}, {0, 0, 0, 0}};
                    break;
                case 1:
                    blockArray = new int[4, 4] {{0, 2, 0, 0}, {0, 2, 0, 0}, {0, 2, 0, 0}, {0, 2, 0, 0}};
                    break;
                case 2:
                    blockArray = new int[4, 4] {{0, 3, 0, 0}, {0, 3, 3, 0}, {0, 0, 3, 0}, {0, 0, 0, 0}};
                    break;
                case 3:
                    blockArray = new int[4, 4] {{0, 0, 4, 0}, {0, 4, 4, 0}, {0, 4, 0, 0}, {0, 0, 0, 0}};
                    break;
                case 4:
                    blockArray = new int[4, 4] {{0, 5, 5, 0}, {0, 0, 5, 0}, {0, 0, 5, 0}, {0, 0, 0, 0}};
                    break;
                case 5:
                    blockArray = new int[4, 4] {{0, 0, 6, 0}, {0, 0, 6, 0}, {0, 6, 6, 0}, {0, 0, 0, 0}};
                    break;
                case 6:
                    blockArray = new int[4, 4] {{0, 0, 7, 0}, {0, 7, 7, 0}, {0, 0, 7, 0}, {0, 0, 0, 0}};
                    break;
            }

            if (player2) marginLeft = marginLeft - (Width + 6) * unitSize;
            for (var i = 0; i < size; i++)
            for (var j = 0; j < size; j++)
            {
                var x1 = marginLeft + Width * unitSize + i * unitSize + unitSize;
                var y1 = 140 + marginTop + j * unitSize - unitSize;
                PaintCell(game_page, unitSize, x1, y1, i, j, blockArray);
            }
        }

        private void PaintCell(GamePage game_page, int unitSize, int x1, int y1, int i, int j, int[,] Array)
        {
            var r = new Rectangle();
            r.Width = unitSize;
            r.Height = unitSize;
            r.StrokeThickness = 1;
            r.Stroke = Brushes.White;

            if (Array == GuideGrid)
            {
                r.StrokeThickness = 3;
                r.Stroke = Brushes.White;
                r.Margin = new Thickness(x1, y1, 0, 0);
                game_page.Canvas.Children.Add(r);
                return;
            }

            r.Fill = Brushes.Gray;

            switch (Array[i, j])
            {
                case 1:
                    r.Fill = Brushes.Yellow;
                    break;
                case 2:
                    r.Fill = Brushes.Cyan;
                    break;
                case 3:
                    r.Fill = Brushes.Red;
                    break;
                case 4:
                    r.Fill = Brushes.LimeGreen;
                    break;
                case 5:
                    r.Fill = Brushes.Blue;
                    break;
                case 6:
                    r.Fill = Brushes.Orange;
                    break;
                case 7:
                    r.Fill = Brushes.Purple;
                    break;
                case 10:
                    switch (CurrentBlock)
                    {
                        case 0:
                            r.Fill = Brushes.Yellow;
                            break;
                        case 1:
                            r.Fill = Brushes.Cyan;
                            break;
                        case 2:
                            r.Fill = Brushes.Red;
                            break;
                        case 3:
                            r.Fill = Brushes.LimeGreen;
                            break;
                        case 4:
                            r.Fill = Brushes.Blue;
                            break;
                        case 5:
                            r.Fill = Brushes.Orange;
                            break;
                        case 6:
                            r.Fill = Brushes.Purple;
                            break;
                    }

                    break;
                default:
                    r.Stroke = Brushes.Black;
                    r.StrokeThickness = 0.5;
                    r.Fill = Brushes.Black;
                    break;
            }

            r.Margin = new Thickness(x1, y1, 0, 0);
            game_page.Canvas.Children.Add(r);
        }

        public void AIVirtualMove(double AIa, double AIb, double AIc, double AId)
        {
            var GridClone = (int[,]) Grid.Clone();
            var GridClone2 = (int[,]) GridClone.Clone();
            double AIScore;
            var AIResultX = 0;
            var AIResultY = 0;
            var AIResultDirection = 0;
            double AIresultTemp = -1000;
            for (var directiontemp = 3; directiontemp >= 0; directiontemp--)
            {
                RemoveCurrentBlock(GridClone);
                var xtemp = 4;
                var ytemp = CurrentY;

                while (true)
                    if (CanAction(GridClone, directiontemp, xtemp - 1, ytemp))
                        xtemp--;
                    else
                        break;

                while (true)
                {
                    while (true)
                        if (CanAction(GridClone, directiontemp, xtemp, ytemp + 1))
                        {
                            ytemp++;
                        }
                        else
                        {
                            break;
                        }

                    GridClone2 = (int[,]) GridClone.Clone();
                    GridClone2 = (int[,]) MergeCurrentBlockToBoard(GridClone2, xtemp, ytemp, directiontemp).Clone();

                    AIScore = AIa * AIHeightValueSUM(GridClone2) + AIb * AILinesValue(GridClone2)
                                                                 + AIc * AIHolesValue(GridClone2) +
                                                                 AId * AIBumpinessValue(GridClone2);

                    if (AIScore >= AIresultTemp)
                    {
                        AIresultTemp = AIScore;
                        AIResultX = xtemp;
                        AIResultY = ytemp;
                        AIResultDirection = directiontemp;
                    }

                    ytemp = CurrentY;
                    if (CanAction(GridClone, directiontemp, xtemp + 1, ytemp))
                    {
                        xtemp++;
                        continue;
                    }

                    break;
                }
            }

            Grid = (int[,]) GridClone.Clone();
            RemoveCurrentBlock(Grid);
            CurrentX = AIResultX;
            CurrentY = AIResultY;
            CurrentDirection = AIResultDirection;
            MergeCurrentBlockToBoard(Grid, CurrentX, CurrentY, CurrentDirection);
            FixBlock();
            NewBlock();
        }

        private int AIHeightValueSUM(int[,] GridClone)
        {
            var ValueScore = 0;

            for (var x = 0; x < Width; x++)
            {
                var EachHeight = Height - 2;
                for (var y = 2; y < Height; y++)
                    if (GridClone[x, y] != 0)
                        break;
                    else
                        EachHeight--;
                ValueScore += EachHeight;
            }

            return ValueScore;
        }

        private int AILinesValue(int[,] GridClone)
        {
            var ValueScore = 0;
            for (var y = Height - 1; y >= 0; y--)
            for (var x = 0; x < Width; x++)
            {
                if (GridClone[x, y] == 0) break;
                if (x == Width - 1) ValueScore++;
            }

            return ValueScore;
        }

        private int AIHolesValue(int[,] GridClone)
        {
            var ValueScore = 0;
            var HoleCheck = false;
            for (var x = 0; x < Width; x++)
            {
                for (var y = 2; y < Height; y++)
                {
                    if (GridClone[x, y] != 0)
                        HoleCheck = true;
                    if (HoleCheck && GridClone[x, y] == 0)
                        ValueScore++;
                }

                HoleCheck = false;
            }

            return ValueScore;
        }

        private int AIBumpinessValue(int[,] GridClone)
        {
            var ValueScore = 0;
            var EachHeight_Current = 0;
            var EachHeight_Old = 0;
            for (var x = 0; x < Width; x++)
            {
                var EachHeight = Height - 2;
                for (var y = 2; y < Height; y++)
                    if (GridClone[x, y] != 0)
                        break;
                    else
                        EachHeight--;
                EachHeight_Old = EachHeight_Current;
                EachHeight_Current = EachHeight;

                if (x > 0)
                {
                    if (EachHeight_Current - EachHeight_Old > 0)
                        ValueScore += EachHeight_Current - EachHeight_Old;
                    else
                        ValueScore += EachHeight_Old - EachHeight_Current;
                }
            }

            return ValueScore;
        }
    }
}