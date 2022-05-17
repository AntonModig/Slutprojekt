using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Slutprojekt
{
    public class EnemyHPBar
    {
        Texture2D texture;
        Vector2 position;
        Rectangle bar;
        Enemy Enemy;

        public EnemyHPBar (Texture2D texture, Vector2 position, Enemy Enemy)
        {
            this.texture = texture;
            this.position = position;
            this.Enemy = Enemy;
            bar = new Rectangle((int)position.X, (int)position.Y, 20, 5);
        }

        public void Update()
        {
            bar.Width = (int)((float)Enemy.HP / (float)Enemy.MaxHP * Enemy.enemy.Width);
            bar.X = Enemy.enemy.X;
            bar.Y = Enemy.enemy.Y - 10;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bar, Color.Red);
        }
    }
}