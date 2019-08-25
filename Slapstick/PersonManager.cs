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
        Texture2D tex, noisy1, normy1, noisy2, normy2, noisy3, normy3, noisy4, normy4, noisy5, normy5, noisy6, normy6, noisy7, normy7;
        System.Random random = new System.Random();
        int texNum;
        public Person makePerson(GraphicsDeviceManager gdm, int bpm)
        {
            Person p = new Person();
            direction = random.Next(2) == 1 ? Direction.right : Direction.left;
            isNoisy = random.Next(2) == 1 ? true : false;
            texNum = random.Next(7) + 1;
            switch (texNum)
            {
                case 1:
                    tex = isNoisy ? noisy1 : normy1;
                    break;
                case 2:
                    tex = isNoisy ? noisy2 : normy2;
                    break;
                case 3:
                    tex = isNoisy ? noisy3 : normy3;
                    break;
                case 4:
                    tex = isNoisy ? noisy4 : normy4;
                    break;
                case 5:
                    tex = isNoisy ? noisy5 : normy5;
                    break;
                case 6:
                    tex = isNoisy ? noisy6 : normy6;
                    break;
                case 7:
                    tex = isNoisy ? noisy7 : normy7;
                    break;
            }
            p.Initialize(direction, isNoisy, random.Next(5) * 5 + bpm, tex, gdm); //Third value is speed, increases as BPM increases. 4th value is unused but could be used for different characters
            return p;
        }
        public void LoadContent(ContentManager Content)
        {
            normy1 = Content.Load<Texture2D>("Images/Normie_SpriteSheet1");
            noisy1 = Content.Load<Texture2D>("Images/Crazy_SpriteSheet1");
            normy2 = Content.Load<Texture2D>("Images/Normie_SpriteSheet2");
            noisy2 = Content.Load<Texture2D>("Images/Crazy_SpriteSheet2");
            normy3 = Content.Load<Texture2D>("Images/Normie_SpriteSheet3");
            noisy3 = Content.Load<Texture2D>("Images/Crazy_SpriteSheet3");
            normy4 = Content.Load<Texture2D>("Images/Normie_SpriteSheet4");
            noisy4 = Content.Load<Texture2D>("Images/Crazy_SpriteSheet4");
            normy5 = Content.Load<Texture2D>("Images/Normie_SpriteSheet5");
            noisy5 = Content.Load<Texture2D>("Images/Crazy_SpriteSheet5");
            normy6 = Content.Load<Texture2D>("Images/Normie_SpriteSheet6");
            noisy6 = Content.Load<Texture2D>("Images/Crazy_SpriteSheet6");
            normy7 = Content.Load<Texture2D>("Images/Normie_SpriteSheet7");
            noisy7 = Content.Load<Texture2D>("Images/Crazy_SpriteSheet7");
        }
    }
}
