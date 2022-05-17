using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Slutprojekt
{
    public class Attack
    {
        Texture2D texture;
        Vector2 position;
        Player Player;
        public Rectangle attack;
        int adder = 0;



        public Attack (Texture2D texture, Vector2 position, Player Player)
        {
            this.texture = texture;
            this.position = position;
            this.Player = Player;
            attack = new Rectangle((int)position.X, (int)position.Y, 15, 5);
        }

        public void Update()
        {
            if (Player.FacingRight == true)
            {
                attack.X = Player.player.X + Player.player.Width;
            }
            else
            {
                attack.X = Player.player.X - attack.Width;
            }
            attack.Y = Player.player.Y + adder;
            adder+=3;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw (texture, attack, Color.Pink);
        }
    }
}