using Intersect.GameObjects;
using Intersect.Network.Packets.Server;
using Intersect.Server.Entities;
using Intersect.Server.Entities.Events;
using Intersect.Server.Utilities;

namespace Intersect.Server.DTOs
{
    public class PlayerHarvestDTO
    {
        public string ResourceName;

        public string ResourceTexture;

        public string HarvestBonus;

        public int HarvestLevel;

        public long Remaining;

        public float PercentCompleted;

        public bool Harvestable;

        public string CannotHarvestMessage;

        public PlayerHarvestDTO(Player player, ResourceBase resource)
        {
            var id = resource.Id;
            var bonusValue = HarvestBonusHelper.CalculateHarvestBonus(player, id);

            HarvestBonus = $"{(int)(bonusValue * 100)}%";
            HarvestLevel = HarvestBonusHelper.GetHarvestBonusLevel(player, id);
            Remaining = HarvestBonusHelper.GetHarvestsUntilNextBonus(player, id);

            float percentRemaining = 0f;
            if (HarvestLevel >= Options.Combat.HarvestBonusIntervals.Count - 1)
            {
                PercentCompleted = -1f; // Completed
            }
            else
            {
                var nextLevelAt = Options.Combat.HarvestBonusIntervals[HarvestLevel] + Remaining;
                var currAmt = Options.Combat.HarvestBonusIntervals[HarvestLevel + 1] - Remaining;
                PercentCompleted = (float)currAmt / nextLevelAt;
            }

            ResourceName = resource.Name;
            ResourceTexture = resource.Initial.GraphicFromTileset ? resource.Initial.Graphic : string.Empty;

            Harvestable = Conditions.MeetsConditionLists(resource.HarvestingRequirements, player, null);
            CannotHarvestMessage = resource.CannotHarvestMessage;
        }

        public ResourceInfoPacket Packetize()
        {
            return new ResourceInfoPacket(ResourceName, 
                ResourceTexture, 
                HarvestBonus, 
                HarvestLevel, 
                Remaining, 
                PercentCompleted,
                Harvestable,
                CannotHarvestMessage);
        }
    }
}
