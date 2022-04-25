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
        int playerheight = 20;
        public float timer = 10;
        const float TIMER = 10;
        Rectangle ground;
        public int HP = 100;


        public Player (Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            player = new Rectangle((int)position.X, (int)position.Y, playerheight, 20);
        }

        public void Update(Game1 game, GameTime gameTime)
        {
            KeyboardState kstate = Keyboard.GetState();
            Board.GetState();
            JumpCharges = 2;
            ground = game.Ground.ground;
            
            if (player.Intersects(ground))
            {
                Stop();
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
            
            oldposition = player;
        }


        public void Stop ()
        {
            player.Y = ground.Y - playerheight;
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