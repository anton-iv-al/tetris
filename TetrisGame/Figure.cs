using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisGame;

namespace TetrisGame
{
    public class Figure
    {
        private static IDrawAdapter DrawAdapter => DrawManager.Instance.Adapter;

        public SquareColor[,] Matrix;

        public int X { get; set; } = 3;
        public int Y { get; set; } = 0;

        public Figure(SquareColor[,] matrix)
        {
            Matrix = matrix;
        }

        public void Draw()
        {
            Matrix.ForEach((i, j, c) => DrawAdapter.DrawSquare(X + i, Y + j, c));
        }

        public void DrawInPrevievw()
        {
            Matrix.ForEach((i, j, c) => DrawAdapter.DrawSquareInPreview(i, j, c));
        }

        public void RotateRight()
        {
            Matrix = Matrix.Select((i, j, c) => Matrix[3 - j, i]);
        }

        public void RotateLeft()
        {
            Matrix = Matrix.Select((i, j, c) => Matrix[j, 3 - i]);
        }
}
}
