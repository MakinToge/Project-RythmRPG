using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG.Character
{
    /// <summary>
    /// Contains the animations of a character, both idle and attacking
    /// </summary>
    public class CharacterSprites
    {
        /// <summary>
        /// Current line of frame
        /// </summary>
        private int frameLine;

        /// <summary>
        /// Current frame in the line
        /// </summary>
        private int frameCol;

        /// <summary>
        /// Number of line
        /// </summary>
        private int frameLineCount;

        /// <summary>
        /// Number of frames per line
        /// </summary>
        private int frameColCount;
        
        /// <summary>
        /// The idle animation
        /// </summary>
        private Texture2D idleAnimation;

        /// <summary>
        /// The attacking animation
        /// </summary>
        private Texture2D attackingAnimation;

        /// <summary>
        /// The time per frame
        /// </summary>
        private float timePerFrame;

        /// <summary>
        /// The elapsed time
        /// </summary>
        private float totalElapsed;

        /// <summary>
        /// The rotation, scale and depth of the animation
        /// </summary>
        private float rotation, scale, depth;

        /// <summary>
        /// The position of the animation on screen
        /// </summary>
        private Vector2 position;

        /// <summary>
        /// The origin of the animation
        /// </summary>
        private Vector2 origin;

        /// <summary>
        /// The size of the sprites on screen
        /// </summary>
        private Vector2 size;

        /// <summary>
        /// True if we want to place the bottom right corner of the image 
        /// </summary>
        private bool bottomRight = false;

        /// <summary>
        /// True if the character is attacking, which changes the animation
        /// </summary>
        private bool isAttacking = false;
        public bool IsAttacking {
            get { return isAttacking; }
            set
            {
                if (isAttacking != value)   // Prevent from stopping an animation before the end
                {
                    this.isAttacking = value;
                    this.frameLine = 0;
                    this.frameCol = 0;
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="position">The position on screen</param>
        /// <param name="size">The size of the sprites</param>
        /// <param name="rotation">The rotation of the animation</param>
        /// <param name="scale">The scale of the animation</param>
        /// <param name="depth">The depth of the animation</param>
        public CharacterSprites(Vector2 position, Vector2 size, float rotation, float scale, float depth)
        {
            this.position = position;
            this.size = size;

            this.rotation = rotation;
            this.scale = scale;
            this.depth = depth;
        }

        /// <summary>
        /// Load the two animation
        /// </summary>
        /// <param name="content">The content manager</param>
        /// <param name="idle">The name of the idle animation</param>
        /// <param name="attacking">The name of the attacking animation</param>
        /// <param name="frameLineCount">The number of lines on the spritesheet</param>
        /// <param name="frameColCount">The number of frames per line</param>
        /// <param name="framesPerSec">The number of frames per second</param>
        public void Load(ContentManager content, string idle, string attacking, int frameLineCount, int frameColCount, int framesPerSec)
        {
            this.frameLineCount = frameLineCount;
            this.frameColCount = frameColCount;

            this.idleAnimation = content.Load<Texture2D>(idle);
            this.attackingAnimation = content.Load<Texture2D>(attacking);

            this.timePerFrame = (float)1 / framesPerSec;
            this.frameLine = 0;
            this.frameCol = 0;
            this.totalElapsed = 0;

            // By default, the origin is top, left-hand corner of the sprite
            this.origin = new Vector2(this.position.X, this.position.Y);
        }

        /// <summary>
        /// Change the origin of the sprite to the bottom, right-hand corner
        /// </summary>
        public void setOriginBottomLeft()
        {
            this.origin = new Vector2(this.position.X + (this.attackingAnimation.Width / this.frameColCount), this.position.Y + (this.attackingAnimation.Height / this.frameLineCount));
            this.bottomRight = true;
        }

        /// <summary>
        /// Update the frame
        /// </summary>
        /// <param name="elapsed">The elapsed time since last update</param>
        public void UpdateFrame(float elapsed)
        {
            totalElapsed += elapsed;

            if (totalElapsed > timePerFrame)
            {
                this.frameCol++;

                if(this.frameCol == this.frameColCount)
                {
                    this.frameCol = 0;
                    this.frameLine++;

                    if (this.frameLine == this.frameLineCount)
                    {
                        this.frameLine = 0;

                        if(this.isAttacking)    // The attack animation is over
                        {
                            this.IsAttacking = false;
                        }
                    }
                }

                totalElapsed -= this.timePerFrame;
            }
        }

        /// <summary>
        /// Draw the current frame
        /// </summary>
        /// <param name="batch">The spritebatch</param>
        public void DrawFrame(SpriteBatch batch)
        {
            batch.Begin();

            int frameWidth;
            int frameHeight;

            // Rectangle to get the right sprite on the spritesheet
            Rectangle sourceRect;
            Vector2 spritePosition;

            SpriteEffects effect = SpriteEffects.FlipHorizontally;

            if (this.isAttacking)
            {
                frameWidth = attackingAnimation.Width / frameColCount;
                frameHeight = attackingAnimation.Height / frameLineCount;

                sourceRect = new Rectangle(frameWidth * frameCol, frameHeight * frameLine, frameWidth, frameHeight);

                if (this.bottomRight)
                {
                    spritePosition = new Vector2(this.origin.X - sourceRect.Width, this.origin.Y - sourceRect.Height);
                }
                else
                {
                    spritePosition = this.position;
                }

                batch.Draw(this.attackingAnimation, spritePosition, sourceRect, Color.White, this.rotation, Vector2.Zero, this.size, effect, this.depth);
            }
            else
            {
                frameWidth = idleAnimation.Width / frameColCount;
                frameHeight = idleAnimation.Height / frameLineCount;

                sourceRect = new Rectangle(frameWidth * frameCol, frameHeight * frameLine, frameWidth, frameHeight);
                if (this.bottomRight)
                {
                    spritePosition = new Vector2(this.origin.X - sourceRect.Width, this.origin.Y - sourceRect.Height);
                }
                else
                {
                    spritePosition = this.position;
                }

                batch.Draw(this.idleAnimation, spritePosition, sourceRect, Color.White, this.rotation, Vector2.Zero, this.size, effect, this.depth);
            }

            batch.End();
        }
    }
}
