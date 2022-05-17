using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Slutprojekt
{
    public class GameOverText
    {
        Texture2D texture;
        Rectangle Text;

        public GameOverText(Texture2D texture)
        {
            this.texture = texture;
            Text = new Rectangle(1366/2 - 165/2, 100, 165, 96);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Text, Color.White);
        }
    }
}