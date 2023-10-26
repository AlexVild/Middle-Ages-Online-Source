using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using Intersect.GameObjects.Maps;
using Intersect.Logging;
using Intersect.Network.Packets.Server;
using Intersect.Server.Database;
using Intersect.Server.Database.PlayerData.Players;
using Intersect.Server.Entities.Combat;
using Intersect.Server.Entities.Events;
using Intersect.Server.Localization;
using Intersect.Server.Maps;
using Intersect.Server.Networking;
using Intersect.Utilities;

using Newtonsoft.Json;

namespace Intersect.Server.Entities
{

    public partial class Entity : IDisposable
    {

        //Instance Values
        private Guid _id;

        public Guid MapInstanceId = Guid.Empty;

        [JsonProperty("MaxVitals"), NotMapped] private int[] _maxVital = new int[(int) Vitals.VitalCount];

        [NotMapped, JsonIgnore] public Stat[] Stat = new Stat[(int) Stats.StatCount];

        [NotMapped, JsonIgnore] public int[] StatVals => Stat.Select(stat => stat.Value()).ToArray();

        [NotMapped, JsonIgnore] public Entity Target { get; set; } = null;

        public Entity() : this(Guid.NewGuid(), Guid.Empty)
        {
        }

        //Initialization
        public Entity(Guid instanceId, Guid mapInstanceId)
        {
            if (!(this is EventPageInstance) && !(this is Projectile))
            {
                for (var i = 0; i < (int)Stats.StatCount; i++)
                {
                    Stat[i] = new Stat((Stats)i, this);
                }
            }
            MapInstanceId = mapInstanceId;

            Id = instanceId;
        }

        [Column(Order = 1), JsonProperty(Order = -2)]
        public string Name { get; set; }

        public Guid MapId { get; set; }

        [NotMapped]
        public string MapName => MapController.GetName(MapId);

        [JsonIgnore]
        [NotMapped]
        public MapController Map => MapController.Get(MapId);

        public int X { get; set; }

        public int Y { get; set; }

        public int Z { get; set; }

        private int _dir;
        /// <summary>
        /// An override of Dir that will instead return a player's face direction if needed because I'm lazy and this might work?
        /// </summary>
        public int Dir 
        {
            get => this is Player player && player.CombatMode && player.FaceDirection != -1 ? player.FaceDirection : _dir;
            set => _dir = value;
        }

        /// <summary>
        /// Used if we ACTUALLY just want the Dir
        /// </summary>
        /// <returns></returns>
        public int GetRealDir()
        {
            return _dir;
        }

        public string Sprite { get; set; }

        /// <summary>
        /// The database compatible version of <see cref="Color"/>
        /// </summary>
        [JsonIgnore, Column(nameof(Color))]
        public string JsonColor
        {
            get => JsonConvert.SerializeObject(Color);
            set => Color = !string.IsNullOrWhiteSpace(value) ? JsonConvert.DeserializeObject<Color>(value) : Color.White;
        }

        /// <summary>
        /// Defines the ARGB color settings for this Entity.
        /// </summary>
        [NotMapped]
        public Color Color { get; set; } = new Color(255, 255, 255, 255);

        public string Face { get; set; }

        public int Level { get; set; }

        [JsonIgnore, Column("Vitals")]
        public string VitalsJson
        {
            get => DatabaseUtils.SaveIntArray(mVitals, (int) Enums.Vitals.VitalCount);
            set => mVitals = DatabaseUtils.LoadIntArray(value, (int) Enums.Vitals.VitalCount);
        }

        [JsonProperty("Vitals"), NotMapped]
        private int[] mVitals { get; set; } = new int[(int) Enums.Vitals.VitalCount];

        [JsonIgnore, NotMapped]
        private int[] mOldVitals { get; set; } = new int[(int)Enums.Vitals.VitalCount];

        [JsonIgnore, NotMapped]
        private int[] mOldMaxVitals { get; set; } = new int[(int)Enums.Vitals.VitalCount];

        //Stats based on npc settings, class settings, etc for quick calculations
        [JsonIgnore, Column(nameof(BaseStats))]
        public string StatsJson
        {
            get => DatabaseUtils.SaveIntArray(BaseStats, (int) Enums.Stats.StatCount);
            set => BaseStats = DatabaseUtils.LoadIntArray(value, (int) Enums.Stats.StatCount);
        }

        [NotMapped]
        public int[] BaseStats { get; set; } =
            new int[(int) Stats.StatCount]; // TODO: Why can this be BaseStats while Vitals is _vital and MaxVitals is _maxVital?

        [JsonIgnore, Column(nameof(StatPointAllocations))]
        public string StatPointsJson
        {
            get => DatabaseUtils.SaveIntArray(StatPointAllocations, (int) Stats.StatCount);
            set => StatPointAllocations = DatabaseUtils.LoadIntArray(value, (int) Stats.StatCount);
        }

        [NotMapped]
        public int[] StatPointAllocations { get; set; } = new int[(int) Stats.StatCount];

        //Inventory
        [JsonIgnore]
        public virtual List<InventorySlot> Items { get; set; } = new List<InventorySlot>();

        //Spells
        [JsonIgnore]
        public virtual List<SpellSlot> Spells { get; set; } = new List<SpellSlot>();

        [JsonIgnore, Column(nameof(NameColor))]
        public string NameColorJson
        {
            get => DatabaseUtils.SaveColor(NameColor);
            set => NameColor = DatabaseUtils.LoadColor(value);
        }

        [NotMapped]
        public Color NameColor { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        public Guid Id { get => _id; set => _id = value; }

        [NotMapped]
        public Label HeaderLabel { get; set; }

        [JsonIgnore, Column(nameof(HeaderLabel))]
        public string HeaderLabelJson
        {
            get => JsonConvert.SerializeObject(HeaderLabel);
            set => HeaderLabel = value != null ? JsonConvert.DeserializeObject<Label>(value) : new Label();
        }

        [NotMapped]
        public Label FooterLabel { get; set; }

        [JsonIgnore, Column(nameof(FooterLabel))]
        public string FooterLabelJson
        {
            get => JsonConvert.SerializeObject(FooterLabel);
            set => FooterLabel = value != null ? JsonConvert.DeserializeObject<Label>(value) : new Label();
        }

        public Guid SelectedLabelId { get; set; }

        [NotMapped]
        public bool Dead { get; set; }

        //Combat
        [NotMapped, JsonIgnore]
        public long CastTime { get; set; }

        [NotMapped, JsonIgnore]
        public bool CanCast => CastTime == 0;

        [NotMapped, JsonIgnore]
        public long AttackTimer { get; set; }

        [NotMapped, JsonIgnore]
        public bool Blocking { get; set; }

        [NotMapped, JsonIgnore]
        public Entity CastTarget { get; set; }

        [NotMapped, JsonIgnore]
        public Guid CollisionIndex { get; set; }

        [NotMapped, JsonIgnore]
        public long CombatTimer { get; set; }

        //Visuals
        [NotMapped, JsonIgnore]
        public bool HideName { get; set; }

        [NotMapped, JsonIgnore]
        public bool HideEntity { get; set; } = false;

        [NotMapped, JsonIgnore]
        public List<Guid> Animations { get; set; } = new List<Guid>();

        //DoT/HoT Spells
        [NotMapped, JsonIgnore]
        public ConcurrentDictionary<Guid, DoT> DoT { get; set; } = new ConcurrentDictionary<Guid, DoT>();

        [NotMapped, JsonIgnore]
        public DoT[] CachedDots { get; set; } = new DoT[0];

        [NotMapped, JsonIgnore]
        public EventMoveRoute MoveRoute { get; set; } = null;

        [NotMapped, JsonIgnore]
        public EventPageInstance MoveRouteSetter { get; set; } = null;

        [NotMapped, JsonIgnore]
        public long MoveTimer { get; set; }

        [NotMapped, JsonIgnore]
        public bool Passable { get; set; } = false;

        [NotMapped, JsonIgnore]
        public long RegenTimer { get; set; } = Timing.Global.Milliseconds;

        [NotMapped, JsonIgnore]
        public long ManaRegenTimer { get; set; } = Timing.Global.Milliseconds;

        [NotMapped, JsonIgnore]
        public int SpellCastSlot { get; set; } = 0;

        //Status effects
        [NotMapped, JsonIgnore]
        public ConcurrentDictionary<SpellBase, Status> Statuses { get; } = new ConcurrentDictionary<SpellBase, Status>();

        [JsonIgnore, NotMapped]
        public Status[] CachedStatuses = new Status[0];

        [JsonIgnore, NotMapped]
        private Status[] mOldStatuses = new Status[0];

        [JsonIgnore, NotMapped]
        public Dictionary<Immunities, bool> ImmuneTo = new Dictionary<Immunities, bool>();

        [NotMapped, JsonIgnore]
        public bool IsDisposed { get; protected set; }

        [NotMapped, JsonIgnore]
        public object EntityLock = new object();

        [NotMapped, JsonIgnore]
        public Guid DeathAnimation = Guid.Empty;

        [NotMapped, JsonIgnore]
        public bool VitalsUpdated
        {
            get => !GetVitals().SequenceEqual(mOldVitals) || !GetMaxVitals().SequenceEqual(mOldMaxVitals);

            set
            {
                if (value == false)
                {
                    mOldVitals = GetVitals();
                    mOldMaxVitals = GetMaxVitals();
                }
            }
        }

        [NotMapped, JsonIgnore]
        public bool StatusesUpdated
        {
            get => CachedStatuses != mOldStatuses; //The whole CachedStatuses assignment gets changed when a status is added, removed, or updated (time remaining changes, so we only check for reference equivity here)

            set
            {
                if (value == false)
                {
                    mOldStatuses = CachedStatuses;
                }
            }
        }

        public virtual void Dispose()
        {
            if (!IsDisposed)
            {
                IsDisposed = true;
            }
        }

        public virtual void CheckForSpellCast(long timeMs)
        {
            // intentionally blank
        }

        public virtual void Update(long timeMs)
        {
            var lockObtained = false;
            try
            {
                Monitor.TryEnter(EntityLock, ref lockObtained);
                if (lockObtained)
                {
                    //Cast timers
                    CheckForSpellCast(timeMs);

                    //DoT/HoT timers
                    foreach (var dot in CachedDots)
                    {
                        dot.Tick();
                    }

                    var statsUpdated = false;
                    var statTime = Timing.Global.Milliseconds;
                    for (var i = 0; i < (int)Stats.StatCount; i++)
                    {
                        if (Stat[i] == default)
                        {
                            continue;
                        }
                        statsUpdated |= Stat[i].Update(statTime);
                    }

                    if (statsUpdated)
                    {
                        PacketSender.SendEntityStatsToProximity(this);
                    }

                    //Regen Timers
                    if (timeMs > CombatTimer && timeMs > RegenTimer)
                    {
                        ProcessRegen();
                        RegenTimer = timeMs + Options.RegenTime;
                    }

                    ProcessManaRegen(timeMs);

                    //Status timers
                    var statusArray = CachedStatuses;
                    foreach (var status in statusArray)
                    {
                        status.TryRemoveStatus();
                    }

                    PruneAllies();
                }
            }
            finally
            {
                if (lockObtained)
                {
                    Monitor.Exit(EntityLock);
                }
            }
        }

        // Default implementation in "AttackingEntity"
        public virtual void ProcessManaRegen(long timeMs)
        {
        }

        public virtual int CanMove(int moveDir, bool moveRouteRequest = false)
        {
            return CanMove(moveDir);
        }

        //Movement
            /// <summary>
            ///     Determines if this entity can move in the direction given.
            ///     Returns -5 if the tile is completely out of bounds.
            ///     Returns -3 if a tile is blocked because of a Z dimension tile
            ///     Returns -2 if a tile is blocked by a map attribute.
            ///     Returns -1 for clear.
            ///     Returns the type of entity that is blocking the way (if one exists)
            /// </summary>
            /// <param name="moveDir"></param>
            /// <returns></returns>
        public virtual int CanMove(int moveDir)
        {
            var xOffset = 0;
            var yOffset = 0;

            // If this is an Npc that has the Static behaviour, it can NEVER move.
            if (this is Npc npc)
            {
                if (npc.Base.Movement == (byte) NpcMovement.Static || npc.Base.StandStill)
                {
                    return -2;
                }
            }

            var tile = new TileHelper(MapId, X, Y);
            switch (moveDir)
            {
                case 0: //Up
                    yOffset--;

                    break;
                case 1: //Down
                    yOffset++;

                    break;
                case 2: //Left
                    xOffset--;

                    break;
                case 3: //Right
                    xOffset++;

                    break;
                case 4: //NW
                    yOffset--;
                    xOffset--;

                    break;
                case 5: //NE
                    yOffset--;
                    xOffset++;

                    break;
                case 6: //SW
                    yOffset++;
                    xOffset--;

                    break;
                case 7: //SE
                    yOffset++;
                    xOffset++;

                    break;
            }

            MapController mapController = null;
            int tileX = 0;
            int tileY = 0;

            if (tile.Translate(xOffset, yOffset))
            {
                mapController = MapController.Get(tile.GetMapId());
                tileX = tile.GetX();
                tileY = tile.GetY();
                var tileAttribute = mapController.Attributes[tileX, tileY];
                if (tileAttribute != null)
                {
                    if (tileAttribute.Type == MapAttributes.Blocked || (tileAttribute.Type == MapAttributes.Animation && ((MapAnimationAttribute)tileAttribute).IsBlock))
                    {
                        return -2;
                    }

                    if (tileAttribute.Type == MapAttributes.Item && ((MapItemAttribute)tileAttribute).IsBlock)
                    {
                        return -2;
                    }

                    if (tileAttribute.Type == MapAttributes.NpcAvoid && this is Npc)
                    {
                        return -2;
                    }

                    if (tileAttribute.Type == MapAttributes.ZDimension &&
                        ((MapZDimensionAttribute) tileAttribute).BlockedLevel > 0 &&
                        ((MapZDimensionAttribute) tileAttribute).BlockedLevel - 1 == Z)
                    {
                        return -3;
                    }

                    if (tileAttribute.Type == MapAttributes.Slide)
                    {
                        if (this is EventPage)
                        {
                            return -4;
                        }

                        switch (((MapSlideAttribute) tileAttribute).Direction)
                        {
                            case 1:
                                if (moveDir == 1)
                                {
                                    return -4;
                                }

                                break;
                            case 2:
                                if (moveDir == 0)
                                {
                                    return -4;
                                }

                                break;
                            case 3:
                                if (moveDir == 3)
                                {
                                    return -4;
                                }

                                break;
                            case 4:
                                if (moveDir == 2)
                                {
                                    return -4;
                                }

                                break;
                        }
                    }
                }
            }
            else
            {
                return -5; //Out of Bounds
            }

            if (!Passable)
            {
                var targetMap = mapController;
                var mapEntities = new List<Entity>();
                if (mapController.TryGetInstance(MapInstanceId, out var mapInstance))
                {
                    mapEntities.AddRange(mapInstance.GetCachedEntities());
                }
                foreach (var en in mapEntities)
                {
                    if (en != null && en.X == tileX && en.Y == tileY && en.Z == Z && !en.Passable)
                    {
                        //Set a target if a projectile
                        CollisionIndex = en.Id;
                        if (en is Player ply)
                        {
                            if (this is Player)
                            {
                                //Check if this target player is passable....
                                if ((!Options.Instance.Passability.Passable[(int)targetMap.ZoneType] || ply.InDuel) && !ply.PlayerDead)
                                {
                                    return (int)EntityTypes.Player;
                                }
                            }
                            else
                            {
                                return (int)EntityTypes.Player;
                            }
                        }
                        else if (en is Npc n)
                        {
                            if (n.Base.Impassable)
                            {
                                return (int)EntityTypes.Event;
                            }

                            return (int)EntityTypes.Player;
                        }
                        else if (en is Resource resource)
                        {
                            //If determine if we should walk
                            if (!resource.IsPassable())
                            {
                                return (int)EntityTypes.Resource;
                            }
                        }
                    }
                }

                //If this is an npc or other event.. if any global page exists that isn't passable then don't walk here!
                if (!(this is Player) && mapInstance != null)
                {
                    foreach (var evt in mapInstance.GlobalEventInstances)
                    {
                        foreach (var en in evt.Value.GlobalPageInstance)
                        {
                            if (en != null && en.X == tileX && en.Y == tileY && en.Z == Z && !en.Passable)
                            {
                                return (int)EntityTypes.Event;
                            }
                        }
                    }
                }
            }

            return IsTileWalkable(tile.GetMap(), tile.GetX(), tile.GetY(), Z);
        }

        protected virtual int IsTileWalkable(MapController map, int x, int y, int z)
        {
            //Out of bounds if no map
            if (map == null)
            {
                return -5;
            }

            //Otherwise fine
            return -1;
        }

        protected virtual bool ProcessMoveRoute(Player forPlayer, long timeMs)
        {
            var moved = false;
            byte lookDir = 0, moveDir = 0;

            if (MoveRoute.ActionIndex < MoveRoute.Actions.Count)
            {
                Func<Directions, bool> canMove = (Directions dir) => { return CanMove((int)dir, true) == -1; };
                switch (MoveRoute.Actions[MoveRoute.ActionIndex].Type)
                {
                    case MoveRouteEnum.MoveUp:

                        if (canMove(Directions.Up))
                        {
                            Move((int) Directions.Up, forPlayer, false, true);
                            moved = true;
                        }

                        break;
                    case MoveRouteEnum.MoveDown:
                        if (canMove(Directions.Down))
                        {
                            Move((int) Directions.Down, forPlayer, false, true);
                            moved = true;
                        }

                        break;
                    case MoveRouteEnum.MoveLeft:
                        if (canMove(Directions.Left))
                        {
                            Move((int) Directions.Left, forPlayer, false, true);
                            moved = true;
                        }

                        break;
                    case MoveRouteEnum.MoveRight:
                        if (canMove(Directions.Right))
                        {
                            Move((int) Directions.Right, forPlayer, false, true);
                            moved = true;
                        }

                        break;
                    case MoveRouteEnum.MoveRandomly:
                        var dir = (byte)Randomization.Next(0, 4);
                        if (canMove((Directions)dir))
                        {
                            Move(dir, forPlayer);
                            moved = true;
                        }

                        break;
                    case MoveRouteEnum.StepForward:
                        if (canMove((Directions)Dir))
                        {
                            Move((byte) Dir, forPlayer, false, true);
                            moved = true;
                        }

                        break;
                    case MoveRouteEnum.StepBack:
                        switch (Dir)
                        {
                            case (int) Directions.Up:
                                moveDir = (int) Directions.Down;

                                break;
                            case (int) Directions.Down:
                                moveDir = (int) Directions.Up;

                                break;
                            case (int) Directions.Left:
                                moveDir = (int) Directions.Right;

                                break;
                            case (int) Directions.Right:
                                moveDir = (int) Directions.Left;

                                break;
                        }

                        if (canMove((Directions) moveDir))
                        {
                            Move(moveDir, forPlayer, false, true);
                            moved = true;
                        }

                        break;
                    case MoveRouteEnum.FaceUp:
                        ChangeDir((int) Directions.Up);
                        moved = true;

                        break;
                    case MoveRouteEnum.FaceDown:
                        ChangeDir((int) Directions.Down);
                        moved = true;

                        break;
                    case MoveRouteEnum.FaceLeft:
                        ChangeDir((int) Directions.Left);
                        moved = true;

                        break;
                    case MoveRouteEnum.FaceRight:
                        ChangeDir((int) Directions.Right);
                        moved = true;

                        break;
                    case MoveRouteEnum.Turn90Clockwise:
                        switch (Dir)
                        {
                            case (int) Directions.Up:
                                lookDir = (int) Directions.Right;

                                break;
                            case (int) Directions.Down:
                                lookDir = (int) Directions.Left;

                                break;
                            case (int) Directions.Left:
                                lookDir = (int) Directions.Up;

                                break;
                            case (int) Directions.Right:
                                lookDir = (int) Directions.Down;

                                break;
                        }

                        ChangeDir(lookDir);
                        moved = true;

                        break;
                    case MoveRouteEnum.Turn90CounterClockwise:
                        switch (Dir)
                        {
                            case (int)Directions.Up:
                                lookDir = (int)Directions.Left;

                                break;
                            case (int)Directions.Down:
                                lookDir = (int)Directions.Right;

                                break;
                            case (int)Directions.Left:
                                lookDir = (int)Directions.Down;

                                break;
                            case (int)Directions.Right:
                                lookDir = (int)Directions.Up;

                                break;
                        }

                        ChangeDir(lookDir);
                        moved = true;

                        break;
                    case MoveRouteEnum.Turn180:
                        switch (Dir)
                        {
                            case (int) Directions.Up:
                                lookDir = (int) Directions.Down;

                                break;
                            case (int) Directions.Down:
                                lookDir = (int) Directions.Up;

                                break;
                            case (int) Directions.Left:
                                lookDir = (int) Directions.Right;

                                break;
                            case (int) Directions.Right:
                                lookDir = (int) Directions.Left;

                                break;
                        }

                        ChangeDir(lookDir);
                        moved = true;

                        break;
                    case MoveRouteEnum.TurnRandomly:
                        ChangeDir((byte)Randomization.Next(0, 4));
                        moved = true;

                        break;
                    case MoveRouteEnum.Wait100:
                        MoveTimer = Timing.Global.Milliseconds + 100;
                        moved = true;

                        break;
                    case MoveRouteEnum.Wait500:
                        MoveTimer = Timing.Global.Milliseconds + 500;
                        moved = true;

                        break;
                    case MoveRouteEnum.Wait1000:
                        MoveTimer = Timing.Global.Milliseconds + 1000;
                        moved = true;

                        break;
                    default:
                        //Gonna end up returning false because command not found
                        return false;
                }

                if (moved || MoveRoute.IgnoreIfBlocked)
                {
                    MoveRoute.ActionIndex++;
                    if (MoveRoute.ActionIndex >= MoveRoute.Actions.Count)
                    {
                        if (MoveRoute.RepeatRoute)
                        {
                            MoveRoute.ActionIndex = 0;
                        }

                        MoveRoute.Complete = true;
                    }
                }

                if (moved && MoveTimer < Timing.Global.Milliseconds)
                {
                    MoveTimer = Timing.Global.Milliseconds + (long) GetMovementTime();
                }
            }

            return true;
        }

        public virtual bool IsPassable()
        {
            return Passable;
        }

        //Returns the amount of time required to traverse 1 tile
        public virtual float GetMovementTime(int fromSpeed = -1)
        {
            var speed = fromSpeed;
            if (speed < 0)
            {
                speed = Stat[(int)Stats.Speed].Value();
            }
            if (this is Player player && player.InVehicle && player.VehicleSpeed > 0L)
            {
                speed = (int) player.VehicleSpeed;
            }

            // Old calc
            //var time = 1000f / (float)(1 + Math.Log(speed * Options.AgilityMovementSpeedModifier));
            var time = Options.BaseSpeed * Math.Exp(-1 * Options.Instance.CombatOpts.SpeedExp * speed);

            if (Blocking)
            {
                time += time * (float)Options.BlockingSlow;
            }

            time *= (float)Options.SpeedModifier;

            if (this is Player pl && pl.CombatMode)
            {
                var moveDir = pl.GetRealDir();
                var faceDir = pl.FaceDirection;
                var maximumTime = time;

                var strafeBonus = 0;
                var backstepBonus = 0;
                if (pl.Equipment[Options.WeaponIndex] > -1)
                {
                    var weapon = pl.Items[pl.Equipment[Options.WeaponIndex]];
                    strafeBonus = weapon?.Descriptor?.StrafeBonus / 100 ?? 0;
                    backstepBonus = weapon?.Descriptor?.BackstepBonus / 100 ?? 0;
                }

                // Bonuses apply as percentages of the original speed modifiers - if backstep is 50% slower, and you have 10% backstep bonus, then backstep is now 50 - (.1 * 50) = 45% slower
                var backstepModifier = Options.Instance.CombatOpts.CombatModeBackModifier - ((Options.Instance.CombatOpts.CombatModeBackModifier - 1) * backstepBonus);
                var strafeModifier = Options.Instance.CombatOpts.CombatModeStrafeModifier - ((Options.Instance.CombatOpts.CombatModeStrafeModifier - 1) * strafeBonus);

                if (moveDir != faceDir)
                {
                    switch (moveDir)
                    {
                        //up
                        case 0:
                            if (faceDir == 1)
                            {
                                time *= backstepModifier;
                            }
                            else
                            {
                                time *= strafeModifier;
                            }

                            break;
                        //down
                        case 1:
                            if (faceDir == 0)
                            {
                                time *= backstepModifier;
                            }
                            else
                            {
                                time *= strafeModifier;
                            }
                            break;
                        //left
                        case 2:
                            if (faceDir == 3)
                            {
                                time *= backstepModifier;
                            }
                            else
                            {
                                time *= strafeModifier;
                            }
                            break;
                        //right
                        case 3:
                            if (faceDir == 2)
                            {
                                time *= backstepModifier;
                            }
                            else
                            {
                                time *= strafeModifier;
                            }
                            break;
                    }

                    time = (float)MathHelper.Clamp(time, maximumTime, float.MaxValue);
                }
            }
            if (StatusActive(StatusTypes.Slowed))
            {
                time *= Options.Instance.CombatOpts.SlowedModifier;
            }
            else if (StatusActive(StatusTypes.Haste))
            {
                time /= Options.Instance.CombatOpts.HasteModifier;
            }

            return Math.Min(1000f, (float)time);
        }

        public virtual EntityTypes GetEntityType()
        {
            return EntityTypes.GlobalEntity;
        }

        public virtual void Move(int moveDir, Player forPlayer)
        {
            Move(moveDir, forPlayer, false, false, -1);
        }

        public virtual void Move(int moveDir, Player forPlayer, bool doNotUpdate)
        {
            Move(moveDir, forPlayer, doNotUpdate, false, -1);
        }

        public virtual void Move(int moveDir, Player forPlayer, bool doNotUpdate, bool correction)
        {
            Move(moveDir, forPlayer, doNotUpdate, correction, -1);
        }

        public virtual void Move(int moveDir, Player forPlayer, bool doNotUpdate = false, bool correction = false, int faceDirection = -1)
        {
            var now = Timing.Global.Milliseconds;
            if (now < MoveTimer || (!Options.Combat.MovementCancelsCast && CastTime > 0) || now < DashTransmissionTimer)
            {
                return;
            }

            lock (EntityLock)
            {
                // If we're not specifying a face direction, just face the movement direction
                if (faceDirection == -1)
                {
                    faceDirection = moveDir;
                }

                if (this is Player pl && CastTime > 0 && Options.Combat.MovementCancelsCast)
                {
                    pl.CancelCast();
                }

                var xOffset = 0;
                var yOffset = 0;

                switch (moveDir)
                {
                    case 0: //Up
                        --yOffset;

                        break;
                    case 1: //Down
                        ++yOffset;

                        break;
                    case 2: //Left
                        --xOffset;

                        break;
                    case 3: //Right
                        ++xOffset;

                        break;

                    default:
                        Log.Warn(
                            new ArgumentOutOfRangeException(nameof(moveDir), $@"Bogus move attempt in direction {moveDir}.")
                        );

                        return;
                }

                // We are facing the direction given to us by the client
                Dir = moveDir;

                var tile = new TileHelper(MapId, X, Y);

                // ReSharper disable once InvertIf
                if (tile.Translate(xOffset, yOffset))
                {
                    X = tile.GetX();
                    Y = tile.GetY();

                    var currentMap = MapController.Get(tile.GetMapId());
                    if (MapId != tile.GetMapId())
                    {
                        var oldMap = MapController.Get(MapId);
                        if (oldMap.TryGetInstance(MapInstanceId, out var oldInstance)) {
                            oldInstance.RemoveEntity(this);
                        }

                        if (currentMap.TryGetInstance(MapInstanceId, out var newInstance))
                        {
                            newInstance.AddEntity(this);
                        }

                        //Send Left Map Packet To the Maps that we are no longer with
                        var oldMaps = oldMap?.GetSurroundingMaps(true);
                        var newMaps = currentMap?.GetSurroundingMaps(true);

                        MapId = tile.GetMapId();

                        if (oldMaps != null)
                        {
                            foreach (var map in oldMaps)
                            {
                                if (newMaps == null || !newMaps.Contains(map))
                                {
                                    PacketSender.SendEntityLeaveMap(this, map.Id);
                                }
                            }
                        }


                        if (newMaps != null)
                        {
                            foreach (var map in newMaps)
                            {
                                if (oldMaps == null || !oldMaps.Contains(map))
                                {
                                    PacketSender.SendEntityDataToMap(this, map, this as Player);
                                }
                            }
                        }

                    }

                    if (doNotUpdate == false)
                    {
                        if (this is EventPageInstance)
                        {
                            if (forPlayer != null)
                            {
                                PacketSender.SendEntityMoveTo(forPlayer, this, correction);
                            }
                            else
                            {
                                PacketSender.SendEntityMove(this, correction);
                            }
                        }
                        else
                        {
                            // Preserve face direction if player
                            if (this is Player ply)
                            {
                                PacketSender.SendEntityMove(ply, ply.FaceDirection, ply.CombatMode, correction);
                            }
                            else
                            {
                                PacketSender.SendEntityMove(this, correction);
                            }
                        }

                        //Check if moving into a projectile.. if so this npc needs to be hit
                        if (currentMap != null)
                        {
                            foreach (var instance in MapController.GetSurroundingMapInstances(currentMap.Id, MapInstanceId, true))
                            {
                                var projectiles = instance.MapProjectilesCached;
                                foreach (var projectile in projectiles)
                                {
                                    var spawns = projectile?.Spawns?.ToArray() ?? Array.Empty<ProjectileSpawn>();
                                    foreach (var spawn in spawns)
                                    {
                                        // TODO: Filter in Spawns variable, there should be no nulls. See #78 for evidence it is null.
                                        if (spawn == null)
                                        {
                                            continue;
                                        }

                                        if (spawn.IsAtLocation(MapId, X, Y, Z) && spawn.HitEntity(this))
                                        {
                                            spawn.Dead = true;
                                        }
                                    }
                                }
                            }
                        }

                        MoveTimer = Timing.Global.Milliseconds + (long)GetMovementTime();
                    }

                    if (TryToChangeDimension() && doNotUpdate == true)
                    {
                        PacketSender.UpdateEntityZDimension(this, (byte)Z);
                    }

                    //Check for traps
                    if (MapController.TryGetInstanceFromMap(currentMap.Id, MapInstanceId, out var mapInstance))
                    {
                        foreach (var trap in mapInstance.MapTrapsCached)
                        {
                            trap.CheckEntityHasDetonatedTrap(this);
                        }
                    }

                    // TODO: Why was this scoped to only Event entities?
                    //                if (currentMap != null && this is EventPageInstance)
                    var attribute = currentMap?.Attributes[X, Y];

                    // ReSharper disable once InvertIf
                    //Check for slide tiles
                    if (attribute?.Type == MapAttributes.Slide)
                    {
                        // If sets direction, set it.
                        if (((MapSlideAttribute)attribute).Direction > 0)
                        {
                            //Check for slide tiles
                            if (attribute != null && attribute.Type == MapAttributes.Slide)
                            {
                                if (((MapSlideAttribute)attribute).Direction > 0)
                                {
                                    Dir = (byte)(((MapSlideAttribute)attribute).Direction - 1);
                                }
                            }
                        }

                        _ = new Dash(this, 1, (byte)Dir);
                    }
                }
            }
        }

        public void ChangeDir(int dir)
        {
            if (dir == -1)
            {
                return;
            }

            if (Dir != dir)
            {
                if (this is Player pl)
                {
                    pl.FaceDirection = dir;
                }
                Dir = dir;

                if (this is EventPageInstance eventPageInstance && eventPageInstance.Player != null)
                {
                    if (((EventPageInstance)this).Player != null)
                    {
                        PacketSender.SendEntityDirTo(((EventPageInstance)this).Player, this);
                    }
                    else
                    {
                        PacketSender.SendEntityDir(this);
                    }
                }
                else
                {
                    PacketSender.SendEntityDir(this);
                }
            }

            if (this is Player player && player.resourceLock != null)
            {
                player.SetResourceLock(false);
            }
        }

        // Change the dimension if the player is on a gateway
        public bool TryToChangeDimension()
        {
            if (X < Options.MapWidth && X >= 0)
            {
                if (Y < Options.MapHeight && Y >= 0)
                {
                    var attribute = MapController.Get(MapId).Attributes[X, Y];
                    if (attribute != null && attribute.Type == MapAttributes.ZDimension)
                    {
                        if (((MapZDimensionAttribute) attribute).GatewayTo > 0)
                        {
                            Z = (byte) (((MapZDimensionAttribute) attribute).GatewayTo - 1);

                            return true;
                        }
                    }
                }
            }

            return false;
        }

        //Misc
        public int GetDirectionTo(Entity target)
        {
            int xDiff = 0, yDiff = 0;

            var map = MapController.Get(MapId);
            var gridId = map.MapGrid;
            var grid = DbInterface.GetGrid(gridId);

            //Loop through surrouding maps to generate a array of open and blocked points.
            for (var x = map.MapGridX - 1; x <= map.MapGridX + 1; x++)
            {
                if (x == -1 || x >= grid.Width)
                {
                    continue;
                }

                for (var y = map.MapGridY - 1; y <= map.MapGridY + 1; y++)
                {
                    if (y == -1 || y >= grid.Height)
                    {
                        continue;
                    }

                    if (grid.MyGrid[x, y] != Guid.Empty &&
                        grid.MyGrid[x, y] == target.MapId)
                    {
                        xDiff = (x - map.MapGridX) * Options.MapWidth + target.X - X;
                        yDiff = (y - map.MapGridY) * Options.MapHeight + target.Y - Y;
                        if (Math.Abs(xDiff) > Math.Abs(yDiff))
                        {
                            if (xDiff < 0)
                            {
                                return (int) Directions.Left;
                            }

                            if (xDiff > 0)
                            {
                                return (int) Directions.Right;
                            }
                        }
                        else
                        {
                            if (yDiff < 0)
                            {
                                return (int) Directions.Up;
                            }

                            if (yDiff > 0)
                            {
                                return (int) Directions.Down;
                            }
                        }
                    }
                }
            }

            return -1;
        }

        //Combat
        public virtual int CalculateAttackTime()
        {
            return (int) (Options.MaxAttackRate +
                          (float) ((Options.MinAttackRate - Options.MaxAttackRate) *
                                   (((float) Options.MaxStatValue - Stat[(int) Stats.Speed].Value()) /
                                    (float) Options.MaxStatValue)));
        }

        public virtual int GetWeaponDamage()
        {
            return 0;
        }

        public virtual bool CanAttack(Entity entity, SpellBase spell)
        {
            return entity != null && !entity.IsDisposed && !entity.IsStealthed;
        }

        public virtual void ProcessRegen()
        {
            if (IsDead())
            {
                return;
            }

            foreach (Vitals vital in Enum.GetValues(typeof(Vitals)))
            {
                if (vital == Vitals.Mana)
                {
                    continue; // We process mana regen differently in MAO, see "ProcessManaRegen"
                }
                if (vital >= Vitals.VitalCount)
                {
                    continue;
                }

                if (vital == Vitals.Health)
                {
                    if (MapRegenType == RegenType.NoHP || MapRegenType == RegenType.NoRegen)
                    {
                        continue;
                    }
                }

                var vitalId = (int)vital;
                var vitalValue = GetVital(vital);
                var maxVitalValue = GetMaxVital(vital);
                if (vitalValue >= maxVitalValue)
                {
                    continue;
                }

                var vitalRegenRate = GetVitalRegenRate(vitalId);
                var regenValue = (int)Math.Max(1, maxVitalValue * vitalRegenRate) *
                                 Math.Abs(Math.Sign(vitalRegenRate));

                AddVital(vital, regenValue);
            }
        }

        public int GetVital(int vital)
        {
            return mVitals[vital];
        }

        public int[] GetVitals()
        {
            var vitals = new int[(int) Vitals.VitalCount];
            Array.Copy(mVitals, 0, vitals, 0, (int) Vitals.VitalCount);

            return vitals;
        }

        public int GetVital(Vitals vital)
        {
            return GetVital((int) vital);
        }

        public void SetVital(int vital, int value)
        {
            if (value < 0)
            {
                value = 0;
            }

            if (GetMaxVital(vital) < value)
            {
                value = GetMaxVital(vital);
            }

            if (vital == (int)Vitals.Health && this is Player player)
            {
                player.CheckForHPWarning(value);
            }

            mVitals[vital] = value;
        }

        public void SetVital(Vitals vital, int value)
        {
            SetVital((int) vital, value);
        }

        public virtual int GetMaxVital(int vital)
        {
            return _maxVital[vital];
        }

        public virtual int GetMaxVital(Vitals vital)
        {
            return GetMaxVital((int) vital);
        }

        public int[] GetMaxVitals()
        {
            var vitals = new int[(int) Vitals.VitalCount];
            for (var vitalIndex = 0; vitalIndex < vitals.Length; ++vitalIndex)
            {
                vitals[vitalIndex] = GetMaxVital(vitalIndex);
            }

            return vitals;
        }

        public void SetMaxVital(int vital, int value)
        {
            if (value <= 0 && vital == (int) Vitals.Health)
            {
                value = 1; //Must have at least 1 hp
            }

            if (value < 0 && vital == (int) Vitals.Mana)
            {
                value = 0; //Can't have less than 0 mana
            }

            _maxVital[vital] = value;
            if (value < GetVital(vital))
            {
                SetVital(vital, value);
            }
        }

        public void SetMaxVital(Vitals vital, int value)
        {
            SetMaxVital((int) vital, value);
        }

        public bool HasVital(Vitals vital)
        {
            return GetVital(vital) > 0;
        }

        public bool IsFullVital(Vitals vital)
        {
            return GetVital(vital) == GetMaxVital(vital);
        }

        //Vitals
        public void RestoreVital(Vitals vital)
        {
            SetVital(vital, GetMaxVital(vital));
        }

        public void AddVital(Vitals vital, int amount)
        {
            if (vital >= Vitals.VitalCount)
            {
                return;
            }

            var vitalId = (int) vital;
            var maxVitalValue = GetMaxVital(vitalId);
            var safeAmount = Math.Min(amount, int.MaxValue - maxVitalValue);
            SetVital(vital, GetVital(vital) + safeAmount);
        }

        public void SubVital(Vitals vital, int amount)
        {
            if (vital >= Vitals.VitalCount)
            {
                return;
            }

            //Check for any shields.
            var oldAmount = amount;
            foreach (var status in CachedStatuses)
            {
                if (status.Type == StatusTypes.Shield)
                {
                    status.DamageShield(vital, ref amount);
                    if (oldAmount != amount)
                    {
                        PacketSender.SendMapEntityStatusUpdate(Map, this, MapInstanceId);
                        // Send shield party update
                        if (this is Player player)
                        {
                            for (var i = 0; i < player.Party.Count; i++)
                            {
                                PacketSender.SendPartyUpdateTo(player.Party[i], player);
                            }
                        }
                    }
                }
            }

            // Reset mana regen each time a spell is cast
            if (vital == Vitals.Mana)
            {
                ManaRegenTimer = Timing.Global.Milliseconds + Options.Instance.CombatOpts.ManaRegenTime;
            }

            var vitalId = (int) vital;
            var maxVitalValue = GetMaxVital(vitalId);
            var safeAmount = Math.Min(amount, GetVital(vital));
            SetVital(vital, GetVital(vital) - safeAmount);
        }

        public virtual int[] GetStatValues()
        {
            var stats = new int[(int) Stats.StatCount];
            for (var i = 0; i < (int) Stats.StatCount; i++)
            {
                stats[i] = Stat[i].Value();
            }

            return stats;
        }

        public virtual bool IsAllyOf(Entity otherEntity)
        {
            return this == otherEntity;
        }

        protected void Animate(Entity target, List<KeyValuePair<Guid, sbyte>> animations, bool fromProjectile = false)
        {
            foreach (var anim in animations)
            {
                PacketSender.SendAnimationToProximity(anim.Key, 1, Id, target.MapId, 0, 0, anim.Value, MapInstanceId, fromProjectile);
            }
        }

        public enum AttackFailures
        {
            BLOCKED,
            MISSED
        }

        public enum AttackType
        {
            Unarmed,
            Weapon,
            Projectile,
            Spell,
        }

        protected void SendMissedAttackMessage(Entity en, DamageType damageType)
        {
            if (en == null) return;

            switch (damageType)
            {
                case DamageType.Magic:
                    PacketSender.SendActionMsg(en, Strings.Combat.resist, CustomColors.Combat.Missed, Options.MissSound);
                    break;
                default:
                    PacketSender.SendActionMsg(en, Strings.Combat.miss, CustomColors.Combat.Missed, Options.MissSound);
                    break;
            }
        }

        protected void SendBlockedAttackMessage(Entity en)
        {
            if (en == null) return;

            PacketSender.SendActionMsg(en, Strings.Combat.blocked, CustomColors.Combat.Blocked, Options.BlockSound);
        }

        protected void SendCombatEffects(Entity enemy, bool isCrit, int damage)
        {
            if (damage == 0)
            {
                return;
            }

            // Calculate combat special effects (entity/screen flash, screen shake, extra sounds)
            // Define vars that will be used for combat effects
            Color flashColor = null;
            Color entityFlashColor = CustomColors.Combat.GenericDamageGiveEntityFlashColor;
            float flashIntensity = 0.0f;
            float flashDuration = Options.HitFlashDuration;
            string damageSound = "";

            if (this is Player player && enemy.Id != player.Id) // if the player is targeting themselves don't bother with any of this, just skip to the part where we send to the enemy
            {
                // Calc distances so shakes aren't as violent when further away
                int enemyDistance = GetDistanceTo(enemy);
                float shakeModifier = 1.0f;
                if (enemyDistance > 1)
                {
                    shakeModifier = shakeModifier - (enemyDistance / Options.MaxDamageShakeDistance); // Don't shake if more than 6 tiles away
                    shakeModifier = (float)MathHelper.Clamp(shakeModifier, 0.0f, 1.0f);
                }

                if (string.IsNullOrEmpty(damageSound) && damage > 0)
                {
                    damageSound = Options.GenericDamageGivenSound;
                }

                if (player.Client != null && !(enemy is Resource))
                {
                    var shakeAmount = Options.DamageGivenShakeAmount * shakeModifier;
                    if (isCrit)
                    {
                        flashColor = CustomColors.Combat.CriticalHitDealtColor;
                        flashIntensity = Options.CriticalHitFlashIntensity;
                        damageSound = Options.CriticalHitDealtSound;
                    }

                    if (damage < 0) // healing exceptions
                    {
                        entityFlashColor = CustomColors.Combat.GenericHealingReceivedEntityFlashColor;
                        flashColor = CustomColors.Combat.HealingFlashColor;
                        entityFlashColor = CustomColors.Combat.GenericHealingReceivedEntityFlashColor;
                        flashColor = CustomColors.Combat.HealingFlashColor;
                        shakeAmount = 0.0f;
                    }

                    PacketSender.SendCombatEffectPacket(player.Client,
                        enemy.Id,
                        shakeAmount,
                        entityFlashColor,
                        damageSound,
                        flashIntensity,
                        flashDuration,
                        flashColor);
                }
                else if (enemy is Resource)
                {
                    if (enemy.GetVital(Vitals.Health) <= 0)
                    {
                        // Only shake for an exhausted resource
                        PacketSender.SendCombatEffectPacket(player.Client,
                            enemy.Id,
                            Options.ResourceDestroyedShakeAmount * shakeModifier,
                            null, // Don't want a resource to flash
                            "", // Don't want a resource to make a hit sound
                            0.0f,
                            0.0f,
                            null);
                    }
                }
            }

            // Send damaged effects to enemy - this will make their screen flash if critted, play THEIR hurt sound, and also make THEIR entity sprite flash
            if (enemy.GetVital(Vitals.Health) > 0 && enemy is Player en)
            {
                var shakeAmount = Options.DamageTakenShakeAmount;
                flashIntensity = Options.DamageTakenFlashIntensity;
                
                if (damage < 0) // healing exceptions
                {
                    entityFlashColor = CustomColors.Combat.GenericHealingReceivedEntityFlashColor;
                    flashColor = CustomColors.Combat.HealingFlashColor;
                    shakeAmount = 0.0f;
                    damageSound = string.Empty;
                }
                else if (damage > 0)
                {
                    flashColor = CustomColors.Combat.DamageTakenFlashColor;
                    flashIntensity = Options.DamageTakenFlashIntensity;
                    damageSound = Options.GenericDamageReceivedSound;

                    if (isCrit)
                    {
                        flashColor = CustomColors.Combat.CriticalHitReceivedColor;
                        flashIntensity = Options.CriticalHitFlashIntensity;
                        damageSound = Options.CriticalHitReceivedSound;
                    }
                }
                
                PacketSender.SendCombatEffectPacket(en.Client,
                        en.Id,
                        shakeAmount,
                        entityFlashColor,
                        damageSound,
                        flashIntensity,
                        flashDuration,
                        flashColor);
            }
        }

        public virtual void KilledEntity(Entity entity)
        {
        }

        protected int[] GetPositionNearTarget(Guid mapId, int x, int y, int dir)
        {
            if (MapController.TryGetInstanceFromMap(mapId, MapInstanceId, out var instance))
            {
                List<int[]> validPosition = new List<int[]>();

                // Start by north, west, est and south
                for (int col = -1; col < 2; col++)
                {
                    for (int row = -1; row < 2; row++)
                    {
                        if (Math.Abs(col % 2) != Math.Abs(row % 2))
                        {
                            int newX = x + row;
                            int newY = y + col;

                            if (newX >= 0 && newX <= Options.MapWidth &&
                                newY >= 0 && newY <= Options.MapHeight &&
                                !instance.TileBlocked(newX, newY))
                            {
                                validPosition.Add(new int[] { newX, newY });
                            }
                        }
                    }
                }

                if (validPosition.Count > 0)
                {
                    // Prefer the _back_ of the target, if possible
                    var idealIdx = -1;
                    switch (dir)
                    {
                        case (int)Directions.Down:
                            idealIdx = validPosition.FindIndex((pos) =>
                            {
                                return pos[0] == x && pos[1] == y - 1;
                            });
                            break;
                        case (int)Directions.Up:
                            idealIdx = validPosition.FindIndex((pos) =>
                            {
                                return pos[0] == x && pos[1] == y + 1;
                            });
                            break;
                        case (int)Directions.Left:
                            idealIdx = validPosition.FindIndex((pos) =>
                            {
                                return pos[0] == x + 1 && pos[1] == y;
                            });
                            break;
                        case (int)Directions.Right:
                            idealIdx = validPosition.FindIndex((pos) =>
                            {
                                return pos[0] == x - 1 && pos[1] == y;
                            });
                            break;
                    }

                    if (idealIdx != -1)
                    {
                        return validPosition[idealIdx];
                    }
                    else
                    {
                        return validPosition[Randomization.Next(0, validPosition.Count)];
                    }
                }

                // If nothing found, diagonal direction
                for (int col = -1; col < 2; col++)
                {
                    for (int row = -1; row < 2; row++)
                    {
                        if (Math.Abs(col % 2) == Math.Abs(row % 2))
                        {
                            int newX = x + row;
                            int newY = y + col;

                            // Tile must not be the target position
                            if (newX >= 0 && newX <= Options.MapWidth &&
                                newY >= 0 && newY <= Options.MapHeight &&
                                !(x + row == x && y + col == y) &&
                                !instance.TileBlocked(newX, newY))
                            {
                                validPosition.Add(new int[] { newX, newY });
                            }
                        }
                    }
                }

                if (validPosition.Count > 0)
                {
                    return validPosition[Randomization.Next(0, validPosition.Count)];
                }

                // If nothing found, return target position
                return new int[] { x, y }; 
            } else
            {
                return new int[] { x, y };
            }
        }

        //Check if the target is either up, down, left or right of the target on the correct Z dimension.
        protected bool TargetInMeleeRange(Entity target)
        {
            var myTile = new TileHelper(MapId, X, Y);
            var enemyTile = new TileHelper(target.MapId, target.X, target.Y);
            if (Z != target.Z)
            {
                return false;
            }

            myTile.Translate(0, -1); // Target Up
            if (myTile.Matches(enemyTile))
            {
                return true;
            }

            myTile.Translate(0, 2); // Target Down
            if (myTile.Matches(enemyTile))
            {
                return true;
            }

            myTile.Translate(-1, -1); // Target Left
            if (myTile.Matches(enemyTile))
            {
                return true;
            }

            myTile.Translate(2, 0); // Target Right 
            if (myTile.Matches(enemyTile))
            {
                return true;
            }

            return false;
        }

        //These functions only work when one block away.
        protected bool IsFacingTarget(Entity target)
        {
            if (!TargetInMeleeRange(target))
            {
                return false;
            }

            var myTile = new TileHelper(MapId, X, Y);
            var enemyTile = new TileHelper(target.MapId, target.X, target.Y);
            myTile.Translate(0, -1);
            if (myTile.Matches(enemyTile) && Dir == (int)Directions.Up)
            {
                return true;
            }

            myTile.Translate(0, 2);
            if (myTile.Matches(enemyTile) && Dir == (int)Directions.Down)
            {
                return true;
            }

            myTile.Translate(-1, -1);
            if (myTile.Matches(enemyTile) && Dir == (int)Directions.Left)
            {
                return true;
            }

            myTile.Translate(2, 0);
            if (myTile.Matches(enemyTile) && Dir == (int)Directions.Right)
            {
                return true;
            }

            return false;
        }

        public int GetDistanceTo(Entity target)
        {
            if (target != null)
            {
                return GetDistanceTo(target.Map, target.X, target.Y);
            }
            //Something is null.. return a value that is out of range :) 
            return 9999;
        }

        public int GetDistanceTo(MapController targetMap, int targetX, int targetY)
        {
            return GetDistanceBetween(Map, targetMap, X, targetX, Y, targetY);
        }

        public int GetDistanceBetween(MapController mapA, MapController mapB, int xTileA, int xTileB, int yTileA, int yTileB)
        {
            if (mapA != null && mapB != null && mapA.MapGrid == mapB.MapGrid
            ) //Make sure both maps exist and that they are in the same dimension
            {
                //Calculate World Tile of Me
                var x1 = xTileA + mapA.MapGridX * Options.MapWidth;
                var y1 = yTileA + mapA.MapGridY * Options.MapHeight;

                //Calculate world tile of target
                var x2 = xTileB + mapB.MapGridX * Options.MapWidth;
                var y2 = yTileB + mapB.MapGridY * Options.MapHeight;

                return (int)Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
            }

            //Something is null.. return a value that is out of range :) 
            return 9999;
        }

        public bool InRangeOf(Entity target, int range)
        {
            if (target is Player player && player.PlayerDead)
            {
                return false;
            }

            var dist = GetDistanceTo(target);
            if (dist == 9999)
            {
                return false;
            }

            if (dist <= range)
            {
                return true;
            }

            return false;
        }

        public virtual void NotifySwarm(Entity attacker)
        {
        }

        protected byte DirToEnemy(Entity target)
        {
            //Calculate World Tile of Me
            var x1 = X + MapController.Get(MapId).MapGridX * Options.MapWidth;
            var y1 = Y + MapController.Get(MapId).MapGridY * Options.MapHeight;

            //Calculate world tile of target
            var x2 = target.X + MapController.Get(target.MapId).MapGridX * Options.MapWidth;
            var y2 = target.Y + MapController.Get(target.MapId).MapGridY * Options.MapHeight;


            if (Math.Abs(x1 - x2) > Math.Abs(y1 - y2))
            {
                //Left or Right
                if (x1 - x2 < 0)
                {
                    return (byte) Directions.Right;
                }

                return (byte) Directions.Left;
            }

            //Left or Right
            if (y1 - y2 < 0)
            {
                return (byte) Directions.Down;
            }

            return (byte) Directions.Up;
        }

        // Outdated : Check if the target is either up, down, left or right of the target on the correct Z dimension.
        // Check for 8 directions
        protected bool IsOneBlockAway(Guid mapId, int x, int y, int z = 0)
        {
            //Calculate World Tile of Me
            var x1 = X + MapController.Get(MapId).MapGridX * Options.MapWidth;
            var y1 = Y + MapController.Get(MapId).MapGridY * Options.MapHeight;

            //Calculate world tile of target
            var x2 = x + MapController.Get(mapId).MapGridX * Options.MapWidth;
            var y2 = y + MapController.Get(mapId).MapGridY * Options.MapHeight;
            if (z == Z)
            {
                if (y1 == y2)
                {
                    if (x1 == x2 - 1)
                    {
                        return true;
                    }
                    else if (x1 == x2 + 1)
                    {
                        return true;
                    }
                }

                if (x1 == x2)
                {
                    if (y1 == y2 - 1)
                    {
                        return true;
                    }
                    else if (y1 == y2 + 1)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        protected void PlayDeathAnimation()
        {
            if (DeathAnimation != Guid.Empty)
            {
                PacketSender.SendAnimationToProximity(DeathAnimation, -1, Id, MapId, (byte) X, (byte) Y, (sbyte)Directions.Up, MapInstanceId);
            }
            if (this is Player)
            {
                PacketSender.SendAnimationToProximity(new Guid(Options.PlayerDeathAnimationId), -1, Id, MapId, (byte) X, (byte) Y, (sbyte)Directions.Up, MapInstanceId);
            }
        }

        //Spawning/Dying
        public virtual void Die(bool dropItems = true, Entity killer = null, bool transform = false)
        {
            if (IsDead() || Items == null)
            {
                return;
            }

            if (dropItems || transform)
            {
                PlayDeathAnimation();
            }

            // Run events and other things.
            if (!transform)
            {
                killer?.KilledEntity(this);
            }

            if (dropItems)
            {
                // If this is an NPC, drop loot for every single player that participated in the fight.
                if (this is Npc npc && npc.Base.IndividualizedLoot)
                {
                    npc.DropIndividualizedLoot();
                }
                else
                {
                    // Drop as normal.
                    DropItems(killer);
                }
            }
            
            foreach (var instance in MapController.GetSurroundingMapInstances(MapId, MapInstanceId, true))
            {
                instance.ClearEntityTargetsOf(this);
            }

            DoT?.Clear();
            CachedDots = new DoT[0];
            Statuses?.Clear();
            CachedStatuses = new Status[0];
            Stat?.ToList().ForEach(stat => stat?.Reset());

            if (!transform)
            {
                Dead = true;
            }
        }

        public virtual void DropItems(Entity killer, bool sendUpdate = true)
        {
            for (var n = 0; n < Items.Count; n++)
            {
                if (Items[n] == null)
                {
                    continue;
                }

                // Don't mess with the actual object.
                var item = Items[n].Clone();
                
                var itemBase = ItemBase.Get(item.ItemId);
                if (itemBase == null)
                {
                    continue;
                }

                //Calculate the killers luck (If they are a player)
                var playerKiller = killer as Player;
                var luck = 1 + playerKiller?.GetBonusEffectTotal(EffectType.Luck) / 100f;

                Guid lootOwner = Guid.Empty;

                //Npc drop rates
                var randomChance = Randomization.Next(1, 100001);
                if (randomChance >= (item.DropChance * 1000) * luck)
                {
                    continue;
                }

                // Set owner to player that killed this, if there is any.
                if (playerKiller != null)
                {
                    // Yes, so set the owner to the player that killed it.
                    lootOwner = playerKiller.Id;
                }

                // Set the attributes for this item.
                item.Set(new Item(item.ItemId, item.Quantity, item.ItemProperties));

                // Spawn the actual item!
                if (MapController.TryGetInstanceFromMap(MapId, MapInstanceId, out var instance))
                {
                    instance.SpawnItem(X, Y, item, item.Quantity, lootOwner, sendUpdate);
                }
            }
        }

        public virtual bool IsDead()
        {
            return Dead;
        }

        public virtual void Reset()
        {
            for (var i = 0; i < (int) Vitals.VitalCount; i++)
            {
                if (this is Player player)
                {
                    if (player.InstanceType == MapInstanceType.Shared && !Options.Instance.Instancing.RegenManaOnInstanceDeath && i == (int)Vitals.Mana)
                    {
                        continue;
                    }
                }
                
                RestoreVital((Vitals) i);
            }

            Dead = false;
        }

        //Empty virtual functions for players
        public virtual void Warp(Guid newMapId, float newX, float newY, bool adminWarp = false)
        {
            Warp(newMapId, newX, newY, (byte) Dir, adminWarp);
        }

        public virtual void Warp(
            Guid newMapId,
            float newX,
            float newY,
            byte newDir,
            bool adminWarp = false,
            byte zOverride = 0,
            bool mapSave = false,
            bool fromWarpEvent = false,
            MapInstanceType mapInstanceType = MapInstanceType.NoChange,
            bool fromLogin = false,
            bool forceInstanceChange = false,
            Guid? dungeonId = null,
            int instanceLives = 3
        )
        {
        }

        public bool StatusActive(StatusTypes status)
        {
            foreach (var cachedStatus in CachedStatuses)
            {
                if (cachedStatus.Type == status)
                {
                    return true;
                }
            }
            return false;
        }

        public virtual EntityPacket EntityPacket(EntityPacket packet = null, Player forPlayer = null)
        {
            if (packet == null)
            {
                throw new Exception("No packet to populate!");
            }

            packet.EntityId = Id;
            packet.MapId = MapId;
            packet.Name = Name;
            packet.Sprite = Sprite;
            packet.Color = Color;
            packet.Face = Face;
            packet.Level = Level;
            packet.X = (byte) X;
            packet.Y = (byte) Y;
            packet.Z = (byte) Z;
            packet.Dir = (byte) Dir;
            packet.Passable = Passable;
            packet.HideName = HideName;
            packet.HideEntity = HideEntity;
            packet.Animations = Animations.ToArray();
            packet.Vital = GetVitals();
            packet.MaxVital = GetMaxVitals();
            packet.Stats = GetStatValues();
            packet.StatusEffects = StatusPackets();
            packet.NameColor = NameColor;
            packet.HeaderLabel = new LabelPacket(HeaderLabel.Text, HeaderLabel.Color);
            packet.FooterLabel = new LabelPacket(FooterLabel.Text, FooterLabel.Color);

            return packet;
        }

        public StatusPacket[] StatusPackets()
        {
            var statuses = CachedStatuses;
            var statusPackets = new StatusPacket[statuses.Length];
            for (var i = 0; i < statuses.Length; i++)
            {
                var status = statuses[i];
                int[] vitalShields = null;
                if (status.Type == StatusTypes.Shield)
                {
                    vitalShields = new int[(int) Vitals.VitalCount];
                    for (var x = 0; x < (int) Vitals.VitalCount; x++)
                    {
                        vitalShields[x] = status.shield[x];
                    }
                }

                statusPackets[i] = new StatusPacket(
                    status.Spell.Id, status.Type, status.Data, (int) (status.Duration - Timing.Global.Milliseconds),
                    (int) (status.Duration - status.StartTime), vitalShields
                );
            }

            return statusPackets;
        }

        public Immunities StatusToImmunity(StatusTypes status)
        {
            switch (status)
            {
                case StatusTypes.Stun:
                    return Immunities.Stun;
                case StatusTypes.Silence:
                    return Immunities.Silence;
                case StatusTypes.Sleep:
                    return Immunities.Sleep;
                case StatusTypes.Blind:
                    return Immunities.Blind;
                case StatusTypes.Snare:
                    return Immunities.Snare;
                case StatusTypes.Taunt:
                    return Immunities.Taunt;
                case StatusTypes.Transform:
                    return Immunities.Transform;
                case StatusTypes.Confused:
                    return Immunities.Confused;
                case StatusTypes.Slowed:
                    return Immunities.Slowed;
                default:
                    return Immunities.None;
            }
        }

        public bool IsImmuneTo(Immunities effect)
        {
            if (effect == Immunities.None)
            {
                return false;
            } else
            {
                return ImmuneTo.TryGetValue(effect, out var value) ? value : false;
            }
        }

        #region Spell Cooldowns

        [JsonIgnore, Column("SpellCooldowns")]
        public string SpellCooldownsJson
        {
            get => JsonConvert.SerializeObject(SpellCooldowns);
            set => SpellCooldowns = JsonConvert.DeserializeObject<ConcurrentDictionary<Guid, long>>(value ?? "{}");
        }

        [NotMapped] public ConcurrentDictionary<Guid, long> SpellCooldowns = new ConcurrentDictionary<Guid, long>();

        #endregion

    }

    public partial class Entity : IDisposable
    {
        public int GetCurrentShield()
        {
            var shield = 0;
            foreach (var status in CachedStatuses)
            {
                if (status.Type == StatusTypes.Shield)
                {
                    shield += status.shield[(int)Vitals.Health];
                }
            }

            return shield;
        }

        public List<Entity> GetEntitiesOnTile(int tileX, int tileY)
        {
            var tileEntities = new List<Entity>();

            if (Map != null && Map.TryGetInstance(MapInstanceId, out var mapInstance))
            {
                foreach (var en in mapInstance.GetCachedEntities())
                {
                    if (en.Id == Id) continue;
                    if (en != null && en.X == tileX && en.Y == tileY && en.Z == Z && !en.Passable)
                    {
                        tileEntities.Add(en);
                    }
                }
            }
            
            return tileEntities;
        }
    }

    public partial class Entity : IDisposable
    {
        [NotMapped, JsonIgnore]
        public long DashTransmissionTimer { get; set; }

        public virtual bool IsNonTrivialTo(Player player)
        {
            return false;
        }

        [NotMapped, JsonIgnore]
        public virtual int TierLevel => -1;

        public void FullHeal()
        {
            SetVital((int)Vitals.Health, GetMaxVital((int)Vitals.Health));
            SetVital((int)Vitals.Mana, GetMaxVital((int)Vitals.Mana));
        }

        public void ClearHostileDoTs()
        {
            foreach (var dot in CachedDots)
            {
                if (!dot.SpellBase.Combat.Friendly)
                {
                    dot.Expire();
                }
            }

            foreach (var status in CachedStatuses)
            {
                if (!StatusHelpers.TenacityExcluded.Contains(status.Type))
                {
                    status.RemoveStatus();
                }
            }

            PacketSender.SendMapEntityStatusUpdate(Map, this, MapInstanceId);
        }
    }

    public partial class Entity : IDisposable
    {
        [NotMapped]
        public Dictionary<Guid, long> RecentAllies { get; set; } = new Dictionary<Guid, long>();

        [NotMapped]
        public long NextAllyPruneTimestamp { get; set; } = 0L;

        /// <summary>
        /// Adds an entity as an ally of this entity. Allies are pruned after a configurable amount of seconds since last heal
        /// </summary>
        /// <param name="en">The entity to add as ally</param>
        public virtual void AddRecentAlly(AttackingEntity en)
        {
            // Blank
        }

        /// <summary>
        /// Keeps the ally list up to date
        /// </summary>
        public virtual void PruneAllies()
        {
            // Blank
        }

        public virtual void ResetCooldowns()
        {
            SpellCooldowns.Clear();
        }

        public virtual bool IsInvincibleTo(Entity entity)
        {
            return false;
        }

        [NotMapped, JsonIgnore]
        public RegenType MapRegenType => Map?.RegenType ?? RegenType.Normal;

        /// <summary>
        /// Returns a vital regen rate for a given vital based on the entity's definitions
        /// </summary>
        /// <param name="vital">The <see cref="Vitals"/>, typecasted to int</param>
        /// <returns>The regen rate, as a decimal</returns>
        public virtual float GetVitalRegenRate(int vital) 
        {
            return 0f;
        }
    }
}
