using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Slutprojekt
{
    public class QuitButton
    {       
        Texture2D texture;
        Vector2 position;
        Rectangle button;
        Color color;
        
        public QuitButton(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            button = new Rectangle((int)position.X, (int)position.Y, 135, 60);
        }
        public void Update(Game1 game)
        {
            MouseState mstate = Mouse.GetState();
            if (button.Intersects(new Rectangle(mstate.Position.X, mstate.Position.Y, 1, 1)))
            {
                color = Color.White;
                if (mstate.LeftButton == ButtonState.Pressed)
                {
                    game.Exit();
                }
            }
            else
            {
                color = Color.Gray;
            }
        }

        public void Draw (SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, button, color);
        }
    }
}