using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace HrumGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D playerSprite;
        Texture2D backgroundSprite;

        Rectangle spriteRectangle;

        public Matrix transform;

        Vector2 distance;
        Vector2 spritePosition;
        Vector2 camera;
        Vector2 spriteOrigin;

        float rotation;

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

            playerSprite = Content.Load<Texture2D>(@"2d\player");
            spritePosition = new Vector2(300, 250);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {         
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            spriteRectangle = new Rectangle((int)spritePosition.X, (int)spritePosition.Y,
                playerSprite.Width, playerSprite.Height);
            spriteOrigin = new Vector2(spriteRectangle.Width / 2, spriteRectangle.Height / 2);


            MouseState mState = Mouse.GetState();
            IsMouseVisible = true;

            distance.X = mState.X - spritePosition.X;
            distance.Y = mState.Y - spritePosition.Y;

            rotation = (float)(Math.Atan2(distance.Y, distance.X) - Math.PI / 2);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            _spriteBatch.Begin(SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                null, null, null, null);
            _spriteBatch.Draw(playerSprite, spritePosition, null,Color.White,rotation, spriteOrigin , 1f, SpriteEffects.None,0);
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
        
        static void Main()
        {
            using var game = new HrumGame.Game1();
            game.Run();
        }
    }
}