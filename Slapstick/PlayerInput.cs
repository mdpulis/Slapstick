using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Slapstick
{
    /// <summary>
    /// Handles player input
    /// </summary>
    public class PlayerInput
    {
        private const float LONG_COOLDOWN_TIME = 3.0f;
        private const float MEDIUM_COOLDOWN_TIME = 2.0f;
        private const float SHORT_COOLDOWN_TIME = 1.0f;

        private const int PAD_PIXEL_WIDTH = 150;

        private Texture2D aPad;
        private Texture2D sPad;
        private Texture2D dPad;
        private Texture2D jPad;
        private Texture2D kPad;
        private Texture2D lPad;

        private SpriteFont font;

        private float aCooldownTime = 0.0f;
        private float sCooldownTime = 0.0f;
        private float dCooldownTime = 0.0f;
        private float jCooldownTime = 0.0f;
        private float kCooldownTime = 0.0f;
        private float lCooldownTime = 0.0f;

        private bool aActive = true;
        private bool sActive = true;
        private bool dActive = true;
        private bool jActive = true;
        private bool kActive = true;
        private bool lActive = true;

        public PlayerInput()
        {

        }

        public void Initialize()
        {

        }

        public void LoadContent(ContentManager Content)
        {
            aPad = Content.Load<Texture2D>("Images/square_150x150");
            sPad = Content.Load<Texture2D>("Images/square_150x150");
            dPad = Content.Load<Texture2D>("Images/square_150x150");
            jPad = Content.Load<Texture2D>("Images/square_150x150");
            kPad = Content.Load<Texture2D>("Images/square_150x150");
            lPad = Content.Load<Texture2D>("Images/square_150x150");

            font = Content.Load<SpriteFont>("Fonts/BigArial");

        }

        public void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime, List<Person> people)
        {
            

            if (aActive && Keyboard.GetState().IsKeyDown(Keys.A))
            {
                aActive = false;
                aCooldownTime = LONG_COOLDOWN_TIME;
            }
            else if (sActive && Keyboard.GetState().IsKeyDown(Keys.S))
            {
                sActive = false;
                sCooldownTime = LONG_COOLDOWN_TIME;
            }
            else if (dActive && Keyboard.GetState().IsKeyDown(Keys.D))
            {
                dActive = false;
                dCooldownTime = LONG_COOLDOWN_TIME;
            }
            else if (jActive && Keyboard.GetState().IsKeyDown(Keys.J))
            {
                jActive = false;
                jCooldownTime = LONG_COOLDOWN_TIME;
            }
            else if (kActive && Keyboard.GetState().IsKeyDown(Keys.K))
            {
                kActive = false;
                kCooldownTime = LONG_COOLDOWN_TIME;
            }
            else if (lActive && Keyboard.GetState().IsKeyDown(Keys.L))
            {
                lActive = false;
                lCooldownTime = LONG_COOLDOWN_TIME;
            }




            CheckCooldownTimes(gameTime);
        }


        /// <summary>
        /// Check each of the cooldown times and active states to determine if we should reset parameters
        /// </summary>
        /// <param name="gameTime"></param>
        private void CheckCooldownTimes(GameTime gameTime)
        {
            if(!aActive)
            {
                aCooldownTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if(aCooldownTime <= 0.0f)
                {
                    aActive = true;
                    aCooldownTime = 0.0f;
                }
            }
            else if (!sActive)
            {
                sCooldownTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (sCooldownTime <= 0.0f)
                {
                    sActive = true;
                    sCooldownTime = 0.0f;
                }
            }
            else if (!dActive)
            {
                dCooldownTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (dCooldownTime <= 0.0f)
                {
                    dActive = true;
                    dCooldownTime = 0.0f;
                }
            }
            else if (!jActive)
            {
                jCooldownTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (jCooldownTime <= 0.0f)
                {
                    jActive = true;
                    jCooldownTime = 0.0f;
                }
            }
            else if (!kActive)
            {
                kCooldownTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (kCooldownTime <= 0.0f)
                {
                    kActive = true;
                    kCooldownTime = 0.0f;
                }
            }
            else if (!lActive)
            {
                lCooldownTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (lCooldownTime <= 0.0f)
                {
                    lActive = true;
                    lCooldownTime = 0.0f;
                }
            }

        }


        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(aPad, new Vector2(PAD_PIXEL_WIDTH, 900), Color.White);
            spriteBatch.Draw(sPad, new Vector2(PAD_PIXEL_WIDTH * 2 + (PAD_PIXEL_WIDTH / 2), 900), Color.White);
            spriteBatch.Draw(dPad, new Vector2(PAD_PIXEL_WIDTH * 3 + ((PAD_PIXEL_WIDTH / 2) * 2), 900), Color.White);
            spriteBatch.Draw(jPad, new Vector2(1920 - (PAD_PIXEL_WIDTH * 3) - ((PAD_PIXEL_WIDTH / 2) * 2) - PAD_PIXEL_WIDTH, 900), Color.White);
            spriteBatch.Draw(kPad, new Vector2(1920 - (PAD_PIXEL_WIDTH * 2) - (PAD_PIXEL_WIDTH / 2) - PAD_PIXEL_WIDTH, 900), Color.White);
            spriteBatch.Draw(lPad, new Vector2(1920 - (PAD_PIXEL_WIDTH) - PAD_PIXEL_WIDTH, 900), Color.White);

            spriteBatch.DrawString(font, "a", new Vector2(100, 100), Color.White);

        }

    }


}
