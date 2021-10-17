using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using Intersect.Enums;
using Intersect.GameObjects.Conditions;
using Intersect.GameObjects.Events;
using Intersect.Localization;
using Intersect.Models;

using Newtonsoft.Json;

namespace Intersect.GameObjects.QuestList
{
    public class QuestListBase : DatabaseObject<QuestListBase>, IFolderable
    {
        [NotMapped]
        public List<QuestBase> Quests = new List<QuestBase>();

        [NotMapped]
        public ConditionLists Requirements { get; set; } = new ConditionLists();

        [JsonConstructor]
        public QuestListBase(Guid id) : base(id)
        {
            Name = "New Quest List";
        }
        
        // For EF
        public QuestListBase()
        {
            Name = "New Quest List";
        }

        [Column("Quests")]
        [JsonIgnore]
        public string QuestsJson
        {
            get => JsonConvert.SerializeObject(Quests);
            set => Quests = JsonConvert.DeserializeObject<List<QuestBase>>(value);
        }

        //Requirements - Store with json
        [Column("Requirements")]
        [JsonIgnore]
        public string JsonRequirements
        {
            get => Requirements.Data();
            set => Requirements.Load(value);
        }

        /// <inheritdoc />
        public string Folder { get; set; } = "";
    }
}
