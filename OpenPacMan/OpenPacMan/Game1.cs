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
using OpenPacMan;

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

        //components
        Player pacman;
        List<Tile> tiles;
        List<Pill> pills;
        
        KeyboardState keyboard;
        KeyboardState oldState;
        
        // graphical variables
        Rectangle screenBounds;
        Texture2D background;
        Texture2D logo;
        
        // audio variables
        Song gamemusic;
        Song gamemusic2;
        Song gamemusic3;
        Song gamemusic4;
        Song gamemusic5;
        Song gamemusic6;
        Song gamemusic7;
        Song gamemusic8;
        AudioEngine audioEngine;
        WaveBank waveBank;
        SoundBank soundBank;
        
        // Font
        SpriteFont font;

        // score
        int score = 0;

        // var for newgame sound
        int playstart = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            pacman = new Player(this);
            pills = new List<Pill> { };
            tiles = new List<Tile> { };

            screenBounds = new Rectangle(0, 0, 600, 300);
            //tiles = new Tile(this, 250,325,16,8);
        }

        protected override void Initialize()
        {
            this.Components.Add(pacman);
            pacman.ScreenBounds(screenBounds);

            oldState = Keyboard.GetState();

            // load audio
            audioEngine = new AudioEngine(@"Content\Audio\audio.xgs");
            waveBank = new WaveBank(audioEngine, @"Content\Audio\WaveBank.xwb");
            soundBank = new SoundBank(audioEngine, @"Content\Audio\SoundBank.xsb");
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            this.Services.AddService(typeof(SpriteBatch), spriteBatch);

            // set screen dimensions
            graphics.PreferredBackBufferWidth = 600;
            graphics.PreferredBackBufferHeight = 350;
            graphics.ApplyChanges();

            // Load data files
            background = this.Content.Load<Texture2D>(@"Images\background");
            font = this.Content.Load<SpriteFont>(@"font");
            gamemusic = this.Content.Load<Song>(@"Audio\Music\background9");
            gamemusic2 = this.Content.Load<Song>(@"Audio\Music\background8");
            gamemusic3 = this.Content.Load<Song>(@"Audio\Music\background3");
            gamemusic4 = this.Content.Load<Song>(@"Audio\Music\background7");
            gamemusic5 = this.Content.Load<Song>(@"Audio\Music\background");
            gamemusic6 = this.Content.Load<Song>(@"Audio\Music\background5");
            gamemusic7 = this.Content.Load<Song>(@"Audio\Music\background6");
            gamemusic8 = this.Content.Load<Song>(@"Audio\Music\background12");
            logo = this.Content.Load<Texture2D>(@"Images\logo");
        }
        
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            keyboard = Keyboard.GetState();
            
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (keyboard.IsKeyDown(Keys.Escape))
                this.Exit();

            KeyboardState newState = Keyboard.GetState();

            // Get keys pressed now that weren't pressed before
            var newPressedKeys = from k in newState.GetPressedKeys()
                                 where !(oldState.GetPressedKeys().Contains(k))
                                 select k;

            // play some music by pressing the corresponding key
            if (keyboard.IsKeyDown(Keys.F1))
            {
                MediaPlayer.Play(gamemusic5);
            }
            if (keyboard.IsKeyDown(Keys.F2))
            {
                MediaPlayer.Play(gamemusic2);
            }
            if (keyboard.IsKeyDown(Keys.F3))
            {
                MediaPlayer.Play(gamemusic3);
            }
            if (keyboard.IsKeyDown(Keys.F4))
            {
                MediaPlayer.Play(gamemusic4);
            }
            if (keyboard.IsKeyDown(Keys.F5))
            {
                MediaPlayer.Play(gamemusic5);
            }
            if (keyboard.IsKeyDown(Keys.F6))
            {
                MediaPlayer.Play(gamemusic6);
            }
            if (keyboard.IsKeyDown(Keys.F7))
            {
                MediaPlayer.Play(gamemusic7);
            }
            if (keyboard.IsKeyDown(Keys.F8))
            {
                MediaPlayer.Play(gamemusic8);
            }
            if (keyboard.IsKeyDown(Keys.F12))
            {
                MediaPlayer.Stop();
            }
            if (keyboard.IsKeyDown(Keys.P))
            {
                score += 10;
            }
            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            BeginDraw();

            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteBlendMode.AlphaBlend);

                spriteBatch.Draw(background,new Vector2(0,0), Color.White);
                spriteBatch.Draw(logo, new Vector2(300, 10),Color.White);
                pacman.Draw(gameTime, spriteBatch);
                spriteBatch.DrawString(font, "Score: " + score, new Vector2(10, 30), Color.Red);

            spriteBatch.End();
            
            base.Draw(gameTime);
            EndDraw();

            // Play the start tune at the beginning
            if (playstart == 0)
            {
                newGame();
            }
        }

        // create a new game when all pills are eaten.
        // Cornel Alders
        public void newGame()
        {
            for (int i = 0; i <= 10; i++)
            {
                Pill food = new Pill(this);
                food.positionUpdate(100 + i * 50, 250);
                this.Components.Add(food);
                pills.Add(food);
            }
            soundBank.GetCue("newgame").Play();
            //System.Threading.Thread.Sleep(4411);
            playstart = 1;
        }

        // Check collisions with pills
        // Cornel Alders
        public void checkPill()
        {
            for (int i = pills.Count - 1; i >= 0;i++ )
            {
                if (pacman.DestRect.Intersects(pills[i].DestRect))
                {
                    score += 10;
                    soundBank.GetCue("eatpill1").Play();
                    pills.Remove(pills[i]);
                }
            }

            // start a new game if no pills are left
            if (pills.Count == 0)
            {
                playstart = 0;
            }
        }
    }
}
