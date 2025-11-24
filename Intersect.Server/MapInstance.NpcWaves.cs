using System;

namespace Intersect.Server.Maps
{
    public partial class MapInstance : IDisposable
    {
        protected NpcWaveController NpcWaveController { get; set; }

        protected void InitializeNpcWaves()
        {
            NpcWaveController = new NpcWaveController(this);
            NpcWaveController.Initialize();
        }

        protected void ProcessNpcWaves()
        {
            NpcWaveController.ProcessWaves();
        }
    }
}
