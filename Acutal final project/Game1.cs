using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;

namespace Acutal_final_project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Vector2 Speed;
        Texture2D mouseTexture;
        Rectangle mouseRect;
        MouseState mouseState, prevMouseState;
        Rectangle window;
        Random generator;
        Texture2D Target;
        Rectangle targetRect;
        Texture2D pannelTexture, buttonTexture;

        Rectangle pannelRect, buttonRect;

        SoundEffect gunShot;

        int Score;

        SpriteFont textFont;

        Texture2D targetTexture;

        List<Targets> targets;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            window = new Rectangle(0, 0, 900, 600);
            mouseRect = new Rectangle(10, 10, 20, 20);
            
            generator = new Random();
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();
            targetRect = new Rectangle(generator.Next(0, window.Width), generator.Next(0, window.Height), 50, 50);
            targets = new List<Targets>();
            Speed = new Vector2(generator.Next(-4, 4), generator.Next(-4, 4));
            textFont = Content.Load<SpriteFont>("File");

            pannelRect = new Rectangle(600, 0, 300, 600);
            buttonRect = new Rectangle(650, 25, 200, 35);

            gunShot = Content.Load<SoundEffect>("gunshot");

            Score = 0;

            for (int i = 0; i < 100; i++)
            {
                int size = generator.Next(40, 100);
                targets.Add(new Targets(targetTexture, new Rectangle(generator.Next(_graphics.PreferredBackBufferWidth - size), generator.Next(_graphics.PreferredBackBufferHeight - size), size, size), new Vector2(generator.Next(-4, 4), generator.Next(-4, 4))));
            }
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            mouseTexture = Content.Load<Texture2D>("pngwing.com");
            targetTexture = Content.Load<Texture2D>("enemy");
            pannelTexture = Content.Load<Texture2D>("red");
            buttonTexture = Content.Load<Texture2D>("dark red");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            prevMouseState = mouseState;
            mouseState = Mouse.GetState();
            mouseRect.Location = mouseState.Position;


            for (int i = 0; i < targets.Count; i++)
            {
                // Move target


                targets[i].Move(window);



                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released && targets[i].Bounds.Contains(mouseRect))
                {
                    targets.RemoveAt(i);
                    i--;

                    Score = Score + 1;
                }
            }

            if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
            {
                gunShot.Play();
            }

            if (mouseState.LeftButton == ButtonState.Pressed || prevMouseState.LeftButton == ButtonState.Released)
                

                // CLICKER ELEMENTS

            

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();


            foreach (Targets target in targets)
            {
                _spriteBatch.Draw(target.Texture, target.Bounds, Color.White);
            }

            _spriteBatch.Draw(mouseTexture, mouseRect, Color.White);

            _spriteBatch.DrawString(textFont, "Score: " + Score, new Vector2(10, 10), Color.Black);
            _spriteBatch.Draw(pannelTexture, pannelRect, Color.White);
            _spriteBatch.Draw(buttonTexture, buttonRect, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
