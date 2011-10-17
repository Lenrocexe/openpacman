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
        Tile tile;
        KeyboardState keyboard;
        KeyboardState oldState;
        
        // graphical variables
        Rectangle screenBounds;
        Texture2D background;
        Texture2D logo;
        
        // audio variables
        Song gamemusic01;
        Song gamemusic02;
        Song gamemusic03;
        Song gamemusic04;
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
            tile = new Tile(this, "tile1", 150, 280 );
            screenBounds = new Rectangle(0, 0, 600, 300);
            //tiles = new Tile(this, 250,325,16,8);
        }

        protected override void Initialize()
        {
            this.Components.Add(pacman);
            foreach (Pill pill in pills)
            {
                this.Components.Add(pill);
            }
            this.Components.Add(tile);
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
            gamemusic = this.Content.Load<Song>(@"Audio\Music\background01");
            gamemusic2 = this.Content.Load<Song>(@"Audio\Music\background02");
            gamemusic3 = this.Content.Load<Song>(@"Audio\Music\background03");
            gamemusic4 = this.Content.Load<Song>(@"Audio\Music\background04");
            logo = this.Content.Load<Texture2D>(@"Images\logo");
        }
        
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here1
        }

        protected override void Update(GameTime gameTime)
        {
            keyboard = Keyboard.GetState();
            
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (keyboard.IsKeyDown(Keys.Escape))
                this.Exit();
            checkPill();
            KeyboardState newState = Keyboard.GetState();
            
            // Get keys pressed now that weren't pressed before
            var newPressedKeys = from k in newState.GetPressedKeys()
                                 where !(oldState.GetPressedKeys().Contains(k))
                                 select k;

            // play some music by pressing the corresponding key
            if (keyboard.IsKeyDown(Keys.F1))
            {
                MediaPlayer.Play(gamemusic1);
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
            foreach (Pill pill in pills)
            {
                pill.Draw(gameTime, spriteBatch);
            }
                //tile.Draw(gameTime, spriteBatch);
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
            for (int i = 0; i <= 9; i++)
            {
                Pill food = new Pill(this);
                food.positionUpdate(100 + i * 50, 250);
                pills.Add(food);
            }
            playstart = 1;
        }

        // Check collisions with pills
        // Cornel Alders
        public void checkPill()
        {
            try
            {
                foreach (Pill pill in pills)
                {
                    if (pacman.DestRect.Intersects(pill.DestRect))
                    {
                        score += 10;
                        soundBank.GetCue("eatpill1").Play();
                        pills.Remove(pill);
                    }
                }
            }
            catch
            {

            }

            // start a new game if no pills are left
            if (pills.Count == 0)
            {
                newGame();
            }
        }

    }
}
