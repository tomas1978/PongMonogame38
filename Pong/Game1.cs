using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D pixel;
        Rectangle ball = new Rectangle(100, 100, 20, 20);
        Rectangle left_paddle = new Rectangle(10, 150, 20, 150);
        Rectangle right_paddle = new Rectangle(770, 150, 20, 150);

        int x_speed = 2;
        int y_speed = 2;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            pixel = Content.Load<Texture2D>("pixel");

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            ball.X += x_speed;
            ball.Y += y_speed;

            //Om bollen träffar nedre eller övre kanten så byt riktning i y-led
            if (ball.Y < 0 || ball.Y > Window.ClientBounds.Height - ball.Height)
                y_speed *= -1;
            //Om bollen träffar en paddel så byt riktning i x-led
            if (ball.Intersects(left_paddle) || ball.Intersects(right_paddle))
                x_speed *= -1;
            //Om bollen kommer utanför skärmen i x-led så avsluta spelet
            if (ball.X < 0 || ball.X > Window.ClientBounds.Width - ball.Width)
                Exit();
            KeyboardState kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.Up))
                right_paddle.Y -= 5;
            if (kstate.IsKeyDown(Keys.Down))
                right_paddle.Y += 5;
            if (kstate.IsKeyDown(Keys.W))
                left_paddle.Y -= 5;
            if (kstate.IsKeyDown(Keys.S))
                left_paddle.Y += 5;

            //Kontrollera om någon paddel är på väg att flytta utanför skärmen
            if (left_paddle.Y < 0)
                left_paddle.Y = 0;
            if (left_paddle.Y > Window.ClientBounds.Height - left_paddle.Height)
                left_paddle.Y = Window.ClientBounds.Height - left_paddle.Height;
            if (right_paddle.Y < 0)
                right_paddle.Y = 0;
            if (right_paddle.Y > Window.ClientBounds.Height - right_paddle.Height)
                right_paddle.Y = Window.ClientBounds.Height - right_paddle.Height;



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(pixel, ball, Color.Green);
            _spriteBatch.Draw(pixel, left_paddle, Color.Green);
            _spriteBatch.Draw(pixel, right_paddle, Color.Green);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
