using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame
{
    public class Field
    {
        private static DrawManager DrawManager { get; set; }

        private const int _matrixSizeX = 10;
        private const int _matrixSizeY = 20;
        private SquareColor[,] _matrix = new SquareColor[_matrixSizeX, _matrixSizeY];

        public Field(DrawManager drawManager)
        {
            DrawManager = drawManager;
            DrawManager.MatrixSizeX = _matrixSizeX;
            DrawManager.MatrixSizeY = _matrixSizeY;
        }

        public void Draw()
        {
            for(int i=0; i < _matrixSizeX; ++i)
            {
                for (int j = 0; j < _matrixSizeY; ++j)
                {
                    DrawManager.DrawSquare(i, j, _matrix[i, j]);
                }
            }
        }

        public bool CheckFigureRotation(Figure figure, out int shiftX)
        {
            throw new NotImplementedException();
            shiftX = 0;
            return true;
        }

        public bool CheckFigureDescent(Figure figure)
        {
            figure.Y += 1;
            for(int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    if (figure.Matrix[i, j] <= 0) continue;
                    bool isLowerThanField = figure.Y + j >= _matrixSizeY;
                    bool isSquareIntersects = false;
                    if (!isLowerThanField && figure.Y + j >= 0) isSquareIntersects = _matrix[figure.X + i, figure.Y + j] > 0;
                    if (isLowerThanField || isSquareIntersects) 
                    {
                        figure.Y -= 1;
                        return false;
                    }
                }
            }
            figure.Y -= 1;
            return true;
        }

        public bool AddFigure(Figure figure)
        {
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    if (figure.Matrix[i, j] <= 0) continue;
                    if (figure.Y + j < 0) return false;
                    _matrix[figure.X + i, figure.Y + j] = figure.Matrix[i, j];
                }
            }
            return true;
        }
    }
}
