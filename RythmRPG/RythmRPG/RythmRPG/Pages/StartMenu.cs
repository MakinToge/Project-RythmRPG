using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG.Pages {
    public class StartMenu : Page{
        public Sprite MainImage { get; set; }
        public Sprite Start { get; set; }
        public Sprite Options { get; set; }
        public Sprite[] SaveSprites { get; set; }
        public Sprite LeftSave { get; set; }
        public Sprite RightSave { get; set; }

        public override void Initialize() {
            this.MainImage = new Sprite(0, 0, Game1.Width, Game1.Height);
            this.Start = new Sprite(3 * Game1.UnitX, 4 * Game1.UnitY, Game1.ButtonWidth, 2 * Game1.ButtonHeight);
            this.Options = new Sprite(3 * Game1.UnitX, 8 * Game1.UnitY, Game1.ButtonWidth, 2* Game1.ButtonHeight);
            this.SaveSprites = new Sprite[Save.NB_SAVE];
            for (int i = 0; i < Save.NB_SAVE; i++) {
                this.SaveSprites[i] = new Sprite(5 * Game1.UnitX, 13 * Game1.UnitY, 4 * Game1.UnitX, Game1.UnitY);
            }
            this.LeftSave = new Sprite(3 * Game1.UnitX, 13 * Game1.UnitY, Game1.UnitX, Game1.UnitY);
            this.RightSave = new Sprite(10 * Game1.UnitX, 13 * Game1.UnitY, Game1.UnitX, Game1.UnitY);
            
        }
        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content) {
            this.MainImage.LoadContent(content, "StartMenu/StartMenu");
            this.Start.LoadContent(content, "StartMenu/Start");
            this.Options.LoadContent(content, "StartMenu/Options");
            this.LeftSave.LoadContent(content, "Options/ArrowLeft");
            this.RightSave.LoadContent(content, "Options/ArrowRight");
            for (int i = 0; i < Save.NB_SAVE; i++) {
                this.SaveSprites[i].LoadContent(content, "StartMenu/Save" + (i+1));
            }
        }
        public override void HandleInput(Microsoft.Xna.Framework.Input.KeyboardState previousKeyboardState, Microsoft.Xna.Framework.Input.KeyboardState currentKeyboardState, Microsoft.Xna.Framework.Input.MouseState previousMouseState, Microsoft.Xna.Framework.Input.MouseState currentMouseState) {
            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released) {
                Rectangle mouse = new Rectangle(currentMouseState.X,currentMouseState.Y ,10,10);

                if (isOver(mouse, Options)) {
                    Game1.GameState = GameState.Options;
                }
                else if (isOver(mouse, Start)) {
                    Game1.GameState = GameState.GameMenu;
                }
                else if (isOver(mouse, LeftSave) && Game1.Save.SelectedSave > 0) {
                    Game1.Save.SelectedSave -= 1;
                }
                else if (isOver(mouse, RightSave) && Game1.Save.SelectedSave < Save.NB_SAVE - 1) {
                    Game1.Save.SelectedSave += 1;
                }
            }
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.GameTime gameTime) {
            this.MainImage.Draw(spriteBatch, gameTime);
            this.Start.Draw(spriteBatch, gameTime);
            this.Options.Draw(spriteBatch, gameTime);
            this.LeftSave.Draw(spriteBatch, gameTime);
            this.RightSave.Draw(spriteBatch, gameTime);
            this.SaveSprites[Game1.Save.SelectedSave].Draw(spriteBatch, gameTime);
        }
    }
}
