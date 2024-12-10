
using Intersect.Enums;
using System;

namespace Intersect.Utilities
{
    public static class PositionUtilities
    {
        public static Point RotateXYToDirection(int direction, int x, int y)
        {
            return RotateXYToDirection((Directions)direction, x, y);
        }

        public static Point RotateXYToDirection(Directions direction, int x, int y)
        {
            switch (direction)
            {
                case Directions.Up:
                    return new Point(x, y);
                case Directions.Right:
                    return new Point(-y, x);
                case Directions.Down:
                    return new Point(-x, -y);
                case Directions.Left:
                    return new Point(y, -x);
                default:
                    throw new ArgumentOutOfRangeException($"Invalid direction passed when translating points: {direction}");
            }
        }
    }
}
