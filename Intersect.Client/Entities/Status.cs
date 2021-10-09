using System;

using Intersect.Client.General;
using Intersect.Enums;
using Intersect.Utilities;

namespace Intersect.Client.Entities
{

    public partial class Status
    {

        public string Data = "";

        public int[] Shield = new int[(int) Vitals.VitalCount];

        public Guid SpellId;

        public long TimeRecevied = 0;

        public long TimeRemaining = 0;

        public long TotalDuration = 1;

        public StatusTypes Type;

        public Status(Guid spellId, StatusTypes type, string data, long timeRemaining, long totalDuration)
        {
            SpellId = spellId;
            Type = type;
            Data = data;
            TimeRemaining = timeRemaining;
            TotalDuration = totalDuration;
            TimeRecevied = Timing.Global.Milliseconds;
        }

        public bool IsActive()
        {
            return RemainingMs > 0;
        }

        public long RemainingMs => TimeRemaining - (Timing.Global.Milliseconds - TimeRecevied);

    }

}
