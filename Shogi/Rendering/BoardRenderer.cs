using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shogi.Pieces;
using Microsoft.Xna.Framework.Content;

namespace Shogi.Rendering
{
    /// <summary>
    /// Class that handles rendering of the game board
    /// </summary>
    public class BoardRenderer
    {
        #region Constants
        // Constants for the board rendering
        public static readonly int BOARD_SIZE = 9;
        public static readonly int CELL_SIZE = 70; // pixel size of each cell

        // Constants for the color of the board cells
        private readonly Color CELL_COLOR_BASE = new(183, 141, 95); // beige
        private readonly Color CELL_COLOR_SELECTED = new(255, 255, 0, 10); // semi-transparent yellow
        #endregion

        #region Attributes
        // MonoGame graphics objects
        private GraphicsDevice graphicsDevice;
        private SpriteBatch spriteBatch;
        private Board _gameBoard;

        private PieceRenderer pieceRenderer;

        // Texture for the board cells
        private Texture2D cellTexture;

        // Board position on the screen
        private Vector2 boardPosition;

        // Used to manage the selected cell, therefore the selected piece
        private Vector2? selectedCell;

        // Used to manage the mouse input
        private bool isMousePressed = false;

        // Dictionary to store textures used for rendering  
        private readonly Dictionary<string, Texture2D> boardTextures = [];
        #endregion

        #region Initialization
        /// <summary>
        /// Constructor for the BoardRenderer class
        /// </summary>
        /// <param name="graphicsDevice"> The GraphicsDevice used to render the game </param>
        /// <param name="gameBoard"> The game board to be rendered </param>
        public BoardRenderer(ContentManager content, GraphicsDevice graphicsDevice, Board gameBoard, SpriteFont piecesFont)
        {
            this.graphicsDevice = graphicsDevice;
            spriteBatch = new SpriteBatch(graphicsDevice);
            boardPosition = new Vector2(100, 50); // posizione della tavola sullo schermo
            _gameBoard = gameBoard;
            pieceRenderer = new PieceRenderer(content, graphicsDevice, spriteBatch, piecesFont, CELL_SIZE);

            // Initializes the selected cell attribute to null
            selectedCell = null;

            // Initializes the textures used for rendering
            InitializeTextures();
        }

        /// <summary>
        /// Initializes the textures used for rendering
        /// </summary>
        private void InitializeTextures()
        {
            // Create the texture for the board cells
            cellTexture = new Texture2D(graphicsDevice, CELL_SIZE, CELL_SIZE);

            Color[] cellData = new Color[CELL_SIZE * CELL_SIZE];

            // Fill the texture with the light color
            for (int i = 0; i < cellData.Length; i++)
            {
                cellData[i] = CELL_COLOR_BASE;
            }

            cellTexture.SetData(cellData);
        }
        #endregion

        #region GameRelatedLogic
        /// <summary>
        /// Updates the board renderer
        /// </summary>
        /// <param name="gameTime">GameTime object</param>
        public void Update(GameTime gameTime)
        {
            // Handles user input
            HandleInput();
        }

        /// <summary>
        /// Handles user input for selecting pieces on the board
        /// </summary>
        private void HandleInput()
        {
            MouseState mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed && !isMousePressed)
            {
                isMousePressed = true;
                Point mousePosition = new(mouseState.X, mouseState.Y);

                // Check if the mouse position is on the board
                if (IsPointOnBoard(mousePosition))
                {
                    // Calculate the cell position based on the mouse position
                    int cellX = (int)((mousePosition.X - boardPosition.X) / CELL_SIZE);
                    int cellY = (int)((mousePosition.Y - boardPosition.Y) / CELL_SIZE);

                    // Invert the cell X position to match the board orientation
                    cellX = BOARD_SIZE - 1 - cellX;

                    if (selectedCell.Equals(new Vector2(cellX, cellY)) || _gameBoard.GetPieceAt(cellX, cellY) == null)
                    {
                        // Deselect the cell if it was already selected
                        selectedCell = null;
                    }
                    else
                    {
                        // Update the selected cell accordingly
                        selectedCell = new(cellX, cellY);
                    }
                }
            } 
            else if(mouseState.LeftButton == ButtonState.Released)
            {
                isMousePressed = false;
            }
        }

        /// <summary>
        /// Highlight the selected cell
        /// </summary>
        /// <param name="position">Cell Position</param>
        private void DrawSelectedCell(Vector2 position)
        {
            // Disegna un overlay semi-trasparente per evidenziare la cella selezionata
            if (!boardTextures.TryGetValue("highlight", out Texture2D value))
            {
                Texture2D highlight = new(graphicsDevice, CELL_SIZE, CELL_SIZE);
                Color[] data = new Color[CELL_SIZE * CELL_SIZE];
                for (int i = 0; i < data.Length; i++)
                {
                    data[i] = CELL_COLOR_SELECTED; // giallo semi-trasparente
                }
                highlight.SetData(data);
                value = highlight;
                boardTextures["highlight"] = value;
            }

            spriteBatch.Draw(value, position, Color.White);
        }

        /// <summary>
        /// Checks if a given point is on the board
        /// </summary>
        /// <param name="point">Point to check</param>
        /// <returns>True if the point is on the board, false otherwise</returns>
        private bool IsPointOnBoard(Point point)
        {
            return point.X >= boardPosition.X &&
                   point.X < boardPosition.X + BOARD_SIZE * CELL_SIZE &&
                   point.Y >= boardPosition.Y &&
                   point.Y < boardPosition.Y + BOARD_SIZE * CELL_SIZE;
        }
        #endregion

        #region Drawing
        /// <summary>
        /// Draws the game board
        /// </summary>
        public void Draw()
        {
            spriteBatch.Begin();

            // Draw the board cells
            for (int y = 0; y < BOARD_SIZE; y++)
            {
                for (int x = 0; x < BOARD_SIZE; x++)
                {
                    // Calculate the position of the cell
                    Vector2 position = boardPosition + new Vector2(x * CELL_SIZE, y * CELL_SIZE);

                    // Draw the cell
                    spriteBatch.Draw(cellTexture, position, Color.White);

                    // Draw the cell border
                    DrawCellBorder(position);

                    // Highlight the selected cell
                    if (selectedCell.HasValue &&
                        BOARD_SIZE - 1 - x == selectedCell.Value.X &&
                        y == selectedCell.Value.Y)
                    {
                        DrawSelectedCell(position);
                    }
                }
            }

            // Draw the pieces on the board
            DrawPieces();

            spriteBatch.End();
        }

        /// <summary>
        /// Draw the border of a cell
        /// </summary>
        /// <param name="position">Cell position</param>
        private void DrawCellBorder(Vector2 position)
        {
            // Calculate the border lines of the cell
            Vector2[] borderLines =
            [
                new Vector2(position.X, position.Y),
                new Vector2(position.X + CELL_SIZE, position.Y),
                new Vector2(position.X + CELL_SIZE, position.Y + CELL_SIZE),
                new Vector2(position.X, position.Y + CELL_SIZE),
                new Vector2(position.X, position.Y)
            ];

            // Draw the border lines
            for (int i = 0; i < borderLines.Length - 1; i++)
            {
                DrawLine(borderLines[i], borderLines[i + 1], Color.Black);
            }
        }

        /// <summary>
        /// Draw a line on the board
        /// </summary>
        /// <param name="start">Starting point</param>
        /// <param name="end">Ending point</param>
        /// <param name="color">Line color</param>
        private void DrawLine(Vector2 start, Vector2 end, Color color)
        {
            Vector2 edge = end - start;
            float angle = (float)Math.Atan2(edge.Y, edge.X);
            float length = edge.Length();

            if (!boardTextures.TryGetValue("line", out Texture2D value))
            {
                value = new Texture2D(graphicsDevice, 1, 1);
                value.SetData([Color.White]);
                boardTextures["line"] = value;
            }

            spriteBatch.Draw(value, start, null, color, angle, Vector2.Zero, new Vector2(length, 1), SpriteEffects.None, 0);
        }

        /// <summary>
        /// Draw the pieces on the board
        /// </summary>
        private void DrawPieces()
        {
            for (int y = 0; y < BOARD_SIZE; y++)
            {
                for (int x = 0; x < BOARD_SIZE; x++)
                {
                    // Calculate the position of the cell on the board
                    int boardX = BOARD_SIZE - 1 - x;
                    int boardY = y;

                    // Get the piece at the current cell
                    Piece piece = _gameBoard.GetPieceAt(boardX, boardY);

                    if (piece != null)
                    {
                        // Draw the piece on the board
                        pieceRenderer.DrawPiece(piece, boardPosition);
                    }
                }
            }
        }
        #endregion

    }
}
