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
        public Rectangle enemy;
        public int HP = 20;
        public int MaxHP = 20;
        int UpSpeed;
        Vector2 PosInGrid;
        Map1 Map1;
        bool falling;
        EnemyHPBar HPbar;

        public Enemy (Texture2D texture, Vector2 position, Map1 Map1)
        {
            this.texture = texture;
            this.position = position;
            this.Map1 = Map1;
            HPbar = new EnemyHPBar(texture, new Vector2 (position.X, position.Y - 10), this);
            enemy = new Rectangle((int)position.X, (int)position.Y, 20 , 20);
        }

        public void Update(Player Player)
        {
            CheckGridPosition();

            if (enemy.X > Player.player.X && Player.player.X > enemy.X - 700)
            {
                enemy.X--;
            }
            if (enemy.X < Player.player.X && Player.player.X < enemy.X + 700)
            {
                enemy.X++;
            }
            if (falling == true)
            {
                EnemyGravity();
            }
            CheckCollisions();
            
            if (enemy.Intersects(Player.player))
            {
                Player.HP--;
            }
            foreach (Attack attack in Player.ActiveAttacks)
            {
                if (enemy.Intersects(attack.attack))
                {
                    HP-= 5;
                    if (Player.player.X > enemy.X)
                    {
                        enemy.X -= 40;
                        UpSpeed = 5;
                        falling = true;
                    }
                    if (Player.player.X < enemy.X)
                    {
                        enemy.X += 40;
                        UpSpeed = 5;
                        falling = true;
                    }
                }
            }
            HPbar.Update();
        }

        public void Draw (SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, enemy, Color.DarkRed);
            HPbar.Draw(spriteBatch);
        }

        private void CheckGridPosition()
        {
           PosInGrid = new Vector2(enemy.X / Map1.TileSize, enemy.Y / Map1.TileSize);
        }
        private void CheckCollisions()
        {
            if (Map1.ActiveMap[(int)PosInGrid.Y + 1, (int)PosInGrid.X] != 0 && Map1.ActiveMap[(int)PosInGrid.Y + 1, (int)PosInGrid.X] != 5 && enemy.Y >= PosInGrid.Y * Map1.TileSize + Map1.TileSize - enemy.Height)
            {
                enemy.Y = ((int)PosInGrid.Y) * Map1.TileSize - enemy.Height + Map1.TileSize;
                Stop();
            }
            else if (Map1.ActiveMap[(int)PosInGrid.Y + 1, (int)PosInGrid.X + 1] != 0 && enemy.Y >= PosInGrid.Y * Map1.TileSize + Map1.TileSize - enemy.Height && enemy.X > PosInGrid.X * Map1.TileSize - enemy.Width + Map1.TileSize)
            {
                enemy.Y = ((int)PosInGrid.Y) * Map1.TileSize - enemy.Height + Map1.TileSize;
            }
            else if (Map1.ActiveMap[(int)PosInGrid.Y + 1, (int)PosInGrid.X] == 0 || Map1.ActiveMap[(int)PosInGrid.Y + 1, (int)PosInGrid.X] == 5)
            {
                falling = true;
            }
            if(Map1.ActiveMap[(int)PosInGrid.Y - 1, (int)PosInGrid.X] != 0 && enemy.Y < PosInGrid.Y * Map1.TileSize)
            {
                enemy.Y = (int)PosInGrid.Y * Map1.TileSize;
            }
            if (Map1.ActiveMap[(int)PosInGrid.Y, (int)PosInGrid.X + 1] != 0 && enemy.X >= PosInGrid.X * Map1.TileSize + Map1.TileSize - enemy.Width)
            {
                enemy.X = (int)PosInGrid.X * Map1.TileSize - enemy.Width + Map1.TileSize;
            }
            if (Map1.ActiveMap[(int)PosInGrid.Y, (int)PosInGrid.X - 1] != 0 && enemy.X < PosInGrid.X * Map1.TileSize)
            {
                enemy.X = (int)PosInGrid.X * Map1.TileSize;
            }
        }
        private void EnemyGravity()
        {
            if (UpSpeed > -15)
            {
                UpSpeed--;
            }
            enemy.Y -= UpSpeed;
        }
        private void Stop()
        {
            UpSpeed = 0;
            falling = false;
        }
    }
}