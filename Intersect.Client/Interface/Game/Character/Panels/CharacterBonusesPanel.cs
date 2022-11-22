using Intersect.Client.Core;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Interface.Components;
using Intersect.Client.Interface.Game.Components;
using Intersect.Client.Localization;
using Intersect.Enums;
using Intersect.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intersect.Client.Interface.Game.Character.Panels
{
    public struct CharacterBonusInfo
    {
        public string Name;
        public string Description;

        public CharacterBonusInfo(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }

    public static class CharacterBonusesPanelController
    {
        public static bool Refresh { get; set; } = false;
    }

    public class CharacterBonusesPanel : CharacterWindowPanel
    {
        public CharacterPanelType Type = CharacterPanelType.Bonuses;

        readonly Dictionary<EffectType, CharacterBonusInfo> BonusEffects = new Dictionary<EffectType, CharacterBonusInfo>
        {
            {EffectType.None, new CharacterBonusInfo("None", "N/A")},
            {EffectType.CooldownReduction, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.CooldownReduction], "Reduces length of skill cooldowns.")}, // Cooldown Reduction
            {EffectType.Lifesteal, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.Lifesteal], "Gives life as a percentage of damage dealt.")}, // Lifesteal
            {EffectType.Tenacity, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.Tenacity], "Reduces negative status effect duration.")}, // Tenacity
            {EffectType.Luck, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.Luck], "Increases chances for extra mob loot, ammo recovery.")}, // Luck
            {EffectType.EXP, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.EXP], "Grants extra EXP when earned.")}, // Bonus Experience
            {EffectType.Affinity, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.Affinity], "Increases crit chance.")}, // Affinity
            {EffectType.CritBonus, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.CritBonus], "Increases crit damage bonus.")}, // Critical bonus
            {EffectType.Swiftness, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.Swiftness], "Increases weapon attack speed.")}, // Swiftness
        };

        private ImagePanel BonusBackground { get; set; }

        private ScrollControl BonusContainer { get; set; }

        private ComponentList<GwenComponent> BonusRows = new ComponentList<GwenComponent>();

        private Label NoBonusesLabel { get; set; }
        private const string NoBonusesText = "No bonuses active...";

        private const int YPadding = 32;

        private bool Refresh => CharacterBonusesPanelController.Refresh;

        public CharacterBonusesPanel(ImagePanel panelBackground)
        {
            mParentContainer = panelBackground;
            mBackground = new ImagePanel(mParentContainer, "CharacterWindowMAO_Bonuses");

            BonusBackground = new ImagePanel(mBackground, "BonusContainerBackground");
            NoBonusesLabel = new Label(BonusBackground, "NoBonusesMessage")
            {
                Text = NoBonusesText
            };

            BonusContainer = new ScrollControl(BonusBackground, "BonusContainer");

            mBackground.LoadJsonUi(Framework.File_Management.GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        }

        public override void Show()
        {
            RefreshValues();
            base.Show();
        }

        public override void Hide()
        {
            ClearBonusRows();
            base.Hide();
        }

        private void ClearBonusRows()
        {
            BonusRows?.DisposeAll();
            BonusContainer?.ClearCreatedChildren();
        }

        private Tuple<float, double> GetCritInfo()
        {
            var critChance = 0.0f;
            var critMulti = 0.0;

            var equippedWeaponSlot = Globals.Me.MyEquipment[Options.Equipment.WeaponSlot];
            if (equippedWeaponSlot != -1 && equippedWeaponSlot < Globals.Me.Inventory.Length)
            {
                var equippedWeapon = Globals.Me.Inventory[equippedWeaponSlot];
                critChance = equippedWeapon.Base.CritChance;
                critMulti = equippedWeapon.Base.CritMultiplier;
            }
            else
            {
                var cls = ClassBase.Get(Globals.Me.Class);
                if (cls == null)
                {
                    new Tuple<float, double>(critChance, critMulti);
                }

                critChance = cls.CritChance;
                critMulti = cls.CritMultiplier;
            }

            return new Tuple<float, double>(critChance, critMulti);
        }

        public void RefreshValues()
        {
            ClearBonusRows();

            if (Globals.Me == null)
            {
                NoBonusesLabel.Show();
                return;
            }

            var critInfo = GetCritInfo();
            var bonusEffects = Globals.Me?.GetAllBonusEffects();
            if (critInfo == null && bonusEffects == null)
            {
                NoBonusesLabel.Show();
                return;
            }

            NoBonusesLabel.Hide();

            var yStart = 0;
            AddCritInfo(critInfo, yStart, out var y);
            AddBonusEffectInfo(bonusEffects, y, out _);

            BonusRows.InitializeAll();
        }

        private void AddCritInfo(Tuple<float, double> critInfo, int yStart, out int yEnd)
        {
            yEnd = yStart;
            var critChance = $"{critInfo.Item1.ToString("N2")}%";
            var critMulti = $"{critInfo.Item2.ToString("N2")}x";

            var critChanceRow = new CharacterBonusRow(BonusContainer, "CritChanceRow", "Base Crit Chance", critChance, "Your base chance to critical hit.", BonusRows);
            critChanceRow.SetPosition(critChanceRow.X, critChanceRow.Y + yStart);

            var critMultiRow = new CharacterBonusRow(BonusContainer, "CritMultiRow", "Base Crit Multi.", critMulti, "Base damage multiplier for critical hits.", BonusRows);
            critChanceRow.SetPosition(critChanceRow.X, critChanceRow.Y + yStart + YPadding);

            yEnd = critChanceRow.Y + YPadding;
        }

        private void AddBonusEffectInfo(Dictionary<EffectType, int> bonusEffects, int yStart, out int yEnd)
        {
            var idx = 0;
            
            yEnd = yStart;
            
            foreach (var effectMapping in bonusEffects)
            {
                var effect = effectMapping.Key;
                var amount = effectMapping.Value;

                if (!BonusEffects.ContainsKey(effect))
                {
                    continue;
                }

                var effectName = BonusEffects[effect].Name.ToString().Split(':').FirstOrDefault();
                var tooltip = BonusEffects[effect].Description;

                var row = new CharacterBonusRow(BonusContainer, "BonusRow", effectName, $"{amount}%", tooltip, BonusRows);

                row.SetPosition(row.X, row.Y + (YPadding * idx) + yStart);

                idx++;
            }

            yEnd = yStart + (idx * YPadding);
        }

        public override void Update()
        {
            if (Refresh)
            {
                RefreshValues();
                CharacterBonusesPanelController.Refresh = false;
            }
        }
    }

    public class CharacterBonusRow : GwenComponent
    {
        private Label Bonus { get; set; }
        private string BonusText { get; set; }

        private Label Separator { get; set; }
        const string SeparatorText = "...";

        private Label Percentage { get; set; }
        private string Value { get; set; }

        private ImagePanel Background { get; set; }

        private string TooltipText { get; set; }

        private Color TextColor => new Color(255, 255, 255, 255);
        private Color TextHoveredColor => new Color(255, 169, 169, 169);

        public int X => ParentContainer?.X ?? default;

        public int Y => ParentContainer?.Y ?? default;

        public CharacterBonusRow(
            Base parent,
            string containerName,
            string bonus,
            string value,
            string tooltip,
            ComponentList<GwenComponent> referenceList = null) : base(parent, containerName, "BonusRowComponent", referenceList)
        {
            BonusText = bonus;
            Value = value;
            TooltipText = tooltip;
        }

        public override void Initialize()
        {
            SelfContainer = new ImagePanel(ParentContainer, ComponentName);
            SelfContainer.HoverEnter += SelfContainer_HoverEnter;
            SelfContainer.HoverLeave += SelfContainer_HoverLeave;
            SelfContainer.SetToolTipText(TooltipText);

            var percent = $"{Value}";
            Percentage = new Label(SelfContainer, "Percentage")
            {
                Text = percent
            };
            
            Separator = new Label(SelfContainer, "Separator")
            {
                Text = SeparatorText
            };
            
            Bonus = new Label(SelfContainer, "BonusName")
            {
                Text = BonusText
            };

            base.Initialize();
            FitParentToComponent();

            Bonus.SetTextColor(TextColor, Label.ControlState.Normal);
            Bonus.SetTextColor(TextHoveredColor, Label.ControlState.Hovered);
        }

        private void SelfContainer_HoverEnter(Base sender, EventArgs arguments)
        {
            Bonus.SetTextColor(TextHoveredColor, Label.ControlState.Normal);
            Separator.SetTextColor(TextHoveredColor, Label.ControlState.Normal);
            Percentage.SetTextColor(TextHoveredColor, Label.ControlState.Normal);
        }

        private void SelfContainer_HoverLeave(Base sender, EventArgs arguments)
        {
            Bonus.SetTextColor(TextColor, Label.ControlState.Normal);
            Separator.SetTextColor(TextColor, Label.ControlState.Normal);
            Percentage.SetTextColor(TextColor, Label.ControlState.Normal);
        }

        public void SetPosition(int x, int y)
        {
            ParentContainer?.SetPosition(x, y);
        }
    }
}
