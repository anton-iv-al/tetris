﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TetrisGame
{
    public class TetrisGame : Game
    {
        public static SpriteBatch SpriteBatch { get; private set; }
        public static Microsoft.Xna.Framework.Content.ContentManager StaticContent;
        public static GraphicsDeviceManager StaticGraphics;
        public static GameWindow StaticWindow;

        private IDrawAdapter DrawAdapter => DrawManager.Instance.Adapter;

        private int _gameLevel = 1;
        private int _gameSpeed = 1;
        private bool _isSpeedUp = false;
        private int _linesCount = 0;

        private int _figureCounter = 0;
        private int _nextLevelFigureCount = 10;

        private bool _isRun = true;
        private bool _isGameOver = false;

        private Field _field;
        private FigureTypes _figureTypes;
        private Figure _currentFigure;
        private Figure _nextFigure;

        private int _maxGameLevel = 10;
        private int _firstLevelTimeDelay = 1000;
        private int _lastLevelTimeDelay = 20;

        private int _currentTime1 = 0;
        private int UpdatePeriodTime1 => _firstLevelTimeDelay + (_lastLevelTimeDelay - _firstLevelTimeDelay) / (_maxGameLevel - 1) * (_gameSpeed - 1);
        private int _currentTime2 = 0;
        private int UpdatePeriodTime2 { get; } = 50;
        private int _currentTime3 = 0;
        private int UpdatePeriodTime3 { get; } = 100;

        public TetrisGame()
        {
            try
            {
                StaticGraphics = new GraphicsDeviceManager(this);
                StaticWindow = Window;
                _field = new Field();
                _figureTypes = new FigureTypes();

                _nextFigure = new Figure(_figureTypes.GetRandomMatrix());
                CreateNewFigure();

                Content.RootDirectory = "Content";
            }
            catch (System.Exception e)
            {
                _isRun = false;
                ShowExceptionMessage(e);
            }
        }

        protected override void Initialize()
        {
            try
            {
                DrawAdapter.Initialize();
            }
            catch (System.Exception e)
            {
                _isRun = false;
                ShowExceptionMessage(e);
            }
            base.Initialize();
        }

        protected override void LoadContent()
        {           
            try
            {
                SpriteBatch = new SpriteBatch(GraphicsDevice);
                StaticContent = Content;
                DrawAdapter.LoadContent();
            }
            catch (System.Exception e)
            {
                _isRun = false;
                ShowExceptionMessage(e);
            }
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();
            if (_isGameOver && Keyboard.GetState().IsKeyDown(Keys.Enter)) Restart();
            if (!_isRun) return;
            if (Keyboard.GetState().IsKeyDown(Keys.Space)) SpeedUp(true);
            if (Keyboard.GetState().IsKeyUp(Keys.Space)) SpeedUp(false);

            try
            {
                PeriodicUpdate3(gameTime);
                PeriodicUpdate2(gameTime);
                PeriodicUpdate1(gameTime);
            }
            catch (System.Exception e)
            {
                _isRun = false;
                ShowExceptionMessage(e);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();

            try
            {
                if (_isGameOver) DrawAdapter.DrawGameOver();
                if (!_isRun) return;
                DrawAdapter.DrawBackGround();
                DrawAdapter.DrawText(_gameLevel, _linesCount);
                _field.Draw();
                _currentFigure.Draw();
                _nextFigure.DrawInPrevievw();
            }
            catch (System.Exception e)
            {
                _isRun = false;
                ShowExceptionMessage(e);
            }
            finally
            {
                SpriteBatch.End();
            }
            base.Draw(gameTime);
        }

        private void ShowExceptionMessage(System.Exception e)
        {
            System.Windows.Forms.MessageBox.Show(
                    e.GetType().Name + "\n\n" +
                    e.Message + "\n\n" +
                    e.StackTrace
                , "Error");
        }

        private void CreateNewFigure()
        {
            _currentFigure = _nextFigure;
            _nextFigure = new Figure(_figureTypes.GetRandomMatrix());
            _figureCounter++;
            TryAddLevel();
        }

        private void TryAddLevel()
        {
            if (_figureCounter >= _nextLevelFigureCount && _gameLevel < _maxGameLevel)
            {
                _figureCounter = 0;
                _gameLevel += 1;
                if (!_isSpeedUp) _gameSpeed += 1;
            }
        }

        private void GameOver()
        {
            _isRun = false;
            _isGameOver = true;
        }

        private void SpeedUp(bool isEnabled)
        {
            _isSpeedUp = isEnabled;
            if (isEnabled) _gameSpeed = _maxGameLevel;
            else _gameSpeed = _gameLevel;
        }

        private void PeriodicUpdate1(GameTime gameTime)
        {
            _currentTime1 += gameTime.ElapsedGameTime.Milliseconds;
            if (_currentTime1 < UpdatePeriodTime1) return;
            else _currentTime1 = 0;

            _currentFigure.Y += 1;
            if (!_field.CheckFigureIntersection(_currentFigure))
            {
                _currentFigure.Y -= 1;
                _field.AddFigure(_currentFigure);
                _linesCount += _field.RemoveLines();
                if (_field.CheckGamoOver()) GameOver();
                else CreateNewFigure();
            }
        }

        private void PeriodicUpdate2(GameTime gameTime)
        {
            _currentTime2 += gameTime.ElapsedGameTime.Milliseconds;
            if (_currentTime2 < UpdatePeriodTime2) return;

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                _currentFigure.X -= 1;
                if (!_field.CheckFigureIntersection(_currentFigure)) _currentFigure.X += 1;
                _currentTime2 = 0;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                _currentFigure.X += 1;
                if (!_field.CheckFigureIntersection(_currentFigure)) _currentFigure.X -= 1;
                _currentTime2 = 0;
            }
        }

        private void PeriodicUpdate3(GameTime gameTime)
        {
            _currentTime3 += gameTime.ElapsedGameTime.Milliseconds;
            if (_currentTime3 < UpdatePeriodTime3) return;

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                _currentFigure.RotateRight();
                if (!_field.CheckFigureIntersection(_currentFigure)) _currentFigure.RotateLeft();
                _currentTime3 = 0;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                _currentFigure.RotateLeft();
                if (!_field.CheckFigureIntersection(_currentFigure)) _currentFigure.RotateRight();
                _currentTime3 = 0;
            }
        }

        private void Restart()
        {
            _isRun = true;
            _isGameOver = false;
            _field.Clear();
            CreateNewFigure();
            _gameLevel = 1;
            _gameSpeed = 1;
            _linesCount = 0;
    }
    }
}
