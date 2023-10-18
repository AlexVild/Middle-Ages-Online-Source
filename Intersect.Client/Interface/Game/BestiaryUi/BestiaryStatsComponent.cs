using Intersect.Client.Framework.Graphics;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Interface.Components;
using Intersect.Client.Interface.Game.Character.Panels;
using Intersect.Client.Interface.Game.Components;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intersect.Client.Interface.Game.BestiaryUi
{
    public class BestiaryStatsComponent : BestiaryComponent
    {
        private NpcBase Beast { get; set; }
        private readonly Color AtkLabelColor = new Color(255, 222, 124, 112);
        private readonly Color DefLabelColor = new Color(255, 105, 158, 252);
        private readonly Color StatColor = new Color(255, 255, 255, 255);
        private readonly Color StatLabelColor = new Color(255, 50, 19, 0);
        private readonly Color StatLabelHoverColor = new Color(255, 111, 63, 0);
        private readonly Color StatInnerLabelColor = new Color(255, 166, 167, 37);

        public override GameTexture UnlockedBg => Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "bestiary_stats.png");
        public override GameTexture LockedBg => Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "bestiary_stats_locked.png");
        public GameTexture UnlockedCombatBg => Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "bestiary_combat.png");
        public GameTexture LockedCombatBg => Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "bestiary_combat_locked.png");
        public GameTexture UnlockedResistancesBg => Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "bestiary_resistances.png");
        public GameTexture LockedResistancesBg => Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "bestiary_resistances_locked.png");

        private ImagePanel CombatStatsContainer { get; set; }
        private NumberContainerComponent BluntAttack { get; set; }
        private NumberContainerComponent BluntDefense { get; set; }
        private NumberContainerComponent SlashAttack { get; set; }
        private NumberContainerComponent SlashDefense { get; set; }
        private NumberContainerComponent PierceAttack { get; set; }
        private NumberContainerComponent PierceDefense { get; set; }
        private NumberContainerComponent MagicAttack { get; set; }
        private NumberContainerComponent MagicDefense { get; set; }
        
        private ImagePanel StatsContainer { get; set; }
        private ImageLabelComponent EvasionLabel { get; set; }
        private NumberContainerComponent EvasionNumber { get; set; }
        private ImageLabelComponent AccuracyLabel { get; set; }
        private NumberContainerComponent AccuracyNumber { get; set; }
        private ImageLabelComponent SpeedLabel { get; set; }
        private NumberContainerComponent SpeedNumber { get; set; }

        private ImagePanel BonusContainer { get; set; }
        private Label BonusLabel { get; set; }
        private ScrollControl BonusScrollContainer { get; set; }
        private ComponentList<GwenComponent> BonusInfo { get; set; } = new ComponentList<GwenComponent>();

        private ComponentList<IGwenComponent> Components { get; set; } = new ComponentList<IGwenComponent>();

        public BestiaryStatsComponent(Base parent, 
            string containerName,
            ComponentList<GwenComponent> referenceList = null) : base(parent, containerName, "BestiaryStatsComponent", referenceList)
        {
        }

        public override void Initialize()
        {
            SelfContainer = new ImagePanel(ParentContainer, ComponentName);

            CombatStatsContainer = new ImagePanel(SelfContainer, "CombatStatsContainer");
            BluntAttack = new NumberContainerComponent(CombatStatsContainer, "BluntAttack", AtkLabelColor, StatColor, "ATK", "The enemy's blunt attack power.", Components);
            BluntDefense = new NumberContainerComponent(CombatStatsContainer, "BluntDefense", DefLabelColor, StatColor, "DEF", "The enemy's blunt resistance.", Components);
            SlashAttack = new NumberContainerComponent(CombatStatsContainer, "SlashAttack", AtkLabelColor, StatColor, "ATK", "The enemy's slashing attack power.", Components);
            SlashDefense = new NumberContainerComponent(CombatStatsContainer, "SlashDefense", DefLabelColor, StatColor, "DEF", "The enemy's slash resistance.", Components);
            PierceAttack = new NumberContainerComponent(CombatStatsContainer, "PierceAttack", AtkLabelColor, StatColor, "ATK", "The enemy's piercing attack power.", Components);
            PierceDefense = new NumberContainerComponent(CombatStatsContainer, "PierceDefense", DefLabelColor, StatColor, "DEF", "The enemy's pierce resistance.", Components);
            MagicAttack = new NumberContainerComponent(CombatStatsContainer, "MagicAttack", AtkLabelColor, StatColor, "ATK", "The enemy's magic attack power.", Components);
            MagicDefense = new NumberContainerComponent(CombatStatsContainer, "MagicDefense", DefLabelColor, StatColor, "DEF", "The enemy's magic resistance.", Components);

            StatsContainer = new ImagePanel(SelfContainer, "StatsContainer");
            EvasionLabel = new ImageLabelComponent(StatsContainer, "EvasionLabel", StatLabelColor, StatLabelHoverColor, "character_stats_evasion.png", "EVASION", "Affects the enemy's dodge chance.", Components);
            EvasionNumber = new NumberContainerComponent(StatsContainer, "Evasion", StatInnerLabelColor, StatColor, "MAX", string.Empty, Components);
            AccuracyLabel = new ImageLabelComponent(StatsContainer, "AccuracyLabel", StatLabelColor, StatLabelHoverColor, "character_stats_accuracy.png", "ACCURACY", "Affects the chance to hit vs evasion.", Components);
            AccuracyNumber = new NumberContainerComponent(StatsContainer, "Accuracy", StatInnerLabelColor, StatColor, "MAX", string.Empty, Components);
            SpeedLabel = new ImageLabelComponent(StatsContainer, "SpeedLabel", StatLabelColor, StatLabelHoverColor, "character_stats_speed.png", "SPEED", "Affects the enemy's movement speed.", Components);
            SpeedNumber = new NumberContainerComponent(StatsContainer, "Speed", StatInnerLabelColor, StatColor, "MAX", string.Empty, Components);

            BonusContainer = new ImagePanel(SelfContainer, "ResistancesContainer");
            BonusLabel = new Label(BonusContainer, "ResistancesLabel")
            {
                Text = "EXTRA INFO"
            };
            BonusScrollContainer = new ScrollControl(BonusContainer, "ResistancesScrollContainer");

            base.Initialize();
            FitParentToComponent();

            Components.InitializeAll();
        }

        public override void Dispose()
        {
            Components.DisposeAll();
            ClearResistances();
            
            base.Dispose();
        }

        public override void SetUnlockStatus(bool unlocked)
        {
            Unlocked = unlocked;

            if (Unlocked)
            {
                StatsContainer.Texture = UnlockedBg;
                CombatStatsContainer.Texture = UnlockedCombatBg;
                BonusContainer.Texture = UnlockedResistancesBg;
                BonusScrollContainer.Show();
                BonusLabel.TextColor = Color.White;
                Components.ShowAll();
                HideLock();
            }
            else
            {
                StatsContainer.Texture = LockedBg;
                CombatStatsContainer.Texture = LockedCombatBg;
                BonusContainer.Texture = LockedResistancesBg;
                BonusLabel.TextColor = new Color(255,82,82,82);
                BonusScrollContainer.Hide();
                Components.HideAll();
                ShowLock();
            }
        }

        public void SetBeast(NpcBase beast, int kc)
        {
            Beast = beast;
            RequiredKillCount = kc;

            LockLabel.SetText(RequirementString);

            var stats = beast.Stats;

            foreach (var stat in Enum.GetValues(typeof(Stats)))
            {
                if ((int)stat >= (int)Stats.StatCount)
                {
                    continue;
                }

                var idx = (int)stat;
                var val = stats[idx].ToString();

                switch(stat)
                {
                    case Stats.Attack:
                        BluntAttack.SetValue(val);
                        break;
                    case Stats.Defense:
                        BluntDefense.SetValue(val);
                        break;
                    case Stats.AbilityPower:
                        MagicAttack.SetValue(val);
                        break;
                    case Stats.MagicResist:
                        MagicDefense.SetValue(val);
                        break;
                    case Stats.SlashAttack:
                        SlashAttack.SetValue(val);
                        break;
                    case Stats.SlashResistance:
                        SlashDefense.SetValue(val);
                        break;
                    case Stats.PierceAttack:
                        PierceAttack.SetValue(val);
                        break;
                    case Stats.PierceResistance:
                        PierceDefense.SetValue(val);
                        break;
                    case Stats.Evasion:
                        EvasionNumber.SetValue(val);
                        break;
                    case Stats.Accuracy:
                        AccuracyNumber.SetValue(val);
                        break;
                    case Stats.Speed:
                        SpeedNumber.SetValue(val);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(stat));
                }

                var immunities = beast.Immunities.Where(imm => imm.Value == true).Select(imm => imm.Key);
                var tenacity = beast.Tenacity;
                var exp = Options.Instance.CombatOpts.UseGeneratedMobExp ? NpcExperienceCalculator.Calculate(beast) : beast.Experience;

                ClearResistances();
                var y = 16;
                
                if (tenacity > 0)
                {
                    AddBonusRow($"Tenacity: {tenacity.ToString("N1")}%", string.Empty, "Reduces negative effect duration.", ref y);
                }

                if (exp > 0)
                {
                    AddBonusRow($"{exp.ToString("N0")} Exp.", string.Empty, "Base experience given.", ref y);
                }

                foreach(var resistance in immunities)
                {
                    AddBonusRow($"Immune: {resistance.GetDescription()}", string.Empty, string.Empty, ref y);
                }

                foreach (var attackType in beast.AttackTypes)
                {
                    AddBonusRow($"{attackType.GetDescription()} damage", string.Empty, string.Empty, ref y);
                }
                if (beast.IsSpellcaster && !beast.AttackTypes.Contains(AttackTypes.Magic))
                {
                    AddBonusRow($"{AttackTypes.Magic.GetDescription()} damage", string.Empty, string.Empty, ref y);
                }

                if (beast.MeleeLifesteal != 0)
                {
                    AddBonusRow($"Lifesteal: {beast.MeleeLifesteal}%", string.Empty, string.Empty, ref y);
                }

                if (beast.MeleeManasteal != 0)
                {
                    AddBonusRow($"Manasteal: {beast.MeleeManasteal}%", string.Empty, string.Empty, ref y);
                }
            }
        }

        private void AddBonusRow(string label, string value, string toolTip, ref int y)
        {
            var tmpRow = new CharacterBonusRow(BonusScrollContainer, string.Empty, label, value, toolTip, BonusInfo);
            tmpRow.SetPosition(0, y);
            tmpRow.SetSize(BonusScrollContainer.GetContentWidth(), tmpRow.Height);
            tmpRow.Initialize();
            y += 24;
        }

        private void ClearResistances()
        {
            BonusInfo.Clear();
            BonusScrollContainer.ClearCreatedChildren();
        }
    }
}
