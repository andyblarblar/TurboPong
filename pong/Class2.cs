using System;
using System.Collections.Generic;
using System.Linq;
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

        public Paddle hitBy { get; set; }

        private GraphicsDevice graphics;

        public Ball(Texture2D texture,GraphicsDevice graphics)
        {
            this.texture = texture;
            this.graphics = graphics;

            //set in center
            position.X = graphics.Viewport.Width / 2f;
            position.Y = graphics.Viewport.Width / 2f;

            rate = new Vector2(-10f ,0f);//go left


        }


        public void Update()
        {
            if (!isHit)
            {
                position += rate;

                if (position.Y >= graphics.Viewport.Height)
                {

                    rate.Y *= -1;//invert slope

                }

                if (position.X >= graphics.Viewport.Width)
                {
                    isInGoal = true;

                }

            }
            else//hit
            {

                rate.X *= -1;
                rate.Y = hitBy.position.Y - position.Y;//TODO fix 

            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position);

        }




    }
}
