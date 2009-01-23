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
    public class Pill : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public Texture2D sprite;
        public Rectangle DestRect;
        public Rectangle SourceRect;

        private Game game;

        public Pill(Game game)
            : base(game)
        {
            this.game = game;
            sprite = game.Content.Load<Texture2D>(@"Images\Objects\pill");
            SourceRect = new Rectangle(0, 0, 7, 7);
            
        }

        public override void Initialize()
        {
            
            base.Initialize();
        }
        protected override void LoadContent()
        {
            
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        public void positionUpdate(int x, int y)
        {
            this.DestRect.X = x;
            this.DestRect.Y = y;
            this.DestRect.Width = this.sprite.Width;
            this.DestRect.Height = this.sprite.Height;
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SpriteBatch batch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            
            batch.Draw(sprite, DestRect, SourceRect, Color.White);
            
            base.Draw(gameTime);
        }
    }
}