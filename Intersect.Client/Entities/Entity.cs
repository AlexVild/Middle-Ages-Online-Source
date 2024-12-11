using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;

using Intersect.Client.Core;
using Intersect.Client.Entities.CombatNumbers;
using Intersect.Client.Entities.Events;
using Intersect.Client.Entities.Projectiles;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.General;
using Intersect.Client.General.Bestiary;
using Intersect.Client.Interface.Game.Chat;
using Intersect.Client.Items;
using Intersect.Client.Localization;
using Intersect.Client.Maps;
using Intersect.Client.Spells;
using Intersect.Configuration;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using Intersect.GameObjects.Maps;
using Intersect.Logging;
using Intersect.Network.Packets.Server;
using Intersect.Utilities;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using static Intersect.Client.Framework.File_Management.GameContentManager;

namespace Intersect.Client.Entities
{

    public partial class Entity
    {

        public enum LabelType
        {

            Header = 0,

            Footer,

            Name,

            ChatBubble,

            Guild

        }

        public int AnimationFrame;

        //Entity Animations
        public List<Animation> Animations = new List<Animation>();

        //Animation Timer (for animated sprites)
        public long AnimationTimer;

        //Combat
        public long AttackTimer { get; set; } = 0;
        public int AttackTime { get; set; } = -1;

        public bool Blocking = false;

        //Combat Status
        public long CastTime = 0;

        public bool IsCasting => CastTime != 0;

        //Dashing instance
        public Dash Dashing;

        public Queue<Dash> DashQueue = new Queue<Dash>();

        public long DashTimer;

        public float elapsedtime; //to be removed

        public Guid[] Equipment = new Guid[Options.EquipmentSlots.Count];

        public Animation[] EquipmentAnimations = new Animation[Options.EquipmentSlots.Count];

        //Extras
        public string Face = "";

        public Label FooterLabel;

        public Gender Gender = Gender.Male;

        public Label HeaderLabel;

        public bool HideEntity = false;

        public bool HideName;

        //Core Values
        public Guid Id;

        //Inventory/Spells/Equipment
        public Item[] Inventory = new Item[Options.MaxInvItems];

        public bool InView = true;

        public bool IsLocal = false;

        public bool IsMoving;

        //Caching
        public MapInstance LatestMap;

        public int Level = 1;

        //Vitals & Stats
        public int[] MaxVital = new int[(int) Vitals.VitalCount];

        protected Pointf mCenterPos = Pointf.Empty;

        //Chat
        private List<ChatBubble> mChatBubbles = new List<ChatBubble>();

        private byte mDir;

        protected bool mDisposed;

        private long mLastUpdate;

        protected string mMySprite = "";

        public Color Color = new Color(255,255,255,255);

        public int MoveDir = -1;

        public long MoveTimer;

        protected byte mRenderPriority = 1;

        protected string mTransformedSprite = "";

        private long mWalkTimer;

        public int[] MyEquipment = new int[Options.EquipmentSlots.Count];

        public string Name = "";

        public Color NameColor = null;

        public float OffsetX;

        public float OffsetY;

        public bool Passable;

        //Rendering Variables
        public HashSet<Entity> RenderList;

        public Guid SpellCast;

        public Spell[] Spells = new Spell[Options.MaxPlayerSkills];

        public int[] Stat = new int[(int) Stats.StatCount];

        public int[] TrueStats = new int[(int)Stats.StatCount];
        
        public List<Guid> ActivePassives = new List<Guid>();

        public int Target = -1;

        public Guid EntityTarget = Guid.Empty;

        public GameTexture Texture;

        #region "Animation Textures and Timing"
        public SpriteAnimations SpriteAnimation = SpriteAnimations.Normal;

        public Dictionary<SpriteAnimations,GameTexture> AnimatedTextures = new Dictionary<SpriteAnimations, GameTexture>();

        public int SpriteFrame = 0;

        public long SpriteFrameTimer = -1;

        public long LastActionTime = -1;
        #endregion

        public int Type;

        public int[] Vital = new int[(int) Vitals.VitalCount];

        public int WalkFrame;

        public FloatRect WorldPos = new FloatRect();

        public bool IsTargeted = false;

        //Location Info
        public byte X;

        public byte Y;

        public byte Z;

        public string[] MyDecors = new string[Options.DecorSlots.Count];

        public bool Flash = false; // Whether or not the entity sprite is flashing

        public Color FlashColor = null; // What color to flash the entity

        public long FlashEndTime = 0L;

        public Entity(Guid id, EntityPacket packet, bool isEvent = false)
        {
            Id = id;
            CurrentMap = Guid.Empty;
            if (id != Guid.Empty && !isEvent)
            {
                for (var i = 0; i < Options.MaxInvItems; i++)
                {
                    Inventory[i] = new Item();
                }

                for (var i = 0; i < Options.MaxPlayerSkills; i++)
                {
                    Spells[i] = new Spell();
                }

                for (var i = 0; i < Options.EquipmentSlots.Count; i++)
                {
                    Equipment[i] = Guid.Empty;
                    MyEquipment[i] = -1;
                }

                for (var i = 0; i < Options.DecorSlots.Count; i++)
                {
                    MyDecors[i] = null;
                }
            }

            AnimationTimer = Timing.Global.Milliseconds + Globals.Random.Next(0, 500);

            //TODO Remove because fixed orrrrr change the exception text
            if (Options.EquipmentSlots.Count == 0)
            {
                throw new Exception("What the fuck is going on!?!?!?!?!?!");
            }

            Load(packet);

            var inProximity = InNameProximity();
            //NameOpacity = inProximity ? byte.MaxValue : ClientConfiguration.Instance.MinimumNameOpacity;
            NameOpacity = inProximity ? byte.MaxValue : 0;
            FadeName = !inProximity;
        }

        //Status effects
        public List<Status> Status { get; private set; } = new List<Status>();

        public byte Dir
        {
            get => mDir;
            set => mDir = (byte) ((value + 4) % 4);
        }

        public virtual string TransformedSprite
        {
            get => mTransformedSprite;
            set
            {
                if (mTransformedSprite != value)
                {
                    mTransformedSprite = value;
                    if (value == "")
                    {
                        Texture = Globals.ContentManager.GetTexture(GameContentManager.TextureType.Entity, mMySprite);
                        LoadAnimationTextures(mMySprite);
                    }
                    else
                    {
                        Texture = Globals.ContentManager.GetTexture(GameContentManager.TextureType.Entity, mTransformedSprite);
                        LoadAnimationTextures(mTransformedSprite);
                    }
                }
            }
        }

        public virtual string MySprite
        {
            get => mMySprite;
            set
            {
                if (mMySprite != value)
                {
                    mMySprite = value;
                    Texture = Globals.ContentManager.GetTexture(GameContentManager.TextureType.Entity, mMySprite);
                    LoadAnimationTextures(mMySprite);
                }
            }
        }

        public virtual int SpriteFrames
        {
            get
            {
                switch (SpriteAnimation)
                {
                    case SpriteAnimations.Normal:
                        return Options.Instance.Sprites.NormalFrames;
                    case SpriteAnimations.Idle:
                        return Options.Instance.Sprites.IdleFrames;
                    case SpriteAnimations.Attack:
                        return Options.Instance.Sprites.AttackFrames;
                    case SpriteAnimations.Shoot:
                        return Options.Instance.Sprites.ShootFrames;
                    case SpriteAnimations.Cast:
                        return Options.Instance.Sprites.CastFrames;
                    case SpriteAnimations.Weapon:
                        return Options.Instance.Sprites.WeaponFrames;
                }

                return Options.Instance.Sprites.NormalFrames;

            }
        }

        public MapInstance MapInstance => MapInstance.Get(CurrentMap);

        public virtual Guid CurrentMap { get; set; }

        public virtual EntityTypes GetEntityType()
        {
            return EntityTypes.GlobalEntity;
        }

        //Deserializing
        public virtual void Load(EntityPacket packet)
        {
            if (packet == null)
            {
                return;
            }

            CurrentMap = packet.MapId;
            Name = packet.Name;
            MySprite = packet.Sprite;
            Color = packet.Color;
            Face = packet.Face;
            Level = packet.Level;
            X = packet.X;
            Y = packet.Y;
            Z = packet.Z;
            Dir = packet.Dir;
            Passable = packet.Passable;
            HideName = packet.HideName;
            HideEntity = packet.HideEntity;
            NameColor = packet.NameColor;
            HeaderLabel = new Label(packet.HeaderLabel.Label, packet.HeaderLabel.Color);
            FooterLabel = new Label(packet.FooterLabel.Label, packet.FooterLabel.Color);

            var animsToClear = new List<Animation>();
            var animsToAdd = new List<AnimationBase>();
            for (var i = 0; i < packet.Animations.Length; i++)
            {
                var anim = AnimationBase.Get(packet.Animations[i]);
                if (anim != null)
                {
                    animsToAdd.Add(anim);
                }
            }

            foreach (var anim in Animations)
            {
                animsToClear.Add(anim);
                if (!anim.InfiniteLoop)
                {
                    animsToClear.Remove(anim);
                }
                else
                {
                    foreach (var addedAnim in animsToAdd)
                    {
                        if (addedAnim.Id == anim.MyBase.Id)
                        {
                            animsToClear.Remove(anim);
                            animsToAdd.Remove(addedAnim);

                            break;
                        }
                    }

                    foreach (var equipAnim in EquipmentAnimations)
                    {
                        if (equipAnim == anim)
                        {
                            animsToClear.Remove(anim);
                        }
                    }
                }
            }

            ClearAnimations(animsToClear);
            AddAnimations(animsToAdd);

            Vital = packet.Vital;
            MaxVital = packet.MaxVital;

            //Update status effects
            Status.Clear();

            if (packet.StatusEffects == null)
            {
                Log.Warn($"'{nameof(packet)}.{nameof(packet.StatusEffects)}' is null.");
            }
            else
            {
                foreach (var status in packet.StatusEffects)
                {
                    var instance = new Status(
                        status.SpellId, status.Type, status.TransformSprite, status.TimeRemaining, status.TotalDuration
                    );

                    Status?.Add(instance);

                    if (instance.Type == StatusTypes.Shield)
                    {
                        instance.Shield = status.VitalShields;
                    }
                }
            }

            SortStatuses();
            Stat = packet.Stats;

            mDisposed = false;

            //Status effects box update
            if (Globals.Me == null)
            {
                Log.Warn($"'{nameof(Globals.Me)}' is null.");
            }
            else
            {
                if (Id == Globals.Me.Id)
                {
                    if (Interface.Interface.GameUi == null)
                    {
                        Log.Warn($"'{nameof(Interface.Interface.GameUi)}' is null.");
                    }
                }
                else if (Id != Guid.Empty && Id == Globals.Me.TargetIndex)
                {
                    if (Globals.Me.TargetBox == null)
                    {
                        Log.Warn($"'{nameof(Globals.Me.TargetBox)}' is null.");
                    }
                    else
                    {
                        Globals.Me.TargetBox.UpdateStatuses = true;
                    }
                }
            }
        }

        public void AddAnimations(List<AnimationBase> anims)
        {
            if (this is Player && Globals.Me?.MapInstance?.ZoneType != MapZones.Safe && !InPvpSight)
            {
                return;
            }

            foreach (var anim in anims)
            {
                Animations.Add(new Animation(anim, true, false, -1, this));
            }
        }

        public void ClearAnimations(List<Animation> anims)
        {
            if (anims == null)
            {
                anims = Animations;
            }

            if (anims.Count > 0)
            {
                for (var i = 0; i < anims.Count; i++)
                {
                    anims[i].Dispose();
                    Animations.Remove(anims[i]);
                }
            }
        }

        public virtual bool IsDisposed()
        {
            return mDisposed;
        }

        public virtual void Dispose()
        {
            if (RenderList != null)
            {
                RenderList.Remove(this);
            }

            ClearAnimations(null);
            mDisposed = true;
        }

        public virtual float StrafeBonus => 0.0f;
        public virtual float BackstepBonus => 0.0f;

        public virtual bool GetCombatMode()
        {
            return false;
        }

        public virtual int GetFaceDirection()
        {
            return Dir;
        }

        //Returns the amount of time required to traverse 1 tile
        public virtual float GetMovementTime(int fromSpeed = -1)
        {
            return MovementUtilities.GetMovementTime(fromSpeed > 0 ? fromSpeed : Speed,
                GetCombatMode(),
                Dir,
                GetFaceDirection(),
                StatusActive(StatusTypes.Haste),
                StatusActive(StatusTypes.Slowed),
                BackstepBonus,
                StrafeBonus);
        }

        public virtual int Speed => Stat[(int)Stats.Speed];

        //Movement Processing
        public virtual bool Update()
        {
            MapInstance map = null;
            if (mDisposed)
            {
                LatestMap = null;

                return false;
            }
            else
            {
                map = MapInstance.Get(CurrentMap);
                LatestMap = map;
                if (map == null || !map.InView())
                {
                    Globals.EntitiesToDispose.Add(Id);

                    return false;
                }
            }

            if (!Graphics.Viewport.IntersectsWith(GetRenderBounds()) && Id != Globals.Me?.Id)
            {
                RenderList?.Remove(this);
                return false;
            }
            RenderList = DetermineRenderOrder(RenderList, map);

            if (mLastUpdate == 0)
            {
                mLastUpdate = Timing.Global.Milliseconds;
            }

            var ecTime = (float)(Timing.Global.Milliseconds - mLastUpdate);
            elapsedtime = ecTime;

            // Update flash timer
            if (Flash && Timing.Global.Milliseconds > FlashEndTime)
            {
                Flash = false;
            }

            if (Dashing != null)
            {
                WalkFrame = Options.Instance.Sprites.NormalSheetDashFrame; //Fix the frame whilst dashing
            }
            else if (mWalkTimer < Timing.Global.Milliseconds)
            {
                if (!IsMoving && DashQueue.Count > 0)
                {
                    Dashing = DashQueue.Dequeue();
                    Dashing.Start(this);
                    OffsetX = 0;
                    OffsetY = 0;
                    DashTimer = Timing.Global.Milliseconds + Options.MaxDashSpeed;
                }
                else
                {
                    if (IsMoving)
                    {
                        WalkFrame++;
                        if (WalkFrame >= SpriteFrames)
                        {
                            WalkFrame = 0;
                        }
                    }
                    else
                    {
                        if (WalkFrame > 0 && WalkFrame / SpriteFrames < 0.7f)
                        {
                            WalkFrame = (int)SpriteFrames / 2;
                        }
                        else
                        {
                            WalkFrame = 0;
                        }
                    }

                    var speedRatio = GetMovementTime() / GetMovementTime(1);
                    mWalkTimer = Timing.Global.Milliseconds + (int)(155 * speedRatio); // 190 hardcoded replacement for Options.Instance.Sprites.MovingFrameDuration because I'm lazy
                }
            }

            if (Dashing != null)
            {
                if (Dashing.Update(this))
                {
                    OffsetX = Dashing.GetXOffset();
                    OffsetY = Dashing.GetYOffset();
                }
                else
                {
                    OffsetX = 0;
                    OffsetY = 0;
                }
            }
            else if (IsMoving)
            {
                switch (Dir)
                {
                    case 0:
                        OffsetY -= (float) ecTime * (float) Options.TileHeight / GetMovementTime();
                        OffsetX = 0;
                        if (OffsetY < 0)
                        {
                            OffsetY = 0;
                        }

                        break;

                    case 1:
                        OffsetY += (float) ecTime * (float) Options.TileHeight / GetMovementTime();
                        OffsetX = 0;
                        if (OffsetY > 0)
                        {
                            OffsetY = 0;
                        }

                        break;

                    case 2:
                        OffsetX -= (float) ecTime * (float) Options.TileHeight / GetMovementTime();
                        OffsetY = 0;
                        if (OffsetX < 0)
                        {
                            OffsetX = 0;
                        }

                        break;

                    case 3:
                        OffsetX += (float) ecTime * (float) Options.TileHeight / GetMovementTime();
                        OffsetY = 0;
                        if (OffsetX > 0)
                        {
                            OffsetX = 0;
                        }

                        break;
                }

                if (OffsetX == 0 && OffsetY == 0)
                {
                    IsMoving = false;
                }
            }

            //Check to see if we should start or stop equipment animations
            if (Equipment.Length == Options.EquipmentSlots.Count)
            {
                for (var z = 0; z < Options.EquipmentSlots.Count; z++)
                {
                    if (Equipment[z] != Guid.Empty && (this != Globals.Me || MyEquipment[z] < Options.MaxInvItems))
                    {
                        var itemId = Guid.Empty;
                        if (this == Globals.Me)
                        {
                            var slot = MyEquipment[z];
                            if (slot > -1)
                            {
                                itemId = Inventory[slot].ItemId;
                            }
                        }
                        else
                        {
                            itemId = Equipment[z];
                        }

                        var itm = ItemBase.Get(itemId);
                        AnimationBase anim = null;
                        if (itm != null)
                        {
                            anim = itm.EquipmentAnimation;
                        }

                        if (anim != null)
                        {
                            var inVehicle = this is Player player && player.InVehicle;
                            if (EquipmentAnimations[z] != null &&
                                (EquipmentAnimations[z].MyBase != anim || EquipmentAnimations[z].Disposed() || inVehicle))
                            {
                                EquipmentAnimations[z].Dispose();
                                Animations.Remove(EquipmentAnimations[z]);
                                EquipmentAnimations[z] = null;
                            }

                            if (EquipmentAnimations[z] == null && !inVehicle)
                            {
                                EquipmentAnimations[z] = new Animation(anim, true, true, -1, this);
                                Animations.Add(EquipmentAnimations[z]);
                            }
                        }
                        else
                        {
                            if (EquipmentAnimations[z] != null)
                            {
                                EquipmentAnimations[z].Dispose();
                                Animations.Remove(EquipmentAnimations[z]);
                                EquipmentAnimations[z] = null;
                            }
                        }
                    }
                    else
                    {
                        if (EquipmentAnimations[z] != null)
                        {
                            EquipmentAnimations[z].Dispose();
                            Animations.Remove(EquipmentAnimations[z]);
                            EquipmentAnimations[z] = null;
                        }
                    }
                }
            }

            var chatbubbles = mChatBubbles.ToArray();
            foreach (var chatbubble in chatbubbles)
            {
                if (!chatbubble.Update())
                {
                    mChatBubbles.Remove(chatbubble);
                }
            }

            if (AnimationTimer < Timing.Global.Milliseconds)
            {
                AnimationTimer = Timing.Global.Milliseconds + 200;
                AnimationFrame++;
                if (AnimationFrame >= SpriteFrames)
                {
                    AnimationFrame = 0;
                }
            }

            CalculateCenterPos();

            List<Animation> animsToRemove = null;
            foreach (var animInstance in Animations)
            {
                animInstance.Update();

                //If disposed mark to be removed and continue onward
                if (animInstance.Disposed())
                {
                    if (animsToRemove == null)
                    {
                        animsToRemove = new List<Animation>();
                    }

                    animsToRemove.Add(animInstance);

                    continue;
                }

                if (IsStealthed() || HideEntity)
                {
                    animInstance.Hide();
                }
                else
                {
                    animInstance.Show();
                }

                if (animInstance.AutoRotate)
                {
                    if (this is Player pl && pl.CombatMode)
                    {
                        animInstance.SetPosition(
                            (int)Math.Ceiling(GetCenterPos().X), (int)Math.Ceiling(GetCenterPos().Y), X, Y, CurrentMap,
                            pl.FaceDirection, Z
                        );
                    }
                    else
                    {
                        animInstance.SetPosition(
                            (int)Math.Ceiling(GetCenterPos().X), (int)Math.Ceiling(GetCenterPos().Y), X, Y, CurrentMap,
                            Dir, Z
                        );
                    }
                }
                else
                {
                    animInstance.SetPosition(
                        (int) Math.Ceiling(GetCenterPos().X), (int) Math.Ceiling(GetCenterPos().Y), X, Y, CurrentMap,
                        -1, Z
                    );
                }
            }

            if (animsToRemove != null)
            {
                foreach (var anim in animsToRemove)
                {
                    Animations.Remove(anim);
                }
            }

            mLastUpdate = Timing.Global.Milliseconds;

            UpdateSpriteAnimation();

            return true;
        }

        public virtual int CalculateAttackTime()
        {
            //If this is an npc we don't know it's attack time. Luckily the server provided it!
            if (this != Globals.Me && AttackTime > -1)
            {
                return AttackTime;
            }

            //Otherwise return the legacy attack speed calculation
            return (int) (Options.MaxAttackRate +
                          (float) ((Options.MinAttackRate - Options.MaxAttackRate) *
                                   (((float) Options.MaxStatValue - Stat[(int) Stats.Speed]) /
                                    (float) Options.MaxStatValue)));
        }

        public virtual bool IsStealthed()
        {
            //If the entity has transformed, apply that sprite instead.
            if (this == Globals.Me)
            {
                return false;
            }

            for (var n = 0; n < Status.Count; n++)
            {
                if (Status[n].Type == StatusTypes.Stealth)
                {
                    return true;
                }
            }

            return false;
        }

        public virtual HashSet<Entity> DetermineRenderOrder(HashSet<Entity> renderList, MapInstance map)
        {
            if (renderList != null)
            {
                renderList.Remove(this);
            }

            if (map == null || Globals.Me == null || Globals.Me.MapInstance == null)
            {
                return null;
            }

            var gridX = Globals.Me.MapInstance.MapGridX;
            var gridY = Globals.Me.MapInstance.MapGridY;
            for (var x = gridX - 1; x <= gridX + 1; x++)
            {
                for (var y = gridY - 1; y <= gridY + 1; y++)
                {
                    if (!Graphics.MapAtCoord(x, y) || Globals.MapGrid[x, y] != CurrentMap)
                    {
                        continue;
                    }

                    var priority = mRenderPriority;
                    if (Z != 0)
                    {
                        priority += 3;
                    }

                    HashSet<Entity> renderSet;

                    if (y == gridY - 1)
                    {
                        renderSet = Graphics.RenderingEntities[priority, Options.MapHeight + Y];
                    }
                    else if (y == gridY)
                    {
                        renderSet = Graphics.RenderingEntities[priority, Options.MapHeight * 2 + Y];
                    }
                    else
                    {
                        renderSet = Graphics.RenderingEntities[priority, Options.MapHeight * 3 + Y];
                    }

                    renderSet.Add(this);
                    renderList = renderSet;

                    return renderList;
                }
            }

            return renderList;
        }

        public virtual int DetermineRenderDirection(byte dir)
        {
            switch (dir)
            {
                case 0:
                    return 3;

                    break;
                case 1:
                    return 0;

                    break;
                case 2:
                    return 1;

                    break;
                case 3:
                    return 2;

                    break;
                default:
                    return 3;
            }
        }

        /// <summary>
        /// Lazily makes a best guess at a entity's render bounds. Can be improved upon
        /// by getting sprite information, but for now just drawing a bigger box than a tile seems
        /// to do the trick.
        /// </summary>
        /// <returns>A float rect representing a entity's rendering position, with some give</returns>
        public virtual FloatRect GetRenderBounds()
        {
            var map = MapInstance.Get(CurrentMap);
            if (map == null || !Globals.GridMaps.Contains(CurrentMap))
            {
                return new FloatRect(0, 0, 0, 0);
            }

            return new FloatRect(
                map.GetX() + X * Options.TileWidth + OffsetX - (Options.TileWidth * 2),
                map.GetY() + Y * Options.TileHeight + OffsetY - (Options.TileHeight * 2),
                Options.TileWidth * 4,
                Options.TileHeight * 4);
        }

        //Rendering Functions
        public virtual void Draw()
        {
            if (HideEntity || IsDead)
            {
                return; //Don't draw if the entity is hidden
            }

            // Player outside of PvP LOS?
            if (Globals.Me != null && Globals.Me.MapInstance.ZoneType != MapZones.Safe && this is Player && !InPvpSight)
            {
                return;
            }

            WorldPos.Reset();
            var map = MapInstance.Get(CurrentMap);
            if (map == null || !Globals.GridMaps.Contains(CurrentMap))
            {
                return;
            }

            var srcRectangle = new FloatRect();
            var destRectangle = new FloatRect();
            var dir = 0;

            var sprite = "";
            // Copy the actual render color, because we'll be editing it later and don't want to overwrite it.
            var renderColor = new Color(Color.A, Color.R, Color.G, Color. B);
            if (Flash && FlashColor != null) // Flash the sprite some color for some duration
            {
                renderColor = FlashColor;
            }

            string transformedSprite = "";

            //If the entity has transformed, apply that sprite instead.
            for (var n = 0; n < Status.Count; n++)
            {
                if (Status[n].Type == StatusTypes.Transform)
                {
                    sprite = Status[n].Data;
                    transformedSprite = sprite;
                }

                //If unit is stealthed, don't render unless the entity is the player.
                if (Status[n].Type == StatusTypes.Stealth)
                {
                    if (this != Globals.Me && !IsAllyOf(Globals.Me))
                    {
                        return;
                    }
                    else
                    {
                        renderColor.A /= 2;
                    }
                }
            }

            if (this is Player p && p.InVehicle)
            {
                sprite = p.VehicleSprite;
                transformedSprite = sprite;
            }

            if (transformedSprite != TransformedSprite)
            {
                TransformedSprite = transformedSprite;
            }

            //Check if there is no transformed sprite set
            if (string.IsNullOrEmpty(sprite))
            {
                sprite = MySprite;
                MySprite = sprite;
            }

            AnimatedTextures.TryGetValue(SpriteAnimation, out var texture);
            texture = texture ?? Texture;

            if (texture == null)
            {
                // We don't have a texture to render, but we still want this to be targetable.
                WorldPos = new FloatRect(
                    map.GetX() + X * Options.TileWidth + OffsetX,
                    map.GetY() + Y * Options.TileHeight + OffsetY,
                    Options.TileWidth,
                    Options.TileHeight);

                return;
            }

            if (texture.ScaledHeight / Options.Instance.Sprites.Directions > Options.TileHeight)
            {
                destRectangle.X = map.GetX() + X * Options.TileWidth + OffsetX + Options.TileWidth / 2;
                destRectangle.Y = GetCenterPos().Y - texture.ScaledHeight / (Options.Instance.Sprites.Directions * 2);
            }
            else
            {
                destRectangle.X = map.GetX() + X * Options.TileWidth + OffsetX + Options.TileWidth / 2;
                destRectangle.Y = map.GetY() + Y * Options.TileHeight + OffsetY;
            }

            destRectangle.X -= texture.ScaledWidth / (SpriteFrames * 2);

            var paperdollDir = Dir;
            if (this is Player player && player.CombatMode)
            {
                dir = DetermineRenderDirection(player.FaceDirection);
                paperdollDir = player.FaceDirection;
            }
            else
            {
                dir = DetermineRenderDirection(Dir);
            }

            destRectangle.X = (int)Math.Ceiling(destRectangle.X);
            destRectangle.Y = (int)Math.Ceiling(destRectangle.Y);
            if (Options.AnimatedSprites.Contains(sprite.ToLower()) || (NpcBase.TryGet(NpcId, out var npc) && npc.AnimatedSprite))
            {
                srcRectangle = new FloatRect(
                    AnimationFrame * (int)texture.GetWidth() / SpriteFrames, dir * (int)texture.GetHeight() / Options.Instance.Sprites.Directions,
                    (int)texture.GetWidth() / SpriteFrames, (int)texture.GetHeight() / Options.Instance.Sprites.Directions
                );
            }
            else
            {
                if (SpriteAnimation == SpriteAnimations.Normal)
                {
                    bool inAction = AttackTimer - CalculateAttackTime() / 2 > Timing.Global.Ticks / TimeSpan.TicksPerMillisecond || Blocking;
                    if (inAction && !(this is Player play && play.InVehicle) && !StatusActive(StatusTypes.Stun) && !StatusActive(StatusTypes.Sleep))
                    {
                        srcRectangle = new FloatRect(
                            Options.Instance.Sprites.NormalSheetAttackFrame * (int)texture.GetWidth() / SpriteFrames, dir * (int)texture.GetHeight() / Options.Instance.Sprites.Directions,
                            (int)texture.GetWidth() / SpriteFrames, (int)texture.GetHeight() / Options.Instance.Sprites.Directions
                        );
                    }
                    else
                    {
                        //Restore Original Attacking/Blocking Code
                        srcRectangle = new FloatRect(
                            WalkFrame * (int)texture.GetWidth() / SpriteFrames, dir * (int)texture.GetHeight() / Options.Instance.Sprites.Directions,
                            (int)texture.GetWidth() / SpriteFrames, (int)texture.GetHeight() / Options.Instance.Sprites.Directions
                        );
                    }
                }
                else
                {
                    srcRectangle = new FloatRect(
                        SpriteFrame * (int)texture.GetWidth() / SpriteFrames, dir * (int)texture.GetHeight() / Options.Instance.Sprites.Directions,
                        (int)texture.GetWidth() / SpriteFrames, (int)texture.GetHeight() / Options.Instance.Sprites.Directions
                    );
                }
            }

            destRectangle.Width = srcRectangle.Width * Options.Scale;
            destRectangle.Height = srcRectangle.Height * Options.Scale;

            WorldPos = destRectangle;

            //Order the layers of paperdolls and sprites
            for (var z = 0; z < Options.PaperdollOrder[paperdollDir].Count; z++)
            {
                var paperdoll = Options.PaperdollOrder[paperdollDir][z];
                var equipSlot = Options.EquipmentSlots.IndexOf(paperdoll);
                var decorSlot = Options.DecorSlots.IndexOf(paperdoll);

                //Don't render the paperdolls if they have transformed.
                var notTransformed = sprite == MySprite && Equipment.Length == Options.EquipmentSlots.Count;

                if (this is Player pl && pl.InVehicle)
                {
                    notTransformed = false;
                }

                //Check for player
                if (paperdoll == "Player")
                {
                    Graphics.DrawGameTexture(
                        texture, srcRectangle, destRectangle, renderColor
                    );
                }
                else if (notTransformed)
                {
                    if (equipSlot > -1)
                    {
                        if (Equipment[equipSlot] != Guid.Empty && this != Globals.Me ||
                            MyEquipment[equipSlot] < Options.MaxInvItems)
                        {
                            var itemId = Guid.Empty;
                            if (this == Globals.Me)
                            {
                                var slot = MyEquipment[equipSlot];
                                if (slot > -1)
                                {
                                    itemId = Inventory[slot].ItemId;
                                }
                            }
                            else
                            {
                                itemId = Equipment[equipSlot];
                            }

                            // Cosmetic override
                            if (this is Player cosmeticOverride && cosmeticOverride.Cosmetics.ElementAtOrDefault(equipSlot) != default)
                            {
                                itemId = cosmeticOverride.Cosmetics[equipSlot];
                            }

                            var item = ItemBase.Get(itemId);
                            if (item != null)
                            {
                                if (Gender == 0)
                                {
                                    DrawEquipment(item.MalePaperdoll, renderColor.A, dir);
                                }
                                else
                                {
                                    DrawEquipment(item.FemalePaperdoll, renderColor.A, dir);
                                }
                            }
                        }
                    }
                    else if (this is Player cosmetic && cosmetic.Cosmetics.ElementAtOrDefault(equipSlot) != default)
                    {
                        var item = ItemBase.Get(cosmetic.Cosmetics[equipSlot]);
                        if (item != null)
                        {
                            if (Gender == 0)
                            {
                                DrawEquipment(item.MalePaperdoll, renderColor.A, dir);
                            }
                            else
                            {
                                DrawEquipment(item.FemalePaperdoll, renderColor.A, dir);
                            }
                        }
                    }
                    else if (decorSlot > -1 && decorSlot < Options.Player.DecorSlots.Count)
                    {
                        var hideHair = false;
                        var hideBeard = false;
                        var hideExtra = false;
                        var shortHair = false;

                        if (this is Player cosmeticOvr && cosmeticOvr.Cosmetics[Options.HelmetIndex] != Guid.Empty)
                        {
                            var helmet = ItemBase.Get(cosmeticOvr.Cosmetics[Options.HelmetIndex]);
                            hideHair = helmet.HideHair;
                            hideBeard = helmet.HideBeard;
                            hideExtra = helmet.HideExtra;
                            shortHair = helmet.ShortHair;

                            if ((decorSlot == Options.HairSlot && hideHair && !shortHair)
                                || decorSlot == Options.BeardSlot && hideBeard
                                || decorSlot == Options.ExtraSlot && hideExtra)
                            {
                                continue;
                            }
                        }
                        else if (Equipment[Options.HelmetIndex] != Guid.Empty)
                        {
                            var helmet = ItemBase.Get(Equipment[Options.HelmetIndex]);
                            hideHair = helmet.HideHair;
                            hideBeard = helmet.HideBeard;
                            hideExtra = helmet.HideExtra;
                            shortHair = helmet.ShortHair;

                            if ((decorSlot == Options.HairSlot && hideHair && !shortHair)
                                || decorSlot == Options.BeardSlot && hideBeard
                                || decorSlot == Options.ExtraSlot && hideExtra)
                            {
                                continue;
                            }
                        }

                        if (MyDecors[decorSlot] != null)
                        {
                            // Do mappings for short hair if needed
                            if (decorSlot == Options.HairSlot && shortHair && Options.Instance.PlayerOpts.ShortHairMappings.TryGetValue(MyDecors[decorSlot], out var hairText))
                            {
                                DrawEquipment(hairText, renderColor.A, dir, TextureType.Decor);
                            }
                            else
                            {
                                DrawEquipment(MyDecors[decorSlot], renderColor.A, dir, TextureType.Decor);
                            }
                        }
                    }
                }
            }
        }

        public void DrawChatBubbles()
        {
            //Don't draw if the entity is hidden
            if (HideEntity)
            {
                return; 
            }

            //If unit is stealthed, don't render unless the entity is the player or party member.
            if (this != Globals.Me && !(this is Player player && Globals.Me.IsInMyParty(player)))
            {
                for (var n = 0; n < Status.Count; n++)
                {
                    if (Status[n].Type == StatusTypes.Stealth)
                    {
                        return;
                    }
                }
            }

            var chatbubbles = mChatBubbles.ToArray();
            var bubbleoffset = 0f;
            for (var i = chatbubbles.Length - 1; i > -1; i--)
            {
                bubbleoffset = chatbubbles[i].Draw(bubbleoffset);
            }
        }

        public virtual void DrawEquipment(string filename, int alpha, int dir, GameContentManager.TextureType textureType = GameContentManager.TextureType.Paperdoll)
        {
            var map = MapInstance.Get(CurrentMap);
            if (map == null)
            {
                return;
            }

            var srcRectangle = new FloatRect();
            var destRectangle = new FloatRect();

            GameTexture paperdollTex = null;
            var filenameNoExt = Path.GetFileNameWithoutExtension(filename);
            paperdollTex = Globals.ContentManager.GetTexture(
                textureType, $"{filenameNoExt}_{SpriteAnimation.ToString()}.png"
            );

            var spriteFrames = SpriteFrames;

            if (paperdollTex == null)
            {
                paperdollTex = Globals.ContentManager.GetTexture(textureType, filename);
                spriteFrames = Options.Instance.Sprites.NormalFrames;
            }

            if (paperdollTex != null)
            {
                if (paperdollTex.ScaledHeight / Options.Instance.Sprites.Directions > Options.TileHeight)
                {
                    destRectangle.X = map.GetX() + X * Options.TileWidth + OffsetX;
                    destRectangle.Y = GetCenterPos().Y - paperdollTex.ScaledHeight / (Options.Instance.Sprites.Directions * 2);
                }
                else
                {
                    destRectangle.X = map.GetX() + X * Options.TileWidth + OffsetX;
                    destRectangle.Y = map.GetY() + Y * Options.TileHeight + OffsetY;
                }

                if (paperdollTex.ScaledWidth / spriteFrames > Options.TileWidth)
                {
                    destRectangle.X -= (paperdollTex.ScaledWidth / spriteFrames - Options.TileWidth) / 2;
                }

                destRectangle.X = (int) Math.Ceiling(destRectangle.X);
                destRectangle.Y = (int) Math.Ceiling(destRectangle.Y);
                if (SpriteAnimation == SpriteAnimations.Normal)
                {
                    if (AttackTimer - CalculateAttackTime() / 2 > Timing.Global.Ticks / TimeSpan.TicksPerMillisecond || Blocking)
                    {
                        srcRectangle = new FloatRect(
                            3 * (int)paperdollTex.GetWidth() / spriteFrames, dir * (int)paperdollTex.GetHeight() / Options.Instance.Sprites.Directions,
                            (int)paperdollTex.GetWidth() / spriteFrames, (int)paperdollTex.GetHeight() / Options.Instance.Sprites.Directions
                        );
                    }
                    else
                    {
                        srcRectangle = new FloatRect(
                            WalkFrame * (int) paperdollTex.GetWidth() / spriteFrames, dir * (int) paperdollTex.GetHeight() / Options.Instance.Sprites.Directions,
                            (int) paperdollTex.GetWidth() / spriteFrames, (int) paperdollTex.GetHeight() / Options.Instance.Sprites.Directions
                        );
                    }
                }
                else
                {
                    srcRectangle = new FloatRect(
                        SpriteFrame * (int)paperdollTex.GetWidth() / spriteFrames, dir * (int)paperdollTex.GetHeight() / Options.Instance.Sprites.Directions,
                        (int)paperdollTex.GetWidth() / spriteFrames, (int)paperdollTex.GetHeight() / Options.Instance.Sprites.Directions
                    );
                }

                destRectangle.Width = srcRectangle.Width * Options.Scale;
                destRectangle.Height = srcRectangle.Height * Options.Scale;
                var renderColor = new Color(alpha, 255, 255, 255);
                if (Flash)
                {
                    renderColor = FlashColor;
                    renderColor.A = (byte) alpha;
                }

                Graphics.DrawGameTexture(
                    paperdollTex, srcRectangle, destRectangle, renderColor
                );
            }
        }

        protected virtual void CalculateCenterPos()
        {
            var pos = new Pointf(
                LatestMap.GetX() + X * Options.TileWidth + OffsetX + Options.TileWidth / 2,
                LatestMap.GetY() + Y * Options.TileHeight + OffsetY + Options.TileHeight / 2
            );

            if (Texture != null)
            {
                pos.Y += Options.TileHeight / 2;
                pos.Y -= Texture.ScaledHeight / (Options.Instance.Sprites.Directions * 2);
            }

            mCenterPos = pos;
        }

        //returns the point on the screen that is the center of the player sprite
        public Pointf GetCenterPos()
        {
            if (LatestMap == null)
            {
                return new Pointf(0, 0);
            }

            return mCenterPos;
        }

        public virtual float GetTopPos(int overrideHeight = 0)
        {
            var map = LatestMap;
            if (map == null)
            {
                return 0f;
            }

            var y = (int) Math.Ceiling(GetCenterPos().Y);
            if (overrideHeight != 0)
            {
                y = y - (int) (overrideHeight / (Options.Instance.Sprites.Directions * 2));
                y -= 12;
            }
            else
            {
                if (Texture != null)
                {
                    y = y - (int) (Texture.ScaledHeight / (Options.Instance.Sprites.Directions * 2));
                    y -= 12;
                }
            }

            if (GetType() != typeof(Event))
            {
                y -= 10;
            } //Need room for HP bar if not an event.

            return y;
        }

        public void DrawLabels(
            string label,
            int position,
            Color labelColor,
            Color textColor,
            Color borderColor = null,
            Color backgroundColor = null
        )
        {
            if (string.IsNullOrEmpty(label))
            {
                return;
            }

            if (label.Trim().Length == 0)
            {
                return;
            }

            if (HideName)
            {
                return;
            }

            if (borderColor == null)
            {
                borderColor = Color.Transparent;
            }

            if (backgroundColor == null)
            {
                backgroundColor = Color.Transparent;
            }

            //If we have a non-transparent label color then use it, otherwise use the players name color
            if (labelColor != null && labelColor.A != 0)
            {
                textColor = labelColor;
            }

            //Check for stealth amoungst status effects.
            for (var n = 0; n < Status.Count; n++)
            {
                //If unit is stealthed, don't render unless the entity is the player.
                if (Status[n].Type == StatusTypes.Stealth)
                {
                    if (this != Globals.Me && !(this is Player player && Globals.Me.IsInMyParty(player)))
                    {
                        return;
                    }
                }
            }

            var map = MapInstance;
            if (map == null)
            {
                return;
            }

            var textSize = Graphics.Renderer.MeasureText(label, Graphics.EntityNameFont, 1);

            var x = (int) Math.Ceiling(GetCenterPos().X);
            var y = position == 0 ? GetLabelLocation(LabelType.Header) : GetLabelLocation(LabelType.Footer);

            if (backgroundColor != Color.Transparent)
            {
                Graphics.DrawGameTexture(
                    Graphics.Renderer.GetWhiteTexture(), new FloatRect(0, 0, 1, 1),
                    new FloatRect(x - textSize.X / 2f - 4, y, textSize.X + 8, textSize.Y), backgroundColor
                );
            }
            
            var nameColor = Color.FromArgb(textColor.ToArgb());
            var outlineColor = Color.FromArgb(borderColor.ToArgb());
            nameColor.A = (byte)NameOpacity;
            outlineColor.A = NameOpacity < byte.MaxValue ? (byte)0 : (byte)NameOpacity;

            Graphics.Renderer.DrawString(
                label, Graphics.EntityNameFont, (int) (x - (int) Math.Ceiling(textSize.X / 2f)), (int) y, 1,
                nameColor, true, null, outlineColor
            );
        }

        public virtual void DrawName(Color textColor, Color borderColor = null, Color backgroundColor = null)
        {
            if (!Globals.Database.DisplayNpcNames && !(this is Player))
            {
                return;
            }

            if (Globals.Me?.InCutscene() ?? false)
            {
                return;
            }

            if (NpcBase.TryGet(NpcId, out var npcBase) && npcBase.HideName)
            {
                return;
            }

            if (this is Player && Globals.Me.MapInstance.ZoneType != MapZones.Safe && !InPvpSight)
            {
                return;
            }

            // Don't draw if non-aggressive NPC, IF namefading is off
            if (!(this is Event) && !(this is Player) && !IsTargeted && Type != -1)
            {
                return;
            }

            if (HideName || Name.Trim().Length == 0)
            {
                return;
            }

            if (IsDead)
            {
                return;
            }

            if (borderColor == null)
            {
                borderColor = Color.Transparent;
            }

            if (backgroundColor == null)
            {
                backgroundColor = Color.Transparent;
            }

            //Check for npc colors
            if (textColor == null)
            {
                LabelColor? color = null;
                switch (Type)
                {
                    case -1: //When entity has a target (showing aggression)
                        if (EntityTarget == Globals.Me.Id) // If that target is YOU!
                        {
                            color = CustomColors.Names.Npcs["PlayerAggro"];
                        }
                        else
                        {
                            color = CustomColors.Names.Npcs["Aggressive"];
                        }

                        break;
                    case 0: //Attack when attacked
                        color = CustomColors.Names.Npcs["AttackWhenAttacked"];

                        break;
                    case 1: //Attack on sight
                        color = CustomColors.Names.Npcs["AttackOnSight"];

                        break;
                    case 3: //Guard
                        color = CustomColors.Names.Npcs["Guard"];

                        break;
                    case 2: //Neutral
                    default:
                        color = CustomColors.Names.Npcs["Neutral"];

                        break;
                }

                if (color != null)
                {
                    textColor = color?.Name;
                    backgroundColor = color?.Background;
                    borderColor = color?.Outline;
                }
            }

            //Check for stealth amoungst status effects.
            for (var n = 0; n < Status.Count; n++)
            {
                //If unit is stealthed, don't render unless the entity is the player.
                if (Status[n].Type == StatusTypes.Stealth)
                {
                    if (this != Globals.Me && !IsAllyOf(Globals.Me))
                    {
                        return;
                    }
                }
            }

            var map = MapInstance;
            if (map == null)
            {
                return;
            }

            var name = Name;
            if ((this is Player && Options.Player.ShowLevelByName) || (!(this is Player) && Options.Npc.ShowLevelByName))
            {
                if (this is Player)
                {
                    name = Strings.GameWindow.EntityNameAndLevel.ToString(Name, Level);
                }
                else
                {
                    name = Strings.GameWindow.NpcNameAndLevel.ToString(Name, Level);
                }
            }

            if (NpcId != default && !BestiaryController.MyBestiary.HasUnlock(NpcId, BestiaryUnlock.NameAndDescription))
            {
                name = "???";
            }

            var textSize = Graphics.Renderer.MeasureText(name, Graphics.EntityNameFont, 1);

            var x = (int) Math.Ceiling(GetCenterPos().X);
            var y = GetLabelLocation(LabelType.Name);

            if (backgroundColor != Color.Transparent)
            {
                Graphics.DrawGameTexture(
                    Graphics.Renderer.GetWhiteTexture(), new FloatRect(0, 0, 1, 1),
                    new FloatRect(x - textSize.X / 2f - 4, y, textSize.X + 8, textSize.Y), backgroundColor
                );
            }

            var nameColor = Color.FromArgb(textColor.ToArgb());
            var outlineColor = Color.FromArgb(borderColor.ToArgb());
            NameOpacityUpdate();
            nameColor.A = (byte)NameOpacity;
            outlineColor.A = NameOpacity < byte.MaxValue ? (byte)0 : (byte)NameOpacity;

            var finalX = (int)(x - (int)Math.Ceiling(textSize.X / 2f));
            Graphics.Renderer.DrawString(
                name, Graphics.EntityNameFont, finalX, (int) y, 1,
                nameColor, true, null, outlineColor
            );

            DrawFlair(finalX - 24 - (1 * Options.Scale), (int)y - (1 * Options.Scale));

            IsTargeted = false; // allow resetting of target-only name display
        }

        public float GetLabelLocation(LabelType type)
        {
            var y = GetTopPos() - 4;
            switch (type)
            {
                case LabelType.Header:
                    if (string.IsNullOrWhiteSpace(HeaderLabel.Text))
                    {
                        return GetLabelLocation(LabelType.Name);
                    }

                    y = GetLabelLocation(LabelType.Name);
                    var headerSize = Graphics.Renderer.MeasureText(HeaderLabel.Text, Graphics.EntityNameFont, 1);
                    y -= headerSize.Y;

                    break;
                case LabelType.Footer:
                    if (string.IsNullOrWhiteSpace(FooterLabel.Text))
                    {
                        break;
                    }

                    var footerSize = Graphics.Renderer.MeasureText(FooterLabel.Text, Graphics.EntityNameFont, 1);
                    y -= footerSize.Y - 8;

                    break;
                case LabelType.Name:
                    y = GetLabelLocation(LabelType.Footer);
                    var nameSize = Graphics.Renderer.MeasureText(Name, Graphics.EntityNameFont, 1);
                    if (string.IsNullOrEmpty(FooterLabel.Text))
                    {
                        y -= nameSize.Y - 8;
                    }
                    else
                    {
                        y -= nameSize.Y;
                    }

                    break;
                case LabelType.ChatBubble:
                    y = GetLabelLocation(LabelType.Header) - 4;

                    break;
                case LabelType.Guild:
                    // ???? This should never NOT run on a player ????
                    if (this is Player player)
                    {
                        if (string.IsNullOrWhiteSpace(player.Guild))
                        {
                            return GetLabelLocation(LabelType.Name);
                        }

                        // Do we have a header? If so, slightly change the position!
                        if (string.IsNullOrWhiteSpace(HeaderLabel.Text))
                        {
                            y = GetLabelLocation(LabelType.Name);
                        }
                        else
                        {
                            y = GetLabelLocation(LabelType.Header);
                        }

                        var guildSize = Graphics.Renderer.MeasureText(player.Guild, Graphics.EntityNameFont, 1);
                        y -= guildSize.Y;
                    }
                    break;
            }

            return y;
        }
        public int GetShieldSize()
        {
            var shieldSize = 0;
            foreach (var status in Status)
            {
                if (status.Type == StatusTypes.Shield)
                {
                    shieldSize += status.Shield[(int)Vitals.Health];
                }
            }
            return shieldSize;
        }

        public void DrawHpBar()
        {
            if (HideName && HideEntity)
            {
                return;
            }

            if (PvpHide)
            {
                return;
            }

            if (IsDead)
            {
                return;
            }

            if (this is Resource && Options.HideResourceHealthBars)
            {
                return;
            }

            var currentHealth = Vital[(int)Vitals.Health];
            if (currentHealth <= 0)
            {
                return;
            }

            var maxVital = MaxVital[(int)Vitals.Health];
            var newMaxVital = maxVital;
            var shieldSize = 0;

            //Check for shields
            var currHealthAndShield = currentHealth;
            foreach (var status in Status)
            {
                if (status.Type == StatusTypes.Shield)
                {
                    shieldSize += status.Shield[(int) Vitals.Health];
                    currHealthAndShield += shieldSize;
                    if (shieldSize + currHealthAndShield > newMaxVital)
                    {
                        newMaxVital += status.Shield[(int)Vitals.Health];
                    }
                }
            }

            if (currentHealth == maxVital && shieldSize <= 0)
            {
                return;
            }

            if (!BestiaryController.MyBestiary.HasUnlock(NpcId, BestiaryUnlock.HP))
            {
                return;
            }

            //Check for stealth amoungst status effects.
            for (var n = 0; n < Status.Count; n++)
            {
                //If unit is stealthed, don't render unless the entity is the player.
                if (Status[n].Type == StatusTypes.Stealth)
                {
                    if (this != Globals.Me && !(this is Player player && Globals.Me.IsInMyParty(player)))
                    {
                        return;
                    }
                }
            }

            var map = MapInstance.Get(CurrentMap);
            if (map == null)
            {
                return;
            }

            // Let's start drawing
            var hpBackground = Globals.ContentManager.GetTexture(TextureType.Misc, "hpbackground.png");
            var hpForeground = Globals.ContentManager.GetTexture(TextureType.Misc, "hpbar.png");
            var shieldForeground = Globals.ContentManager.GetTexture(TextureType.Misc, "shieldbar.png");

            var width = hpBackground.GetWidth();

            var hpfillRatio = (float) currentHealth / newMaxVital;
            hpfillRatio = (float) MathHelper.Clamp(hpfillRatio, 0f, 1f);
            var hpfillWidth = MathHelper.RoundNearestMultiple((int)(hpfillRatio * hpForeground.GetWidth()), 4);

            var shieldfillRatio = (float)shieldSize / newMaxVital;
            shieldfillRatio = (float)MathHelper.Clamp(shieldfillRatio, 0f, 1f);
            var shieldfillWidth = MathHelper.RoundNearestMultiple((int)(shieldfillRatio * shieldForeground.GetWidth()), 4);

            var y = (int) Math.Ceiling(GetCenterPos().Y);
            var x = (int) Math.Ceiling(GetCenterPos().X);
            var entityTex = Globals.ContentManager.GetTexture(TextureType.Entity, MySprite);
            if (entityTex != null)
            {
                y = y - (int) (entityTex.ScaledHeight / (Options.Instance.Sprites.Directions * 2));
                y -= 8;
            }

            var centerX = x - hpBackground.GetWidth() / 2;

            if (hpBackground != null)
            {
                Graphics.DrawGameTexture(
                    hpBackground, new FloatRect(0, 0, hpBackground.GetWidth(), hpBackground.GetHeight()),
                    new FloatRect(centerX, y, width, hpBackground.GetHeight()), Color.White
                );
            }
            
            var color = Color.White;
            if (this != Globals.Me && Globals.Me.TargetIndex != Id)
            {
                color.A = 150;
            }

            if (hpForeground != null)
            {
                Graphics.DrawGameTexture(
                    hpForeground, 
                    new FloatRect(0, 0, hpForeground.GetWidth(), hpForeground.GetHeight()),
                    new FloatRect(centerX + 4, y + 4, hpfillWidth, hpForeground.GetHeight()), color
                );
            }

            if (shieldSize > 0 && shieldForeground != null) //Check for a shield to render
            {
                Graphics.DrawGameTexture(
                    shieldForeground,
                    new FloatRect(0, 0, shieldForeground.GetWidth(), shieldForeground.GetHeight()),
                    new FloatRect(centerX + hpfillWidth + 4, y + 4, shieldfillWidth, shieldForeground.GetHeight()), color
                );
            }
        }

        public void DrawCastingBar()
        {
            if (!IsCasting)
            {
                SpellCast = default;
                return;
            }

            if (PvpHide)
            {
                return;
            }

            if (CurrentMap == default)
            {
                return;
            }

            if (IsDead)
            {
                return;
            }

            var drawBars = BestiaryController.MyBestiary.HasUnlock(NpcId, BestiaryUnlock.SpellCombatInfo);
            var drawSpell = BestiaryController.MyBestiary.HasUnlock(NpcId, BestiaryUnlock.Spells);

            var map = MapInstance.Get(CurrentMap);
            var castSpell = SpellBase.Get(SpellCast);
            if (castSpell != null)
            {
                var castBackground = Globals.ContentManager.GetTexture(TextureType.Misc, "castbackground.png");
                var castForeground = Globals.ContentManager.GetTexture(TextureType.Misc, "castbar.png");

                var width = castBackground.GetWidth();
                var fillratio = (castSpell.CastDuration - (CastTime - Timing.Global.Milliseconds)) /
                                (float)castSpell.CastDuration;
                fillratio = (float)MathHelper.Clamp(fillratio, 0f, 1f);
                var castFillWidth = MathHelper.RoundNearestMultiple((int)(fillratio * castForeground.GetWidth()), 4);

                var y = (int) Math.Ceiling(GetCenterPos().Y);
                var x = (int) Math.Ceiling(GetCenterPos().X);
                var entityTex = Globals.ContentManager.GetTexture(TextureType.Entity, MySprite);
                if (entityTex != null)
                {
                    y = y + (int) (entityTex.ScaledHeight / (Options.Instance.Sprites.Directions * 2));
                    y += 19;
                }

                var centerX = x - width / 2;
                y -= 1;

                var color = Color.White;
                if (this != Globals.Me && Globals.Me.TargetIndex != Id)
                {
                    color.A = 150;
                }

                if (drawBars)
                {
                    if (castBackground != null)
                    {
                        Graphics.DrawGameTexture(
                            castBackground, new FloatRect(0, 0, castBackground.GetWidth(), castBackground.GetHeight()),
                            new FloatRect(centerX, y, width, castBackground.GetHeight()), color
                        );
                    }

                    if (castForeground != null && drawBars)
                    {
                        Graphics.DrawGameTexture(
                            castForeground,
                            new FloatRect(0, 0, castForeground.GetWidth(), castForeground.GetHeight()),
                            new FloatRect(centerX + 4, y + 4, castFillWidth, castForeground.GetHeight()), color
                        );
                    }
                }

                if (!string.IsNullOrEmpty(castSpell.Icon) && castSpell.Icon != Strings.General.none && drawSpell)
                {
                    DrawSpellIcon(x, y, castSpell.Icon, color);
                }
            }
        }

        public bool PvpHide => this is Player && Globals.Me.MapInstance.ZoneType != MapZones.Safe && !InPvpSight;

        //
        public void DrawTarget(int priority)
        {
            IsTargeted = true;
            if (GetType() == typeof(Projectile))
            {
                return;
            }

            var map = MapInstance.Get(CurrentMap);
            if (map == null)
            {
                return;
            }

            if (IsDead)
            {
                return;
            }

            if (PvpHide)
            {
                return;
            }

            var targetTex = Globals.Me.StatusActive(StatusTypes.Taunt) ?
                Globals.ContentManager.GetTexture(TextureType.Misc, "taunt_target.png") :
                Globals.ContentManager.GetTexture(TextureType.Misc, "target.png");

            if (targetTex != null)
            {
                var srcRectangle = new FloatRect(
                    priority * targetTex.GetWidth() / 2f,
                    0,
                    targetTex.GetWidth() / 2f,
                    targetTex.GetHeight()
                );

                var destRectangle = new FloatRect(
                    (float) Math.Ceiling(GetCenterPos().X - targetTex.GetWidth() / 4f),
                    (float) Math.Ceiling(GetCenterPos().Y - targetTex.GetHeight() / 2f),
                    srcRectangle.Width,
                    srcRectangle.Height
                );

                Graphics.DrawGameTexture(targetTex, srcRectangle, destRectangle, Color.White);
            }
        }

        public virtual bool CanBeAttacked()
        {
            return !IsDead;
        }

        //Chatting
        public void AddChatBubble(string text, ChatBubbleType type)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            if (PvpHide)
            {
                return;
            }

            mChatBubbles.Add(new ChatBubble(this, text, type));
        }

        //Statuses
        public bool SpellStatusActive(Guid guid)
        {
            foreach (var status in Status)
            {
                if (status.SpellId == guid && status.IsActive())
                {
                    return true;
                }
            }

            return false;
        }

        public bool StatusActive(StatusTypes status, Action action = default)
        {
            bool statusFound = false;
            for (var n = 0; n < Status.Count; n++)
            {
                if (Status[n].Type == status)
                {
                    statusFound = true;
                    if (action != default)
                    {
                        action();
                    }
                }
            }

            return statusFound;
        }

        public int StatusCount(StatusTypes status)
        {
            return Status.Where(Status => Status.Type == status).Count();
        }

        public Status GetStatus(Guid guid)
        {
            foreach (var status in Status)
            {
                if (status.SpellId == guid && status.IsActive())
                {
                    return status;
                }
            }

            return null;
        }

        public void SortStatuses()
        {
            //Sort Status effects by remaining time
            Status = Status.OrderByDescending(x => x.RemainingMs).ToList();
        }

        public void UpdateSpriteAnimation()
        {
            var oldAnim = SpriteAnimation;

            //Exit if textures haven't been loaded yet
            if (AnimatedTextures.Count == 0)
            {
                return;
            }

            if (IsDead)
            {
                return;
            }

            SpriteAnimation = AnimatedTextures[SpriteAnimations.Idle] != null && LastActionTime + Options.Instance.Sprites.TimeBeforeIdle < Timing.Global.Milliseconds ? SpriteAnimations.Idle : SpriteAnimations.Normal;
            if (IsMoving)
            {
                SpriteAnimation = SpriteAnimations.Normal;
                LastActionTime = Timing.Global.Milliseconds;
            }
            else if (AttackTimer > Timing.Global.Ticks / TimeSpan.TicksPerMillisecond) //Attacking
            {
                var timeIn = CalculateAttackTime() - (AttackTimer - Timing.Global.Ticks / TimeSpan.TicksPerMillisecond);
                LastActionTime = Timing.Global.Milliseconds;

                if (AnimatedTextures[SpriteAnimations.Attack] != null)
                {
                    SpriteAnimation = SpriteAnimations.Attack;
                }

                if (Options.WeaponIndex > -1 && Options.WeaponIndex < Equipment.Length)
                {
                    if (Equipment[Options.WeaponIndex] != Guid.Empty && this != Globals.Me ||
                        MyEquipment[Options.WeaponIndex] < Options.MaxInvItems)
                    {
                        var itemId = Guid.Empty;
                        if (this == Globals.Me)
                        {
                            var slot = MyEquipment[Options.WeaponIndex];
                            if (slot > -1)
                            {
                                itemId = Inventory[slot].ItemId;
                            }
                        }
                        else
                        {
                            itemId = Equipment[Options.WeaponIndex];
                        }

                        var item = ItemBase.Get(itemId);
                        if (item != null)
                        {
                            if (AnimatedTextures[SpriteAnimations.Weapon] != null)
                            {
                                SpriteAnimation = SpriteAnimations.Weapon;
                            }

                            if (AnimatedTextures[SpriteAnimations.Shoot] != null && item.ProjectileId != Guid.Empty)
                            {
                                SpriteAnimation = SpriteAnimations.Shoot;
                            }
                        }
                    }
                }

                if (SpriteAnimation != SpriteAnimations.Normal && SpriteAnimation != SpriteAnimations.Idle)
                {
                    SpriteFrame = (int)Math.Floor((timeIn / (CalculateAttackTime() / (float)SpriteFrames)));
                }
            }
            else if (IsCasting)
            {
                var spell = SpellBase.Get(SpellCast);
                if (spell != null)
                {
                    var duration = spell.CastDuration;
                    var timeIn = duration - (CastTime - Timing.Global.Milliseconds);

                    if (AnimatedTextures[SpriteAnimations.Cast] != null)
                    {
                        SpriteAnimation = SpriteAnimations.Cast;
                    }

                    if (spell.SpellType == SpellTypes.CombatSpell &&
                        spell.Combat.TargetType == SpellTargetTypes.Projectile && AnimatedTextures[SpriteAnimations.Shoot] != null)
                    {
                        SpriteAnimation = SpriteAnimations.Shoot;
                    }

                    SpriteFrame = (int)Math.Floor((timeIn / (duration / (float)SpriteFrames)));
                }
                LastActionTime = Timing.Global.Milliseconds;
            }

            if (SpriteAnimation == SpriteAnimations.Normal)
            {
                ResetSpriteFrame();
            }
            else if (SpriteAnimation == SpriteAnimations.Idle)
            {
                if (SpriteFrameTimer + Options.Instance.Sprites.IdleFrameDuration < Timing.Global.Milliseconds)
                {
                    SpriteFrame++;
                    if (SpriteFrame >= SpriteFrames)
                    {
                        SpriteFrame = 0;
                    }
                    SpriteFrameTimer = Timing.Global.Milliseconds;
                }
            }
        }

        public void ResetSpriteFrame()
        {
            SpriteFrame = 0;
            SpriteFrameTimer = Timing.Global.Milliseconds;
        }

        public void LoadAnimationTextures(string tex)
        {
            var file = Path.GetFileNameWithoutExtension(tex);
            var ext = Path.GetExtension(tex);

            AnimatedTextures.Clear();
            foreach (var anim in Enum.GetValues(typeof(SpriteAnimations)))
            {
                AnimatedTextures.Add((SpriteAnimations)anim, Globals.ContentManager.GetTexture(GameContentManager.TextureType.Entity, $@"{file}_{anim}.png"));
            }
        }

        public byte GetDirectionTo(Entity otherEntity)
        {
            if (otherEntity == null) return Dir;

            bool preferY;
            int yOffset, xOffset;
            byte newDir = Dir;

            if (otherEntity.MapInstance.Id != MapInstance.Id)
            {
                int relX = X;
                int relY = Y;
                int relEntityX = otherEntity.X;
                int relEntityY = otherEntity.Y;

                if (otherEntity.MapInstance.MapGridX < MapInstance.MapGridX)
                {
                    relX += Options.MapWidth - 1;
                } else if (otherEntity.MapInstance.MapGridX > MapInstance.MapGridX)
                {
                    relEntityX += (Options.MapWidth - 1);
                }
                
                if (otherEntity.MapInstance.MapGridY < MapInstance.MapGridY)
                {
                    relY += Options.MapHeight - 1;
                } else if (otherEntity.MapInstance.MapGridY > MapInstance.MapGridY)
                {
                    relEntityY += Options.MapHeight - 1;
                }

                yOffset = relY - relEntityY;
                xOffset = relX - relEntityX;
                preferY = (Math.Abs(yOffset) - Math.Abs(xOffset)) > 0;
            } else
            {
                yOffset = Y - otherEntity.Y;
                xOffset = X - otherEntity.X;
                preferY = (Math.Abs(yOffset) - Math.Abs(xOffset)) > 0;
            }

            if (preferY)
            {
                if (yOffset > 0) newDir = (byte)Directions.Up;
                else if (yOffset < 0) newDir = (byte)Directions.Down;
                else if (xOffset > 0) newDir = (byte)Directions.Left;
                else if (xOffset < 0) newDir = (byte)Directions.Right;
            }
            else
            {
                if (xOffset > 0) newDir = (byte)Directions.Left;
                else if (xOffset < 0) newDir = (byte)Directions.Right;
                else if (yOffset > 0) newDir = (byte)Directions.Up;
                else if (yOffset < 0) newDir = (byte)Directions.Down;
            }

            return newDir;
        }

        //Movement
        /// <summary>
        ///     Returns -6 if the tile is blocked by a global (non-event) entity
        ///     Returns -5 if the tile is completely out of bounds.
        ///     Returns -4 if a tile is blocked because of a local event.
        ///     Returns -3 if a tile is blocked because of a Z dimension tile
        ///     Returns -2 if a tile does not exist or is blocked by a map attribute.
        ///     Returns -1 is a tile is passable.
        ///     Returns any value zero or greater matching the entity index that is in the way.
        /// </summary>
        /// <returns></returns>
        public int IsTileBlocked(
            int x,
            int y,
            int z,
            Guid mapId,
            ref Entity blockedBy,
            bool ignoreAliveResources = true,
            bool ignoreDeadResources = true,
            bool ignoreNpcAvoids = true,
            bool grounded = true
        )
        {
            var mapInstance = MapInstance.Get(mapId);
            if (mapInstance == null)
            {
                return -2;
            }

            var gridX = mapInstance.MapGridX;
            var gridY = mapInstance.MapGridY;
            try
            {
                var tmpX = x;
                var tmpY = y;
                var tmpMapId = Guid.Empty;
                if (x < 0)
                {
                    gridX--;
                    tmpX = Options.MapWidth - x * -1;
                }

                if (y < 0)
                {
                    gridY--;
                    tmpY = Options.MapHeight - y * -1;
                }

                if (x > Options.MapWidth - 1)
                {
                    gridX++;
                    tmpX = x - Options.MapWidth;
                }

                if (y > Options.MapHeight - 1)
                {
                    gridY++;
                    tmpY = y - Options.MapHeight;
                }

                if (gridX < 0 || gridY < 0 || gridX >= Globals.MapGridWidth || gridY >= Globals.MapGridHeight)
                {
                    return -2;
                }

                tmpMapId = Globals.MapGrid[gridX, gridY];

                foreach (var en in Globals.Entities)
                {
                    if (en.Value == null)
                    {
                        continue;
                    }

                    if (en.Value == Globals.Me)
                    {
                        continue;
                    }
                    else
                    {
                        if (en.Value.CurrentMap == tmpMapId &&
                            en.Value.X == tmpX &&
                            en.Value.Y == tmpY &&
                            en.Value.Z == Z)
                        {
                            if (en.Value.GetType() != typeof(Projectile))
                            {
                                if (en.Value.GetType() == typeof(Resource))
                                {
                                    var resourceBase = ((Resource)en.Value).GetResourceBase();
                                    if (resourceBase != null)
                                    {
                                        if (!ignoreAliveResources && !((Resource)en.Value).IsDead)
                                        {
                                            blockedBy = en.Value;

                                            return -6;
                                        }

                                        if (!ignoreDeadResources && ((Resource)en.Value).IsDead)
                                        {
                                            blockedBy = en.Value;

                                            return -6;
                                        }

                                        if (resourceBase.WalkableAfter && ((Resource)en.Value).IsDead ||
                                            resourceBase.WalkableBefore && !((Resource)en.Value).IsDead)
                                        {
                                            continue;
                                        }
                                    }
                                }
                                else if (en.Value.GetType() == typeof(Player))
                                {
                                    //Return the entity key as this should block the player.  Only exception is if the MapZone this entity is on is passable.
                                    var entityMap = MapInstance.Get(en.Value.CurrentMap);
                                    if (Options.Instance.Passability.Passable[(int)entityMap.ZoneType] || ((Player)en.Value).IsDead)
                                    {
                                        // AND if the entity isn't currently a dueler!
                                        if (!Globals.Me.DuelingIds.Contains(en.Key))
                                        {
                                            continue;
                                        }
                                    }
                                }

                                blockedBy = en.Value;

                                return -6;
                            }
                        }
                    }
                }

                if (MapInstance.Get(tmpMapId) != null)
                {
                    foreach (var en in MapInstance.Get(tmpMapId).LocalEntities)
                    {
                        if (en.Value == null)
                        {
                            continue;
                        }

                        if (en.Value.CurrentMap == tmpMapId &&
                            en.Value.X == tmpX &&
                            en.Value.Y == tmpY &&
                            en.Value.Z == Z &&
                            !en.Value.Passable)
                        {
                            var entityMap = MapInstance.Get(en.Value.CurrentMap);
                            blockedBy = en.Value;
                            if (!(this is Projectile))
                            {
                                return -4;
                            }
                        }
                    }

                    foreach (var en in MapInstance.Get(tmpMapId).Critters)
                    {
                        if (en.Value == null)
                        {
                            continue;
                        }

                        if (en.Value.CurrentMap == tmpMapId &&
                            en.Value.X == tmpX &&
                            en.Value.Y == tmpY &&
                            en.Value.Z == Z &&
                            !en.Value.Passable)
                        {
                            blockedBy = en.Value;

                            return -4;
                        }
                    }
                }

                var gameMap = MapInstance.Get(Globals.MapGrid[gridX, gridY]);
                if (gameMap != null)
                {
                    if (gameMap.Attributes[tmpX, tmpY] != null)
                    {
                        if (gameMap.Attributes[tmpX, tmpY] is MapBlockedAttribute blockAttr)
                        {
                            if (grounded)
                            {
                                return -2;
                            } else
                            {
                                if (!blockAttr.GroundLevel)
                                {
                                    return -2;
                                }
                            }
                        }

                        if ((gameMap.Attributes[tmpX, tmpY].Type == MapAttributes.Animation && ((MapAnimationAttribute)gameMap.Attributes[tmpX, tmpY]).IsBlock))
                        {
                            return -2;
                        }
                        if ((gameMap.Attributes[tmpX, tmpY].Type == MapAttributes.Item && ((MapItemAttribute)gameMap.Attributes[tmpX, tmpY]).IsBlock))
                        {
                            return -2;
                        }
                        else if (gameMap.Attributes[tmpX, tmpY].Type == MapAttributes.ZDimension)
                        {
                            if (((MapZDimensionAttribute)gameMap.Attributes[tmpX, tmpY]).BlockedLevel - 1 == z)
                            {
                                return -3;
                            }
                        }
                        else if (gameMap.Attributes[tmpX, tmpY].Type == MapAttributes.NpcAvoid)
                        {
                            if (!ignoreNpcAvoids)
                            {
                                return -2;
                            }
                        }
                    }
                }
                else
                {
                    return -5;
                }

                return -1;
            }
            catch
            {
                return -2;
            }
        }

        ~Entity()
        {
            Dispose();
        }
    }

    public partial class Entity
    {
        public virtual bool IsAllyOf(Entity en)
        {
            if (en == null || CurrentMap == default || en.CurrentMap == default)
            {
                return false;
            }

            var myMap = MapInstance.Get(CurrentMap);

            if (myMap == null)
            {
                return false;
            }

            var entityType = en.GetEntityType();
            if (entityType == EntityTypes.Resource || entityType == EntityTypes.Event)
            {
                return true;
            }

            if (Id == Globals.Me.Id)
            {
                return true;
            }

            if (en is Player targetPlayer)
            {
                // Player V Player
                if (this is Player me)
                {
                    // Always a friend in a safe zone!
                    if (myMap.ZoneType == MapZones.Safe)
                    {
                        return true;
                    }
                    else
                    {
                        return me.IsInMyParty(targetPlayer.Id) || targetPlayer.IsInMyParty(me.Id) || (!string.IsNullOrEmpty(me.Guild) && me.Guild == targetPlayer.Guild);
                    }
                }
                // Entity V Player
                else
                {
                    return Type != -1;
                }
            }
            else
            {
                return Type != -1;
            }
        }

        public double CalculateDirectionTo(Entity en)
        {
            var selfTile = GetCenterPos();
            var selfX = selfTile.X;
            var selfY = selfTile.Y;

            var otherTile = en.GetCenterPos();
            var otherX = otherTile.X;
            var otherY = otherTile.Y;

            return CalculateDirectionToPoint(selfX, selfY, otherX, otherY);
        }

        public static double CalculateDirectionToPoint(float selfX, float selfY, float otherX, float otherY)
        {
            return Math.Atan2(otherY - selfY, otherX - selfX) * (180 / Math.PI);
        }

        public double CalculateDistanceTo(Entity en)
        {
            if (en == null)
            {
                throw new ArgumentNullException(nameof(en));
            }

            var selfTile = GetCurrentTileRectangle();
            var selfX = selfTile.CenterX;
            var selfY = selfTile.CenterY;

            var otherTile = en.GetCurrentTileRectangle();
            var otherX = otherTile.CenterX;
            var otherY = otherTile.CenterY;

            return MathHelper.CalculateDistanceToPoint(selfX, selfY, otherX, otherY);
        }

        public int CalculateTileDistanceTo(Entity en)
        {
            if (en == null)
            {
                return 0;
            }

            var distance = CalculateDistanceTo(en);
            return (int)Math.Ceiling(distance / Math.Max(Options.TileHeight, Options.TileWidth));
        }
    }

    // New drawing functions
    public partial class Entity
    {
        private bool IndicatorFlash = false;
        private const int IndicatorFrames = 2;
        private long LastFlash;
        private const int IndicatorRadius = 48;
        private GameTexture CASTER_INDICATOR_TEXTURE = Globals.ContentManager.GetTexture(TextureType.Misc, "caster_indicator.png");
        
        private int AoeAlpha = MAX_AOE_ALPHA;
        private int AoeAlphaDir = -1;
        private long AoeAlphaUpdate;
        private GameTexture COMBAT_TILE_AOE = Globals.ContentManager.GetTexture(TextureType.Misc, "aoe.png");
        private GameTexture COMBAT_TILE_NEUTRAL = Globals.ContentManager.GetTexture(TextureType.Misc, "aoe_neutral.png");
        private GameTexture COMBAT_TILE_FRIENDLY = Globals.ContentManager.GetTexture(TextureType.Misc, "aoe_heal.png");
        private GameTexture SINGLE_TARGET = Globals.ContentManager.GetTexture(TextureType.Misc, "single-target_hostile.png");
        private GameTexture SINGLE_TARGET_NEUTRAL = Globals.ContentManager.GetTexture(TextureType.Misc, "single-target_neutral.png");
        private GameTexture SINGLE_TARGET_FRIENDLY = Globals.ContentManager.GetTexture(TextureType.Misc, "single-target_friendly.png");
        private GameTexture SINGLE_TARGET_FRIENDLY_OUTLINE = Globals.ContentManager.GetTexture(TextureType.Misc, "single-target-ol_friendly.png");
        private GameTexture SINGLE_TARGET_OUTLINE = Globals.ContentManager.GetTexture(TextureType.Misc, "single-target-ol_hostile.png");
        private GameTexture SINGLE_TARGET_NEUTRAL_OUTLINE = Globals.ContentManager.GetTexture(TextureType.Misc, "single-target-ol_neutral.png");
        private GameTexture PROJECTILE_TEXTURES = Globals.ContentManager.GetTexture(TextureType.Misc, "projectile_dir_markers.png");
        public bool IsDead;

        public void DrawAggroIndicator(int maxRange, bool friendly)
        {
            if (!(this is Player) && EntityTarget != Globals.Me.Id)
            {
                return;
            }
            if (friendly || !Globals.Database.CastingIndicator)
            {
                return;
            }
            if (Globals.Me == null || Globals.Me.Id == Id)
            {
                return;
            }
            if (this is Player)
            {
                return;
            }

            var angle = Globals.Me.CalculateDirectionTo(this);
            var width = CASTER_INDICATOR_TEXTURE.GetWidth() / IndicatorFrames;
            var height = CASTER_INDICATOR_TEXTURE.GetHeight();
            
            var x = Globals.Me.GetCenterPos().X - (width / 2);
            var y = Globals.Me.GetCenterPos().Y;

            if (Timing.Global.Milliseconds > LastFlash)
            {
                LastFlash = Timing.Global.Milliseconds + 150;
                IndicatorFlash = !IndicatorFlash;
            }

            var maxCastingDistance = maxRange * Options.Map.TileWidth; 
            var distanceBetween = CalculateDistanceTo(Globals.Me);
            var alpha = 255 - (int)Math.Round((distanceBetween / maxCastingDistance) * 255f);
            alpha = MathHelper.Clamp(alpha, 0, 255);

            var rcos = IndicatorRadius * MathHelper.DCos(angle);
            var rsin = IndicatorRadius * MathHelper.DSin(angle);
            var xPos = x + rcos;
            var yPos = y + rsin;

            var frame = Convert.ToInt32(IndicatorFlash);
            Graphics.DrawGameTexture(
               CASTER_INDICATOR_TEXTURE, new FloatRect(width * frame, 0, width, height),
               new FloatRect((float)xPos, (float)yPos, width * 4, height * 4), new Color(alpha, 255, 255, 255),
               rotationDegrees: (float)angle
           );
        }

        public FloatRect GetCurrentTileRectangle()
        {
            var selfMap = MapInstance.Get(CurrentMap);
            return GetTileRectangle(selfMap, X, Y);
        }


        public static FloatRect GetTileRectangle(MapInstance map, byte x, byte y)
        {
            if (map == null)
            {
                return new FloatRect(0, 0, 0, 0);
            }

            return new FloatRect(
                map.GetX() + x * Options.TileWidth,
                map.GetY() + y * Options.TileHeight,
                Options.TileWidth,
                Options.TileHeight);
        }

        private bool ShouldRenderMarkers(bool friendly)
        {
            if (!BestiaryController.MyBestiary.HasUnlock(NpcId, BestiaryUnlock.SpellCombatInfo))
            {
                return false;
            }

            if (PvpHide)
            {
                return false;
            }

            if (!friendly && !Globals.Database.HostileTileMarkers)
            {
                return false;
            }
            if (friendly)
            {
                if (Id == Globals.Me.Id && !Globals.Database.SelfTileMarkers)
                {
                    return false;
                }
                else if (this is Player friendlyPlayer && friendlyPlayer.Id != Globals.Me.Id)
                {
                    if (!Globals.Me.IsInMyParty(friendlyPlayer.Id) && !friendlyPlayer.IsInMyParty(Globals.Me.Id))
                    {
                        return false;
                    }
                    else if (!Globals.Database.PartyTileMarkers)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private const int MAX_AOE_ALPHA = 200;
        private const int MIN_AOE_ALPHA = 125;
        private const int AOE_UPDATE_MS = 100;
        private const int AOE_UPDATE_AMT = 15;
        public void DrawAoe(SpellBase spell, SpellTargetTypes targetType, MapInstance spawnMap, byte spawnX, byte spawnY, bool friendly, int size, bool onlyMe = false)
        {
            if (spell == null)
            {
                return;
            }
            if (!ShouldRenderMarkers(friendly))
            {
                return;
            }
            if (onlyMe && EntityTarget != Globals.Me.Id)
            {
                return;
            }

            if (Timing.Global.Milliseconds > AoeAlphaUpdate)
            {
                AoeAlphaUpdate = Timing.Global.Milliseconds + AOE_UPDATE_MS;

                if (AoeAlpha <= MIN_AOE_ALPHA)
                {
                    AoeAlphaDir = 1;
                }
                else if (AoeAlpha >= MAX_AOE_ALPHA)
                {
                    AoeAlphaDir = -1;
                }
                AoeAlpha = MathHelper.Clamp(AoeAlpha + (AOE_UPDATE_AMT * AoeAlphaDir), MIN_AOE_ALPHA, MAX_AOE_ALPHA);
            }

            // First, calculate the relative offset positions
            var offsetX = spell.Combat.AoeXOffset;
            var offsetY = spell.Combat.AoeYOffset;
            if (spell.Combat.AoeRelativeOffset)
            {
                var aoeOffset = PositionUtilities.GetAoeOffset(
                   GetFaceDirection(),
                   offsetX,
                   offsetY,
                   spell.Combat.AoeShape,
                   spell.Combat.AoeRectWidth,
                   spell.Combat.AoeRectHeight);

                offsetX = aoeOffset.X;
                offsetY = aoeOffset.Y;
            }
            
            // Then, translate these offsets from our spell's starting position
            MapInstance.TryGetMapInstanceFromCoords(spawnMap.Id, spawnX + offsetX, spawnY + offsetY, out var offsettedSpawnMap, out var offsettedSpawnX, out var offsettedSpawnY);
            if (offsettedSpawnMap == null || !offsettedSpawnX.HasValue || !offsettedSpawnY.HasValue)
            {
                return;
            }

            int left = offsettedSpawnX.Value - size;
            int right = offsettedSpawnX.Value + size;
            int top = offsettedSpawnY.Value - size;
            int bottom = offsettedSpawnY.Value + size;
            if (spell.Combat.AoeShape == AoeShape.Rectangle)
            {
                var width = spell.Combat.AoeRectWidth - 1;
                var height = spell.Combat.AoeRectHeight - 1;
                // Check for swapping width/height if we're turned and relative dir is enabled
                if (spell.Combat.AoeRelativeOffset)
                {
                    var faceDir = (Directions)GetFaceDirection();
                    if (faceDir == Directions.Right || faceDir == Directions.Left)
                    {
                        var tmpWidth = width;
                        width = height;
                        height = tmpWidth;
                    }
                }

                // Rectangle positions are bottom-left oriented
                left = offsettedSpawnX.Value;
                right = offsettedSpawnX.Value + width;
                top = offsettedSpawnY.Value - height;
                bottom = offsettedSpawnY.Value;
            }
            
            GameTexture texture = null;

            Func<double, bool> inRange = new Func<double, bool>((double distance) =>
            {
                if (spell.Combat.TargetType == SpellTargetTypes.Single && spell.Combat.MinRange > 0)
                {
                    return Math.Floor(distance) <= size && Math.Floor(distance) > spell.Combat.MinRange;
                }
                return Math.Floor(distance) <= size;
            });

            if (targetType == SpellTargetTypes.AoE || targetType == SpellTargetTypes.Single)
            {
                if (targetType == SpellTargetTypes.AoE)
                {
                    if (friendly)
                    {
                        texture = spell.Combat.Friendly ? COMBAT_TILE_FRIENDLY : COMBAT_TILE_NEUTRAL;
                    }
                    else
                    {
                        texture = spell.Combat.Friendly ? COMBAT_TILE_NEUTRAL : COMBAT_TILE_AOE;
                    }
                }
                else
                {
                    if (friendly)
                    {
                        texture = spell.Combat.Friendly ? SINGLE_TARGET_FRIENDLY : SINGLE_TARGET_NEUTRAL;
                    }
                    else
                    {
                        texture = spell.Combat.Friendly ? SINGLE_TARGET_NEUTRAL : SINGLE_TARGET;
                    }
                }

                for (int y = top; y <= bottom; y++)
                {
                    for (int x = left; x <= right; x++)
                    {
                        var distanceFromCaster = MathHelper.CalculateDistanceToPoint(offsettedSpawnX.Value, offsettedSpawnY.Value, x, y);
                        if (spell.Combat.AoeShape == AoeShape.Circle)
                        {
                            if (!inRange(distanceFromCaster))
                            {
                                continue;
                            }
                        }

                        if (!MapInstance.TryGetMapInstanceFromCoords(offsettedSpawnMap.Id, x, y, out var currMap, out var mapX, out var mapY))
                        {
                            continue;
                        }

                        var tile = GetTileRectangle(currMap, (byte)mapX, (byte)mapY);
                        if (currMap.TileIsBlocking((byte)mapX, (byte)mapY))
                        {
                            continue;
                        }

                        if (texture == COMBAT_TILE_AOE || texture == SINGLE_TARGET || texture == SINGLE_TARGET_OUTLINE) // If we're drawing the "DANGER" texture, give it a light so we can see it in darkness
                        {
                            Graphics.AddLight((int)tile.CenterX, (int)tile.CenterY, 100, 200, 1.0f, new Color(255, 222, 124, 112));
                        }
                        Graphics.DrawGameTexture(
                            texture, new FloatRect(0, 0, texture.Width, texture.Height),
                            tile, new Color(AoeAlpha, 255, 255, 255)
                        );
                    }
                }
            }
            // Draw outlines
            if (spell.Combat.TargetType == SpellTargetTypes.Single)
            {
                if (friendly)
                {
                    texture = spell.Combat.Friendly ? SINGLE_TARGET_FRIENDLY_OUTLINE : SINGLE_TARGET_NEUTRAL_OUTLINE;
                }
                else
                {
                    texture = spell.Combat.Friendly ? SINGLE_TARGET_NEUTRAL_OUTLINE : SINGLE_TARGET_OUTLINE;
                }

                Edge[,] edges = new Edge[right - left + 1, bottom - top + 1];
                for (int y = top; y <= bottom; y++)
                {
                    for (int x = left; x <= right; x++)
                    {
                        var edgeX = x - left;
                        var edgeY = y - top;
                        edges[edgeX, edgeY] = new Edge();

                        var distanceFromCaster = MathHelper.CalculateDistanceToPoint(offsettedSpawnX.Value, offsettedSpawnY.Value, x, y);
                        if (!inRange(distanceFromCaster))
                        {
                            continue;
                        }

                        if (!MapInstance.TryGetMapInstanceFromCoords(offsettedSpawnMap.Id, x, y, out var currMap, out var mapX, out var mapY))
                        {
                            continue;
                        }

                        var tile = GetTileRectangle(currMap, (byte)mapX, (byte)mapY);
                        if (currMap.TileIsBlocking((byte)mapX, (byte)mapY))
                        {
                            continue;
                        }
                        edges[edgeX, edgeY].Tile = tile;

                        if (texture == SINGLE_TARGET) // If we're drawing the "DANGER" texture, give it a light so we can see it in darkness
                        {
                            Graphics.AddLight((int)tile.CenterX, (int)tile.CenterY, 100, 200, 1.0f, new Color(255, 222, 124, 112));
                        }

                        var distLeft = MathHelper.CalculateDistanceToPoint(spawnX, spawnY, x - 1, y);
                        var distRight = MathHelper.CalculateDistanceToPoint(spawnX, spawnY, x + 1, y);
                        var distUp = MathHelper.CalculateDistanceToPoint(spawnX, spawnY, x, y - 1);
                        var distDown = MathHelper.CalculateDistanceToPoint(spawnX, spawnY, x, y + 1);

                        edges[edgeX, edgeY].Left = x == left || !inRange(distLeft);
                        edges[edgeX, edgeY].Right = x == right || !inRange(distRight);
                        edges[edgeX, edgeY].Top = y == top || !inRange(distUp);
                        edges[edgeX, edgeY].Bottom = y == bottom || !inRange(distDown);
                    }
                }

                var segmentSize = 8;
                var tileSize = 16;
                for (int y = 0; y < edges.GetLength(1); y++)
                {
                    for (int x = 0; x < edges.GetLength(0); x++)
                    {
                        var edge = edges[x, y];
                        if (edge.Empty || edge.Tile == null)
                        {
                            continue;
                        }

                        if (edge.Left)
                        {
                            Graphics.DrawGameTexture(
                                texture, new FloatRect(0, segmentSize * 3, tileSize, tileSize),
                                edge.Tile.Value, new Color(AoeAlpha, 255, 255, 255)
                            );
                        }
                        if (edge.Right)
                        {
                            Graphics.DrawGameTexture(
                                texture, new FloatRect(segmentSize * 2, segmentSize * 3, tileSize, tileSize),
                                edge.Tile.Value, new Color(AoeAlpha, 255, 255, 255)
                            );
                        }
                        if (edge.Top)
                        {
                            Graphics.DrawGameTexture(
                                texture, new FloatRect(segmentSize * 1, segmentSize * 2, tileSize, tileSize),
                                edge.Tile.Value, new Color(AoeAlpha, 255, 255, 255)
                            );
                        }
                        if (edge.Bottom)
                        {
                            Graphics.DrawGameTexture(
                                texture, new FloatRect(segmentSize * 1, segmentSize * 4, tileSize, tileSize),
                                edge.Tile.Value, new Color(AoeAlpha, 255, 255, 255)
                            );
                        }

                        var tile = edge.Tile.Value;
                    }
                }
            }
        }

        struct Edge
        {
            public bool Left { get; set; }
            public bool Right { get; set; }
            public bool Top { get; set; }
            public bool Bottom { get; set; }
            public bool NW { get; set; }
            public bool NE { get; set; }
            public bool SW { get; set; }
            public bool SE { get; set; }
            public FloatRect? Tile { get; set; }

            public bool Empty => !Left && !Right && !Top && !Bottom;
        }

        public void DrawProjectileSpawns(SpellBase spell, MapInstance spawnMap, byte spawnX, byte spawnY, bool friendly)
        {
            if (!ShouldRenderMarkers(friendly))
            {
                return;
            }

            if (Timing.Global.Milliseconds > AoeAlphaUpdate)
            {
                AoeAlphaUpdate = Timing.Global.Milliseconds + AOE_UPDATE_MS;

                if (AoeAlpha <= MIN_AOE_ALPHA)
                {
                    AoeAlphaDir = 1;
                }
                else if (AoeAlpha >= MAX_AOE_ALPHA)
                {
                    AoeAlphaDir = -1;
                }
                AoeAlpha = MathHelper.Clamp(AoeAlpha + (AOE_UPDATE_AMT * AoeAlphaDir), MIN_AOE_ALPHA, MAX_AOE_ALPHA);
            }

            var dir = (byte)GetFaceDirection();

            // Get our information
            var projectile = spell.Combat.Projectile;
            var range = projectile.Range + 1;

            // Get the possible bounds for the projectile spawner
            int left = spawnX - (ProjectileBase.SPAWN_LOCATIONS_WIDTH / 2);
            int right = spawnX + (ProjectileBase.SPAWN_LOCATIONS_WIDTH / 2);
            int top = spawnY - (ProjectileBase.SPAWN_LOCATIONS_HEIGHT / 2);
            int bottom = spawnY + (ProjectileBase.SPAWN_LOCATIONS_HEIGHT / 2);

            // Loop through the possible projectile spawner locations
            var projectileX = 0;
            var projectileY = 0;

            List<FloatRect> tilesDrawn = new List<FloatRect>();

            // We have to rotate the spawn locations in accordance to the direction of the caster
            Location[,] rotatedLocations = (Location[,]) projectile.SpawnLocations.Clone();
            switch (dir)
            {
                // Left
                case 2:
                    rotatedLocations = MathHelper.RotateArray90CW(5, rotatedLocations);
                    rotatedLocations = MathHelper.RotateArray90CW(5, rotatedLocations);
                    rotatedLocations = MathHelper.RotateArray90CW(5, rotatedLocations);
                    break;
                // Down
                case 1:
                    rotatedLocations = MathHelper.RotateArray90CW(5, rotatedLocations);
                    rotatedLocations = MathHelper.RotateArray90CW(5, rotatedLocations);
                    break;
                // Right
                case 3:
                    rotatedLocations = MathHelper.RotateArray90CW(5, rotatedLocations);
                    break;
            }

            for (int y = top; y <= bottom; y++, projectileY++)
            {
                projectileX = 0;
                for (int x = left; x <= right; x++, projectileX++)
                {
                    var directions = rotatedLocations[projectileX, projectileY].Directions;
                    var totalDirections = directions.Count((dirVal) => dirVal == true);

                    // If this projectile spawn location doesn't have anything to show, don't bother
                    if (totalDirections == 0)
                    {
                        continue;
                    }
                    // Otherwise, draw 'em... first, get our spawner's tile
                    if (!MapInstance.TryGetMapInstanceFromCoords(CurrentMap, x, y, out var currMap, out var mapX, out var mapY))
                    {
                        continue;
                    }
                    // Then, for each direction, draw a tile for each in the range
                    for (var dirId = 0; dirId < directions.Length; dirId++)
                    {
                        if (!directions[dirId])
                        {
                            continue;
                        }
                        DrawProjectilePath(dir, dirId, range, (int)mapX, (int)mapY, currMap, ref tilesDrawn, friendly, spell.Combat.Friendly, projectile);
                    }
                }
            }
        }

        private void DrawProjectilePath(byte dir, int projectileDir, int range, int x, int y, MapInstance map, ref List<FloatRect> tilesDrawn, bool friendly, bool combatFriendly, ProjectileBase projectile)
        {
            var pathBroken = false;
            for (var pathIdx = 0; pathIdx < range; pathIdx++)
            {
                var right = x + pathIdx;
                var left = x - pathIdx;
                var up = y - pathIdx;
                var down = y + pathIdx;

                switch (projectileDir)
                {
                    // Up
                    case 0:
                        switch (dir)
                        {
                            case 0:
                                DrawProjectileTile(map.Id, PositionUtilities.RotateProjectileDir(dir, projectileDir), x, up, ref tilesDrawn, friendly, combatFriendly, projectile, out pathBroken);
                                break;
                            case 1:
                                DrawProjectileTile(map.Id, PositionUtilities.RotateProjectileDir(dir, projectileDir), x, down, ref tilesDrawn, friendly, combatFriendly, projectile, out pathBroken);
                                break;
                            case 2:
                                DrawProjectileTile(map.Id, PositionUtilities.RotateProjectileDir(dir, projectileDir), left, y, ref tilesDrawn, friendly, combatFriendly, projectile, out pathBroken);
                                break;
                            case 3:
                                DrawProjectileTile(map.Id, PositionUtilities.RotateProjectileDir(dir, projectileDir), right, y, ref tilesDrawn, friendly, combatFriendly, projectile, out pathBroken);
                                break;
                        }
                        break;
                    // Down
                    case 1:
                        switch (dir)
                        {
                            case 0:
                                DrawProjectileTile(map.Id, PositionUtilities.RotateProjectileDir(dir, projectileDir), x, down, ref tilesDrawn, friendly, combatFriendly, projectile, out pathBroken);
                                break;
                            case 1:
                                DrawProjectileTile(map.Id, PositionUtilities.RotateProjectileDir(dir, projectileDir), x, up, ref tilesDrawn, friendly, combatFriendly, projectile, out pathBroken);
                                break;
                            case 2:
                                DrawProjectileTile(map.Id, PositionUtilities.RotateProjectileDir(dir, projectileDir), right, y, ref tilesDrawn, friendly, combatFriendly, projectile, out pathBroken);
                                break;
                            case 3:
                                DrawProjectileTile(map.Id, PositionUtilities.RotateProjectileDir(dir, projectileDir), left, y, ref tilesDrawn, friendly, combatFriendly, projectile, out pathBroken);
                                break;
                        }
                        break;
                    // Left
                    case 2:
                        switch (dir)
                        {
                            case 0:
                                DrawProjectileTile(map.Id, PositionUtilities.RotateProjectileDir(dir, projectileDir), left, y, ref tilesDrawn, friendly, combatFriendly, projectile, out pathBroken);
                                break;
                            case 1:
                                DrawProjectileTile(map.Id, PositionUtilities.RotateProjectileDir(dir, projectileDir), right, y, ref tilesDrawn, friendly, combatFriendly, projectile, out pathBroken);
                                break;
                            case 2:
                                DrawProjectileTile(map.Id, PositionUtilities.RotateProjectileDir(dir, projectileDir), x, down, ref tilesDrawn, friendly, combatFriendly, projectile, out pathBroken);
                                break;
                            case 3:
                                DrawProjectileTile(map.Id, PositionUtilities.RotateProjectileDir(dir, projectileDir), x, up, ref tilesDrawn, friendly, combatFriendly, projectile, out pathBroken);
                                break;
                        }
                        break;
                    // Right
                    case 3:
                        switch (dir)
                        {
                            case 0:
                                DrawProjectileTile(map.Id, PositionUtilities.RotateProjectileDir(dir, projectileDir), right, y, ref tilesDrawn, friendly, combatFriendly, projectile, out pathBroken);
                                break;
                            case 1:
                                DrawProjectileTile(map.Id, PositionUtilities.RotateProjectileDir(dir, projectileDir), left, y, ref tilesDrawn, friendly, combatFriendly, projectile, out pathBroken);
                                break;
                            case 2:
                                DrawProjectileTile(map.Id, PositionUtilities.RotateProjectileDir(dir, projectileDir), x, up, ref tilesDrawn, friendly, combatFriendly, projectile, out pathBroken);
                                break;
                            case 3:
                                DrawProjectileTile(map.Id, PositionUtilities.RotateProjectileDir(dir, projectileDir), x, down, ref tilesDrawn, friendly, combatFriendly, projectile, out pathBroken);
                                break;
                        }
                        break;
                    // Top-left
                    case 4:
                        switch (dir)
                        {
                            case 0:
                                DrawProjectileTile(map.Id, PositionUtilities.RotateProjectileDir(dir, projectileDir), left, up, ref tilesDrawn, friendly, combatFriendly, projectile, out pathBroken);
                                break;
                            case 1:
                                DrawProjectileTile(map.Id, PositionUtilities.RotateProjectileDir(dir, projectileDir), right, down, ref tilesDrawn, friendly, combatFriendly, projectile, out pathBroken);
                                break;
                            case 2:
                                DrawProjectileTile(map.Id, PositionUtilities.RotateProjectileDir(dir, projectileDir), left, down, ref tilesDrawn, friendly, combatFriendly, projectile, out pathBroken);
                                break;
                            case 3:
                                DrawProjectileTile(map.Id, PositionUtilities.RotateProjectileDir(dir, projectileDir), right, up, ref tilesDrawn, friendly, combatFriendly, projectile, out pathBroken);
                                break;
                        }
                        break;
                    // Top-right
                    case 5:
                        switch (dir)
                        {
                            case 0:
                                DrawProjectileTile(map.Id, PositionUtilities.RotateProjectileDir(dir, projectileDir), right, up, ref tilesDrawn, friendly, combatFriendly, projectile, out pathBroken);
                                break;
                            case 1:
                                DrawProjectileTile(map.Id, PositionUtilities.RotateProjectileDir(dir, projectileDir), left, down, ref tilesDrawn, friendly, combatFriendly, projectile, out pathBroken);
                                break;
                            case 2:
                                DrawProjectileTile(map.Id, PositionUtilities.RotateProjectileDir(dir, projectileDir), left, up, ref tilesDrawn, friendly, combatFriendly, projectile, out pathBroken);
                                break;
                            case 3:
                                DrawProjectileTile(map.Id, PositionUtilities.RotateProjectileDir(dir, projectileDir), right, down, ref tilesDrawn, friendly, combatFriendly, projectile, out pathBroken);
                                break;
                        }
                        break;
                    // Bottom-left
                    case 6:
                        switch (dir)
                        {
                            case 0:
                                DrawProjectileTile(map.Id, PositionUtilities.RotateProjectileDir(dir, projectileDir), left, down, ref tilesDrawn, friendly, combatFriendly, projectile, out pathBroken);
                                break;
                            case 1:
                                DrawProjectileTile(map.Id, PositionUtilities.RotateProjectileDir(dir, projectileDir), right, up, ref tilesDrawn, friendly, combatFriendly, projectile, out pathBroken);
                                break;
                            case 2:
                                DrawProjectileTile(map.Id, PositionUtilities.RotateProjectileDir(dir, projectileDir), right, down, ref tilesDrawn, friendly, combatFriendly, projectile, out pathBroken);
                                break;
                            case 3:
                                DrawProjectileTile(map.Id, PositionUtilities.RotateProjectileDir(dir, projectileDir), left, up, ref tilesDrawn, friendly, combatFriendly, projectile, out pathBroken);
                                break;
                        }
                        break;
                    // Bottom-right
                    case 7:
                        switch (dir)
                        {
                            case 0:
                                DrawProjectileTile(map.Id, PositionUtilities.RotateProjectileDir(dir, projectileDir), right, down, ref tilesDrawn, friendly, combatFriendly, projectile, out pathBroken);
                                break;
                            case 1:
                                DrawProjectileTile(map.Id, PositionUtilities.RotateProjectileDir(dir, projectileDir), left, up, ref tilesDrawn, friendly, combatFriendly, projectile, out pathBroken);
                                break;
                            case 2:
                                DrawProjectileTile(map.Id, PositionUtilities.RotateProjectileDir(dir, projectileDir), right, up, ref tilesDrawn, friendly, combatFriendly, projectile, out pathBroken);
                                break;
                            case 3:
                                DrawProjectileTile(map.Id, PositionUtilities.RotateProjectileDir(dir, projectileDir), left, down, ref tilesDrawn, friendly, combatFriendly, projectile, out pathBroken);
                                break;
                        }
                        break;
                }

                if (pathBroken)
                {
                    return;
                }
            }
        }

        private void DrawProjectileTile(Guid mapId, int dirId, int x, int y, ref List<FloatRect> tilesDrawn, bool friendly, bool combatFriendly, ProjectileBase projectile, out bool pathBroken)
        {
            pathBroken = false;

            if (!MapInstance.TryGetMapInstanceFromCoords(mapId, x, y, out var currMap, out var mapX, out var mapY) || projectile == null)
            {
                return;
            }

            if (currMap.TileIsBlocking((byte)mapX, (byte)mapY, projectile.Grounded) && !projectile.IgnoreMapBlocks)
            {
                pathBroken = true;
                return;
            }

            var tile = GetTileRectangle(currMap, (byte)mapX, (byte)mapY);

            // Add a light if we haven't previously drawn on this tile & it's hostile
            if (!tilesDrawn.Any(t => t.X == tile.X && t.Y == tile.Y) && !friendly && !combatFriendly)
            {
                Graphics.AddLight((int)tile.CenterX, (int)tile.CenterY, 100, 200, 1.0f, new Color(255, 222, 124, 112));
            }
            tilesDrawn.Add(tile);

            var srcRect = GetProjectileTextureSource(dirId, friendly, combatFriendly);

            Graphics.DrawGameTexture(
                PROJECTILE_TEXTURES, srcRect,
                tile, new Color(AoeAlpha, 255, 255, 255)
            );
        }

        private FloatRect GetProjectileTextureSource(int dirId, bool friendly, bool combatFriendly)
        {
            var srcX = dirId * 16;
            var srcY = 0;

            if (friendly)
            {
                srcY = combatFriendly ? 32 : 16;
            }
            else if (combatFriendly)
            {
                srcY = 16;
            }

            var width = PROJECTILE_TEXTURES.Width / 8; // There are 8 projectile directions
            var height = PROJECTILE_TEXTURES.Height / 3; // There are 3 potential styles to draw (hostile, neutral, friendly)
            return new FloatRect(srcX, srcY, width, height);
        }

        private void DrawSpellIcon(int x, int y, string icon, Color color)
        {
            if (PvpHide)
            {
                return;
            }

            var backgroundTex = Globals.ContentManager.GetTexture(TextureType.Misc, "spellcast.png");
            var texture = Globals.ContentManager.GetTexture(TextureType.Spell, icon);
            var iconWidth = 40;
            var iconHeight = 40;

            var iconX = x - iconWidth * 2;
            var iconY = y - iconHeight / 2;

            // Draw BG
            Graphics.DrawGameTexture(
                backgroundTex, new FloatRect(0, 0, backgroundTex.GetWidth(), backgroundTex.GetHeight()),
                new FloatRect(iconX, iconY, iconWidth, iconHeight), color
            );

            Graphics.DrawGameTexture(
                texture, new FloatRect(0, 0, texture.GetWidth(), texture.GetHeight()),
                new FloatRect(iconX + 4, iconY + 4, 32, 32), color
            );
        }

        public void DrawStatuses()
        {
            if (Texture == null || (IsStealthed() && !IsAllyOf(Globals.Me)) || PvpHide)
            {
                return;
            }

            // Don't show if non-ally in PvP
            if (Globals.Me.MapInstance.ZoneType != MapZones.Safe || !(this is Player player))
            {
                if (IsStealthed() && !IsAllyOf(Globals.Me))
                {
                    return;
                }
            }
            else
            {
                // Don't show if not in party in PvE
                if (IsStealthed() && Id != Globals.Me.Id && !Globals.Me.IsInMyParty(player))
                {
                    return;
                }
            }

            if (!Globals.Database.DisplaySelfStatusMarkers && Id == Globals.Me.Id)
            {
                return;
            }

            if (!Globals.Database.DisplayStatusMarkers && Id != Globals.Me.Id)
            {
                return;
            }

            List<GameTexture> statusTextures = new List<GameTexture>();
            foreach(StatusTypes status in Enum.GetValues(typeof(StatusTypes)))
            {
                if (!StatusActive(status))
                {
                    continue;
                }

                var texture = GetStatusTexture(status);
                if (texture == null)
                {
                    continue;
                }

                statusTextures.Add(texture);
            }

            // Fetch DoT/HoT and the like
            foreach(Status status in Status)
            {
                if (status.Type != StatusTypes.None || status.SpellId == default || status.SpellId.ToString() == Options.Combat.InspiredSpellId)
                {
                    continue;
                }

                var spell = SpellBase.Get(status.SpellId);

                var texture = Globals.ContentManager.GetTexture(TextureType.Spell, spell.Icon);

                if (texture == null)
                {
                    continue;
                }

                statusTextures.Add(texture);
            }

            // We need to know our index of each status we're drawing to position it appropriately
            for (var txtIdx = 0; txtIdx < statusTextures.Count; txtIdx++)
            {
                var width = 16;
                var height = 16;
                var currTexture = statusTextures[txtIdx];
                var texturesRemaining = statusTextures.Count - (txtIdx + 1);
                var xPadding = 4;
                var yPadding = 2;
                var textureSpace = xPadding + width;

                // Initialize X to be centered with the entity
                var center = (int)Math.Ceiling(GetCenterPos().X) - (width / 2);
                var x = center;
                if (statusTextures.Count % 2 == 0)
                {
                    var texturesPerSide = statusTextures.Count / 2;
                    // Draw to the left of center
                    if (txtIdx + 1 <= texturesPerSide)
                    {
                        // move our center point, as we have an even number of statuses to display
                        center -= (textureSpace / 2);
                        var leftIdx = texturesPerSide - txtIdx - 1;
                        x = center - (textureSpace * leftIdx);
                    }
                    // Draw to the right of center
                    else
                    {
                        // move our center point, as we have an even number of statuses to display
                        center += (textureSpace / 2);
                        var rightIdx = (txtIdx) - texturesPerSide;
                        x = center + (textureSpace * rightIdx);
                    }
                }
                else
                {
                    // If we are the median index for an odd-number amount of statuses, don't mess with x - it's already centered
                    var median = (statusTextures.Count + 1) / 2;
                    var texturesPerSide = median - 1;
                    // Draw to the left of center
                    if (txtIdx < median)
                    {
                        var leftIdx = texturesPerSide - txtIdx;
                        x = center - (textureSpace * leftIdx);
                    }
                    // Draw to the right of center
                    else
                    {
                        var rightIdx = txtIdx - texturesPerSide;
                        x = textureSpace * rightIdx + center;
                    }
                }

                var y = GetCenterPos().Y + 48;

                var srcRectangle = new FloatRect(0, 0, currTexture.GetWidth(), currTexture.GetHeight());
                var location = new FloatRect(x, y, height, width);

                Graphics.DrawGameTexture(currTexture, srcRectangle, location, Color.White);
            }
        }

        private GameTexture GetStatusTexture(StatusTypes status)
        {
            var textureName = $"status_{Enum.GetName(typeof(StatusTypes), status)}.png";
            return Globals.ContentManager.GetTexture(TextureType.Misc, textureName);
        }

        public void ShowActionMessage(string text, Color color, bool stationary)
        {
            if (CurrentMap == default)
            {
                return;
            }

            var map = MapInstance.Get(CurrentMap);
            if (map == default)
            {
                return;
            }

            map.ActionMsgs.Add(new ActionMessage(map, X, Y, text, color, stationary));
        }
    }

    public partial class Entity
    {
        public bool FadeName = false;

        public int NameOpacity = 0;

        public long LastNameUpdate = 0L;

        public long GetCastStart() 
        {
            var now = Timing.Global.Milliseconds;
            if (SpellCast == default)
            {
                return now;
            }

            return CastTime - SpellBase.Get(SpellCast).CastDuration;
        }

        public Guid NpcId;

        public virtual void SetSpellCast(SpellBase spell, Guid targetId)
        {
            if (spell == null)
            {
                return;
            }

            if (spell.CastDuration == 0)
            {
                return;
            }
            CastTime = Timing.Global.Milliseconds + spell.CastDuration;
            SpellCast = spell.Id;
            
            if (spell.Combat.TargetType == SpellTargetTypes.Single)
            {
                EntityTarget = targetId;
            }
        }

        public bool InNameProximity()
        {
            if (!Globals.Database.NameFading)
            {
                return true;
            }

            var mousePos = Graphics.ConvertToWorldPoint(Globals.InputManager.GetMousePosition());
            /*return CalculateTileDistanceTo(Globals.Me) <= ClientConfiguration.Instance.ProximityNameDistance // AV - Commented this out to use magic number instead tee-hee
                || WorldPos.Contains(mousePos.X, mousePos.Y) 
                || IsTargeted 
                || !IsAllyOf(Globals.Me) 
                || this is Player && (Globals.Me.MapInstance?.ZoneType != MapZones.Safe);*/
            return CalculateTileDistanceTo(Globals.Me) <= 6 
                || WorldPos.Contains(mousePos.X, mousePos.Y) 
                || IsTargeted 
                || !IsAllyOf(Globals.Me) 
                || this is Player && (Globals.Me.MapInstance?.ZoneType != MapZones.Safe);
        }

        protected void NameOpacityUpdate()
        {
            if (!InNameProximity())
            {
                FadeName = true;
            }
            else
            {
                FadeName = false;
            }

            //var minOpacity = ClientConfiguration.Instance.MinimumNameOpacity; FOR NOW
            var minOpacity = 0;
            if (Graphics.BrightnessLevel == 0)
            {
                minOpacity = 0;
            }

            if (NameOpacity == minOpacity && FadeName)
            {
                return;
            }

            if (Timing.Global.MillisecondsUtcUnsynced - LastNameUpdate > 80)
            {
                if (FadeName)
                {
                    NameOpacity -= 50;
                }
                else
                {
                    NameOpacity += 50;
                }

                var maxValue = (Globals.Me?.CombatMode ?? false) ? 150 : byte.MaxValue;

                NameOpacity = MathHelper.Clamp(NameOpacity, minOpacity, maxValue);
                LastNameUpdate = Timing.Global.MillisecondsUtcUnsynced;
            }
        }

        protected virtual bool ShouldDrawFlair()
        {
            var npc = NpcBase.Get(NpcId);
            if (npc == default || !npc.IsChampion)
            {
                return false;
            }

            return true;
        }

        protected virtual void DrawFlair(int x, int y)
        {
            if (!ShouldDrawFlair())
            {
                return;
            }

            var championTexture = Globals.ContentManager.GetTexture(TextureType.Misc, "champion_medal.png");
            if (championTexture == default)
            {
                return;
            }
            Graphics.DrawGameTexture(championTexture,
                new FloatRect(0, 0, championTexture.Width, championTexture.Height),
                new FloatRect(x, y, championTexture.Width * Options.Scale, championTexture.Width * Options.Scale),
                Color.White);
        }

        public bool InPvpSight => Globals.Me?.CalculateTileDistanceTo(this) < 17;

        /// <summary>
        /// Caclulate crit chance based on the player's current affinity
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="effect"></param>
        /// <returns></returns>
        public virtual int ApplyBonusEffect(int amount, EffectType effect, bool subtractive = false)
        {
            return amount;
        }

        /// <summary>
        /// Gets the value of a bonus effect as granted by the currently equipped gear.
        /// </summary>
        /// <param name="effect">The <see cref="EffectType"/> to retrieve the amount for.</param>
        /// <param name="startValue">The starting value to which we're adding our gear amount.</param>
        /// <returns></returns>
        public virtual int GetBonusEffect(EffectType effect, int startValue = 0)
        {
            return 0;
        }

        public virtual Dictionary<EffectType, int> GetAllBonusEffects()
        {
            return new Dictionary<EffectType, int>();
        }
    }
}