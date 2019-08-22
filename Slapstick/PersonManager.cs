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
    public enum Direction { right, left };
    class PersonManager
    {
        Direction direction;
        bool isNoisy;
        Texture2D tex, noisy, normy;
        System.Random random = new System.Random();
        public Person makePerson(GraphicsDeviceManager gdm, int bpm)
        {
            Person p = new Person();
            direction = random.Next(2) == 1 ? Direction.right : Direction.left;
            isNoisy = random.Next(2) == 1 ? true : false;
            tex = isNoisy ? noisy : normy;
            p.Initialize(direction, isNoisy, random.Next(5) * 5 + bpm, 0, tex, gdm);
            return p;
        }
        public void LoadContent(ContentManager Content)
        {
            noisy = Content.Load<Texture2D>("Images/shuttle");
            normy = Content.Load<Texture2D>("Images/ball");
        }
    }
}
