using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Shogi.Utils;

namespace Shogi.Pieces;

class Rook : Piece
{
    public Rook(Vector2 position, bool invertedDirection) : base(position, invertedDirection)
    {
        PieceName = new Dictionary<string, string>
        {
            { "jp", "飛" },
            { "en", "R" }
        };

        PromotedPieceName = new Dictionary<string, string>
        {
            { "jp", "竜" },
            { "en", "+R" }
        };

        PossibleMoves =
        [
            new(Movement.Forward, invertedDirection, 9),
            new(Movement.Right, invertedDirection, 9),
            new(Movement.Left, invertedDirection, 9),
            new(Movement.Backward, invertedDirection, 9)
        ];

        PromotedPossibleMoves =
        [
            new(Movement.Forward, invertedDirection, 9),
            new(Movement.Right, invertedDirection, 9),
            new(Movement.Left, invertedDirection, 9),
            new(Movement.Backward, invertedDirection, 9),
            new(Movement.ForwardRight, invertedDirection),
            new(Movement.ForwardLeft, invertedDirection)
        ];
    }
}
