using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Shogi.Utils;

namespace Shogi.Pieces;

class Gold : Piece
{
    public Gold(Vector2 Position, bool invertedDirection) : base(Position, invertedDirection)
    {
        IsPromotable = false;

        PieceName = new Dictionary<string, string>
        {
            { "jp", "金" },
            { "en", "Gold" }
        };

        PossibleMoves =
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
