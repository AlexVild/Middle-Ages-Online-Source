﻿using Intersect.GameObjects;
using Intersect.Network.Packets.Server;
using Intersect.Server.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Intersect.Server.Database.PlayerData.Players
{
    public class LabelInstance : IPlayerOwned
    {
        /// <summary>
        /// The database Id of the record.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }

        // EF
        public LabelInstance() { }

        public LabelInstance(Guid playerId, Guid descriptorId)
        {
            PlayerId = playerId;
            DescriptorId = descriptorId;
            IsNew = true;
        }

        [ForeignKey(nameof(Player))]
        public Guid PlayerId { get; private set; }

        [JsonIgnore, NotMapped]
        public virtual Player Player { get; private set; }

        public Guid DescriptorId { get; set; }

        [JsonIgnore, NotMapped]
        public LabelDescriptor Descriptor { get => LabelDescriptor.Get(DescriptorId) ?? null; }

        public bool IsNew { get; set; }

        public PlayerLabelPacket Packetize(Guid selectedId)
        {
            return new PlayerLabelPacket(DescriptorId, IsNew, DescriptorId == selectedId);
        }
    }
}
