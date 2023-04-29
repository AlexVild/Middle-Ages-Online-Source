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
    public class PassiveRowComponent : GwenComponent, IDisposable
    {
        string FrameTexture => "character_resource_unlocked_bg.png";
        private SpellImageFrameComponent Image { get; set; }

        private Label Title { get; set; }

        public int X => ParentContainer.X;
        public int Y => ParentContainer.Y;

        public int Height => ParentContainer.Height;
        public int Width => ParentContainer.Width;

        private GameTexture BandingTexture => Globals.ContentManager.GetTexture(TextureType.Gui, "character_harvest_banding.png");

        Guid SpellId { get; set; }
        SpellBase Spell { get; set; }

        public PassiveRowComponent(
            Base parent,
            string containerName,
            Guid spellId,
            ComponentList<GwenComponent> referenceList = null) : base(parent, containerName, "PassiveRowsComponent", referenceList)
        {
            SpellId = spellId;
            Spell = SpellBase.Get(spellId);
        }

        public void Initialize()
        {
            SelfContainer = new ImagePanel(ParentContainer, ComponentName);
            Image = new SpellImageFrameComponent(SelfContainer, 
                "SpellImage", 
                FrameTexture, 
                Spell?.Icon, 
                TextureType.Spell, 
                1, 
                8, 
                SpellId);

            Title = new Label(SelfContainer, "SkillName")
            {
                Text = Spell?.Name ?? "NOT FOUND"
            };

            base.Initialize();
            FitParentToComponent();

            Image.Initialize();
        }

        public void SetBanding()
        {
            SelfContainer.Texture = BandingTexture;
        }

        public override void Dispose()
        {
            Image?.Dispose();
        }
    }
}
