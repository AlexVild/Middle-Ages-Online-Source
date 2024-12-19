using System;

using Intersect.Client.General;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.Maps;
using Intersect.Client.Maps;
using Intersect.Network.Packets.Server;
using Intersect.Utilities;
using Pango;
using Intersect.Client.Framework.Graphics;
using static Intersect.Client.Framework.File_Management.GameContentManager;
using System.Drawing;
using Intersect.Client.Framework.GenericClasses;
using Gtk;
using Intersect.Client.Framework.Gwen;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using static Intersect.Client.Framework.Gwen.Skin.SkinTextures._Tab;

namespace Intersect.Client.Entities.Projectiles
{

    public partial class Projectile : Entity
    {

        private bool mDisposing;

        private bool mLoaded;

        private object mLock = new object();

        private ProjectileBase mMyBase;

        private Guid mOwner;

        private int mQuantity;

        private int mSpawnCount;

        private int mSpawnedAmount;

        private long mSpawnTime;

        private int mTotalSpawns;

        public bool Grounded = false;

        public Guid ProjectileId;

        // Individual Spawns
        public ProjectileSpawns[] Spawns;

        public Guid TargetId;

        private GameTexture COMBAT_TILE_AOE = Globals.ContentManager.GetTexture(TextureType.Misc, "aoe.png");

        /// <summary>
        ///     The constructor for the inherated projectile class
        /// </summary>
        public Projectile(Guid id, ProjectileEntityPacket packet) : base(id, packet)
        {
            Vital[(int) Vitals.Health] = 1;
            MaxVital[(int) Vitals.Health] = 1;
            HideName = true;
            Passable = true;
            IsMoving = true;
        }

        public override void Load(EntityPacket packet)
        {
            if (mLoaded)
            {
                return;
            }

            base.Load(packet);
            var pkt = (ProjectileEntityPacket) packet;
            ProjectileId = pkt.ProjectileId;
            Dir = pkt.ProjectileDirection;
            TargetId = pkt.TargetId;
            mOwner = pkt.OwnerId;
            mMyBase = ProjectileBase.Get(ProjectileId);
            if (mMyBase != null)
            {
                for (var x = 0; x < ProjectileBase.SPAWN_LOCATIONS_WIDTH; x++)
                {
                    for (var y = 0; y < ProjectileBase.SPAWN_LOCATIONS_WIDTH; y++)
                    {
                        for (var d = 0; d < ProjectileBase.MAX_PROJECTILE_DIRECTIONS; d++)
                        {
                            if (mMyBase.SpawnLocations[x, y].Directions[d] == true)
                            {
                                mTotalSpawns++;
                            }
                        }
                    }
                }

                mTotalSpawns *= mMyBase.Quantity;
                Grounded = mMyBase.Grounded;
            }

            Spawns = new ProjectileSpawns[mTotalSpawns];
            mLoaded = true;
        }

        public override EntityTypes GetEntityType()
        {
            return EntityTypes.Projectile;
        }

        public override void Dispose()
        {
            if (!mDisposing)
            {
                lock (mLock)
                {
                    mDisposing = true;
                    if (mSpawnedAmount == 0)
                    {
                        Update();
                    }

                    if (Spawns != null)
                    {
                        foreach (var s in Spawns)
                        {
                            if (s != null && s.Anim != null)
                            {
                                s.Anim.DisposeNextDraw();
                            }
                        }
                    }
                }
            }
        }

        /// <inheritdoc />
        public override bool CanBeAttacked()
        {
            return false;
        }

        //Find out which animation data to load depending on what spawn wave we are on during projection.
        private int FindSpawnAnimationData()
        {
            var start = 0;
            var end = 1;
            for (var i = 0; i < mMyBase.Animations.Count; i++)
            {
                end = mMyBase.Animations[i].SpawnRange;
                if (mQuantity >= start && mQuantity < end)
                {
                    return i;
                }

                start = end;
            }

            //If reaches maximum and the developer(s) have fucked up the animation ranges on each spawn of projectiles somewhere, just assign it to the last animation state.
            return mMyBase.Animations.Count - 1;
        }

        private void AddProjectileSpawns()
        {
            var spawn = FindSpawnAnimationData();
            var animBase = AnimationBase.Get(mMyBase.Animations[spawn].AnimationId);

            for (var x = 0; x < ProjectileBase.SPAWN_LOCATIONS_WIDTH; x++)
            {
                for (var y = 0; y < ProjectileBase.SPAWN_LOCATIONS_WIDTH; y++)
                {
                    for (var d = 0; d < ProjectileBase.MAX_PROJECTILE_DIRECTIONS; d++)
                    {
                        if (mMyBase.SpawnLocations[x, y].Directions[d] == true)
                        {
                            var s = new ProjectileSpawns(
                                FindProjectileRotationDir(Dir, d), X + FindProjectileRotationX(Dir, x - 2, y - 2),
                                Y + FindProjectileRotationY(Dir, x - 2, y - 2), Z, CurrentMap, animBase,
                                mMyBase.Animations[spawn].AutoRotate, mMyBase, this
                            );

                            Spawns[mSpawnedAmount] = s;
                            if (Collided(mSpawnedAmount))
                            {
                                Spawns[mSpawnedAmount].Dispose();
                                Spawns[mSpawnedAmount] = null;
                                mSpawnCount--;
                            }

                            mSpawnedAmount++;
                            mSpawnCount++;
                        }
                    }
                }
            }

            mQuantity++;
            mSpawnTime = Timing.Global.Milliseconds + mMyBase.Delay;
        }

        private int FindProjectileRotationX(int direction, int x, int y)
        {
            switch (direction)
            {
                case 0: //Up
                    return x;
                case 1: //Down
                    return -x;
                case 2: //Left
                    return y;
                case 3: //Right
                    return -y;
                default:
                    return x;
            }
        }

        private int FindProjectileRotationY(int direction, int x, int y)
        {
            switch (direction)
            {
                case 0: //Up
                    return y;
                case 1: //Down
                    return -y;
                case 2: //Left
                    return -x;
                case 3: //Right
                    return x;
                default:
                    return y;
            }
        }

        private int FindProjectileRotationDir(int entityDir, int projectionDir)
        {
            switch (entityDir)
            {
                case 0: //Up
                    return projectionDir;
                case 1: //Down
                    switch (projectionDir)
                    {
                        case 0: //Up
                            return 1;
                        case 1: //Down
                            return 0;
                        case 2: //Left
                            return 3;
                        case 3: //Right
                            return 2;
                        case 4: //UpLeft
                            return 7;
                        case 5: //UpRight
                            return 6;
                        case 6: //DownLeft
                            return 5;
                        case 7: //DownRight
                            return 4;
                        default:
                            return projectionDir;
                    }
                case 2: //Left
                    switch (projectionDir)
                    {
                        case 0: //Up
                            return 2;
                        case 1: //Down
                            return 3;
                        case 2: //Left
                            return 1;
                        case 3: //Right
                            return 0;
                        case 4: //UpLeft
                            return 6;
                        case 5: //UpRight
                            return 4;
                        case 6: //DownLeft
                            return 7;
                        case 7: //DownRight
                            return 5;
                        default:
                            return projectionDir;
                    }
                case 3: //Right
                    switch (projectionDir)
                    {
                        case 0: //Up
                            return 3;
                        case 1: //Down
                            return 2;
                        case 2: //Left
                            return 0;
                        case 3: //Right
                            return 1;
                        case 4: //UpLeft
                            return 5;
                        case 5: //UpRight
                            return 7;
                        case 6: //DownLeft
                            return 4;
                        case 7: //DownRight
                            return 6;
                        default:
                            return projectionDir;
                    }
                default:
                    return projectionDir;
            }
        }

        private float GetRangeX(int direction, float range)
        {
            //Left, UpLeft, DownLeft
            if (direction == 2 || direction == 4 || direction == 6)
            {
                return -range;
            }

            //Right, UpRight, DownRight
            else if (direction == 3 || direction == 5 || direction == 7)
            {
                return range;
            }

            //Up and Down
            else
            {
                return 0;
            }
        }

        private float GetRangeY(int direction, float range)
        {
            //Up, UpLeft, UpRight
            if (direction == 0 || direction == 4 || direction == 5)
            {
                return -range;
            }

            //Down, DownLeft, DownRight
            else if (direction == 1 || direction == 6 || direction == 7)
            {
                return range;
            }

            //Left and Right
            else
            {
                return 0;
            }
        }

        /// <summary>
        ///     Gets the displacement of the projectile during projection
        /// </summary>
        /// <returns>The displacement from the co-ordinates if placed on a Options.TileHeight grid.</returns>
        private float GetDisplacement(long spawnTime)
        {
            var elapsedTime = Timing.Global.Milliseconds - spawnTime;
            var displacementPercent = elapsedTime / (float)GetSpeedWithModifier();
            var clampedDisplacement = (float)MathHelper.Clamp(displacementPercent, 0f, 1f);

            return clampedDisplacement * Options.TileHeight * mMyBase.Range;
        }

        public int GetSpeedWithModifier()
        {
            if (!Globals.Entities.TryGetValue(mOwner, out var entity))
            {
                return mMyBase.Speed;
            }

            return entity.ApplyBonusEffect(mMyBase.Speed, EffectType.Swiftshot, subtractive: true);
        }

        /// <summary>
        ///     Overwrite updating the offsets for projectile movement.
        /// </summary>
        public override bool Update()
        {
            if (mMyBase == null)
            {
                return false;
            }

            lock (mLock)
            {
                var tmpI = -1;
                var map = CurrentMap;
                var y = Y;

                if (!mDisposing && mQuantity < mMyBase.Quantity && mSpawnTime < Timing.Global.Milliseconds)
                {
                    AddProjectileSpawns();
                }

                if (IsMoving)
                {
                    for (var s = 0; s < mSpawnedAmount; s++)
                    {
                        var spawn = Spawns[s];
                        var spawnMap = MapInstance.Get(spawn?.SpawnMapId ?? Guid.Empty);
                        if (spawn == null || spawnMap == null)
                        {
                            continue;
                        }

                        var animWidth = spawn.Anim?.Width ?? 0;
                        var animHeight = spawn.Anim?.Height ?? 0;

                        spawn.OffsetX = GetRangeX(spawn.Dir, GetDisplacement(spawn.SpawnTime));
                        spawn.OffsetY = GetRangeY(spawn.Dir, GetDisplacement(spawn.SpawnTime));

                        var worldTile = GetTileRectangle(spawnMap, (byte)spawn.SpawnX, (byte)spawn.SpawnY);

                        // Base world positions (centered by default)
                        float worldX = worldTile.X + spawn.OffsetX + Options.TileWidth / 2;
                        float worldY = worldTile.Y + spawn.OffsetY + Options.TileHeight / 2;

                        // Adjust for direction -- we align projectiles such that in the UP direction, the top of the projectile is aligned CenterH,Top
                        if (spawn.ProjectileBase.UseNewClientAlignment)
                        {
                            var heightShift = (animHeight - Options.TileHeight) / 2;
                            var widthShift = (animWidth - Options.TileWidth) / 2;

                            var angleRadians = spawn.AutoRotate ? spawn.Anim.GetRotationDegrees() * Math.PI / 180 : 0;

                            var rotatedWidth = MathHelper.CalculateRotatedWidth(animWidth, animHeight, (float)angleRadians);
                            var rotatedHeight = MathHelper.CalculateRotatedHeight(animWidth, animHeight, (float)angleRadians);

                            var rotatedWidthShift = (float)((rotatedWidth - Options.TileWidth) / 2);
                            var rotatedHeightShift = (float)((rotatedHeight - Options.TileHeight) / 2);

                            switch ((ProjectileDirections)spawn.Dir)
                            {
                                case ProjectileDirections.Up:
                                    worldY += heightShift;
                                    break;

                                case ProjectileDirections.Down:
                                    worldY -= heightShift;
                                    break;

                                case ProjectileDirections.Left:
                                    if (spawn.AutoRotate)
                                    {
                                        worldX += heightShift;
                                    }
                                    else
                                    {
                                        worldX += widthShift;
                                    }
                                    break;

                                case ProjectileDirections.Right:
                                    if (spawn.AutoRotate)
                                    {
                                        worldX -= heightShift;
                                    }
                                    else
                                    {
                                        worldX -= widthShift;
                                    }
                                    break;

                                case ProjectileDirections.UpLeft:
                                    if (!spawn.AutoRotate)
                                    {
                                        worldX += widthShift / (float)Math.Sqrt(2); // Move horizontally by half diagonal offset
                                        worldY += heightShift / (float)Math.Sqrt(2); // Move vertically by half diagonal offset
                                    }
                                    else
                                    {
                                        worldX += rotatedWidthShift;
                                        worldY += rotatedHeightShift;
                                    }
                                    break;

                                case ProjectileDirections.UpRight:
                                    if (!spawn.AutoRotate)
                                    {
                                        worldX -= widthShift / (float)Math.Sqrt(2);
                                        worldY += heightShift / (float)Math.Sqrt(2);
                                    }
                                    else
                                    {
                                        worldX -= rotatedWidthShift;
                                        worldY += rotatedHeightShift;
                                    }

                                    break;

                                case ProjectileDirections.DownLeft:
                                    if (!spawn.AutoRotate)
                                    {
                                        worldX += widthShift / (float)Math.Sqrt(2);
                                        worldY -= heightShift / (float)Math.Sqrt(2);
                                    }
                                    else
                                    {
                                        worldX += rotatedWidthShift;
                                        worldY -= rotatedHeightShift;
                                    }
                                    break;

                                case ProjectileDirections.DownRight:
                                    if (!spawn.AutoRotate)
                                    {
                                        worldX -= widthShift / (float)Math.Sqrt(2);
                                        worldY -= heightShift / (float)Math.Sqrt(2);
                                    }
                                    else
                                    {
                                        worldX -= rotatedWidthShift;
                                        worldY -= rotatedHeightShift;
                                    }
                                    break;
                            }
                        }

                        // Set position and update animation
                        spawn.Anim.SetPosition(
                            worldX,
                            worldY,
                            spawn.X,
                            spawn.Y,
                            spawn.MapId,
                            spawn.AutoRotate ? spawn.Dir : 0,
                            spawn.Z
                        );

                        spawn.Anim.Update();
                    }

                }

                CheckForCollision();
            }

            return true;
        }

        public void DrawActiveTile(ProjectileSpawns spawn)
        {
            // If mod with debug open
            if (!Interface.Interface.GameUi.DebugMenuOpen() || Globals.Me.Type <= 0 || spawn == null)
            {
                    return;
            }

            var map = MapInstance.Get(spawn.MapId);
            if (map == null)
            {
                return;
            }
            var tile = GetTileRectangle(map, (byte)spawn.X, (byte)spawn.Y);
            Core.Graphics.DrawGameTexture(
                COMBAT_TILE_AOE, new FloatRect(0, 0, COMBAT_TILE_AOE.Width, COMBAT_TILE_AOE.Height),
                new FloatRect(tile.X, tile.Y, Options.TileWidth, Options.TileHeight), Color.White
            );
        }

        public void CheckForCollision()
        {
            if (mSpawnCount != 0 || mQuantity < mMyBase.Quantity)
            {
                for (var i = 0; i < mSpawnedAmount; i++)
                {
                    if (Spawns[i] != null && Timing.Global.Milliseconds > Spawns[i].TransmissionTimer)
                    {
                        var spawnMap = MapInstance.Get(Spawns[i].MapId);
                        if (spawnMap != null)
                        {
                            var newx = Spawns[i].X + (int) GetRangeX(Spawns[i].Dir, 1);
                            var newy = Spawns[i].Y + (int) GetRangeY(Spawns[i].Dir, 1);
                            var newMapId = Spawns[i].MapId;
                            var killSpawn = false;

                            Spawns[i].Distance++;

                            if (newx < 0)
                            {
                                if (MapInstance.Get(spawnMap.Left) != null)
                                {
                                    newMapId = spawnMap.Left;
                                    newx = Options.MapWidth - 1;
                                }
                                else
                                {
                                    killSpawn = true;
                                }
                            }

                            if (newx > Options.MapWidth - 1)
                            {
                                if (MapInstance.Get(spawnMap.Right) != null)
                                {
                                    newMapId = spawnMap.Right;
                                    newx = 0;
                                }
                                else
                                {
                                    killSpawn = true;
                                }
                            }

                            if (newy < 0)
                            {
                                if (MapInstance.Get(spawnMap.Up) != null)
                                {
                                    newMapId = spawnMap.Up;
                                    newy = Options.MapHeight - 1;
                                }
                                else
                                {
                                    killSpawn = true;
                                }
                            }

                            if (newy > Options.MapHeight - 1)
                            {
                                if (MapInstance.Get(spawnMap.Down) != null)
                                {
                                    newMapId = spawnMap.Down;
                                    newy = 0;
                                }
                                else
                                {
                                    killSpawn = true;
                                }
                            }

                            if (killSpawn)
                            {
                                Spawns[i].Dispose();
                                Spawns[i] = null;
                                mSpawnCount--;

                                continue;
                            }

                            Spawns[i].X = newx;
                            Spawns[i].Y = newy;
                            Spawns[i].MapId = newMapId;
                            var newMap = MapInstance.Get(newMapId);

                            //Check for Z-Dimension
                            if (newMap.Attributes[Spawns[i].X, Spawns[i].Y] != null)
                            {
                                if (newMap.Attributes[Spawns[i].X, Spawns[i].Y].Type == MapAttributes.ZDimension)
                                {
                                    if (((MapZDimensionAttribute) newMap.Attributes[Spawns[i].X, Spawns[i].Y])
                                        .GatewayTo >
                                        0)
                                    {
                                        Spawns[i].Z =
                                            ((MapZDimensionAttribute) newMap.Attributes[Spawns[i].X, Spawns[i].Y])
                                            .GatewayTo -
                                            1;
                                    }
                                }
                            }

                            if (killSpawn == false)
                            {
                                killSpawn = Collided(i);
                            }

                            Spawns[i].TransmissionTimer = Timing.Global.Milliseconds +
                                                          (long) ((float) GetSpeedWithModifier() / mMyBase.Range);

                            if (Spawns[i].Distance >= mMyBase.Range)
                            {
                                killSpawn = true;
                            }

                            if (killSpawn)
                            {
                                Spawns[i].Dispose();
                                Spawns[i] = null;
                                mSpawnCount--;
                            }
                        }
                    }
                }
            }
            else
            {
                Globals.Entities[Id].Dispose();
            }
        }

        private bool Collided(int i)
        {
            var killSpawn = false;
            Entity blockedBy = null;
            var tileBlocked = Globals.Me.IsTileBlocked(
                Spawns[i].X, Spawns[i].Y, Z, Spawns[i].MapId, ref blockedBy,
                Spawns[i].ProjectileBase.IgnoreActiveResources, Spawns[i].ProjectileBase.IgnoreExhaustedResources, true,
                Grounded
            );

            if (tileBlocked != -1)
            {
                if (tileBlocked == -6 &&
                    blockedBy != null &&
                    blockedBy.Id != mOwner &&
                    Globals.Entities.ContainsKey(blockedBy.Id))
                {
                    if (blockedBy.GetType() == typeof(Resource))
                    {
                        if (!Spawns[i].ProjectileBase.PierceTarget && !Spawns[i].ProjectileBase.IgnoreActiveResources)
                        {
                            killSpawn = true;
                        }
                    }
                }
                else
                {
                    if (tileBlocked == -2)
                    {
                        if (!Spawns[i].ProjectileBase.IgnoreMapBlocks)
                        {
                            killSpawn = true;
                        }
                    }
                    else if (tileBlocked == -3)
                    {
                        if (!Spawns[i].ProjectileBase.IgnoreZDimension)
                        {
                            killSpawn = true;
                        }
                    }
                    else if (tileBlocked == -5)
                    {
                        killSpawn = true;
                    }
                }
            }

            return killSpawn;
        }

        /// <summary>
        ///     Rendering all of the individual projectiles from a singular spawn to a map.
        /// </summary>
        public override void Draw()
        {
            if (MapInstance.Get(CurrentMap) == null || !Globals.GridMaps.Contains(CurrentMap))
            {
                return;
            }

            foreach (var spawn in Spawns)
            {
                DrawActiveTile(spawn);
            }

            if (Globals.Me.MapInstance.ZoneType != MapZones.Safe && !InPvpSight)
            {
                return;
            }
        }

        public void SpawnDead(int spawnIndex)
        {
            if (spawnIndex < mSpawnedAmount && Spawns[spawnIndex] != null)
            {
                Spawns[spawnIndex].Dispose();
                Spawns[spawnIndex] = null;
            }
        }

    }

}
