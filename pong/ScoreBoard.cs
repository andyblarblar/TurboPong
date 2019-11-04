using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace pong
{
    class ScoreBoard
    {
        private Score leftScore;
        private Score rightScore;
        private Ball ball;
        private GraphicsDevice graphics;

        public ScoreBoard(GraphicsDevice graphics, Game game, Ball ball)
        {
            leftScore = new Score(new Vector2(100,30),graphics,game);
            rightScore = new Score(new Vector2(200, 30), graphics, game);
            this.ball = ball;
            this.graphics = graphics;
        }

        public void Update()
        {
            Random random = new Random();

            if (ball.isInGoal)
            {
                if (ball.position.X >= graphics.Viewport.Width) leftScore.currentScore++;
                else
                {
                    rightScore.currentScore++;
                }

                ball.position = new Vector2(250, random.Next(graphics.Viewport.Height -50));
                ball.rate = new Vector2(-8f, 0f);
                ball.isInGoal = false;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            leftScore.Draw(spriteBatch);
            rightScore.Draw(spriteBatch);

        }




    }
}
