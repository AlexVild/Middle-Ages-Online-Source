using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class RequestVehiclePacket : IntersectPacket
    {
        public RequestVehiclePacket() { }

        public RequestVehiclePacket(bool inVehicle, string vehicleSprite, long vehicleSpeed)
        {
            InVehicle = inVehicle;
            VehicleSprite = vehicleSprite;
            VehicleSpeed = vehicleSpeed;
        }

        [Key(0)]
        public bool InVehicle;

        [Key(1)]
        public string VehicleSprite;

        [Key(2)]
        public long VehicleSpeed;
    }
}