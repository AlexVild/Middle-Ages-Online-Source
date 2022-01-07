﻿using System;
using System.Collections.Generic;
using Intersect.Enums;
using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class PlayerEntityPacket : EntityPacket
    {
        //Parameterless Constructor for MessagePack
        public PlayerEntityPacket()
        {
        }


        [Key(25)]
        public int AccessLevel { get; set; }


        [Key(26)]
        public Gender Gender { get; set; }


        [Key(27)]
        public Guid ClassId { get; set; }


        [Key(28)]
        public EquipmentPacket Equipment { get; set; }

    
        [Key(29)]
        public long CombatTimeRemaining { get; set; }


        [Key(30)]
        public string Guild { get; set; }


        [Key(31)]
        public int GuildRank { get; set; }

        [Key(32)]
        public bool InVehicle { get; set; }

        [Key(33)]
        public string VehicleSprite { get; set; }

        [Key(34)]
        public long VehicleSpeed { get; set; }
    }

}
