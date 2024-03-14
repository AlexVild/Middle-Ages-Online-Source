using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using Intersect.Network.Packets.Server;
using Intersect.Server.Database;
using Intersect.Server.Database.PlayerData.Players;
using Intersect.Server.General;
using Intersect.Server.Localization;
using Intersect.Server.Maps;
using Intersect.Server.Networking;
using Intersect.Server.Utilities;
using Intersect.Utilities;

namespace Intersect.Server.Entities
{

    public partial class Resource : Entity
    {

        // Resource Number
        public ResourceBase Base;

        //Respawn
        public long RespawnTime = 0;

        public Resource(ResourceBase resource) : base()
        {
            Base = resource;
            Name = resource.Name;
            Sprite = resource.Initial.Graphic;
            SetMaxVital(
                Vitals.Health,
                Randomization.Next(
                    Math.Min(1, resource.MinHp), Math.Max(resource.MaxHp, Math.Min(1, resource.MinHp)) + 1
                )
            );

            RestoreVital(Vitals.Health);
            Passable = resource.WalkableBefore;
            HideName = true;
        }

        public void Destroy(bool dropItems = false, Entity killer = null)
        {
            lock (EntityLock)
            {
                Die(dropItems, killer);
            }
            
            PacketSender.SendEntityDie(this);
            PacketSender.SendEntityLeave(this);
        }

        public override void Die(bool dropItems = true, Entity killer = null, bool transform = false)
        {
            lock (EntityLock)
            {
                base.Die(false, killer);
            }
            
            Sprite = Base.Exhausted.Graphic;
            Passable = Base.WalkableAfter;
            Dead = true;

            if (dropItems)
            {
                DropItems(killer);
                if (Base.AnimationId != Guid.Empty)
                {
                    PacketSender.SendAnimationToProximity(
                        Base.AnimationId, -1, Guid.Empty, MapId, (byte)X, (byte)Y, (int)Directions.Up, MapInstanceId
                    );
                }
            }
 
            PacketSender.SendEntityDataToProximity(this);
            PacketSender.SendEntityPositionToAll(this);

            if (!(killer is Player playerKiller))
            {
                return;
            }

            playerKiller.GiveInspiredExperience(Base.Experience);
            if (Base.DoNotRecord)
            {
                return;
            }

            // Increment records/determine resource bonuses
            long recordKilled = playerKiller.IncrementRecord(RecordType.ResourceGathered, Base.Id);
            long amountHarvested = Utilities.HarvestBonusHelper.GetAmountInGroupHarvested(playerKiller, Base.Id);
            List<int> intervals = Options.Instance.CombatOpts.HarvestBonusIntervals;
            long progressUntilNextBonus = Utilities.HarvestBonusHelper.GetHarvestsUntilNextBonus(playerKiller, Base.Id);
            
            // Harvest info updated - generate fresh if requested
            playerKiller.UseCachedHarvestInfo = false;

            if (Options.SendResourceRecordUpdates)
            {
                if (amountHarvested <= intervals.Last())
                {
                    // If we just unlocked a new harvesting bonus
                    if (intervals.Find(x => x == amountHarvested) != default)
                    {
                        if (amountHarvested != intervals.Last())
                        {
                            // The player still has some unlocks to go.
                            var nextIntervalIdx = intervals.FindIndex(x => x == amountHarvested) + 1;
                            var nextInterval = intervals[nextIntervalIdx];
                            PacketSender.SendChatMsg(playerKiller, Strings.Records.resourcegatheredbonusunlock.ToString(nextInterval.ToString()), ChatMessageType.Experience, CustomColors.General.GeneralCompleted, sendToast: true);
                        }
                        else
                        {
                            // The player has completed these resource bonuses
                            PacketSender.SendChatMsg(playerKiller, Strings.Records.resourcegatheredbonusunlockcomplete, ChatMessageType.Experience, CustomColors.General.GeneralCompleted, sendToast: true);
                        }
                    }
                    else if (amountHarvested % Options.ResourceRecordUpdateInterval == 0)
                    {
                        // Otherwise, figure out when their next bonus will be awarded and let them know about it.
                        playerKiller.SendRecordUpdate(Strings.Records.resourcegatheredbonus.ToString(amountHarvested, progressUntilNextBonus));
                    }
                }
                else if (amountHarvested % Options.ResourceRecordUpdateInterval == 0)
                {
                    playerKiller.SendRecordUpdate(Strings.Records.resourcegathered.ToString(amountHarvested));
                }
            }
        }

        public void Spawn()
        {
            Sprite = Base.Initial.Graphic;
            if (Base.MaxHp < Base.MinHp)
            {
                Base.MaxHp = Base.MinHp;
            }

            SetMaxVital(Vitals.Health, Randomization.Next(Base.MinHp, Base.MaxHp + 1));
            RestoreVital(Vitals.Health);
            Passable = Base.WalkableBefore;

            Dead = false;
            PacketSender.SendEntityDataToProximity(this);
            PacketSender.SendEntityPositionToAll(this);
        }

        public override float GetVitalRegenRate(int vital)
        {
            if (Base == null)
            {
                return 0f;
            }

            return Base.VitalRegen / 100f;
        }

        public override bool IsPassable()
        {
            return IsDead() & Base.WalkableAfter || !IsDead() && Base.WalkableBefore;
        }

        public override EntityPacket EntityPacket(EntityPacket packet = null, Player forPlayer = null)
        {
            if (packet == null)
            {
                packet = new ResourceEntityPacket();
            }

            packet = base.EntityPacket(packet, forPlayer);

            var pkt = (ResourceEntityPacket) packet;
            pkt.ResourceId = Base.Id;
            pkt.IsDead = IsDead();

            return pkt;
        }

        public override EntityTypes GetEntityType()
        {
            return EntityTypes.Resource;
        }
    }

    public partial class Resource : Entity
    {
        public override void DropItems(Entity killer, bool sendUpdate = true)
        {
            if (!(killer is Player))
            {
                return;
            }
            var playerKiller = killer as Player;

            Guid lootOwner = Guid.Empty;
            // Set owner to player that killed this, if there is any.
            if (playerKiller != null)
            {
                // Yes, so set the owner to the player that killed it.
                lootOwner = playerKiller.Id;
            }

            var rolledItems = new List<Item>();

            // Calculate RP
            var rpBase = ItemBase.Get(Guid.Parse(Options.Instance.LootOpts.RPItemGuid));
            if (rpBase != null && Base?.RP > 0)
            {
                // In PvP, award extra RP
                var amount = Base.RP;
                if (killer.Map?.ZoneType == MapZones.Normal)
                {
                    amount += (int)Math.Ceiling(amount * Options.Instance.PlayerOpts.PvPRPBonus);
                }
                var rp = new Item(rpBase.Id, amount);
                rolledItems.Add(rp);
            }

            var baseDropTable = LootTableServerHelpers.GenerateDropTable(Base.Drops, playerKiller);
            rolledItems.Add(LootTableServerHelpers.GetItemFromTable(baseDropTable));

            LootTableServerHelpers.SpawnItemsOnMap(rolledItems, killer.MapId, killer.MapInstanceId, killer.X, killer.Y, lootOwner, sendUpdate);
        }
    }

}
