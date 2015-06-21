using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG.CharacterStuff
{
    /// <summary>
    /// Contains the animations of a character, both idle and attacking
    /// </summary>
    public class CharacterSprites
    {
        private int frameLine;
        private int frameCol;
        private int frameLineCount;
        private int frameColCount;
        
        private Texture2D idleAnimation;
        private Texture2D attackingAnimation;

        private float timePerFrame;
        private float totalElapsed;

        private float rotation, scale, depth;
        private Vector2 position;
        private Vector2 origin;
        private bool bottomLeft = false;

        private bool isAttacking = false;
        public bool IsAttacking {
            get { return isAttacking; }
            set
            {
                if (isAttacking != value)
                {
                    this.isAttacking = value;
                    this.frameLine = 0;
                    this.frameCol = 0;
                }
            }
        }

        public CharacterSprites(Vector2 position, float rotation, float scale, float depth)
        {
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
            this.depth = depth;
        }

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

            this.origin = new Vector2(this.position.X, this.position.Y);
        }

        public void setOriginBottomLeft()
        {
            this.origin = new Vector2(this.position.X + (this.attackingAnimation.Width / this.frameColCount), this.position.Y + (this.attackingAnimation.Height / this.frameLineCount));
            this.bottomLeft = true;
        }

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

                        if(this.isAttacking)
                        {
                            this.IsAttacking = false;
                        }
                    }
                }

                totalElapsed -= this.timePerFrame;
            }
        }

        public void DrawFrame(SpriteBatch batch)
        {
            int frameWidth;
            int frameHeight;

            Rectangle sourceRect;
            Vector2 pos;

            SpriteEffects effect = SpriteEffects.FlipHorizontally;

            if (this.isAttacking)
            {
                frameWidth = attackingAnimation.Width / frameColCount;
                frameHeight = attackingAnimation.Height / frameLineCount;

                sourceRect = new Rectangle(frameWidth * frameCol, frameHeight * frameLine, frameWidth, frameHeight);

                if (this.bottomLeft)
                {
                    pos = new Vector2(this.origin.X - sourceRect.Width, this.origin.Y - sourceRect.Height);
                }
                else
                {
                    pos = this.position;
                }

                batch.Draw(this.attackingAnimation, pos, sourceRect, Color.White, this.rotation, Vector2.Zero, this.scale, effect, this.depth);
            }
            else
            {
                frameWidth = idleAnimation.Width / frameColCount;
                frameHeight = idleAnimation.Height / frameLineCount;

                sourceRect = new Rectangle(frameWidth * frameCol, frameHeight * frameLine, frameWidth, frameHeight);
                if (this.bottomLeft)
                {
                    pos = new Vector2(this.origin.X - sourceRect.Width, this.origin.Y - sourceRect.Height);
                }
                else
                {
                    pos = this.position;
                }

                batch.Draw(this.idleAnimation, pos, sourceRect, Color.White, this.rotation, Vector2.Zero, this.scale, effect, this.depth);
            }
        }
    }
}
