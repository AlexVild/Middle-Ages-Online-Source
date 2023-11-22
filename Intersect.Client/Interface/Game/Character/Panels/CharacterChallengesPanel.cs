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

        public WeaponTypeProgress SelectedProgress => Progress?.Find(progress => WeaponTypeDescriptor.GetName(progress.WeaponTypeId) == (SkillTypeSelection?.SelectedItem?.Text ?? string.Empty)) ?? default;

        private Label NoTrackLabel { get; set; }

        private ImagePanel SkillTypeBackground { get; set; }
        private Label SkillTypeLabel { get; set; }
        private ComboBox SkillTypeSelection { get; set; }

        private Button TrackSkillButton { get; set; }

        private ImagePanel TrackProgressBackground { get; set; }
        private WeaponTrackProgressBarComponent TrackProgressBar { get; set; }

        private ImagePanel ChallengesBackground { get; set; }
        private ScrollControl ChallengesContainer { get; set; }

        private Label HelpLabel { get; set; }

        private ComponentList<IGwenComponent> Components { get; set; } = new ComponentList<IGwenComponent>();
        private ComponentList<GwenComponent> ChallengeRows { get; set; } = new ComponentList<GwenComponent>();

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

            TrackSkillButton = new Button(mBackground, "TrackButton")
            {
                Text = "Track"
            };
            TrackSkillButton.SetToolTipText("Track/Untrack EXP progress in your EXP bar");
            TrackSkillButton.Clicked += TrackSkillButton_Clicked;

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

            HelpLabel = new Label(mBackground, "HelpText")
            {
                Text = "Hover over icons to see what each challenge unlocks"
            };

            mBackground.LoadJsonUi(Framework.File_Management.GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

            TrackProgressBar.Initialize();

            Globals.Me.ChallengeUpdateDelegate = () =>
            {
                if (Interface.GameUi?.CurrentCharacterPanel == CharacterPanelType.Challenges)
                {
                    PacketSender.SendRequestChallenges();
                }
                Globals.CanEarnWeaponExp = Globals.Me?.CanEarnWeaponExp(Globals.Me.TrackedWeaponTypeId, Globals.Me.TrackedWeaponLevel) ?? false;
                CharacterChallengesController.AwaitingTrackChange = false;
            };
        }

        private void TrackSkillButton_Clicked(Base sender, Framework.Gwen.Control.EventArguments.ClickedEventArgs arguments)
        {
            if (CharacterChallengesController.AwaitingTrackChange)
            {
                return;
            }
            if (Globals.Me.TrackedWeaponTypeId == (SelectedProgress?.WeaponTypeId ?? Guid.Empty))
            {
                PacketSender.SendTrackWeaponProgress(Guid.Empty);
            }
            else
            {
                PacketSender.SendTrackWeaponProgress(SelectedProgress?.WeaponTypeId ?? Guid.Empty);
            }
        }

        public override void Show()
        {
            PacketSender.SendRequestChallenges();
            base.Show();
        }

        public override void Hide()
        {
            ClearChallenges();
            base.Hide();
        }

        public override void Update()
        {
            NoTrackLabel.IsHidden = Progress.Count > 0;
            SkillTypeBackground.IsHidden = Progress.Count <= 0;
            TrackProgressBackground.IsHidden = Progress.Count <= 0;
            ChallengesBackground.IsHidden = Progress.Count <= 0;
            HelpLabel.IsHidden = Progress.Count <= 0;
            TrackSkillButton.IsHidden = Progress.Count <= 0;
            TrackSkillButton.Text = Globals.Me.TrackedWeaponTypeId == (SelectedProgress?.WeaponTypeId ?? Guid.Empty) ? "UNTRACK" : "TRACK";
            TrackSkillButton.IsDisabled = CharacterChallengesController.AwaitingTrackChange;

            if (!Refresh)
            {
                return;
            }

            //do stuff
            RefreshTrackOptions();
            RefreshTrackProgression();
            RefreshChallenges();
            TrackSkillButton.IsHidden = (SelectedProgress?.WeaponTypeId ?? Guid.Empty) != Guid.Empty;

            Refresh = false;
        }

        private void ClearChallenges()
        {
            ChallengesContainer?.ClearCreatedChildren();
            ChallengeRows?.DisposeAll();
        }

        void RefreshChallenges()
        {
            ClearChallenges();

            var weaponTypeDescriptor = WeaponTypeDescriptor.Get(SelectedProgress?.WeaponTypeId ?? Guid.Empty);
            if (weaponTypeDescriptor == default)
            {
                return;
            }

            var idx = 0;
            foreach (var unlock in weaponTypeDescriptor.Unlocks)
            {
                var level = unlock.Key;
                foreach(var challenge in unlock.Value.ChallengeIds)
                {
                    var progress = SelectedProgress.Challenges
                        .Find(c => c.ChallengeId == challenge);

                    var row = new ChallengeRowComponent(
                        ChallengesContainer,
                        $"Challenge_{idx}",
                        challenge,
                        progress,
                        level,
                        SelectedProgress.Level,
                        unlock.Value.RequiredExp - SelectedProgress.Exp,
                        weaponTypeDescriptor.Name ?? "NOT FOUND",
                        weaponTypeDescriptor.Id,
                        ChallengeRows);

                    row.Initialize();
                    row.SetPosition(row.X, row.Height * idx);

                    idx++;
                }
            }
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
                TrackProgressBar.SetBarBg("weapon_track_progress_bg_locked.png");
                TrackProgressBar.SetBarFg("weapon_track_progress_bar_fg_locked.png");
                return;
            }

            descriptor.Unlocks.TryGetValue(SelectedProgress.Level, out var currentUnlock);
            var hasUnlock = descriptor.Unlocks.TryGetValue(SelectedProgress.Level + 1, out var nextUnlock);

            TrackProgressBar.SetLabelText(ProgressBarLabel.Top, $"{WeaponTypeDescriptor.GetName(SelectedProgress.WeaponTypeId)} Level: {SelectedProgress.Level}");

            // Max level?
            if (!hasUnlock)
            {
                TrackProgressBar.SetLabelText(ProgressBarLabel.Bottom, "MAX");
                TrackProgressBar.Percent = 1.0f;
                TrackProgressBackground.SetToolTipText("You've completed this weapon track!");
                TrackProgressBar.SetBarRenderColor(new Color(255, 255, 255, 255));
                TrackProgressBar.SetBarFg("weapon_track_progress_bar_fg_complete.png");
                TrackProgressBar.Update();
                return;
            }

            // Still have progress to make?
            var correctWeaponType = Globals.Me.TryGetEquippedWeaponDescriptor(out var weapon)
                && weapon.WeaponTypes.Contains(descriptor.Id);

            var correctWeaponLvl = true; // disabling this feature for a bit, trying it out
            /*var correctWeaponLvl = weapon != default
                && weapon.MaxWeaponLevels.TryGetValue(descriptor.Id, out var maxWeaponLvl)
                && maxWeaponLvl > SelectedProgress.Level;*/

            var canProgress = correctWeaponLvl && correctWeaponType;

            var progressPercent = SelectedProgress.Exp / (float)nextUnlock.RequiredExp;
            var remaining = nextUnlock.RequiredExp - SelectedProgress.Exp;
            TrackProgressBar.Percent = !hasUnlock ? 1.0f : progressPercent;

            TrackProgressBar.SetBarBg(canProgress ? "weapon_track_progress_bar_bg.png" : "weapon_track_progress_bg_locked.png");
            TrackProgressBar.SetBarFg(canProgress ? "weapon_track_progress_bar_fg.png" : "weapon_track_progress_bar_fg_locked.png");
            if (progressPercent < 1)
            {
                TrackProgressBar.SetLabelText(ProgressBarLabel.Bottom, $"EXP until next challenge: {remaining.ToString("N0")}");
                if (canProgress)
                {
                    TrackProgressBackground.SetToolTipText("Earn EXP using this weapon type to advance");
                }
                else
                {
                    if (!correctWeaponType)
                    {
                        TrackProgressBackground.SetToolTipText("Your current weapon is not of this weapon type!");
                    }
                    else if (!correctWeaponLvl)
                    {
                        TrackProgressBackground.SetToolTipText($"Your current weapon's {descriptor.VisibleName} level is too low to progress this track!");
                    }
                }
                TrackProgressBar.SetBarRenderColor(new Color(255, 255, 255, 255));
            }
            else
            {
                var challenges = nextUnlock.ChallengeIds.Select(id => ChallengeDescriptor.GetName(id)).ToArray();
                TrackProgressBar.SetLabelText(ProgressBarLabel.Bottom, $"Awaiting challenges: {string.Join(", ", challenges)}");
                if (canProgress)
                {
                    TrackProgressBackground.SetToolTipText("You have an uncompleted challenge preventing track advancement");
                }
                else
                {
                    if (!correctWeaponType)
                    {
                        TrackProgressBackground.SetToolTipText("Your current weapon is not of this weapon type!");
                    }
                    else if (!correctWeaponLvl)
                    {
                        TrackProgressBackground.SetToolTipText("Your current weapon is not high enough level to progress this track!");
                    }
                }
                TrackProgressBar.SetBarRenderColor(new Color(100, 255, 255, 255));
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

        public static Guid CurrentContractId { get; set; }

        public static bool Refresh { get; set; }

        public static bool AwaitingTrackChange { get; set; } = false;
    }
}
