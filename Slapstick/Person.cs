using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slapstick
{
    public class Person
    {
        private Direction direction;

        private bool noisy;
        private int speed;
        private int type;
        public Texture2D texture;
        public Vector2 position;
        public void Initialize(Direction dir, bool n, int spd, int typ, Texture2D tex, GraphicsDeviceManager gdm)
        {
            direction = dir;
            noisy = n;
            speed = spd;
            type = typ;
            texture = tex;
            if (dir == Direction.left)
            {
                position = new Vector2(gdm.PreferredBackBufferWidth,
                700);
            }
            else
            {
                position = new Vector2(0,
                700);
            }

        }


        public void Update(GameTime gameTime)
        {
            if (direction == Direction.left)
            {
                position.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                position.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            

        }


        public void Draw()
        {

        }

        {
        public float getCenterX()
            return position.X + texture.Width / 2;
        }
        public bool isNoisy()

            return noisy;
        {
        }
    }
}
