using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame
{
    interface IDrawAdapter
    {
        int MatrixSizeX { get; set; }
        int MatrixSizeY { get; set; }

        void Initialize();

        void LoadContent();

        void DrawBackGround();

        void DrawText(int gameLevel, int linesCount);

        void DrawGameOver();

        void DrawSquare(int x, int y, SquareColor color);    // координаты в матрице

        void DrawSquareInPreview(int x, int y, SquareColor color); // координаты в матрице
    }
}
