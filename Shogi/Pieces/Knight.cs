using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Shogi.Utils;

namespace Shogi.Pieces;

class Knight : Piece
{
    public Knight(Vector2 position, bool invertedDirection) : base(position, invertedDirection)
    {
        PieceName = new Dictionary<string, string>
            {
            { "jp", "桂" },
            { "en", "Kn" }
        };

        PromotedPieceName = new Dictionary<string, string>
            {
            { "jp", "圭" },
            { "en", "Gin" }
        };

        PossibleMoves =
        [
            // TODO: Implement Knight movement
        ];

        PromotedPossibleMoves =
        [
            new(Movement.Forward, invertedDirection),
            new(Movement.Right, invertedDirection),
            new(Movement.Left, invertedDirection),
            new(Movement.Backward, invertedDirection),
            new(Movement.ForwardRight, invertedDirection),
            new(Movement.ForwardLeft, invertedDirection)
        ];
    }
}

