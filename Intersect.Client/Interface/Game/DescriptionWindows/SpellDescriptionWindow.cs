using System;

using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Client.General;
using Intersect.Client.Localization;
using System.Collections.Generic;
using Intersect.Utilities;

namespace Intersect.Client.Interface.Game.DescriptionWindows
{
    public class SpellDescriptionWindow : DescriptionWindowBase
    {
        protected SpellBase mSpell;

        bool IsPassive => mSpell != null && mSpell.SpellType == SpellTypes.Passive;

        public SpellDescriptionWindow(Guid spellId, int x, int y) : base(Interface.GameUi.GameCanvas, "DescriptionWindow")
        {
            mSpell = SpellBase.Get(spellId);

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
            if (!string.IsNullOrWhiteSpace(mSpell.Description))
            {
                SetupDescription();
            }

            if (mSpell.CastingComponents.Count > 0)
            {
                SetupComponentInfo();
            }

            // Set up requirements
            if (mSpell.RestrictionStrings.Count > 0)
            {
                SetupRestrictionInfo();
            }

            // Set up our basic spell info.
            SetupSpellInfo();

            // Set up information depending on the item type.
            switch (mSpell.SpellType)
            {
                case SpellTypes.CombatSpell:
                case SpellTypes.WarpTo:
                case SpellTypes.Passive:
                    SetupCombatInfo();
                    break;
                case SpellTypes.Dash:
                    SetupDashInfo();
                    break;
            }

            // Set up bind info, if applicable.
            SetupExtraInfo();
            

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
                    rows.AddKeyValueRow(Strings.SpellDescription.Friendly, string.Empty);
                }
                else
                {
                    rows.AddKeyValueRow(Strings.SpellDescription.Unfriendly, string.Empty);
                }
            }

            var castTime = Strings.SpellDescription.Instant;
            if (mSpell.CastDuration > 0)
            {
                castTime = Strings.SpellDescription.Seconds.ToString(mSpell.CastDuration / 1000f);
            }
            rows.AddKeyValueRow(Strings.SpellDescription.CastTime, castTime);

            // Add Vital Costs
            for (var i = 0; i < (int)Vitals.VitalCount; i++)
            {
                if (mSpell.VitalCost[i] != 0)
                {
                    rows.AddKeyValueRow(Strings.SpellDescription.VitalCosts[i], mSpell.VitalCost[i].ToString());
                }
            }

            // Add Cooldown time
            if (mSpell.CooldownDuration > 0)
            {
                rows.AddKeyValueRow(Strings.SpellDescription.Cooldown, Strings.SpellDescription.Seconds.ToString(mSpell.CooldownDuration / 1000f));
            }

            // Add Cooldown Group
            // ALEX - disable
            /*
            if (!string.IsNullOrWhiteSpace(mSpell.CooldownGroup))
            {
                rows.AddKeyValueRow(Strings.SpellDescription.CooldownGroup, mSpell.CooldownGroup);
            }
            */

            // Ignores global cooldown if enabled?
            if (Options.Instance.CombatOpts.EnableGlobalCooldowns && mSpell.IgnoreGlobalCooldown)
            {
                rows.AddKeyValueRow(Strings.SpellDescription.IgnoreGlobalCooldown, string.Empty);
            }

            // Ignore cooldown reduction stat?
            if (mSpell.IgnoreCooldownReduction)
            {
                rows.AddKeyValueRow(Strings.SpellDescription.IgnoreCooldownReduction, string.Empty);
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

        protected void SetupCombatInfo(SpellBase spellOverride = null)
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

            // Add a divider.
            AddDivider();

            // Add a row component.
            var rows = AddRowContainer();
 
            var isHeal = false;
            var isDamage = false;
            var showDuration = false;
            if (!IsPassive)
            {
                // Vital Damage, if 0 don't display!
                for (var i = 0; i < (int)Vitals.VitalCount; i++)
                {
                    if (spell.Combat.VitalDiff[i] < 0)
                    {
                        rows.AddKeyValueRow(Strings.SpellDescription.VitalRecovery[i], Math.Abs(spell.Combat.VitalDiff[i]).ToString());
                        isHeal = true;
                    }
                    else if (spell.Combat.VitalDiff[i] > 0)
                    {
                        rows.AddKeyValueRow(Strings.SpellDescription.VitalDamage[i], spell.Combat.VitalDiff[i].ToString());
                        isDamage = true;
                    }
                }

                // Damage Type:
                Strings.SpellDescription.DamageTypes.TryGetValue(spell.Combat.DamageType, out var damageType);
                rows.AddKeyValueRow(Strings.SpellDescription.DamageType, damageType);

                if (spell.Combat.Scaling > 0)
                {
                    Strings.SpellDescription.Stats.TryGetValue(spell.Combat.ScalingStat, out var stat);
                    rows.AddKeyValueRow(Strings.SpellDescription.ScalingStat, stat);
                    rows.AddKeyValueRow(Strings.SpellDescription.ScalingPercentage, Strings.SpellDescription.Percentage.ToString(spell.Combat.Scaling));
                }

                // Crit Chance
                if (spell.Combat.CritChance > 0)
                {
                    rows.AddKeyValueRow(Strings.SpellDescription.CritChance, Strings.SpellDescription.Percentage.ToString(spell.Combat.CritChance));
                    rows.AddKeyValueRow(Strings.SpellDescription.CritMultiplier, Strings.SpellDescription.Multiplier.ToString(spell.Combat.CritMultiplier));
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
                        rows.AddKeyValueRow(Strings.SpellDescription.StatBuff, string.Empty);
                        blankAdded = true;
                    }

                    rows.AddKeyValueRow(data.Item1, data.Item2);
                    showDuration = true;
                }
            }

            // Handle HoT and DoT displays.
            if (!IsPassive && spell.Combat.HoTDoT)
            {
                showDuration = true;
                rows.AddKeyValueRow(string.Empty, string.Empty);
                if (isHeal)
                {
                    rows.AddKeyValueRow(Strings.SpellDescription.HoT, string.Empty);
                } 
                else if (isDamage)
                {
                    rows.AddKeyValueRow(Strings.SpellDescription.DoT, string.Empty);
                }
                rows.AddKeyValueRow(Strings.SpellDescription.Tick, Strings.SpellDescription.Seconds.ToString(spell.Combat.HotDotInterval / 1000f));
            }

            // Handle effect display.
            if (spell.Combat.Effect != StatusTypes.None)
            {
                showDuration = true;
                rows.AddKeyValueRow(string.Empty, string.Empty);
                rows.AddKeyValueRow(Strings.SpellDescription.Effect, Strings.SpellDescription.Effects[(int) spell.Combat.Effect]);
            }

            // Show Stat Buff / Effect / HoT / DoT duration.
            if (showDuration && !IsPassive)
            {
                rows.AddKeyValueRow(Strings.SpellDescription.Duration, Strings.SpellDescription.Seconds.ToString(spell.Combat.Duration / 1000f));
            }

            if (spell.WeaponSpell)
            {
                rows.AddKeyValueRow(Strings.SpellDescription.WeaponSkill, string.Empty, CustomColors.ItemDesc.Special, Color.White);
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

            // Dash Distance Information.
            rows.AddKeyValueRow(Strings.SpellDescription.Distance, Strings.SpellDescription.Tiles.ToString(mSpell.Combat.CastRange));

            // Ignore map blocks?
            if (mSpell.Dash.IgnoreMapBlocks)
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
            }

            if (mSpell.Dash.Spell != null)
            {
                SetupCombatInfo();
            }

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
                rows.AddKeyValueRow(Strings.SpellDescription.Bound, string.Empty);

                // Resize and position the container.
                rows.SizeToChildren(true, true);
            }
        }

        protected void SetupRestrictionInfo()
        {
            // Our list of data to add, should we need to.
            var data = new List<Tuple<string, string>>();

            // Display each condition list as returned to us by the server
            data.Add(new Tuple<string, string>(Strings.ItemDescription.Restriction, String.Empty));
            for (var i = 0; i < mSpell.RestrictionStrings.Count; i++)
            {
                var restriction = mSpell.RestrictionStrings[i];
                if (i == 0)
                {
                    data.Add(new Tuple<string, string>(restriction, String.Empty));
                }
                else
                {
                    data.Add(new Tuple<string, string>(Strings.ItemDescription.RestrictionOr.ToString(restriction), String.Empty));
                }
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
