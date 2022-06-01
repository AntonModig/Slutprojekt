using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Slutprojekt
{
    public class Coin
    {
        Texture2D texture;
        Rectangle coin;
        public int CoinCounter = 0;
        SpriteFont font;
        Game1 game;

        public Coin(Texture2D texture, SpriteFont font, Game1 game)
        {
            this.texture = texture;
            this.font = font;
            this.game = game;
            coin = new Rectangle(1211, 50, 55, 80);
        }

        public void Update()
        {
            if (game.GameOver == false)
            {
                coin.X = 1211;
                coin.Y = 50;
            }
            else
            {
                coin.X = 655;
                coin.Y = 344;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, coin, Color.White);

            spriteBatch.DrawString(font, CoinCounter.ToString(), new Vector2(coin.X + 55, coin.Y + 30), Color.White);
        }
    }
}