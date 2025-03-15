using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Shogi.Utils;

namespace Shogi.Pieces;

class Lance : Piece
{
    public Lance(Vector2 position, bool invertedDirection) : base(position, invertedDirection)
    {
        PieceName = new Dictionary<string, string>
        {
            { "jp", "香" },
            { "en", "L" }
        };

        PromotedPieceName = new Dictionary<string, string>
        {
            { "jp", "成香" },
            { "en", "Promoted Lance" }
        };

        PossibleMoves =
        [
            new(Movement.Forward, invertedDirection, 9)
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
