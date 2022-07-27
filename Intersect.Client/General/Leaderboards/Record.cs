namespace Intersect.Client.General.Leaderboards
{
    public class Record
    {
        public string Holder { get; set; }

        public long Index { get; set; }

        public string Value { get; set; }

        public Record(string holder, long index, string value)
        {
            Holder = holder;
            Index = index;
            Value = value;
        }
    }
}
