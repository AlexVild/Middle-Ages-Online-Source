using Intersect.Client.Framework.Graphics;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Interface.Components;
using Intersect.Client.Interface.Game.Components;
using Intersect.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Interface.Game.BestiaryUi
{
    public class BestiaryMagicComponent : BestiaryComponent
    {
        NpcBase Beast;

        private ImagePanel MagicImage { get; set; }

        private ImagePanel SpellsBg { get; set; }

        private ScrollControl SpellsContainer { get; set; }

        private ComponentList<GwenComponent> SpellContainers { get; set; } = new ComponentList<GwenComponent>();

        public BestiaryMagicComponent(Base parent, string containerName, ComponentList<GwenComponent> referenceList = null) : base(parent, containerName, "BestiaryMagicComponent", referenceList)
        {
        }

        public override void Initialize()
        {
            SelfContainer = new ImagePanel(ParentContainer, ComponentName);
            MagicImage = new ImagePanel(SelfContainer, "MagicImage");
            SpellsBg = new ImagePanel(SelfContainer, "SpellsBg");
            SpellsContainer = new ScrollControl(SpellsBg, "SpellsContainer");

            base.Initialize();
            FitParentToComponent();
        }

        public override void Dispose()
        {
            ClearSpells();
            base.Dispose();
        }

        private void ClearSpells()
        {
            SpellsContainer.ClearCreatedChildren();
            SpellContainers.DisposeAll();
            SpellContainers.Clear();
        }

        public void SetBeast(NpcBase beast, int requiredKc)
        {
            if (beast == null)
            {
                return;
            }
            
            // setup
            Beast = beast;
            RequiredKillCount = requiredKc;
            LockLabel.SetText(RequirementString);
            ClearSpells();
            
            if (!Unlocked)
            {
                return;
            }

            // populatiion
        }

        public override void SetUnlockStatus(bool unlocked)
        {
            if (unlocked)
            {
                MagicImage.RenderColor = Color.White;
            }
            else
            {
                MagicImage.RenderColor = Color.Black;
            }
            
            base.SetUnlockStatus(unlocked);

            SelfContainer.Texture = null;
            if (Unlocked)
            {
                SpellsBg.Texture = UnlockedBg;
            }
            else
            {
                SpellsBg.Texture = LockedBg;
            }
        }

        public override GameTexture UnlockedBg => Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "bestiary_magic_bg.png");

        public override GameTexture LockedBg => Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "bestiary_magic_bg_locked.png");
    }
}
