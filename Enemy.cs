using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Slutprojekt
{
    public class Enemy
    {
        Texture2D texture;
        Vector2 position;
        Rectangle enemy;
        public int HP = 20;
        int UpSpeed;

        public Enemy (Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            enemy = new Rectangle((int)position.X, (int)position.Y, 20 , 20);
        }

        public void Update(Game1 game, Player Player)
        {
            if (enemy.X > Player.player.X)
            {
                enemy.X--;
            }
            if (enemy.X < Player.player.X)
            {
                enemy.X++;
            }
        }

        public void Draw (SpriteBatch spriteBatch)
        {
            if (HP >= 0)
            {
                spriteBatch.Draw(texture, enemy, Color.Red);
            }
        }

        private void EnemyGravity()
        {
            if (UpSpeed > -15)
                {
                    UpSpeed--;
                }
        }
        private void Stop()
        {
            UpSpeed = 0;
        }
    }
}