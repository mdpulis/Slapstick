﻿using Microsoft.Xna.Framework;
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
    public class PersonManager
    {
        private const int MAX_NOISIES_PER_SIDE = 5;
        private const int MAX_GIGAS = 1;
        private const int TIME_UNTIL_FIRST_GIGA = 10;
        private double firstGigaTimer = 0;
        public List<Person> people = new List<Person>();

        double personTimer;
        Vector2 zeroVector = new Vector2(0, 0);
        Direction direction;
        bool isNoisy, isGiga;
        Texture2D tex, noisy1, normy1, noisy2, normy2, noisy3, normy3, noisy4, normy4, noisy5, normy5, noisy6, normy6, noisy7, normy7, 
            normy_n1, normy_n2, normy_n3, normy_n4, normy_n5, papa;
        System.Random random = new System.Random();
        int texNum, peopleIndexToDelete;

        public Person makePerson(GraphicsDeviceManager gdm, int bpm)
        {
            int leftNoisies = people.Where(x => x.isNoisy() && x.getDirection() == Direction.left).Count();
            int rightNoisies = people.Where(x => x.isNoisy() && x.getDirection() == Direction.right).Count();

            Person p = new Person();
            direction = random.Next(2) == 1 ? Direction.right : Direction.left;
            isNoisy = random.Next(2) == 1 ? true : false;
            isGiga = random.Next(20) == 1 && firstGigaTimer > 10 && isNoisy ? true : false;
            if(isNoisy)
            {
                if(direction == Direction.left)
                {
                    if (people.Where(x => x.isNoisy() && x.getDirection() == Direction.left).Sum(x => x.GetPersonSpawnWeight()) > MAX_NOISIES_PER_SIDE)
                        isNoisy = false;
                }
                else if (direction == Direction.right)
                {
                    if (people.Where(x => x.isNoisy() && x.getDirection() == Direction.right).Sum(x => x.GetPersonSpawnWeight()) > MAX_NOISIES_PER_SIDE)
                        isNoisy = false;
                }
            }
            if (isGiga)
            {
                if (people.Where(x => x.isGiga()).Count() >= MAX_GIGAS)
                    isGiga = false;
            }


            //texNum = random.Next(7) + 1;
            //switch (texNum)
            //{
            //    case 1:
            //        tex = isNoisy ? papa : normy1;
            //        break;
            //    case 2:
            //        tex = isNoisy ? papa : normy2;
            //        break;
            //    case 3:
            //        tex = isNoisy ? papa : normy3;
            //        break;
            //    case 4:
            //        tex = isNoisy ? papa : normy4;
            //        break;
            //    case 5:
            //        tex = isNoisy ? papa : normy5;
            //        break;
            //    case 6:
            //        tex = isNoisy ? papa : normy6;
            //        break;
            //    case 7:
            //        tex = isNoisy ? papa : normy7;
            //        break;
            //}

            texNum = random.Next(5) + 1;
            switch (texNum)
            {
                case 1:
                    tex = isNoisy ? papa : normy_n1;
                    break;
                case 2:
                    tex = isNoisy ? papa : normy_n2;
                    break;
                case 3:
                    tex = isNoisy ? papa : normy_n3;
                    break;
                case 4:
                    tex = isNoisy ? papa : normy_n4;
                    break;
                case 5:
                    tex = isNoisy ? papa : normy_n5;
                    break;
            }

            p.Initialize(direction, isNoisy, isGiga, random.Next(5) * 5 + bpm, tex, gdm); //Third value is speed, increases as BPM increases. 4th value is unused but could be used for different characters
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

            normy_n1 = Content.Load<Texture2D>("Images/Normie_SpriteSheet_New1");
            normy_n2 = Content.Load<Texture2D>("Images/Normie_SpriteSheet_New2");
            normy_n3 = Content.Load<Texture2D>("Images/Normie_SpriteSheet_New3");
            normy_n4 = Content.Load<Texture2D>("Images/Normie_SpriteSheet_New4");
            normy_n5 = Content.Load<Texture2D>("Images/Normie_SpriteSheet_New5");

            papa = Content.Load<Texture2D>("Images/Paparazzi_SpriteSheet");
        }

        public void update(GameTime gameTime, GraphicsDeviceManager graphics, UI gameUI, Celeb celeb) {
            personTimer += gameTime.ElapsedGameTime.TotalSeconds;
            firstGigaTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if ((GameState.BeatsPerMinute >= 260 && personTimer >= 0.4f) || (GameState.BeatsPerMinute < 260 && personTimer >= 3 - GameState.BeatsPerMinute * 1.0 / 100))
            {
                Person newPerson = makePerson(graphics, GameState.BeatsPerMinute);
                people.Add(makePerson(graphics, GameState.BeatsPerMinute));
                personTimer = 0;
            }

            peopleIndexToDelete = -1;
            foreach (Person p in people)
            {
                p.Update(gameTime);
                if (p.position.X < 0 || p.position.X > 1920)
                {
                    peopleIndexToDelete = people.IndexOf(p);
                }
                if ((p.getCenterX() >= celeb.position.X) && (p.getCenterX() <= celeb.getCenterX() + celeb.texture.Width / 12 / 2))
                {
                    peopleIndexToDelete = people.IndexOf(p);
                    celeb.collision(p.isNoisy());
                }
            }
            if (peopleIndexToDelete != -1)
            {
                people.RemoveAt(peopleIndexToDelete);
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            foreach (Person p in people)
            {
                    spriteBatch.Draw(p.texture, p.position, p.currentFrame, Color.White, 0.0f, zeroVector, p.scale, p.spriteEffects, 0.0f);
            }
        }
    }
}
