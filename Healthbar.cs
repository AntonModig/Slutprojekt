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


        public Healthbar(Texture2D texture, Player player)
        {
            this.texture = texture;
            HPbar = new Rectangle(50, 50, player.HP * 3, 20);
        }

        public void Update(Player player)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, HPbar, Color.Red);
        }
    }
}