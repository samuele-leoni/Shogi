using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Shogi.Utils;

namespace Shogi.Pieces;

class Bishop : Piece
{
    public Bishop(Vector2 position, bool invertedDirection) : base(position, invertedDirection)
    {
        PieceName = new Dictionary<string, string>
            {
            { "jp", "角" },
            { "en", "Bishop" }
        };

        PromotedPieceName = new Dictionary<string, string>
            {
            { "jp", "馬" },
            { "en", "H" }
        };

        PossibleMoves =
        [
            new(Movement.ForwardRight, invertedDirection, 9),
            new(Movement.ForwardLeft, invertedDirection, 9),
            new(Movement.BackwardRight, invertedDirection, 9),
            new(Movement.BackwardLeft, invertedDirection, 9)
        ];

        PromotedPossibleMoves =
        [
            new(Movement.Forward, invertedDirection),
            new(Movement.Right, invertedDirection),
            new(Movement.Left, invertedDirection),
            new(Movement.Backward, invertedDirection),
            new(Movement.ForwardRight, invertedDirection, 9),
            new(Movement.ForwardLeft, invertedDirection, 9),
            new(Movement.BackwardRight, invertedDirection, 9),
            new(Movement.BackwardLeft, invertedDirection, 9)
        ];
    }
}
