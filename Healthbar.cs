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
        float length;

        public Healthbar(Texture2D texture, Player player)
        {
            this.texture = texture;
            length = player.HP;
            HPbar = new Rectangle(50, 50, (int)length, 20);
        }

        public void Update(Player player)
        {
            length = player.HP / player.MaxHP * 300;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, HPbar, Color.Red);
        }
    }
}