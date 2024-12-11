using System;
using System.Linq;
using System.Collections.Generic;

using Intersect.Client.Core;
using Intersect.Client.Core.Controls;
using Intersect.Client.Entities.Events;
using Intersect.Client.Entities.Projectiles;
using Intersect.Client.General;
using Intersect.Client.Interface.Game;
using Intersect.Client.Interface.Game.EntityPanel;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Localization;
using Intersect.Client.Maps;
using Intersect.Client.Networking;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.Maps;
using Intersect.Network.Packets.Server;

using Newtonsoft.Json;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Utilities;
using Intersect.Client.Items;
using Intersect.Client.Interface.Game.Chat;
using Intersect.Config.Guilds;
using Intersect.Client.Interface.Game.DescriptionWindows;
using Intersect.GameObjects.Crafting;
using Intersect.Client.General.Leaderboards;
using Intersect.Localization;
using Intersect.GameObjects.Events;
using Microsoft.Xna.Framework.Graphics;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intersect.Client.Entities
{

    public partial class Player : Entity
    {

        public delegate void InventoryUpdated();

        public Guid Class;

        public long Experience = 0;

        public long ExperienceToNextLevel = 0;

        public long WeaponExp = 0;

        public long WeaponExpTnl = 0;

        public int TrackedWeaponLevel = 0;

        public Guid TrackedWeaponTypeId = Guid.Empty;

        public long LastAttemptedCast = 0L;

        public List<FriendInstance> Friends = new List<FriendInstance>();

        public HotbarInstance[] Hotbar = new HotbarInstance[Options.MaxHotbar];

        public InventoryUpdated InventoryUpdatedDelegate;

        public Dictionary<Guid, long> ItemCooldowns = new Dictionary<Guid, long>();

        private Entity mLastBumpedEvent = null;

        private List<PartyMember> mParty;

        public Dictionary<Guid, QuestProgress> QuestProgress = new Dictionary<Guid, QuestProgress>();

        public Guid[] HiddenQuests = new Guid[0];

        public Dictionary<Guid, long> SpellCooldowns = new Dictionary<Guid, long>();

        public int StatPoints = 0;

        public EntityBox TargetBox;

        public Guid TargetIndex;

        public int TargetType;

        public bool DirKeyPressed = false;

        public long[] MoveDirectionTimers = new long[4];

        public bool HPWarning = false;

        public bool MPWarning = false;

        public bool UnspentPointsWarning = false;

        public bool ResourceLocked = false;
        
        public double CurrentHarvestBonus = 0.0f;
        
        public int HarvestsRemaining = 0;
        public Guid HarvestingResource = Guid.Empty;

        public long CombatTimer { get; set; }

        public int ComboExp { get; set; }

        public int CurrentCombo { get; set; }

        public long MaxComboWindow { get; set; }

        public long LastComboUpdate { get; set; }

        public string MiningTier { get; set; }

        public string FishingTier { get; set; }

        public string WoodcutTier { get; set; }

        public Dictionary<string, int> ClassRanks { get; set; } 

        public string QuestPoints { get; set; }

        public string LifetimeQuestPoints { get; set; }

        private long mLastSpellCastMessageSent = 0L;

        // Used for doing smart direction changing if requesting to face a target
        public long SmartDirTime { get; set; }

        // Target data
        private long mlastTargetScanTime = 0;

        Guid mlastTargetScanMap = Guid.Empty;

        Point mlastTargetScanLocation = new Point(-1, -1);

        Dictionary<Entity, TargetInfo> mlastTargetList = new Dictionary<Entity, TargetInfo>(); // Entity, Last Time Selected

        Entity mLastEntitySelected = null;

        private Dictionary<int, long> mLastHotbarUseTime = new Dictionary<int, long>();
        private int mHotbarUseDelay = 150;

        /// <summary>
        /// Name of our guild if we are in one.
        /// </summary>
        public string Guild;

        /// <summary>
        /// Index of our rank where 0 is the leader
        /// </summary>
        public int Rank;

        /// <summary>
        /// Returns whether or not we are in a guild by checking to see if we are assigned a guild name
        /// </summary>
        public bool InGuild => !string.IsNullOrWhiteSpace(Guild);

        /// <summary>
        /// Obtains our rank and permissions from the game config
        /// </summary>
        public GuildRank GuildRank => InGuild ? Options.Instance.Guild.Ranks[Math.Max(0, Math.Min(this.Rank, Options.Instance.Guild.Ranks.Length - 1))] : null;

        /// <summary>
        /// Contains a record of all members of this player's guild.
        /// </summary>
        public GuildMember[] GuildMembers = new GuildMember[0];

        public bool InVehicle = false;

        public string VehicleSprite = string.Empty;
        
        public long VehicleSpeed = 0L;

        public long LastProjectileCastTime = 0L;

        public byte DungeonLives = 0;

        public bool InDungeon = false;

        public bool CombatMode = false;

        public byte FaceDirection = 0;

        public byte RelevantDir => CombatMode ? FaceDirection : Dir;

        public Player(Guid id, PlayerEntityPacket packet) : base(id, packet)
        {
            for (var i = 0; i < Options.MaxHotbar; i++)
            {
                Hotbar[i] = new HotbarInstance();
            }

            mRenderPriority = 2;
        }

        public List<PartyMember> Party
        {
            get
            {
                if (mParty == null)
                {
                    mParty = new List<PartyMember>();
                }

                return mParty;
            }
        }

        public override Guid CurrentMap
        {
            get => base.CurrentMap;
            set
            {
                if (value != base.CurrentMap)
                {
                    var oldMap = MapInstance.Get(base.CurrentMap);
                    var newMap = MapInstance.Get(value);
                    base.CurrentMap = value;
                    if (Globals.Me == this)
                    {
                        if (MapInstance.Get(Globals.Me.CurrentMap) != null)
                        {
                            Audio.PlayMusic(MapInstance.Get(Globals.Me.CurrentMap).Music, 6f, 10f, true);
                        }

                        if (newMap != null && oldMap != null)
                        {
                            newMap.CompareEffects(oldMap);
                        }
                    }
                }
            }
        }

        public bool IsInParty()
        {
            return Party.Count > 0;
        }

        public bool IsInMyParty(Player player) => IsInMyParty(player.Id);

        public bool IsInMyParty(Guid id) => Party.Any(member => member.Id == id);

        public bool InCutscene()
        {
            return (Globals.EventHolds.Count > 0 && Globals.EventHoldWidescreenTime < Timing.Global.Milliseconds) || Globals.MoveRouteActive || IsDead;
        }

        public bool IsBusy()
        {
            return !(Globals.EventHolds.Count == 0 &&
                     !Globals.MoveRouteActive &&
                     Globals.GameShop == null &&
                     Globals.InBank == false &&
                     Globals.InCraft == false &&
                     Globals.InQuestBoard == false &&
                     Globals.InTrade == false &&
                     Globals.InMapTransition == false &&
                     (!Interface.Interface.GameUi?.Map?.IsOpen ?? true) &&
                     (!Globals.Me.Leaderboard?.IsOpen ?? true) &&
                     (!Globals.Me.Deconstructor?.IsOpen ?? true) &&
                     (!Globals.Me.Enhancement?.IsOpen ?? true) &&
                     (!Globals.Me.UpgradeStation?.IsOpen ?? true) &&
                     (!Interface.Interface.GameUi?.WeaponPickerOpen ?? true) &&
                     !Interface.Interface.HasInputFocus());
        }

        public bool CanHarvest()
        {
            return (Globals.EventHolds.Count == 0 &&
                     !Globals.MoveRouteActive &&
                     !IsDead &&
                     Globals.GameShop == null);
        }

        public override bool Update()
        {
            if (Globals.Me == this)
            {
                HandleInput();
            }

            if (!IsMoving && ChangeCombatModeNextTile)
            {
                ChangeCombatModeNextTile = false;
                ToggleCombatMode(true);
            }

            if (!IsBusy())
            {
                // Combat mode direction processing
                if (this == Globals.Me && CombatMode && !IsStunned())
                {
                    var prevFace = FaceDirection;
                    FaceDirection = GetDirectionFromMouse(WorldPos);
                    if (FaceDirection != prevFace)
                    {
                        PacketSender.SendDirection(FaceDirection);
                    }
                }

                if (this == Globals.Me && IsMoving == false)
                {
                    ProcessDirectionalInput();
                }

                if (Controls.KeyDown(Control.AttackInteract) || ResourceLocked)
                {
                    if (StatusActive(StatusTypes.Stun) || StatusActive(StatusTypes.Sleep))
                    {
                        SendStunAlerts(true);
                    }
                    else if (!Globals.Me.TryAttack())
                    {
                        UpdateAttackTimer();
                    }
                }
            } else if (CanHarvest() && ResourceLocked) // Allow resource locking to persist in more situations than attacking
            {
                if (StatusActive(StatusTypes.Stun) || StatusActive(StatusTypes.Sleep))
                {
                    SendStunAlerts(true);
                }
                if (!Globals.Me.TryAttack())
                {
                    UpdateAttackTimer();
                }
            } else
            {
                CombatMode = false;
            }

            if (InVehicle && CombatMode)
            {
                CombatMode = false;
            }

            if (TargetBox != null)
            {
                TargetBox.Update();
            }
            else if (this == Globals.Me && TargetBox == null && Interface.Interface.GameUi != null)
            {
                // If for WHATEVER reason the box hasn't been created, create it.
                TargetBox = new EntityBox(Interface.Interface.GameUi.GameCanvas, EntityTypes.Player, null);
                TargetBox.Hide();
            }

            // Hide our Guild window if we're not in a guild!
            if (this == Globals.Me && string.IsNullOrEmpty(Guild) && Interface.Interface.GameUi != null)
            {
                Interface.Interface.GameUi.HideGuildWindow();
            }

            bool wasMoving = IsMoving;
            bool smartDirPossible = false;
            if (SmartDirTime > Timing.Global.Milliseconds) // If we're still in the potential zone for a smart dir change
            {
                smartDirPossible = true;
            }

            var returnval = base.Update(); // Will update IsMoving

            if (smartDirPossible)
            {
                // Make sure the player would potentially WANT to turn
                if (!IsMoving && wasMoving != IsMoving && !IsBusy() && noDirectionalInputPressed())
                {
                    TryFaceTarget(true);
                }
            }

            if (DirRequestTimes.Count > 0 && DirRequestTimes.Peek() + Options.Instance.PlayerOpts.DirectionChangeLimiter < Timing.Global.Milliseconds)
            {
                PacketSender.SendDirection(RelevantDir, true);
            }

            return returnval;
        }

        public void UpdateAttackTimer()
        {
            if (Globals.Me.AttackTimer < Timing.Global.Ticks / TimeSpan.TicksPerMillisecond)
            {
                Globals.Me.AttackTimer = Timing.Global.Ticks / TimeSpan.TicksPerMillisecond + Globals.Me.CalculateAttackTime();
            }
        }

        //Loading
        public override void Load(EntityPacket packet)
        {
            base.Load(packet);
            var pkt = (PlayerEntityPacket) packet;
            Gender = pkt.Gender;
            Class = pkt.ClassId;
            Type = pkt.AccessLevel;
            CombatTimer = pkt.CombatTimeRemaining + Timing.Global.Milliseconds;
            Guild = pkt.Guild;
            Rank = pkt.GuildRank;
            InVehicle = pkt.InVehicle;
            VehicleSprite = pkt.VehicleSprite;
            VehicleSpeed = pkt.VehicleSpeed;
            TrueStats = pkt.TrueStats;
            ScaledTo = pkt.ScaledTo;
            IsScaledDown = pkt.IsScaledDown;
            MapType = pkt.MapType;
            ClanWarWinner = pkt.ClanWarWinner;

            if (pkt.Equipment != null)
            {
                MyDecors = pkt.Equipment.Decor;
                if (this == Globals.Me && pkt.Equipment.InventorySlots != null)
                {
                    MyEquipment = pkt.Equipment.InventorySlots;
                }
                else if (pkt.Equipment.ItemIds != null)
                {
                    Equipment = pkt.Equipment.ItemIds;
                }
                Cosmetics = pkt.Equipment.CosmeticItemIds;
            }

            if (this == Globals.Me && TargetBox == null && Interface.Interface.GameUi != null)
            {
                TargetBox = new EntityBox(Interface.Interface.GameUi.GameCanvas, EntityTypes.Player, null);
                TargetBox.Hide();
            }

            if (this == Globals.Me)
            {
                Globals.CanEarnWeaponExp = Globals.Me?.CanEarnWeaponExp(Globals.Me.TrackedWeaponTypeId, Globals.Me.TrackedWeaponLevel) ?? false;
            }
        }

        public override EntityTypes GetEntityType()
        {
            return EntityTypes.Player;
        }

        //Item Processing
        public void SwapItems(int Label, int Color)
        {
            var tmpInstance = Inventory[Color].Clone();
            Inventory[Color] = Inventory[Label].Clone();
            Inventory[Label] = tmpInstance.Clone();
        }

        // Alex: The two default values here can be set to FALSE if we want to drop through to "DROP" functionality - but this means we won't ever see the "cannot destroy message"
        public void TryDropItem(int index, bool fromPacket = true, bool canDestroy = true)
        {
            var item = Inventory[index];
            if (item == null || item.Base == null)
            {
                return;
            }

            var destroyable = Inventory[index].Base.CanDestroy;

            if (destroyable)
            {
                if (!fromPacket)
                {
                    PacketSender.SendDestroyItem(index, true);
                    return;
                }
                if (canDestroy)
                {
                    if (item.Quantity > 1)
                    {
                        _ = new InputBox(
                            Strings.Inventory.destroyitem,
                            Strings.Inventory.destroyitemprompt.ToString(ItemBase.Get(Inventory[index].ItemId).Name), true,
                            InputBox.InputType.NumericInput, DestroyItemInputBoxOkay, null, index, Inventory[index].Quantity
                        );
                        return;
                    }

                    _ = new InputBox(
                        Strings.Inventory.destroyitem,
                        Strings.Inventory.destroyprompt.ToString(ItemBase.Get(Inventory[index].ItemId).Name), true,
                        InputBox.InputType.YesNo, DestroyInputBoxOkay, null, index
                    );
                };
            }
            else
            {
                ManageItem(
                    PacketSender.SendDropItem,
                    PacketSender.SendDropItems,
                    index,
                    Strings.Inventory.dropitem,
                    Strings.Inventory.dropprompt,
                    Strings.Inventory.dropitemprompt,
                    !item.Base.CanDrop,
                    Strings.Inventory.dropitem, Strings.Inventory.cannotdrop
                );
            }
        }

        private void DestroyItemInputBoxOkay(object sender, EventArgs e)
        {
            var value = (int)((InputBox)sender).Value;
            if (value > 0)
            {
                PacketSender.SendDestroyItem((int)((InputBox)sender).UserData, false, value);
            }
        }

        private void DestroyInputBoxOkay(object sender, EventArgs e)
        {
            PacketSender.SendDestroyItem((int)((InputBox)sender).UserData, false, 1);
        }

        public int FindItem(Guid itemId, int itemVal = 1)
        {
            for (var i = 0; i < Options.MaxInvItems; i++)
            {
                if (Inventory[i].ItemId == itemId && Inventory[i].Quantity >= itemVal)
                {
                    return i;
                }
            }

            return -1;
        }

        public void TryUseItem(int index)
        {
            if (IsDead)
            {
                return;
            }

            if (StatusActive(StatusTypes.Sleep))
            {
                SendAlert(Strings.Spells.sleep, Strings.Combat.sleep);
                return;
            }

            if (Globals.GameShop == null && Globals.InBank == false && Globals.InQuestBoard == false && Globals.InTrade == false && !ItemOnCd(index) &&
                index >= 0 && index < Globals.Me.Inventory.Length && Globals.Me.Inventory[index]?.Quantity > 0)
            {
                PacketSender.SendUseItem(index, TargetIndex);
            }
        }

        public long GetItemCooldown(Guid id)
        {
            if (ItemCooldowns.ContainsKey(id))
            {
                return ItemCooldowns[id];
            }

            return 0;
        }

        // TODO this needs re-worked now that items have more going on. See Equals override of ItemProperties class
        public int FindHotbarItem(HotbarInstance hotbarInstance)
        {
            var bestMatch = -1;

            if (hotbarInstance.ItemOrSpellId != Guid.Empty)
            {
                for (var i = 0; i < Inventory.Length; i++)
                {
                    var itm = Inventory[i];
                    if (itm != null && itm.ItemId == hotbarInstance.ItemOrSpellId)
                    {
                        bestMatch = i;
                        var itemBase = ItemBase.Get(itm.ItemId);
                        if (itemBase != null)
                        {
                            if (itemBase.ItemType == ItemTypes.Bag)
                            {
                                if (hotbarInstance.BagId == itm.BagId)
                                {
                                    break;
                                }
                            }
                            else if (itemBase.ItemType == ItemTypes.Equipment)
                            {
                                if (hotbarInstance.PreferredStatBuffs != null)
                                {
                                    var statMatch = true;
                                    for (var s = 0; s < hotbarInstance.PreferredStatBuffs.Length; s++)
                                    {
                                        if (itm.ItemProperties.StatModifiers[s] != hotbarInstance.PreferredStatBuffs[s])
                                        {
                                            statMatch = false;
                                        }
                                    }

                                    if (statMatch)
                                    {
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }

            return bestMatch;
        }

        public bool IsEquipped(int slot)
        {
            for (var i = 0; i < Options.EquipmentSlots.Count; i++)
            {
                if (MyEquipment[i] == slot)
                {
                    return true;
                }
            }

            return false;
        }

        public bool ItemOnCd(int slot)
        {
            if (Inventory[slot] != null)
            {
                var itm = Inventory[slot];
                if (itm.ItemId != Guid.Empty)
                {
                    if (ItemCooldowns.ContainsKey(itm.ItemId) && ItemCooldowns[itm.ItemId] > Timing.Global.Milliseconds)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public long ItemCdRemainder(int slot)
        {
            if (Inventory[slot] != null)
            {
                var itm = Inventory[slot];
                if (itm.ItemId != Guid.Empty)
                {
                    if (ItemCooldowns.ContainsKey(itm.ItemId) && ItemCooldowns[itm.ItemId] > Timing.Global.Milliseconds)
                    {
                        return ItemCooldowns[itm.ItemId] - Timing.Global.Milliseconds;
                    }
                }
            }

            return 0;
        }
        
        public void TrySellItem(int index)
        {
            ManageItem(
                PacketSender.SendSellItem,
                PacketSender.SendSellManyNonstackable,
                index,
                Strings.Shop.sellitem,
                Strings.Shop.sellprompt,
                Strings.Shop.sellitemprompt,
                !Globals.GameShop.BuysItem(ItemBase.Get(Inventory[index].ItemId)),
                Strings.Shop.sellitem, Strings.Shop.cannotsell
            );
        }

        private EventHandler HandleStackableSlotAction(Action<int, int> slotAction, Action<int[], int> multiSlotAction)
        {
            return (sender, e) =>
            {
                var value = (int)((InputBox)sender).Value;
                var ogSlotAndSlots = (KeyValuePair<int, int[]>)((InputBox)sender).UserData;
                if (value == 0)
                {
                    return;
                }

                var slot = ogSlotAndSlots.Key;
                var slots = ogSlotAndSlots.Value;

                var item = Inventory[slot];
                if (item == null)
                {
                    return;
                }

                if (item.Quantity < value)
                {
                    multiSlotAction.Invoke(slots, value);
                }
                else
                {
                    slotAction.Invoke(slot, value);
                }
            };
        }

        private EventHandler HandleNonstackableSlotAction(Action<int, int> slotAction)
        {
            return (sender, e) =>
            {
                var slot = (int)((InputBox)sender).UserData;
                slotAction.Invoke(slot, 1);
            };
        }

        private EventHandler HandleNonstackableMultislotAction(Action<int, int> slotAction, Action<int[], int> multiSlotAction)
        {
            return (sender, e) =>
            {
                var value = (int)((InputBox)sender).Value;

                var ogSlotAndSlots = (KeyValuePair<int, int[]>)((InputBox)sender).UserData;
                if (value <= 0)
                {
                    return;
                }

                if (value == 1)
                {
                    slotAction.Invoke(ogSlotAndSlots.Key, 1);
                }
                else
                {
                    multiSlotAction.Invoke(ogSlotAndSlots.Value, value);
                }
            };
        }

        private void ManageItem(Action<int, int> slotAction,
           Action<int[], int> multiSlotAction,
           int selectedSlot,
           LocalizedString promptTitle,
           LocalizedString singlePromptText,
           LocalizedString manyPromptText,
           bool alwaysQuickSingle = false)
        {
            if (IsDead)
            {
                return;
            }

            var invItem = Inventory[selectedSlot];
            if (invItem == null || invItem.Base == null)
            {
                return;
            }

            var itemName = invItem.Base.Name;

            var stackable = invItem.Base.Stackable;
            var selectedQuantity = stackable ? invItem.Quantity : 1;

            // If shift held, do a quick-action (no prompt)
            if (TryQuickManage(slotAction, selectedSlot, selectedQuantity))
            {
                return;
            }

            var slots = GetSlotsContainingItem(invItem.Base.Id);

            // Otherwise, prepare prompts
            if (stackable)
            {
                var totalItems = GetInventoryItemsAndQuantities()
                    .Where(kv => kv.Key == invItem.Base.Id)
                    .Aggregate(0, (prev, next) => prev + next.Value);

                if (totalItems > 1)
                {
                    _ = new InputBox(
                        promptTitle,
                        manyPromptText.ToString(itemName), true,
                        InputBox.InputType.NumericInput, HandleStackableSlotAction(slotAction, multiSlotAction), null, new KeyValuePair<int, int[]>(selectedSlot, slots), totalItems
                    );
                }
                else
                {
                    if (alwaysQuickSingle)
                    {
                        QuickManage(slotAction, selectedSlot, selectedQuantity);
                        return;
                    }

                    _ = new InputBox(
                       promptTitle,
                       singlePromptText.ToString(itemName), true,
                       InputBox.InputType.YesNo, HandleNonstackableSlotAction(slotAction), null, selectedSlot
                    );
                }
            }
            else
            {
                if (slots.Length == 1)
                {
                    if (alwaysQuickSingle)
                    {
                        QuickManage(slotAction, selectedSlot, selectedQuantity);
                        return;
                    }

                    _ = new InputBox(
                       promptTitle,
                       singlePromptText.ToString(itemName), true,
                       InputBox.InputType.YesNo, HandleNonstackableSlotAction(slotAction), null, selectedSlot
                    );

                    return;
                }

                _ = new InputBox(
                    promptTitle,
                    manyPromptText.ToString(itemName), true,
                    InputBox.InputType.NumericInput, HandleNonstackableMultislotAction(slotAction, multiSlotAction), null, new KeyValuePair<int, int[]>(selectedSlot, slots), slots.Length
                );
            }
        }

        private void ManageItem(Action<int, int> slotAction, 
            Action<int[], int> multiSlotAction, 
            int selectedSlot,
            LocalizedString promptTitle,
            LocalizedString singlePromptText,
            LocalizedString manyPromptText,
            bool isFailure,
            LocalizedString failureTitle,
            LocalizedString failurePrompt)
        {
            if (isFailure)
            {
                _ = new InputBox(
                    failureTitle, failurePrompt, true, InputBox.InputType.OkayOnly, null, null,
                    -1
                );

                return;
            }

            ManageItem(slotAction, multiSlotAction, selectedSlot, promptTitle, singlePromptText, manyPromptText);
        }

        public bool TryQuickManage(Action<int, int> action, int index, int quantity)
        {
            if (!Input.QuickModifierActive())
            {
                return false;
            }
            QuickManage(action, index, quantity);
            return true;
        }

        private void QuickManage(Action<int, int> action, int index, int quantity)
        {
            action.Invoke(index, quantity);
        }

        //bank
        public void TryDepositItem(int index)
        {
            var allowed = true;
            if (Globals.GuildBank)
            {
                var rank = Globals.Me.GuildRank;
                if (string.IsNullOrWhiteSpace(Globals.Me.Guild) || (!rank.Permissions.BankDeposit && Globals.Me.Rank != 0))
                {
                    ChatboxMsg.AddMessage(new ChatboxMsg(Strings.Guilds.NotAllowedDeposit.ToString(Globals.Me.Guild), CustomColors.Alerts.Error, ChatMessageType.Bank));
                    return;
                }
            }

            ManageItem(
                PacketSender.SendDepositItem,
                PacketSender.SendDepositItems,
                index,
                Strings.Bank.deposititem,
                Strings.Bank.deposititem,
                Strings.Bank.deposititemprompt,
                alwaysQuickSingle: true
            );
        }

        public void TryWithdrawItem(int index)
        {
            if (Globals.Bank[index] != null && ItemBase.Get(Globals.Bank[index].ItemId) != null)
            {
                //Permission Check
                if (Globals.GuildBank)
                {
                    var rank = Globals.Me.GuildRank;
                    if (string.IsNullOrWhiteSpace(Globals.Me.Guild) || (!rank.Permissions.BankRetrieve && Globals.Me.Rank != 0))
                    {
                        ChatboxMsg.AddMessage(new ChatboxMsg(Strings.Guilds.NotAllowedWithdraw.ToString(Globals.Me.Guild), CustomColors.Alerts.Error, ChatMessageType.Bank));
                        return;
                    }
                }

                if (Globals.Bank[index].Quantity > 1)
                {
                    if (Input.QuickModifierActive())
                    {
                        PacketSender.SendWithdrawItem(index, Globals.Bank[index].Quantity);
                        return;
                    }

                    var iBox = new InputBox(
                        Strings.Bank.withdrawitem,
                        Strings.Bank.withdrawitemprompt.ToString(ItemBase.Get(Globals.Bank[index].ItemId).Name), true,
                        InputBox.InputType.NumericInput, WithdrawItemInputBoxOkay, null, index, Globals.Bank[index].Quantity
                    );
                }
                else
                {
                    PacketSender.SendWithdrawItem(index, 1);
                }
            }
        }

        private void WithdrawItemInputBoxOkay(object sender, EventArgs e)
        {
            var value = (int) ((InputBox) sender).Value;
            if (value > 0)
            {
                PacketSender.SendWithdrawItem((int) ((InputBox) sender).UserData, value);
            }
        }

        //Bag
        public void TryStoreBagItem(int invSlot, int bagSlot)
        {
            var inventoryItem = Inventory[invSlot];
            var item = ItemBase.Get(inventoryItem.ItemId);
            if (item != null)
            {
                if (inventoryItem.Quantity > 1)
                {
                    if (Input.QuickModifierActive())
                    {
                        PacketSender.SendStoreBagItem(invSlot, inventoryItem.Quantity, bagSlot);
                        return;
                    }

                    int[] userData = new int[2] { invSlot, bagSlot };
                    var iBox = new InputBox(
                        Strings.Bags.storeitem,
                        Strings.Bags.storeitemprompt.ToString(item.Name), true,
                        InputBox.InputType.NumericInput, StoreBagItemInputBoxOkay, null, userData, inventoryItem.Quantity
                    );
                }
                else
                {
                    PacketSender.SendStoreBagItem(invSlot, 1, bagSlot);
                }
            }
        }

        private void StoreBagItemInputBoxOkay(object sender, EventArgs e)
        {
            var value = (int)((InputBox)sender).Value;
            if (value > 0)
            {
                int[] userData = (int[])((InputBox)sender).UserData;
                PacketSender.SendStoreBagItem(userData[0], value, userData[1]);
            }
        }

        public void TryRetreiveBagItem(int bagSlot, int invSlot)
        {
            var bagItem = Globals.Bag[bagSlot];
            var item = ItemBase.Get(bagItem.ItemId);
            if (bagItem != null && item != null)
            {
                if (Input.QuickModifierActive())
                {
                    PacketSender.SendRetrieveBagItem(bagSlot, bagItem.Quantity, invSlot);
                    return;
                }

                int[] userData = new int[2] { bagSlot, invSlot };
                if (Globals.Bag[bagSlot].Quantity > 1)
                {
                    var iBox = new InputBox(
                        Strings.Bags.retreiveitem,
                        Strings.Bags.retreiveitemprompt.ToString(item.Name), true,
                        InputBox.InputType.NumericInput, RetreiveBagItemInputBoxOkay, null, userData, bagItem.Quantity
                    );
                }
                else
                {
                    PacketSender.SendRetrieveBagItem(bagSlot, 1, invSlot);
                }
            }
        }

        private void RetreiveBagItemInputBoxOkay(object sender, EventArgs e)
        {
            var value = (int)((InputBox)sender).Value;
            if (value > 0)
            {
                int[] userData = (int[])((InputBox)sender).UserData;
                PacketSender.SendRetrieveBagItem(userData[0], value, userData[1]);
            }
        }

        //Trade
        public void TryTradeItem(int index)
        {
            ManageItem(
                PacketSender.SendOfferTradeItem,
                PacketSender.SendOfferTradeItems,
                index,
                Strings.Trading.offeritem,
                Strings.Trading.offeritemprompt,
                Strings.Trading.offeritemprompt,
                alwaysQuickSingle: true
            );
        }

        public void TryRevokeItem(int index)
        {
            var tradeItem = Globals.Trade[0, index];
            var item = ItemBase.Get(Globals.Trade[0, index].ItemId);

            if (tradeItem != null && item != null)
            {
                if (Input.QuickModifierActive())
                {
                    PacketSender.SendRevokeTradeItem(index, tradeItem.Quantity);
                    return;
                }

                if (Globals.Trade[0, index].Quantity > 1)
                {
                    var iBox = new InputBox(
                        Strings.Trading.revokeitem,
                        Strings.Trading.revokeitemprompt.ToString(item.Name),
                        true, InputBox.InputType.NumericInput, RevokeItemInputBoxOkay, null, index, tradeItem.Quantity
                    );
                }
                else
                {
                    PacketSender.SendRevokeTradeItem(index, 1);
                }
            }
        }

        private void RevokeItemInputBoxOkay(object sender, EventArgs e)
        {
            var value = (int) ((InputBox) sender).Value;
            if (value > 0)
            {
                PacketSender.SendRevokeTradeItem((int) ((InputBox) sender).UserData, value);
            }
        }

        //Spell Processing
        public void SwapSpells(int spell1, int spell2)
        {
            if (CastTime == 0)
            {
                var tmpInstance = Spells[spell2].Clone();
                Spells[spell2] = Spells[spell1].Clone();
                Spells[spell1] = tmpInstance.Clone();
            }
        }

        public void TryForgetSpell(int index)
        {
            if (SpellBase.Get(Spells[index].SpellId) != null)
            {
                var iBox = new InputBox(
                    Strings.Spells.forgetspell,
                    Strings.Spells.forgetspellprompt.ToString(SpellBase.Get(Spells[index].SpellId).Name), true,
                    InputBox.InputType.YesNo, ForgetSpellInputBoxOkay, null, index
                );
            }
        }

        private void ForgetSpellInputBoxOkay(object sender, EventArgs e)
        {
            PacketSender.SendForgetSpell((int) ((InputBox) sender).UserData);
        }

        public void TryUseSpell(int index)
        {
            if (StatusActive(StatusTypes.Silence))
            {
                SendAlert(Strings.Spells.silenced, Strings.Combat.silenced);
                return;
            }

            if (IsBusy())
            {
                return;
            }

            var spell = Spells[index];
            if (spell.SpellId != Guid.Empty &&
                (!Globals.Me.SpellCooldowns.ContainsKey(spell.SpellId) ||
                 Globals.Me.SpellCooldowns[spell.SpellId] < Timing.Global.Milliseconds))
            {
                var spellBase = SpellBase.Get(spell.SpellId);

                if (spellBase.CastDuration > 0 && (Options.Instance.CombatOpts.MovementCancelsCast && Globals.Me.IsMoving))
                {
                    return;
                }

                if (spellBase.Combat.TargetType == SpellTargetTypes.Single && spellBase.SpellType == SpellTypes.CombatSpell)
                {
                    if (TargetIndex == Guid.Empty)
                    {
                        SendAlert(Strings.Spells.targetneeded, Strings.Combat.targetneeded);
                        return;
                    }
                    else if (Globals.Entities.TryGetValue(TargetIndex, out var target))
                    {
                        var distanceToTarget = (int)Math.Round(CalculateDistanceTo(target) / Options.TileHeight);
                        if (distanceToTarget > spellBase.Combat.CastRange)
                        {
                            SendAlert("The target is too far away to cast this spell!", "Too far!");
                            return;
                        }
                    }
                }
                
                /* Done on the server now
                if (!HasCastingComponentsFor(spellBase))
                {
                    SendAlert(Strings.Spells.norunes, Strings.Combat.needsrunes);
                    return;
                }*/

                var timeWindow = GetCastStart() + Options.Instance.CombatOpts.CancelCastLeeway;
                if (IsCasting && SpellCast == spell.SpellId 
                    && Globals.Database.AttackCancelsCast 
                    && Timing.Global.Milliseconds > timeWindow)
                {
                    PacketSender.CancelPlayerCast(Globals.Me.Id);
                }
                else
                {
                    PacketSender.SendUseSpell(index, TargetIndex);
                }
            }
        }

        private bool HasCastingComponentsFor(SpellBase spell)
        {
            if (spell.CastingComponents.Count <= 0 || 
                (TryGetEquippedWeapon(out var equippedWeapon) && equippedWeapon.Base.ReplaceCastingComponents))
            {
                return true;
            }

            // Got rid of this -- now done on the server
            var components = spell.CastingComponents;
            
            return true;
        }

        public long GetSpellCooldown(Guid id)
        {
            if (SpellCooldowns.ContainsKey(id))
            {
                return SpellCooldowns[id];
            }

            return 0;
        }

        public void TryUseSpell(Guid spellId)
        {
            if (spellId == Guid.Empty)
            {
                return;
            }

            if (IsDead)
            {
                return;
            }

            for (var i = 0; i < Spells.Length; i++)
            {
                if (Spells[i].SpellId == spellId)
                {
                    TryUseSpell(i);

                    return;
                }
            }
        }

        public int FindHotbarSpell(HotbarInstance hotbarInstance)
        {
            if (hotbarInstance.ItemOrSpellId != Guid.Empty && SpellBase.Get(hotbarInstance.ItemOrSpellId) != null)
            {
                for (var i = 0; i < Spells.Length; i++)
                {
                    if (Spells[i].SpellId == hotbarInstance.ItemOrSpellId)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        //Hotbar Processing
        public void AddToHotbar(byte hotbarSlot, sbyte itemType, int itemSlot)
        {
            Hotbar[hotbarSlot].ItemOrSpellId = Guid.Empty;
            Hotbar[hotbarSlot].PreferredStatBuffs = new int[(int) Stats.StatCount];
            if (itemType == 0)
            {
                var item = Inventory[itemSlot];
                if (item != null)
                {
                    Hotbar[hotbarSlot].ItemOrSpellId = item.ItemId;
                    Hotbar[hotbarSlot].PreferredStatBuffs = item.ItemProperties.StatModifiers;
                }
            }
            else if (itemType == 1)
            {
                var spell = Spells[itemSlot];
                if (spell != null)
                {
                    Hotbar[hotbarSlot].ItemOrSpellId = spell.SpellId;
                }
            }

            PacketSender.SendHotbarUpdate(hotbarSlot, itemType, itemSlot);
        }

        public void HotbarSwap(byte index, byte swapIndex)
        {
            var itemId = Hotbar[index].ItemOrSpellId;
            var bagId = Hotbar[index].BagId;
            var stats = Hotbar[index].PreferredStatBuffs;

            Hotbar[index].ItemOrSpellId = Hotbar[swapIndex].ItemOrSpellId;
            Hotbar[index].BagId = Hotbar[swapIndex].BagId;
            Hotbar[index].PreferredStatBuffs = Hotbar[swapIndex].PreferredStatBuffs;

            Hotbar[swapIndex].ItemOrSpellId = itemId;
            Hotbar[swapIndex].BagId = bagId;
            Hotbar[swapIndex].PreferredStatBuffs = stats;

            PacketSender.SendHotbarSwap(index, swapIndex);
        }

        // Change the dimension if the player is on a gateway
        private void TryToChangeDimension()
        {
            if (X < Options.MapWidth && X >= 0)
            {
                if (Y < Options.MapHeight && Y >= 0)
                {
                    if (MapInstance.Get(CurrentMap) != null && MapInstance.Get(CurrentMap).Attributes[X, Y] != null)
                    {
                        if (MapInstance.Get(CurrentMap).Attributes[X, Y].Type == MapAttributes.ZDimension)
                        {
                            if (((MapZDimensionAttribute) MapInstance.Get(CurrentMap).Attributes[X, Y]).GatewayTo > 0)
                            {
                                Z = (byte) (((MapZDimensionAttribute) MapInstance.Get(CurrentMap).Attributes[X, Y])
                                            .GatewayTo -
                                            1);
                            }
                        }
                    }
                }
            }
        }

        //Input Handling
        private void HandleInput()
        {
            var movex = 0f;
            var movey = 0f;
            if (Interface.Interface.HasInputFocus())
            {
                return;
            }

            if (Controls.KeyDown(Control.MoveUp))
            {
                if (StatusActive(StatusTypes.Confused))
                {
                    movey = -1;
                }
                else 
                {
                    movey = 1;
                }
            }

            if (Controls.KeyDown(Control.MoveDown))
            {
                if (StatusActive(StatusTypes.Confused))
                {
                    movey = 1;
                }
                else
                {
                    movey = -1;
                }
            }

            if (Controls.KeyDown(Control.MoveLeft))
            {
                if (StatusActive(StatusTypes.Confused))
                {
                    movex = 1;
                }
                else
                {
                    movex = -1;
                }
            }

            if (Controls.KeyDown(Control.MoveRight))
            {
                if (StatusActive(StatusTypes.Confused))
                {
                    movex = -1;
                }
                else
                {
                    movex = 1;
                }
            }

            Globals.Me.MoveDir = -1;
            var isMoving = movex != 0f || movey != 0f;

            if (isMoving)
            {
                if (movey < 0)
                {
                    Globals.Me.MoveDir = 1;
                }

                if (movey > 0)
                {
                    Globals.Me.MoveDir = 0;
                }

                if (movex < 0)
                {
                    Globals.Me.MoveDir = 2;
                }

                if (movex > 0)
                {
                    Globals.Me.MoveDir = 3;
                }

                if (Globals.Database.TapToTurn && !CombatMode)
                {
                    //Loop through our direction timers and keep track of how long we've been requesting to move in each direction
                    //If we have only just tapped a button we will set Globals.Me.MoveDir to -1 in order to cancel the movement
                    for (var i = 0; i < 4; i++)
                    {
                        if (i == Globals.Me.MoveDir)
                        {
                            //If we just started to change to a new direction then turn the player only (set the timer to now + Xms)
                            if (MoveDirectionTimers[i] == -1 && !Globals.Me.IsMoving && Dir != Globals.Me.MoveDir)
                            {
                                //Turn Only
                                Dir = (byte)Globals.Me.MoveDir;
                                PacketSender.SendDirection((byte)Globals.Me.MoveDir);
                                MoveDirectionTimers[i] = Timing.Global.Milliseconds + Options.DirChangeTimer;
                                Globals.Me.MoveDir = -1;
                            }
                            //If we're already facing the direction then just start moving (set the timer to now)
                            else if (MoveDirectionTimers[i] == -1 && !Globals.Me.IsMoving && Dir == Globals.Me.MoveDir)
                            {
                                MoveDirectionTimers[i] = Timing.Global.Milliseconds;
                            }
                            //The timer is greater than the currect time, let's cancel the move.
                            else if (MoveDirectionTimers[i] > Timing.Global.Milliseconds && !Globals.Me.IsMoving)
                            {
                                //Don't trigger the actual move immediately, wait until button is held
                                Globals.Me.MoveDir = -1;
                            }
                        }
                        else
                        {
                            //Reset the timer if the direction isn't being requested
                            MoveDirectionTimers[i] = -1;
                        }
                    }
                }
            }

            var castInput = -1;
            for (var barSlot = 0; barSlot < Options.MaxHotbar; barSlot++)
            {
                if (!mLastHotbarUseTime.ContainsKey(barSlot))
                {
                    mLastHotbarUseTime.Add(barSlot, 0);
                }

                if (Controls.KeyDown((Control)barSlot + (int)Control.Hotkey1))
                {
                    castInput = barSlot;
                }
            }

            if (castInput != -1)
            {
                if (0 <= castInput && castInput < Interface.Interface.GameUi?.Hotbar?.Items?.Count && mLastHotbarUseTime[castInput] < Timing.Global.Milliseconds)
                {
                    Interface.Interface.GameUi?.Hotbar?.Items?[castInput]?.Activate();
                    mLastHotbarUseTime[castInput] = Timing.Global.Milliseconds + mHotbarUseDelay;
                }
            }  
        }

        protected int GetDistanceTo(Entity target)
        {
            if (target != null)
            {
                var myMap = MapInstance.Get(CurrentMap);
                var targetMap = MapInstance.Get(target.CurrentMap);
                if (myMap != null && targetMap != null)
                {
                    //Calculate World Tile of Me
                    var x1 = X + myMap.MapGridX * Options.MapWidth;
                    var y1 = Y + myMap.MapGridY * Options.MapHeight;

                    //Calculate world tile of target
                    var x2 = target.X + targetMap.MapGridX * Options.MapWidth;
                    var y2 = target.Y + targetMap.MapGridY * Options.MapHeight;

                    return (int) Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
                }
            }

            //Something is null.. return a value that is out of range :) 
            return 9999;
        }

        public void TargetPartyMember(int idx)
        {
            if (IsDead)
            {
                return;
            }
            if (Globals.Me.IsInParty() && idx < Globals.Me.Party.Count)
            {
                if (Globals.Entities.TryGetValue(Globals.Me.Party[idx].Id, out var partyMember) && GetDistanceTo(partyMember) <= Options.Instance.CombatOpts.PartyTargetDistance)
                {
                    TryTarget(partyMember);
                }
            } else if (idx == 0)
            {
                TryTarget(this);
            }
        }

        public bool TryAutoTarget(bool onlyAggro, Guid exclude = default, bool dontTryFace = false)
        {
            //Check for taunt status if so don't allow to change target
            for (var i = 0; i < Status.Count; i++)
            {
                if (Status[i].Type == StatusTypes.Taunt)
                {
                    return false;
                }
            }

            // Do we need to account for players?
            // Depends on what type of map we're currently on.
            if (Globals.Me.MapInstance == null)
            {
                return false;
            }
            var canTargetPlayers = Globals.Me.MapInstance.ZoneType == MapZones.Safe && Globals.Me.DuelingIds.Count == 0 ? false : true;

            // Build a list of Entities to select from with positions if our list is either old, we've moved or changed maps somehow.
            if (
                mlastTargetScanTime < Timing.Global.Milliseconds ||
                mlastTargetScanMap != Globals.Me.CurrentMap ||
                mlastTargetScanLocation != new Point(X, Y)
                )
            {
                // Add new items to our list!
                foreach (var en in Globals.Entities)
                {
                    // Check if this is a valid entity.
                    if (en.Value == null)
                    {
                        continue;
                    }

                    // Don't allow us to auto target ourselves.
                    if (en.Value == Globals.Me)
                    {
                        continue;
                    }

                    // Check if the entity has stealth status
                    if (en.Value.HideEntity || (en.Value.IsStealthed() && !Globals.Me.IsInMyParty(en.Value.Id)))
                    {
                        continue;
                    }

                    // Check if we are allowed to target players here, if we're not and this is a player then skip!
                    // If we are, check to see if they're our party or nation member, then exclude them. We're friendly happy people here.
                    if (!canTargetPlayers && en.Value.GetEntityType() == EntityTypes.Player)
                    {
                        continue;
                    }
                    else if (canTargetPlayers && en.Value.GetEntityType() == EntityTypes.Player)
                    {
                        var player = en.Value as Player;
                        if (player.IsAllyOf(Globals.Me))
                        {
                            continue;
                        }
                    }

                    if (en.Value.GetEntityType() == EntityTypes.GlobalEntity || en.Value.GetEntityType() == EntityTypes.Player)
                    {
                        // Already in our list?
                        if (mlastTargetList.ContainsKey(en.Value))
                        {
                            mlastTargetList[en.Value].DistanceTo = GetDistanceTo(en.Value);
                        }
                        else
                        {
                            // Add entity with blank time. Never been selected.
                            mlastTargetList.Add(en.Value, new TargetInfo() { DistanceTo = GetDistanceTo(en.Value), LastTimeSelected = 0 });
                        }
                    }
                }

                // Remove old items.
                var toRemove = mlastTargetList.Where(en => !Globals.Entities.ContainsValue(en.Key)).ToArray();
                foreach(var en in toRemove)
                {
                    mlastTargetList.Remove(en.Key);
                }

                // Skip scanning for another second or so.. And set up other values.
                mlastTargetScanTime = Timing.Global.Milliseconds + 300;
                mlastTargetScanMap = CurrentMap;
                mlastTargetScanLocation = new Point(X, Y);
            }

            // Find all valid entities in the direction we are facing.
            var validEntities = Array.Empty<KeyValuePair<Entity, TargetInfo>>(); 

            // TODO: Expose option to users
            if (Globals.Database.TargetAccountDirection)
            {
                switch (Dir)
                {
                    case (byte)Directions.Up:
                        validEntities = mlastTargetList.Where(en =>
                            ((en.Key.CurrentMap == CurrentMap || en.Key.CurrentMap == MapInstance.Left || en.Key.CurrentMap == MapInstance.Right) && en.Key.Y < Y) || en.Key.CurrentMap == MapInstance.Down)
                            .ToArray();
                        break;

                    case (byte)Directions.Down:
                        validEntities = mlastTargetList.Where(en =>
                            ((en.Key.CurrentMap == CurrentMap || en.Key.CurrentMap == MapInstance.Left || en.Key.CurrentMap == MapInstance.Right) && en.Key.Y > Y) || en.Key.CurrentMap == MapInstance.Up)
                            .ToArray();
                        break;

                    case (byte)Directions.Left:
                        validEntities = mlastTargetList.Where(en =>
                            ((en.Key.CurrentMap == CurrentMap || en.Key.CurrentMap == MapInstance.Up || en.Key.CurrentMap == MapInstance.Down) && en.Key.X < X) || en.Key.CurrentMap == MapInstance.Left)
                            .ToArray();
                        break;

                    case (byte)Directions.Right:
                        validEntities = mlastTargetList.Where(en =>
                                    ((en.Key.CurrentMap == CurrentMap || en.Key.CurrentMap == MapInstance.Up || en.Key.CurrentMap == MapInstance.Down) && en.Key.X > X) || en.Key.CurrentMap == MapInstance.Right)
                                    .ToArray();
                        break;
                }
            }
            else
            {
                validEntities = mlastTargetList.ToArray();
            }

            // Reduce the number of targets down to what is in our allowed range.
            validEntities = validEntities.Where(en => en.Value.DistanceTo <= Options.Combat.MaxPlayerAutoTargetRadius && en.Key.Id != exclude).ToArray();
            if (onlyAggro)
            {
                validEntities = validEntities.Where(en => en.Key.Type == -1).ToArray();
            }

            int currentDistance = 9999;
            long currentTime = Timing.Global.Milliseconds;
            Entity currentEntity = mLastEntitySelected;
            foreach(var entity in validEntities)
            {
                if (currentEntity == entity.Key)
                {
                    continue;
                }

                // if distance is the same
                if (entity.Value.DistanceTo == currentDistance)
                {
                    if (entity.Value.LastTimeSelected < currentTime)
                    {
                        currentTime = entity.Value.LastTimeSelected;
                        currentDistance = entity.Value.DistanceTo;
                        currentEntity = entity.Key;
                    }
                }
                else if (entity.Value.DistanceTo < currentDistance)
                {
                    if (entity.Value.LastTimeSelected < currentTime || entity.Value.LastTimeSelected == currentTime)
                    {
                        currentTime = entity.Value.LastTimeSelected;
                        currentDistance = entity.Value.DistanceTo;
                        currentEntity = entity.Key;
                    }
                }
            }

            // We didn't target anything? Can we default to closest?
            if (currentEntity == null)
            {
                currentEntity = validEntities.Where(x => x.Value.DistanceTo == validEntities.Min(y => y.Value.DistanceTo)).FirstOrDefault().Key;

                // Also reset our target times so we can start auto targetting again.
                foreach(var entity in mlastTargetList)
                {
                    entity.Value.LastTimeSelected = 0;
                }
            }

            if (currentEntity == null)
            {
                mLastEntitySelected = null;
                return false;
            }

            if (mlastTargetList.ContainsKey(currentEntity))
            {
                mlastTargetList[currentEntity].LastTimeSelected = Timing.Global.Milliseconds;
            }
            mLastEntitySelected = currentEntity;

            if (TargetIndex != currentEntity.Id && currentEntity.Id != exclude)
            {
                SetTargetBox(currentEntity);
                TargetIndex = currentEntity.Id;
                TargetType = 0;
                if (Globals.Database.ClassicMode && !dontTryFace)
                {
                    TryFaceTarget();
                }

                return true;
            }
            
            return false;
        }

        private void SetTargetBox(Entity en)
        {
            if (en == null)
            {
                TargetBox?.SetEntity(null);
                TargetBox?.Hide();
                return;
            }

            if (en is Player)
            {
                TargetBox?.SetEntity(en, EntityTypes.Player);
            }
            else if (en is Event)
            {
                TargetBox?.SetEntity(en, EntityTypes.Event);
            }
            else
            {
                TargetBox?.SetEntity(en, EntityTypes.GlobalEntity);
            }

            Audio.AddGameSound(Configuration.ClientConfiguration.Instance.TargetSound, false);
            TargetBox?.Show();

            if (!CombatMode && Globals.Database.EnterCombatOnTarget && !Globals.Database.ClassicMode)
            {
                var friendlyTarget = false;
                if (en is Player pl)
                {
                    friendlyTarget = IsAllyOf(en);
                }

                // Don't enter combat mode on self-target, event target, or friendly target
                if (en.Id != Id && en.GetEntityType() != EntityTypes.Event && !friendlyTarget)
                {
                    ToggleCombatMode();
                }
            }
        }

        public bool TryBlock()
        {
            return false;
        }

        public void StopBlocking()
        {
            Blocking = false;
        }

        public bool TryAttack()
        {
            if (AttackTimer > Timing.Global.Ticks / TimeSpan.TicksPerMillisecond || Blocking || (IsMoving && !Options.Instance.PlayerOpts.AllowCombatMovement))
            {
                return false;
            }

            if (Interface.Interface.GameUi.Map.IsOpen)
            {
                return false;
            }

            if (IsDead)
            {
                return false;
            }

            if (StatusActive(StatusTypes.Sleep) || StatusActive(StatusTypes.Stun))
            {
                SendAttackStatusAlerts();
                return false;
            }

            // Can attack when confused/blinded, but at a disadvantage/miss
            if (StatusActive(StatusTypes.Blind) || StatusActive(StatusTypes.Confused))
            {
                SendAttackStatusAlerts();
            }

            int x = Globals.Me.X;
            int y = Globals.Me.Y;
            var map = Globals.Me.CurrentMap;
            var dir = RelevantDir;
            switch (dir)
            {
                case 0:
                    y--;

                    break;
                case 1:
                    y++;

                    break;
                case 2:
                    x--;

                    break;
                case 3:
                    x++;

                    break;
            }

            if (GetRealLocation(ref x, ref y, ref map))
            {
                foreach (var en in Globals.Entities)
                {
                    if (en.Value == null)
                    {
                        continue;
                    }

                    if (en.Value != Globals.Me)
                    {
                        if (en.Value.CurrentMap == map &&
                            en.Value.X == x &&
                            en.Value.Y == y &&
                            en.Value.CanBeAttacked())
                        {
                            //ATTACKKKKK!!!
                            if (!InVehicle || en.Value is Resource)
                            {
                                PacketSender.SendAttack(en.Key);
                            }
                            AttackTimer = Timing.Global.Ticks / TimeSpan.TicksPerMillisecond + CalculateAttackTime();

                            return true;
                        }
                    }
                }
            }

            foreach (MapInstance eventMap in MapInstance.Lookup.Values)
            {
                foreach (var en in eventMap.LocalEntities)
                {
                    if (en.Value == null)
                    {
                        continue;
                    }

                    if (en.Value.CurrentMap == map && en.Value.X == x && en.Value.Y == y)
                    {
                        if (en.Value is Event evt && evt.Trigger == EventTrigger.ActionButton)
                        {
                            //Talk to Event
                            CombatMode = false;
                            PacketSender.SendActivateEvent(en.Key);
                            AttackTimer = Timing.Global.Ticks / TimeSpan.TicksPerMillisecond + CalculateAttackTime();

                            return true;
                        }
                    }
                }
            }

            //Projectile/empty swing for animations
            if (!InVehicle)
            {
                PacketSender.SendAttack(Guid.Empty);
            }
            AttackTimer = Timing.Global.Ticks / TimeSpan.TicksPerMillisecond + CalculateAttackTime();

            return true;
        }

        public bool GetRealLocation(ref int x, ref int y, ref Guid mapId)
        {
            var tmpX = x;
            var tmpY = y;
            var tmpI = -1;
            if (MapInstance.Get(mapId) != null)
            {
                var gridX = MapInstance.Get(mapId).MapGridX;
                var gridY = MapInstance.Get(mapId).MapGridY;

                if (x < 0)
                {
                    tmpX = Options.MapWidth - x * -1;
                    gridX--;
                }

                if (y < 0)
                {
                    tmpY = Options.MapHeight - y * -1;
                    gridY--;
                }

                if (y > Options.MapHeight - 1)
                {
                    tmpY = y - Options.MapHeight;
                    gridY++;
                }

                if (x > Options.MapWidth - 1)
                {
                    tmpX = x - Options.MapWidth;
                    gridX++;
                }

                if (gridX >= 0 && gridX < Globals.MapGridWidth && gridY >= 0 && gridY < Globals.MapGridHeight)
                {
                    if (MapInstance.Get(Globals.MapGrid[gridX, gridY]) != null)
                    {
                        x = (byte) tmpX;
                        y = (byte) tmpY;
                        mapId = Globals.MapGrid[gridX, gridY];

                        return true;
                    }
                }
            }

            return false;
        }

        public bool TryTarget()
        {
            if (IsDead)
            {
                return false;
            }

            //Check for taunt status if so don't allow to change target
            for (var i = 0; i < Status.Count; i++)
            {
                if (Status[i].Type == StatusTypes.Taunt)
                {
                    SendAlert("You are taunted and can't change targets!", "Taunted!");
                    return false;
                }
            }

            var mouseInWorld = Graphics.ConvertToWorldPoint(Globals.InputManager.GetMousePosition());
            var x = (int)mouseInWorld.X;
            var y = (int)mouseInWorld.Y;

            var targetRect = new FloatRect(x - 8, y - 8, 16, 16); //Adjust to allow more/less error

            Entity bestMatch = null;
            var bestAreaMatch = 0f;


            foreach (MapInstance map in MapInstance.Lookup.Values)
            {
                if (x >= map.GetX() && x <= map.GetX() + Options.MapWidth * Options.TileWidth)
                {
                    if (y >= map.GetY() && y <= map.GetY() + Options.MapHeight * Options.TileHeight)
                    {
                        //Remove the offsets to just be dealing with pixels within the map selected
                        x -= (int) map.GetX();
                        y -= (int) map.GetY();

                        //transform pixel format to tile format
                        x /= Options.TileWidth;
                        y /= Options.TileHeight;
                        var mapId = map.Id;

                        if (GetRealLocation(ref x, ref y, ref mapId))
                        {
                            foreach (var en in Globals.Entities)
                            {
                                if (en.Value == null || en.Value.CurrentMap != mapId || en.Value is Projectile || en.Value is Resource || (en.Value.IsStealthed() && !Globals.Me.IsInMyParty(en.Value.Id)))
                                {
                                    continue;
                                }

                                var intersectRect = FloatRect.Intersect(en.Value.WorldPos, targetRect);
                                if (intersectRect.Width * intersectRect.Height > bestAreaMatch)
                                {
                                    bestAreaMatch = intersectRect.Width * intersectRect.Height;
                                    bestMatch = en.Value;
                                }
                            }

                            foreach (MapInstance eventMap in MapInstance.Lookup.Values)
                            {
                                foreach (var en in eventMap.LocalEntities)
                                {
                                    if (en.Value == null || en.Value.CurrentMap != mapId || ((Event)en.Value).DisablePreview)
                                    {
                                        continue;
                                    }

                                    var intersectRect = FloatRect.Intersect(en.Value.WorldPos, targetRect);
                                    if (intersectRect.Width * intersectRect.Height > bestAreaMatch)
                                    {
                                        bestAreaMatch = intersectRect.Width * intersectRect.Height;
                                        bestMatch = en.Value;
                                    }
                                }
                            }

                            if (bestMatch != null && bestMatch.Id != TargetIndex)
                            {
                                var targetType = bestMatch is Event ? 1 : 0;

                                SetTargetBox(bestMatch);

                                if (bestMatch is Player)
                                {
                                    //Select in admin window if open
                                    if (Interface.Interface.GameUi.AdminWindowOpen())
                                    {
                                        Interface.Interface.GameUi.AdminWindowSelectName(bestMatch.Name);
                                    }
                                }

                                TargetType = targetType;
                                TargetIndex = bestMatch.Id;

                                TryFaceTarget();

                                return true;
                            }
                            //else if (!Globals.Database.StickyTarget) <- Old, but also unused? Could leverage if make this configurable
                            else if (bestMatch == null)
                            {
                                // We've clicked off of our target and are allowed to clear it!
                                ClearTarget();
                                return true;
                            }
                            else if (bestMatch.Id == TargetIndex && !bestMatch.IsDead)
                            {
                                TryFaceTarget();

                                return true;
                            }
                        }

                        return false;
                    }
                }
            }

            return false;
        }

        public bool TryTarget(Entity entity, bool force = false)
        {
            if (IsDead)
            {
                return false;
            }

            //Check for taunt status if so don't allow to change target
            for (var i = 0; i < Status.Count; i++)
            {
                if (Status[i].Type == StatusTypes.Taunt && !force)
                {
                    SendAlert("You are taunted and can't target another player!", "Taunted!");
                    return false;
                }
            }

            if (entity == null)
            {
                return false;
            }

            // Are we already targetting this?
            if (TargetBox != null && TargetBox.MyEntity == entity )
            {
                return true;
            }

            var targetType = entity is Event ? 1 : 0;

            if (entity.CalculateDistanceTo(this) > Options.Combat.MaxPlayerAutoTargetRadius * Options.TileWidth)
            {
                return false;
            }

            if (entity.GetType() == typeof(Player))
            {
                //Select in admin window if open
                if (Interface.Interface.GameUi.AdminWindowOpen())
                {
                    Interface.Interface.GameUi.AdminWindowSelectName(entity.Name);
                }
            }

            if (TargetIndex != entity.Id)
            {
                SetTargetBox(entity);
                TargetType = targetType;
                TargetIndex = entity.Id;
            }

            return true;

        }

        public Dictionary<Guid, int> GetInventoryItemsAndQuantities()
        {
            var itemsAndQuantities = new Dictionary<Guid, int>();
            foreach (var item in Inventory)
            {
                if (item != null)
                {
                    if (itemsAndQuantities.ContainsKey(item.ItemId))
                    {
                        itemsAndQuantities[item.ItemId] += item.Quantity;
                    }
                    else
                    {
                        itemsAndQuantities.Add(item.ItemId, item.Quantity);
                    }
                }
            }

            return itemsAndQuantities;
        }

        public int GetTotalOfItem(Guid itemId)
        {
            return GetInventoryItemsAndQuantities()
                .Where(kv => kv.Key == itemId)
                .Aggregate(0, (total, itemAndQuantity) => total += itemAndQuantity.Value);
        }

        public int[] GetSlotsContainingItem(Guid itemId)
        {
            List<int> slots = new List<int>();
            for (var idx = 0; idx < Inventory.Length; idx++)
            {
                if (Globals.Me != null && Globals.Me.MyEquipment.Contains(idx))
                {
                    continue;
                }
                var item = Inventory[idx];
                if (item == null || item.Base == default)
                {
                    continue;
                }
                
                if (item.Base.Id == itemId)
                {
                    slots.Add(idx);
                }
            }

            return slots.ToArray();
        }

        public bool CanCraftItem(Guid craftId)
        {
            var inventoryItemsAndQuantities = GetInventoryItemsAndQuantities();
            foreach (var craft in CraftBase.Get(craftId).Ingredients)
            {
                if (inventoryItemsAndQuantities.ContainsKey(craft.ItemId))
                {
                    if (inventoryItemsAndQuantities[craft.ItemId] >= craft.Quantity)
                    {
                        inventoryItemsAndQuantities[craft.ItemId] -= craft.Quantity;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Returns whether or not a character is stunned via status
        /// </summary>
        /// <returns>true if stunned/snared/sleep</returns>
        private bool IsStunned()
        {
            for (var n = 0; n < Status.Count; n++)
            {
                if (Status[n].Type == StatusTypes.Stun ||
                    Status[n].Type == StatusTypes.Snare ||
                    Status[n].Type == StatusTypes.Sleep)
                {
                    return true;
                }
            }
            return false;
        }

        public bool TryFaceTarget(bool skipSmartDir = false, bool force = false)
        {
            // Check if we're currently casting
            if (IsCasting) return false;

            //check if player is stunned or snared, if so don't let them turn.
            if (IsStunned())
            {
                SendStunAlerts(false);
                return false;
            }

            if (TargetIndex != null && (Globals.Database.FaceOnLock || force))
            {
                foreach (var en in Globals.Entities)
                {
                    if (en.Key == TargetIndex)
                    {
                        if (!IsMoving)
                        {
                            byte newDir = GetDirectionTo(en.Value);
                            Dir = newDir;
                            PacketSender.SendDirection(newDir);
                            return true;
                        } else if (!skipSmartDir)
                        {
                            SmartDirTime = Timing.Global.Milliseconds + Options.Combat.FaceTargetPredictionTime;
                            return false;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Toggles combat mode, and returns whether it's now true
        /// </summary>
        /// <returns>true if combat mode set to true</returns>
        public bool ToggleCombatMode(bool sound = false)
        {
            if (IsDead)
            {
                return false;
            }

            if (Globals.Database.ClassicMode)
            {
                TryFaceTarget();
                return true;
            }

            if (IsBusy() || InVehicle || Interface.Interface.GameUi.FocusedInventory)
            {
                return false;
            }
            if (IsMoving && CombatMode && !ChangeCombatModeNextTile)
            {
                ChangeCombatModeNextTile = true;
                return CombatMode;
            }
            
            ChangeCombatModeNextTile = false;

            CombatMode = !CombatMode;
            if (CombatMode)
            {
                Audio.AddGameSound("al_combat_enter.wav", false);
                FaceDirection = GetDirectionFromMouse(WorldPos);
                PacketSender.SendDirection(FaceDirection);
            }
            else if (sound)
            {
                Dir = FaceDirection;
                PacketSender.SendDirection(Dir);
                Audio.AddGameSound("al_combat_end.wav", false);
            }

            return CombatMode;
        }

        public bool noDirectionalInputPressed()
        {
            return !Controls.KeyDown(Control.MoveLeft)
                && !Controls.KeyDown(Control.MoveRight)
                && !Controls.KeyDown(Control.MoveDown)
                && !Controls.KeyDown(Control.MoveUp)
                && !Controls.KeyDown(Control.TurnClockwise)
                && !Controls.KeyDown(Control.TurnCounterClockwise);
        }

        public void ClearTarget(bool fromDeath = false, Guid entityId = default)
        {
            SetTargetBox(null);
            TargetIndex = Guid.Empty;
            TargetType = -1;

            // If we're in combat mode when our target is cleared via entity death, try to grab the next closest aggro target.
            if (fromDeath && (CombatMode || Globals.Database.ClassicMode) && TryAutoTarget(true, entityId, true))
            {
                return;
            }

            if (Globals.Database.EnterCombatOnTarget)
            {
                CombatMode = false;
            }
        }

        /// <summary>
        /// Attempts to pick up an item at the specified location.
        /// </summary>
        /// <param name="mapId">The Id of the map we are trying to loot from.</param>
        /// <param name="x">The X location on the current map.</param>
        /// <param name="y">The Y location on the current map.</param>
        /// <param name="uniqueId">The Unique Id of the specific item we want to pick up, leave <see cref="Guid.Empty"/> to not specificy an item and pick up the first thing we can find.</param>
        /// <param name="firstOnly">Defines whether we only want to pick up the first item we can find when true, or all items when false.</param>
        /// <returns></returns>
        public bool TryPickupItem(Guid mapId, int tileIndex, Guid uniqueId = new Guid(), bool firstOnly = false)
        {
            if (IsDead)
            {
                return false;
            }

            var map = MapInstance.Get(mapId);
            if (map == null || tileIndex < 0 || tileIndex >= Options.MapWidth * Options.MapHeight)
            {
                return false;
            }
            
            // Are we trying to pick up anything in particular, or everything?
            if (uniqueId != Guid.Empty || firstOnly)
            {
                if (!map.MapItems.ContainsKey(tileIndex) || map.MapItems[tileIndex].Count < 1)
                {
                    return false;
                }

                foreach (var item in map.MapItems[tileIndex])
                {
                    // Check if we are trying to pick up a specific item, and if this is the one.
                    if (uniqueId != Guid.Empty && item.UniqueId != uniqueId)
                    {
                        continue;
                    }

                    PacketSender.SendPickupItem(mapId, tileIndex, item.UniqueId);

                    return true;
                }
            }
            else
            {
                // Let the server worry about what we can and can not pick up.
                PacketSender.SendPickupItem(mapId, tileIndex, uniqueId);

                return true;
            }

            return false;
        }

        //Forumlas
        public long GetNextLevelExperience()
        {
            return ExperienceToNextLevel;
        }

        public bool TryGetEquippedWeapon(out Item equippedWeapon)
        {
            equippedWeapon = null;
            if (this == Globals.Me)
            {
                if (Options.WeaponIndex > -1 &&
                    Options.WeaponIndex < Equipment.Length &&
                    MyEquipment[Options.WeaponIndex] >= 0)
                {
                    equippedWeapon = Inventory[MyEquipment[Options.WeaponIndex]];
                }
            }

            return equippedWeapon != null;
        }

        public bool TryGetEquippedWeaponDescriptor(out ItemBase weapon)
        {
            weapon = null;
            if (this == Globals.Me && TryGetEquippedWeapon(out var equippedWeapon))
            {
                weapon = ItemBase.Get(equippedWeapon.ItemId);
            }
            else
            {
                if (Options.WeaponIndex > -1 &&
                    Options.WeaponIndex < Equipment.Length &&
                    Equipment[Options.WeaponIndex] != Guid.Empty)
                {
                    weapon = ItemBase.Get(Equipment[Options.WeaponIndex]);
                }
            }

            return weapon != null;
        }

        public int AttackSpeed()
        {
            var attackTime = 0;
            var cls = ClassBase.Get(Class);
            if (cls != null && cls.AttackSpeedModifier == 1) //Static
            {
                attackTime = cls.AttackSpeedValue;
            }

            if (TryGetEquippedWeaponDescriptor(out var weapon))
            {
                if (weapon.AttackSpeedModifier == 1) // Static
                {
                    attackTime = weapon.AttackSpeedValue;
                }
                else if (weapon.AttackSpeedModifier == 2) //Percentage
                {
                    attackTime = (int)(attackTime * (100f / weapon.AttackSpeedValue));
                }
            }

            return attackTime;
        }

        public override int CalculateAttackTime()
        {
            var attackTime = base.CalculateAttackTime();

            var cls = ClassBase.Get(Class);
            if (cls != null && cls.AttackSpeedModifier == 1) //Static
            {
                attackTime = cls.AttackSpeedValue;
            }

            if (TryGetEquippedWeaponDescriptor(out var weapon))
            {
                if (weapon.AttackSpeedModifier == 1) // Static
                {
                    // Calculating resource harvest bonus
                    if (ResourceLocked)
                    {
                        var harvestBonusValue = CurrentHarvestBonus;
                        var harvestBonusEffect = HarvestBonusHelper.GetBonusEffectForResource(Globals.Me.HarvestingResource);
                        if (harvestBonusEffect != EffectType.None)
                        {
                            harvestBonusValue += GetBonusEffect(harvestBonusEffect) * 0.01;
                        }

                        StatusTypes relevantStatus = HarvestBonusHelper.GetStatusTypeForResource(Globals.Me.HarvestingResource, out int bonus);

                        var statusCount = StatusCount(relevantStatus);
                        if (statusCount > 0)
                        {
                            harvestBonusValue += bonus * 0.01;
                        };

                        var harvestBonus = (int)Math.Floor(weapon.AttackSpeedValue * harvestBonusValue);
                        attackTime = weapon.AttackSpeedValue - harvestBonus;
                    }
                    else
                    {
                        attackTime = weapon.AttackSpeedValue;
                    }
                }
                else if (weapon.AttackSpeedModifier == 2) //Percentage
                {
                    attackTime = (int) (attackTime * (100f / weapon.AttackSpeedValue));
                }
            }

            var swiftBonus = (100 - GetBonusEffect(EffectType.Swiftness)) / 100f;
            attackTime = (int) Math.Floor(attackTime * swiftBonus);

            if (StatusActive(StatusTypes.Swift))
            {
                attackTime = (int)Math.Floor(attackTime * Options.Instance.CombatOpts.SwiftAttackSpeedMod);
            }

            return attackTime;
        }

        /// <summary>
        /// Calculate the attack time for the player as if they have a specified speed stat.
        /// </summary>
        /// <param name="speed"></param>
        /// <returns></returns>
        public virtual int CalculateAttackTime(int speed)
        {
            return (int)(Options.MaxAttackRate +
                          (float)((Options.MinAttackRate - Options.MaxAttackRate) *
                                   (((float)Options.MaxStatValue - speed) /
                                    (float)Options.MaxStatValue)));
        }

        private byte GetDirectionFromMouse(FloatRect fromRect)
        {
            var fromX = fromRect.Left + fromRect.Width / 2;
            var fromY = fromRect.Top + fromRect.Height / 2;

            var right = false;
            var bottom = false;
            var mousePos = Graphics.ConvertToWorldPoint(Globals.InputManager.GetMousePosition());
            var xDiff = fromX - mousePos.X;
            var yDiff = fromY - mousePos.Y;
            if (Math.Sign(xDiff) > 0)
            {
                right = true;
            }
            if (Math.Sign(yDiff) > 0)
            {
                bottom = true;
            }

            if (Math.Abs(yDiff) > Math.Abs(xDiff))
            {
                return (byte)(bottom ? 0 : 1);
            }
            else
            {
                return (byte)(right ? 2 : 3);
            }
        }

        //Movement Processing
        private void ProcessDirectionalInput()
        {
            //Check if player is crafting
            if (Globals.InCraft == true)
            {
                return;
            }

            // Check if the player is currently transitioning between maps with a fade
            if (Globals.InMapTransition == true)
            {
                return;
            }

            //check if player is stunned or snared, if so don't let them move.
            if (IsStunned())
            {
                SendStunAlerts(true);
                return;
            }

            if (IsDead)
            {
                return;
            }

            //Check if the player is dashing, if so don't let them move.
            if (Dashing != null || DashQueue.Count > 0 || DashTimer > Timing.Global.Milliseconds)
            {
                return;
            }

            if (AttackTimer > Timing.Global.Ticks / TimeSpan.TicksPerMillisecond && !Options.Instance.PlayerOpts.AllowCombatMovement)
            {
                return;
            }

            if (Interface.Interface.GameUi.Map.IsOpen)
            {
                return;
            }

            var tmpX = (sbyte) X;
            var tmpY = (sbyte) Y;
            Entity blockedBy = null;

            var prevFace = FaceDirection;

            if (MoveDir > -1 && Globals.EventDialogs.Count == 0)
            {
                //Try to move if able and not casting spells.
                if (!IsMoving && MoveTimer < Timing.Global.Ticks / TimeSpan.TicksPerMillisecond && !IsCasting) 
                {
                    switch (MoveDir)
                    {
                        case 0: // Up
                            if (IsTileBlocked(X, Y - 1, Z, CurrentMap, ref blockedBy) == -1)
                            {
                                tmpY--;
                                IsMoving = true;
                                Dir = 0;
                                OffsetY = Options.TileHeight;
                                OffsetX = 0;
                            }

                            break;
                        case 1: // Down
                            if (IsTileBlocked(X, Y + 1, Z, CurrentMap, ref blockedBy) == -1)
                            {
                                tmpY++;
                                IsMoving = true;
                                Dir = 1;
                                OffsetY = -Options.TileHeight;
                                OffsetX = 0;
                            }

                            break;
                        case 2: // Left
                            if (IsTileBlocked(X - 1, Y, Z, CurrentMap, ref blockedBy) == -1)
                            {
                                tmpX--;
                                IsMoving = true;
                                Dir = 2;
                                OffsetY = 0;
                                OffsetX = Options.TileWidth;
                            }

                            break;
                        case 3: // Right
                            if (IsTileBlocked(X + 1, Y, Z, CurrentMap, ref blockedBy) == -1)
                            {
                                tmpX++;
                                IsMoving = true;
                                Dir = 3;
                                OffsetY = 0;
                                OffsetX = -Options.TileWidth;
                            }

                            break;
                    }

                    if (blockedBy != mLastBumpedEvent)
                    {
                        mLastBumpedEvent = null;
                    }

                    if (IsMoving)
                    {
                        if (tmpX < 0 || tmpY < 0 || tmpX > Options.MapWidth - 1 || tmpY > Options.MapHeight - 1)
                        {
                            var gridX = MapInstance.Get(Globals.Me.CurrentMap).MapGridX;
                            var gridY = MapInstance.Get(Globals.Me.CurrentMap).MapGridY;
                            if (tmpX < 0)
                            {
                                gridX--;
                                X = (byte) (Options.MapWidth - 1);
                            }
                            else if (tmpX >= Options.MapWidth)
                            {
                                X = 0;
                                gridX++;
                            }
                            else
                            {
                                X = (byte) tmpX;
                            }

                            if (tmpY < 0)
                            {
                                gridY--;
                                Y = (byte) (Options.MapHeight - 1);
                            }
                            else if (tmpY >= Options.MapHeight)
                            {
                                Y = 0;
                                gridY++;
                            }
                            else
                            {
                                Y = (byte) tmpY;
                            }

                            if (CurrentMap != Globals.MapGrid[gridX, gridY])
                            {
                                CurrentMap = Globals.MapGrid[gridX, gridY];
                                FetchNewMaps();
                            }

                        }
                        else
                        {
                            X = (byte) tmpX;
                            Y = (byte) tmpY;
                        }

                        // because we want to disable combat mode on warp tiles
                        var currAttr = MapInstance.Get(CurrentMap)?.Attributes[X, Y];
                        if (CurrentMap != default && currAttr?.Type == MapAttributes.Warp)
                        {
                            CombatMode = false;
                        }
                        if (currAttr?.Type == MapAttributes.Footstep) 
                        {
                            Audio.AddGameSound(((MapFootstepAttribute)currAttr).File, false);
                        }
                        TryToChangeDimension();
                        PacketSender.SendMove();
                        MoveTimer = (Timing.Global.Ticks / TimeSpan.TicksPerMillisecond) + (long)GetMovementTime();
                    }
                    else
                    {
                        if (MoveDir != Dir && !CombatMode)
                        {
                            Dir = (byte) MoveDir;
                            PacketSender.SendDirection(Dir);
                        }
                        else if (CombatMode && FaceDirection != prevFace)
                        {
                            PacketSender.SendDirection(FaceDirection);
                        }

                        if (blockedBy != null && mLastBumpedEvent != blockedBy && blockedBy.GetType() == typeof(Event))
                        {
                            CombatMode = false;
                            PacketSender.SendBumpEvent(blockedBy.CurrentMap, blockedBy.Id);
                            mLastBumpedEvent = blockedBy;
                        }
                    }
                }
                // Trying to move while casting? turn the player
                else if (!IsMoving && IsCasting && !CombatMode)
                {
                    if (MoveDir != Dir)
                    {
                        Dir = (byte)MoveDir;
                        PacketSender.SendDirection(Dir);
                    }
                }
            }
            else if (!IsMoving && !DirKeyPressed && !CombatMode)
            {
                if (CastTime > Timing.Global.Milliseconds && !Options.Instance.CombatOpts.TurnWhileCasting)
                {
                    return;
                }

                if (Controls.KeyDown(Control.TurnClockwise))
                {
                    DirKeyPressed = true;
                    switch (Dir)
                    {
                        case (byte)Directions.Up:
                            Dir = (byte)Directions.Right;
                            break;
                        case (byte)Directions.Down:
                            Dir = (byte)Directions.Left;
                            break;
                        case (byte)Directions.Left:
                            Dir = (byte)Directions.Up;
                            break;
                        case (byte)Directions.Right:
                            Dir = (byte)Directions.Down;
                            break;
                    }
                    PacketSender.SendDirection(Dir);
                }
                else if (Controls.KeyDown(Control.TurnCounterClockwise))
                {
                    DirKeyPressed = true;
                    switch (Dir)
                    {
                        case (byte)Directions.Up:
                            Dir = (byte)Directions.Left;
                            break;
                        case (byte)Directions.Down:
                            Dir = (byte)Directions.Right;
                            break;
                        case (byte)Directions.Left:
                            Dir = (byte)Directions.Down;
                            break;
                        case (byte)Directions.Right:
                            Dir = (byte)Directions.Up;
                            break;
                    }
                    PacketSender.SendDirection(Dir);
                }
            }
        }

        public void FetchNewMaps()
        {
            if (Globals.MapGridWidth == 0 || Globals.MapGridHeight == 0)
            {
                return;
            }

            var currentMap = MapInstance.Get(Globals.Me.CurrentMap);

            if (currentMap == null)
            {
                return;
            }

            var gridX = currentMap.MapGridX;
            var gridY = currentMap.MapGridY;
            for (var x = gridX - 1; x <= gridX + 1; x++)
            {
                for (var y = gridY - 1; y <= gridY + 1; y++)
                {
                    if (!Graphics.MapAtCoord(x, y))
                    {
                        continue;
                    }
                        
                    PacketSender.SendNeedMap(Globals.MapGrid[x, y]);
                }
            }
        }

        public override void DrawEquipment(string filename, int alpha, int dir, GameContentManager.TextureType textureType = GameContentManager.TextureType.Paperdoll)
        {
            //check if player is stunned or snared, if so don't let them move.
            for (var n = 0; n < Status.Count; n++)
            {
                if (Status[n].Type == StatusTypes.Transform)
                {
                    return;
                }
            }
            
            if (this is Player player && player.CombatMode)
            {
                base.DrawEquipment(filename, alpha, DetermineRenderDirection(FaceDirection), textureType);
            }
            else
            {
                base.DrawEquipment(filename, alpha, DetermineRenderDirection(Dir), textureType);
            }
        }

        //Override of the original function, used for rendering the color of a player based on rank
        public override void DrawName(Color textColor, Color borderColor, Color backgroundColor)
        {
            if (!Globals.Database.DisplayPlayerNames)
            {
                return;
            }

            if (Globals.Me?.InCutscene() ?? false)
            {
                return;
            }

            if (textColor == null)
            {
                if (Type == 1) //Mod
                {
                    textColor = CustomColors.Names.Players["Moderator"].Name;
                    borderColor = CustomColors.Names.Players["Moderator"].Outline;
                    backgroundColor = CustomColors.Names.Players["Moderator"].Background;
                }
                else if (Type == 2) //Admin
                {
                    textColor = CustomColors.Names.Players["Admin"].Name;
                    borderColor = CustomColors.Names.Players["Admin"].Outline;
                    backgroundColor = CustomColors.Names.Players["Admin"].Background;
                }
                else //No Power
                {
                    textColor = CustomColors.Names.Players["Normal"].Name;
                    borderColor = CustomColors.Names.Players["Normal"].Outline;
                    backgroundColor = CustomColors.Names.Players["Normal"].Background;
                }
            }

            var customColorOverride = NameColor;
            if (customColorOverride != null)
            {
                //We don't want to override the default colors if the color is transparent!
                if (customColorOverride.A != 0)
                {
                    textColor = customColorOverride;
                }
            }

            // Party member names
            if (Globals.Me.Id != Id && Globals.Me.IsInMyParty(this) && CustomColors.Names.Players.ContainsKey("Party") && Globals.Database.DisplayPartyMembers)
            {
                textColor = CustomColors.Names.Players["Party"].Name;
                borderColor = CustomColors.Names.Players["Party"].Outline;
                backgroundColor = CustomColors.Names.Players["Party"].Background;
            }
            // Guildies in PvP
            else if (Globals.Me.Id != Id && Guild == Globals.Me.Guild && Globals.Me.MapInstance.ZoneType != MapZones.Safe && MapInstance.ZoneType != MapZones.Safe)
            {
                textColor = CustomColors.Names.Players["Party"].Name;
                borderColor = CustomColors.Names.Players["Party"].Outline;
                backgroundColor = CustomColors.Names.Players["Party"].Background;
            }

            // Enemies in PvP
            if (!IsAllyOf(Globals.Me) && Globals.Me.MapInstance.ZoneType != MapZones.Safe)
            {
                textColor = CustomColors.Names.Npcs["PlayerAggro"].Name;
                borderColor = CustomColors.Names.Npcs["PlayerAggro"].Outline;
                backgroundColor = CustomColors.Names.Npcs["PlayerAggro"].Background;
            }

            DrawNameAndLabels(textColor, borderColor, backgroundColor);
        }

        private void DrawNameAndLabels(Color textColor, Color borderColor, Color backgroundColor)
        {
            if (IsDead)
            {
                return;
            }
            base.DrawName(textColor, borderColor, backgroundColor);

            if (InCutscene() || (Globals.Me.MapInstance.ZoneType != MapZones.Safe && !InPvpSight))
            {
                return;
            }

            // If in PvP, draw all colors the same (red)
            if (!IsAllyOf(Globals.Me) && Globals.Me.MapInstance.ZoneType != MapZones.Safe)
            {
                DrawLabels(HeaderLabel.Text, 0, textColor, textColor, borderColor, backgroundColor);
                DrawLabels(FooterLabel.Text, 1, textColor, textColor, borderColor, backgroundColor);
            }
            // Otherwise, draw whatever the label color is supposed to be
            else
            {
                DrawLabels(HeaderLabel.Text, 0, HeaderLabel.Color, textColor, borderColor, backgroundColor);
                DrawLabels(FooterLabel.Text, 1, FooterLabel.Color, textColor, borderColor, backgroundColor);
            }
            DrawGuildName(textColor, borderColor, backgroundColor);
        }

        public virtual void DrawGuildName(Color textColor, Color borderColor = null, Color backgroundColor = null)
        {
            if (HideName || Guild == null || Guild.Trim().Length == 0 || !Options.Instance.Guild.ShowGuildNameTagsOverMembers)
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

            var textSize = Graphics.Renderer.MeasureText(Guild, Graphics.EntityNameFont, 1);

            var x = (int)Math.Ceiling(GetCenterPos().X);
            var y = GetLabelLocation(LabelType.Guild);

            if (backgroundColor != Color.Transparent)
            {
                Graphics.DrawGameTexture(
                    Graphics.Renderer.GetWhiteTexture(), new Framework.GenericClasses.FloatRect(0, 0, 1, 1),
                    new Framework.GenericClasses.FloatRect(x - textSize.X / 2f - 4, y, textSize.X + 8, textSize.Y), backgroundColor
                );
            }

            // If the other player is in the same guild as the client, display their guild appropriately
            if (Globals.Database.DisplayClanMembers && Globals.Me.Guild != null && Globals.Me.Id != Id && Guild == Globals.Me.Guild && CustomColors.Names.Players.ContainsKey("Guild"))
            {
                textColor = CustomColors.Names.Players["Guild"].Name;
                borderColor = CustomColors.Names.Players["Guild"].Outline;
                backgroundColor = CustomColors.Names.Players["Guild"].Background;
            }

            var nameColor = Color.FromArgb(textColor.ToArgb());
            var outlineColor = Color.FromArgb(borderColor.ToArgb());
            nameColor.A = (byte)NameOpacity;
            outlineColor.A = NameOpacity < byte.MaxValue ? (byte)0 : (byte)NameOpacity;

            Graphics.Renderer.DrawString(
                Guild, Graphics.EntityNameFont, (int)(x - (int)Math.Ceiling(textSize.X / 2f)), (int)y, 1,
                nameColor, true, null, outlineColor
            );
        }

        public void DrawTargets()
        {
            if (IsDead)
            {
                return;
            }

            foreach (var en in Globals.Entities)
            {
                var entity = en.Value;
                if (entity == null)
                {
                    continue;
                }

                if (entity.IsStealthed() && !entity.IsAllyOf(Globals.Me))
                {
                    if (TargetIndex == en.Key)
                    {
                        ClearTarget();
                    }
                    continue;
                }

                if (entity.HideEntity && TargetIndex == en.Key)
                {
                    ClearTarget();
                    continue;
                }

                if (entity.GetType() == typeof(Projectile) || entity.GetType() == typeof(Resource))
                {
                    continue;
                }

                if (TargetType == 0 && TargetIndex == en.Value.Id)
                {
                    entity.DrawTarget((int)TargetTypes.Selected);
                }
            }

            foreach (MapInstance eventMap in MapInstance.Lookup.Values)
            {
                foreach (var en in eventMap.LocalEntities)
                {
                    if (en.Value == null)
                    {
                        continue;
                    }

                    if (en.Value.CurrentMap == eventMap.Id &&
                        !((Event) en.Value).DisablePreview &&
                        !en.Value.HideEntity &&
                        (!en.Value.IsStealthed() || en.Value is Player player && Globals.Me.IsInMyParty(player)))
                    {
                        if (TargetType == 1 && TargetIndex == en.Value.Id)
                        {
                            en.Value.DrawTarget((int) TargetTypes.Selected);
                        }
                    }
                }
            }

            var mouseInWorld = Graphics.ConvertToWorldPoint(Globals.InputManager.GetMousePosition());
            foreach (MapInstance map in MapInstance.Lookup.Values)
            {
                if (mouseInWorld.X >= map.GetX() && mouseInWorld.X <= map.GetX() + Options.MapWidth * Options.TileWidth)
                {
                    if (mouseInWorld.Y >= map.GetY() && mouseInWorld.Y <= map.GetY() + Options.MapHeight * Options.TileHeight)
                    {
                        var mapId = map.Id;

                        foreach (var en in Globals.Entities)
                        {
                            if (en.Value == null)
                            {
                                continue;
                            }

                            if (en.Value.CurrentMap == mapId &&
                                !en.Value.HideName &&
                                (!en.Value.IsStealthed() || en.Value is Player player && Globals.Me.IsInMyParty(player)) &&
                                en.Value.WorldPos.Contains(mouseInWorld.X, mouseInWorld.Y))
                            {
                                if (en.Value.GetType() != typeof(Projectile) && en.Value.GetType() != typeof(Resource))
                                {
                                    if (TargetType != 0 || TargetIndex != en.Value.Id)
                                    {
                                        en.Value.DrawTarget((int) TargetTypes.Hover);
                                    }
                                }
                            }
                        }

                        foreach (MapInstance eventMap in MapInstance.Lookup.Values)
                        {
                            foreach (var en in eventMap.LocalEntities)
                            {
                                if (en.Value == null)
                                {
                                    continue;
                                }

                                if (en.Value.CurrentMap == mapId &&
                                    !((Event) en.Value).DisablePreview &&
                                    !en.Value.HideEntity &&
                                    (!en.Value.IsStealthed() || en.Value is Player player && Globals.Me.IsInMyParty(player)) &&
                                    en.Value.WorldPos.Contains(mouseInWorld.X, mouseInWorld.Y))
                                {
                                    if (TargetType != 1 || TargetIndex != en.Value.Id)
                                    {
                                        en.Value.DrawTarget((int) TargetTypes.Hover);
                                    }
                                }
                            }
                        }

                        break;
                    }
                }
            }
        }

        private class TargetInfo
        {
            public long LastTimeSelected;

            public int DistanceTo;
        }

    }

    public class FriendInstance
    {

        public string Map;

        public string Name;

        public bool Online = false;

    }

    public class HotbarInstance
    {

        public Guid BagId = Guid.Empty;

        public Guid ItemOrSpellId = Guid.Empty;

        public int[] PreferredStatBuffs = new int[(int) Stats.StatCount];

        public void Load(string data)
        {
            JsonConvert.PopulateObject(data, this);
        }

    }
    
    public partial class Player : Entity
    {
        private void SendStunAlerts(bool quiet)
        {
            if (Timing.Global.Milliseconds > mLastSpellCastMessageSent)
            {
                if (StatusActive(StatusTypes.Sleep))
                {
                    SendAlert(Strings.Spells.sleep, Strings.Combat.sleep, quiet);
                }
                else if (StatusActive(StatusTypes.Stun))
                {
                    SendAlert(Strings.Spells.stunned, Strings.Combat.stunned, quiet);
                }
                else if (StatusActive(StatusTypes.Snare))
                {
                    SendAlert(Strings.Spells.snared, Strings.Combat.snared, quiet);
                }
            }
        }

        private void SendAlert(string chatMsg, string actionMsg, bool quiet = false)
        {
            if (Timing.Global.Milliseconds > mLastSpellCastMessageSent)
            {
                mLastSpellCastMessageSent = Timing.Global.Milliseconds + 700;
                if (!quiet)
                {
                    Audio.AddGameSound(Options.UIDenySound, false);
                    
                    ChatboxMsg.AddMessage(new ChatboxMsg(chatMsg, CustomColors.Alerts.Error, ChatMessageType.Spells));
                }

                ShowActionMessage(actionMsg, CustomColors.General.GeneralDisabled, true);
            }
        }

        private void SendAttackStatusAlerts()
        {
            if (Timing.Global.Milliseconds > mLastSpellCastMessageSent)
            {
                if (StatusActive(StatusTypes.Blind))
                {
                    SendAlert(Strings.Spells.blind, Strings.Combat.blind);
                }
                if (StatusActive(StatusTypes.Sleep))
                {
                    SendAlert(Strings.Spells.sleep, Strings.Combat.sleep);
                }
                else if (StatusActive(StatusTypes.Stun))
                {
                    SendAlert(Strings.Spells.stunned, Strings.Combat.stunned);
                }
                else if (StatusActive(StatusTypes.Snare))
                {
                    SendAlert(Strings.Spells.snared, Strings.Combat.snared);
                }
                else if (StatusActive(StatusTypes.Confused))
                {
                    SendAlert(Strings.Spells.confused, Strings.Combat.confused);
                }
            }
        }
    }

    public partial class Player : Entity
    {
        public Stack<long> DirRequestTimes = new Stack<long>();
        public List<Guid> MapsExplored;

        public bool ChangeCombatModeNextTile;
    }

    public partial class Player : Entity
    {
        public Leaderboard Leaderboard = new Leaderboard();
    }

    public partial class Player : Entity
    {
        public List<Item> RolledLoot;

        public void LoadRolledLoot(List<Loot> loot)
        {
            RolledLoot = new List<Item>();
            if (loot == null)
            {
                return;
            }
            foreach(var drop in loot)
            {
                var item = new Item();
                item.Load(drop.ItemId, drop.Quantity, null, drop.ItemProperties);
                RolledLoot.Add(item);
            }
        }

        public void ResetLoot()
        {
            RolledLoot = null;
        }
        
        /// <summary>
        /// Caclulate crit chance based on the player's current affinity
        /// </summary>
        /// <param name="amountToApplyTo"></param>
        /// <param name="effect"></param>
        /// <returns></returns>
        public override int ApplyBonusEffect(int amountToApplyTo, EffectType effect, bool subtractive = false)
        {
            int effectAmt = GetBonusEffect(effect, 0);

            if (effectAmt == 0) return amountToApplyTo;

            float effectMod = effectAmt / 100f;
            if (subtractive)
            {
                amountToApplyTo = (int)Math.Round(amountToApplyTo * (1 - effectMod));
            }
            else
            {
                amountToApplyTo = (int)Math.Round(amountToApplyTo * (1 + effectMod));
            }

            return amountToApplyTo;
        }

        public override int GetBonusEffect(EffectType effect, int startValue = 0)
        {
            var value = startValue;

            for (var i = 0; i < Options.EquipmentSlots.Count; i++)
            {
                if (Equipment[i] == default)
                {
                    continue;
                }
                
                var item = ItemBase.Get(Equipment[i]);

                if (item == null || !item.EffectsEnabled.Contains(effect))
                {
                    continue;
                }

                value += item.GetEffectPercentage(effect);
            }

            if (TryGetEquippedWeapon(out var weapon))
            {
                value += ItemInstanceHelper.GetEffectBoost(weapon.ItemProperties, effect);
            }

            foreach (var spellId in ActivePassives)
            {
                var descriptor = SpellBase.Get(spellId);
                if (descriptor == default || !descriptor.BonusEffectsEnabled.Contains(effect))
                {
                    continue;
                }
                
                value += descriptor.GetBonusEffectPercentage(effect);
            }

            return value;
        }

        public override Dictionary<EffectType, int> GetAllBonusEffects()
        {
            var effectValues = new Dictionary<EffectType, int>();

            for (var i = 0; i < Options.EquipmentSlots.Count; i++)
            {
                if (Equipment[i] == default)
                {
                    continue;
                }

                var item = ItemBase.Get(Equipment[i]);
                
                foreach (var effect in item.EffectsEnabled)
                {
                    var amt = item.GetEffectPercentage(effect);
                    if (!effectValues.ContainsKey(effect))
                    {
                        effectValues[effect] = amt;
                        continue;
                    }
                    effectValues[effect] += amt;
                }
            }

            if (TryGetEquippedWeapon(out var weapon))
            {
                var idx = 0;
                foreach (var effect in weapon.ItemProperties.EffectEnhancements)
                {
                    if (effect == 0)
                    {
                        idx++;
                        continue;
                    }
                    if (!effectValues.ContainsKey((EffectType)idx)) 
                    {
                        effectValues[(EffectType)idx] = effect;
                        idx++;
                        continue;
                    }
                    effectValues[(EffectType)idx] += effect;
                    idx++;
                }
            }

            foreach (var spellId in ActivePassives)
            {
                var descriptor = SpellBase.Get(spellId);
                if (descriptor == default)
                {
                    continue;
                }

                foreach (var effect in descriptor.BonusEffectsEnabled)
                {
                    var amt = descriptor.GetBonusEffectPercentage(effect);
                    if (!effectValues.ContainsKey(effect))
                    {
                        effectValues[effect] = amt;
                        continue;
                    }
                    effectValues[effect] += amt;
                }
            }

            for (var idx = 0; idx < ChallengeBonusEffects.Count; idx++)
            {
                var amt = ChallengeBonusEffects[idx];
                if (ChallengeBonusEffects.Count <= idx || amt == 0)
                {
                    continue;
                }
                
                if (!effectValues.ContainsKey((EffectType)idx))
                {
                    effectValues[(EffectType)idx] = amt;
                    continue;
                }
                effectValues[(EffectType)idx] += amt;
            }

            return effectValues.OrderBy(kv => kv.Key.GetDescription()).ToDictionary(pair => pair.Key, pair => pair.Value);
        }
    }

    public partial class Player : Entity
    {
        public Guid[] Cosmetics = new Guid[Options.CosmeticSlots.Count];
        public Guid LabelDescriptorId { get; set; }

        public delegate void CharacterWindowUpdate();

        public CharacterWindowUpdate CosmeticsUpdateDelegate;
        public CharacterWindowUpdate ChallengeUpdateDelegate;

        public List<Guid> UnlockedRecipes { get; set; } = new List<Guid>();

        public List<int> ChallengeBonusEffects { get; set; } = new List<int>();
    }

    public partial class Player : Entity
    {
        public Dictionary<Guid, SkillbookInstance> Skillbook = new Dictionary<Guid, SkillbookInstance>();
    }

    public partial class Player : Entity
    {
        public bool CanEarnWeaponExp(Guid weaponId, int weaponLvl)
        {
            var correctWeaponType = Globals.Me.TryGetEquippedWeaponDescriptor(out var weapon)
                && weapon.WeaponTypes.Contains(weaponId);

            var correctWeaponLvl = true; // disbaling below for now
            /*var correctWeaponLvl = weapon != default
                && weapon.MaxWeaponLevels.TryGetValue(weaponId, out var maxWeaponLvl)
                && maxWeaponLvl > weaponLvl;*/

            return correctWeaponLvl && correctWeaponType;
        }
    }

    public partial class Player : Entity
    {
        public List<Guid> DuelingIds { get; set; } = new List<Guid>();

        public override bool IsAllyOf(Entity en)
        {
            if (en is Player ply && ply.DuelingIds.Contains(Id))
            {
                return false;
            }

            return base.IsAllyOf(en);
        }

        public Dictionary<Vitals, int> GetVitalRegens()
        {
            var regens = new Dictionary<Vitals, int>();
            foreach (var eqp in Equipment)
            {
                if (eqp == Guid.Empty)
                {
                    continue;
                }

                var item = ItemBase.Get(eqp);
                if (item == default)
                {
                    continue;
                }

                var health = item.VitalsRegen[(int)Vitals.Health];
                var mana = item.VitalsRegen[(int)Vitals.Mana];

                if (regens.ContainsKey(Vitals.Health))
                {
                    regens[Vitals.Health] += health;
                }
                else
                {
                    regens[Vitals.Health] = health;
                }

                if (regens.ContainsKey(Vitals.Mana))
                {
                    regens[Vitals.Mana] += mana;
                }
                else
                {
                    regens[Vitals.Mana] = mana;
                }
            }

            return regens;
        }

        public Dictionary<Vitals, int> GetVitalBuffs()
        {
            var regens = new Dictionary<Vitals, int>();
            foreach (var eqp in Equipment)
            {
                if (eqp == Guid.Empty)
                {
                    continue;
                }

                var item = ItemBase.Get(eqp);
                if (item == default)
                {
                    continue;
                }

                regens[Vitals.Health] += item.VitalsGiven[(int)Vitals.Health];
                regens[Vitals.Mana] += item.VitalsGiven[(int)Vitals.Mana];
            }

            return regens;
        }

        public Dictionary<Vitals, int> GetVitalPercentageBuffs()
        {
            var regens = new Dictionary<Vitals, int>();
            foreach (var eqp in Equipment)
            {
                if (eqp == Guid.Empty)
                {
                    continue;
                }

                var item = ItemBase.Get(eqp);
                if (item == default)
                {
                    continue;
                }

                regens[Vitals.Health] += item.PercentageVitalsGiven[(int)Vitals.Health];
                regens[Vitals.Mana] += item.PercentageVitalsGiven[(int)Vitals.Mana];
            }

            return regens;
        }

        public bool IsScaledDown { get; set; }

        public int ScaledTo { get; set; }

        public MapType MapType { get; set; }
        
        public bool ClanWarWinner { get; set; }

        protected override bool ShouldDrawFlair()
        {
            return ClanWarWinner;
        }

        public override float StrafeBonus
        {
            get
            {
                if (!TryGetEquippedWeaponDescriptor(out var weapon))
                {
                    return 0.0f;
                }

                return weapon.StrafeBonus / 100f;
            }
        }

        public override float BackstepBonus
        {
            get
            {
                if (!TryGetEquippedWeaponDescriptor(out var weapon))
                {
                    return 0.0f;
                }

                return weapon.BackstepBonus / 100f;
            }
        }

        public override int Speed => InVehicle && VehicleSpeed > 0 ? (int)VehicleSpeed : base.Speed;

        public override bool GetCombatMode()
        {
            return CombatMode;
        }

        public override int GetFaceDirection()
        {
            if (CombatMode)
            {
                return FaceDirection;
            }

            return Dir;
        }
    }
}
