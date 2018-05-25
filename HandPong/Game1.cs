using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HandPong
{    
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D ballSprite;
        Vector2 ballPosition = Vector2.Zero;
        Vector2 ballSpeed = new Vector2(150, 150);

		Texture2D paddleSprite;
		Vector2 paddlePosition;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
			IsMouseVisible = true;

			paddlePosition = new Vector2(
				graphics.GraphicsDevice.Viewport.Width / 2 - paddleSprite.Width / 2,
				graphics.GraphicsDevice.Viewport.Height - paddleSprite.Height); 
        }
		  
        protected override void LoadContent()
        {            
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
			ballSprite = Content.Load<Texture2D>("basketball");
			paddleSprite = Content.Load<Texture2D>("hand");
        }

        protected override void UnloadContent()
        {
        
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

			ballPosition += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

			int maxX = GraphicsDevice.Viewport.Width - ballSprite.Width;
			int maxY = GraphicsDevice.Viewport.Height - ballSprite.Height;

			if (ballPosition.X > maxX || ballPosition.X < 0)
			{
				ballSpeed.X = ballSpeed.X * 1;
			}

			if (ballPosition.Y > maxY || ballPosition.Y < 0)
			{
				ballSpeed.Y = ballSpeed.Y * -1;
			}
			
			KeyboardState keyState = Keyboard.GetState();
			if (keyState.IsKeyDown(Keys.Right))
				paddlePosition.X += 5;
			else if (keyState.IsKeyDown(Keys.Left))
				paddlePosition.X -= 5;
			
			if (ballPosition.X > maxX || ballPosition.X < 0)
				ballSpeed.X *= -1;

			if (ballPosition.Y < 0)
			{
				ballSpeed.Y *= -1;
			}
			else if (ballPosition.Y > maxY)
			{
				ballPosition.Y = 0;
				ballSpeed.X = 150;
				ballSpeed.Y  = 150;
			}

			Rectangle ballRect = new Rectangle((int)ballPosition.X, (int)ballPosition.Y,
			                                   ballSprite.Width, ballSprite.Height);

			Rectangle handRect = new Rectangle((int)paddlePosition.X, (int)paddlePosition.Y,
											   paddleSprite.Width, paddleSprite.Height);
			if (ballRect.Intersects(handRect))
			{
				ballSpeed.Y += 50;

				if (ballSpeed.X < 0)
					ballSpeed.X -= 50;
				else
					ballSpeed.Y += 50;
				
				ballSpeed.Y *= -1;
			}

			base.Update(gameTime);
        }
		        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(ballSprite, ballPosition, Color.White);
			spriteBatch.Draw(paddleSprite, paddlePosition, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
