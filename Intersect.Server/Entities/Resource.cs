using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Network.Packets.Server;
using Intersect.Server.Database;
using Intersect.Server.Database.PlayerData.Players;
using Intersect.Server.General;
using Intersect.Server.Localization;
using Intersect.Server.Maps;
using Intersect.Server.Networking;
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
                SpawnResourceItems(killer);
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
                int recordKilled = playerKiller.IncrementRecord(RecordType.ResourceGathered, Base.Id);
                List<int> intervals = Options.Instance.CombatOpts.HarvestBonusIntervals;
                int progressUntilNextBonus = GetHarvestsUntilNextBonus(recordKilled);

                if (Options.SendResourceRecordUpdates)
                {
                    if (recordKilled <= intervals.Last())
                    {
                        // If we just unlocked a new harvesting bonus
                        if (intervals.Find(x => x == recordKilled) != default)
                        {
                            if (recordKilled != intervals.Last())
                            {
                                // The player still has some unlocks to go.
                                var nextIntervalIdx = intervals.FindIndex(x => x == recordKilled) + 1;
                                var nextInterval = intervals[nextIntervalIdx];
                                PacketSender.SendEventDialog(playerKiller, Strings.Records.resourcegatheredbonusunlock.ToString(nextInterval.ToString()), "", Guid.NewGuid());
                            }
                            else
                            {
                                // The player has completed these resource bonuses
                                PacketSender.SendEventDialog(playerKiller, Strings.Records.resourcegatheredbonusunlockcomplete, "", Guid.NewGuid());
                            }
                        }
                        else if (recordKilled % Options.ResourceRecordUpdateInterval == 0)
                        {
                            // Otherwise, figure out when their next bonus will be awarded and let them know about it.
                            playerKiller.SendRecordUpdate(Strings.Records.resourcegatheredbonus.ToString(recordKilled, progressUntilNextBonus));
                        }
                    }
                    else if (recordKilled % Options.ResourceRecordUpdateInterval == 0)
                    {
                        playerKiller.SendRecordUpdate(Strings.Records.resourcegathered.ToString(recordKilled));
                    }
                }

                playerKiller.GiveInspiredExperience(Base.Experience);
            }
        }

        public int GetHarvestsUntilNextBonus(Player player)
        {
            if (player == null) return 0;

            int currentHarvests = 0;
            var currentRecord = player.PlayerRecords.Find(record => record.Type == RecordType.ResourceGathered && record.RecordId == Base.Id);
            if (currentRecord != default)
            {
                currentHarvests = currentRecord.Amount;
            }

            var intervals = Options.Instance.CombatOpts.HarvestBonusIntervals;
            if (currentHarvests >= intervals.Last())
            {
                return 0;
            }

            return intervals.Find(x => currentHarvests < x) - currentHarvests;
        }

        public int GetHarvestsUntilNextBonus(int currentHarvests)
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
            Items.Clear();

            //Give Resource Drops
            var itemSlot = 0;
            foreach (var drop in Base.Drops)
            {
                if (Randomization.Next(1, 10001) <= drop.Chance * 100 && ItemBase.Get(drop.ItemId) != null)
                {
                    var slot = new InventorySlot(itemSlot);
                    slot.Set(new Item(drop.ItemId, drop.Quantity));
                    Items.Add(slot);
                    itemSlot++;
                }
            }

            Dead = false;
            PacketSender.SendEntityDataToProximity(this);
            PacketSender.SendEntityPositionToAll(this);
        }

        public void SpawnResourceItems(Entity killer)
        {
            //Find tile to spawn items
            var tiles = new List<TileHelper>();
            for (var x = X - 1; x <= X + 1; x++)
            {
                for (var y = Y - 1; y <= Y + 1; y++)
                {
                    var tileHelper = new TileHelper(MapId, x, y);
                    if (tileHelper.TryFix())
                    {
                        //Tile is valid.. let's see if its open
                        var mapId = tileHelper.GetMapId();
                        if (MapController.TryGetInstanceFromMap(mapId, MapInstanceId, out var mapInstance))
                        {
                            if (!mapInstance.TileBlocked(tileHelper.GetX(), tileHelper.GetY()))
                            {
                                tiles.Add(tileHelper);
                            }
                            else
                            {
                                if (killer.MapId == tileHelper.GetMapId() &&
                                    killer.X == tileHelper.GetX() &&
                                    killer.Y == tileHelper.GetY())
                                {
                                    tiles.Add(tileHelper);
                                }
                            }
                        }
                    }
                }
            }

            if (tiles.Count > 0)
            {
                TileHelper selectedTile = null;

                //Prefer the players tile, otherwise choose randomly
                for (var i = 0; i < tiles.Count; i++)
                {
                    if (tiles[i].GetMapId() == killer.MapId &&
                        tiles[i].GetX() == killer.X &&
                        tiles[i].GetY() == killer.Y)
                    {
                        selectedTile = tiles[i];
                    }
                }

                if (selectedTile == null)
                {
                    selectedTile = tiles[Randomization.Next(0, tiles.Count)];
                }

                // Drop items
                foreach (var item in Items)
                {
                    if (ItemBase.Get(item.ItemId) != null)
                    {
                        var mapId = selectedTile.GetMapId();
                        if (MapController.TryGetInstanceFromMap(mapId, MapInstanceId, out var mapInstance))
                        {
                            mapInstance.SpawnItem(selectedTile.GetX(), selectedTile.GetY(), item, item.Quantity, killer.Id);
                        }
                    }
                }
            }

            Items.Clear();
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

        public double CalculateHarvestBonus(Player attacker)
        {
            var playerRecord = attacker.PlayerRecords.Find(record => record.Type == RecordType.ResourceGathered && record.RecordId == Base.Id);

            if (playerRecord == null) return 0.0f;

            int amtHarvested = playerRecord.Amount;

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

            for(int i = 0; i < intervals.Count - 1; i++)
            {
                if (amtHarvested >= intervals[i] && amtHarvested < intervals[i])
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

}
