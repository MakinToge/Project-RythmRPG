using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG.CharacterStuff
{
    class CharacterSprites
    {
        private int frame;
        private int framecount;
        
        private Texture2D idleAnimation;
        private Texture2D attackingAnimation;

        private float timePerFrame;
        private float totalElapsed;

        private float rotation, scale, depth;
        private Vector2 position;

        public bool isAttacking { get; set; }
        public bool mirror { get; set; }

        public CharacterSprites(Vector2 position, float rotation, float scale, float depth, bool mirror)
        {
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
            this.depth = depth;

            this.isAttacking = false;
            this.mirror = mirror;
        }

        public void Load(ContentManager content, string idle, string attacking, int frameCount, int framesPerSec)
        {
            this.framecount = frameCount;

            this.idleAnimation = content.Load<Texture2D>(idle);
            this.attackingAnimation = content.Load<Texture2D>(attacking);

            this.timePerFrame = (float)1 / framesPerSec;
            this.frame = 0;
            this.totalElapsed = 0;
        }

        public void UpdateFrame(float elapsed)
        {
            totalElapsed += elapsed;

            if (totalElapsed > timePerFrame)
            {
                this.frame++;

                if(this.frame == this.framecount - 1 && this.isAttacking)
                {
                    this.isAttacking = false;
                }
                // Keep the Frame between 0 and the total frames, minus one.
                this.frame = this.frame % this.framecount;
                totalElapsed -= this.timePerFrame;
            }
        }

        public void DrawFrame(SpriteBatch batch)
        {
            DrawFrame(batch, this.frame);
        }

        public void DrawFrame(SpriteBatch batch, int frame)
        {
            int FrameWidth = idleAnimation.Width / framecount;
            Rectangle sourceRect = new Rectangle(FrameWidth * frame, 0, FrameWidth, idleAnimation.Height);

            SpriteEffects effect = SpriteEffects.None;
            if (this.mirror)
            {
                effect = SpriteEffects.FlipHorizontally;
            }

            if (this.isAttacking)
            {
                batch.Draw(this.attackingAnimation, this.position, sourceRect, Color.White, this.rotation, Vector2.Zero, this.scale, effect, this.depth);
            }
            else
            {
                batch.Draw(this.idleAnimation, this.position, sourceRect, Color.White, this.rotation, Vector2.Zero, this.scale, effect, this.depth);
            }
        }
    }
}
