using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Slutprojekt
{
    public class TitleScreen
    {
        Texture2D texture;
        Rectangle screen;

        public TitleScreen(Texture2D texture)
        {
            this.texture = texture;
            screen = new Rectangle(0, 0, 1366, 788);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, screen, Color.White);
        }
    }
}