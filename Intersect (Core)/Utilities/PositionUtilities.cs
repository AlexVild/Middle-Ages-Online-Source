
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

        public static Point GetFlippedBottomLeftCorner(
            Directions direction,
            int bottomLeftX,
            int bottomLeftY,
            int width,
            int height
        )
        {
            var rotatedCorner = RotateXYToDirection(direction, bottomLeftX, bottomLeftY);

            // Because we're dealing with tiles
            width -= 1;
            height -= 1;

            switch (direction)
            {
                // No change
                case Directions.Up:
                    return new Point(bottomLeftX, bottomLeftY);
                // Rotated is now Top-Left
                case Directions.Right:
                    return new Point(rotatedCorner.X, rotatedCorner.Y + width);
                // Rotated is now Top-Right
                case Directions.Down:
                    return new Point(rotatedCorner.X - width, rotatedCorner.Y + height);
                // Rotated is now Bottom-Right
                case Directions.Left:
                    return new Point(rotatedCorner.X - height, rotatedCorner.Y);
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), "Invalid direction provided.");
            }
        }

    }
}
