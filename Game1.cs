using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Mono4_Time_n_Sound
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D bombTexture, explosionTexture;

        Vector2 explosionVector;

        Rectangle bombRect, explosionRect;

        SpriteFont timeFont;

        float seconds, startTime;

        MouseState mouseState;

        SoundEffect explode;
        SoundEffectInstance explodeInstance;

        bool boom = false;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 800; // Sets the width of the window
            _graphics.PreferredBackBufferHeight = 600; // Sets the height of the window
            _graphics.ApplyChanges(); // Applies the new dimensions
            this.Window.Title = "Part 4 - Time and Sound";

            bombRect = new Rectangle(50, 50, 700, 400);
            explosionRect = new Rectangle(50, 50, 700, 400);
            explosionVector = new Vector2(1, 1);

            base.Initialize();

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            bombTexture = Content.Load<Texture2D>("bomb");
            explode = Content.Load<SoundEffect>("explosion");
            explosionTexture = Content.Load<Texture2D>("boom");
            explodeInstance = explode.CreateInstance();
            timeFont = Content.Load<SpriteFont>("time");


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            mouseState = Mouse.GetState();

            seconds = (float)gameTime.TotalGameTime.TotalSeconds - startTime;
            if (mouseState.LeftButton == ButtonState.Pressed)
                startTime = (float)gameTime.TotalGameTime.TotalSeconds;

            if (seconds >= 15)
            {
                explodeInstance.Play();
                startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                boom = true;

            }

            if (boom == true)
            {
                explosionRect.X -= (int)explosionVector.X;
                explosionRect.Y -= (int)explosionVector.Y;
                explosionRect.Width += 2;
                explosionRect.Height += 2;
                if (explodeInstance.State == SoundState.Stopped)
                    System.Environment.Exit(0);
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _spriteBatch.Draw(bombTexture, bombRect, Color.White);
            _spriteBatch.DrawString(timeFont, (15 - seconds).ToString("00.0"), new Vector2(270, 180), Color.Black);

            if (boom == true)
                _spriteBatch.Draw(explosionTexture, explosionRect, Color.White);

            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}