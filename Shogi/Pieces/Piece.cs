using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Shogi.Utils;

namespace Shogi.Pieces
{
    /// <summary>
    /// Base class for all pieces
    /// </summary>
    /// <param name="position"> Piece position on the board </param>
    /// <param name="invertedDirection"> If true, the piece moves in the opposite direction </param>
    public abstract class Piece(Vector2 position, bool invertedDirection)
    {
        /// <summary>
        /// Piece position on the board
        /// </summary>
        public Vector2 Position { get; set; } = position;
        /// <summary>
        /// If true, the piece moves in the opposite direction
        /// </summary>
        public bool InvertedDirection { get; private set; } = invertedDirection;
        /// <summary>
        /// If true, the piece is promoted, default is false
        /// </summary>
        public bool IsPromoted { get; private set; } = false;
        /// <summary>
        /// If true, the piece can be promoted, default is true
        /// </summary>
        public bool IsPromotable { get; protected set; } = true;

        /// <summary>
        /// Piece character which gets rendered on screen, based on the language 
        /// <para>"en" for English, "jp" for Japanese</para>
        /// </summary>
        public Dictionary<string, string> PieceName { get; protected set; } = new Dictionary<string, string>();
        /// <summary>
        /// Promoted piece character which gets rendered on screen, based on the language
        /// <para>"en" for English, "jp" for Japanese</para>
        /// </summary>
        public Dictionary<string, string> PromotedPieceName { get; protected set; } = new Dictionary<string, string>();

        /// <summary>
        /// List of possible movements for the piece setted in the subclass constructor
        /// </summary>
        public List<Movement> PossibleMoves { get; protected set; } = new List<Movement>();
        /// <summary>
        /// List of possible movements for the promoted piece setted in the subclass constructor
        /// </summary>
        public List<Movement> PromotedPossibleMoves { get; protected set; } = new List<Movement>();

        /// <summary>
        /// Handles the promotion of the piece setting the IsPromoted and IsPromotable properties
        /// </summary>
        public void Promote()
        {
            if (!IsPromoted)
            {
                IsPromoted = true;
                IsPromotable = false;
            }
        }

        /// <summary>
        /// Handles the demotion of the piece setting the IsPromoted and IsPromotable properties
        /// </summary>
        public void Demote()
        {
            if (IsPromoted)
            {
                IsPromoted = false;
                IsPromotable = true;
            }
        }

        /// <summary>
        /// Returns the name of the piece based on the language
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public string GetCurrentName(string language)
        {
            // Checks wether the piece is promoted or not and returns the correct name
            // accordingly based also on the chosen language
            if (IsPromoted && PromotedPieceName.TryGetValue(language, out string promotedValue))
            {
                return promotedValue;
            }
            else if (PieceName.TryGetValue(language, out string pieceValue))
            {
                return pieceValue;
            }
            return "";
        }
    }
}