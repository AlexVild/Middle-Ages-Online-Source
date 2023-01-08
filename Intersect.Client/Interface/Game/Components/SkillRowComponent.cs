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
        string Frame => Prepared ? "character_resource_unlocked_bg.png" : "character_resource_locked_bg.png";
        private ImageFrameComponent Image { get; set; }

        private Color SecondaryColor => new Color(255, 169, 169, 169);
        private Color PrimaryColor => new Color(255, 255, 255, 255);
        private Color TitleColor => Prepared ? PrimaryColor : SecondaryColor;

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

        public SkillRowComponent(Base parent,
            string containerName,
            Guid spellId,
            bool prepared,
            int points,
            ComponentList<GwenComponent> referenceList = null
            ) : base(parent, containerName, "SkillRowComponent", referenceList)
        {
            SpellId = spellId;
            Prepared = prepared;
            PointVal = points;

            Spell = SpellBase.Get(SpellId);
        }

        public override void Initialize()
        {
            SelfContainer = new ImagePanel(ParentContainer, ComponentName);
            Image = new ImageFrameComponent(SelfContainer, "SpellImage", Frame, Spell.Icon, TextureType.Spell, 1, 8);

            Title = new Label(SelfContainer, "SkillName")
            {
                Text = Spell?.Name ?? "NOT FOUND"
            };

            Points = new Label(SelfContainer, "Points")
            {
                Text = $"{PointVal} SP"
            };

            UseCheckbox = new CheckBox(SelfContainer, "UseCheckbox");
            UseCheckbox.CheckChanged += UseCheckbox_CheckChanged;

            UseCheckbox.IsChecked = Prepared;

            base.Initialize();
            FitParentToComponent();
            
            Title.SetTextColor(TitleColor, Label.ControlState.Normal);
            Image.Initialize();

            Initializing = false;
        }

        private void UseCheckbox_CheckChanged(Base sender, EventArgs arguments)
        {
            if (Initializing)
            {
                return;
            }

            PacketSender.SendSkillPreparationChange(SpellId, !Prepared);
        }

        public void SetBanding()
        {
            SelfContainer.Texture = BandingTexture;
        }

        public void SetPosition(float x, float y)
        {
            ParentContainer.SetPosition(x, y);
        }
    }
}
