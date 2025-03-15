using Microsoft.Xna.Framework;
using Shogi.Pieces;
using System.Collections.Generic;
using System.Linq;

namespace Shogi
{
    public class Board
    {
        public Vector2 Position { get; private set; }

        private Piece[,] board;

        /// <summary>
        /// Get all pieces on the board
        /// </summary>
        /// <returns> List of all pieces on the board </returns>
        public List<Piece> GetPieces() {
            return [.. board.Cast<Piece>().Where(p => p != null)];
        }
            

        public Board()
        {
            board = new Piece[9, 9];
            Position = new Vector2(0, 0);

            SetupBoard();
        }

        /// <summary>
        /// Setup the board with the pieces in their initial positions
        /// </summary>
        public void SetupBoard()
        {
            InitializeBackRank(0, true);
            InitializeBackRank(8, false);

            for (int i = 0; i < 9; i++)
            {
                board[i, 2] = new Pawn(new Vector2(i, 2), true);
                board[i, 6] = new Pawn(new Vector2(i, 6), false);
            }

            board[7, 1] = new Bishop(new Vector2(7, 1), true);
            board[1, 7] = new Bishop(new Vector2(1, 7), false);

            board[1, 1] = new Rook(new Vector2(1, 1), true);
            board[7, 7] = new Rook(new Vector2(7, 7), false);
        }

        /// <summary>
        /// Helper method to initialize the back rank of the board
        /// </summary>
        /// <param name="row"> Row to initialize </param>
        /// <param name="invertedDirection"> If true, the pieces move in the opposite direction </param>
        private void InitializeBackRank(int row, bool invertedDirection)
        {
            board[0, row] = new Lance(new Vector2(0, row), invertedDirection);
            board[8, row] = new Lance(new Vector2(8, row), invertedDirection);

            board[1, row] = new Knight(new Vector2(1, row), invertedDirection);
            board[7, row] = new Knight(new Vector2(7, row), invertedDirection);

            board[2, row] = new Silver(new Vector2(2, row), invertedDirection);
            board[6, row] = new Silver(new Vector2(6, row), invertedDirection);

            board[3, row] = new Gold(new Vector2(3, row), invertedDirection);
            board[5, row] = new Gold(new Vector2(5, row), invertedDirection);

            board[4, row] = new King(new Vector2(4, row), invertedDirection);
        }

        /// <summary>
        /// Get the piece at the specified position
        /// </summary>
        /// <param name="row"> Row </param>
        /// <param name="col"> Column </param>
        /// <returns></returns>
        public Piece GetPieceAt(int row, int col)
        {
            if (IsValidPosition(row, col))
            {
                return board[row, col];
            }
            return null;
        }

        /// <summary>
        /// Check if the position is within the board bounds
        /// </summary>
        /// <param name="row"> Row </param>
        /// <param name="col"> Column </param>
        /// <returns> True if the position is valid, false otherwise </returns>
        private static bool IsValidPosition(int row, int col)
        {
            return row >= 0 && row < 9 && col >= 0 && col < 9;
        }

        /// <summary>
        /// Move a piece to the specified position
        /// </summary>
        /// <param name="piece"> Piece to move </param>
        /// <param name="destRow"> Destination row </param>
        /// <param name="destCol"> Destination column </param>
        /// <returns> True if the move was successful, false otherwise </returns>
        public bool MovePiece(Piece piece, int destRow, int destCol)
        {
            if (IsValidPosition(destRow, destCol))
            {
                board[destRow, destCol] = piece;
                board[(int)piece.Position.X, (int)piece.Position.Y] = null;
                piece.Position = new Vector2(destRow, destCol);
                return true;
            }
            return false;
        }
    }
}
