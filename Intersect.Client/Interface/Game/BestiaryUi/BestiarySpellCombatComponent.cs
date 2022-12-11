using Intersect.Client.Framework.Graphics;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Interface.Components;
using Intersect.Client.Interface.Game.Components;
using Intersect.GameObjects;

namespace Intersect.Client.Interface.Game.BestiaryUi
{
    public class BestiarySpellCombatComponent : BestiaryComponent
    {
        private Label CombatInfoLabel { get; set; }

        public BestiarySpellCombatComponent(Base parent, string containerName, ComponentList<GwenComponent> referenceList = null) : base(parent, containerName, "BestiarySpellCombatComponent", referenceList)
        {
        }

        public override void Initialize()
        {
            SelfContainer = new ImagePanel(ParentContainer, ComponentName);
            CombatInfoLabel = new Label(SelfContainer, "CombatInfoLabel")
            {
                Text = "Casting markers & timers discovered!"
            };

            base.Initialize();
            FitParentToComponent();
        }

        public void SetBeast(NpcBase beast, int reqKc)
        {
            RequiredKillCount = reqKc;
            LockLabel.SetText(RequirementString);
            CombatInfoLabel.IsHidden = !Unlocked;
        }

        public override void SizeLockTo()
        {
            LockImage.Texture = LockTexture;
            LockImage.SetSize(LockTexture.Width, LockTexture.Height);
            LockImage.ProcessAlignments();

            LockLabel.SetPosition(LockImage.Right + 8, LockImage.Bottom - LockImage.Height / 2 - 8);
            LockLabel.ProcessAlignments();
        }

        public override GameTexture UnlockedBg => Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "bestiary_spell_combat_bg.png");

        public override GameTexture LockedBg => Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "bestiary_spell_combat_bg_locked.png");
    }
}
