using Intersect.Server.Entities;

namespace Intersect.Server.Maps
{

    public partial class MapNpcSpawn
    {

        public Npc Entity;

        public long RespawnTime = -1;

        public int deaths = 0;
    }

}
