using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Slutprojekt
{
    public class HealAbility
    {
        Texture2D texture;
        Rectangle Icon;
        public float timer = 20;
        const float TIMER = 20;
        Color color;
        SpriteFont font;
        bool Charged;
        int Countdown;
        string CountdownStr;
        Vector2 CountdownPosition;
        

        public HealAbility(Texture2D texture)
        {
            this.texture = texture;
            Icon = new Rectangle (15, 698, 32, 32);
        }

        public void Update(Player player, GameTime gameTime, Game1 game)
        {
            Charged = player.HealingReady;
            Countdown = (int)timer + 1;
            CountdownStr = Countdown.ToString();
            
            if (Charged == false)
            {   
                RechargeHeal(player, gameTime);
            }

            if (Charged == true)
            {
                color = Color.White;
            }
            else
            {
                color = Color.Gray;
            }
            if (Countdown >= 10)
            {   
                CountdownPosition = new Vector2(Icon.X + 2, Icon.Y + 4);
            }
            else if (Countdown < 10)
            {
                CountdownPosition = new Vector2(Icon.X + 9, Icon.Y + 4);
            }
            font = game.font;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Icon, color);

            if (Charged == false)
            {
                spriteBatch.DrawString(font, CountdownStr, CountdownPosition, Color.White);
            }
        }

        public void RechargeHeal(Player player, GameTime gameTime)
        {
            float Elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer -= Elapsed;
            if (timer < 0)
            {
                player.RefillHeal();
                timer = TIMER;
            }
        }
    }
}