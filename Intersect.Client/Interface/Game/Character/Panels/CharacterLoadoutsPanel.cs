using Intersect.Client.Networking;
using Intersect.Network.Packets.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Interface.Game.Character.Panels
{
    public class CharacterLoadoutsPanel
    {

    }

    public static class CharacterLoadoutsController
    {
        public static List<Loadout> Loadouts { get; set; }

        public static bool RefreshUi { get; set; }

        public static void LoadLoadouts(Loadout[] loadouts)
        {
            Loadouts.Clear();
            Loadouts = loadouts.ToList();
            RefreshUi = true;
        }

        public static bool TryGetLoadout(Guid loadoutId, out Loadout loadout)
        {
            loadout = Loadouts.Find(l => l.Id == loadoutId);

            return loadout != default;
        }

        public static void RequestLoadoutOverwrite(Guid loadoutId)
        {
            PacketSender.SendOverwriteLoadout(loadoutId);
        }

        public static void RequestLoadoutOverwritePrompt(object sender, EventArgs e)
        {
            var input = (InputBox)sender;
            var loadoutId = (Guid)input.UserData;
            RequestLoadoutOverwrite(loadoutId);
        }

        public static void RequestNewLoadoutSave(string name)
        {
            PacketSender.SendSaveLoadout(name);
        }

        public static void RequestLoadoutRemove(Guid loadoutId)
        {
            PacketSender.SendRemoveLoadout(loadoutId);
        }

        public static void RequestLoadoutSelect(Guid loadoutId)
        {
            PacketSender.SendSelectLoadout(loadoutId);
        }
    }
}
