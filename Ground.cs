using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Slutprojekt
{
    public class Ground
    {
        Texture2D texture;
        Vector2 position;
        public Rectangle ground;

        public Ground (Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            ground = new Rectangle ((int)position.X, (int)position.Y, 1366, 200);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, ground, Color.DarkGreen);
        }
    }
}