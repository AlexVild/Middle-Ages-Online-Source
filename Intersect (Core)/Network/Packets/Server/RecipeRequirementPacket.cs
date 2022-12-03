using Intersect.GameObjects;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class RecipeRequirementPacket : IntersectPacket
    {
        [Key(0)]
        public Guid RecipeId { get; set; }

        [Key(1)]
        public bool Completed { get; set; }

        [Key(2)]
        public string Image { get; set; }

        [Key(3)]
        public int Amount { get; set; }

        [Key(4)]
        public int Progress { get; set; }

        [Key(5)]
        public int ImageSourceDir { get; set; }

        [Key(6)]
        public string Hint { get; set; }
        
        [Key(7)]
        public RecipeTrigger TriggerType { get; set; }
        
        [Key(8)]
        public Guid TriggerId { get; set; }

        public RecipeRequirementPacket() { }

        public RecipeRequirementPacket(Guid recipeId, bool completed, string image, int amount, int progress, int imageSrcDir, string hint, RecipeTrigger triggerType, Guid triggerId)
        {
            RecipeId = recipeId;
            Completed = completed;
            Image = image;
            Amount = amount;
            Progress = progress;
            ImageSourceDir = imageSrcDir;
            Hint = hint;
            TriggerType = triggerType;
            TriggerId = triggerId;
        }
    }

    [MessagePackObject]
    public class RecipeRequirementPackets : IntersectPacket
    {
        [Key(0)]
        public List<RecipeRequirementPacket> Packets { get; set; }

        public RecipeRequirementPackets() { }

        public RecipeRequirementPackets(List<RecipeRequirementPacket> packets)
        {
            Packets = packets;
        }
    }
}
