using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace pong
{
    class Paddle
    {
        public Texture2D sprite;
        public Vector2 position;
        public Rectangle hitBox;
        private Ball ball;
        private GraphicsDevice graphics;
        public bool isLeft;
        public 
        Paddle(Texture2D sprite, Ball ball, GraphicsDevice graphics, bool isLeft)
        {
            this.sprite = sprite;
            this.ball = ball;
            this.graphics = graphics;
            this.isLeft = isLeft;

            if (isLeft)
            {

                position.X = 30;
                position.Y = graphics.Viewport.Height / 2f;

            }
            else
            {
                position.X = graphics.Viewport.Width - 60;
                position.Y = graphics.Viewport.Height / 2f;

            }

            hitBox = new Rectangle((int)position.X,(int)position.Y, 30, 70);

        }


        public void Update(KeyboardState keyboard)
        {
            #region InputParseing

            if (keyboard.IsKeyDown(Keys.Up) && !isLeft)
            {
                if (keyboard.IsKeyDown(Keys.NumPad0))//TURBOOOO
                {
                    position.Y -= 10;
                }

                position.Y -= 5;

            }

            if (keyboard.IsKeyDown(Keys.Down) && !isLeft)
            {
                if (keyboard.IsKeyDown(Keys.NumPad0))//TURBOOOO
                {
                    position.Y += 10;
                }

                position.Y += 5;
            }

            if (keyboard.IsKeyDown(Keys.W) && isLeft)
            {
                if (keyboard.IsKeyDown(Keys.LeftShift))//TURBOOOO
                {
                    position.Y -= 10;
                }

                position.Y -= 5;

            }

            if (keyboard.IsKeyDown(Keys.S) && isLeft)
            {
                if (keyboard.IsKeyDown(Keys.LeftShift))//TURBOOOO
                {
                    position.Y += 10;
                }

                position.Y += 5;
            }

            #endregion

            #region collisions

            if (position.Y >= graphics.Viewport.Height - 60 )//keep in bounds
            {
                position.Y = 420;
            }
            else if (position.Y <= 0f)
            {
                position.Y = 0f;
            }

            
            if (this.hitBox.Intersects(ball.hitBox))
            {
                ball.hitBy = this;
                ball.isHit = true;
            }

            #endregion

            hitBox.X = (int)position.X;
            hitBox.Y = (int)position.Y;


        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position);

        }
        











    }
}
