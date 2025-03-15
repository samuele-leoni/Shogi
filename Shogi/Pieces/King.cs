using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Shogi.Utils;

namespace Shogi.Pieces;

class King : Piece
{
    public King(Vector2 position, bool invertedDirection) : base(position, invertedDirection)
    {
        IsPromotable = false;

        PieceName = new Dictionary<string, string>
        {
            { "jp", invertedDirection ? "玉将" : "王将" },
            { "en", "King" }
        };

        PossibleMoves =
        [
            new(Movement.Forward, invertedDirection),
            new(Movement.Right, invertedDirection),
            new(Movement.Left, invertedDirection),
            new(Movement.Backward, invertedDirection),
            new(Movement.ForwardRight, invertedDirection),
            new(Movement.ForwardLeft, invertedDirection),
            new(Movement.BackwardRight, invertedDirection),
            new(Movement.BackwardLeft, invertedDirection)
        ];
    }
}
