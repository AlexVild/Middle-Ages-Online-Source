using Intersect.Client.General;

namespace Intersect.Client.Maps
{

    public partial class ActionMessage
    {

        public Color Clr = new Color();

        public MapInstance Map;

        public string Msg = "";

        public long TransmittionTimer;

        public int X;

        public long XOffset;

        public long YOffset;

        public int Y;

        public ActionMessage(MapInstance map, int x, int y, string message, Color color)
        {
            Map = map;
            X = x;
            Y = y;
            Msg = message;
            Clr = color;
            XOffset = Globals.Random.Next(-60, 60); //+- 16 pixels so action msg's don't overlap!
            YOffset = Globals.Random.Next(-20, 20);
            TransmittionTimer = Globals.System.GetTimeMs() + 1000;
        }

        public void TryRemove()
        {
            if (TransmittionTimer <= Globals.System.GetTimeMs())
            {
                Map.ActionMsgs.Remove(this);
            }
        }

    }

}
