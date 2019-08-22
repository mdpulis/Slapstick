using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slapstick
{

    public class UI
    {

        public string score;
        private int points;
        public int beatsPerMinute = 80;
        public const int maxBeatsPerMinute = 240;

        public UI()
        {
            score = "000";
            points = 0;
        }

        public void addScore(int scoreToAdd)
        {
            score = score + scoreToAdd;
        }

        public void penalize(int penalty)
        {
            //score = score - penalty;
        }

        public void addBPM()
        {
            beatsPerMinute = beatsPerMinute + 20;
        } 

    }
}
