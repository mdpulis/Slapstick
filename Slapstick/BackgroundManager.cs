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
    public class Cloud
    {
        private int speed;
        public Texture2D texture;
        public Vector2 position;

        System.Random random = new System.Random();

        public void Initialize(int spd, Texture2D tex)
        {
            speed = spd;
            texture = tex;
            position = new Vector2(0, 50 + random.Next(100));
        }

        public void Update(GameTime gameTime)
        {
            position.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

    }


    /// <summary>
    /// Handles lots of background data
    /// </summary>
    public class BackgroundManager
    {

        private Texture2D background;
        private Texture2D tree;
        private Texture2D sky;
        private Texture2D cloud;

        private List<Cloud> clouds;

        private float cloudTimer = 0.0f;

        System.Random random = new System.Random();


        public BackgroundManager()
        {
            clouds = new List<Cloud>();
        }

        public void Initialize()
        {

        }

        public void LoadContent(ContentManager Content)
        {
            background = Content.Load<Texture2D>("Images/chinese_theatre");
            tree = Content.Load<Texture2D>("Images/tree");
            sky = Content.Load<Texture2D>("Images/sky");
            cloud = Content.Load<Texture2D>("Images/cloud");
        }

        public void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        public void Update(GameTime gameTime)
        {
            cloudTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(cloudTimer >= 3.0f)
            {
                MakeCloud();
                cloudTimer = 0.0f;
            }

            foreach (Cloud c in clouds)
            {
                c.Update(gameTime);
            }
        }

        private void MakeCloud()
        {
            Cloud c = new Cloud();
            c.Initialize(random.Next(5) * 5, cloud); //Third value is speed, increases as BPM increases. 4th value is unused but could be used for different characters
        }
        

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(sky, new Rectangle(0, 0, 1920, 442), Color.White);

            foreach(Cloud c in clouds)
            {
                spriteBatch.Draw(c.texture, c.position, Color.White);
            }

            spriteBatch.Draw(background, new Rectangle(0, 0, 1920, 1080), Color.White);
            spriteBatch.Draw(tree, new Vector2(300, 900), Color.White);
        }
    }
}
