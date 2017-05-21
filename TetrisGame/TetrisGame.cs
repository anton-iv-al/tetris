using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TetrisGame
{
    public class TetrisGame : Game
    {
        private SpriteBatch spriteBatch;
        private DrawManager drawManager;

        private int _gameLevel = 1;
        private int _gameSpeed = 1;
        private bool _isSpeedUp = false;
        private int _linesCount = 0;

        private int _figureCounter = 0;
        private int _nextLevelFigureCount = 10;

        private bool _isRun = true;

        private Field _field;
        private FigureTypes _figureTypes;
        private Figure _currentFigure;

        private int _currentTime1 = 0;
        private int UpdatePeriodTime1 { get { return 1000 / _gameSpeed; } }
        private int _currentTime2 = 0;
        private int UpdatePeriodTime2 { get; } = 50;
        private int _currentTime3 = 0;
        private int UpdatePeriodTime3 { get; } = 100;

        public TetrisGame()
        {
            try
            {
                drawManager = new DrawManager(new GraphicsDeviceManager(this), Window);
                _field = new Field(drawManager);
                Figure.DrawManager = drawManager;
                _figureTypes = new FigureTypes();
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
                drawManager.Initialize();
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
                spriteBatch = new SpriteBatch(GraphicsDevice);
                drawManager.LoadContent(spriteBatch, Content);
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
            if (!_isRun) return;
            spriteBatch.Begin();

            try
            {
                drawManager.DrawBackGround();
                drawManager.DrawText(_gameLevel, _linesCount);
                _field.Draw();
                _currentFigure.Draw();
            }
            catch (System.Exception e)
            {
                _isRun = false;
                ShowExceptionMessage(e);
            }

            spriteBatch.End();
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
            _currentFigure = new Figure(_figureTypes.GetRandomMatrix());
            _figureCounter++;
            if(_figureCounter >= _nextLevelFigureCount)
            {
                _figureCounter = 0;
                _gameLevel += 1;
                if (!_isSpeedUp) _gameSpeed += 1;
            }
        }

        private void GameOver()
        {
            _isRun = false;
        }

        private void SpeedUp(bool isEnabled)
        {
            _isSpeedUp = isEnabled;
            if (isEnabled) _gameSpeed = _gameLevel + 30;
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
                if (_field.AddFigure(_currentFigure)) CreateNewFigure();
                else GameOver();
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
    }
}
