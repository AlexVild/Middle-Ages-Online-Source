using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using Intersect.Enums;
using Intersect.GameObjects.Conditions;
using Intersect.GameObjects.Events;
using Intersect.GameObjects.QuestList;
using Intersect.GameObjects.QuestBoard;
using Intersect.Localization;
using Intersect.Models;

using Newtonsoft.Json;

namespace Intersect.GameObjects.QuestBoard
{
    public class QuestBoardBase : DatabaseObject<QuestBoardBase>, IFolderable
    {
        [NotMapped]
        public List<QuestListBase> QuestLists = new List<QuestListBase>();

        [JsonConstructor]
        public QuestBoardBase(Guid id) : base(id)
        {
            Name = "New Quest List";
        }

        // For EF
        public QuestBoardBase()
        {
            Name = "New Quest Board";
        }

        [Column("QuestLists")]
        [JsonIgnore]
        public string QuestListsJson
        {
            get => JsonConvert.SerializeObject(QuestLists);
            set => QuestLists = JsonConvert.DeserializeObject<List<QuestListBase>>(value);
        }

        /// <inheritdoc />
        public string Folder { get; set; } = "";
    }
}
