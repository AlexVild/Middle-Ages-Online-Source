using Intersect.Client.Framework.Graphics;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Interface.Components;
using Intersect.Client.Interface.Game.Components;
using Intersect.GameObjects;
using System.Collections.Generic;

namespace Intersect.Client.Interface.Game.BestiaryUi
{
    public class BestiaryStatsComponent : BestiaryComponent
    {
        private NpcBase Beast { get; set; }

        public override GameTexture UnlockedBg => Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "bestiary_stats.png");
        public override GameTexture LockedBg => Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "bestiary_stats_locked.png");
        public GameTexture UnlockedCombatBg => Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "bestiary_combat.png");
        public GameTexture LockedCombatBg => Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "bestiary_combat_locked.png");
        public GameTexture UnlockedResistancesBg => Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "bestiary_resistance.png");
        public GameTexture LockedResistancesBg => Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "bestiary_resistances_locked.png");

        private ImagePanel CombatStatsContainer { get; set; }
        private NumberContainerComponent BluntAttack { get; set; }
        private NumberContainerComponent BluntDefense { get; set; }
        private NumberContainerComponent SlashAttack { get; set; }
        private NumberContainerComponent SlashDefense { get; set; }
        private NumberContainerComponent PierceAttack { get; set; }
        private NumberContainerComponent PiereceDefense { get; set; }
        private NumberContainerComponent MagicAttack { get; set; }
        private NumberContainerComponent MagicDefense { get; set; }
        
        private ImagePanel StatsContainer { get; set; }
        private ImageLabelComponent EvasionLabel { get; set; }
        private NumberContainerComponent EvasionNumber { get; set; }
        private ImageLabelComponent AccuracyLabel { get; set; }
        private NumberContainerComponent AccuracyNumber { get; set; }
        private ImageLabelComponent SpeedLabel { get; set; }
        private NumberContainerComponent SpeedNumber { get; set; }

        private ImagePanel ResistancesContainer { get; set; }
        private List<Label> Resistances { get; set; }

        public BestiaryStatsComponent(Base parent, 
            string containerName,
            ComponentList<GwenComponent> referenceList = null) : base(parent, containerName, "BestiaryStatsComponent", referenceList)
        {
        }

        public override void Initialize()
        {
            SelfContainer = new ImagePanel(ParentContainer, ComponentName);

            CombatStatsContainer = new ImagePanel(SelfContainer, "CombatStatsContainer");
            
            StatsContainer = new ImagePanel(SelfContainer, "StatsContainer");
            
            ResistancesContainer = new ImagePanel(SelfContainer, "ResistancesContainer");

            base.Initialize();
            FitParentToComponent();
        }

        public override void SetUnlockStatus(bool unlocked)
        {
            Unlocked = unlocked;

            if (Unlocked)
            {
                StatsContainer.Texture = UnlockedBg;
                CombatStatsContainer.Texture = UnlockedCombatBg;
                ResistancesContainer.Texture = UnlockedResistancesBg;
                HideLock();
            }
            else
            {
                StatsContainer.Texture = LockedBg;
                CombatStatsContainer.Texture = LockedCombatBg;
                ResistancesContainer.Texture = LockedResistancesBg;
                ShowLock();
            }
        }

        public void SetBeast(NpcBase beast, int kc)
        {
            Beast = beast;
            RequiredKillCount = kc;

            LockLabel.SetText(RequirementString);
        }
    }
}
