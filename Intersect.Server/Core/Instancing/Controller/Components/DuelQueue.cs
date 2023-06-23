using Intersect.Server.Entities;
using System.Collections.Generic;

namespace Intersect.Server.Core.Instancing.Controller.Components
{
    public sealed class DuelQueue : List<Player>
    {
        public void SendToBack(Player player1, Player player2)
        {
            Remove(player1);
            Remove(player2);
            Add(player2);
            Add(player1);
        }
    }
}
