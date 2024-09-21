namespace Intersect.Config
{
    public class MovementOptions
    {
        public float MinSpeedMs { get; set; } = 1000f;
        public int SpeedStatSoftcap { get; set; } = 100; 
        public float BaseSpeedPerTileMs { get; set; } = 320.0f;
        public float CappedSpeedPerTileMs { get; set; } = 270.0f;
        public float SpeedCapDiminishFactor { get; set; } = 3.5f;
    }
}
