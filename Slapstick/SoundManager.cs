using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slapstick
{
    public class SoundManager
    {
        private SoundEffect beat;
        private Song crowdAmbiance;
        private double beatTimer;



        public void LoadContent(ContentManager Content)
        {
            beat = Content.Load<SoundEffect>("Sounds/Metronome_Beat");
            crowdAmbiance = Content.Load<Song>("Sounds/crowd_ambiance");

            MediaPlayer.Play(crowdAmbiance);
            MediaPlayer.IsRepeating = true; //puts the media player on repeat
        }


        public void Update(GameTime gameTime, UI gameUI)
        {
            beatTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if(beatTimer >= 60.0 / GameState.BeatsPerMinute)
            {
                beat.Play();
                beatTimer = 0;
            }
        }
    }
}
