using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Intersect.GameObjects.Events;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Intersect.GameObjects.Conditions
{

    [JsonConverter(typeof(ConditionListsSerializer))]
    public class ConditionLists
    {

        public List<ConditionList> Lists = new List<ConditionList>();

        public ConditionLists()
        {
        }

        public ConditionLists(string data)
        {
            Load(data);
        }

        public int Count => Lists?.Count ?? 0;

        public void Load(string data)
        {
            Lists.Clear();
            JsonConvert.PopulateObject(
                data, Lists,
                new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                }
            );
        }

        public string Data()
        {
            return JsonConvert.SerializeObject(
                Lists,
                new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate
                }
            );
        }

        public List<string> ConditionListsToRequirementsString()
        {
            List<string> requirementLists = new List<string>();

            foreach (var conditionList in Lists)
            {
                var requirements = new List<string>();

                var classes = new List<string>();
                var classRanks = new List<string>();
                var stats = new List<string>();
                var equip = new List<string>();
                var highestCrs = new List<string>();
                var questsComplete = new List<string>();
                var bestiaryEntries = new List<string>();
                var npcKilled = new List<string>();
                var records = new List<string>();
                var weaponTracks = new List<string>();
                foreach (var condition in conditionList.Conditions)
                {
                    if (condition.Type == ConditionTypes.ClassIs && condition is ClassIsCondition classIs)
                    {
                        if (condition.Negated)
                        {
                            classes.Add($"NOT {ClassBase.GetName(classIs.ClassId)}");
                        }
                        else
                        {
                            classes.Add(ClassBase.GetName(classIs.ClassId));
                        }
                    }
                    else if (condition.Type == ConditionTypes.InNpcGuildWithRank && condition is InNpcGuildWithRankCondition npcGuildCond)
                    {
                        if (condition.Negated)
                        {
                            classRanks.Add($"NOT {npcGuildCond.GetPrettyString()}");
                        }
                        else
                        {
                            classRanks.Add(npcGuildCond.GetPrettyString());
                        }
                    }
                    else if (condition.Type == ConditionTypes.LevelOrStat && condition is LevelOrStatCondition statCond)
                    {
                        if (condition.Negated)
                        {
                            classRanks.Add($"NOT {statCond.GetPrettyString()}");
                        }
                        else
                        {
                            stats.Add(statCond.GetPrettyString());
                        }
                    }
                    else if (condition.Type == ConditionTypes.ItemEquippedWithTag && condition is EquipmentTagCondition equipTag)
                    {
                        if (condition.Negated)
                        {
                            equip.Add($"NOT {equipTag.GetPrettyString()}");
                        }
                        else
                        {
                            equip.Add(equipTag.GetPrettyString());
                        }
                    }
                    else if (condition.Type == ConditionTypes.HighestClassRankIs && condition is HighestClassRankIs highCrCond)
                    {
                        if (condition.Negated)
                        {
                            highestCrs.Add($"NOT {highCrCond.GetPrettyString()}");
                        }
                        else
                        {
                            highestCrs.Add(highCrCond.GetPrettyString());
                        }
                    }
                    else if (condition.Type == ConditionTypes.QuestCompleted && condition is QuestCompletedCondition questCompleteCond)
                    {
                        if (condition.Negated)
                        {
                            questsComplete.Add($"NOT {questCompleteCond.GetPrettyString()}");
                        }
                        else
                        {
                            questsComplete.Add(questCompleteCond.GetPrettyString());
                        }
                    }
                    if (condition.Type == ConditionTypes.BeastsCompleted && condition is BeastsCompleted beastsCompleted)
                    {
                        if (condition.Negated)
                        {
                            bestiaryEntries.Add($"NOT {beastsCompleted.Amount} bestiary entries completed");
                        }
                        else
                        {
                            bestiaryEntries.Add($"{beastsCompleted.Amount} bestiary entries completed");
                        }
                    }
                    if (condition.Type == ConditionTypes.RecordIs && condition is RecordIs recordIs)
                    {
                        if (condition.Negated)
                        {
                            if (recordIs.RecordType == RecordType.NpcKilled)
                            {
                                records.Add($"NOT {recordIs.Value} {NpcBase.GetName(recordIs.RecordId)}s killed");
                            }
                            if (recordIs.RecordType == RecordType.Combo)
                            {
                                records.Add($"NOT record combo of {recordIs.Value}");
                            }
                            if (recordIs.RecordType == RecordType.ItemCrafted)
                            {
                                records.Add($"NOT {recordIs.Value} {ItemBase.GetName(recordIs.RecordId)}s crafted");
                            }
                            if (recordIs.RecordType == RecordType.ResourceGathered)
                            {
                                records.Add($"NOT {recordIs.Value} {ResourceBase.GetName(recordIs.RecordId)}s harvested");
                            }
                        }
                        else
                        {
                            if (recordIs.RecordType == RecordType.NpcKilled)
                            {
                                records.Add($"{recordIs.Value} {NpcBase.GetName(recordIs.RecordId)}s killed");
                            }
                            if (recordIs.RecordType == RecordType.Combo)
                            {
                                records.Add($"Record combo of {recordIs.Value}");
                            }
                            if (recordIs.RecordType == RecordType.ItemCrafted)
                            {
                                records.Add($"{recordIs.Value} {ItemBase.GetName(recordIs.RecordId)}s crafted");
                            }
                            if (recordIs.RecordType == RecordType.ResourceGathered)
                            {
                                records.Add($"{recordIs.Value} {ResourceBase.GetName(recordIs.RecordId)}s harvested");
                            }
                        }
                    }
                    if (condition.Type == ConditionTypes.WeaponMasteryOf && condition is WeaponTypeIs weaponTypeIs)
                    {
                        if (condition.Negated)
                        {
                            weaponTracks.Add($"{WeaponTypeDescriptor.GetName(weaponTypeIs.WeaponTypeId)} track level lower than {weaponTypeIs.Level}");
                        }
                        else
                        {
                            weaponTracks.Add($"{WeaponTypeDescriptor.GetName(weaponTypeIs.WeaponTypeId)} track level {weaponTypeIs.Level} or higher");
                        }
                    }
                }

                if (classes.Count > 0)
                {
                    requirements.Add(string.Join(" and ", classes));
                }

                if (classRanks.Count > 0)
                {
                    requirements.Add(string.Join(", ", classRanks));
                }
                
                if (stats.Count > 0)
                {
                    requirements.Add(string.Join(", ", stats));
                }

                if (equip.Count > 0)
                {
                    requirements.Add(string.Join(", ", equip));
                }

                if (highestCrs.Count > 0)
                {
                    requirements.Add(string.Join(", ", highestCrs));
                }
                
                if (questsComplete.Count > 0)
                {
                    requirements.Add(string.Join(", ", questsComplete));
                }

                if (bestiaryEntries.Count > 0)
                {
                    requirements.Add(string.Join(", ", bestiaryEntries));
                }

                if (records.Count > 0)
                {
                    requirements.Add(string.Join(", ", records));
                }

                if (weaponTracks.Count > 0)
                {
                    weaponTracks.Add(string.Join(", ", weaponTracks));
                }


                requirementLists.Add(string.Join("; ", requirements));
            }

            return requirementLists.Where(str => !string.IsNullOrWhiteSpace(str)).ToList();
        }

    }

    public class ConditionListsSerializer : JsonConverter
    {

        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer
        )
        {
            var jsonObject = JObject.Load(reader);
            var properties = jsonObject.Properties().ToList();
            var lists = existingValue != null ? (ConditionLists) existingValue : new ConditionLists();
            lists.Load((string) properties[0].Value);

            return lists;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("Lists");
            serializer.Serialize(writer, ((ConditionLists) value).Data());
            writer.WriteEndObject();
        }

    }

}
