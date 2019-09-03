using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
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
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public void makePresent()
        {
            Present p = new Present();
            p.Initialize(tex);
            p.position.Y = -400;
            p.position.X = random.Next(934);
            presents.Add(p);
        }
        public void Draw(SpriteBatch spriteBatch,SpriteFont font)
        {
            foreach (Present p in presents)
            {
                spriteBatch.Draw(p.texture, p.position, Color.White);
                spriteBatch.DrawString(font, chars[p.letterNumber].ToString(),new Vector2(p.position.X+35, p.position.Y + 280),Color.Red,0.0f,new Vector2(0,0),4,SpriteEffects.None,1f);
                
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
                foreach(Keys K in Keyboard.GetState().GetPressedKeys())
                {
                    if (chars[p.letterNumber].ToString() == K.ToString())
                    {
                        presentIndexToDelete = presents.IndexOf(p);
                        if (p.position.Y <= 100)
                        {
                            GameState.Score += 100;   
                        }
                        else if (p.position.Y > 100 && (p.position.Y < 200))
                        {
                            GameState.Score += 80;
                        }
                        else if (p.position.Y > 200 && (p.position.Y < 300))
                        {
                            GameState.Score += 60;
                        }
                        else
                        {
                            GameState.Score += 40;
                        }
                    }
                }
            }
            if (presentIndexToDelete != -1)
            {
                presents.RemoveAt(presentIndexToDelete);
            }
        }
    }
}
