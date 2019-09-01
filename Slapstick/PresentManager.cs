using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slapstick
{
    class PresentManager
    {
        public List<Present> presents = new List<Present>();
        Texture2D tex;
        double presentTimer = 0;
        int presentIndexToDelete = -1;
        Random random = new Random();
        public void makePresent()
        {
            Present p = new Present();
            p.Initialize(tex);
            p.position.Y = -400;
            p.position.X = random.Next(934);
            presents.Add(p);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Present p in presents)
            {
                spriteBatch.Draw(p.texture, p.position, Color.White);
            }
        }

        public void LoadContent(ContentManager Content)
        {
            tex = Content.Load<Texture2D>("Images/Balloon_Present");
        }

        public void Update(GameTime gameTime)
        {
            presentTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (presentTimer > 15)
            {
                makePresent();
                presentTimer = 0;
            }

            presentIndexToDelete = -1;
            foreach (Present p in presents)
            {
                p.Update(gameTime);
                if (p.position.Y > 400)
                {
                    presentIndexToDelete = presents.IndexOf(p);
                }
            }
            if (presentIndexToDelete != -1)
            {
                presents.RemoveAt(presentIndexToDelete);
            }
        }
    }
}
