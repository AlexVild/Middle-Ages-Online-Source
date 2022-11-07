using Intersect.GameObjects.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Entities.CombatNumbers
{
    interface ICombatNumberDisplayer
    {
        Queue<CombatNumber> ActiveCombatNumbers { get; set; }

        void AddCombatNumber(int value,
            CombatNumberType type,
            int mapX,
            int mapY,
            Guid mapId,
            Entity visibleTo);

        void UpdateAndDrawCombatNumbers();
    }
}
