using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace Slapstick
{
    /// <summary>
    /// Handles player input
    /// </summary>
    public class PlayerInput
    {
        private const float LONG_COOLDOWN_TIME = 3.2f;
        private const float MEDIUM_COOLDOWN_TIME = 2.4f;
        private const float SHORT_COOLDOWN_TIME = 1.6f;

        private const float PENALTY_MODIFIER = 2.0f;

        private const float BARRIER_MOVE_COOLDOWN_TIME = 0.2f;

        private const float SLAP_DISPLAY_TIME = 0.3f;

        private const int SLAP_HEIGHT_LOCATION = 675;
        private const int PAD_HEIGHT_LOCATION = 900;

        private const int PAD_PIXEL_WIDTH = 150;
        private const int PAD_PIXEL_HEIGHT = 150;

        private Texture2D whiteSquare;
        private Texture2D graySquare;
        private Texture2D slapHand;
        private SpriteFont letterFont;
        private SoundEffect slapSFX;
        private SoundEffect whooshSFX;
        private SoundEffect boingSFX;

        private Texture2D aSlapper;
        private Texture2D aSlapper2;
        private Texture2D sSlapper;
        private Texture2D sSlapper2;
        private Texture2D dSlapper;
        private Texture2D dSlapper2;
        private Texture2D jSlapper;
        private Texture2D jSlapper2;
        private Texture2D kSlapper;
        private Texture2D kSlapper2;
        private Texture2D lSlapper;
        private Texture2D lSlapper2;


        private bool aSlapHandActive;
        private bool sSlapHandActive;
        private bool dSlapHandActive;
        private bool jSlapHandActive;
        private bool kSlapHandActive;
        private bool lSlapHandActive;

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

        private bool aPenalty = true;
        private bool sPenalty = true;
        private bool dPenalty = true;
        private bool jPenalty = true;
        private bool kPenalty = true;
        private bool lPenalty = true;

        private bool barrierMoveOnCooldown = false;
        private float barrierMoveCooldownTime = 0.0f;


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
            graySquare = Content.Load<Texture2D>("Images/square_150x150");
            whiteSquare = Content.Load<Texture2D>("Images/square_150x150_white");

            slapHand = Content.Load<Texture2D>("Images/slap_hand_orange");
            letterFont = Content.Load<SpriteFont>("Fonts/BigArial");
            slapSFX = Content.Load<SoundEffect>("Sounds/slap_sound");
            boingSFX = Content.Load<SoundEffect>("Sounds/boing");
            whooshSFX = Content.Load<SoundEffect>("Sounds/whoosh");

            aSlapper = Content.Load<Texture2D>("Images/slapper_a");
            aSlapper2 = Content.Load<Texture2D>("Images/slapper_a2");
            sSlapper = Content.Load<Texture2D>("Images/slapper_s");
            sSlapper2 = Content.Load<Texture2D>("Images/slapper_s2");
            dSlapper = Content.Load<Texture2D>("Images/slapper_d");
            dSlapper2 = Content.Load<Texture2D>("Images/slapper_d2");
            jSlapper = Content.Load<Texture2D>("Images/slapper_j");
            jSlapper2 = Content.Load<Texture2D>("Images/slapper_j2");
            kSlapper = Content.Load<Texture2D>("Images/slapper_k");
            kSlapper2 = Content.Load<Texture2D>("Images/slapper_k2");
            lSlapper = Content.Load<Texture2D>("Images/slapper_l");
            lSlapper2 = Content.Load<Texture2D>("Images/slapper_l2");

            aSize = letterFont.MeasureString("a");
            sSize = letterFont.MeasureString("s");
            dSize = letterFont.MeasureString("d");
            jSize = letterFont.MeasureString("j");
            kSize = letterFont.MeasureString("k");
            lSize = letterFont.MeasureString("l");
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
        public void Update(GameTime gameTime, List<Person> people, BarrierManager barrierManager)
        {

            if (aActive && Keyboard.GetState().IsKeyDown(Keys.A))
            {
                aActive = false;
                aCooldownTime = 0.0f;
                aFill = 0;
                Slap(aPadLocation, aPadLocation + PAD_PIXEL_WIDTH, ref aSlapHandActive, ref aPenalty, people);
            }
            if (sActive && Keyboard.GetState().IsKeyDown(Keys.S))
            {
                sActive = false;
                sCooldownTime = 0.0f;
                sFill = 0;
                Slap(sPadLocation, sPadLocation + PAD_PIXEL_WIDTH, ref sSlapHandActive, ref sPenalty, people);
            }
            if (dActive && Keyboard.GetState().IsKeyDown(Keys.D))
            {
                dActive = false;
                dCooldownTime = 0.0f;
                dFill = 0;
                Slap(dPadLocation, dPadLocation + PAD_PIXEL_WIDTH, ref dSlapHandActive, ref dPenalty, people);
            }
            if (jActive && Keyboard.GetState().IsKeyDown(Keys.J))
            {
                jActive = false;
                jCooldownTime = 0.0f;
                jFill = 0;
                Slap(jPadLocation, jPadLocation + PAD_PIXEL_WIDTH, ref jSlapHandActive, ref jPenalty, people);
            }
            if (kActive && Keyboard.GetState().IsKeyDown(Keys.K))
            {
                kActive = false;
                kCooldownTime = 0.0f;
                kFill = 0;
                Slap(kPadLocation, kPadLocation + PAD_PIXEL_WIDTH, ref kSlapHandActive, ref kPenalty, people);
            }
            if (lActive && Keyboard.GetState().IsKeyDown(Keys.L))
            {
                lActive = false;
                lCooldownTime = 0.0f;
                lFill = 0;
                Slap(lPadLocation, lPadLocation + PAD_PIXEL_WIDTH, ref lSlapHandActive, ref lPenalty, people);
            }


            if (!barrierMoveOnCooldown)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.U))
                {
                    barrierManager.MoveBarrier(true); //move right
                    barrierMoveOnCooldown = true;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.R))
                {
                    barrierManager.MoveBarrier(false); //move left
                    barrierMoveOnCooldown = true;
                }
            }
            

            CheckCooldownTimes(gameTime);
        }


        /// <summary>
        /// Check each of the cooldown times and active states to determine if we should reset parameters
        /// </summary>
        /// <param name="gameTime"></param>
        private void CheckCooldownTimes(GameTime gameTime)
        {
            if (!aActive)
            {
                if (!aPenalty)
                    aCooldownTime += (float)gameTime.ElapsedGameTime.TotalSeconds * GameState.BeatsPerMinute / 100;
                else
                    aCooldownTime += ((float)gameTime.ElapsedGameTime.TotalSeconds * GameState.BeatsPerMinute / 100 ) / PENALTY_MODIFIER;

                float aTimePercentage = aCooldownTime / LONG_COOLDOWN_TIME;
                aFill = (int)(aTimePercentage * PAD_PIXEL_HEIGHT);

                if (aSlapHandActive && aCooldownTime >= SLAP_DISPLAY_TIME)
                {
                    aSlapHandActive = false;
                }

                if (aCooldownTime >= LONG_COOLDOWN_TIME)
                {
                    aActive = true;
                    aPenalty = false;
                    aCooldownTime = 0.0f;
                    aFill = PAD_PIXEL_HEIGHT;
                }
            }

            if (!sActive)
            {
                if (!sPenalty)
                    sCooldownTime += (float)gameTime.ElapsedGameTime.TotalSeconds * GameState.BeatsPerMinute / 100;
                else
                    sCooldownTime += ((float)gameTime.ElapsedGameTime.TotalSeconds * GameState.BeatsPerMinute / 100) / PENALTY_MODIFIER;

                float sTimePercentage = sCooldownTime / MEDIUM_COOLDOWN_TIME;
                sFill = (int)(sTimePercentage * PAD_PIXEL_HEIGHT);

                if (sSlapHandActive && sCooldownTime >= SLAP_DISPLAY_TIME)
                {
                    sSlapHandActive = false;
                }

                if (sCooldownTime >= MEDIUM_COOLDOWN_TIME)
                {
                    sActive = true;
                    sPenalty = false;
                    sCooldownTime = 0.0f;
                    sFill = PAD_PIXEL_HEIGHT;
                }
            }

            if (!dActive)
            {
                if (!dPenalty)
                    dCooldownTime += (float)gameTime.ElapsedGameTime.TotalSeconds * GameState.BeatsPerMinute / 100;
                else
                    dCooldownTime += ((float)gameTime.ElapsedGameTime.TotalSeconds * GameState.BeatsPerMinute / 100) / PENALTY_MODIFIER;

                float dTimePercentage = dCooldownTime / SHORT_COOLDOWN_TIME;
                dFill = (int)(dTimePercentage * PAD_PIXEL_HEIGHT);

                if (dSlapHandActive && dCooldownTime >= SLAP_DISPLAY_TIME)
                {
                    dSlapHandActive = false;
                }

                if (dCooldownTime >= SHORT_COOLDOWN_TIME)
                {
                    dActive = true;
                    dPenalty = false;
                    dCooldownTime = 0.0f;
                    dFill = PAD_PIXEL_HEIGHT;
                }
            }

            if (!jActive)
            {
                if (!jPenalty)
                    jCooldownTime += (float)gameTime.ElapsedGameTime.TotalSeconds * GameState.BeatsPerMinute / 100;
                else
                    jCooldownTime += ((float)gameTime.ElapsedGameTime.TotalSeconds * GameState.BeatsPerMinute / 100) / PENALTY_MODIFIER;

                float jTimePercentage = jCooldownTime / SHORT_COOLDOWN_TIME;
                jFill = (int)(jTimePercentage * PAD_PIXEL_HEIGHT);

                if (jSlapHandActive && jCooldownTime >= SLAP_DISPLAY_TIME)
                {
                    jSlapHandActive = false;
                }

                if (jCooldownTime >= SHORT_COOLDOWN_TIME)
                {
                    jActive = true;
                    jPenalty = false;
                    jCooldownTime = 0.0f;
                    jFill = PAD_PIXEL_HEIGHT;
                }
            }

            if (!kActive)
            {
                if (!kPenalty)
                    kCooldownTime += (float)gameTime.ElapsedGameTime.TotalSeconds * GameState.BeatsPerMinute / 100;
                else
                    kCooldownTime += ((float)gameTime.ElapsedGameTime.TotalSeconds * GameState.BeatsPerMinute / 100) / PENALTY_MODIFIER;

                float kTimePercentage = kCooldownTime / MEDIUM_COOLDOWN_TIME;
                kFill = (int)(kTimePercentage * PAD_PIXEL_HEIGHT);

                if (kSlapHandActive && kCooldownTime >= SLAP_DISPLAY_TIME)
                {
                    kSlapHandActive = false;
                }

                if (kCooldownTime >= MEDIUM_COOLDOWN_TIME)
                {
                    kActive = true;
                    kPenalty = false;
                    kCooldownTime = 0.0f;
                    kFill = PAD_PIXEL_HEIGHT;
                }
            }

            if (!lActive)
            {
                if (!lPenalty)
                    lCooldownTime += (float)gameTime.ElapsedGameTime.TotalSeconds * GameState.BeatsPerMinute / 100;
                else
                    lCooldownTime += ((float)gameTime.ElapsedGameTime.TotalSeconds * GameState.BeatsPerMinute / 100) / PENALTY_MODIFIER;

                float lTimePercentage = lCooldownTime / LONG_COOLDOWN_TIME;
                lFill = (int)(lTimePercentage * PAD_PIXEL_HEIGHT);

                if (lSlapHandActive && lCooldownTime >= SLAP_DISPLAY_TIME)
                {
                    lSlapHandActive = false;
                }

                if (lCooldownTime >= LONG_COOLDOWN_TIME)
                {
                    lActive = true;
                    lPenalty = false;
                    lCooldownTime = 0.0f;
                    lFill = PAD_PIXEL_HEIGHT;
                }
            }

            if (barrierMoveOnCooldown)
            {
                barrierMoveCooldownTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                
                if (barrierMoveCooldownTime >= BARRIER_MOVE_COOLDOWN_TIME)
                {
                    barrierMoveOnCooldown = false;
                    barrierMoveCooldownTime = 0.0f;
                }
            }

        }

        /// <summary>
        /// Checks all people to slap!
        /// </summary>
        public void Slap(int xStartPosition, int xEndPosition, ref bool slapHandActive, ref bool penalty, List<Person> people)
        {
            bool normieSlapped = false;
            bool crazySlapped = false;

            for(int i = 0; i < people.Count; i++)
            {
                if (people[i].getCenterX() > xStartPosition && people[i].getCenterX() < xEndPosition)
                {
                    if (people[i].isNoisy())
                        crazySlapped = true;
                    else
                        normieSlapped = true;

                    if (--people[i].lives == 0)
                    {
                        people.RemoveAt(i);
                        i--;
                    } else
                    {
                        people[i].scale--;
                        people[i].position.Y += 200;
                    }
                   
                }
            }

            slapHandActive = true;

            if(normieSlapped && !crazySlapped) //if only normies, no crazies, were slapped
            {
                boingSFX.Play();
                penalty = true;
            }
            else if (crazySlapped) //or if a crazy was slapped
            {
                slapSFX.Play();
            }
            else //or you missed entirely
            {
                whooshSFX.Play();
            }
        }


        /// <summary>
        /// Draws objects
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="gameTime"></param>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //spriteBatch.Draw(graySquare, new Vector2(aPadLocation, PAD_HEIGHT_LOCATION), Color.White);
            //spriteBatch.Draw(graySquare, new Vector2(sPadLocation, PAD_HEIGHT_LOCATION), Color.White);
            //spriteBatch.Draw(graySquare, new Vector2(dPadLocation, PAD_HEIGHT_LOCATION), Color.White);
            //spriteBatch.Draw(graySquare, new Vector2(jPadLocation, PAD_HEIGHT_LOCATION), Color.White);
            //spriteBatch.Draw(graySquare, new Vector2(kPadLocation, PAD_HEIGHT_LOCATION), Color.White);
            //spriteBatch.Draw(graySquare, new Vector2(lPadLocation, PAD_HEIGHT_LOCATION), Color.White);

            //spriteBatch.Draw(whiteSquare, new Rectangle(aPadLocation + PAD_PIXEL_WIDTH, PAD_HEIGHT_LOCATION + PAD_PIXEL_HEIGHT, PAD_PIXEL_WIDTH, aFill), null, Color.White, MathHelper.Pi, new Vector2(0,0), SpriteEffects.None, 1);
            //spriteBatch.Draw(whiteSquare, new Rectangle(sPadLocation + PAD_PIXEL_WIDTH, PAD_HEIGHT_LOCATION + PAD_PIXEL_HEIGHT, PAD_PIXEL_WIDTH, sFill), null, Color.White, MathHelper.Pi, new Vector2(0,0), SpriteEffects.None, 1);
            //spriteBatch.Draw(whiteSquare, new Rectangle(dPadLocation + PAD_PIXEL_WIDTH, PAD_HEIGHT_LOCATION + PAD_PIXEL_HEIGHT, PAD_PIXEL_WIDTH, dFill), null, Color.White, MathHelper.Pi, new Vector2(0,0), SpriteEffects.None, 1);
            //spriteBatch.Draw(whiteSquare, new Rectangle(jPadLocation + PAD_PIXEL_WIDTH, PAD_HEIGHT_LOCATION + PAD_PIXEL_HEIGHT, PAD_PIXEL_WIDTH, jFill), null, Color.White, MathHelper.Pi, new Vector2(0,0), SpriteEffects.None, 1);
            //spriteBatch.Draw(whiteSquare, new Rectangle(kPadLocation + PAD_PIXEL_WIDTH, PAD_HEIGHT_LOCATION + PAD_PIXEL_HEIGHT, PAD_PIXEL_WIDTH, kFill), null, Color.White, MathHelper.Pi, new Vector2(0,0), SpriteEffects.None, 1);
            //spriteBatch.Draw(whiteSquare, new Rectangle(lPadLocation + PAD_PIXEL_WIDTH, PAD_HEIGHT_LOCATION + PAD_PIXEL_HEIGHT, PAD_PIXEL_WIDTH, lFill), null, Color.White, MathHelper.Pi, new Vector2(0,0), SpriteEffects.None, 1);

            if (aActive)
                spriteBatch.Draw(aSlapper, new Vector2(aPadLocation, PAD_HEIGHT_LOCATION), Color.White);
            else
                spriteBatch.Draw(aSlapper2, new Vector2(aPadLocation, PAD_HEIGHT_LOCATION), Color.White);

            if (sActive)
                spriteBatch.Draw(sSlapper, new Vector2(sPadLocation, PAD_HEIGHT_LOCATION), Color.White);
            else
                spriteBatch.Draw(sSlapper2, new Vector2(sPadLocation, PAD_HEIGHT_LOCATION), Color.White);

            if (dActive)
                spriteBatch.Draw(dSlapper, new Vector2(dPadLocation, PAD_HEIGHT_LOCATION), Color.White);
            else
                spriteBatch.Draw(dSlapper2, new Vector2(dPadLocation, PAD_HEIGHT_LOCATION), Color.White);

            if (jActive)
                spriteBatch.Draw(jSlapper, new Vector2(jPadLocation, PAD_HEIGHT_LOCATION), Color.White);
            else
                spriteBatch.Draw(jSlapper2, new Vector2(jPadLocation, PAD_HEIGHT_LOCATION), Color.White);

            if (kActive)
                spriteBatch.Draw(kSlapper, new Vector2(kPadLocation, PAD_HEIGHT_LOCATION), Color.White);
            else
                spriteBatch.Draw(kSlapper2, new Vector2(kPadLocation, PAD_HEIGHT_LOCATION), Color.White);

            if (lActive)
                spriteBatch.Draw(lSlapper, new Vector2(lPadLocation, PAD_HEIGHT_LOCATION), Color.White);
            else
                spriteBatch.Draw(lSlapper2, new Vector2(lPadLocation, PAD_HEIGHT_LOCATION), Color.White);


            if (aSlapHandActive)
                spriteBatch.Draw(slapHand, new Vector2(aPadLocation, SLAP_HEIGHT_LOCATION), Color.White);
            if (sSlapHandActive)
                spriteBatch.Draw(slapHand, new Vector2(sPadLocation, SLAP_HEIGHT_LOCATION), Color.White);
            if (dSlapHandActive)
                spriteBatch.Draw(slapHand, new Vector2(dPadLocation, SLAP_HEIGHT_LOCATION), Color.White);
            if (jSlapHandActive)
                spriteBatch.Draw(slapHand, new Vector2(jPadLocation, SLAP_HEIGHT_LOCATION), Color.White);
            if (kSlapHandActive)
                spriteBatch.Draw(slapHand, new Vector2(kPadLocation, SLAP_HEIGHT_LOCATION), Color.White);
            if (lSlapHandActive)
                spriteBatch.Draw(slapHand, new Vector2(lPadLocation, SLAP_HEIGHT_LOCATION), Color.White);


            //spriteBatch.DrawString(letterFont, "a", new Vector2(aPadLocation + (PAD_PIXEL_WIDTH / 2), PAD_HEIGHT_LOCATION + (PAD_PIXEL_HEIGHT / 2)), Color.Black, 0, aSize * 0.5f, 1, SpriteEffects.None, 0);
            //spriteBatch.DrawString(letterFont, "s", new Vector2(sPadLocation + (PAD_PIXEL_WIDTH / 2), PAD_HEIGHT_LOCATION + (PAD_PIXEL_HEIGHT / 2)), Color.Black, 0, sSize * 0.5f, 1, SpriteEffects.None, 0);
            //spriteBatch.DrawString(letterFont, "d", new Vector2(dPadLocation + (PAD_PIXEL_WIDTH / 2), PAD_HEIGHT_LOCATION + (PAD_PIXEL_HEIGHT / 2)), Color.Black, 0, dSize * 0.5f, 1, SpriteEffects.None, 0);
            //spriteBatch.DrawString(letterFont, "j", new Vector2(jPadLocation + (PAD_PIXEL_WIDTH / 2), PAD_HEIGHT_LOCATION + (PAD_PIXEL_HEIGHT / 2)), Color.Black, 0, jSize * 0.5f, 1, SpriteEffects.None, 0);
            //spriteBatch.DrawString(letterFont, "k", new Vector2(kPadLocation + (PAD_PIXEL_WIDTH / 2), PAD_HEIGHT_LOCATION + (PAD_PIXEL_HEIGHT / 2)), Color.Black, 0, kSize * 0.5f, 1, SpriteEffects.None, 0);
            //spriteBatch.DrawString(letterFont, "l", new Vector2(lPadLocation + (PAD_PIXEL_WIDTH / 2), PAD_HEIGHT_LOCATION + (PAD_PIXEL_HEIGHT / 2)), Color.Black, 0, lSize * 0.5f, 1, SpriteEffects.None, 0);

        }

    }


}
