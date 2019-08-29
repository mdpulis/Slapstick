﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slapstick
{
    /// <summary>
    /// Handles things related to gameplay for this specific run
    /// </summary>
    public class GameplayManager
    {

        private double elapsedGameTime = 0.0f;
        private double bpmIncreaseTimer = 0.0f;

        private SoundEffect tiresSquealingSFX;


        public void LoadContent(ContentManager Content)
        {
            tiresSquealingSFX = Content.Load<SoundEffect>("Sounds/tires_squealing");
        }

        public void Update(GameTime gameTime)
        {
            elapsedGameTime += gameTime.ElapsedGameTime.TotalSeconds;
            bpmIncreaseTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (bpmIncreaseTimer >= 20)
            {
                GameState.AddBPM();
                bpmIncreaseTimer = 0;
            }

            if (GameState.Lives == 0)
            {
                tiresSquealingSFX.Play();
                GameState.ProgressGameplayState();
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
        }



        /// <summary>
        /// Gets the elapsed game time for this run
        /// </summary>
        public double GetElapsedGameTime()
        {
            return elapsedGameTime;
        }

        /// <summary>
        /// Gets the current BPM increase timer value
        /// </summary>
        public double GetBPMIncreaseTimer()
        {
            return bpmIncreaseTimer;
        }

        /// <summary>
        /// Reset the gameplay variables
        /// </summary>
        public void ResetGameplayVariables()
        {
            elapsedGameTime = 0.0f;
            bpmIncreaseTimer = 0.0f;
        }


    }
}
