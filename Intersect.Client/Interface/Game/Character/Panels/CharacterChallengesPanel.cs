using Intersect.Client.Core;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Interface.Components;
using Intersect.Client.Interface.Game.Components;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Network.Packets.Server;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intersect.Client.Interface.Game.Character.Panels
{
    public class CharacterChallengesPanel : CharacterWindowPanel
    {
        public CharacterPanelType Type = CharacterPanelType.Challenges;
        public bool Refresh
        {
            get => CharacterChallengesController.Refresh;
            set => CharacterChallengesController.Refresh = value;
        }

        public List<WeaponTypeProgress> Progress
        {
            get => CharacterChallengesController.WeaponTypeProgresses;
            set => CharacterChallengesController.WeaponTypeProgresses = value;
        }

        public CharacterChallengesPanel(ImagePanel panelBackground)
        {
            mParentContainer = panelBackground;
            mBackground = new ImagePanel(mParentContainer, "CharacterWindowMAO_Challenges");

            mBackground.LoadJsonUi(Framework.File_Management.GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        }

        public override void Show()
        {
            PacketSender.SendRequestChallenges();
            base.Show();
        }

        public override void Hide()
        {
            base.Hide();
        }

        public override void Update()
        {
            if (!Refresh)
            {
                return;
            }

            Console.WriteLine(Progress);
        }
    }

    public static class CharacterChallengesController
    {
        private static List<WeaponTypeProgress> _weaponTypeProgresses = new List<WeaponTypeProgress>();
        public static List<WeaponTypeProgress> WeaponTypeProgresses 
        { 
            get => _weaponTypeProgresses; 
            set 
            {
                _weaponTypeProgresses = value.OrderBy(v => WeaponTypeDescriptor.GetName(v.WeaponTypeId)).ToList();
                Refresh = true;
            } 
        }

        public static bool Refresh { get; set; }
    }
}
