﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Slutprojekt
{
    public class Game1 : Game
    {
        Texture2D TitleScreenBG;
        Rectangle TitleScreen;


        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.IsFullScreen = true;
            _graphics.PreferredBackBufferHeight = 788;
            _graphics.PreferredBackBufferWidth = 1366;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Loading in titlescreen
            TitleScreenBG = Content.Load<Texture2D>("TitleScreen");
            TitleScreen = new Rectangle(0 ,0 , GraphicsDevice.DisplayMode.Width, GraphicsDevice.DisplayMode.Height);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(TitleScreenBG, TitleScreen, Color.White);

            _spriteBatch.End();

            // TODO: Add your drawing code here
            

            base.Draw(gameTime);
        }
    }
}