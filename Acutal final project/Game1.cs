using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Actual_final_project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        
        Texture2D mouseTexture;
        Rectangle mouseRect;
        MouseState mouseState;
        MouseState prevMouseState;

        
        Rectangle window;
        Random random = new Random();
        List<Targets> targets = new List<Targets>();

        
        Texture2D targetTexture;
        Texture2D panelTexture;
        Texture2D buttonTexture;

        
        Rectangle panelRect;
        Rectangle button1; // target spawn cap
        Rectangle button2; // target spawn speed
        Rectangle button3; // points per  target

        
        SoundEffect gunShot;

        
        SpriteFont font;

        
        int score = 0;
        int pointsPerClick = 1;

        int maxTargets = 5;
        float spawnDelay = 2f;
        float spawnTimer = 0f;

        int costTargets = 25;
        int costSpeed = 25;
        int costPoints = 25;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            window = new Rectangle(0, 0, 900, 600);

            graphics.PreferredBackBufferWidth = window.Width;
            graphics.PreferredBackBufferHeight = window.Height;
            graphics.ApplyChanges();

            mouseRect = new Rectangle(0, 0, 20, 20);

            panelRect = new Rectangle(600, 0, 300, 600);

            button1 = new Rectangle(650, 25, 200, 40);
            button2 = new Rectangle(650, 80, 200, 40);
            button3 = new Rectangle(650, 135, 200, 40);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            
            mouseTexture = Content.Load<Texture2D>("pngwing.com");
            targetTexture = Content.Load<Texture2D>("enemy");
            panelTexture = Content.Load<Texture2D>("red");
            buttonTexture = Content.Load<Texture2D>("dark red");

            font = Content.Load<SpriteFont>("File");
            gunShot = Content.Load<SoundEffect>("gunshot");

            for (int i = 0; i < 3; i++)
                SpawnTarget();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            prevMouseState = mouseState;
            mouseState = Mouse.GetState();

            mouseRect.Location = mouseState.Position;

            // target spawn
            spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (spawnTimer >= spawnDelay && targets.Count < maxTargets)
            {
                SpawnTarget();
                spawnTimer = 0f;
            }

            // update targets
            for (int i = 0; i < targets.Count; i++)
            {
                targets[i].Move(window, panelRect);

                if (mouseState.LeftButton == ButtonState.Pressed &&
                    prevMouseState.LeftButton == ButtonState.Released &&
                    targets[i].Bounds.Contains(mouseRect))
                {
                    targets.RemoveAt(i);
                    i--;
                    score += pointsPerClick;
                }
            }

            // upgrades
            if (mouseState.LeftButton == ButtonState.Pressed &&
                prevMouseState.LeftButton == ButtonState.Released)
            {
                gunShot?.Play();

                if (button1.Contains(mouseRect) && score >= costTargets)
                {
                    score -= costTargets;
                    maxTargets++;
                    costTargets += 25;
                }

                if (button2.Contains(mouseRect) && score >= costSpeed)
                {
                    score -= costSpeed;

                    if (spawnDelay > 0.5f)
                        spawnDelay -= 0.2f;

                    costSpeed += 25;
                }

                if (button3.Contains(mouseRect) && score >= costPoints)
                {
                    score -= costPoints;
                    pointsPerClick++;
                    costPoints += 25;
                }
            }

            base.Update(gameTime);
        }

        private void SpawnTarget()
        {
            if (targetTexture == null)
                return;

            int x = random.Next(0, 550);
            int y = random.Next(0, 550);

            Rectangle rect = new Rectangle(x, y, 50, 50);

            Vector2 speed = new Vector2(
                random.Next(-4, 5),
                random.Next(-4, 5)
            );

            targets.Add(new Targets(targetTexture, rect, speed));
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            foreach (var t in targets)
                spriteBatch.Draw(t.Texture, t.Bounds, Color.White);

            spriteBatch.Draw(panelTexture, panelRect, Color.White);

            spriteBatch.Draw(buttonTexture, button1, Color.White);
            spriteBatch.Draw(buttonTexture, button2, Color.White);
            spriteBatch.Draw(buttonTexture, button3, Color.White);

            spriteBatch.DrawString(font, "Score: " + score, new Vector2(10, 10), Color.Black);

            spriteBatch.DrawString(font, "Targets: " + maxTargets, new Vector2(530, 10), Color.White);
            spriteBatch.DrawString(font, "Spawn: " + spawnDelay.ToString("0.0"), new Vector2(530, 30), Color.White);


            spriteBatch.DrawString(font,"UPGRADES",new Vector2(650, 0),Color.Yellow);

            spriteBatch.DrawString(font,"More Targets ($" + costTargets + ")", new Vector2(660, 25), Color.White);

            spriteBatch.DrawString(font,"Increases max enemies", new Vector2(660, 45), Color.Gray);

            spriteBatch.DrawString(font,"Faster Spawn ($" + costSpeed + ")", new Vector2(660, 80), Color.White);

            spriteBatch.DrawString(font,"Enemies spawn faster", new Vector2(660, 100), Color.Gray);

            
            spriteBatch.DrawString(font,"More Points ($" + costPoints + ")", new Vector2(660, 135), Color.White);

            spriteBatch.DrawString(font,"More score per click", new Vector2(660, 155), Color.Gray);

            spriteBatch.Draw(mouseTexture, mouseRect, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}