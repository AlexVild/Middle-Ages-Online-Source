using Intersect.Client.Framework.Graphics;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Interface.Components;
using Intersect.Client.Interface.Game.Character.Panels;
using Intersect.Client.Utilities;
using Intersect.GameObjects;
using System;
using static Intersect.Client.Framework.File_Management.GameContentManager;

namespace Intersect.Client.Interface.Game.Components
{
    public class RecipeRequirementComponent : GwenComponent
    {
        private Label HintTemplate { get; set; }
        private RichLabel Hint { get; set; }

        private RecipeRequirementProgressBarComponent ProgressBar { get; set; }

        private string ProgressBarBgTexture => "character_harvest_progressbar.png";
        private string ProgressBarTexture => "character_harvest_progressbar_fill.png";
        private string ProgressBarCompleteTexture => "character_harvest_progressbar_complete.png";

        private int Progress { get; set; }
        private int Goal { get; set; }
        private int Remaining { get; set; }
        private bool Completed { get; set; }
        private string HintText { get; set; }

        public int X => ParentContainer.X;
        public int Y => ParentContainer.Y;
        public int Width => ParentContainer.Width;
        public int Height => ParentContainer.Height;
        public int Bottom => ParentContainer.Bottom;

        public Color CompleteHintColor => new Color(255, 129, 186, 40);
        public Color IncompleteHintColor => new Color(255, 169, 169, 169);

        private float _Percent;
        private float Percent
        {
            get
            {
                return _Percent;
            }
            set
            {
                _Percent = MathHelper.Clamp(value, 0f, 1f);
            }
        }

        public RecipeRequirementComponent(
            Base parent,
            string containerName,
            string hint,
            int progress,
            int goal,
            int remaining,
            bool completed,
            ComponentList<GwenComponent> referenceList = null
            ) : base(parent, containerName, "RecipeProgressRowComponent", referenceList)
        {
            Progress = progress;
            Goal = goal;
            Remaining = remaining;
            Completed = completed;
            HintText = hint;

            if (goal > 0)
            {
                Percent = (float)progress / goal;
            }
        }

        public override void Initialize()
        {
            SelfContainer = new ImagePanel(ParentContainer, ComponentName);

            HintTemplate = new Label(SelfContainer, "Hint");
            Hint = new RichLabel(SelfContainer);

            var formattedRemaining = Remaining.ToString("N0");
            ProgressBar = new RecipeRequirementProgressBarComponent(SelfContainer,
                "ProgressBar",
                ProgressBarBgTexture,
                Completed ? ProgressBarCompleteTexture : ProgressBarTexture,
                bottomLabelColor: IncompleteHintColor,
                bottomLabel: Completed ? string.Empty : $"{formattedRemaining} to go!",
                leftLabel: Completed ? string.Empty : $"{Progress}",
                leftLabelColor: IncompleteHintColor,
                rightLabel: Completed ? string.Empty : $"{Goal}",
                rightLabelColor: IncompleteHintColor,
                topLabelColor: CompleteHintColor,
                topLabel: Completed ? "COMPLETE!" : string.Empty);
            ProgressBar.Percent = Completed ? 1.0f : Percent;

            base.Initialize();
            FitParentToComponent();

            ProgressBar.Hide();
            if (Goal > 1)
            {
                ProgressBar.Initialize();
                ProgressBar.Show();
            }
            HintTemplate.SetTextColor(Completed ? CompleteHintColor : IncompleteHintColor, Label.ControlState.Normal);
            FormatHint();
        }

        private void FormatHint()
        {
            Hint.ClearText();
            var progressBarWidth = ProgressBar.Width + 20;

            Hint.Width = SelfContainer.Width - progressBarWidth - HintTemplate.Padding.Left - HintTemplate.Padding.Right;
            Hint.AddText($"* {HintText}", HintTemplate);
            Hint.SizeToChildren(false, true);
            Hint.Y += HintTemplate.Padding.Top;
            Hint.X += HintTemplate.Padding.Left;
        }

        public void SetPosition(int x, int y)
        {
            ParentContainer.SetPosition(x, y);
        }
    }
}
