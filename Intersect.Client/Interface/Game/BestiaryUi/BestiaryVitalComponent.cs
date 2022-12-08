using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Interface.Components;
using Intersect.Client.Interface.Game.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Interface.Game.BestiaryUi
{
    public class BestiaryVitalComponent : GwenComponent
    {
        private Color LabelColor => new Color(255, 50, 19, 0);
        private Color LabelHoverColor => new Color(255, 111, 63, 0);
        private Color StatLabelColor => new Color(255, 166, 167, 37);
        private Color StatColor => new Color(255, 255, 255, 255);

        private string Image { get; set; }
        private string Name { get; set; }
        private string InnerName { get; set; }
        private string Tooltip { get; set; }
        private string Value { get; set; }
        private int RequiredKillCount { get; set; }
        private bool Unlocked { get; set; }

        private ImageLabelComponent ImageLabel { get; set; }
        private NumberContainerComponent NumberContainer { get; set; }
        private ImagePanel LockImage { get; set; }
        private Label LockLabel { get; set; }

        private string RequirementString => $"{RequiredKillCount} kills";

        public BestiaryVitalComponent(Base parent,
            string containerName,
            string image,
            string name,
            string innerLabel,
            string tootltip,
            ComponentList<GwenComponent> referenceList = null) : base(parent, containerName, "BestiaryVitalComponent", referenceList)
        {
            Image = image;
            Name = name;
            InnerName = innerLabel;
            Tooltip = tootltip;
        }

        public void SetValues(int value, int kc, bool unlocked)
        {
            Value = value.ToString();
            RequiredKillCount = kc;
            Unlocked = unlocked;

            LockLabel.SetText(RequirementString);
            NumberContainer.SetValue(Value);
        }

        public override void Initialize()
        {
            SelfContainer = new ImagePanel(ParentContainer, ComponentName);

            ImageLabel = new ImageLabelComponent(SelfContainer, "Image", LabelColor, LabelHoverColor, Image, Name, Tooltip);
            NumberContainer = new NumberContainerComponent(SelfContainer, "NumberContainer", StatLabelColor, StatColor, InnerName, string.Empty);

            LockImage = new ImagePanel(SelfContainer, "LockImage");
            LockLabel = new Label(SelfContainer, "LockLabel");

            LockImage.Hide();
            LockLabel.Hide();

            base.Initialize();
            FitParentToComponent();

            ImageLabel.Initialize();
            NumberContainer.Initialize();
        }
    }
}
