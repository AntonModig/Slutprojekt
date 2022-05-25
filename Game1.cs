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
        HealAbility Healing;
        Texture2D HealingIcon;
        Camera Camera;
        Vector2 CameraPosition = new Vector2 (1366 / 2, 768 / 2); //Konstanter screen height / width
        Texture2D spikes;
        public bool GameOver;
        Texture2D GameOverTXT;
        GameOverText GameOverText;
        BigBoyCastle Castle;
        Texture2D CastleTXT;


        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.IsFullScreen = false;
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

           //Loading pixel
            pixel = Content.Load<Texture2D>("pixel");
           
            //Loading in titlescreen
            TitleScreenBG = Content.Load<Texture2D>("TitleScreen");

            //Making the titlescreen
            TitleScreen = new TitleScreen(TitleScreenBG);

            //Loading in the buttons on the title screen & pause screen
            StartTexture = Content.Load<Texture2D>("Start Button");
            QuitTexture = Content.Load<Texture2D>("Quit Button");
            ResumeTexture = Content.Load<Texture2D>("Resume");
            //Not a button but text
            GameOverTXT = Content.Load<Texture2D>("Game Over");

            //Loading it the font
            font = Content.Load<SpriteFont>("Font");

            //Loading background in game
            GameBGtxt = Content.Load<Texture2D>("GameBackground");
            
            //Loading in the blink icon & healing
            BlinkIcon = Content.Load<Texture2D>("Dash Icon");
            HealingIcon = Content.Load<Texture2D>("Healing Icon");

            //Loading in spikes
            spikes = Content.Load<Texture2D>("Spikes");

            //Loading in background castle
            CastleTXT = Content.Load<Texture2D>("BigBoyCastle");

            //Making the Buttons
            StartButton = new StartButton(StartTexture, new Vector2(300, 588));
            QuitButton = new QuitButton(QuitTexture, new Vector2(866, 588));
            ResumeButton = new ResumeButton(ResumeTexture, new Vector2(300, 588));

            //Making Camera that follows player
            Camera = new Camera(GraphicsDevice.Viewport);
            Camera.Position = CameraPosition;
            
            //Making the background in game
            GameBG = new GameBackground(GameBGtxt);

            // Making game over text
            GameOverText = new GameOverText(GameOverTXT);

            //Making background castle
            Castle = new BigBoyCastle(CastleTXT);
           
            //Making the ground
            MAP1 = new Map1 (pixel, spikes, this);
            
            //Making the Player
            Player1 = new Player (pixel, new Vector2(100, 580), MAP1);



            //Making the Blink Icon && healing
            Blinkicon = new BlinkIcon(BlinkIcon);
            Healing = new HealAbility(HealingIcon);

            //Making hp-bar
            healthbar = new Healthbar (pixel, Player1);


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape) && hasstarted == true && isPaused == false && GameOver == false)
            {
                isPaused = true;
            }    

            if (this.hasstarted == false || GameOver == true)
            {
                StartButton.Update(this);
                QuitButton.Update(this);
            }
            if (hasstarted == true && isPaused == false && GameOver == false)
            {
                Player1.Update(this, gameTime);
                MAP1.Update();
                healthbar.Update(Player1);
                Blinkicon.Update(this);
                Healing.Update(Player1, gameTime, this);

            }
            if (isPaused == true)
            {
                QuitButton.Update(this);
                ResumeButton.Update(this);
            }

            // TODO: Add your update logic here
            Camera.UpdateCamera(GraphicsDevice.Viewport, Player1);

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

            if (hasstarted == true && isPaused == false && GameOver == false)
            {
                GameBG.Draw(_spriteBatch);
            }

            if (isPaused == true)
            {
                GameBG.Draw(_spriteBatch);
                QuitButton.Draw(_spriteBatch);
                ResumeButton.Draw(_spriteBatch);
            }
            
            if (GameOver == true)
            {
                GameBG.Draw(_spriteBatch);
                GameOverText.Draw(_spriteBatch);
                StartButton.Draw(_spriteBatch);
                QuitButton.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            _spriteBatch.Begin(SpriteSortMode.Texture,null,null,null,null,null,Camera.Transform);

            if (hasstarted == true && isPaused == false && GameOver == false)
            {
                Castle.Draw(_spriteBatch);
                MAP1.Draw(_spriteBatch);
                Player1.Draw(_spriteBatch);
            }
    
            _spriteBatch.End();

            _spriteBatch.Begin();

            if (hasstarted == true && isPaused == false && GameOver == false)
            {
                Blinkicon.Draw(_spriteBatch);
                Healing.Draw(_spriteBatch);
                healthbar.Draw(_spriteBatch);
            }

            _spriteBatch.End();



            

            // TODO: Add your drawing code here
            

            base.Draw(gameTime);
        }
    }
}