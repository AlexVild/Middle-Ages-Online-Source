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

        public override void Die(bool dropItems = true, Entity killer = null)
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

            if (killer is Player playerKiller)
            {
                playerKiller.GiveInspiredExperience(Base.Experience);
                if (Base.DoNotRecord)
                {
                    return;
                }

                // Increment records/determine resource bonuses
                long recordKilled = playerKiller.IncrementRecord(RecordType.ResourceGathered, Base.Id);
                long amountHarvested = GetAmountInGroupHarvested(playerKiller);
                List<int> intervals = Options.Instance.CombatOpts.HarvestBonusIntervals;
                long progressUntilNextBonus = GetHarvestsUntilNextBonus(playerKiller);

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
                                PacketSender.SendEventDialog(playerKiller, Strings.Records.resourcegatheredbonusunlock.ToString(nextInterval.ToString()), "", Guid.NewGuid());
                            }
                            else
                            {
                                // The player has completed these resource bonuses
                                PacketSender.SendEventDialog(playerKiller, Strings.Records.resourcegatheredbonusunlockcomplete, "", Guid.NewGuid());
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
        }

        public long GetHarvestsUntilNextBonus(Player player)
        {
            if (player == null) return 0;

            long currentHarvests = GetAmountInGroupHarvested(player);

            return GetHarvestsUntilNextBonus(currentHarvests);
        }

        public long GetHarvestsUntilNextBonus(long currentHarvests)
        {
            var intervals = Options.Instance.CombatOpts.HarvestBonusIntervals;
            if (currentHarvests >= intervals.Last())
            {
                return 0;
            }

            return intervals.Find(x => currentHarvests < x) - currentHarvests;
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

        public override void ProcessRegen()
        {
            //For now give npcs/resources 10% health back every regen tick... in the future we should put per-npc and per-resource regen settings into their respective editors.
            if (!IsDead())
            {
                if (Base == null)
                {
                    return;
                }
                
                var vital = Vitals.Health;

                var vitalId = (int) vital;
                var vitalValue = GetVital(vital);
                var maxVitalValue = GetMaxVital(vital);
                if (vitalValue < maxVitalValue)
                {
                    var vitalRegenRate = Base.VitalRegen / 100f;
                    var regenValue = (int) Math.Max(1, maxVitalValue * vitalRegenRate) *
                                     Math.Abs(Math.Sign(vitalRegenRate));

                    AddVital(vital, regenValue);
                }
            }
        }

        public override bool IsPassable()
        {
            return IsDead() & Base.WalkableAfter || !IsDead() && Base.WalkableBefore;
        }

        public long GetAmountInGroupHarvested(Player player)
        {
            if (player == null)
            {
                return 0;
            }

            var playerRecord = player.PlayerRecords.Find(record => record.Type == RecordType.ResourceGathered && record.RecordId == Base.Id);

            // Handle resource groups
            if (!string.IsNullOrEmpty(Base.ResourceGroup))
            {
                var resources = Globals.CachedResources.Where(rsc => Base.ResourceGroup == rsc.ResourceGroup).ToArray();
                long totalHarvested = 0;
                foreach (var resource in resources)
                {
                    var tmpRecord = player.PlayerRecords.Find(record => record.Type == RecordType.ResourceGathered && record.RecordId == resource.Id);
                    if (tmpRecord == default)
                    {
                        continue;
                    }
                    totalHarvested += tmpRecord.Amount;
                }

                return totalHarvested;
            }
            else
            {
                if (playerRecord == default)
                {
                    return -1;
                }
                return playerRecord.Amount;
            }
        }

        public double CalculateHarvestBonus(Player attacker)
        {
            if (attacker == null)
            {
                return 0.0;
            }

            long amtHarvested = GetAmountInGroupHarvested(attacker);
            if (amtHarvested <= 0)
            {
                return 0.0f;
            }

            var intervals = Options.Instance.CombatOpts.HarvestBonusIntervals;
            var bonuses = Options.Instance.CombatOpts.HarvestBonuses;
            if (bonuses.Count != intervals.Count)
            {
                Logging.Log.Error($"You fucked up the server config for harvest bonuses. Count is {bonuses.Count}, must be {intervals.Count}");
                return 0.0f;
            }

            if (amtHarvested >= intervals.Last())
            {
                return bonuses.Last();
            }

            for(int i = 0; i < intervals.Count - 2; i++)
            {
                if (amtHarvested >= intervals[i] && amtHarvested < intervals[i + 1])
                {
                    return bonuses[i];
                }
            }

            return 0.0f;
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
            var baseDropTable = LootTableServerHelpers.GenerateDropTable(Base.Drops, playerKiller);
            rolledItems.Add(LootTableServerHelpers.GetItemFromTable(baseDropTable));

            LootTableServerHelpers.SpawnItemsOnMap(rolledItems, MapId, MapInstanceId, killer.X, killer.Y, lootOwner, sendUpdate);
        }
    }

}
