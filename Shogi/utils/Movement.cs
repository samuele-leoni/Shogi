
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;

namespace Shogi.Utils
{
    // All movements are relative to the piece's position and direction facing
    public class Movement
    {
        public static readonly Vector2 Forward = new(0, -1);
        public static readonly Vector2 Backward = new(0, 1);
        public static readonly Vector2 Right = new(1, 0);
        public static readonly Vector2 Left = new(-1, 0);
        public static readonly Vector2 ForwardRight = Forward + Right;
        public static readonly Vector2 ForwardLeft = Forward + Left;
        public static readonly Vector2 BackwardRight = Backward + Right;
        public static readonly Vector2 BackwardLeft = Backward + Left;
        public static readonly Vector2 KnightRight = new(1, -2);
        public static readonly Vector2 KnightLeft = new(-1, -2);

        public Vector2 Direction { get; private set; }
        public int Repetitions { get; private set; }

        public Movement(Vector2 direction, bool invertedDirection, int repetitions = 0)
        {
            int multiplier = invertedDirection ? -1 : 1;
            Direction = direction * multiplier;
            Repetitions = repetitions;
        }
    }
}