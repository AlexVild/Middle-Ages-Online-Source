using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.QuestList;
using Intersect.GameObjects.Conditions;
using Intersect.GameObjects.Events;
using Intersect.GameObjects.Switches_and_Variables;
using Intersect.Server.General;
using Intersect.Server.Maps;
using Intersect.Utilities;
using Intersect.Server.Core;
using Intersect.GameObjects.Timers;
using Intersect.Server.Database.PlayerData.Players;

namespace Intersect.Server.Entities.Events
{
    public static partial class Conditions
    {
        public static bool CanSpawnPage(EventPage page, Player player, Event activeInstance)
        {
            return MeetsConditionLists(page.ConditionLists, player, activeInstance);
        }

        public static bool MeetsConditionLists(
            ConditionLists lists,
            Player player,
            Event eventInstance,
            bool singleList = true,
            QuestBase questBase = null
        )
        {
            if (player == null)
            {
                return false;
            }

            //If no condition lists then this passes
            if (lists.Lists.Count == 0)
            {
                return true;
            }

            for (var i = 0; i < lists.Lists.Count; i++)
            {
                if (MeetsConditionList(lists.Lists[i], player, eventInstance, questBase))

                //Checks to see if all conditions in this list are met
                {
                    //If all conditions are met.. and we only need a single list to pass then return true
                    if (singleList)
                    {
                        return true;
                    }

                    continue;
                }

                //If not.. and we need all lists to pass then return false
                if (!singleList)
                {
                    return false;
                }
            }

            //There were condition lists. If single list was true then we failed every single list and should return false.
            //If single list was false (meaning we needed to pass all lists) then we've made it.. return true.
            return !singleList;
        }

        public static bool MeetsConditionList(
            ConditionList list,
            Player player,
            Event eventInstance,
            QuestBase questBase
        )
        {
            for (var i = 0; i < list.Conditions.Count; i++)
            {
                var meetsCondition = MeetsCondition(list.Conditions[i], player, eventInstance, questBase);

                if (!meetsCondition)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool MeetsCondition(
            Condition condition,
            Player player,
            Event eventInstance,
            QuestBase questBase
        )
        {
            var result = ConditionHandlerRegistry.CheckCondition(condition, player, eventInstance, questBase);
            if (condition.Negated)
            {
                result = !result;
            }
            return result;
        }

        public static bool MeetsCondition(
            VariableIsCondition condition,
            Player player,
            Event eventInstance,
            QuestBase questBase
        )
        {
            VariableValue value = null;
            if (condition.VariableType == VariableTypes.PlayerVariable)
            {
                value = player.GetVariableValue(condition.VariableId);
            }
            else if (condition.VariableType == VariableTypes.ServerVariable)
            {
                value = ServerVariableBase.Get(condition.VariableId)?.Value;
            }
            else if (condition.VariableType == VariableTypes.InstanceVariable && MapController.TryGetInstanceFromMap(player.MapId, player.MapInstanceId, out var mapInstance))
            {
                value = mapInstance.GetInstanceVariable(condition.VariableId);
            }

            if (value == null)
            {
                value = new VariableValue();
            }

            return CheckVariableComparison(value, condition.Comparison, player, eventInstance);
        }

        public static bool MeetsCondition(
            HasItemCondition condition,
            Player player,
            Event eventInstance,
            QuestBase questBase
        )
        {
            var quantity = condition.Quantity;
            if (condition.UseVariable)
            {
                switch (condition.VariableType)
                {
                    case VariableTypes.PlayerVariable:
                        quantity = (int)player.GetVariableValue(condition.VariableId).Integer;

                        break;
                    case VariableTypes.ServerVariable:
                        quantity = (int)ServerVariableBase.Get(condition.VariableId)?.Value.Integer;
                        break;
                    case VariableTypes.InstanceVariable:
                        if (MapController.TryGetInstanceFromMap(player.MapId, player.MapInstanceId, out var mapInstance))
                        {
                            quantity = (int)mapInstance.GetInstanceVariable(condition.VariableId).Integer;
                        }
                        break;
                }
            }

            return player.CountItems(condition.ItemId, true, condition.CheckBank) >= quantity;
        }

        public static bool MeetsCondition(
            ClassIsCondition condition,
            Player player,
            Event eventInstance,
            QuestBase questBase
        )
        {
            if (player.ClassId == condition.ClassId)
            {
                return true;
            }

            return false;
        }

        public static bool MeetsCondition(
            KnowsSpellCondition condition,
            Player player,
            Event eventInstance,
            QuestBase questBase
        )
        {
            if (player.KnowsSpell(condition.SpellId))
            {
                return true;
            }

            return false;
        }

        public static bool MeetsCondition(
            LevelOrStatCondition condition,
            Player player,
            Event eventInstance,
            QuestBase questBase
        )
        {
            var lvlStat = 0;
            if (condition.ComparingLevel)
            {
                lvlStat = player.Level;
            }
            else
            {
                lvlStat = player.Stat[(int)condition.Stat].Value();
                if (condition.IgnoreBuffs)
                {
                    lvlStat = player.GetNonBuffedStat(condition.Stat);
                }
            }

            switch (condition.Comparator) //Comparator
            {
                case VariableComparators.Equal:
                    if (lvlStat == condition.Value)
                    {
                        return true;
                    }

                    break;
                case VariableComparators.GreaterOrEqual:
                    if (lvlStat >= condition.Value)
                    {
                        return true;
                    }

                    break;
                case VariableComparators.LesserOrEqual:
                    if (lvlStat <= condition.Value)
                    {
                        return true;
                    }

                    break;
                case VariableComparators.Greater:
                    if (lvlStat > condition.Value)
                    {
                        return true;
                    }

                    break;
                case VariableComparators.Less:
                    if (lvlStat < condition.Value)
                    {
                        return true;
                    }

                    break;
                case VariableComparators.NotEqual:
                    if (lvlStat != condition.Value)
                    {
                        return true;
                    }

                    break;
            }

            return false;
        }

        public static bool MeetsCondition(
            SelfSwitchCondition condition,
            Player player,
            Event eventInstance,
            QuestBase questBase
        )
        {
            if (eventInstance != null)
            {
                if (eventInstance.Global && MapController.TryGetInstanceFromMap(eventInstance.MapId, player.MapInstanceId, out var instance))
                {
                    if (instance.GlobalEventInstances.TryGetValue(eventInstance.BaseEvent, out Event evt))
                    {
                        if (evt != null)
                        {
                            return evt.SelfSwitch[condition.SwitchIndex] == condition.Value;
                        }
                    }
                }
                else
                {
                    return eventInstance.SelfSwitch[condition.SwitchIndex] == condition.Value;
                }
            }

            return false;
        }

        public static bool MeetsCondition(
            AccessIsCondition condition,
            Player player,
            Event eventInstance,
            QuestBase questBase
        )
        {
            var power = player.Power;
            if (condition.Access == 0)
            {
                return power.Ban || power.Kick || power.Mute;
            }
            else if (condition.Access > 0)
            {
                return power.Editor;
            }

            return false;
        }

        public static bool MeetsCondition(
            TimeBetweenCondition condition,
            Player player,
            Event eventInstance,
            QuestBase questBase
        )
        {
            if (condition.Ranges[0] > -1 &&
                condition.Ranges[1] > -1 &&
                condition.Ranges[0] < 1440 / TimeBase.GetTimeBase().RangeInterval &&
                condition.Ranges[1] < 1440 / TimeBase.GetTimeBase().RangeInterval)
            {
                return Time.GetTimeRange() >= condition.Ranges[0] && Time.GetTimeRange() <= condition.Ranges[1];
            }

            return true;
        }

        public static bool MeetsCondition(
            CanStartQuestCondition condition,
            Player player,
            Event eventInstance,
            QuestBase questBase
        )
        {
            var startQuest = QuestBase.Get(condition.QuestId);
            if (startQuest == questBase)
            {
                //We cannot check and see if we meet quest requirements if we are already checking to see if we meet quest requirements :P
                return true;
            }

            if (startQuest != null)
            {
                return player.CanStartQuest(startQuest);
            }

            return false;
        }

        public static bool MeetsCondition(
            QuestInProgressCondition condition,
            Player player,
            Event eventInstance,
            QuestBase questBase
        )
        {
            return player.QuestInProgress(condition.QuestId, condition.Progress, condition.TaskId);
        }

        public static bool MeetsCondition(
            QuestCompletedCondition condition,
            Player player,
            Event eventInstance,
            QuestBase questBase
        )
        {
            return player.QuestCompleted(condition.QuestId);
        }

        public static bool MeetsCondition(
            NoNpcsOnMapCondition condition,
            Player player,
            Event eventInstance,
            QuestBase questBase
        )
        {
            var map = MapController.Get(eventInstance?.MapId ?? Guid.Empty);
            if (map == null)
            {
                // If we couldn't get an entity's map, use the player's map
                map = MapController.Get(player.MapId);
            }

            if (map != null && map.TryGetInstance(player.MapInstanceId, out var mapInstance))
            {
                var entities = mapInstance.GetEntities();
                foreach (var en in entities)
                {
                    if (en is Npc npc)
                    {
                        if (!condition.SpecificNpc)
                        {
                            return false;
                        }
                        else if (npc.Base?.Id == condition.NpcId)
                        {
                            return false;
                        }
                    }
                }

                return true;
            }

            return false;
        }

        public static bool MeetsCondition(
            GenderIsCondition condition,
            Player player,
            Event eventInstance,
            QuestBase questBase
        )
        {
            return player.Gender == condition.Gender;
        }

        public static bool MeetsCondition(
            MapIsCondition condition,
            Player player,
            Event eventInstance,
            QuestBase questBase
        )
        {
            return player.MapId == condition.MapId;
        }

        public static bool MeetsCondition(
            IsItemEquippedCondition condition,
            Player player,
            Event eventInstance,
            QuestBase questBase
        )
        {
            if (player == null || condition == null)
            {
                return false;
            }

            var equipmentIds = player.EquippedItems.Select(item => item.Descriptor.Id).ToArray();

            return equipmentIds?.Contains(condition.ItemId) ?? false;
        }

        public static bool MeetsCondition(
            HasFreeInventorySlots condition,
            Player player,
            Event eventInstance,
            QuestBase questBase
        )
        {

            var quantity = condition.Quantity;
            if (condition.UseVariable)
            {
                switch (condition.VariableType)
                {
                    case VariableTypes.PlayerVariable:
                        quantity = (int)player.GetVariableValue(condition.VariableId).Integer;

                        break;
                    case VariableTypes.ServerVariable:
                        quantity = (int)ServerVariableBase.Get(condition.VariableId)?.Value.Integer;
                        break;
                    case VariableTypes.InstanceVariable:
                        if (MapController.TryGetInstanceFromMap(player.MapId, player.MapInstanceId, out var mapInstance))
                        {
                            quantity = (int)mapInstance.GetInstanceVariable(condition.VariableId).Integer;
                        }
                        break;
                }
            }

            // Check if the user has (or does not have when negated) the desired amount of inventory slots.
            var slots = player.FindOpenInventorySlots().Count;

            return slots >= quantity;
        }

        public static bool MeetsCondition(
            InGuildWithRank condition,
            Player player,
            Event eventInstance,
            QuestBase questBase
        )
        {
            return player.Guild != null && player.GuildRank <= condition.Rank;
        }

        public static bool MeetsCondition(
            MapZoneTypeIs condition,
            Player player,
            Event eventInstance,
            QuestBase questBase)
        {
            return player.Map?.ZoneType == condition.ZoneType;
        }

        public static bool MeetsCondition(
            InventoryTagCondition condition,
            Player player,
            Event eventInstance,
            QuestBase questBase
        )
        {
            return player.HasItemWithTag(condition.Tag, true, condition.IncludeBank);
        }

        public static bool MeetsCondition(
            EquipmentTagCondition condition,
            Player player,
            Event eventInstance,
            QuestBase questBase
        )
        {
            var equipment = player.Equipment;
            var items = player.Items;

            for (var i = 0; i < Options.EquipmentSlots.Count; i++)
            {
                if (equipment[i] >= 0 && equipment[i] < Options.MaxInvItems && equipment[i] < items.Count)
                {
                    if (items[equipment[i]].ItemId != Guid.Empty)
                    {
                        var item = ItemBase.Get(items[equipment[i]].ItemId);
                        if (item != null)
                        {
                            if (item.Tags.Contains(condition.Tag))
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public static bool MeetsCondition(
            EquipmentInSlotCondition condition,
            Player player,
            Event eventInstance,
            QuestBase questBase
        )
        {
            var equipment = player.Equipment;
            var items = player.Items;

            // Make sure we don't try to access something that doesn't exist
            if (equipment.Length >= condition.slot)
            {
                // Return true if there is some item in the requested equipment slot
                if (equipment[condition.slot] == -1)
                {
                    return false;
                } else
                {
                    return items[equipment[condition.slot]].ItemId != Guid.Empty;
                }
            }

            return false;
        }

        public static bool MeetsCondition(
            InVehicleCondition condition,
            Player player,
            Event eventInstance,
            QuestBase questBase
        )
        {
            return player.InVehicle;
        }
        
        public static bool MeetsCondition(
            InPartyWithCondition condition,
            Player player,
            Event eventInstance,
            QuestBase questBase
        )
        {
            if (player.Party == null)
            {
                return false;
            } 
            return player.Party.Count >= condition.Members && player.Party.Count > 1;
        }

        public static bool MeetsCondition(
            InNpcGuildWithRankCondition condition,
            Player player,
            Event eventInstance,
            QuestBase questBase
        )
        {
            if (player != null && player.ClassInfo.ContainsKey(condition.ClassId))
            {
                var classInfo = player.ClassInfo[condition.ClassId];
                return classInfo.InGuild && classInfo.Rank >= condition.ClassRank;
            }
            return false;
        }

        public static bool MeetsCondition(
            HasSpecialAssignmentForClassCondition condition,
            Player player,
            Event eventInstance,
            QuestBase questBase
        )
        {
            if (player != null && player.ClassInfo.ContainsKey(condition.ClassId))
            {
                var classInfo = player.ClassInfo[condition.ClassId];
                return classInfo.AssignmentAvailable;
            }
            return false;
        }

        public static bool MeetsCondition(
            IsOnGuildTaskForClassCondition condition,
            Player player,
            Event eventInstance,
            QuestBase questBase
        )
        {
            if (player != null && player.ClassInfo.ContainsKey(condition.ClassId))
            {
                var classInfo = player.ClassInfo[condition.ClassId];
                return classInfo.OnTask;
            }
            return false;
        }

        public static bool MeetsCondition(
            HasTaskCompletedForClassCondition condition,
            Player player,
            Event eventInstance,
            QuestBase questBase
        )
        {
            if (player != null && player.ClassInfo.ContainsKey(condition.ClassId))
            {
                var classInfo = player.ClassInfo[condition.ClassId];
                return classInfo.TaskCompleted;
            }
            return false;
        }

        public static bool MeetsCondition(
            TaskIsOnCooldownForClassCondition condition,
            Player player,
            Event eventInstance,
            QuestBase questBase
        )
        {
            if (player != null && player.ClassInfo.ContainsKey(condition.ClassId))
            {
                var classInfo = player.ClassInfo[condition.ClassId];
                return (classInfo.LastTaskStartTime + Options.TaskCooldown > Timing.Global.MillisecondsUtc);
            }
            return false;
        }

        public static bool MeetsCondition(
            HighestClassRankIs condition,
            Player player,
            Event eventInstance,
            QuestBase questBase
        )
        {
            if (player != null && player.ClassInfo.Count > 0)
            {
                return player.HighestClassRank >= condition.ClassRank;
            }
            return false;
        }
        
        public static bool MeetsCondition(
            TimerIsActive condition,
            Player player,
            Event eventInstance,
            QuestBase questBase
        )
        {
            if (player == null)
            {
                return false;
            }

            var descriptor = TimerDescriptor.Get(condition.descriptorId);
            if (descriptor == null)
            {
                return false;
            }

            if (TimerProcessor.TryGetOwnerId(descriptor.OwnerType, condition.descriptorId, player, out var ownerId) && TimerProcessor.TryGetActiveTimer(condition.descriptorId, ownerId, out var timer))
            {
                switch (condition.ConditionType)
                {
                    case TimerActiveConditions.IsActive:
                        return true;
                    case TimerActiveConditions.Elapsed:
                        return timer.ElapsedTime >= condition.ElapsedSeconds * 1000;
                    case TimerActiveConditions.Repetitions:
                        return timer.CompletionCount >= condition.Repetitions;
                    default:
                        throw new NotImplementedException("Invalid TimerActiveCondition given while evaluating TimerIsActive condition");
                }
            }
            else
            {
                return false;
            }
        }

        public static bool MeetsCondition(
            RecordIs condition,
            Player player,
            Event eventInstance,
            QuestBase questBase
        )
        {
            PlayerRecord record = player?.PlayerRecords.Find(r => r.Type == condition.RecordType && r.RecordId == condition.RecordId);
            return record?.Amount >= condition.Value;
        }

        public static bool MeetsCondition(
            RecipeUnlocked condition,
            Player player,
            Event eventInstance,
            QuestBase questBase
        )
        {
            return player?.UnlockedRecipeIds.Contains(condition.RecipeId) ?? false;
        }

        public static bool MeetsCondition(
            BeastsCompleted condition,
            Player player,
            Event eventInstance,
            QuestBase questBase
        )
        {
            return player?.GetBeastsCompleted() >= condition?.Amount;
        }

        public static bool MeetsCondition(
            BeastHasUnlock condition,
            Player player,
            Event eventInstance,
            QuestBase questBase
        )
        {
            return player?.HasUnlockFor(condition.NpcId, condition.Unlock) ?? false;
        }

        public static bool MeetsCondition(
           SpellInSkillbook condition,
           Player player,
           Event eventInstance,
           QuestBase questBase
        )
        {
            return player?.TryGetSkillInSkillbook(condition?.SpellId ?? Guid.Empty, out _) ?? false;
        }

        public static bool MeetsCondition(
           ChallengeCompleted condition,
           Player player,
           Event eventInstance,
           QuestBase questBase
        )
        {
            if (player == null)
            {
                return false;
            }

            var challenge = player.Challenges.Find(c => c.ChallengeId == condition.ChallengeId);

            if (challenge == default)
            {
                return false;
            }

            return challenge.Complete;
        }

        public static bool MeetsCondition(
           WeaponTypeIs condition,
           Player player,
           Event eventInstance,
           QuestBase questBase
        )
        {
            if (player == null)
            {
                return false;
            }

            var weaponType = player.WeaponMasteries.Find(c => c.WeaponTypeId == condition.WeaponTypeId);

            if (weaponType == default)
            {
                return false;
            }

            return weaponType.Level >= condition.Level;
        }

        public static bool MeetsCondition(
          DungeonIs condition,
          Player player,
          Event eventInstance,
          QuestBase questBase
        )
        {
            if (player == null || condition == null || !InstanceProcessor.TryGetInstanceController(player.MapInstanceId, out var instanceController))
            {
                return false;
            }

            return instanceController.DungeonState == condition.State;
        }

        public static bool MeetsCondition(
          SoloDungeon condition,
          Player player,
          Event eventInstance,
          QuestBase questBase
        )
        {
            if (player == null || condition == null || !InstanceProcessor.TryGetInstanceController(player.MapInstanceId, out var instanceController))
            {
                return false;
            }

            return instanceController.Dungeon?.IsSolo ?? false;
        }

        public static bool MeetsCondition(
          GnomeIs condition,
          Player player,
          Event eventInstance,
          QuestBase questBase
        )
        {
            if (player == null || condition == null || !InstanceProcessor.TryGetInstanceController(player.MapInstanceId, out var instanceController))
            {
                return false;
            }

            return instanceController.Dungeon.GnomeObtained;
        }

        public static bool MeetsCondition(
         DungeonTreasureLevelIs condition,
         Player player,
         Event eventInstance,
         QuestBase questBase
       )
        {
            if (player == null || condition == null || !InstanceProcessor.TryGetInstanceController(player.MapInstanceId, out var instanceController))
            {
                return false;
            }

            return instanceController.Dungeon.TreasureLevel >= condition.TreasureLevel;
        }

        public static bool MeetsCondition(
         GnomeLocationIs condition,
         Player player,
         Event eventInstance,
         QuestBase questBase
        )
        {
            if (player == null || condition == null || !InstanceProcessor.TryGetInstanceController(player.MapInstanceId, out var instanceController))
            {
                return false;
            }

            return instanceController.Dungeon.GnomeLocation == condition.GnomeLocation;
        }

        public static bool MeetsCondition(
         EnhancementKnown condition,
         Player player,
         Event eventInstance,
         QuestBase questBase
        )
        {
            if (player == null || condition == null)
            {
                return false;
            }

            return player.KnownEnhancements.ToArray().Contains(condition.EnhancementId);
        }

        public static bool MeetsCondition(
         EnhancementApplied condition,
         Player player,
         Event eventInstance,
         QuestBase questBase
        )
        {
            if (player == null || condition == null)
            {
                return false;
            }

            if (!player.TryGetEquippedItem(Options.WeaponIndex, out var weapon))
            {
                return false;
            }

            return weapon.ItemProperties.AppliedEnhancementIds.Contains(condition.EnhancementId);
        }

        public static bool MeetsCondition(
            IsPartyLeader condition,
            Player player,
            Event eventInstance,
            QuestBase questBase
            )
        {
            if (player == null || condition == null)
            {
                return false;
            }

            if (player.Party == null || player.Party.Count < 2)
            {
                return true;
            }

            return player.Party[0].Id == player.Id;
        }

        public static bool MeetsCondition(
            IsInOpenMelee condition,
            Player player,
            Event eventInstance,
            QuestBase questBase
            )
        {
            if (player == null || condition == null)
            {
                return false;
            }

            if (InstanceProcessor.TryGetInstanceController(player.MapInstanceId, out var controller))
            {
                return controller.DuelPool.Contains(player);
            }
            else
            {
                return false;
            }
        }

        public static bool MeetsCondition(
           MapSpawnGroupIs condition,
           Player player,
           Event eventInstance,
           QuestBase questBase
           )
        {
            if (player == null || condition == null)
            {
                return false;
            }

            if (InstanceProcessor.TryGetInstanceController(player.MapInstanceId, out var controller) && controller.MapSpawnGroups.TryGetValue(player.MapId, out var spawnInfo))
            {
                var group = spawnInfo.Group;
                
                if (condition.OrGreater && condition.OrLess)
                {
                    return true;
                }
                else if (condition.OrLess)
                {
                    return group <= condition.SpawnGroup;
                }
                else if (condition.OrGreater)
                {
                    return group >= condition.SpawnGroup;
                }

                return condition.SpawnGroup == group;
            }
            else
            {
                return false;
            }
        }

        public static bool MeetsCondition(
            ChallengeContractTaken condition,
            Player player,
            Event eventInstance,
            QuestBase questBase
            )
        {
            if (player == null || condition == null)
            {
                return false;
            }

            if (!ChallengeDescriptor.TryGet(condition.ChallengeId, out var descriptor)) 
            {
                return false;
            }

            if (!descriptor.RequiresContract)
            {
                return true;
            }

            return player.ChallengeContractId == descriptor.Id;
        }

        //Variable Comparison Processing

        public static bool CheckVariableComparison(
            VariableValue currentValue,
            VariableCompaison comparison,
            Player player,
            Event instance
        )
        {
            return VariableCheckHandlerRegistry.CheckVariableComparison(currentValue, comparison, player, instance);
        }

        public static bool CheckVariableComparison(
            VariableValue currentValue,
            BooleanVariableComparison comparison,
            Player player,
            Event instance
        )
        {
            VariableValue compValue = null;
            if (comparison.CompareVariableId != Guid.Empty)
            {
                if (comparison.CompareVariableType == VariableTypes.PlayerVariable)
                {
                    compValue = player.GetVariableValue(comparison.CompareVariableId);
                }
                else if (comparison.CompareVariableType == VariableTypes.ServerVariable)
                {
                    compValue = ServerVariableBase.Get(comparison.CompareVariableId)?.Value;
                }
                else if (comparison.CompareVariableType == VariableTypes.InstanceVariable && MapController.TryGetInstanceFromMap(player.MapId, player.MapInstanceId, out var mapInstance))
                {
                    compValue = mapInstance.GetInstanceVariable(comparison.CompareVariableId);
                }
            }
            else
            {
                compValue = new VariableValue();
                compValue.Boolean = comparison.Value;
            }

            if (compValue == null)
            {
                compValue = new VariableValue();
            }

            if (currentValue.Type == 0)
            {
                currentValue.Boolean = false;
            }

            if (compValue.Type != currentValue.Type)
            {
                return false;
            }

            if (comparison.ComparingEqual)
            {
                return currentValue.Boolean == compValue.Boolean;
            }
            else
            {
                return currentValue.Boolean != compValue.Boolean;
            }
        }

        public static bool CheckVariableComparison(
            VariableValue currentValue,
            IntegerVariableComparison comparison,
            Player player,
            Event instance
        )
        {
            long compareAgainst = 0;

            VariableValue compValue = null;
            if (comparison.CompareVariableId != Guid.Empty)
            {
                if (comparison.CompareVariableType == VariableTypes.PlayerVariable)
                {
                    compValue = player.GetVariableValue(comparison.CompareVariableId);
                }
                else if (comparison.CompareVariableType == VariableTypes.ServerVariable)
                {
                    compValue = ServerVariableBase.Get(comparison.CompareVariableId)?.Value;
                }
                else if (comparison.CompareVariableType == VariableTypes.InstanceVariable && MapController.TryGetInstanceFromMap(player.MapId, player.MapInstanceId, out var mapInstance))
                {
                    compValue = mapInstance.GetInstanceVariable(comparison.CompareVariableId);
                }
            }
            else
            {
                compValue = new VariableValue();
                compValue.Integer = comparison.Value;
            }

            if (compValue == null)
            {
                compValue = new VariableValue();
            }

            if (currentValue.Type == 0)
            {
                currentValue.Integer = 0;
            }

            if (compValue.Type != currentValue.Type)
            {
                return false;
            }

            var varVal = currentValue.Integer;
            compareAgainst = compValue.Integer;

            switch (comparison.Comparator) //Comparator
            {
                case VariableComparators.Equal:
                    if (varVal == compareAgainst)
                    {
                        return true;
                    }

                    break;
                case VariableComparators.GreaterOrEqual:
                    if (varVal >= compareAgainst)
                    {
                        return true;
                    }

                    break;
                case VariableComparators.LesserOrEqual:
                    if (varVal <= compareAgainst)
                    {
                        return true;
                    }

                    break;
                case VariableComparators.Greater:
                    if (varVal > compareAgainst)
                    {
                        return true;
                    }

                    break;
                case VariableComparators.Less:
                    if (varVal < compareAgainst)
                    {
                        return true;
                    }

                    break;
                case VariableComparators.NotEqual:
                    if (varVal != compareAgainst)
                    {
                        return true;
                    }

                    break;
            }

            return false;
        }

        public static bool CheckVariableComparison(
            VariableValue currentValue,
            StringVariableComparison comparison,
            Player player,
            Event instance
        )
        {
            var varVal = CommandProcessing.ParseEventText(currentValue.String ?? "", player, instance);
            var compareAgainst = CommandProcessing.ParseEventText(comparison.Value ?? "", player, instance);

            switch (comparison.Comparator)
            {
                case StringVariableComparators.Equal:
                    return varVal == compareAgainst;
                case StringVariableComparators.Contains:
                    return varVal.Contains(compareAgainst);
            }

            return false;
        }

        public static bool MeetsCondition(
           ChampionsDisabled condition,
           Player player,
           Event eventInstance,
           QuestBase questBase
       )
        {
            return player?.DisableChampionSpawns ?? false;
        }

        public static bool MeetsCondition(
           SpellIsActive condition,
           Player player,
           Event eventInstance,
           QuestBase questBase
       )
        {
            return player?.Statuses?.Any(status => status.Key.Id == condition.SpellId) ?? false;
        }

    }

}
