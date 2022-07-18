using System;

using Intersect.Enums;

namespace Intersect.Client.Entities
{

    public partial class PartyMember
    {

        public Guid Id;

        public int Level;

        public int[] MaxVital = new int[(int) Vitals.VitalCount];

        public string Name;

        public int[] Vital = new int[(int) Vitals.VitalCount];

        public int Shield;

        public PartyMember(Guid id, string name, int[] vital, int[] maxVital, int level, int shield)
        {
            Id = id;
            Name = name;
            Vital = vital;
            MaxVital = maxVital;
            Level = level;
            Shield = shield;
        }

    }

}
