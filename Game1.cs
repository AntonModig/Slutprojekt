using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Slutprojekt
{
    public class Game1 : Game
    {
        //Declaration of all variables
        Texture2D TitleScreenBG;
        TitleScreen TitleScreen;
        Texture2D StartTexture;
        Texture2D QuitTexture;
        Texture2D GameBGtxt;
        StartButton StartButton;
        QuitButton QuitButton;
        GameBackground GameBG;
        public Ground Ground;
        Player Player;
        bool hasstarted;


                

        //Temporary texture
        Texture2D pixel;
        
        //Starting the game
        public void StartGame()
        {
            this.hasstarted = true;
        }
        
        //method for quitting game
        public void Quit()
        {
            this.Exit();    
        }
        
        //Only detecting a single press
        public void hasbeenpressed(Keys key)
        {
            Board.GetState();
            Board.HasBeenPressed(key);
        }


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

           //Loading temporary texture
           pixel = Content.Load<Texture2D>("pixel");
           
            //Loading in titlescreen
            TitleScreenBG = Content.Load<Texture2D>("TitleScreen");

            //Making the titlescreen
            TitleScreen = new TitleScreen(TitleScreenBG);

            //Loading in the buttons on the title screen
            StartTexture = Content.Load<Texture2D>("Start Button");
            QuitTexture = Content.Load<Texture2D>("Quit Button");

            //Loading background in game
            GameBGtxt = Content.Load<Texture2D>("GameBackground");

            //Making the Buttons
            StartButton = new StartButton(StartTexture, new Vector2(300, 588));
            QuitButton = new QuitButton(QuitTexture, new Vector2(866, 588));

            //Making the background in game
            GameBG = new GameBackground(GameBGtxt);

            //Making the ground
            Ground = new Ground (pixel, new Vector2(0, 588));

            //Making the Player
            Player = new Player (pixel, new Vector2(100, 568));

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (this.hasstarted == false)
            {
                StartButton.Update(this);
                QuitButton.Update(this);
            }
            if (hasstarted == true)
            {
                Player.Update(this);
                if (Player.player.Intersects(Ground.ground))
                {
                    Player.Stop();
                }
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            if (hasstarted == false)
            {
                TitleScreen.Draw(_spriteBatch);
                StartButton.Draw(_spriteBatch);
                QuitButton.Draw(_spriteBatch);
            }
            if (hasstarted == true)
            {
                GameBG.Draw(_spriteBatch);
                Ground.Draw(_spriteBatch);
                Player.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            // TODO: Add your drawing code here
            

            base.Draw(gameTime);
        }
    }
}