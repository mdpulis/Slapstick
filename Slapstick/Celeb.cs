using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slapstick
{
    public class Celeb
    {
        public Texture2D texture;
        public Vector2 position;

        private SoundEffect clearThroatSFX;
        private SoundEffect cameraSFX;

        private Texture2D heart;
        private Texture2D anger;

        private Texture2D heartsFX;
        private Texture2D angerFX;

        private int emoteWidthDifference = 0;

        private double animTimer;

        private Rectangle celebCurrentFrame;
        private Rectangle[] celebFrames = new Rectangle[180];
        private int celebFrameCounter = 0;

        private Rectangle heartsCurrentFrame;
        private Rectangle[] heartsFrames = new Rectangle[30];
        private int heartsFrameCounter = 0;

        private Rectangle angerCurrentFrame;
        private Rectangle[] angerFrames = new Rectangle[30];
        private int angerFrameCounter = 0;

        private float frameTime = .05f;
        private Vector2 zeroVector = new Vector2(0,0);
        public int scale = 1;

        private int emoteScale = 2;

        private const int HEALTH_HEIGHT_ABOVE_CELEB = 100;
        private const int CELEB_FRAME_WIDTH = 175;
        private const int CELEB_FRAME_HEIGHT = 175;

        private const int EMOTE_FRAME_WIDTH = 175;
        private const int EMOTE_FRAME_HEIGHT = 175;

        public Celeb()
        {

        }

        public void Initialize(GraphicsDeviceManager gdm)
        {
            position = new Vector2((gdm.PreferredBackBufferWidth - 150) / 2,
                           700);

            int count = 0;

            count = 0;
            for(int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    celebFrames[count] = new Rectangle(CELEB_FRAME_WIDTH * i, CELEB_FRAME_HEIGHT * j, CELEB_FRAME_WIDTH, CELEB_FRAME_HEIGHT);
                    count++;
                }
            }
            celebCurrentFrame = celebFrames[celebFrameCounter];

            count = 0;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    heartsFrames[count] = new Rectangle(EMOTE_FRAME_WIDTH * i, EMOTE_FRAME_HEIGHT * j, EMOTE_FRAME_WIDTH, EMOTE_FRAME_HEIGHT);
                    count++;
                }
            }
            heartsCurrentFrame = heartsFrames[heartsFrameCounter];

            count = 0;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    angerFrames[count] = new Rectangle(EMOTE_FRAME_WIDTH * i, EMOTE_FRAME_HEIGHT * j, EMOTE_FRAME_WIDTH, EMOTE_FRAME_HEIGHT);
                    count++;
                }
            }
            angerCurrentFrame = angerFrames[angerFrameCounter];

        }

        public void celebUpdate(GameTime gameTime)
        {
            animTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (animTimer > frameTime) //every 1/30 seconds for normals, 1/60 for gigas
            {
                celebFrameCounter++;
                if (celebFrameCounter >= 180)
                {
                    celebFrameCounter = 0;
                }
                celebCurrentFrame = celebFrames[celebFrameCounter];

                heartsFrameCounter++;
                if (heartsFrameCounter >= 30)
                {
                    heartsFrameCounter = 0;
                }
                heartsCurrentFrame = heartsFrames[heartsFrameCounter];

                angerFrameCounter++;
                if (angerFrameCounter >= 30)
                {
                    angerFrameCounter = 0;
                }
                angerCurrentFrame = angerFrames[angerFrameCounter];

                animTimer = 0;
            }
            
        }

        public void LoadContent(ContentManager Content)
        {
            clearThroatSFX = Content.Load<SoundEffect>("Sounds/clear_throat");
            cameraSFX = Content.Load<SoundEffect>("Sounds/camera");

            texture = Content.Load<Texture2D>("Images/CelebWave");
            heart = Content.Load<Texture2D>("Images/heart");
            anger = Content.Load<Texture2D>("Images/anger");

            heartsFX = Content.Load<Texture2D>("Images/hearts_fx");
            angerFX = Content.Load<Texture2D>("Images/anger_fx");

            emoteWidthDifference = (int)(EMOTE_FRAME_WIDTH * emoteScale * 1.5f);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Texture2D life1Texture = GameState.Lives >= 1 ? heartsFX : angerFX;
            Texture2D life2Texture = GameState.Lives >= 2 ? heartsFX : angerFX;
            Texture2D life3Texture = GameState.Lives >= 3 ? heartsFX : angerFX;

            spriteBatch.Draw(texture, position, celebCurrentFrame, Color.White, 0.0f, zeroVector, scale, SpriteEffects.None, 0.0f);

            spriteBatch.Draw(life3Texture, new Vector2(position.X + (CELEB_FRAME_WIDTH / 2) - (EMOTE_FRAME_WIDTH * emoteScale / 2) - 100, position.Y - HEALTH_HEIGHT_ABOVE_CELEB), heartsCurrentFrame, Color.White, 0.0f, zeroVector, emoteScale, SpriteEffects.None, 0.0f);
            spriteBatch.Draw(life2Texture, new Vector2(position.X + (CELEB_FRAME_WIDTH / 2) - (EMOTE_FRAME_WIDTH * emoteScale / 2), position.Y - HEALTH_HEIGHT_ABOVE_CELEB), heartsCurrentFrame, Color.White, 0.0f, zeroVector, emoteScale, SpriteEffects.None, 0.0f);
            spriteBatch.Draw(life1Texture, new Vector2(position.X + (CELEB_FRAME_WIDTH / 2) - (EMOTE_FRAME_WIDTH * emoteScale / 2) + 100, position.Y - HEALTH_HEIGHT_ABOVE_CELEB), heartsCurrentFrame, Color.White, 0.0f, zeroVector, emoteScale, SpriteEffects.None, 0.0f);
        }

        public void collision(bool isNoisy)
        {
            if (isNoisy)
            {
                GameState.Lives--;
                clearThroatSFX.Play();
                cameraSFX.Play();
            }
            else
            {
                GameState.Score += 50;
            }
        }

        public float getCenterX()
        {
            return position.X + (CELEB_FRAME_WIDTH / 2);
        }


    }
}
