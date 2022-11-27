using Intersect.GameObjects;
using Intersect.Logging;
using Intersect.Server.Database.PlayerData.Players;
using Intersect.Server.Localization;
using Intersect.Server.Networking;
using Intersect.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intersect.Server.Entities
{
    public partial class Player : AttackingEntity
    {
        public virtual List<CosmeticInstance> UnlockedCosmetics { get; set; } = new List<CosmeticInstance>();

        [Column("Cosmetics"), JsonIgnore]
        public string CosmeticsJson
        {
            get => DatabaseUtils.SaveGuidArray(Cosmetics, Options.CosmeticSlots.Count);
            set => Cosmetics = DatabaseUtils.LoadGuidArray(value, Options.CosmeticSlots.Count);
        }

        [NotMapped]
        public Guid[] Cosmetics { get; set; } = new Guid[Options.CosmeticSlots.Count];

        public void ChangeCosmeticUnlockStatus(Guid itemId, bool unlocked = true)
        {
            var cosmetic = UnlockedCosmetics.Find(lbl => lbl.ItemId == itemId);
            var descriptor = ItemBase.Get(itemId);

            // If the cosmetic is already unlocked, and we are TRYING to unlock it
            if (cosmetic != default && unlocked)
            {
                if (cosmetic.Unlocked)
                {
                    // Do nothing
                    return;
                }
            }

            // If the cosmetic is already unlocked, but we want to lock it
            if (cosmetic != default && !unlocked)
            {
                cosmetic.Unlocked = false;

                // Alert the player of their lost cosmetic
                PacketSender.SendChatMsg(this,
                    Strings.Player.CosmeticLost.ToString(descriptor.CosmeticDisplayName ?? string.Empty),
                    Enums.ChatMessageType.Experience,
                    CustomColors.General.GeneralWarning);
                return;
            }

            // The cosmetic has NOT been unlocked, and we wish to change that
            UnlockedCosmetics.Add(new CosmeticInstance(Id, itemId));
            
            // Alert the player of their new unlock
            PacketSender.SendChatMsg(this, 
                Strings.Player.CosmeticUnlocked.ToString(descriptor.CosmeticDisplayName ?? string.Empty), 
                Enums.ChatMessageType.Experience, 
                CustomColors.General.GeneralCompleted, 
                sendToast: true);
        }

        public void SetCosmetic(Guid itemId, string slot)
        {
            var slotIdx = Options.CosmeticSlots.IndexOf(slot);
            if (slotIdx == -1)
            {
                return;
            }

            Cosmetics[slotIdx] = itemId;
        }

        /*public List<PlayerLabelPacket> GetUnlockedLabels()
        {
            if (UseLabelCache && CachedLabelPackets != null)
            {
                return CachedLabelPackets;
            }

            CachedLabelPackets = UnlockedLabels.Select(lbl => lbl.Packetize()).ToList();
            UseLabelCache = true;
            return CachedLabelPackets;
        }*/
    }
}
