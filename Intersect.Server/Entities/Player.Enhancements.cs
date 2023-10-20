using Intersect.GameObjects;
using Intersect.Logging;
using Intersect.Server.Database;
using Intersect.Server.Database.PlayerData.Players;
using Intersect.Server.Localization;
using Intersect.Server.Networking;
using Intersect.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Intersect.Server.Entities
{
    public partial class Player : AttackingEntity
    {
        [NotMapped, JsonIgnore]
        public List<PlayerEnhancementInstance> Enhancements { get; set; } = new List<PlayerEnhancementInstance>();

        [NotMapped, JsonIgnore]
        public IEnumerable<Guid> KnownEnhancements => Enhancements.Where(en => en.Unlocked).Select(en => en.EnhancementId);

        public bool TryUnlockEnhancement(Guid enhancementId)
        {
            if (enhancementId == Guid.Empty)
            {
                return false;
            }

            var enhancement = EnhancementDescriptor.Get(enhancementId);
            if (enhancement == default)
            {
                return false;
            }

            if (KnownEnhancements.ToList().Contains(enhancementId))
            {
                return false;
            }

            if (!enhancement.PrerequisitesMet(KnownEnhancements.ToList()))
            {
                return false;
            }

            var enhancementInstance = new PlayerEnhancementInstance(this, enhancementId);
            Enhancements.Add(enhancementInstance);

            PacketSender.SendChatMsg(this, 
                Strings.Enhancements.LearnEnhancement.ToString(EnhancementDescriptor.GetName(enhancementId)), 
                Enums.ChatMessageType.Experience, 
                CustomColors.General.GeneralCompleted, 
                sendToast: true);

            return true;
        }

        public bool TryForgetEnhancement(Guid enhancementId, bool removeDb = false)
        {
            if (!KnownEnhancements.ToList().Contains(enhancementId))
            {
                return false;
            }

            var enhancement = Enhancements.Find(en => en.EnhancementId == enhancementId);
            if (enhancement == default)
            {
                return false;
            }

            PacketSender.SendChatMsg(this,
                Strings.Enhancements.ForgetEnhancemnet.ToString(EnhancementDescriptor.GetName(enhancementId)),
                Enums.ChatMessageType.Experience,
                CustomColors.General.GeneralCompleted,
                sendToast: true);
            enhancement.Unlocked = false;
            if (removeDb)
            {
                DbInterface.Pool.QueueWorkItem(enhancement.RemoveFromDb);
            }
            return true;
        }
    }
}
