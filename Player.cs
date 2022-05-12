using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Slutprojekt
{
    public class Player
    {
        Texture2D texture;
        public Vector2 position;
        public Rectangle player;
        bool falling;
        int UpSpeed;
        int JumpCharges;
        int Jumps;
        bool FacingRight;
        int speed;
        public bool BlinkCharged = true;
        public float timer = 10;
        const float TIMER = 10;
        float MaxHP = 100;
        float HP = 100;
        public float HPpercent;
        public bool HealingReady = true;
        bool GroundPounding;
        Map1 Map1;
        Vector2 PosInGrid;
        bool Moving;


         public Player (Texture2D texture, Vector2 position, Map1 Map1)
        {
            this.texture = texture;
            this.position = position;
            this.Map1 = Map1;
            player = new Rectangle((int)position.X, (int)position.Y, 20, 20);
        }

        public void Update(Game1 game, GameTime gameTime)
        {
            KeyboardState kstate = Keyboard.GetState();
            Board.GetState();
            JumpCharges = 2;
            HPpercent = HP / MaxHP;
            CheckGridPosition();

            
            if (kstate.IsKeyDown(Keys.O))
            {
                HP--;
            }
            if (HP <= 0)
            {
                game.hasstarted = false;
                Reset();
            }
            if (kstate.IsKeyDown(Keys.Left))
            {
                FaceLeft();
                Moving = true;
            }
            if (kstate.IsKeyDown(Keys.Right))
            {
                FaceRight();
                Moving = true;
            }
            if (kstate.IsKeyUp(Keys.Right) || kstate.IsKeyUp(Keys.Left))
            {
                Deccelerate();
            }
            if (GroundPounding == false && Moving == true)
            {
                Move();
            }
            if (falling == true)
            {
                Gravity();
            }
            if (Map1.map1[(int)PosInGrid.Y + 1, (int)PosInGrid.X] == 1 && player.Y >= PosInGrid.Y * Map1.TileSize + Map1.TileSize - player.Height)
            {
                player.Y = ((int)PosInGrid.Y) * Map1.TileSize - player.Height + Map1.TileSize;
                Stop();
            }
            if (Map1.map1[(int)PosInGrid.Y + 1, (int)PosInGrid.X] == 0)
            {
                falling = true;
            }
            if(Map1.map1[(int)PosInGrid.Y - 1, (int)PosInGrid.X] == 1)
            {
                player.Y = (int)PosInGrid.Y * Map1.TileSize;
            }
            if (Map1.map1[(int)PosInGrid.Y, (int)PosInGrid.X + 1] == 1 && player.X >= PosInGrid.X * Map1.TileSize + Map1.TileSize - player.Width)
            {
                player.X = (int)PosInGrid.X * Map1.TileSize - player.Width + Map1.TileSize;
            }
            if (Map1.map1[(int)PosInGrid.Y, (int)PosInGrid.X - 1] == 1 && player.X < PosInGrid.X * Map1.TileSize)
            {
                player.X = (int)PosInGrid.X * Map1.TileSize;
            }
            if (Jumps < JumpCharges && Board.HasBeenPressed(Keys.Up))
            {
                Jump();
            }
            if (BlinkCharged == true && Board.HasBeenPressed(Keys.LeftShift))
            {
                Blink();
                BlinkCharged = false;
            }
            if (BlinkCharged == false)
            {
                RechargeBlink(gameTime);
            }
            if (HP > MaxHP)
            {
                HP = MaxHP;
            }
            if (HealingReady == true && Board.HasBeenPressed(Keys.E))
            {
                Heal();
                HealingReady = false;
            }
            if (Board.HasBeenPressed(Keys.Down) && falling == true)
            {
                GroundPound();
            }
        }

        
        
       private void CheckGridPosition()
       {
           PosInGrid = new Vector2(player.X / Map1.TileSize, player.Y / Map1.TileSize);
       }
       
        private void GroundPound()
        {
            speed = 0;
            UpSpeed = -30;
            GroundPounding = true;
        }
        public void Heal()
        {
            for (int i = 0; i < 50; i++)
            {
                if (HP < MaxHP)
                {
                    HP++;
                }
            }
        }
        private void Stop ()
        {
            falling = false;
            UpSpeed = 0;
            Jumps = 0;
            GroundPounding = false;
        }

        private void Reset()
        {
            HP = 100;
            MaxHP = 100;
            ChargeBlink();
            RefillHeal();
        }
        private void Blink()
        {
            if (FacingRight == true && player.X < GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 19)
            {
                player.X += 150;
            }                    
            else if (FacingRight == false && player.X > 0)
            {
                player.X -= 150;
            }             
        }

        private void RechargeBlink(GameTime gameTime)
        {
            float Elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer -= Elapsed;
            if (timer < 0)
            {
                ChargeBlink();
                timer = TIMER;
            }
        }

        public void RefillHeal()
        {
            HealingReady = true;
        }
        public void ChargeBlink()
        {
            BlinkCharged = true;
        }
        private void Jump()
        {
            falling = true;
            UpSpeed = 15;
            Jumps++;
        }
        private void Gravity()
        {
            if (UpSpeed > -15)
                {
                    UpSpeed--;
                }
            player.Y -= UpSpeed;
        }
        private void Move()
        {
            player.X += speed;
        }
        private void FaceRight()
        {
            speed = 5;
            FacingRight = true;
        }
        private void FaceLeft()
        {
            speed = -5;
            FacingRight = false;
        }
        private void Deccelerate()
        {
            if (speed != 0 && falling == false)
            {
                if (speed > 0)
                {
                    speed --;
                }
                else
                {
                    speed++;
                }
                if (speed == 0)
                {
                    Moving = false;
                }
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, player, Color.White);
        }
    }
}