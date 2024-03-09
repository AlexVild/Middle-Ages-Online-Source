using System;

using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Client.General;
using Intersect.Client.Localization;
using System.Collections.Generic;
using Intersect.Utilities;
using System.Linq;

namespace Intersect.Client.Interface.Game.DescriptionWindows
{
    public class SpellDescriptionWindow : DescriptionWindowBase
    {
        protected SpellBase mSpell;

        readonly Color StatLabelColor = CustomColors.ItemDesc.Muted;
        readonly Color StatValueColor = CustomColors.ItemDesc.Primary;
        readonly Color StatHeaderColor = Color.White;
        readonly Color WeaponInheritColor = CustomColors.ItemDesc.WeaponType;

        private bool mAbridged { get; set; }

        bool IsPassive => mSpell != null && mSpell.SpellType == SpellTypes.Passive;

        public SpellDescriptionWindow(Guid spellId, int x, int y, bool abridged = false) : base(Interface.GameUi.GameCanvas, "DescriptionWindow")
        {
            mSpell = SpellBase.Get(spellId);
            mAbridged = abridged;

            GenerateComponents();
            SetupDescriptionWindow();
            SetPosition(x, y);
        }

        protected void SetupDescriptionWindow()
        {
            if (mSpell == null)
            {
                return;
            }

            // Set up our header information.
            SetupHeader();

            // if we have a description, set that up.
            if (!string.IsNullOrWhiteSpace(mSpell.Description) && !mAbridged)
            {
                SetupDescription();
            }

            if (mSpell.CastingComponents.Count > 0 && !mAbridged)
            {
                SetupComponentInfo();
            }

            // Set up requirements
            if (mSpell.RestrictionStrings.Count > 0 && !mAbridged)
            {
                SetupRestrictionInfo();
            }

            // Set up our basic spell info.
            SetupSpellInfo();

            // Set up information depending on the item type.
            switch (mSpell.SpellType)
            {
                case SpellTypes.CombatSpell:
                    if (mSpell.Combat?.Projectile?.Spell != default)
                    {
                        SetupCombatInfo(title: "Impact Info");
                        SetupCombatInfo(mSpell.Combat.Projectile.Spell, "AoE Info");
                    }
                    else
                    {
                        SetupCombatInfo();
                    }

                    // Add weapon type disclaimer
                    if (!IsPassive && mSpell.WeaponSpell)
                    {
                        AddDivider();
                        var disclaimer = AddDescription();
                        disclaimer.AddText(Strings.SpellDescription.WeaponSkill, CustomColors.ItemDesc.WeaponType);
                        disclaimer.AddLineBreak();
                    }
                    break;

                case SpellTypes.WarpTo:
                case SpellTypes.Passive:
                    SetupCombatInfo();
                    break;
                case SpellTypes.Dash:
                    if (mSpell.Dash.Spell != default)
                    {
                        SetupCombatInfo(mSpell.Dash.Spell);
                    }
                    SetupDashInfo();
                    break;
            }

            // Resize the container, correct the display and position our window.
            FinalizeWindow();
        }

        protected void SetupHeader()
        {
            // Create our header, but do not load our layout yet since we're adding components manually.
            var header = AddHeader();

            // Set up the icon, if we can load it.
            var tex = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Spell, mSpell.Icon);
            if (tex != null)
            {
                header.SetIcon(tex, Color.White);
            }

            // Set up the header as the item name.
            header.SetTitle(mSpell.Name, Color.White);

            // Set up the spell type description.
            header.SetSubtitle(mSpell.SpellType.GetDescription(), Color.White);

            // Set up the spelldescription based on what kind of spell it is.
            if (mSpell.SpellType == (int)SpellTypes.CombatSpell)
            {
                if (mSpell.Combat.TargetType == SpellTargetTypes.Projectile)
                {
                    var proj = ProjectileBase.Get(mSpell.Combat.ProjectileId);
                    header.SetDescription(Strings.SpellDescription.TargetTypes[(int)mSpell.Combat.TargetType].ToString(proj?.Range ?? 0, mSpell.Combat.HitRadius), Color.White);
                }
                else
                {
                    header.SetDescription(Strings.SpellDescription.TargetTypes[(int)mSpell.Combat.TargetType].ToString(mSpell.Combat.CastRange, mSpell.Combat.HitRadius), Color.White);
                }
            }

            header.SizeToChildren(true, false);
        }

        protected void SetupSpellInfo()
        {
            if (IsPassive)
            {
                return;
            }

            // Add a divider.
            AddDivider();
            
            // Add a new row control to add our details into.
            var rows = AddRowContainer();

            // Friendly / Non Friendly for combat spells.
            if (mSpell.SpellType == SpellTypes.CombatSpell || mSpell.SpellType == SpellTypes.WarpTo)
            {
                if (mSpell.Combat.Friendly)
                {
                    rows.AddKeyValueRow($"   {Strings.SpellDescription.Friendly}", string.Empty, StatValueColor, StatValueColor);
                }
                else
                {
                    rows.AddKeyValueRow($"   {Strings.SpellDescription.Unfriendly}", string.Empty, StatValueColor, StatValueColor);
                }
            }

            if (mSpell.SpellType == SpellTypes.Dash)
            {
                rows.AddKeyValueRow($"   Spell Info", string.Empty, StatValueColor, StatValueColor);
            }

            var castTime = Strings.SpellDescription.Instant;
            if (mSpell.CastDuration > 0)
            {
                castTime = Strings.SpellDescription.Seconds.ToString(mSpell.CastDuration / 1000f);
            }
            rows.AddKeyValueRow(Strings.SpellDescription.CastTime, castTime, StatLabelColor, StatValueColor);

            // Add Vital Costs
            for (var i = 0; i < (int)Vitals.VitalCount; i++)
            {
                if (mSpell.VitalCost[i] != 0)
                {
                    rows.AddKeyValueRow(Strings.SpellDescription.VitalCosts[i], mSpell.VitalCost[i].ToString(), StatLabelColor, StatValueColor);
                }
            }

            // Add Cooldown time
            if (mSpell.CooldownDuration > 0)
            {
                var cdr = (Globals.Me.GetBonusEffect(EffectType.CooldownReduction) / 100f);

                if (!mSpell.IgnoreCooldownReduction)
                {
                    var cooldown = (int)Math.Round(mSpell.CooldownDuration - mSpell.CooldownDuration * cdr);

                    var valueColor = StatValueColor;
                    if (cooldown != mSpell.CooldownDuration)
                    {
                        valueColor = WeaponInheritColor;
                    }

                    rows.AddKeyValueRow(Strings.SpellDescription.Cooldown, Strings.SpellDescription.Seconds.ToString(cooldown / 1000f), StatLabelColor, valueColor);
                }
                else
                {
                    rows.AddKeyValueRow(Strings.SpellDescription.Cooldown, Strings.SpellDescription.Seconds.ToString(mSpell.CooldownDuration / 1000f), StatLabelColor, StatValueColor);
                }
            }

            // Ignores global cooldown if enabled?
            if (Options.Instance.CombatOpts.EnableGlobalCooldowns && mSpell.IgnoreGlobalCooldown)
            {
                rows.AddKeyValueRow(Strings.SpellDescription.IgnoreGlobalCooldown, string.Empty, CustomColors.ItemDesc.Notice, StatValueColor);
            }

            // Ignore cooldown reduction stat?
            if (mSpell.IgnoreCooldownReduction)
            {
                rows.AddKeyValueRow(Strings.SpellDescription.IgnoreCooldownReduction, string.Empty, CustomColors.ItemDesc.Notice, StatValueColor);
            }

            if (!string.IsNullOrWhiteSpace(mSpell.SpellGroup))
            {
                rows.AddKeyValueRow(Strings.SpellDescription.SpellGroup, mSpell.SpellGroup, StatLabelColor, StatValueColor);
            }

            // Resize the container.
            rows.SizeToChildren(true, true);
        }

        protected void SetupDescription()
        {
            // Add a divider.
            AddDivider();

            // Add the actual description.
            var description = AddDescription();
            description.AddText(Strings.ItemDescription.Description.ToString(mSpell.Description), Color.White);
        }

        protected void SetupCombatInfo(SpellBase spellOverride = null, string title = "Combat Info")
        {
            var spell = mSpell;
            if (spellOverride != null)
            {
                spell = spellOverride;
            }
            if (spell == null)
            {
                return;
            }

            SpellBase projectileSpell = null;
            if (spell.Combat.TargetType == SpellTargetTypes.Projectile)
            {
                projectileSpell = spell.Combat.Projectile.Spell;
            }

            // Add a divider.
            AddDivider();

            // Add a row component.
            var rows = AddRowContainer();
 
            var isHeal = false;
            var isDamage = false;
            var showDuration = false;

            Globals.Me.TryGetEquippedWeaponDescriptor(out var equippedWeapon);

            if (!IsPassive)
            {
                rows.AddKeyValueRow($"   {title}", string.Empty, StatValueColor, StatValueColor);

                var damageTypeIdx = 0;

                var attackTypes = CombatUtilities.GetSpellAttackTypes(spell, equippedWeapon);
                foreach (var attackType in attackTypes)
                {
                    var valueColor = spell.WeaponSpell && !spell.Combat.DamageTypes.Contains(attackType) ? WeaponInheritColor : StatValueColor;

                    var notice = string.Empty;
                    if (spell.DamageOverrides.TryGetValue((int)attackType, out var ovrride) && ovrride != 0)
                    {
                        valueColor = CustomColors.ItemDesc.Notice;
                        notice = " (static)";
                    }

                    if (damageTypeIdx == 0)
                    {
                        rows.AddKeyValueRow("Damage Types", $"{attackType.GetDescription()}{notice}", StatLabelColor, valueColor);
                    }
                    else
                    {
                        rows.AddKeyValueRow(string.Empty, $"{attackType.GetDescription()}{notice}", StatLabelColor, valueColor);
                    }
                    damageTypeIdx++;
                }

                var projectileTimes = 1;
                var projectilePierce = false;
                var projectile = spell.Combat.Projectile;
                if (spell.Combat.Projectile != default)
                {
                    projectileTimes = projectile.Quantity;
                    projectilePierce = projectile.PierceTarget;
                }

                // Health damage if TrueDamage
                if (spell.Combat.DamageType == (int)DamageType.True)
                {
                    if (projectileTimes > 1)
                    {
                        var healthDamage = (int)Math.Round(spell.Combat.VitalDiff[(int)Vitals.Health] * (spell.Combat.Scaling / 100f));
                        if (healthDamage < 0)
                        {
                            rows.AddKeyValueRow(Strings.SpellDescription.VitalRecovery[(int)Vitals.Health], $"{Math.Abs(healthDamage)} x {projectileTimes}", StatLabelColor, StatValueColor);
                            isHeal = true;
                        }
                        else if (healthDamage > 0)
                        {
                            rows.AddKeyValueRow(Strings.SpellDescription.VitalDamage[(int)Vitals.Health], $"{healthDamage} x {projectileTimes}", StatLabelColor, StatValueColor);
                            isDamage = true;
                        }
                    }
                    else
                    {
                        var healthDamage = spell.Combat.VitalDiff[(int)Vitals.Health];
                        if (healthDamage < 0)
                        {
                            rows.AddKeyValueRow(Strings.SpellDescription.VitalRecovery[(int)Vitals.Health], Math.Abs(healthDamage).ToString(), StatLabelColor, StatValueColor);
                            isHeal = true;
                        }
                        else if (healthDamage > 0)
                        {
                            rows.AddKeyValueRow(Strings.SpellDescription.VitalDamage[(int)Vitals.Health], healthDamage.ToString(), StatLabelColor, StatValueColor);
                            isDamage = true;
                        }
                    }
                }
                // Otherwise, calculate damage
                else
                {
                    var valueColor = spell.WeaponSpell ? WeaponInheritColor : StatValueColor;
                    CombatUtilities.CalculateDamage(attackTypes, 1.0, spell.Combat.Scaling, CombatUtilities.GetOverriddenStats(spell.DamageOverrides, Globals.Me.Stat), new int[(int)Stats.StatCount], out var healthDamage);

                    if (projectileTimes > 1)
                    {
                        if (healthDamage < 0)
                        {
                            rows.AddKeyValueRow(Strings.SpellDescription.VitalRecovery[(int)Vitals.Health], $"{Math.Abs(healthDamage)} x {projectileTimes}", StatLabelColor, valueColor);
                            isHeal = true;
                        }
                        else if (healthDamage > 0)
                        {
                            rows.AddKeyValueRow(Strings.SpellDescription.VitalDamage[(int)Vitals.Health], $"{healthDamage} x {projectileTimes}", StatLabelColor, valueColor);
                            isDamage = true;
                        }
                    }
                    else
                    {
                        if (healthDamage < 0)
                        {
                            rows.AddKeyValueRow(Strings.SpellDescription.VitalRecovery[(int)Vitals.Health], Math.Abs(healthDamage).ToString(), StatLabelColor, valueColor);
                            isHeal = true;
                        }
                        else if (healthDamage > 0)
                        {
                            rows.AddKeyValueRow(Strings.SpellDescription.VitalDamage[(int)Vitals.Health], healthDamage.ToString(), StatLabelColor, valueColor);
                            isDamage = true;
                        }
                    }
                }

                if (mSpell.Combat.LifeSteal)
                {
                    rows.AddKeyValueRow("Lifesteal", string.Empty, CustomColors.ItemDesc.Special, StatValueColor);
                }

                if (projectilePierce)
                {
                    rows.AddKeyValueRow("Pierces Targets", string.Empty, StatLabelColor, Color.White);
                }

                if (spell.Combat.HitRadius > 0)
                {
                    rows.AddKeyValueRow("AoE Radius", spell.Combat.HitRadius.ToString(), StatLabelColor, StatValueColor);
                }

                // Mana Damage - always "True"
                var manaDamage = (int)Math.Round(spell.Combat.VitalDiff[(int)Vitals.Mana] * (spell.Combat.Scaling / 100f));
                if (manaDamage > 0)
                {
                    isDamage = true;
                    rows.AddKeyValueRow(Strings.SpellDescription.VitalDamage[(int)Vitals.Mana], manaDamage.ToString(), StatLabelColor, StatValueColor);
                }
                else if (manaDamage < 0)
                {
                    isHeal = true;
                    rows.AddKeyValueRow(Strings.SpellDescription.VitalRecovery[(int)Vitals.Mana], Math.Abs(manaDamage).ToString(), StatLabelColor, StatValueColor);
                }
                
                if (mSpell.Combat.ManaSteal)
                {
                    rows.AddKeyValueRow("Manasteal", string.Empty, CustomColors.ItemDesc.Special, StatValueColor);
                }

                // Crit Chance
                if (spell.Combat.CritChance > 0)
                {
                    var valueColor = spell.WeaponSpell ? WeaponInheritColor : StatValueColor;

                    var critChance = spell.Combat.CritChance;
                    var critMulti = spell.Combat.CritMultiplier;
                    if (spell.WeaponSpell)
                    {
                        critChance += equippedWeapon?.CritChance ?? 0;
                        critMulti += equippedWeapon?.CritMultiplier ?? 0;
                    }

                    rows.AddKeyValueRow(Strings.SpellDescription.CritChance, Strings.SpellDescription.Percentage.ToString(critChance), StatLabelColor, valueColor);
                    rows.AddKeyValueRow(Strings.SpellDescription.CritMultiplier, Strings.SpellDescription.Multiplier.ToString(critMulti), StatLabelColor, valueColor);
                }
            }
            
            // Handle Stat Buffs
            var blankAdded = false;
            for (var i = 0; i < (int)Stats.StatCount; i++)
            {
                Tuple<string, string> data = null;
                if (spell.Combat.StatDiff[i] != 0 && spell.Combat.PercentageStatDiff[i] != 0)
                {
                    data = new Tuple<string, string>(Strings.SpellDescription.StatCounts[i], Strings.SpellDescription.RegularAndPercentage.ToString(spell.Combat.StatDiff[i], spell.Combat.PercentageStatDiff[i]));
                }
                else if (spell.Combat.StatDiff[i] != 0)
                {
                    data = new Tuple<string, string>(Strings.SpellDescription.StatCounts[i], spell.Combat.StatDiff[i].ToString());
                }
                else if (spell.Combat.PercentageStatDiff[i] != 0)
                {
                    data = new Tuple<string, string>(Strings.SpellDescription.StatCounts[i], Strings.ItemDescription.Percentage.ToString(spell.Combat.PercentageStatDiff[i]));
                }

                // Make sure we only add a blank row the first time we add a stat row.
                if (data != null)
                {
                    if (!blankAdded)
                    {
                        rows.AddKeyValueRow(string.Empty, string.Empty);
                        rows.AddKeyValueRow($"   Stat Mods", string.Empty, StatValueColor, StatValueColor);
                        blankAdded = true;
                    }

                    rows.AddKeyValueRow(data.Item1, data.Item2, StatLabelColor, StatValueColor);
                    showDuration = true;
                }
            }
            if (spell.BonusEffects.Count > 0)
            {
                rows.AddKeyValueRow(string.Empty, string.Empty);
                foreach (var effect in spell.ActiveEffects)
                {
                    rows.AddKeyValueRow($"{effect.Type.GetDescription()}", $"{(effect.Percentage > 0 ? "" : "-")}{effect.Percentage}%", StatLabelColor, StatValueColor);
                }
                showDuration = true;
            }

            // Handle HoT and DoT displays.
            if (!IsPassive && spell.Combat.HoTDoT)
            {
                showDuration = true;
                rows.AddKeyValueRow(string.Empty, string.Empty);
                if (isHeal)
                {
                    rows.AddKeyValueRow(Strings.SpellDescription.HoT, Strings.SpellDescription.Seconds.ToString(spell.Combat.HotDotInterval / 1000f), StatLabelColor, StatValueColor);
                } 
                else if (isDamage)
                {
                    rows.AddKeyValueRow(Strings.SpellDescription.DoT, Strings.SpellDescription.Seconds.ToString(spell.Combat.HotDotInterval / 1000f), StatLabelColor, StatValueColor);
                }
            }

            // Handle effect display.
            if (spell.Combat.Effect != StatusTypes.None)
            {
                showDuration = true;
                rows.AddKeyValueRow(string.Empty, string.Empty);
                rows.AddKeyValueRow($"{Strings.SpellDescription.Effect}", $"{Strings.SpellDescription.Effects[(int)spell.Combat.Effect]}", StatLabelColor, CustomColors.ItemDesc.Notice);
            }

            // Show Stat Buff / Effect / HoT / DoT duration.
            if (showDuration && !IsPassive)
            {
                rows.AddKeyValueRow(Strings.SpellDescription.Duration, Strings.SpellDescription.Seconds.ToString(spell.Combat.Duration / 1000f), StatLabelColor, StatValueColor);
            }

            if (spell.Combat.TargetType == SpellTargetTypes.OnHit)
            {
                if (spell.Combat.PersistMissedAttack)
                {
                    rows.AddKeyValueRow("Persist missed attacks", string.Empty, CustomColors.ItemDesc.Notice, Color.White);
                }
                if (spell.Combat.PersistWeaponSwap)
                {
                    rows.AddKeyValueRow("Persist weapon change", string.Empty, CustomColors.ItemDesc.Notice, Color.White);
                }
            }

            // Resize and position the container.
            rows.SizeToChildren(true, true);
        }

        protected void SetupDashInfo()
        {
            // Add a divider.
            AddDivider();

            // Add a row component.
            var rows = AddRowContainer();

            rows.AddKeyValueRow("   Dash", string.Empty, StatValueColor, StatValueColor);

            // Dash Distance Information.
            rows.AddKeyValueRow(Strings.SpellDescription.Distance, Strings.SpellDescription.Tiles.ToString(mSpell.Combat.CastRange), StatLabelColor, StatValueColor);

            // Ignore map blocks?
           /* if (mSpell.Dash.IgnoreMapBlocks)
            {
                rows.AddKeyValueRow(Strings.SpellDescription.IgnoreMapBlock, String.Empty);
            }

            // Ignore resource blocks?
            if (mSpell.Dash.IgnoreActiveResources)
            {
                rows.AddKeyValueRow(Strings.SpellDescription.IgnoreResourceBlock, String.Empty);
            }

            // Ignore inactive resource blocks?
            if (mSpell.Dash.IgnoreInactiveResources)
            {
                rows.AddKeyValueRow(Strings.SpellDescription.IgnoreConsumedResourceBlock, String.Empty);
            }

            // Ignore Z-Dimension?
            if (Options.Map.ZDimensionVisible && mSpell.Dash.IgnoreZDimensionAttributes)
            {
                rows.AddKeyValueRow(Strings.SpellDescription.IgnoreZDimension, String.Empty);
            }*/

            // Resize and position the container.
            rows.SizeToChildren(true, true);
        }

        protected void SetupExtraInfo()
        {
            // Display only if this spell is bound.
            if (mSpell.Bound)
            {
                // Add a divider.
                AddDivider();

                // Add a row component.
                var rows = AddRowContainer();

                // Display shop value.
                rows.AddKeyValueRow(Strings.SpellDescription.Bound, string.Empty, StatLabelColor, StatValueColor);

                // Resize and position the container.
                rows.SizeToChildren(true, true);
            }
        }

        protected void SetupRestrictionInfo()
        {
            var description = AddDescription();

            description.AddText(Strings.ItemDescription.Restriction, CustomColors.ItemDesc.Notice);
            description.AddLineBreak();
            description.AddText(string.Join(" ", mSpell.RestrictionStrings), CustomColors.ItemDesc.Notice);
        }

        protected void SetupComponentInfo()
        {
            // Our list of data to add, should we need to.
            var data = new List<Tuple<string, string>>();

            // Display each condition list as returned to us by the server
            data.Add(new Tuple<string, string>(Strings.ItemDescription.ComponentsNeeded, string.Empty));
            foreach (var component in mSpell.GetComponentDisplay())
            {
                data.Add(new Tuple<string, string>(component, string.Empty));
            }

            // Do we have any data to display? If so, generate the element and add the data to it.
            if (data.Count > 0)
            {
                // Add a divider.
                AddDivider();

                // Add a row component.
                var rows = AddRowContainer();

                foreach (var item in data)
                {
                    rows.AddKeyValueRow(item.Item1, item.Item2, CustomColors.ItemDesc.Notice, Color.White);
                }

                // Resize and position the container.
                rows.SizeToChildren(true, true);
            }
        }
    }
}
