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
        public bool BlinkCharged;
        public float timer = 10;
        const float TIMER = 10;
        float MaxHP = 100;
        float HP = 100;
        public float HPpercent;


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
            
            foreach (Rectangle element in game.MAP1.Tiles)
            {
                if(player.Intersects(element))
                {
                    if (player.Y > element.Y - player.Height)
                    {
                        Stop();
                        player.Y = element.Y - player.Height;
                    }
                    if(player.X < element.X - player.Height)
                    {
                        player.X = element.X - player.Height;
                    }
                }
            }
            
            if (kstate.IsKeyDown(Keys.O))
            {
                HP--;
            }
            if (HP <= 0)
            {
                game.hasstarted = false;
                HP = 100;
                MaxHP = 100;
                ChargeBlink();
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

            Move();

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
            
            oldposition = player;
        }


        public void Stop ()
        {
            UpSpeed = 0;
            falling = false;
            Jumps = 0;
        }


        private void StopMovement()
        {
            player = oldposition;
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