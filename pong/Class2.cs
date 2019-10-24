﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace pong
{
    class Ball
    {
        private Texture2D texture;

        public Vector2 position;

        public Vector2 rate;

        public bool isHit = false;//true the frame of being hit, false after

        public bool isInGoal = false;

        public Rectangle hitBox;

        /// <summary>
        /// the panel object that last said it was intersecting with the ball
        /// </summary>
        public Paddle hitBy { get; set; }

        private GraphicsDevice graphics;

        public Ball(Texture2D texture,GraphicsDevice graphics)
        {
            this.texture = texture;
            this.graphics = graphics;

            //set in center
            position.X = graphics.Viewport.Width / 2f;
            position.Y = graphics.Viewport.Width / 2f;

            hitBox = new Rectangle((int)position.X,(int)position.Y,10,10);

            rate = new Vector2(-8f ,0f);//go left


        }


        public void Update()
        {

            if (!isHit)
            {
                position += rate; //move

                if (position.Y >= graphics.Viewport.Height || Math.Abs(position.Y) < 20f)//on hitting ceiling
                {

                    rate.Y *= -1;//invert slope

                }

                if (position.X >= graphics.Viewport.Width || position.X <= 0)//offscreen
                {
                    isInGoal = true;

                }

            }
            else//hit
            {
                Console.WriteLine("hit: " + rate.Y);
                rate.X *= -1;

                if (Math.Abs((hitBy.position.Y - position.Y) - 45) > 46)//hit in top third
                {

                    rate.Y-= 2 + (int) ((hitBy.position.Y - position.Y - 45) * 0.1);

                    if (hitBy.position.X > position.X)//is left paddle
                    {
                        position.X -= 10;//shift to avoid being stuck in hitbox
                    }
                    else//is right paddle
                    {
                        position.X += 10;//shift to avoid being stuck in hitbox
                    }

                }

                else if (Math.Abs(hitBy.position.Y - position.Y - 45) < 46 && Math.Abs(hitBy.position.Y - position.Y - 45) > 23)//hit in middle
                {

                    //nothing

                    if (hitBy.position.X > position.X)//is left paddle
                    {
                        position.X -= 10;//shift to avoid being stuck in hitbox
                    }
                    else//is right paddle
                    {
                        position.X += 10;//shift to avoid being stuck in hitbox
                    }

                }

                else if (Math.Abs((hitBy.position.Y - position.Y) - 45) < 23)//hit in bottom
                {

                    rate.Y+=2;

                    if (hitBy.position.X > position.X)//is left paddle
                    {
                        position.X -= 10;//shift to avoid being stuck in hitbox
                    }
                    else//is right paddle
                    {
                        position.X += 10;//shift to avoid being stuck in hitbox
                    }

                }

                isHit = false;//reset

            }

            hitBox.X = (int) position.X;
            hitBox.Y = (int) position.Y;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position);

        }




    }
}