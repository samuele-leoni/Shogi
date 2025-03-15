using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Shogi.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Shogi.Rendering
{
    class PromotionDialog
    {
        private bool isActive;
        private bool response;
        private Rectangle dialogBox;
        private Rectangle yesButton;
        private Rectangle noButton;
        private SpriteFont font;
        private Texture2D backgroundTexture;
        private Texture2D buttonTexture;
        private MouseState currentMouseState;
        private MouseState previousMouseState;

        public delegate void ResponseCallback(bool shouldPromote);
        private ResponseCallback callback;

        public PromotionDialog(GraphicsDevice graphicsDevice, SpriteFont font, Texture2D backgroundTexture, Texture2D buttonTexture)
        {
            this.font = font;
            this.backgroundTexture = backgroundTexture;
            this.buttonTexture = buttonTexture;

            // Definisci le dimensioni e la posizione della finestra di dialogo
            dialogBox = new Rectangle(
                graphicsDevice.Viewport.Width / 2 - 150,
                graphicsDevice.Viewport.Height / 2 - 100,
                300,
                200
            );

            // Definisci i pulsanti "Sì" e "No"
            yesButton = new Rectangle(
                dialogBox.X + 40,
                dialogBox.Y + 130,
                100,
                40
            );

            noButton = new Rectangle(
                dialogBox.X + 160,
                dialogBox.Y + 130,
                100,
                40
            );

            isActive = false;
        }

        public bool IsActive => isActive;

        public void Show(ResponseCallback callback)
        {
            isActive = true;
            this.callback = callback;
        }

        public void Hide()
        {
            isActive = false;
            callback = null;
        }

        public void Update(GameTime gameTime)
        {
            if (!isActive)
                return;

            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            Point mousePosition = new Point(currentMouseState.X, currentMouseState.Y);

            // Controlla se il mouse è stato cliccato su uno dei pulsanti
            if (previousMouseState.LeftButton == ButtonState.Pressed &&
                currentMouseState.LeftButton == ButtonState.Released)
            {
                if (yesButton.Contains(mousePosition))
                {
                    response = true;
                    callback?.Invoke(response);
                    Hide();
                }
                else if (noButton.Contains(mousePosition))
                {
                    response = false;
                    callback?.Invoke(response);
                    Hide();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!isActive)
                return;

            // Disegna un'ombra semi-trasparente su tutto lo schermo
            spriteBatch.Draw(
                backgroundTexture,
                new Rectangle(0, 0, spriteBatch.GraphicsDevice.Viewport.Width, spriteBatch.GraphicsDevice.Viewport.Height),
                new Color(0, 0, 0, 128)
            );

            // Disegna la finestra di dialogo
            spriteBatch.Draw(backgroundTexture, dialogBox, Color.LightGray);

            // Disegna il testo della domanda
            string question = "Do you want to promote this piece?";
            Vector2 textSize = font.MeasureString(question);
            spriteBatch.DrawString(
                font,
                question,
                new Vector2(dialogBox.X + dialogBox.Width / 2 - textSize.X / 2, dialogBox.Y + 40),
                Color.Black
            );

            // Disegna i pulsanti
            spriteBatch.Draw(buttonTexture, yesButton, Color.White);
            spriteBatch.Draw(buttonTexture, noButton, Color.White);

            // Disegna il testo sui pulsanti
            Vector2 yesTextSize = font.MeasureString("Yes");
            Vector2 noTextSize = font.MeasureString("No");

            spriteBatch.DrawString(
                font,
                "Yes",
                new Vector2(yesButton.X + yesButton.Width / 2 - yesTextSize.X / 2, yesButton.Y + yesButton.Height / 2 - yesTextSize.Y / 2),
                Color.Black
            );

            spriteBatch.DrawString(
                font,
                "No",
                new Vector2(noButton.X + noButton.Width / 2 - noTextSize.X / 2, noButton.Y + noButton.Height / 2 - noTextSize.Y / 2),
                Color.Black
            );
        }
    }
}

