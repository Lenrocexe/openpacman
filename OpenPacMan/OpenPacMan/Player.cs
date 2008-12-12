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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace OpenPacMan
{

    public class Player : Microsoft.Xna.Framework.DrawableGameComponent
    {
        KeyboardState keyboard = Keyboard.GetState();
        public Texture2D sprite;
        protected Texture2D spritesheet = null;
        protected Vector2 spritePosition;
        public Vector2 loc = new Vector2(250, 250);
        int i = 0;
        float Timer = 0f;
        protected string key;

        private Game game;

        public Player(Game game)
            : base(game)
        {
            this.game = game;
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            base.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // TODO: Add your drawing code here
            if(i == 0)
            {
                sprite = Game.Content.Load<Texture2D>(@"Images\Sprites\pacman");
                i = 1;
            }
            spriteBatch.Draw(sprite, loc, Color.White);
            base.Draw(gameTime);
        }

        public void pressedKeys(KeyboardState k_State, Vector2 s_Position)
        {
            keyboard = (KeyboardState)k_State;
            loc = (Vector2)s_Position;
            key = keyboard.GetPressedKeys().ToString();
            Console.WriteLine(key+" key pressed.");

            if (keyboard.IsKeyDown(Keys.Down) && keyboard.IsKeyDown(Keys.Right))
            {
                if (keyboard.IsKeyDown(Keys.Down))
                {
                    loc += new Vector2(0.0f, 2.0f);
                }
                else if (keyboard.IsKeyDown(Keys.Right))
                {
                    loc += new Vector2(2.0f, 0.0f);
                } 
                this.moveAni();
            }
            else if (keyboard.IsKeyDown(Keys.Down) && keyboard.IsKeyDown(Keys.Left))
            {
                if (keyboard.IsKeyDown(Keys.Down))
                {
                    loc += new Vector2(0.0f, 2.0f);
                }
                else if (keyboard.IsKeyDown(Keys.Left))
                {
                    loc += new Vector2(-2.0f, 0.0f);
                } 
                this.moveAni();
            }
            else if (keyboard.IsKeyDown(Keys.Up) && keyboard.IsKeyDown(Keys.Right))
            {
                if (keyboard.IsKeyDown(Keys.Up))
                {
                    loc += new Vector2(0.0f, -2.0f);
                }
                else if (keyboard.IsKeyDown(Keys.Right))
                {
                    loc += new Vector2(2.0f, 0.0f);
                } 
                
                this.moveAni();
            }
            else if (keyboard.IsKeyDown(Keys.Up) && keyboard.IsKeyDown(Keys.Left))
            {
                if (keyboard.IsKeyDown(Keys.Up))
                {
                    loc += new Vector2(0.0f, -2.0f);
                }
                else if (keyboard.IsKeyDown(Keys.Left))
                {
                    loc += new Vector2(-2.0f, 0f);
                }
                this.moveAni();
            }
            else
            {
                if (keyboard.IsKeyDown(Keys.Up))
                {
                    loc += new Vector2(0.0f, -2.0f);
                    this.moveAni();
                }
                if (keyboard.IsKeyDown(Keys.Down))
                {
                    loc += new Vector2(0.0f, 2.0f);
                    this.moveAni();
                }
                if (keyboard.IsKeyDown(Keys.Left))
                {
                    loc += new Vector2(-2.0f, 0.0f);
                    this.moveAni();
                }
                if (keyboard.IsKeyDown(Keys.Right))
                {
                    loc += new Vector2(2.0f, 0.0f);
                    this.moveAni();
                }
            }
        }
        public void moveAni()
        {
            if (Timer == 5)
            {
                sprite = Game.Content.Load<Texture2D>(@"Images\Sprites\pacman2");
            }
            else if(Timer == 10)
            {
                sprite = Game.Content.Load<Texture2D>(@"Images\Sprites\pacman3");
            }
            else if (Timer == 15)
            {
                sprite = Game.Content.Load<Texture2D>(@"Images\Sprites\pacman2");
            }
            else if (Timer == 20)
            {
                sprite = Game.Content.Load<Texture2D>(@"Images\Sprites\pacman");
                Timer = 0f;
            }
            Timer++;
        }
    }
}