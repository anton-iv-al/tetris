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

        private bool _isRun = true;

        private Field _field;
        private FigureTypes _figureTypes;
        private Figure _currentFigure;

        private int _currentTime = 0;
        private int UpdatePeriodTime { get { return 1000 / _gameSpeed; } }

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
            if (Keyboard.GetState().IsKeyDown(Keys.Down)) SpeedUp(true);
            if (Keyboard.GetState().IsKeyUp(Keys.Down)) SpeedUp(false);

            _currentTime += gameTime.ElapsedGameTime.Milliseconds;
            if (_currentTime < UpdatePeriodTime) return;
            else _currentTime = 0;

            try
            {
                if (_field.CheckFigureDescent(_currentFigure)) _currentFigure.Y += 1;
                else
                {
                    if (_field.AddFigure(_currentFigure)) CreateNewFigure();
                    else GameOver();
                }
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
        }

        private void GameOver()
        {
            _isRun = false;
        }

        private void SpeedUp(bool isEnabled)
        {
            _isSpeedUp = isEnabled;
            if (isEnabled) _gameSpeed = 30;
            else _gameSpeed = _gameLevel;
        }
    }
}
