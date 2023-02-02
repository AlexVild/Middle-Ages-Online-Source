namespace Intersect.Server.Core.Instancing.Controller.Components
{
    public class SpawnInfo
    {
        public SpawnInfo()
        {
            Group = 0;
            PersistCleanup = false;
        }

        public SpawnInfo(int group, bool persistCleanup)
        {
            Group = group;
            PersistCleanup = persistCleanup;
        }

        public int Group { get; set; }

        public bool PersistCleanup { get; set; }
    }
}
