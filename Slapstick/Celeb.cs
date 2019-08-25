using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slapstick
{
    class Celeb
    {
        public int lives = 3;

        public Texture2D texture;
        public Vector2 position;



        public Celeb(Texture2D tex, GraphicsDeviceManager gdm)
        {
            texture = tex;
            position = new Vector2((gdm.PreferredBackBufferWidth - texture.Width)/2,
               700);
        }

        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public void collision(bool isNoisy)
        {
            if (isNoisy)
            {
                lives--;
            }
        }



    }
}
