using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Monogame_Lesson_3___Animation
{
    public class Game1 : Game
    {
        Random generator = new Random();
        Texture2D tribbleIntroTexture;
        Texture2D tribbleGreyTexture;
        Rectangle greyTribbleRect;
        Vector2 tribbleGreySpeed;
        Texture2D tribbleBrownTexture;
        Rectangle brownTribbleRect;
        Vector2 tribbleBrownSpeed;
        Texture2D tribbleCreamTexture;
        Rectangle creamTribbleRect;
        Vector2 tribbleCreamSpeed;
        Texture2D tribbleOrangeTexture;
        Rectangle orangeTribbleRect;
        Vector2 tribbleOrangeSpeed;
        Texture2D spaceshipTexture;
        Rectangle spaceshipRect;
        Texture2D spaceship2Texture;
        Rectangle spaceship2Rect;
        int randomX;
        int randomY;
        int randomX2;
        int randomY2;
        int randomX3;
        int randomY3;
        int randomX4;
        int randomY4;
        SoundEffect tribbleCoo;
        SoundEffect music;
        SoundEffectInstance tribbleCooSEI;
        private SpriteFont instructFont;
        SoundEffectInstance musicSEI;
        enum Screen
        {
            Intro,
            TribbleYard,
            End
        }
        Screen screen;
        MouseState mouseState;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();
            this.Window.Title = "Bouncing Tribbles";

            randomX = (generator.Next(0, 700));
            randomY = (generator.Next(0, 500));
            randomX2 = (generator.Next(0, 700));
            randomY2 = (generator.Next(0, 500));
            randomX3 = (generator.Next(0, 700));
            randomY3 = (generator.Next(0, 500));
            randomX4 = (generator.Next(0, 700));
            randomY4 = (generator.Next(0, 500));
            greyTribbleRect = new Rectangle(randomX, randomY, 100, 100);
            tribbleGreySpeed = new Vector2(2, 2);
            brownTribbleRect = new Rectangle(randomX2, randomY2, 100, 100);
            tribbleBrownSpeed = new Vector2(2, 0);
            creamTribbleRect = new Rectangle(randomX3, randomY3, 100, 100);
            tribbleCreamSpeed = new Vector2(0, 2);
            orangeTribbleRect = new Rectangle(randomX4, randomY4, 100, 100);
            tribbleOrangeSpeed = new Vector2(2, 4);
            spaceshipRect = new Rectangle(0, 0, 803, 603);
            spaceship2Rect = new Rectangle(0, 0, 803, 603);
            tribbleCooSEI = tribbleCoo.CreateInstance();
            musicSEI = music.CreateInstance();
            musicSEI.IsLooped = true;
            screen = Screen.Intro;


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            instructFont = Content.Load<SpriteFont>("Instructions");
            tribbleGreyTexture = Content.Load<Texture2D>("tribbleGrey");
            tribbleBrownTexture = Content.Load<Texture2D>("tribbleBrown");
            tribbleCreamTexture = Content.Load<Texture2D>("tribbleCream");
            tribbleOrangeTexture = Content.Load<Texture2D>("tribbleOrange");
            spaceshipTexture = Content.Load<Texture2D>("spaceship background");
            spaceship2Texture = Content.Load<Texture2D>("spaceship");
            tribbleCoo = Content.Load<SoundEffect>("tribble_coo");
            music = Content.Load<SoundEffect>("background_music");
            tribbleIntroTexture = Content.Load<Texture2D>("tribble_intro");


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            mouseState = Mouse.GetState();

            if (screen == Screen.Intro)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                    screen = Screen.TribbleYard;

            }
            else if (screen == Screen.TribbleYard)
            {
                musicSEI.Stop();

                greyTribbleRect.X += (int)tribbleGreySpeed.X;
                greyTribbleRect.Y += (int)tribbleGreySpeed.Y;
                if (greyTribbleRect.Right > _graphics.PreferredBackBufferWidth || greyTribbleRect.Left < 0)
                    tribbleGreySpeed.X *= -1;
                if (greyTribbleRect.Bottom > _graphics.PreferredBackBufferHeight || greyTribbleRect.Top < 0)
                    tribbleGreySpeed.Y *= -1;

                brownTribbleRect.X += (int)tribbleBrownSpeed.X;
                brownTribbleRect.Y += (int)tribbleBrownSpeed.Y;
                if (brownTribbleRect.Right > (_graphics.PreferredBackBufferWidth + 100) || brownTribbleRect.Left < -100)
                    brownTribbleRect.X = -100;

                creamTribbleRect.X += (int)tribbleCreamSpeed.X;
                creamTribbleRect.Y += (int)tribbleCreamSpeed.Y;
                if (creamTribbleRect.Bottom > _graphics.PreferredBackBufferHeight || creamTribbleRect.Top < 0)
                {
                    tribbleCooSEI.Play();
                    //change back to play after testing
                    tribbleCreamSpeed.Y *= -1;
                }
                orangeTribbleRect.X += (int)tribbleOrangeSpeed.X;
                orangeTribbleRect.Y += (int)tribbleOrangeSpeed.Y;
                if (orangeTribbleRect.Right > _graphics.PreferredBackBufferWidth || orangeTribbleRect.Left < 0)
                    tribbleOrangeSpeed.X *= -1;
                if (orangeTribbleRect.Bottom > _graphics.PreferredBackBufferHeight || orangeTribbleRect.Top < 0)
                    tribbleOrangeSpeed.Y *= -1;
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.E))
                    screen = Screen.End;
            }
            else if (screen == Screen.End)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.AntiqueWhite);

            _spriteBatch.Begin();
            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(tribbleIntroTexture, new Rectangle(0, 0, 800, 500), Color.White);
                _spriteBatch.DrawString(instructFont, "Tribble Game", new Vector2(310, 515), Color.Black);
                _spriteBatch.DrawString(instructFont, "How To Play: Click to Enter and then watch the Tribbles go!", new Vector2(40, 550), Color.Black);
            }
            else if (screen == Screen.TribbleYard)
            {
                _spriteBatch.Draw(spaceshipTexture, spaceshipRect, Color.White);
                _spriteBatch.Draw(tribbleGreyTexture, greyTribbleRect, Color.White);
                _spriteBatch.Draw(tribbleBrownTexture, brownTribbleRect, Color.White);
                _spriteBatch.Draw(tribbleCreamTexture, creamTribbleRect, Color.White);
                _spriteBatch.Draw(tribbleOrangeTexture, orangeTribbleRect, Color.White);
                _spriteBatch.DrawString(instructFont, "Press E to Leave the Tribbles", new Vector2(440, 570), Color.Black);

            }
            else if (screen == Screen.End)
            {
                _spriteBatch.Draw(spaceship2Texture, spaceship2Rect, Color.White);
                _spriteBatch.DrawString(instructFont, "Press Escape to Exit", new Vector2(280, 290), Color.White);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
           
        }
    }
}