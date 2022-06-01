using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Slutprojekt
{
    public class Portal
    {
        Texture2D texture;
        Vector2 position;
        Rectangle portal;
        Player player;
        SpriteFont font;
        Game1 game;
        Vector2 TextPosition;
        string Text = "Press E to move on";

        public Portal(Texture2D texture, Vector2 position, Game1 game)
        {
            this.texture = texture;
            this.position = position;
            this.game = game;
            this.player = game.Player1;
            this.font = game.font;
            portal = new Rectangle((int)position.X, (int)position.Y, 76, 92);
            TextPosition = new Vector2(portal.X - 65, portal.Y - 50);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, portal, Color.White);

            if (player.player.X > portal.X - 50 && player.player.X < portal.X + 100 && player.player.Y > portal.Y - 50)
            {
                spriteBatch.DrawString(font, Text, TextPosition, Color.White);
                if (Board.HasBeenPressed(Keys.E))
                {
                    game.MAP1.CoinMultiplier += 0.25;
                    player.Reset();
                }
            }
        }
    }
}