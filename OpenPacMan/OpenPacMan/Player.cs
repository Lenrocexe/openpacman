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
        protected Texture2D sprite;
        protected Vector2 spritePosition;
        Vector2 loc = new Vector2(150, 150);
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
            sprite = Game.Content.Load<Texture2D>(@"Images\Sprites\pacman");
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
            
            spriteBatch.Draw(sprite, loc, Color.White);
            base.Draw(gameTime);
        }

        public void pressedKeys(KeyboardState k_State, Vector2 s_Position)
        {
            keyboard = (KeyboardState)k_State;
            spritePosition = (Vector2)s_Position;

            if (keyboard.IsKeyDown(Keys.Down) && keyboard.IsKeyDown(Keys.Right))
            {
                spritePosition += new Vector2(8.0f, 8.0f);
            }
            else if (keyboard.IsKeyDown(Keys.Down) && keyboard.IsKeyDown(Keys.Left))
            {
                spritePosition += new Vector2(-8.0f, 8.0f);
            }
            else if (keyboard.IsKeyDown(Keys.Up) && keyboard.IsKeyDown(Keys.Right))
            {
                spritePosition += new Vector2(8.0f, -8.0f);
            }
            else if (keyboard.IsKeyDown(Keys.Up) && keyboard.IsKeyDown(Keys.Left))
            {
                spritePosition += new Vector2(-8.0f, -8.0f);
            }
            else
            {
                if (keyboard.IsKeyDown(Keys.Up)) spritePosition += new Vector2(0.0f, -8.0f);
                if (keyboard.IsKeyDown(Keys.Down)) spritePosition += new Vector2(0.0f, 8.0f);
                if (keyboard.IsKeyDown(Keys.Left)) spritePosition += new Vector2(-8.0f, 0.0f);
                if (keyboard.IsKeyDown(Keys.Right)) spritePosition += new Vector2(8.0f, 0.0f);
            }
        }
    }
}