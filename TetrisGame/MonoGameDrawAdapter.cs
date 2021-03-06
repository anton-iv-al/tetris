﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using C3.MonoGame;

namespace TetrisGame
{
    public class MonoGameDrawAdapter : IDrawAdapter
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private GameWindow Window;

        private Texture2D _backgroundTexture;
        private Texture2D _squareRed;
        private Texture2D _squareBlue;
        private Texture2D _squareGreen;
        private Texture2D _squareYellow;
        private Texture2D _squareViolet;
        private Texture2D _squareOrange;
        private Texture2D _squareLightBlue;
        private SpriteFont _defaultFont;

        private Rectangle _gameArea;
        private Rectangle _gameAreaForDraw;
        private Rectangle _previewArea;
        private Point _squareSize;
        private int _frameThickness = 10;

        public int MatrixSizeX { get; set; }
        public int MatrixSizeY { get; set; }

        public MonoGameDrawAdapter()
        {
        }

        public void Initialize()
        {
            this.graphics = TetrisGame.StaticGraphics;
            this.Window = TetrisGame.StaticWindow;

            graphics.PreferredBackBufferWidth = 600;
            graphics.PreferredBackBufferHeight = 700;
            graphics.ApplyChanges();

            _gameArea = new Rectangle(50, 50, 300, 600);
            _gameAreaForDraw = new Rectangle(50 - 1, 50 - _frameThickness, 300 + _frameThickness, 600 + _frameThickness);
            _squareSize = new Point(30, 30);

            _previewArea = new Rectangle(400, 50, 150, 150);
        }

        public void LoadContent()
        {
            spriteBatch = TetrisGame.SpriteBatch;
            var content = TetrisGame.StaticContent;

            _backgroundTexture = content.Load<Texture2D>("strips");
            _squareRed = content.Load<Texture2D>("square_red");
            _squareBlue = content.Load<Texture2D>("square_blue");
            _squareGreen = content.Load<Texture2D>("square_green");
            _squareYellow = content.Load<Texture2D>("square_yellow");
            _squareOrange = content.Load<Texture2D>("square_orange");
            _squareViolet = content.Load<Texture2D>("square_violet");
            _squareLightBlue = content.Load<Texture2D>("square_light_blue");

            _defaultFont = content.Load<SpriteFont>("default");
        }

        public void DrawBackGround()
        {
            spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), Color.White);
            Primitives2D.FillRectangle(spriteBatch, _gameArea, Color.Black);
            Primitives2D.DrawRectangle(spriteBatch, _gameAreaForDraw, Color.Gray, _frameThickness);

            Primitives2D.FillRectangle(spriteBatch, _previewArea, Color.Black);
            Primitives2D.DrawRectangle(spriteBatch, _previewArea, Color.Gray, _frameThickness);
        }

        public void DrawText(int gameLevel, int linesCount)
        {
            spriteBatch.DrawString(_defaultFont, "level", new Vector2(400, 300), Color.Black);
            spriteBatch.DrawString(_defaultFont, gameLevel.ToString(), new Vector2(440, 360), Color.Black);

            spriteBatch.DrawString(_defaultFont, "lines", new Vector2(400, 500), Color.Black);
            spriteBatch.DrawString(_defaultFont, linesCount.ToString(), new Vector2(440, 560), Color.Black);
        }

        public void DrawGameOver()
        {
            var textArea = new Rectangle(40, 290, 325, 90);
            Primitives2D.FillRectangle(spriteBatch, textArea, Color.Black);
            Primitives2D.DrawRectangle(spriteBatch, textArea, Color.White, _frameThickness);
            spriteBatch.DrawString(_defaultFont, "GameOver", new Vector2(50, 300), Color.Orange);           
        }

        private Texture2D GetSquareTexture(SquareColor color)
        {
            Texture2D squareTexture;
            switch (color)
            {
                case SquareColor.Empty: return null;
                case SquareColor.Red: squareTexture = _squareRed; break;
                case SquareColor.Blue: squareTexture = _squareBlue; break;
                case SquareColor.Green: squareTexture = _squareGreen; break;
                case SquareColor.Yellow: squareTexture = _squareYellow; break;
                case SquareColor.Orange: squareTexture = _squareOrange; break;
                case SquareColor.Violet: squareTexture = _squareViolet; break;
                case SquareColor.LightBlue: squareTexture = _squareLightBlue; break;
                default: throw new Exception("Unknown SquareColor");
            }
            return squareTexture;
        }

        public void DrawSquare(int x, int y, SquareColor color)    // координаты в матрице
        {
            y -= 4;
            if (x < 0 || x >= MatrixSizeX || y < 0 || y >= MatrixSizeY) return;

            Texture2D squareTexture = GetSquareTexture(color);
            if (squareTexture == null) return;

            var location = new Point(_gameArea.X + x * _squareSize.X, 
                                     _gameArea.Y + y * _squareSize.Y);
            spriteBatch.Draw(squareTexture, new Rectangle(location, _squareSize), Color.White);
        }

        public void DrawSquareInPreview(int x, int y, SquareColor color)    // координаты в матрице
        {
            Texture2D squareTexture = GetSquareTexture(color);
            if (squareTexture == null) return;

            var location = new Point(_previewArea.X + _frameThickness + x * _squareSize.X,
                                     _previewArea.Y + _frameThickness*2 + y * _squareSize.Y);
            spriteBatch.Draw(squareTexture, new Rectangle(location, _squareSize), Color.White);
        }

    }
}
