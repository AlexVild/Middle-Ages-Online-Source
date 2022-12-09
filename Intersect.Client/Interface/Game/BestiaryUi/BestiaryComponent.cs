using Intersect.Client.Framework.Graphics;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Interface.Components;
using Intersect.Client.Interface.Game.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Interface.Game.BestiaryUi
{
    public abstract class BestiaryComponent : GwenComponent
    {
        public abstract GameTexture UnlockedBg { get; }
        public abstract GameTexture LockedBg { get; }
        public GameTexture LockTexture => Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "bestiary_lock.png");

        private ImagePanel LockImage { get; set; }
        public Label LockLabel { get; set; }
        
        public bool Unlocked { get; set; }

        public int RequiredKillCount { get; set; }
        public string RequirementString => $"{RequiredKillCount} kills";

        protected BestiaryComponent(Base parent, string containerName, string componentName, ComponentList<GwenComponent> referenceList = null) : base(parent, containerName, componentName, referenceList)
        {
        }

        public virtual void SetUnlockStatus(bool unlocked)
        {
            Unlocked = unlocked;

            if (Unlocked)
            {
                SelfContainer.Texture = UnlockedBg;
                HideLock();
            }
            else
            {
                SelfContainer.Texture = LockedBg;
                ShowLock();
            }
        }

        public void ShowLock()
        {
            LockLabel.Show();
            LockImage.Show();
        }

        public void HideLock()
        {
            LockLabel.Hide();
            LockImage.Hide();
        }

        public override void Initialize()
        {
            LockImage = new ImagePanel(SelfContainer, "LockImage");
            LockLabel = new Label(SelfContainer, "LockLabel");

            LockImage.Hide();
            LockLabel.Hide();

            base.Initialize();

            SizeLockTo();
        }

        public void SizeLockTo()
        {
            LockImage.Texture = LockTexture;
            LockImage.SetSize(LockTexture.Width, LockTexture.Height);
            LockImage.ProcessAlignments();

            LockLabel.SetPosition(0, LockImage.Bottom + 8);
            LockLabel.ProcessAlignments();
        }

        public virtual void Dispose()
        {
            ParentContainer.Dispose();
        }
    }
}
