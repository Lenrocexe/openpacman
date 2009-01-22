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
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Tile : Microsoft.Xna.Framework.DrawableGameComponent
    {
        protected Vector2 spritePosition;
        protected Texture2D spritesheet = null;

        int CurrentFrameX = 0;
        int SpriteWidth = 14;
        int SpriteHeight = 14;
        public Rectangle SourceRect;
        public Rectangle DestRect;

        private Game game;

        public Tile(Game game, int x, int y, int width, int height): base(game)
        {
            this.game = game;
            DestRect = new Rectangle(x, y, width, height);
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            base.Initialize();
            this.spritesheet = game.Content.Load<Texture2D>(@"Images\Objects\tile");
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // TODO: Add your drawing code here
            SourceRect = new Rectangle(CurrentFrameX * SpriteWidth, 0, SpriteWidth, SpriteHeight);
            spriteBatch.Draw(spritesheet, DestRect, SourceRect, Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 0);
            base.Draw(gameTime);
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
    }
}