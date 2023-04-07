using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace HrumGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;

        Texture2D playerSprite;
        Texture2D backgroundSprite;
        Texture2D targetSprite;

        Rectangle spriteRectangle;

        public Matrix transform;

        Vector2 distance;
        Vector2 camera;

        Vector2 spriteOrigin;

        Vector2 spritePosition;
        float rotation;

        Vector2 spriteVelocity;
        const float tangentialVelocity = 5f;
        float friction = 0.1f;

        // Mouse
        MouseState prevMouseState;
        MouseState MouseState;

        // Bullets
        List<Bullets> bullets = new List<Bullets>();
        KeyboardState pastKey;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            playerSprite = Content.Load<Texture2D>(@"2d\player");
            targetSprite = Content.Load<Texture2D>(@"2d\target");
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


            if (Keyboard.GetState().IsKeyDown(Keys.D))
                spritePosition.X += 3;

            if (Keyboard.GetState().IsKeyDown(Keys.A))
                spritePosition.X -= 3;

            if (Keyboard.GetState().IsKeyDown(Keys.S))
                spritePosition.Y += 3;

            if (Keyboard.GetState().IsKeyDown(Keys.W))
                spritePosition.Y -= 3;


            MouseState mState = Mouse.GetState();
            IsMouseVisible = true;

            distance.X = mState.X - spritePosition.X;
            distance.Y = mState.Y - spritePosition.Y;

            rotation = (float)(Math.Atan2(distance.Y, distance.X) - Math.PI / 2);

            prevMouseState = MouseState;
            MouseState = Mouse.GetState();

            if (MouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                Shoot();

            pastKey = Keyboard.GetState();

            UpdateBullets();
            base.Update(gameTime);
        }

        public void UpdateBullets()
        {
            foreach (Bullets bullet in bullets)
            {
                bullet.position += bullet.velocity;
                if (Vector2.Distance(bullet.position, spritePosition) > 500)
                    bullet.isVisible = false;
            }
            for  (int i = 0; i < bullets.Count; i++)
            {
                if (!bullets[i].isVisible) 
                {
                    bullets.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Shoot()
        {
            Bullets newBullet = new Bullets(Content.Load<Texture2D>(@"2d\bullet"));
            newBullet.velocity = new Vector2((float)Math.Cos(rotation + Math.PI/2), (float)Math.Sin(rotation + Math.PI / 2)) * 5f;
            newBullet.position = spritePosition;
            newBullet.isVisible = true;
            newBullet.rotation = rotation;
            if (bullets.Count < 20)
                bullets.Add(newBullet);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            spriteBatch.Begin(SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                null, null, null, null);

            foreach (Bullets bullet in bullets)
                bullet.Draw(spriteBatch);
            Mouse.SetCursor(MouseCursor.FromTexture2D(targetSprite, 20, 20));


            spriteBatch.Draw(playerSprite, spritePosition, null,Color.White,rotation, spriteOrigin , 1f, SpriteEffects.None,0);
            spriteBatch.End();
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