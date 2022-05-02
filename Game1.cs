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
        Texture2D ResumeTexture;
        Texture2D GameBGtxt;
        Texture2D BlinkIcon;
        StartButton StartButton;
        QuitButton QuitButton;
        GameBackground GameBG;
        public Player Player1;
        ResumeButton ResumeButton;
        BlinkIcon Blinkicon;
        public bool hasstarted;
        public SpriteFont font;
        public bool isPaused;
        Healthbar healthbar;
        Texture2D pixel;
        public Map1 MAP1;


        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.IsFullScreen = true;
            _graphics.PreferredBackBufferHeight = 768;
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

            //Loading in the buttons on the title screen & pause screen
            StartTexture = Content.Load<Texture2D>("Start Button");
            QuitTexture = Content.Load<Texture2D>("Quit Button");
            ResumeTexture = Content.Load<Texture2D>("Resume");

            //Loading it the font
            font = Content.Load<SpriteFont>("Font");

            //Loading background in game
            GameBGtxt = Content.Load<Texture2D>("GameBackground");
            
            //Loading in the blink icon
            BlinkIcon = Content.Load<Texture2D>("Dash Icon");
            

            //Making the Buttons
            StartButton = new StartButton(StartTexture, new Vector2(300, 588));
            QuitButton = new QuitButton(QuitTexture, new Vector2(866, 588));
            ResumeButton = new ResumeButton(ResumeTexture, new Vector2(300, 588));

            //Making the background in game
            GameBG = new GameBackground(GameBGtxt);

            //Making the ground
            MAP1 = new Map1 (pixel);

            //Making the Player
            Player1 = new Player (pixel, new Vector2(100, 580));

            //Setting up for the start of the game;
            DoInitialSetup(Player1);

            //Making the Blink Icon
            Blinkicon = new BlinkIcon(BlinkIcon, new Vector2(50, 698));

            //Making hp-bar
            healthbar = new Healthbar (pixel, Player1);


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    isPaused = true;
                }     

            if (this.hasstarted == false)
            {
                StartButton.Update(this);
                QuitButton.Update(this);
            }
            if (hasstarted == true && isPaused == false)
            {
                Player1.Update(this, gameTime);
                healthbar.Update(Player1);
                Blinkicon.Update(this);
            }
            if (isPaused == true)
            {
                QuitButton.Update(this);
                ResumeButton.Update(this);
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
            if (hasstarted == true && isPaused == false)
            {
                GameBG.Draw(_spriteBatch);
                MAP1.Draw(_spriteBatch);
                
                Blinkicon.Draw(_spriteBatch);
                Player1.Draw(_spriteBatch);
                healthbar.Draw(_spriteBatch);
            }
            if (isPaused == true)
            {
                GameBG.Draw(_spriteBatch);
                QuitButton.Draw(_spriteBatch);
                ResumeButton.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            // TODO: Add your drawing code here
            

            base.Draw(gameTime);
        }

        private void DoInitialSetup(Player player)
        {
            player.ChargeBlink();
        }
    }
}