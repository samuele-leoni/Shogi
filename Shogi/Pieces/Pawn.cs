using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Shogi.Utils;

namespace Shogi.Pieces;

class Pawn : Piece
{
    public Pawn(Vector2 position, bool invertedDirection) : base(position, invertedDirection)
    {
        PieceName = new Dictionary<string, string>
        {
            { "jp", "歩" },
            { "en", "P" }
        };

        PromotedPieceName = new Dictionary<string, string>
        {
            { "jp", "と" },
            { "en", "Promoted Pawn" }
        };

        PossibleMoves =
        [
            new(Movement.Forward, invertedDirection)
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
