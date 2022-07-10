using Intersect.Client.General;
using Intersect.Utilities;

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

        public bool Stationary;

        public ActionMessage(MapInstance map, int x, int y, string message, Color color, bool stationary)
        {
            Map = map;
            X = x;
            Y = y;
            Msg = message;
            Clr = color;
            XOffset = Globals.Random.Next(-32, 32); //+- 16 pixels so action msg's don't overlap!
            YOffset = Globals.Random.Next(-20, 20);
            TransmittionTimer = Timing.Global.Milliseconds + Options.ActionMessageTime;
            Stationary = stationary;
        }

        public void TryRemove()
        {
            if (TransmittionTimer <= Timing.Global.Milliseconds)
            {
                Map.ActionMsgs.Remove(this);
            }
        }

    }

}
