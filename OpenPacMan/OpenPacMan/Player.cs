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
        protected Rectangle screenBounds;
        
        protected int moveDirection;
        float timer = 0f;
        protected string key;
        protected int killed = 0;

        /// animation logic
        float interval = 1000f / 10f;
        int frameCount = 3;
        int currentFrameX = 0;
        int currentFrameY = 0;
        int spriteWidth = 14;
        int spriteHeight = 14;

        /// rectangle for the spritesheet (which part of the spritesheet to draw)
        public Rectangle SourceRect;
        /// position on screen
        public Rectangle DestRect = new Rectangle(20, 320, 14, 14);

        private bool jumpup = true;
        private bool inAir = false;

        enum State
        {
            Walking,
            Jumping
        }
        State CurrentState = State.Walking;

        private Game game;

        public Player(Game game): base(game)
        {
            this.game = game;
        }

        public override void Initialize()
        {
            // TODO: Add your initialization code here
            base.Initialize();
            this.spritesheet = game.Content.Load<Texture2D>(@"Images\Player\pacman");

        }

        public void ScreenBounds(Rectangle theScreenBounds)
        {
            screenBounds = theScreenBounds;
        }

        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            this.PressedKeys(Keyboard.GetState());

            if (CurrentState == State.Jumping)
            {
                Jump();
            }

            // Sprite Modifier
            if (keyboard.IsKeyDown(Keys.D1))
            {
                //change sprite into Pacman
                // 14 by 14
            }
            if (keyboard.IsKeyDown(Keys.D2))
            {
                //change sprite into Mario
                // height 32 by width 16
            }
            if (keyboard.IsKeyDown(Keys.D3))
            {
                //change sprite into Rockman
                // height 30 by  width 26
            }

            // Check screen boundaries
            if (DestRect.X <= screenBounds.Left)
            {
                DestRect.X = screenBounds.Left;
            }
            if (DestRect.X + DestRect.Width >= screenBounds.Width)
            {
                DestRect.X = screenBounds.Width - DestRect.Width;
            }

            // Resets frame to standing position if Left and Right are false
            if (keyboard.IsKeyDown(Keys.Left) == false && keyboard.IsKeyDown(Keys.Right) == false)
            {
                this.currentFrameX = 0;
            }
            base.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // TODO: Add your drawing code here
            
            SourceRect = new Rectangle(currentFrameX * spriteWidth, currentFrameY, spriteWidth, spriteHeight);
            spriteBatch.Draw(spritesheet, DestRect, SourceRect, Color.White, 0, new Vector2(0,0), SpriteEffects.None, 0);
            base.Draw(gameTime);
        }

        public void PressedKeys(KeyboardState k_State)
        {
            keyboard = (KeyboardState)k_State;
            //DestRect = (Rectangle)s_Position;
            int Xpos = this.DestRect.X;
            int Ypos = this.DestRect.Y;
            key = keyboard.GetPressedKeys().ToString();

            if (keyboard.IsKeyDown(Keys.Up))
            {
                //this.rotate(0);
                //Ypos += -2;
                //DestRect = new Rectangle(Xpos, Ypos, 14, 14);
                //this.moveAni();
                this.CurrentState = State.Jumping;
                
            }
            if (keyboard.IsKeyDown(Keys.Right) == true && CurrentState == State.Walking)
            {
                Xpos += 2;
                this.DestRect = new Rectangle(Xpos, Ypos, 14, 14);
                this.MoveAni(1);
                this.CurrentState = State.Walking;
            }
            if (keyboard.IsKeyDown(Keys.Down))
            {
                //this.rotate(2);
                //Ypos += 2;
                //DestRect = new Rectangle(Xpos, Ypos, 14, 14);
                //this.moveAni();
            }
            if (keyboard.IsKeyDown(Keys.Left) == true && CurrentState == State.Walking)
            {
                Xpos += -2;
                this.DestRect = new Rectangle(Xpos, Ypos, 14, 14);
                this.MoveAni(0);
                this.CurrentState = State.Walking;
            }

         }

        public void MoveAni(int direction)
        {
            moveDirection = direction;
            if (timer > interval)
            {
                //Cycle through animation frames
                currentFrameX++;
                if (currentFrameX > frameCount - 1)
                {
                    currentFrameX = 0;
                }
                timer = 0f;
            }
            //check movement direction
            if (moveDirection == 0)
            {
                ///move left
                currentFrameY = 14;
            }
            else if (moveDirection == 1)
            {
                ///move right
                currentFrameY = 0;
            }
        }

        public void Jump()
        {
            if (jumpup)
            {
                this.inAir = true;
                this.DestRect.Y -= 3;

                if (this.DestRect.Y < 250)
                {
                    this.jumpup = false;
                }
            }
            else
            {
                this.DestRect.Y += 3;

                if (this.DestRect.Y == this.screenBounds.Bottom)
                {
                    this.inAir = false;
                    this.CurrentState = State.Walking;
                    this.jumpup = true;
                }
            }
            // allows movement in air
            if (inAir)
            {
                if (this.keyboard.IsKeyDown(Keys.Right))
                {
                    this.DestRect.X += 2;
                    this.MoveAni(1);
                }
                else if (this.keyboard.IsKeyDown(Keys.Left))
                {
                    this.DestRect.X += -2;
                    this.MoveAni(0);
                }
            }
            this.DestRect = new Rectangle(this.DestRect.X, this.DestRect.Y, this.DestRect.Width, this.DestRect.Height);  
        }

    }
}