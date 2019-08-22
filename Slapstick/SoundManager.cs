using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slapstick
{
    public class SoundManager
    {
        SoundEffect beat;
        double beatTimer;
        public void LoadContent(ContentManager Content)
        {
            beat = Content.Load<SoundEffect>("Sounds/Metronome_Beat");
        }

        public void Update(GameTime gameTime, UI gameUI)
        {
            beatTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if(beatTimer >= 60.0 / gameUI.beatsPerMinute)
            {
                beat.Play();
                beatTimer = 0;
            }
        }
    }
}
