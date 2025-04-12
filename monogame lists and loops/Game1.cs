using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace monogame_lists_and_loops
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Random generator;
        Rectangle window;
        Texture2D spaceBackgroundTexture;
        List<Texture2D> textures;
        List<Rectangle> planetRects;
        List<Texture2D> planetTextures;
        MouseState mouseState;

        Texture2D buttonTexture;
        Rectangle buttonRect;

        float seconds, respondTime;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            window = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            generator = new Random();
            textures = new List<Texture2D>();
            planetRects = new List<Rectangle>();
            planetTextures = new List<Texture2D>();

            seconds = 0f;
            respondTime = 3f;


            for (int i = 0; i < 30; i++)
            {
                planetRects.Add(
                         new Rectangle(generator.Next(window.Width - 25),
                    generator.Next(window.Height - 25), 25, 25)
                    );               
           
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            spaceBackgroundTexture = Content.Load<Texture2D>("Images/space_background");

            for (int i = 1; i < 14; i++) 
            {
                textures.Add(Content.Load<Texture2D>("Images/16-bit-planet/16-bit-planet" + i));
            }
            
            for (int i  = 0; i < planetRects.Count; i++)
            {
                planetTextures.Add(textures[generator.Next(textures.Count)]);
            }

            buttonTexture = Content.Load<Texture2D>("Images/button");
            buttonRect = new Rectangle(20, 300, 100, 100);

            
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (seconds > respondTime)
            {
                seconds = 0f;
            }

            mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                for (int i = 0; i < planetRects.Count; i++)
                {
                    if (planetRects[i].Contains(mouseState.Position))
                    {
                        planetRects.RemoveAt(i);
                        planetTextures.RemoveAt(i);
                        i--;
                    }
                }
            }

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (buttonRect.Contains(mouseState.Position))
                {                   
                    Rectangle newPlanetRect = new Rectangle(
                        generator.Next(window.Width - 25),
                        generator.Next(window.Height - 25),
                        25,
                        25
                    );      
                    Texture2D newPlanetTexture = textures[generator.Next(textures.Count)];                
                    planetRects.Add(newPlanetRect);
                    planetTextures.Add(newPlanetTexture);
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            
            _spriteBatch.Draw(spaceBackgroundTexture, window, Color.White);
            for (int i = 0; i < planetRects.Count; i++)
            {
                _spriteBatch.Draw(textures[0], planetRects[i], Color.White);
            }

            for (int i = 0; i < planetTextures.Count; i++)
                _spriteBatch.Draw(planetTextures[i], planetRects[i], Color.White);


            _spriteBatch.Draw(buttonTexture, buttonRect, Color.White);



            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
