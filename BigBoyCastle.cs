using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Slutprojekt
{
    public class BigBoyCastle
    {
        Texture2D texture;
        Rectangle castle;

        public BigBoyCastle(Texture2D texture)
        {
            this.texture = texture;
            castle = new Rectangle (-768, 50, 800, 800);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, castle, Color.White);
        }
    }
}