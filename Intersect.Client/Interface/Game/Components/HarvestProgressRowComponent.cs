using Intersect.Client.Framework.Graphics;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Interface.Components;
using Intersect.Client.Utilities;
using static Intersect.Client.Framework.File_Management.GameContentManager;

namespace Intersect.Client.Interface.Game.Components
{
    public class HarvestProgressRowComponent : GwenComponent
    {
        string ResourceFrame => Harvestable ? "character_resource_unlocked_bg.png" : "character_resource_disabled_bg.png";
        TextureType ResourceTextureType => TextureType.Resource;
        private string ResourceTextureString { get; set; }
        private ImageFrameComponent ResourceImage { get; set; }
        
        private string ResourceText { get; set; }
        private Label ResourceName { get; set; }

        private string ProgressBarBgTexture => "character_harvest_progressbar.png";
        private string ProgressBarTexture => "character_harvest_progressbar_fill.png";
        private string ProgressBarCompleteTexture => "character_harvest_progressbar_complete.png";
        private GameTexture BandingTexture => Globals.ContentManager.GetTexture(TextureType.Gui, "character_harvest_banding.png");
        private Color CurrHarvestColor => new Color(255, 169, 169, 169);
        private Color NextHarvestColor => new Color(255, 255, 255, 255);
        private int CurrentLevel { get; set; }
        private int NextLevel { get; set; }
        private long HarvestsRemaining { get; set; }
        private int MaxLevel => Options.Combat.HarvestBonuses.Count;
        private bool IsMaxed => CurrentLevel >= MaxLevel;

        private float _Percent;
        private float Percent { 
            get
            {
                return _Percent;
            }
            set 
            {
                _Percent = MathHelper.Clamp(value, 0f, 1f);
            }
        }
        private HarvestProgressBarComponent ProgressBar { get; set; }

        private bool Harvestable { get; set; }
        private string CannotHarvestMessage { get; set; }
        
        private Label CannotHarvestLabelTemplate { get; set; }
        private RichLabel CannotHarvestLabel { get; set; }

        public int X => SelfContainer.X;
        public int Y => SelfContainer.X;

        public int Height => SelfContainer.Height;

        public HarvestProgressRowComponent(
            Base parent,
            string containerName,
            string resourceTexture,
            string resourceText,
            int currentLevel,
            int nextLevel,
            long harvestsRemaining,
            float percent,
            bool harvestable,
            string cannotHarvestMessage,
            ComponentList<GwenComponent> referenceList = null
            ) : base(parent, containerName, "HarvestProgressRowComponent", referenceList)
        {
            ResourceTextureString = resourceTexture;
            ResourceText = resourceText;
            CurrentLevel = currentLevel;
            NextLevel = nextLevel;
            HarvestsRemaining = harvestsRemaining;
            Percent = percent;
            Harvestable = harvestable;
            CannotHarvestMessage = cannotHarvestMessage;
        }

        public void SetBanding()
        {
            SelfContainer.Texture = BandingTexture;
        }

        public override void Initialize()
        {
            SelfContainer = new ImagePanel(ParentContainer, ComponentName);
            ResourceImage = new ImageFrameComponent(SelfContainer, "ResourceImage", ResourceFrame, ResourceTextureString, ResourceTextureType, 1, 8);

            ResourceName = new Label(SelfContainer, "ResourceName");

            CannotHarvestLabelTemplate = new Label(SelfContainer, "CannotHarvestMsg");
            CannotHarvestLabel = new RichLabel(SelfContainer);

            var formattedRemaining =  HarvestsRemaining.ToString("N0");

            ProgressBar = new HarvestProgressBarComponent(SelfContainer,
                "ProgressBar",
                ProgressBarBgTexture,
                IsMaxed ? ProgressBarCompleteTexture : ProgressBarTexture,
                bottomLabel: IsMaxed ? string.Empty : $"{formattedRemaining} to go!",
                bottomLabelColor: CurrHarvestColor,
                leftLabel: $"{CurrentLevel}",
                leftLabelColor: CurrHarvestColor,
                rightLabel: IsMaxed ? string.Empty : $"{NextLevel}",
                rightLabelColor: NextHarvestColor,
                topLabel: IsMaxed ? "MAX!" : string.Empty);
            ProgressBar.Percent = IsMaxed ? 1.0f : Percent;

            base.Initialize();
            FitParentToComponent();

            var truncatedName = UiHelper.TruncateString(ResourceText, ResourceName.Font, 64).ToUpper();
            ResourceName.SetText(truncatedName);
            
            ResourceImage.Initialize();
            if (!Harvestable)
            {
                ResourceImage.SetImageRenderColor(new Color(160, 255, 255, 255));
            }
            ResourceImage.SetTooltipText(ResourceText);

            if (Harvestable)
            {
                ResourceName.SetTextColor(NextHarvestColor, Label.ControlState.Normal);
                ProgressBar.Initialize();
            }
            else
            {
                FormatCannotHarvestMessage();

                ResourceName.SetTextColor(CurrHarvestColor, Label.ControlState.Normal);
            }
        }

        public void SetPosition(int x, int y)
        {
            ParentContainer.X = x;
            ParentContainer.Y = y;
        }

        private void FormatCannotHarvestMessage()
        {
            CannotHarvestLabel.ClearText();
            CannotHarvestLabel.Width = SelfContainer.Width / 2 - 8;
            CannotHarvestLabel.X = SelfContainer.Width / 2 - 16;
            CannotHarvestLabel.AddText(CannotHarvestMessage, CannotHarvestLabelTemplate);
            CannotHarvestLabel.SizeToChildren(false, true);
            CannotHarvestLabel.Y += SelfContainer.Padding.Top;
        }
    }
}
