using Intersect.Client.Core;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Interface.Components;
using Intersect.Client.Interface.Game.Components;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Client.Utilities;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Utilities;
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

        Dictionary<EffectType, CharacterBonusInfo> BonusEffects => BonusEffectHelper.BonusEffectDescriptions;

        private ImagePanel BonusBackground { get; set; }

        private ScrollControl BonusContainer { get; set; }

        private ComponentList<GwenComponent> BonusRows = new ComponentList<GwenComponent>();

        private Label NoBonusesLabel { get; set; }
        private const string NoBonusesText = "No bonuses active...";

        private const int YPadding = 32;

        private bool Refresh => CharacterBonusesPanelController.Refresh;

        protected ImagePanel PassivesContainer { get; set; }
        protected Label PassivesLabel { get; set; }
        protected Label NoPassivesLabel { get; set; }
        protected ScrollControl PassivesScrollContainer { get; set; }
        protected ComponentList<GwenComponent> Passives { get; set; } = new ComponentList<GwenComponent>();

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

            PassivesContainer = new ImagePanel(mBackground, "PassivesContainer");
            PassivesLabel = new Label(mBackground, "PassivesLabel")
            {
                Text = "PASSIVE SKILLS"
            };
            NoPassivesLabel = new Label(PassivesContainer, "NoPassivesLabel")
            {
                Text = "No passive skills are currently active."
            };
            PassivesScrollContainer = new ScrollControl(PassivesContainer, "PassivesScrollContainer");

            mBackground.LoadJsonUi(Framework.File_Management.GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        }

        public override void Show()
        {
            PacketSender.SendRequestChallengeBonuses();
            RefreshValues();
            base.Show();
        }

        public override void Hide()
        {
            ClearBonusRows();
            ClearPassiveRows();
            base.Hide();
        }

        private void ClearBonusRows()
        {
            BonusRows?.DisposeAll();
            BonusContainer?.ClearCreatedChildren();
        }

        private void ClearPassiveRows()
        {
            Passives?.DisposeAll();
            PassivesScrollContainer?.ClearCreatedChildren();
        }

        /// <summary>
        /// Gets critical hit info
        /// </summary>
        /// <returns>Two tuples. The first is your unmodified critical hit & bonus values, the second is your values w/ modifiers applied</returns>
        private Tuple<Tuple<float, double>, Tuple<float, double>> GetCritInfo()
        {
            var baseCritChance = 0.0f;
            var baseCritMulti = 0.0;

            Tuple<float, double> baseCritInfo;
            Tuple<float, double> modifiedCritInfo;

            var equippedWeaponSlot = Globals.Me.MyEquipment[Options.Equipment.WeaponSlot];
            if (equippedWeaponSlot != -1 && equippedWeaponSlot < Globals.Me.Inventory.Length)
            {
                var equippedWeapon = Globals.Me.Inventory[equippedWeaponSlot];
                baseCritChance = equippedWeapon.Base.CritChance;
                baseCritMulti = equippedWeapon.Base.CritMultiplier;
            }
            else
            {
                var cls = ClassBase.Get(Globals.Me.Class);
                if (cls == null)
                {
                    baseCritInfo = new Tuple<float, double>(baseCritChance, baseCritMulti);
                }
                else
                {
                    baseCritChance = cls.CritChance;
                    baseCritMulti = cls.CritMultiplier;
                }
            }

            baseCritInfo = new Tuple<float, double>(baseCritChance, baseCritMulti);

            // A weapon with a crit chance of 0 can _not_ critical hit
            if (baseCritChance == 0)
            {
                modifiedCritInfo = new Tuple<float, double>(baseCritChance, baseCritMulti);
            }
            else
            {
                var affinity = Globals.Me.GetBonusEffect(EffectType.Affinity);
                var accuracy = Globals.Me.Stat[(int)Stats.Accuracy];
                var critBonus = Globals.Me.GetBonusEffect(EffectType.CritBonus);
                
                var modifiedCritChance = baseCritChance + CombatUtilities.GetCritChanceBonusValue(affinity, accuracy);
                var modifiedCritBonus = baseCritMulti * (1 + (critBonus / 100f));
                modifiedCritInfo = new Tuple<float, double>(modifiedCritChance, modifiedCritBonus);
            }

            return new Tuple<Tuple<float, double>, Tuple<float, double>>(baseCritInfo, modifiedCritInfo);
        }

        private string GetAttackSpeed()
        {
            int attackSpeedMs = 0;

            var equippedWeaponSlot = Globals.Me.MyEquipment[Options.Equipment.WeaponSlot];
            if (equippedWeaponSlot != -1 && equippedWeaponSlot < Globals.Me.Inventory.Length)
            {
                var equippedWeapon = Globals.Me.Inventory[equippedWeaponSlot];
                attackSpeedMs = equippedWeapon.Base.AttackSpeedValue;
            }
            else
            {
                var cls = ClassBase.Get(Globals.Me.Class);
                if (cls == null)
                {
                    return null;
                }
                attackSpeedMs = cls.AttackSpeedValue;
            }

            var speed = attackSpeedMs / 1000f;

            return $"{speed.ToString("N2")}s";
        }

        public void RefreshValues()
        {
            ClearBonusRows();

            if (Globals.Me == null)
            {
                NoBonusesLabel.Show();
                return;
            }

            var baseCritInfo = GetCritInfo();
            var attackSpeed = GetAttackSpeed();
            var bonusEffects = Globals.Me?.GetAllBonusEffects();

            NoBonusesLabel.Hide();
            var yStart = 0;
            AddVitalRegenInfo(yStart, out var y);
            AddCritInfo(baseCritInfo, y, out y);
            AddAttackSpeed(attackSpeed, y, out y);
            AddBonusEffectInfo(bonusEffects, y, out _);

            BonusRows.InitializeAll();

            RefreshPassiveSkillsDisplay();
        }

        public void RefreshPassiveSkillsDisplay()
        {
            if (Globals.Me?.ActivePassives?.Count <= 0)
            {
                NoPassivesLabel.Show();
                return;
            }

            NoPassivesLabel.Hide();

            ClearPassiveRows();

            var idx = 0;
            foreach (var passive in Globals.Me?.ActivePassives.OrderBy(id => SpellBase.GetName(id)).ToArray())
            {
                var row = new PassiveRowComponent(
                    PassivesScrollContainer,
                    $"Passive_{idx}",
                    passive,
                    Passives);

                row.Initialize();
                row.SetPosition(row.X, row.Height * idx);

                if (idx % 2 == 1)
                {
                    row.SetBanding();
                }

                Passives.Add(row);

                idx++;
            }
        }

        private void AddVitalRegenInfo(int yStart, out int yEnd)
        {
            yEnd = yStart;

            var regens = Globals.Me.GetVitalRegens();
            var speed = Globals.Me.Speed;

            foreach (var vital in regens.Keys)
            {
                if (regens[vital] == 0)
                {
                    continue;
                }

                var label = "OoC HP Regen";
                var helper = $"The percent of health regenerated every regen tick out of combat";
                if (vital == Vitals.Mana)
                {
                    label = "MP Regen";
                    helper = $"The percent of mana regenerated every regen tick";
                }

                var regenRow = new CharacterBonusRow(BonusContainer, "VitalRegenRow", label, $"{regens[vital]}%", helper, BonusRows);
                regenRow.SetPosition(regenRow.X, regenRow.Y + yEnd);

                yEnd = regenRow.Y + YPadding;
            }

            foreach (Vitals vital in Enum.GetValues(typeof(Vitals)))
            {
                if (vital == Vitals.VitalCount)
                {
                    continue;
                }

                var label = "HP OoC Regen Rate";
                var time = CombatUtilities.RecoveryTimeMs(speed, vital) / 1000f;
                var helper = $"The length in time of a health regen tick";
                if (vital == Vitals.Mana)
                {
                    label = "MP Regen Rate";
                    helper = $"The length in time of a mana regen tick";
                }

                var regenRow = new CharacterBonusRow(BonusContainer, "VitalRegenSpeedRow", label, $"{time.ToString("N1")}s", helper, BonusRows);
                regenRow.SetPosition(regenRow.X, regenRow.Y + yEnd);

                yEnd = regenRow.Y + YPadding;
            }

            var evasionStatusBonus = CombatUtilities.GetEvasionStatusAvoidancePercent(Globals.Me.Stat[(int)Stats.Evasion]);
            if (evasionStatusBonus > 0)
            {
                var regenRow = new CharacterBonusRow(BonusContainer, "EvasionResistBonusRow", "Evade Status", $"{evasionStatusBonus}%", "Chance to avoid negative status element due to evasion", BonusRows);
                regenRow.SetPosition(regenRow.X, regenRow.Y + yEnd);
                yEnd = regenRow.Y + YPadding;
            }
        }

        private void AddCritInfo(Tuple<Tuple<float, double>, Tuple<float, double>> critInfo, int yStart, out int yEnd)
        {
            yEnd = yStart;
            if (critInfo == null)
            {
                return;
            }

            var baseCritChance = critInfo.Item1.Item1;
            var modifiedCritChance = critInfo.Item2.Item1;

            var baseCritMulti = critInfo.Item1.Item2;
            var modifiedCritMulti = critInfo.Item2.Item2;

            var critChanceRow = new CharacterBonusRow(BonusContainer, "CritChanceRow", "Base Crit Chance", $"{baseCritChance.ToString("N0")}%", "Your base chance to critical hit.", BonusRows);
            critChanceRow.SetPosition(critChanceRow.X, critChanceRow.Y + yStart);

            if (modifiedCritChance != baseCritChance)
            {
                yStart += YPadding;
                var critChanceModRow = new CharacterBonusRow(BonusContainer, "CritChanceModRow", "Total Crit Chance", $"{modifiedCritChance.ToString("N0")}%", "Your total chance to critical hit, including affinity + accuracy modifiers.", BonusRows);
                critChanceModRow.SetPosition(critChanceModRow.X, critChanceModRow.Y + yStart);
            }

            yStart += YPadding;
            var critMultiRow = new CharacterBonusRow(BonusContainer, "CritMultiRow", "Base Crit Multi.", $"{baseCritMulti.ToString("N2")}x", "Base damage multiplier for critical hits.", BonusRows);
            critMultiRow.SetPosition(critMultiRow.X, critMultiRow.Y + yStart);

            if (modifiedCritMulti != baseCritMulti)
            {
                yStart += YPadding;
                var critMultiModRow = new CharacterBonusRow(BonusContainer, "CritMultiModRow", "Total Crit Multi.", $"{modifiedCritMulti.ToString("N0")}x", "Total damage multiplier for critical hits, including crit bonus.", BonusRows);
                critMultiModRow.SetPosition(critMultiModRow.X, critMultiModRow.Y + yStart);
                
                yEnd = critMultiModRow.Y + YPadding;
            }
            else
            {
                yEnd = critMultiRow.Y + YPadding;
            }
        }

        private void AddBonusEffectInfo(Dictionary<EffectType, int> bonusEffects, int yStart, out int yEnd)
        {
            var idx = 0;
            yEnd = yStart;

            if (bonusEffects == null || bonusEffects.Count == 0)
            {
                return;
            }
            
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

        private void AddAttackSpeed(string atkSpeed, int yStart, out int yEnd)
        {
            yEnd = yStart;
            if (atkSpeed == null)
            {
                return;
            }

            var attackSpeedRow = new CharacterBonusRow(BonusContainer, "AtkSpeedRow", "Base Atk. Speed", atkSpeed, "Your base attack speed.", BonusRows);
            attackSpeedRow.SetPosition(attackSpeedRow.X, attackSpeedRow.Y + yStart);

            yEnd = attackSpeedRow.Y + YPadding;
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
        public int Width => ParentContainer?.Width ?? default;
        public int Height => ParentContainer?.Height ?? default;

        public CharacterBonusRow(
            Base parent,
            string containerName,
            string bonus,
            string value,
            string tooltip,
            ComponentList<GwenComponent> referenceList = null,
            string fileName = "BonusRowComponent") : base(parent, containerName, fileName, referenceList)
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

        public void SetSize(int width, int height)
        {
            ParentContainer?.SetSize(width, height);
            ParentContainer?.ProcessAlignments();
        }
    }
}
