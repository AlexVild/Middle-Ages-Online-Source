using System;
using System.Collections.Generic;
using System.Linq;
using Intersect.Enums;
using Intersect.Logging;
using Intersect.Server.Database;
using Intersect.Server.Entities.Events;
using Intersect.Server.Maps;

namespace Intersect.Server.Entities.Pathfinding
{

    public enum PathfinderResult
    {

        Success,

        OutOfRange,

        NoPathToTarget,

        Failure, //No Map, No Target, Who Knows?

        Wait, //Pathfinder won't run due to recent failures and trying to conserve cpu

    }

    class Pathfinder
    {

        private int mConsecutiveFails;

        private Entity mEntity;

        private IEnumerable<PathNode> mPath;

        private PathfinderTarget mTarget;

        private long mWaitTime;

        private PathNode[,] mGrid = new PathNode[Options.MapWidth * 3, Options.MapHeight * 3];

        public Pathfinder(Entity parent)
        {
            mEntity = parent;
        }

        public void SetTarget(PathfinderTarget target)
        {
            mTarget = target;
        }

        public PathfinderTarget GetTarget()
        {
            return mTarget;
        }

        public PathfinderResult Update(long timeMs)
        {
            //TODO: Pull this out into server config :) 
            var pathfindingRange = Math.Max(
                Options.MapWidth, Options.MapHeight
            ); //Search as far as 1 map out.. maximum.

            //Do lots of logic eventually leading up to an A* pathfinding run if needed.
            var returnVal = PathfinderResult.Failure;
            try
            {
                PathNode[,] mapGrid;
                SpatialAStar aStar;
                var path = mPath;
                if (mWaitTime < timeMs)
                {
                    var currentMap = MapController.Get(mEntity.MapId);
                    if (currentMap != null && mTarget != null)
                    {
                        var grid = DbInterface.GetGrid(currentMap.MapGrid);
                        var gridX = currentMap.MapGridX;
                        var gridY = currentMap.MapGridY;

                        var targetFound = false;
                        var targetX = -1;
                        var targetY = -1;
                        var sourceX = Options.MapWidth + mEntity.X;
                        var sourceY = Options.MapHeight + mEntity.Y;

                        //Loop through surrouding maps to see if our target is even around.
                        for (var x = gridX - 1; x <= gridX + 1; x++)
                        {
                            if (x == -1 || x >= grid.Width)
                            {
                                continue;
                            }

                            for (var y = gridY - 1; y <= gridY + 1; y++)
                            {
                                if (y == -1 || y >= grid.Height)
                                {
                                    continue;
                                }

                                if (grid.MyGrid[x, y] != Guid.Empty)
                                {
                                    if (grid.MyGrid[x, y] == mTarget.TargetMapId)
                                    {
                                        targetX = (x - gridX + 1) * Options.MapWidth + mTarget.TargetX;
                                        targetY = (y - gridY + 1) * Options.MapHeight + mTarget.TargetY;
                                        targetFound = true;
                                    }
                                }
                            }
                        }

                        if (targetFound)
                        {
                            if (AlongPath(mPath, targetX, targetY, mEntity.Passable))
                            {
                                path = mPath;
                                returnVal = PathfinderResult.Success;
                            }
                            else
                            {
                                //See if the target is physically within range:
                                if (Math.Abs(sourceX - targetX) + Math.Abs(sourceY - targetY) < pathfindingRange)
                                {
                                    //Doing good...
                                    mapGrid = mGrid;

                                    for (var x = 0; x < Options.MapWidth * 3; x++)
                                    {
                                        for (var y = 0; y < Options.MapHeight * 3; y++)
                                        {
                                            if (mapGrid[x, y] == null)
                                            {
                                                mapGrid[x, y] = new PathNode(x, y, false);
                                                
                                            }
                                            else
                                            {
                                                mapGrid[x, y].Reset();
                                            }

                                            if (x < sourceX - pathfindingRange ||
                                                    x > sourceX + pathfindingRange ||
                                                    y < sourceY - pathfindingRange ||
                                                    y > sourceY + pathfindingRange)
                                            {
                                                mapGrid[x, y].IsWall = true;
                                            }
                                        }
                                    }

                                    //loop through all surrounding maps.. gather blocking elements, resources, players, npcs, global events, and local events (if this is a local event)
                                    for (var x = gridX - 1; x <= gridX + 1; x++)
                                    {
                                        if (x == -1 || x >= grid.Width)
                                        {
                                            for (var y = 0; y < 3; y++)
                                            {
                                                FillArea(
                                                    mapGrid, (x + 1 - gridX) * Options.MapWidth, y * Options.MapHeight,
                                                    Options.MapWidth, Options.MapHeight
                                                );
                                            }

                                            continue;
                                        }

                                        for (var y = gridY - 1; y <= gridY + 1; y++)
                                        {
                                            if (y == -1 || y >= grid.Height)
                                            {
                                                FillArea(
                                                    mapGrid, (x + 1 - gridX) * Options.MapWidth,
                                                    (y + 1 - gridY) * Options.MapHeight, Options.MapWidth,
                                                    Options.MapHeight
                                                );

                                                continue;
                                            }

                                            if (MapController.TryGetInstanceFromMap(grid.MyGrid[x, y], mEntity.MapInstanceId, out var instance))
                                            {
                                                //Copy the cached array of tile blocks

                                                var blocks = instance.GetCachedBlocks(
                                                    mEntity.GetType() == typeof(Player)
                                                );

                                                foreach (var block in blocks)
                                                {
                                                    mapGrid[(x + 1 - gridX) * Options.MapWidth + block.X,
                                                            (y + 1 - gridY) * Options.MapHeight + block.Y]
                                                        .IsWall = true;
                                                }

                                                //Block of Players, Npcs, and Resources
                                                foreach (var en in instance.GetEntities())
                                                {
                                                    if (!en.IsPassable() && en.X > -1 && en.X < Options.MapWidth && en.Y > -1 && en.Y < Options.MapHeight)
                                                    {
                                                        mapGrid[(x + 1 - gridX) * Options.MapWidth + en.X,
                                                                (y + 1 - gridY) * Options.MapHeight + en.Y]
                                                            .IsWall = true;
                                                    }
                                                }

                                                foreach (var en in instance.GlobalEventInstances)
                                                {
                                                    if (en.Value != null && en.Value.X > -1 && en.Value.X < Options.MapWidth && en.Value.Y > -1 && en.Value.Y < Options.MapHeight)
                                                    {
                                                        foreach (var page in en.Value.GlobalPageInstance)
                                                        {
                                                            if (!page.Passable)
                                                            {
                                                                mapGrid[
                                                                        (x + 1 - gridX) * Options.MapWidth +
                                                                        en.Value.X,
                                                                        (y + 1 - gridY) * Options.MapHeight +
                                                                        en.Value.Y]
                                                                    .IsWall = true;
                                                            }
                                                        }
                                                    }
                                                }

                                                //If this is a local event then we gotta loop through all other local events for the player
                                                if (mEntity.GetType() == typeof(EventPageInstance))
                                                {
                                                    var ev = (EventPageInstance)mEntity;
                                                    if (!ev.Passable && ev.Player != null)

                                                    //Make sure this is a local event
                                                    {
                                                        var player = ev.Player;
                                                        if (player != null)
                                                        {
                                                            if (player.EventLookup.Values.Count >
                                                                Options.MapWidth * Options.MapHeight)
                                                            {
                                                                //Find all events on this map (since events can't switch maps)
                                                                for (var mapX = 0; mapX < Options.MapWidth; mapX++)
                                                                {
                                                                    for (var mapY = 0;
                                                                        mapY < Options.MapHeight;
                                                                        mapY++)
                                                                    {
                                                                        var evt = player.EventExists(new MapTileLoc(
                                                                            ev.MapId, mapX, mapY
                                                                        ));

                                                                        if (evt != null)
                                                                        {
                                                                            if (evt.PageInstance != null &&
                                                                                !evt.PageInstance.Passable &&
                                                                                evt.PageInstance.X > -1 &&
                                                                                evt.PageInstance.Y > -1)
                                                                            {
                                                                                mapGrid[
                                                                                        (x + 1 - gridX) *
                                                                                        Options.MapWidth +
                                                                                        evt.X,
                                                                                        (y + 1 - gridY) *
                                                                                        Options.MapHeight +
                                                                                        evt.Y]
                                                                                    .IsWall = true;
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                var playerEvents = player.EventLookup.Values.ToArray();
                                                                foreach (var evt in playerEvents)
                                                                {
                                                                    if (evt != null &&
                                                                        evt.PageInstance != null &&
                                                                        !evt.PageInstance.Passable &&
                                                                        evt.PageInstance.X > -1 &&
                                                                        evt.PageInstance.Y > -1)
                                                                    {
                                                                        mapGrid[
                                                                                (x + 1 - gridX) * Options.MapWidth +
                                                                                evt.PageInstance.X,
                                                                                (y + 1 - gridY) *
                                                                                Options.MapHeight +
                                                                                evt.PageInstance.Y]
                                                                            .IsWall = true;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    //Optionally, move the along path check down here.. see if each tile is still open before returning success.
                                    //That would be more processor intensive but would also provide ai that recognize blocks in their path quicker.

                                    //Finally done.. let's get a path from the pathfinder.
                                    mapGrid[targetX, targetY].IsWall = false;
                                    aStar = new SpatialAStar(mapGrid);
                                    path = aStar.Search(new Point(sourceX, sourceY), new Point(targetX, targetY), null);
                                    if (path == null)
                                    {
                                        returnVal = PathfinderResult.NoPathToTarget;
                                    }
                                    else
                                    {
                                        returnVal = PathfinderResult.Success;
                                    }
                                }
                                else
                                {
                                    returnVal = PathfinderResult.OutOfRange;
                                }
                            }
                        }
                        else
                        {
                            returnVal = PathfinderResult.OutOfRange;
                        }
                    }
                    else
                    {
                        mPath = null;
                        returnVal = PathfinderResult.Failure;
                    }
                }
                else
                {
                    returnVal = PathfinderResult.Wait;
                }

                switch (returnVal)
                {
                    case PathfinderResult.Success:
                        //Use the same path for at least a second before trying again.
                        mWaitTime = timeMs + 200;
                        mConsecutiveFails = 0;

                        break;
                    case PathfinderResult.OutOfRange:
                        //Npc might immediately find a new target. Give it a 500ms wait but make this wait grow if we keep finding targets out of range.
                        mConsecutiveFails++;
                        mWaitTime = timeMs + mConsecutiveFails * 500;

                        break;
                    case PathfinderResult.NoPathToTarget:
                        //Wait 2 seconds and try again. This will move the npc randomly and might allow other npcs or players to get out of the way
                        mConsecutiveFails++;
                        mWaitTime = timeMs + 1000 + mConsecutiveFails * 500;

                        break;
                    case PathfinderResult.Failure:
                        //Can try again in a second.. we don't waste much processing time on failures
                        mWaitTime = timeMs + 500;
                        mConsecutiveFails = 0;

                        break;
                    case PathfinderResult.Wait:
                        //Nothing to do here.. we are already waiting.
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                mPath = path;
            }
            catch (Exception exception)
            {
                Log.Error(exception);
            }

            return returnVal;
        }

        private void FillArea(PathNode[,] dest, int startX, int startY, int width, int height)
        {
            for (var x = startX; x < startX + width; x++)
            {
                for (var y = startY; y < startY + height; y++)
                {
                    dest[x, y].IsWall = true;
                }
            }
        }

        public bool AlongPath(IEnumerable<PathNode> path, int x, int y, bool exact)
        {
            if (path == null)
            {
                return false;
            }

            var foundUs = false;
            var enm = path.GetEnumerator();
            while (enm.MoveNext())
            {
                if (enm.Current.X - Options.MapWidth == mEntity.X && enm.Current.Y - Options.MapHeight == mEntity.Y)
                {
                    foundUs = true;
                }

                if (foundUs && enm.Current.X == x)
                {
                    if (enm.Current.Y == y || (enm.Current.Y - 1 == y || enm.Current.Y + 1 == y) & !exact)
                    {
                        enm.Dispose();

                        return true;
                    }
                }

                if (foundUs && enm.Current.Y == y)
                {
                    if (enm.Current.X == x || (enm.Current.X - 1 == x || enm.Current.X + 1 == x) & !exact)
                    {
                        enm.Dispose();

                        return true;
                    }
                }
            }

            enm.Dispose();

            return false;
        }

        public static int CalculatePathLength(Entity startEntity, Entity targetEntity)
        {
            if (startEntity == null || targetEntity == null)
            {
                return 9999; // Invalid entities
            }

            // Check if entities are on the same map or adjacent maps
            var startMap = MapController.Get(startEntity.MapId);
            var targetMap = MapController.Get(targetEntity.MapId);

            if (startMap == null || targetMap == null)
            {
                return 9999; // Invalid maps
            }

            var startGrid = DbInterface.GetGrid(startMap.MapGrid);
            var targetGrid = DbInterface.GetGrid(targetMap.MapGrid);

            if (startGrid != targetGrid)
            {
                return 9999; // Entities are not on connected maps
            }

            var startGridX = startMap.MapGridX;
            var startGridY = startMap.MapGridY;
            var targetGridX = targetMap.MapGridX;
            var targetGridY = targetMap.MapGridY;

            // Check if target is within pathfinding range (3x3 map area)
            if (Math.Abs(startGridX - targetGridX) > 1 || Math.Abs(startGridY - targetGridY) > 1)
            {
                return 9999; // Out of range
            }

            var pathfindingRange = Math.Max(Options.MapWidth, Options.MapHeight);

            // Calculate coordinates in the 3x3 grid system
            var sourceX = Options.MapWidth + startEntity.X;
            var sourceY = Options.MapHeight + startEntity.Y;
            var targetX = (targetGridX - startGridX + 1) * Options.MapWidth + targetEntity.X;
            var targetY = (targetGridY - startGridY + 1) * Options.MapHeight + targetEntity.Y;

            // Check if target is within pathfinding range
            if (Math.Abs(sourceX - targetX) + Math.Abs(sourceY - targetY) >= pathfindingRange)
            {
                return 9999; // Out of range
            }

            try
            {
                // Create pathfinding grid
                var mapGrid = new PathNode[Options.MapWidth * 3, Options.MapHeight * 3];

                // Initialize grid
                for (var x = 0; x < Options.MapWidth * 3; x++)
                {
                    for (var y = 0; y < Options.MapHeight * 3; y++)
                    {
                        mapGrid[x, y] = new PathNode(x, y, false);

                        // Mark out-of-range areas as walls
                        if (x < sourceX - pathfindingRange ||
                            x > sourceX + pathfindingRange ||
                            y < sourceY - pathfindingRange ||
                            y > sourceY + pathfindingRange)
                        {
                            mapGrid[x, y].IsWall = true;
                        }
                    }
                }

                // Populate obstacles for surrounding maps
                var grid = startGrid;
                for (var x = startGridX - 1; x <= startGridX + 1; x++)
                {
                    if (x == -1 || x >= grid.Width)
                    {
                        // Fill areas outside grid boundaries
                        for (var y = 0; y < 3; y++)
                        {
                            FillGridArea(mapGrid, (x + 1 - startGridX) * Options.MapWidth, y * Options.MapHeight,
                                Options.MapWidth, Options.MapHeight);
                        }
                        continue;
                    }

                    for (var y = startGridY - 1; y <= startGridY + 1; y++)
                    {
                        if (y == -1 || y >= grid.Height)
                        {
                            // Fill areas outside grid boundaries
                            FillGridArea(mapGrid, (x + 1 - startGridX) * Options.MapWidth,
                                (y + 1 - startGridY) * Options.MapHeight, Options.MapWidth, Options.MapHeight);
                            continue;
                        }

                        if (MapController.TryGetInstanceFromMap(grid.MyGrid[x, y], startEntity.MapInstanceId, out var instance))
                        {
                            // Add tile blocks
                            var blocks = instance.GetCachedBlocks(startEntity.GetType() == typeof(Player));
                            foreach (var block in blocks)
                            {
                                mapGrid[(x + 1 - startGridX) * Options.MapWidth + block.X,
                                        (y + 1 - startGridY) * Options.MapHeight + block.Y].IsWall = true;
                            }

                            // Add entity blocks
                            foreach (var entity in instance.GetEntities())
                            {
                                if (!entity.IsPassable() && entity.X > -1 && entity.X < Options.MapWidth &&
                                    entity.Y > -1 && entity.Y < Options.MapHeight)
                                {
                                    mapGrid[(x + 1 - startGridX) * Options.MapWidth + entity.X,
                                            (y + 1 - startGridY) * Options.MapHeight + entity.Y].IsWall = true;
                                }
                            }

                            // Add global event blocks
                            foreach (var globalEvent in instance.GlobalEventInstances)
                            {
                                if (globalEvent.Value?.X > -1 && globalEvent.Value.X < Options.MapWidth &&
                                    globalEvent.Value.Y > -1 && globalEvent.Value.Y < Options.MapHeight)
                                {
                                    foreach (var page in globalEvent.Value.GlobalPageInstance)
                                    {
                                        if (!page.Passable)
                                        {
                                            mapGrid[(x + 1 - startGridX) * Options.MapWidth + globalEvent.Value.X,
                                                    (y + 1 - startGridY) * Options.MapHeight + globalEvent.Value.Y].IsWall = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                // Make sure target position is passable
                mapGrid[targetX, targetY].IsWall = false;



                // Run A* pathfinding
                var aStar = new SpatialAStar(mapGrid);
                var path = aStar.Search(new Point(sourceX, sourceY), new Point(targetX, targetY), null);

                if (path == null)
                {
                    return 9999; // No path found
                }

                // Check if the path actually reaches the target
                var lastNode = path.Last();
                if (lastNode.X != targetX || lastNode.Y != targetY)
                {
                    return 9999; // Path cannot reach target
                }

                return path.Count - 1; // Return path length (subtract 1 because path includes start node)
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return 9999; // Error occurred
            }
        }

        public static void PrintPathfindingGrid(PathNode[,] mapGrid, int sourceX, int sourceY, int targetX, int targetY, IEnumerable<PathNode> path = null)
        {
            if (mapGrid == null)
            {
                Log.Info("MapGrid is null");
                return;
            }

            var width = mapGrid.GetLength(0);
            var height = mapGrid.GetLength(1);

            // Create a HashSet of path coordinates for quick lookup
            var pathCoords = new HashSet<(int x, int y)>();
            if (path != null)
            {
                foreach (var node in path)
                {
                    pathCoords.Add((node.X, node.Y));
                }
            }

            Console.WriteLine($"Pathfinding Grid Visualization ({width}x{height}):");
            Console.WriteLine($"Legend: O = Open, X = Blocked, S = Start, T = Target, * = Path");
            Console.WriteLine("Grid Layout:");

            // Print column numbers header
            var header = "   ";
            for (var x = 0; x < width; x++)
            {
                header += (x % 10).ToString();
            }
            Console.WriteLine(header);

            // Print each row
            for (var y = 0; y < height; y++)
            {
                var row = $"{y:D2} ";

                for (var x = 0; x < width; x++)
                {
                    var node = mapGrid[x, y];
                    char symbol;

                    // Determine what symbol to show
                    if (x == sourceX && y == sourceY)
                    {
                        symbol = 'S'; // Start position
                    }
                    else if (x == targetX && y == targetY)
                    {
                        symbol = 'T'; // Target position
                    }
                    else if (pathCoords.Contains((x, y)))
                    {
                        symbol = '*'; // Path
                    }
                    else if (node == null)
                    {
                        symbol = '?'; // Uninitialized
                    }
                    else if (node.IsWall)
                    {
                        symbol = 'X'; // Blocked
                    }
                    else
                    {
                        symbol = 'O'; // Open
                    }

                    row += symbol;
                }

                Console.WriteLine(row);
            }

            Console.WriteLine($"Start: ({sourceX}, {sourceY}), Target: ({targetX}, {targetY})");
            if (path != null)
            {
                var pathLength = path.Count();
                Console.WriteLine($"Path found with {pathLength} nodes");
            }
        }

        private static void FillGridArea(PathNode[,] dest, int startX, int startY, int width, int height)
        {
            for (var x = startX; x < startX + width; x++)
            {
                for (var y = startY; y < startY + height; y++)
                {
                    if (x >= 0 && x < dest.GetLength(0) && y >= 0 && y < dest.GetLength(1))
                    {
                        dest[x, y].IsWall = true;
                    }
                }
            }
        }

        public void PathFailed(long timeMs)
        {
            mPath = null;
            mConsecutiveFails++;
            mWaitTime = timeMs + 1000;
        }

        public sbyte GetMove()
        {
            if (mPath == null)
            {
                return -1;
            }

            var enm = mPath.GetEnumerator();
            while (enm.MoveNext())
            {
                if (enm.Current.X - Options.MapWidth == mEntity.X && enm.Current.Y - Options.MapHeight == mEntity.Y)
                {
                    if (enm.MoveNext())
                    {
                        var newX = enm.Current.X - Options.MapWidth;
                        var newY = enm.Current.Y - Options.MapHeight;
                        if (mEntity.X < newX)
                        {
                            enm.Dispose();

                            return (int) Directions.Right;
                        }
                        else if (mEntity.X > newX)
                        {
                            enm.Dispose();

                            return (int) Directions.Left;
                        }
                        else if (mEntity.Y < newY)
                        {
                            enm.Dispose();

                            return (int) Directions.Down;
                        }
                        else if (mEntity.Y > newY)
                        {
                            enm.Dispose();

                            return (int) Directions.Up;
                        }
                    }
                }
            }

            enm.Dispose();

            return -1;
        }

    }

    public class AStarSolver : SpatialAStar
    {

        public AStarSolver(PathNode[,] inGrid) : base(inGrid)
        {
        }

        protected override double Heuristic(PathNode inStart, PathNode inEnd)
        {
            return Math.Abs(inStart.X - inEnd.X) + Math.Abs(inStart.Y - inEnd.Y);
        }

        protected override double NeighborDistance(PathNode inStart, PathNode inEnd)
        {
            return Heuristic(inStart, inEnd);
        }

    }

}
