using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Config
{
    public class RecordOptions
    {
        public bool SendNpcRecordUpdates = true;

        public int NpcRecordUpdateInterval = 100;

        public bool SendResourceRecordUpdates = true;

        public int ResourceRecordUpdateInterval = 5;

        public bool SendCraftingRecordUpdates = true;

        public int CraftingRecordUpdateInterval = 10;
    }
}
