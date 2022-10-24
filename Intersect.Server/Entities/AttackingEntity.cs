using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Server.Entities
{
    public interface AttackingEntity
    {
        void MeleeAttack(Entity enemy, bool ignoreEvasion);

        void SendAttackAnimation(Entity enemy);
    }
}
