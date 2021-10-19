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
        public DbList<QuestListBase> QuestLists = new DbList<QuestListBase>();

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
            set => QuestLists = JsonConvert.DeserializeObject<DbList<QuestListBase>>(value);
        }

        /// <inheritdoc />
        public string Folder { get; set; } = "";
    }
}
