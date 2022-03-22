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

        public Player (Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            player = new Rectangle((int)position.X, (int)position.Y, 20, 20);
        }

        public void Update(Game1 game)
        {
            KeyboardState kstate = Keyboard.GetState();
            Board.GetState();
            JumpCharges = 2;
            
            if (kstate.IsKeyDown(Keys.Left))
            {
                player.X -= 5;
            }
            if (kstate.IsKeyDown(Keys.Right))
            {
                player.X += 5;
            }
            if (player.X < 0)
            {
                player.X = oldposition.X;
            }
            if (player.X > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 19)
            {
                player.X = oldposition.X;
            }
            if (Jumps < JumpCharges)
            {
                if (Board.HasBeenPressed(Keys.Up))
                {
                    falling = true;
                    UpSpeed = 15;
                    Jumps++;
                }
            }
            player.Y -= UpSpeed;
            if (falling == true)
            {
                if (UpSpeed > -15)
                {
                    UpSpeed--;
                }
            }

            oldposition = player;
        }
        public void Stop ()
        {
            UpSpeed = 0;
            falling = false;
            player = oldposition;
            Jumps = 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, player, Color.White);
        }
    }
}