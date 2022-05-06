using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Slutprojekt
{
    public class Healthbar
    {
        Texture2D texture;
        Rectangle HPbar;
        Rectangle TotalHealthbar;
        float barlength;

        public Healthbar(Texture2D texture, Player player)
        {
            this.texture = texture;
            HPbar = new Rectangle(50, 50, 300, 20);
            TotalHealthbar = new Rectangle(50, 50, 300, 20);
        }

        public void Update(Player player)
        {
            barlength = player.HPpercent * 300;
            HPbar.Width = (int)barlength;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, TotalHealthbar, Color.Maroon);
            spriteBatch.Draw(texture, HPbar, Color.Red);
        }
    }
}