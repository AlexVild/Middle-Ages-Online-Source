using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Interface.Components;
using System;
using System.Collections.Generic;

namespace Intersect.Client.Interface.Game.Components
{
    public class ProgressBarLabelMetadata
    {
        public string TopText { get; set; }
        public string BottomText { get; set; }
        public string LeftText { get; set; }
        public string RightText { get; set; }

        public Color TopColor { get; set; }
        public Color BottomColor { get; set; }
        public Color RightColor { get; set; }
        public Color LeftColor { get; set; }

        public ProgressBarLabelMetadata(string topText, string bottomText, string leftText, string rightText, Color topColor, Color bottomColor, Color rightColor, Color leftColor)
        {
            TopText = topText;
            BottomText = bottomText;
            LeftText = leftText;
            RightText = rightText;
            TopColor = topColor;
            BottomColor = bottomColor;
            RightColor = rightColor;
            LeftColor = leftColor;
        }
    }

    public enum ProgressBarLabel
    {
        Top,
        Bottom,
        Right,
        Left
    }

    public abstract class ProgressBarComponent : IGwenComponent
    {
        readonly int ScaleFactor = 4;

        private string ComponentName { get; set; }

        public ImagePanel ParentContainer { get; set; }

        protected ImagePanel SelfContainer { get; set; }

        private string BarBgTexture { get; set; }
        private ImagePanel BarBg { get; set; }

        private string BarFgTexture { get; set; }
        private ImagePanel BarFg { get; set; }

        private ProgressBarLabelMetadata LabelMetadata { get; set; }
        
        private Label TopLabel { get; set; }
        private Label BottomLabel { get; set; }
        private Label LeftLabel { get; set; }
        private Label RightLabel { get; set; }

        public float Percent { get; set; }

        public ProgressBarComponent(
            ImagePanel parent,
            string componentName,
            string containerName,
            string bgTexture,
            string fgTexture,
            string topLabel = "",
            Color topLabelColor = null,
            string bottomLabel = "",
            Color bottomLabelColor = null,
            string leftLabel = "",
            Color leftLabelColor = null,
            string rightLabel = "",
            Color rightLabelColor = null,
            ComponentList<ProgressBarComponent> referenceList = null
        )
        {
            ComponentName = componentName;
            ParentContainer = new ImagePanel(parent, containerName);

            BarBgTexture = bgTexture;
            BarFgTexture = fgTexture;

            LabelMetadata = new ProgressBarLabelMetadata(
                topLabel,
                bottomLabel,
                leftLabel,
                rightLabel,
                topLabelColor,
                bottomLabelColor,
                rightLabelColor,
                leftLabelColor
            );

            if (referenceList != null)
            {
                referenceList.Add(this);
            }
        }

        public virtual void Initialize()
        {
            SelfContainer = new ImagePanel(ParentContainer, ComponentName);
            
            BarBg = new ImagePanel(SelfContainer, "BarBackground");
            BarFg = new ImagePanel(BarBg, "ProgressBar");

            TopLabel = new Label(SelfContainer, "TopLabel")
            {
                Text = LabelMetadata.TopText
            };
            BottomLabel = new Label(SelfContainer, "BottomLabel")
            {
                Text = LabelMetadata.BottomText
            };
            RightLabel = new Label(SelfContainer, "RightLabel")
            {
                Text = LabelMetadata.RightText
            };
            LeftLabel = new Label(SelfContainer, "LeftLabel")
            {
                Text = LabelMetadata.LeftText
            };

            SelfContainer.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
            ParentContainer.SetSize(SelfContainer.Width, SelfContainer.Height);
            ParentContainer.ProcessAlignments();

            TopLabel.SetTextColor(LabelMetadata.TopColor ?? Color.White, Label.ControlState.Normal);
            BottomLabel.SetTextColor(LabelMetadata.BottomColor ?? Color.White, Label.ControlState.Normal);
            RightLabel.SetTextColor(LabelMetadata.RightColor ?? Color.White, Label.ControlState.Normal);
            LeftLabel.SetTextColor(LabelMetadata.LeftColor ?? Color.White, Label.ControlState.Normal);

            BarBg.Texture = Globals.ContentManager.GetTexture(GameContentManager.TextureType.Gui, BarBgTexture);
            BarFg.Texture = Globals.ContentManager.GetTexture(GameContentManager.TextureType.Gui, BarFgTexture);

            SetBarWidth();
        }

        public void SetLabelText(ProgressBarLabel type, string text)
        {
            switch (type)
            {
                case ProgressBarLabel.Top:
                    TopLabel.SetText(text);
                    return;
                case ProgressBarLabel.Bottom:
                    BottomLabel.SetText(text);
                    return;
                case ProgressBarLabel.Right:
                    RightLabel.SetText(text);
                    return;
                case ProgressBarLabel.Left:
                    LeftLabel.SetText(text);
                    return;
                default:
                    throw new ArgumentException(nameof(type));
            }
        }

        public void SetBarWidth()
        {
            //var barWidth = Intersect.Utilities.MathHelper.RoundNearestMultiple((int)(Percent * BarFg.Width), 4);
            var barWidth = (int)Math.Floor(Percent * BarFg.Width);

            BarFg.SetBounds(BarFg.X, BarFg.Y, barWidth, BarFg.Height);
            BarFg.SetTextureRect(BarFg.X, BarFg.Y, barWidth, BarFg.Height);
        }

        public void Update()
        {
            SetBarWidth();
        }

        public void Dispose()
        {
            ParentContainer.DeleteAllNestedChildren();
            ParentContainer.Dispose();
        }
    }
}
