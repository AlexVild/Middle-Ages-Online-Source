﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using Intersect.Server.Networking;

using Newtonsoft.Json;

namespace Intersect.Server.Entities
{
    public partial class Player : AttackingEntity
    {
        public int Fuel { get; set; }

        [NotMapped, JsonIgnore]
        public Deconstructor Deconstructor { get; set; }

        [NotMapped, JsonIgnore]
        public Guid DeconstructorEventId { get; set; }

        public void OpenDeconstructor(float multiplier, Guid eventId, bool bankingDisabled) 
        {
            Deconstructor = new Deconstructor(multiplier, this, bankingDisabled);
            DeconstructorEventId = eventId;
            PacketSender.SendKnownEnhancementUpdate(this);
            PacketSender.SendUnlockedCosmeticsPacket(this);
            PacketSender.SendOpenDeconstructor(this, multiplier);
        }

        public void CloseDeconstructor()
        {
            Deconstructor = null;
        }
    }
}