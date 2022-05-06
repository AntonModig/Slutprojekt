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
        Vector2 position;
        public Rectangle player;
        Rectangle oldposition;
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


         public Player (Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            player = new Rectangle((int)position.X, (int)position.Y, 20, 20);
        }

        public void Update(Game1 game, GameTime gameTime)
        {
            KeyboardState kstate = Keyboard.GetState();
            Board.GetState();
            JumpCharges = 2;
            HPpercent = HP / MaxHP;
            
            foreach (Rectangle GroundTile in game.MAP1.GroundTiles)
            {
                if(player.Intersects(GroundTile))
                {
                    Stop();
                    StopMovement();
                    player.Y = GroundTile.Y - player.Height;
                }
            }
            
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
            }
            if (kstate.IsKeyDown(Keys.Right))
            {
                FaceRight();
            }
            if (kstate.IsKeyUp(Keys.Right) || kstate.IsKeyUp(Keys.Left))
            {
                Deccelerate();
            }
            if (GroundPounding == false)
            {
                Move();
            }

            if (player.X < 0)
            {
                StopMovement();
            }
            if (player.X > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 19)
            {
                StopMovement();
            }
            if (Jumps < JumpCharges && Board.HasBeenPressed(Keys.Up))
            {
                Jump();
            }
            player.Y -= UpSpeed;
            if (falling == true)
            {
                Gravity();
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
            
            oldposition = player;
        }

        
        
        private void GroundPound()
        {
            speed = 0;
            UpSpeed = -30;
            GroundPounding = true;
        }
        public void Heal()
        {
            for (int i = 0; i < 30; i++)
            {
                if (HP < MaxHP)
                {
                    HP++;
                }
            }
        }
        private void Stop ()
        {
            UpSpeed = 0;
            Jumps = 0;
            GroundPounding = false;
        }
        private void StopMovement()
        {
            player = oldposition;
            speed = 0;
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
            for (int i = 0;  i < 150; i++)
                {
                    if (FacingRight == true && player.X < GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 19)
                    {
                        player.X++;
                    }
                    else if (FacingRight == false && player.X > 0)
                    {
                        player.X--;
                    }
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
            player = oldposition;
        }
        private void Gravity()
        {
            if (UpSpeed > -15)
                {
                    UpSpeed--;
                }
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
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, player, Color.White);
        }
    }
}