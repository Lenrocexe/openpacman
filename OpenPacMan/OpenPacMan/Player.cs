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
        ///sprite data
        protected Texture2D spritesheet = null;
        protected Vector2 spritePosition;
        
        protected int moveDirection;
        int i = 0;
        float Timer = 0f;
        protected string key;
        protected int killed = 0;

        /// animation logic
        float Interval = 1000f / 10f;
        int FrameCount = 3;
        int CurrentFrameX = 0;
        int CurrentFrameY = 0;
        int SpriteWidth = 14;
        int SpriteHeight = 14;
        public Rectangle SourceRect;
        public Rectangle DestRect = new Rectangle(20, 320, 14, 14);

        private Game game;

        public Player(Game game)
            : base(game)
        {
            this.game = game;
            // TODO: Construct any child components here
        }

        public override void Initialize()
        {
            // TODO: Add your initialization code here
            base.Initialize();
            this.spritesheet = game.Content.Load<Texture2D>(@"Images\Sprites\pacman");

        }

        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            Timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            base.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // TODO: Add your drawing code here
            SourceRect = new Rectangle(CurrentFrameX * SpriteWidth, 0, SpriteWidth, SpriteHeight);
            spriteBatch.Draw(spritesheet, DestRect, SourceRect, Color.White, 0, new Vector2(0,0), SpriteEffects.None, 0);
            base.Draw(gameTime);
        }

        public void pressedKeys(KeyboardState k_State, Rectangle s_Position)
        {
            keyboard = (KeyboardState)k_State;
            DestRect = (Rectangle)s_Position;
            int Xpos = DestRect.X;
            int Ypos = DestRect.Y;
            key = keyboard.GetPressedKeys().ToString();
            Keys[] keysPressed = keyboard.GetPressedKeys();

            if (keyboard.IsKeyDown(Keys.Down) && keyboard.IsKeyDown(Keys.Right))
            {
                if (keyboard.IsKeyDown(Keys.Down))
                {
                    Ypos += 2;
                    DestRect = new Rectangle(Xpos, Ypos, 14, 14);
                }
                else if (keyboard.IsKeyDown(Keys.Right))
                {
                    Xpos += 2;
                    DestRect = new Rectangle(Xpos, Ypos, 14, 14);
                } 
                this.moveAni();
            }
            else if (keyboard.IsKeyDown(Keys.Down) && keyboard.IsKeyDown(Keys.Left))
            {
                if (keyboard.IsKeyDown(Keys.Down))
                {
                    Ypos += 2;
                    DestRect = new Rectangle(Xpos, Ypos, 14, 14);
                }
                else if (keyboard.IsKeyDown(Keys.Left))
                {
                    Xpos += -2;
                    DestRect = new Rectangle(Xpos, Ypos, 14, 14);
                } 
                this.moveAni();
            }
            else if (keyboard.IsKeyDown(Keys.Up) && keyboard.IsKeyDown(Keys.Right))
            {
                if (keyboard.IsKeyDown(Keys.Up))
                {
                    Ypos += -2;
                    DestRect = new Rectangle(Xpos, Ypos, 14, 14);
                }
                else if (keyboard.IsKeyDown(Keys.Right))
                {
                    Xpos += 2;
                    DestRect = new Rectangle(Xpos, Ypos, 14, 14);
                } 
                
                this.moveAni();
            }
            else if (keyboard.IsKeyDown(Keys.Up) && keyboard.IsKeyDown(Keys.Left))
            {
                if (keysPressed[0] == Keys.Up)
                {
                    Ypos += -2;
                    DestRect = new Rectangle(Xpos, Ypos, 14, 14);
                }
                else if (keysPressed[0] == Keys.Left)
                {
                    Xpos += -2;
                    DestRect = new Rectangle(Xpos, Ypos, 14, 14);
                }
                this.moveAni();
            }
            else
            {
                if (keyboard.IsKeyDown(Keys.Up))
                {
                    this.rotate(0);
                    Ypos += -2;
                    DestRect = new Rectangle(Xpos, Ypos, 14, 14);
                    this.moveAni();
                }
                if (keyboard.IsKeyDown(Keys.Right))
                {
                    this.rotate(1);
                    Xpos += 2;
                    DestRect = new Rectangle(Xpos, Ypos, 14, 14);
                    this.moveAni();
                }
                if (keyboard.IsKeyDown(Keys.Down))
                {
                    this.rotate(2);
                    Ypos += 2;
                    DestRect = new Rectangle(Xpos, Ypos, 14, 14);
                    this.moveAni();
                }
                if (keyboard.IsKeyDown(Keys.Left))
                {
                    this.rotate(3);
                    Xpos += -2;
                    DestRect = new Rectangle(Xpos, Ypos, 14, 14);
                    this.moveAni();
                }
            }
        }

        public void moveAni()
        {

            if (Timer > Interval)
            {
                CurrentFrameX++;
                if (CurrentFrameX > FrameCount - 1)
                {
                    CurrentFrameX = 0;

                }
                Timer = 0f;
            }
            SourceRect = new Rectangle(CurrentFrameX * SpriteWidth, CurrentFrameY, SpriteWidth, SpriteHeight);
        }

        public void rotate(int direction)
        {
            this.moveDirection = direction;
            if (this.moveDirection == 0)
            {
                ///spritesheet xpos +
            }
            else if (this.moveDirection == 1)
            {
                
            }
            else if (this.moveDirection == 2)
            {
                
            }
            else if (this.moveDirection == 3)
            {
                
            }

        }
        public void move()
        {

        }

    }
}