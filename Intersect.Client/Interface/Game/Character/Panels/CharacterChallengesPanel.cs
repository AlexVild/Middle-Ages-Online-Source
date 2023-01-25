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

        public WeaponTypeProgress SelectedProgress => Progress?.Find(progress => WeaponTypeDescriptor.GetName(progress.WeaponTypeId) == SkillTypeSelection.SelectedItem.Text) ?? default;

        private Label NoTrackLabel { get; set; }

        private ImagePanel SkillTypeBackground { get; set; }
        private Label SkillTypeLabel { get; set; }
        private ComboBox SkillTypeSelection { get; set; }

        private ImagePanel TrackProgressBackground { get; set; }
        private WeaponTrackProgressBarComponent TrackProgressBar { get; set; }

        private ImagePanel ChallengesBackground { get; set; }
        private ScrollControl ChallengesContainer { get; set; }

        private ComponentList<IGwenComponent> Components { get; set; } = new ComponentList<IGwenComponent>();

        public CharacterChallengesPanel(ImagePanel panelBackground)
        {
            mParentContainer = panelBackground;
            mBackground = new ImagePanel(mParentContainer, "CharacterWindowMAO_Challenges");

            NoTrackLabel = new Label(mBackground, "NoTrackLabel")
            {
                Text = "Equip weapons to unlock weapon tracks."
            };

            SkillTypeBackground = new ImagePanel(mBackground, "SkillTypeBackground");
            SkillTypeLabel = new Label(SkillTypeBackground, "SkillTypeLabel");
            SkillTypeLabel.SetText("Weapon Track");
            SkillTypeSelection = new ComboBox(SkillTypeBackground, "SkillTypeComboBox");
            SkillTypeSelection.ItemSelected += SkillTypeSelection_ItemSelected;

            TrackProgressBackground = new ImagePanel(mBackground, "TrackProgress");
            TrackProgressBar = new WeaponTrackProgressBarComponent(TrackProgressBackground,
                "WeaponTrackProgressBar",
                "weapon_track_progress_bar_bg.png",
                "weapon_track_progress_bar_fg.png",
                "Lvl.",
                Color.White,
                "EXP. Remaining: ",
                new Color(255, 180, 180, 180),
                referenceList: Components);

            ChallengesBackground = new ImagePanel(mBackground, "ChallengesBackground");
            ChallengesContainer = new ScrollControl(ChallengesBackground, "ChallengesContainer");

            mBackground.LoadJsonUi(Framework.File_Management.GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

            TrackProgressBar.Initialize();
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
            NoTrackLabel.IsHidden = Progress.Count > 0;
            SkillTypeBackground.IsHidden = Progress.Count <= 0;
            TrackProgressBackground.IsHidden = Progress.Count <= 0;
            ChallengesBackground.IsHidden = Progress.Count <= 0;

            if (!Refresh)
            {
                return;
            }

            //do stuff
            RefreshTrackOptions();
            RefreshTrackProgression();

            Refresh = false;
        }

        void RefreshTrackOptions()
        {
            SkillTypeSelection.RemoveMenuItems();
            foreach (string skillType in Progress.ToArray().Select(weaponType => WeaponTypeDescriptor.GetName(weaponType.WeaponTypeId)))
            {
                SkillTypeSelection.AddItem(skillType);
            }
        }

        void RefreshTrackProgression()
        {
            if (SelectedProgress == default)
            {
                return;
            }

            var descriptor = WeaponTypeDescriptor.Get(SelectedProgress.WeaponTypeId);
            if (descriptor == default)
            {
                return;
            }

            descriptor.Unlocks.TryGetValue(SelectedProgress.Level, out var currentUnlock);
            var hasUnlock = descriptor.Unlocks.TryGetValue(SelectedProgress.Level + 1, out var nextUnlock);

            TrackProgressBar.SetLabelText(ProgressBarLabel.Top, $"{WeaponTypeDescriptor.GetName(SelectedProgress.WeaponTypeId)} Level: {SelectedProgress.Level}");
            
            // Max level?
            if (!hasUnlock)
            {
                TrackProgressBar.SetLabelText(ProgressBarLabel.Bottom, "MAX");
                TrackProgressBar.Percent = 100;
                TrackProgressBackground.SetToolTipText("You've completed this weapon track!");
                return;
            }

            // Still have progress to make?
            var progressPercent = SelectedProgress.Exp / (float)nextUnlock.RequiredExp;
            var remaining = nextUnlock.RequiredExp - SelectedProgress.Exp;
            TrackProgressBar.Percent = progressPercent;

            if (progressPercent < 1)
            {
                TrackProgressBar.SetLabelText(ProgressBarLabel.Bottom, $"EXP remaining: {remaining}");
                TrackProgressBackground.SetToolTipText("Earn EXP using this weapon type to advance");
            }
            else
            {
                var challenges = nextUnlock.ChallengeIds.Select(id => ChallengeDescriptor.GetName(id)).ToArray();
                TrackProgressBar.SetLabelText(ProgressBarLabel.Bottom, $"Awaiting challenges: {string.Join(", ", challenges)}");
                TrackProgressBackground.SetToolTipText("You have an uncompleted challenge preventing track advancement");
            }

            TrackProgressBar.Update();
        }

        private void SkillTypeSelection_ItemSelected(Base sender, Framework.Gwen.Control.EventArguments.ItemSelectedEventArgs arguments)
        {
            Refresh = true;
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
