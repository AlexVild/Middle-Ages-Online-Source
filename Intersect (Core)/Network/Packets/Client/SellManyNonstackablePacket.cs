using Intersect.Collections;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class SellManyNonstackablePacket : IntersectPacket
    {
        public SellManyNonstackablePacket()
        {

        }

        public SellManyNonstackablePacket(int[] slots, int quantity)
        {
            Slots = slots;
            Quantity = quantity;
        }

        [Key(0)]
        public int[] Slots { get; set; }

        [Key(1)]
        public int Quantity { get; set; }

        [Key(2)]
        public override bool IsValid => Slots.Length >= 0 && Quantity >= 0;

        public override Dictionary<string, SanitizedValue<object>> Sanitize()
        {
            var sanitizer = new Sanitizer();

            Quantity = sanitizer.Maximum(nameof(Quantity), Quantity, 0);

            return sanitizer.Sanitized;
        }
    }
}
