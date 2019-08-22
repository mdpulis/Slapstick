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

        private const int PAD_HEIGHT_LOCATION = 900;

        private const int PAD_PIXEL_WIDTH = 150;
        private const int PAD_PIXEL_HEIGHT = 150;

        //private Texture2D aPad;
        //private Texture2D sPad;
        //private Texture2D dPad;
        //private Texture2D jPad;
        //private Texture2D kPad;
        //private Texture2D lPad;

        private Texture2D whiteSquare;
        private Texture2D graySquare;

        private int aPadLocation;
        private int sPadLocation;
        private int dPadLocation;
        private int jPadLocation;
        private int kPadLocation;
        private int lPadLocation;

        private Vector2 aSize;
        private Vector2 sSize;
        private Vector2 dSize;
        private Vector2 jSize;
        private Vector2 kSize;
        private Vector2 lSize;

        private int aFill;
        private int sFill;
        private int dFill;
        private int jFill;
        private int kFill;
        private int lFill;

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

        private SpriteFont font;



        public PlayerInput()
        {
            aPadLocation = PAD_PIXEL_WIDTH;
            sPadLocation = PAD_PIXEL_WIDTH * 2 + (PAD_PIXEL_WIDTH / 2);
            dPadLocation = PAD_PIXEL_WIDTH * 3 + ((PAD_PIXEL_WIDTH / 2) * 2);
            jPadLocation = 1920 - (PAD_PIXEL_WIDTH * 3) - ((PAD_PIXEL_WIDTH / 2) * 2) - PAD_PIXEL_WIDTH;
            kPadLocation = 1920 - (PAD_PIXEL_WIDTH * 2) - (PAD_PIXEL_WIDTH / 2) - PAD_PIXEL_WIDTH;
            lPadLocation = 1920 - (PAD_PIXEL_WIDTH) - PAD_PIXEL_WIDTH;

            aFill = PAD_PIXEL_HEIGHT;
            sFill = PAD_PIXEL_HEIGHT;
            dFill = PAD_PIXEL_HEIGHT;
            jFill = PAD_PIXEL_HEIGHT;
            kFill = PAD_PIXEL_HEIGHT;
            lFill = PAD_PIXEL_HEIGHT;
        }

        public void Initialize()
        {
            
        }

        public void LoadContent(ContentManager Content)
        {
            //aPad = Content.Load<Texture2D>("Images/square_150x150");
            //sPad = Content.Load<Texture2D>("Images/square_150x150");
            //dPad = Content.Load<Texture2D>("Images/square_150x150");
            //jPad = Content.Load<Texture2D>("Images/square_150x150");
            //kPad = Content.Load<Texture2D>("Images/square_150x150");
            //lPad = Content.Load<Texture2D>("Images/square_150x150");

            graySquare = Content.Load<Texture2D>("Images/square_150x150");
            whiteSquare = Content.Load<Texture2D>("Images/square_150x150_white");


            font = Content.Load<SpriteFont>("Fonts/BigArial");

            aSize = font.MeasureString("a");
            sSize = font.MeasureString("s");
            dSize = font.MeasureString("d");
            jSize = font.MeasureString("j");
            kSize = font.MeasureString("k");
            lSize = font.MeasureString("l");
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
                aCooldownTime = 0.0f;
                aFill = 0;
                Slap(aPadLocation, aPadLocation + PAD_PIXEL_WIDTH, people);
            }
            if (sActive && Keyboard.GetState().IsKeyDown(Keys.S))
            {
                sActive = false;
                sCooldownTime = 0.0f;
                sFill = 0;
                Slap(sPadLocation, sPadLocation + PAD_PIXEL_WIDTH, people);
            }
            if (dActive && Keyboard.GetState().IsKeyDown(Keys.D))
            {
                dActive = false;
                dCooldownTime = 0.0f;
                dFill = 0;
                Slap(dPadLocation, dPadLocation + PAD_PIXEL_WIDTH, people);
            }
            if (jActive && Keyboard.GetState().IsKeyDown(Keys.J))
            {
                jActive = false;
                jCooldownTime = 0.0f;
                jFill = 0;
                Slap(jPadLocation, jPadLocation + PAD_PIXEL_WIDTH, people);
            }
            if (kActive && Keyboard.GetState().IsKeyDown(Keys.K))
            {
                kActive = false;
                kCooldownTime = 0.0f;
                kFill = 0;
                Slap(kPadLocation, kPadLocation + PAD_PIXEL_WIDTH, people);
            }
            if (lActive && Keyboard.GetState().IsKeyDown(Keys.L))
            {
                lActive = false;
                lCooldownTime = 0.0f;
                lFill = 0;
                Slap(lPadLocation, lPadLocation + PAD_PIXEL_WIDTH, people);
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
                aCooldownTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                float aTimePercentage = aCooldownTime / LONG_COOLDOWN_TIME;
                aFill = (int)(aTimePercentage * 150.0f);

                if (aCooldownTime >= LONG_COOLDOWN_TIME)
                {
                    aActive = true;
                    aCooldownTime = 0.0f;
                    aFill = PAD_PIXEL_HEIGHT;
                }
            }

            if (!sActive)
            {
                sCooldownTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                float sTimePercentage = sCooldownTime / MEDIUM_COOLDOWN_TIME;
                sFill = (int)(sTimePercentage * 150.0f);

                if (sCooldownTime >= MEDIUM_COOLDOWN_TIME)
                {
                    sActive = true;
                    sCooldownTime = 0.0f;
                    sFill = PAD_PIXEL_HEIGHT;
                }
            }

            if (!dActive)
            {
                dCooldownTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                float dTimePercentage = dCooldownTime / SHORT_COOLDOWN_TIME;
                dFill = (int)(dTimePercentage * 150.0f);

                if (dCooldownTime >= SHORT_COOLDOWN_TIME)
                {
                    dActive = true;
                    dCooldownTime = 0.0f;
                    dFill = PAD_PIXEL_HEIGHT;
                }
            }

            if (!jActive)
            {
                jCooldownTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                float jTimePercentage = jCooldownTime / SHORT_COOLDOWN_TIME;
                jFill = (int)(jTimePercentage * 150.0f);

                if (jCooldownTime >= SHORT_COOLDOWN_TIME)
                {
                    jActive = true;
                    jCooldownTime = 0.0f;
                    jFill = PAD_PIXEL_HEIGHT;
                }
            }

            if (!kActive)
            {
                kCooldownTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                float kTimePercentage = kCooldownTime / MEDIUM_COOLDOWN_TIME;
                kFill = (int)(kTimePercentage * 150.0f);

                if (kCooldownTime >= MEDIUM_COOLDOWN_TIME)
                {
                    kActive = true;
                    kCooldownTime = 0.0f;
                    kFill = PAD_PIXEL_HEIGHT;
                }
            }

            if (!lActive)
            {
                lCooldownTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                float lTimePercentage = lCooldownTime / LONG_COOLDOWN_TIME;
                lFill = (int)(lTimePercentage * 150.0f);

                if (lCooldownTime >= LONG_COOLDOWN_TIME)
                {
                    lActive = true;
                    lCooldownTime = 0.0f;
                    lFill = PAD_PIXEL_HEIGHT;
                }
            }

        }

        /// <summary>
        /// Checks all people to slap!
        /// </summary>
        public void Slap(int xStartPosition, int xEndPosition, List<Person> people)
        {
            for(int i = 0; i < people.Count; i++)
            {
                if (people[i].position.X > xStartPosition || people[i].position.Y < xEndPosition)
                {
                    people.RemoveAt(i);
                    i--;
                }
            }
            //foreach(Person p in people)
            //{
            //    if(p.position.X > xStartPosition || p.position.Y < xEndPosition)
            //    {
            //        people.Remove(p);
            //    }
            //}
        }


        /// <summary>
        /// Draws objects
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="gameTime"></param>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(graySquare, new Vector2(aPadLocation, PAD_HEIGHT_LOCATION), Color.White);
            spriteBatch.Draw(graySquare, new Vector2(sPadLocation, PAD_HEIGHT_LOCATION), Color.White);
            spriteBatch.Draw(graySquare, new Vector2(dPadLocation, PAD_HEIGHT_LOCATION), Color.White);
            spriteBatch.Draw(graySquare, new Vector2(jPadLocation, PAD_HEIGHT_LOCATION), Color.White);
            spriteBatch.Draw(graySquare, new Vector2(kPadLocation, PAD_HEIGHT_LOCATION), Color.White);
            spriteBatch.Draw(graySquare, new Vector2(lPadLocation, PAD_HEIGHT_LOCATION), Color.White);

            spriteBatch.Draw(whiteSquare, new Rectangle(aPadLocation + PAD_PIXEL_WIDTH, PAD_HEIGHT_LOCATION + PAD_PIXEL_HEIGHT, PAD_PIXEL_WIDTH, aFill), null, Color.White, MathHelper.Pi, new Vector2(0,0), SpriteEffects.None, 1);
            spriteBatch.Draw(whiteSquare, new Rectangle(sPadLocation + PAD_PIXEL_WIDTH, PAD_HEIGHT_LOCATION + PAD_PIXEL_HEIGHT, PAD_PIXEL_WIDTH, sFill), null, Color.White, MathHelper.Pi, new Vector2(0,0), SpriteEffects.None, 1);
            spriteBatch.Draw(whiteSquare, new Rectangle(dPadLocation + PAD_PIXEL_WIDTH, PAD_HEIGHT_LOCATION + PAD_PIXEL_HEIGHT, PAD_PIXEL_WIDTH, dFill), null, Color.White, MathHelper.Pi, new Vector2(0,0), SpriteEffects.None, 1);
            spriteBatch.Draw(whiteSquare, new Rectangle(jPadLocation + PAD_PIXEL_WIDTH, PAD_HEIGHT_LOCATION + PAD_PIXEL_HEIGHT, PAD_PIXEL_WIDTH, jFill), null, Color.White, MathHelper.Pi, new Vector2(0,0), SpriteEffects.None, 1);
            spriteBatch.Draw(whiteSquare, new Rectangle(kPadLocation + PAD_PIXEL_WIDTH, PAD_HEIGHT_LOCATION + PAD_PIXEL_HEIGHT, PAD_PIXEL_WIDTH, kFill), null, Color.White, MathHelper.Pi, new Vector2(0,0), SpriteEffects.None, 1);
            spriteBatch.Draw(whiteSquare, new Rectangle(lPadLocation + PAD_PIXEL_WIDTH, PAD_HEIGHT_LOCATION + PAD_PIXEL_HEIGHT, PAD_PIXEL_WIDTH, lFill), null, Color.White, MathHelper.Pi, new Vector2(0,0), SpriteEffects.None, 1);

            spriteBatch.DrawString(font, "a", new Vector2(aPadLocation + (PAD_PIXEL_WIDTH / 2), PAD_HEIGHT_LOCATION + (PAD_PIXEL_HEIGHT / 2)), Color.Black, 0, aSize * 0.5f, 1, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "s", new Vector2(sPadLocation + (PAD_PIXEL_WIDTH / 2), PAD_HEIGHT_LOCATION + (PAD_PIXEL_HEIGHT / 2)), Color.Black, 0, sSize * 0.5f, 1, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "d", new Vector2(dPadLocation + (PAD_PIXEL_WIDTH / 2), PAD_HEIGHT_LOCATION + (PAD_PIXEL_HEIGHT / 2)), Color.Black, 0, dSize * 0.5f, 1, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "j", new Vector2(jPadLocation + (PAD_PIXEL_WIDTH / 2), PAD_HEIGHT_LOCATION + (PAD_PIXEL_HEIGHT / 2)), Color.Black, 0, jSize * 0.5f, 1, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "k", new Vector2(kPadLocation + (PAD_PIXEL_WIDTH / 2), PAD_HEIGHT_LOCATION + (PAD_PIXEL_HEIGHT / 2)), Color.Black, 0, kSize * 0.5f, 1, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "l", new Vector2(lPadLocation + (PAD_PIXEL_WIDTH / 2), PAD_HEIGHT_LOCATION + (PAD_PIXEL_HEIGHT / 2)), Color.Black, 0, lSize * 0.5f, 1, SpriteEffects.None, 0);

        }

    }


}
