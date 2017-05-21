using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame
{
    public class Figure
    {
        public static DrawManager DrawManager { get; set; }

        private SquareColor[,] _matrix;
        public SquareColor this[int x, int y] { get { return _matrix[x, y]; } }

        public int X { get; set; } = 3;
        public int Y { get; set; } = 0;

        public Figure(SquareColor[,] matrix)
        {
            _matrix = matrix;
        }

        public void Draw()
        {
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    DrawManager.DrawSquare(X+i, Y+j, _matrix[i, j]);
                }
            }
        }

        public void DrawInPrevievw()
        {
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    DrawManager.DrawSquareInPreview(i, j, _matrix[i, j]);
                }
            }
        }

        public void RotateRight()
        {
            var newMatrix = new SquareColor[4, 4];
            for(int i=0; i<4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    newMatrix[i, j] = _matrix[3 - j, i];
                }
            }
            _matrix = newMatrix;
        }

        public void RotateLeft()
        {
            var newMatrix = new SquareColor[4, 4];
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    newMatrix[i, j] = _matrix[j, 3 - i];
                }
            }
            _matrix = newMatrix;
        }
    }
}
