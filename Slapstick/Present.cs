﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slapstick
{
    public class Present
    {
        public Texture2D texture;
        public Vector2 position;
        public double speed = 50;
        public int letterNumber = 0;
        static Random random = new Random();
        public void Initialize(Texture2D tex)
        {
            texture = tex;
            letterNumber = random.Next(0, 25);
        }
        public void Update(GameTime gameTime)
        {
            position.Y += (float)speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        public void Draw()
        {

        }
    }
}