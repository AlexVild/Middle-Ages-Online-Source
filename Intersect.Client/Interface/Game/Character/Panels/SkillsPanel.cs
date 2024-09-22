using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Interface.Components;
using Intersect.Client.Interface.Game.Components;
using Intersect.Client.Networking;
using Intersect.Client.Utilities;
using Intersect.GameObjects;
using Intersect.Network.Packets.Server;
using Intersect.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Interface.Game.Character.Panels
{
    public static class SkillsPanelController
    {
        public static readonly string AllSkillsText = "All";
        
        public static readonly string OtherSkillsText = "Other";

        public static List<string> AvailableSkillTypes = new List<string>();

        public static bool RefreshDisplay = false;

        public static int SkillPointsAvailable = 0;
        
        public static int SkillPointTotal = 0;

        public static void RefreshAvailableSkillTypes()
        {
            AvailableSkillTypes.Clear();
            AvailableSkillTypes.Add(AllSkillsText);
            AvailableSkillTypes.Add(OtherSkillsText);

            List<string> skillTypes = new List<string>();
            foreach (var spell in Globals.Me?.Skillbook.Select(kv => SpellBase.Get(kv.Key)))
            {
                if (spell == default)
                {
                    continue;
                }

                if (string.IsNullOrEmpty(spell.SpellGroup))
                {
                    continue;
                }

                if (skillTypes.Contains(spell.SpellGroup))
                {
                    continue;
                }

                skillTypes.Add(spell.SpellGroup);
            }

            skillTypes.Sort();

            AvailableSkillTypes.AddRange(skillTypes);
        }
    }

    public class SkillsPanel : CharacterWindowPanel
    {
        private Label SearchLabel { get; set; }
        private ImagePanel SearchContainer { get; set; }
        private ImagePanel SearchBg { get; set; }
        private TextBox SearchBar { get; set; }
        private string SearchTerm
        {
            get => SearchBar.Text;
            set => SearchBar.SetText(value);
        }
        private Button SearchClearButton { get; set; }

        private ImagePanel SkillTypeBackground { get; set; }
        private Label SkillTypeLabel { get; set; }
        private ComboBox SkillTypeSelection { get; set; }

        private ImagePanel SkillsContainer { get; set; }
        private ScrollControl SkillsScrollContainer { get; set; }
        private ComponentList<GwenComponent> SkillRows { get; set; } = new ComponentList<GwenComponent>();

        private Label SkillPointsRemaining;

        private CheckBox ShowHideLowerSkills { get; set; }

        public SkillsPanel(ImagePanel characterWindow)
        {
            mParentContainer = characterWindow;
            mBackground = new ImagePanel(mParentContainer, "CharacterWindowMAO_Skills");

            SearchContainer = new ImagePanel(mBackground, "SearchContainer");
            SearchLabel = new Label(SearchContainer, "SearchLabel")
            {
                Text = "Search:"
            };
            SearchBg = new ImagePanel(SearchContainer, "SearchBg");
            SearchBar = new TextBox(SearchBg, "SearchBar");
            SearchBar.TextChanged += SearchBar_TextChanged;

            SearchClearButton = new Button(SearchContainer, "ClearButton")
            {
                Text = "CLEAR"
            };
            SearchClearButton.Clicked += SearchClearButton_Clicked;

            SkillTypeBackground = new ImagePanel(mBackground, "SkillTypeBackground");
            SkillTypeLabel = new Label(SkillTypeBackground, "SkillTypeLabel");
            SkillTypeLabel.SetText("Skill Type");
            SkillTypeSelection = new ComboBox(SkillTypeBackground, "SkillTypeComboBox");
            SkillTypeSelection.ItemSelected += SkillTypeSelection_ItemSelected;

            SkillsContainer = new ImagePanel(mBackground, "SkillsContainer");
            SkillsScrollContainer = new ScrollControl(SkillsContainer, "SkillsScrollContainer");

            SkillPointsRemaining = new Label(SkillsContainer, "SkillPointsReamining");
            ShowHideLowerSkills = new CheckBox(mBackground, "ShowHideLowerSkills")
            {
                Text = ""
            };

            ShowHideLowerSkills.CheckChanged += ShowHideLowerSkills_Checked;

            mBackground.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

            ShowHideLowerSkills.SetTooltipGraphicsMAO();
            ShowHideLowerSkills.SetToolTipText("When checked, shows all skills even if you know a better version of that skill");
        }

        private void ShowHideLowerSkills_Checked(Base sender, EventArgs arguments)
        {
            RefreshSkillbookDisplay();
        }

        public override void Show()
        {
            RefreshSkillTypeFilters();
            Interface.InputBlockingElements.Add(SearchBar);
            Globals.SkillPointUpdate = false;
            SearchTerm = string.Empty;

            RefreshSkillbookDisplay();

            base.Show();
        }

        private void RefreshSkillTypeFilters()
        {
            SkillTypeSelection.RemoveMenuItems();
            foreach (string skillType in SkillsPanelController.AvailableSkillTypes.ToArray())
            {
                SkillTypeSelection.AddItem(skillType);
            }
        }

        private void RefreshSkillbookDisplay()
        {
            ClearSkills();

            if (SkillsPanelController.SkillPointTotal == 0)
            {
                SkillPointsRemaining.Hide();
            }
            else
            {
                SkillPointsRemaining.SetText($"Skill Points: {SkillsPanelController.SkillPointsAvailable} / {SkillsPanelController.SkillPointTotal}");
                if (SkillsPanelController.SkillPointsAvailable == 0)
                {
                    SkillPointsRemaining.SetTextColor(new Color(255, 169, 169, 169), Label.ControlState.Normal);
                }
                else
                {
                    SkillPointsRemaining.SetTextColor(new Color(255, 255, 255, 255), Label.ControlState.Normal);
                }

                SkillPointsRemaining.Show();
            }

            var idx = 0;
            foreach(var skillKv in Globals.Me?.Skillbook.OrderBy(kv => SpellBase.GetName(kv.Key)).ToArray())
            {
                var descriptor = SpellBase.Get(skillKv.Key);
                if (descriptor == default)
                {
                    continue;
                }

                // Filter by spell group
                if (SkillTypeSelection.SelectedItem.Text != SkillsPanelController.AllSkillsText)
                {
                    if (SkillTypeSelection.SelectedItem.Text == SkillsPanelController.OtherSkillsText
                        && (descriptor.SpellGroup != SkillsPanelController.OtherSkillsText && !string.IsNullOrEmpty(descriptor.SpellGroup)))
                    {
                        continue;
                    } else if (SkillTypeSelection.SelectedItem.Text != SkillsPanelController.OtherSkillsText && SkillTypeSelection.SelectedItem.Text != descriptor.SpellGroup)
                    {
                        continue;
                    }
                }

                // Filter by search term
                if (!SearchHelper.IsSearchable(descriptor.Name, SearchTerm))
                {
                    continue;
                }

                if (!ShowHideLowerSkills.IsChecked && HasHigherLevelInSkillbook(descriptor) && !skillKv.Value.Prepared)
                {
                    continue;
                }

                var row = new SkillRowComponent(
                    SkillsScrollContainer,
                    $"Skill_{idx}",
                    skillKv.Key,
                    skillKv.Value.Prepared,
                    skillKv.Value.PointsRequired,
                    SkillsPanelController.SkillPointsAvailable,
                    SkillRows);

                row.Initialize();
                row.SetPosition(row.X, row.Height * idx);

                if (idx % 2 == 1)
                {
                    row.SetBanding();
                }

                idx++;
            }
        }

        private bool HasHigherLevelInSkillbook(SpellBase skill)
        {
            return (Globals.Me.Skillbook.Any(kv =>
            {
                if (!SpellBase.TryGet(kv.Key, out var compSpell))
                {
                    return false;
                }

                return compSpell.UpgradeOfSpellId == skill.Id;
            }));
        }

        private void ClearSkills()
        {
            SkillsScrollContainer?.ClearCreatedChildren();
            SkillRows?.DisposeAll();
        }

        public override void Hide()
        {
            Interface.InputBlockingElements.Remove(SearchBar);
            ClearSkills();
            
            base.Hide();
        }

        public override void Update()
        {
            if (SkillsPanelController.RefreshDisplay)
            {
                RefreshSkillbookDisplay();
                SkillsPanelController.RefreshDisplay = false;
            }
        }

        private void SearchBar_TextChanged(Base sender, EventArgs arguments)
        {
            if (SearchTerm.Length >= 3 || string.IsNullOrEmpty(SearchTerm))
            {
                RefreshSkillbookDisplay();
            }
        }

        private void SearchClearButton_Clicked(Base sender, Framework.Gwen.Control.EventArguments.ClickedEventArgs arguments)
        {
            SearchTerm = string.Empty;
            RefreshSkillbookDisplay();
        }

        private void SkillTypeSelection_ItemSelected(Base sender, Framework.Gwen.Control.EventArguments.ItemSelectedEventArgs arguments)
        {
            RefreshSkillbookDisplay();
        }
    }
}
