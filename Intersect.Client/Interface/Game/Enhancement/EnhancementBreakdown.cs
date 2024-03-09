using Intersect.Client.Core;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.Character.Panels;
using Intersect.Client.Localization;
using Intersect.Client.Utilities;
using Intersect.Enums;
using Intersect.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intersect.Client.Interface.Game.Enhancement
{
    public class EnhancementBreakdown
    {
        EnhancementWindow Parent { get; set; }

        Base GameCanvas { get; set; }

        public ImagePanel Background { get; set; }

        ScrollControl ModContainer { get; set; }

        private const int YPadding = 32;

        Dictionary<Stats, Enhancement<Stats>> StatEnhancements = new Dictionary<Stats, Enhancement<Stats>>();
        Dictionary<EffectType, Enhancement<EffectType>> BonusEnhancements = new Dictionary<EffectType, Enhancement<EffectType>>();
        Dictionary<Vitals, Enhancement<Vitals>> VitalEnhancements = new Dictionary<Vitals, Enhancement<Vitals>>();
        Dictionary<Guid, SpellEnhancementDescriptor> SpellEnhancements = new Dictionary<Guid, SpellEnhancementDescriptor>();

        public EnhancementBreakdown(EnhancementWindow parent, Base gameCanvas)
        {
            Parent = parent;
            GameCanvas = gameCanvas;

            Background = new ImagePanel(GameCanvas, "EnhancementBreakdownWindow");
            ModContainer = new ScrollControl(Background, "ModContainer");

            Background.LoadJsonUi(Framework.File_Management.GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        }

        public void Show()
        {
            Background.Show();
        }

        public void Update()
        {
            if (Background.IsHidden)
            {
                return;
            }

            if (Globals.Me == null || (!Globals.Me.Enhancement?.IsOpen ?? false))
            {
                Hide();
                return;
            }

            Background.SetPosition(Parent.X + Parent.Width, Parent.Y + 40);

            if (!Globals.Me.Enhancement.RefreshUi)
            {
                return;
            }

            var enhancements = Globals.Me.Enhancement.NewEnhancements;

            BonusEnhancements.Clear();
            StatEnhancements.Clear();
            VitalEnhancements.Clear();
            SpellEnhancements.Clear();
            foreach (var enhancement in enhancements)
            {
                var desc = EnhancementDescriptor.Get(enhancement.EnhancementId);

                foreach(var effect in desc.EffectMods)
                {
                    if (BonusEnhancements.ContainsKey(effect.EnhancementType))
                    {
                        BonusEnhancements[effect.EnhancementType].MinValue += effect.MinValue;
                        BonusEnhancements[effect.EnhancementType].MaxValue += effect.MaxValue;
                    } 
                    else
                    {
                        BonusEnhancements[effect.EnhancementType] = new Enhancement<EffectType>(effect.EnhancementType, effect.MinValue, effect.MaxValue);
                    }
                }
                foreach (var effect in desc.StatMods)
                {
                    if (StatEnhancements.ContainsKey(effect.EnhancementType))
                    {
                        StatEnhancements[effect.EnhancementType].MinValue += effect.MinValue;
                        StatEnhancements[effect.EnhancementType].MaxValue += effect.MaxValue;
                    }
                    else
                    {
                        StatEnhancements[effect.EnhancementType] = new Enhancement<Stats>(effect.EnhancementType, effect.MinValue, effect.MaxValue);
                    }
                }
                foreach (var effect in desc.VitalMods)
                {
                    if (VitalEnhancements.ContainsKey(effect.EnhancementType))
                    {
                        VitalEnhancements[effect.EnhancementType].MinValue += effect.MinValue;
                        VitalEnhancements[effect.EnhancementType].MaxValue += effect.MaxValue;
                    }
                    else
                    {
                        VitalEnhancements[effect.EnhancementType] = new Enhancement<Vitals>(effect.EnhancementType, effect.MinValue, effect.MaxValue);
                    }
                }
                foreach (var effect in desc.SpellEnhancements)
                {
                    if (SpellEnhancements.ContainsKey(effect.SpellId))
                    {
                        SpellEnhancements[effect.SpellId].MinValue += effect.MinValue;
                        SpellEnhancements[effect.SpellId].MaxValue += effect.MaxValue;
                    }
                    else
                    {
                        SpellEnhancements[effect.SpellId] = new SpellEnhancementDescriptor(effect.SpellId, effect.MinValue, effect.MaxValue);
                    }
                }
            }

            var yEnd = 0;
            
            ClearRows();
            RefreshStats(0, out yEnd);
            RefreshVitals(yEnd, out yEnd);
            RefreshBonuses(yEnd, out yEnd);
            RefreshSpells(yEnd, out _);
        }

        void RefreshStats(int yStart, out int yEnd)
        {
            var idx = 0;
            yEnd = yStart;

            if (StatEnhancements.Count == 0)
            {
                return;
            }

            foreach (var effectMapping in StatEnhancements.Values)
            {
                var effect = effectMapping.EnhancementType;
                var range = effectMapping.GetRangeDisplay(false, true);

                Strings.ItemDescription.Stats.TryGetValue((int)effect, out var effectName);

                var row = new CharacterBonusRow(ModContainer, "BonusRow", effectName, range, string.Empty);

                row.SetPosition(row.X, row.Y + (YPadding * idx) + yStart);
                row.Initialize();

                idx++;
            }

            yEnd = yStart + (idx * YPadding);
        }

        void RefreshVitals(int yStart, out int yEnd)
        {
            var idx = 0;
            yEnd = yStart;

            if (VitalEnhancements.Count == 0)
            {
                return;
            }

            foreach (var effectMapping in VitalEnhancements.Values)
            {
                var effect = effectMapping.EnhancementType;
                var range = effectMapping.GetRangeDisplay(false, true);

                Strings.ItemDescription.Vitals.TryGetValue((int)effect, out var effectName);

                var row = new CharacterBonusRow(ModContainer, "BonusRow", effectName, range, string.Empty);

                row.SetPosition(row.X, row.Y + (YPadding * idx) + yStart);
                row.Initialize();

                idx++;
            }

            yEnd = yStart + (idx * YPadding);
        }

        void RefreshBonuses(int yStart, out int yEnd)
        {
            var idx = 0;
            yEnd = yStart;

            if (BonusEnhancements.Count == 0)
            {
                return;
            }

            foreach (var effectMapping in BonusEnhancements.Values)
            {
                var effect = effectMapping.EnhancementType;
                var range = effectMapping.GetRangeDisplay(true, true);

                if (!BonusEffectHelper.BonusEffectDescriptions.ContainsKey(effect))
                {
                    continue;
                }

                var effectName = BonusEffectHelper.BonusEffectDescriptions[effect].Name.ToString().Split(':').FirstOrDefault();
                var tooltip = BonusEffectHelper.BonusEffectDescriptions[effect].Description;

                var row = new CharacterBonusRow(ModContainer, "BonusRow", effectName, range, tooltip);

                row.SetPosition(row.X, row.Y + (YPadding * idx) + yStart);
                row.Initialize();

                idx++;
            }

            yEnd = yStart + (idx * YPadding);
        }

        void RefreshSpells(int yStart, out int yEnd)
        {
            var idx = 0;
            yEnd = yStart;

            if (SpellEnhancements.Count == 0)
            {
                return;
            }

            foreach (var effectMapping in SpellEnhancements.Values)
            {
                var range = effectMapping.GetRangeDisplay();

                var row = new CharacterBonusRow(ModContainer, "BonusRow", $"Proc {SpellBase.GetName(effectMapping.SpellId)}", range, string.Empty);

                row.SetPosition(row.X, row.Y + (YPadding * idx) + yStart);
                row.Initialize();

                idx++;
            }

            yEnd = yStart + (idx * YPadding);
        }

        public void ClearRows()
        {
            ModContainer.ClearCreatedChildren();
        }

        public void Hide()
        {
            ClearRows();
            Background.Hide();
        }
    }
}
