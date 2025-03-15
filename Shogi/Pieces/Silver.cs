using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Shogi.Utils;

namespace Shogi.Pieces;

class Silver : Piece
{
    public Silver(Vector2 position, bool invertedDirection) : base(position, invertedDirection)
    {
        PieceName = new Dictionary<string, string>
        {
            { "jp", "銀" },
            { "en", "Silver General" }
        };

        PromotedPieceName = new Dictionary<string, string>
        {
            { "jp", "成銀" },
            { "en", "Promoted Silver General" }
        };

        PossibleMoves =
        [
                new(Movement.Forward, invertedDirection),
                new(Movement.ForwardRight, invertedDirection),
                new(Movement.ForwardLeft, invertedDirection),
                new(Movement.BackwardRight, invertedDirection),
                new(Movement.BackwardLeft, invertedDirection)
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