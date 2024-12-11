
using Intersect.Enums;
using Intersect.GameObjects;
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
           int direction,
           int bottomLeftX,
           int bottomLeftY,
           int width,
           int height
       )
        {
            return GetFlippedBottomLeftCorner((Directions) direction, bottomLeftX, bottomLeftY, width, height);
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

        public static Point GetAoeOffset(
            int dir,
            int xOffset,
            int yOffset,
            AoeShape shape,
            int width,
            int height
        )
        {
            return GetAoeOffset(
                (Directions)dir,
                xOffset,
                yOffset,
                shape,
                width,
                height
            );
        }

        public static Point GetAoeOffset(
            Directions dir,
            int xOffset,
            int yOffset,
            AoeShape shape,
            int width,
            int height
        )
        {
            // Offset such that our position is the bottom-right of the rectangle
            if (shape == AoeShape.Rectangle)
            {
                var bottomLeftRect = GetFlippedBottomLeftCorner(
                    dir,
                    xOffset,
                    yOffset,
                    width,
                    height);

                xOffset = bottomLeftRect.X;
                yOffset = bottomLeftRect.Y;
            }
            else if (shape == AoeShape.Circle)
            {
                var rotatedStart = RotateXYToDirection(dir, xOffset, yOffset);
                xOffset = rotatedStart.X;
                yOffset = rotatedStart.Y;
            }

            return new Point(xOffset, yOffset);
        }

    }
}
