using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;

namespace OpenPacMan
{
    /// <summary>
    /// Free pacman clone created by students in the netherlands
    /// 
    /// Cornel Alders
    /// Maarten Hus
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState keyboard = Keyboard.GetState();
        Texture2D background;
        Player pacman;
        Song gamemusic;
        Song gamemusic2;
        AudioEngine audioEngine;
        WaveBank waveBank;
        SoundBank soundBank;
        Cue effectSound;
        SpriteFont font;

        KeyboardState oldState;

        // var for start music
        int playstart = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            pacman = new Player(this);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.Components.Add(pacman);
            oldState = Keyboard.GetState();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            this.Services.AddService(typeof(SpriteBatch), spriteBatch);
            // TODO: use this.Content to load your game content here
            graphics.PreferredBackBufferWidth = 600;
            graphics.PreferredBackBufferHeight = 350;
            graphics.ApplyChanges();
            background = this.Content.Load<Texture2D>(@"Images\background");
            font = this.Content.Load<SpriteFont>(@"font");
            gamemusic = this.Content.Load<Song>(@"Audio\Music\background");
            gamemusic2 = this.Content.Load<Song>(@"Audio\Music\background2");
            
            audioEngine = new AudioEngine(@"Content\Audio\sounds.xgs");
            waveBank = new WaveBank(audioEngine, @"Content\Audio\WBank.xwb");
            soundBank = new SoundBank(audioEngine, @"Content\Audio\SBank.xsb");

        }
        
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Escape))
                this.Exit();

            KeyboardState newState = Keyboard.GetState();

            // Get keys pressed now that weren't pressed before
            var newPressedKeys = from k in newState.GetPressedKeys()
                                 where !(oldState.GetPressedKeys().Contains(k))
                                 select k;

            if(newPressedKeys.Contains(Keys.A))
            {
                soundBank.PlayCue("fruiteat");
            }

            this.pacman.pressedKeys(Keyboard.GetState(), this.pacman.DestRect);

            // TODO: Add your update logic here
            if (keyboard.IsKeyDown(Keys.F1))
            {
                MediaPlayer.Play(gamemusic);
            }
            if (keyboard.IsKeyDown(Keys.F2))
            {
                MediaPlayer.Play(gamemusic2);
            }
            if (keyboard.IsKeyDown(Keys.F3))
            {
                MediaPlayer.Stop();
            }

            if (keyboard.IsKeyDown(Keys.S))
            {
                soundBank.PlayCue("fruiteat");
            }
            if (keyboard.IsKeyDown(Keys.D))
            {
                soundBank.PlayCue("ghosteaten");
            }
            if (keyboard.IsKeyDown(Keys.F))
            {
                soundBank.PlayCue("interm");
            }
            if (keyboard.IsKeyDown(Keys.G))
            {
                soundBank.PlayCue("killed");
            }
            if (keyboard.IsKeyDown(Keys.H))
            {
                soundBank.PlayCue("pacchomp");
            }
            if (keyboard.IsKeyDown(Keys.J))
            {
                soundBank.PlayCue("start");
            }
            base.Update(gameTime);
        }
        
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            BeginDraw();
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend);
            spriteBatch.Draw(background,new Vector2(0,0), Color.White);
            Texture2D logo = Content.Load<Texture2D>(@"Images\Sprites\logo");
            spriteBatch.Draw(logo, new Vector2(300, 10),Color.White);
            pacman.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(font,"Score: ", new Vector2(10, 30), Color.Red);
            spriteBatch.End();
            // TODO: Add your drawing code here
            
            base.Draw(gameTime);
            EndDraw();
            if (playstart == 0)
            {
                soundBank.GetCue("start").Play();
                System.Threading.Thread.Sleep(4411);
                //soundBank.GetCue("pacchomp").Play();
                playstart = 1;
            }
            
        }
    }
}
