using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Shogi.Pieces;
using System;
using System.Collections.Generic;

namespace Shogi.Rendering
{
    /// <summary>
    /// Class that handles rendering of Shogi pieces on the game board.
    /// </summary>
    /// <param name="graphicsDevice"> The graphics device used to render the game </param>
    /// <param name="spriteBatch"> The sprite batch used to draw the game </param>
    /// <param name="font"> The font used to render pieces characters depending on the chosen language </param>
    /// <param name="cellSize"> The size of each cell on the game board </param>
    public class PieceRenderer(ContentManager content, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, SpriteFont font, int cellSize)
    {
        private readonly GraphicsDevice _graphicsDevice = graphicsDevice;
        private readonly SpriteBatch _spriteBatch = spriteBatch;
        private readonly SpriteFont _font = font;
        private readonly int _cellSize = cellSize;
        private readonly ContentManager _content = content;

        // Texture caching for pieces to avoid recreating them every frame
        private readonly Dictionary<string, Texture2D> _pieceTextures = [];

        /// <summary>
        /// Draw a piece on the board at the specified position.
        /// </summary>
        /// <param name="piece">The generic Piece to be rendered</param>
        /// <param name="boardPosition">The position on the game board where the Piece will be rendered</param>
        /// <param name="isSelected">Manages the Piece click event</param>
        public void DrawPiece(Piece piece,  Vector2 offset)
        {
            if (piece == null)
            {
                return;
            }

            // Calculate the position of the piece on the board
            Vector2 position = new(
                offset.X + (BoardRenderer.BOARD_SIZE - 1 - piece.Position.X) * _cellSize,
                offset.Y + piece.Position.Y * _cellSize
            );

            // Get the piece name based on the game language, piece type and promotion status
            string pieceName = ShogiGame.GameLanguage + piece.GetType().Name + (piece.IsPromoted ? "+" : "");
            
            // Get the piece texture based on the piece type
            Texture2D texture = _content.Load<Texture2D>($"Pieces\\{pieceName}");

            // Draw the piece texture scaled to the cell size and rotated if piece.InvertedDirection is true
            _spriteBatch.Draw(
                texture, 
                position, 
                null, 
                Color.White, 
                piece.InvertedDirection ? MathHelper.Pi : 0, 
                piece.InvertedDirection ? new(texture.Width, texture.Height-50) : new(0, 50),
                (float)_cellSize / texture.Width, 
                SpriteEffects.None, 
                0
            );


            _pieceTextures[pieceName] = texture;
        }
    }
}