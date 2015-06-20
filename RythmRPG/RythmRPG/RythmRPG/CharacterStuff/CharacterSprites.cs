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
        /// <summary>
        /// Name of the idle sprite
        /// </summary>
        public String idleSpriteName { get; set; }

        /// <summary>
        /// Name of the attacking sprite
        /// </summary>
        public String attackingSpriteName { get; set; }

        /// <summary>
        /// Image of the character when idle
        /// </summary>
        public Texture2D idleSprite { get; set; }

        /// <summary>
        /// Image of the character while attacking
        /// </summary>
        public Texture2D attackingSprite { get; set; }

        /// <summary>
        /// The position of the sprites
        /// </summary>
        public Vector2 position { get; set; }
        /// <summary>
        /// The size of the sprites
        /// </summary>
        public Vector2 size { get; set; }

        /// <summary>
        /// True when attacking, false otherwise
        /// </summary>
        public bool isAttacking { get; set; }

        public CharacterSprites(string idleSpriteName, string attackingSpriteName, Vector2 position, Vector2 size)
        {
            this.isAttacking = false;
            this.idleSpriteName = idleSpriteName;
            this.attackingSpriteName = attackingSpriteName;
        }

        public void LoadContent(ContentManager content)
        {
            this.idleSprite = content.Load<Texture2D>(this.idleSpriteName);
            this.attackingSprite = content.Load<Texture2D>(this.attackingSpriteName);
        }

        public void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            
            if(this.isAttacking)
            {
                spriteBatch.Draw(this.attackingSprite, this.position, null, Color.White, 0, Vector2.Zero, this.size, SpriteEffects.None, 0);
            }
            else
            {
                spriteBatch.Draw(this.idleSprite, this.position, null, Color.White, 0, Vector2.Zero, this.size, SpriteEffects.None, 0);
            }

            spriteBatch.End();
        }
    }
}
