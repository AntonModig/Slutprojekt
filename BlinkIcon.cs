using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Slutprojekt
{
    public class BlinkIcon
    {
        Texture2D texture;
        Vector2 position;
        Rectangle icon;
        Color color;
        SpriteFont font;
        bool Charged;
        int Countdown;
        string CountdownStr;
        Vector2 CountdownPosition;

        public BlinkIcon(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            icon = new Rectangle((int)position.X, (int)position.Y, 32, 32);
        }

        public void Update(Game1 game)
        {
            Charged = game.Player1.BlinkCharged;
            Countdown = (int)game.Player1.timer + 1;
            CountdownStr = Countdown.ToString();

            if (Charged == true)
            {
                color = Color.White;
            }
            else
            {
                color = Color.Gray;
            }
            if (Countdown == 10)
            {   
                CountdownPosition = new Vector2(position.X + 2, position.Y + 4);
            }
            else if (Countdown < 10)
            {
                CountdownPosition = new Vector2(position.X + 9, position.Y + 4);
            }
            font = game.font;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, icon, color);

            if (Charged == false)
            {
                spriteBatch.DrawString(font, CountdownStr, CountdownPosition, Color.White);
            }
        }
    }   
}