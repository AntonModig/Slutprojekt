using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Slutprojekt
{
    public class Button
    {
        Texture2D texture;
        Vector2 position;
        int height;
        int width;
        Rectangle button;

        public Button(Texture2D texture, Vector2 position, int height, int width)
        {
            texture = this.texture;
            position = this.position;
            height = this.height;
            width = this.height;
            button = new Rectangle((int)position.X, (int)position.Y, height, width);
        }
        public void Update()
        {
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, button, Color.White);
        }
    }
}