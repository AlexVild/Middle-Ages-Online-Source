using Intersect.Client.Core;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Interface.Components;
using Intersect.Client.Networking;
using Intersect.GameObjects;
using System;
using static Intersect.Client.Framework.File_Management.GameContentManager;

namespace Intersect.Client.Interface.Game.Components
{
    public class SkillRowComponent : GwenComponent
    {
        string Frame => Prepared ? "character_resource_selected_bg.png" : 
            RemainingPoints >= Spell.RequiredSkillPoints ? "character_resource_unlocked_bg.png" : "character_resource_disabled_bg.png";
        private SpellImageFrameComponent Image { get; set; }

        private Color LockedColor => new Color(255, 100, 100, 100);
        private Color SecondaryColor => new Color(255, 180, 180, 180);
        private Color PrimaryColor => new Color(255, 255, 255, 255);
        private Color TitleColor => Prepared ? PrimaryColor : RemainingPoints >= Spell.RequiredSkillPoints ?
            SecondaryColor : LockedColor;

        private Label Title { get; set; }
        private Label Points { get; set; }

        public bool Prepared { get; set; }

        public int X => ParentContainer.X;
        public int Y => ParentContainer.Y;

        public int Height => ParentContainer.Height;
        public int Width => ParentContainer.Width;

        private GameTexture BandingTexture => Globals.ContentManager.GetTexture(TextureType.Gui, "character_harvest_banding.png");

        private CheckBox UseCheckbox { get; set; }
        private const string UseToolTip = "Prepare / Unprepare skill";

        Guid SpellId { get; set; }
        SpellBase Spell { get; set; }

        bool Initializing = true;

        public int PointVal { get; set; }
        public int RemainingPoints { get; set; }

        private readonly GameTexture RowHoverTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "character_hover_select.png");

        bool IsBanded = false;

        public SkillRowComponent(Base parent,
            string containerName,
            Guid spellId,
            bool prepared,
            int points,
            int remainingPoints,
            ComponentList<GwenComponent> referenceList = null
            ) : base(parent, containerName, "SkillRowComponent", referenceList)
        {
            SpellId = spellId;
            Prepared = prepared;
            PointVal = points;
            RemainingPoints = remainingPoints;

            Spell = SpellBase.Get(SpellId);
        }

        public override void Initialize()
        {
            SelfContainer = new ImagePanel(ParentContainer, ComponentName);
            Image = new SpellImageFrameComponent(SelfContainer, "SpellImage", Frame, Spell?.Icon, TextureType.Spell, 1, 8, SpellId);

            Title = new Label(SelfContainer, "SkillName")
            {
                Text = Spell?.Name ?? "NOT FOUND"
            };

            Points = new Label(SelfContainer, "Points")
            {
                Text = $"{PointVal} SP"
            };

            UseCheckbox = new CheckBox(SelfContainer, "UseCheckbox");
            SelfContainer.Clicked += SelfContainer_Clicked;
            SelfContainer.HoverEnter += SelfContainer_HoverEnter;
            SelfContainer.HoverLeave += SelfContainer_HoverLeave;

            UseCheckbox.CheckChanged += UseCheckbox_CheckChanged1;

            base.Initialize();
            FitParentToComponent();

            UseCheckbox.IsChecked = Prepared;
            if (!Prepared && RemainingPoints < Spell.RequiredSkillPoints)
            {
                UseCheckbox.Disable();
            }

            Title.SetTextColor(TitleColor, Label.ControlState.Normal);
            
            Image.Initialize();
            Image.SetOnClick(ImageContainer_Clicked);
            Image.SetHoverLeaveAction(HoverLeave);
            Image.SetHoverEnterAction(HoverEnter);
            if (!Prepared)
            {
                if (UseCheckbox.IsDisabled)
                {
                    Image.SetImageRenderColor(new Color(90, 255, 255, 255));
                }
                else
                {
                    Image.SetImageRenderColor(new Color(160, 255, 255, 255));
                }
            }

            Initializing = false;
        }

        private void SelfContainer_HoverLeave(Base sender, EventArgs arguments)
        {
            HoverLeave();
        }

        public void HoverLeave()
        {
            if (IsBanded)
            {
                SetBanding();
            }
            else
            {
                SelfContainer.Texture = null;
            }
        }

        public void HoverEnter()
        {
            if (!UseCheckbox.IsDisabled)
            {
                SelfContainer.Texture = RowHoverTexture;
            }
        }

        private void SelfContainer_HoverEnter(Base sender, EventArgs arguments)
        {
            HoverEnter();
        }

        private void UseCheckbox_CheckChanged1(Base sender, EventArgs arguments)
        {
            if (Initializing)
            {
                return;
            }

            PacketSender.SendSkillPreparationChange(SpellId, !Prepared);
        }

        private void SelfContainer_Clicked(Base sender, Framework.Gwen.Control.EventArguments.ClickedEventArgs arguments)
        {
            if (Initializing)
            {
                return;
            }

            PacketSender.SendSkillPreparationChange(SpellId, !Prepared);
        }

        private void ImageContainer_Clicked(Base sender, Framework.Gwen.Control.EventArguments.ClickedEventArgs arguments)
        {
            if (Initializing)
            {
                return;
            }

            Audio.AddGameSound("ui_press.wav", false);
            PacketSender.SendSkillPreparationChange(SpellId, !Prepared);
        }

        public void SetBanding()
        {
            IsBanded = true;
            SelfContainer.Texture = BandingTexture;
        }

        public virtual void SetPosition(float x, float y)
        {
            ParentContainer.SetPosition(x, y);
        }

        public override void Dispose()
        {
            Image.Dispose();
            base.Dispose();
        }
    }
}
