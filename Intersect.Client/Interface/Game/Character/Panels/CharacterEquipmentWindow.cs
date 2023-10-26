using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Intersect.Client.Core;
using Intersect.Client.Entities;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Interface.Components;
using Intersect.Client.Interface.Game.Components;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Network.Packets.Server;
using Label = Intersect.Client.Framework.Gwen.Control.Label;

namespace Intersect.Client.Interface.Game.Character.Equipment
{
    public partial class CharacterEquipmentWindow : CharacterWindowPanel
    {
        private Color AtkLabelColor => new Color(255, 222, 124, 112);
        private Color DefLabelColor => new Color(255, 105, 158, 252);
        private Color StatColor => new Color(255, 255, 255, 255);

        private Player Me => Globals.Me;
        Label mName { get; set; }
        Label mLevelAndClass { get; set; }
        Label mClassRank { get; set; }

        //Equipment List
        public List<EquipmentItem> Items = new List<EquipmentItem>();
        ImagePanel mCharacterBackground { get; set; }
        ImagePanel mCharacter { get; set; }
        public ImagePanel[] PaperdollPanels { get; set; }
        public string[] PaperdollTextures { get; set; }

        NumberContainerComponent mBluntAtk { get; set; }
        NumberContainerComponent mBluntDef { get; set; }

        NumberContainerComponent mSlashAtk { get; set; }
        NumberContainerComponent mSlashDef { get; set; }

        NumberContainerComponent mPierceAtk { get; set; }
        NumberContainerComponent mPierceDef { get; set; }

        NumberContainerComponent mMagicAtk { get; set; }
        NumberContainerComponent mMagicDef { get; set; }

        ImagePanel ScaleDownImage { get; set; }

        private GameTexture DayBgTexture;
        private GameTexture NightBgTexture;
        private GameTexture InteriorBgTexture;

        public CharacterPanelType Type { get; } = CharacterPanelType.Equipment;
        
        private int[] mEmptyStatBoost = new int[(int)Stats.StatCount];

        private Guid PreviousMap;

        private ComponentList<IGwenComponent> ContainerComponents;

        private List<GameTexture> ScaleDownTextures = new List<GameTexture>();

        public CharacterEquipmentWindow(CharacterWindow parent, ImagePanel characterWindow)
        {
            mParent = parent;
            DayBgTexture = Globals.ContentManager.GetTexture(GameContentManager.TextureType.Gui, "character_equip_bg_day.png");
            NightBgTexture = Globals.ContentManager.GetTexture(GameContentManager.TextureType.Gui, "character_equip_bg_night.png");
            InteriorBgTexture = Globals.ContentManager.GetTexture(GameContentManager.TextureType.Gui, "character_equip_bg_interior.png");

            ScaleDownTextures.Clear();
            ScaleDownTextures.Add(Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Misc, "ceil_t0.png"));
            ScaleDownTextures.Add(Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Misc, "ceil_t1.png"));
            ScaleDownTextures.Add(Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Misc, "ceil_t2.png"));
            ScaleDownTextures.Add(Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Misc, "ceil_t3.png"));
            ScaleDownTextures.Add(Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Misc, "ceil_t4.png"));
            ScaleDownTextures.Add(Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Misc, "ceil_t5.png"));
            ScaleDownTextures.Add(Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Misc, "ceil_t6.png"));
            ScaleDownTextures.Add(Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Misc, "ceil_t7.png"));
            ScaleDownTextures.Add(Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Misc, "ceil_t8.png"));

            mParentContainer = characterWindow;
            mBackground = new ImagePanel(mParentContainer, "CharacterWindowMAO_Equipment");

            InitializeEquipmentStatContainers();
            InitializeCharacterInfo();
            InitializeCharacterDisplay();

            ScaleDownImage = new ImagePanel(mBackground, "ScaleDownImage");

            mBackground.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

            ScaleDownImage.SetTooltipGraphicsMAO();

            ContainerComponents.InitializeAll();
        }

        public override void Show()
        {
            SetCharacterBackground();
            base.Show();
        }

        public override void Update()
        {
            if (Me == null || IsHidden)
            {
                return;
            }

            // Refreshes the character background if the menu is open during a map transition :>. How cute.
            if ((Me.MapInstance?.Id ?? Guid.Empty) != PreviousMap)
            {
                SetCharacterBackground();
            }
            PreviousMap = Me.MapInstance?.Id ?? Guid.Empty;

            ScaleDownImage.IsHidden = !Me.IsScaledDown;
            if (ScaleDownImage.IsVisible)
            {
                ScaleDownImage.Texture = ScaleDownTextures.ElementAtOrDefault(Me.ScaledTo);
                ScaleDownImage.SetToolTipText($"Equipment Stats currently scaled to Tier ${Me.ScaledTo}.");
            }

            PopulateCharacterInfo();
            PopulateEquipStats();
            PopulateEquipment();
            PopulateCharacterPortrait();
        }

        private void PopulateCharacterInfo()
        {
            var name = Me.Name;
            var className = ClassBase.Get(Me.Class)?.Name ?? "Class Not Found";
            var level = Me.Level;
            var classRank = 0;
            if (Me.ClassRanks?.ContainsKey(className) ?? false)
            {
                Me.ClassRanks?.TryGetValue(className, out classRank);
            }

            mName.SetText(name);
            mLevelAndClass.SetText(Strings.Character.levelandclass.ToString(level, className));
            mClassRank.SetText(Strings.Character.classrank.ToString(className, classRank));
        }

        private void PopulateEquipStats()
        {
            var bluntStats = new EquipRow(Me, Stats.Attack, Stats.Defense);
            var slashStats = new EquipRow(Me, Stats.SlashAttack, Stats.SlashResistance);
            var pierceStats = new EquipRow(Me, Stats.PierceAttack, Stats.PierceResistance);
            var magicStats = new EquipRow(Me, Stats.AbilityPower, Stats.MagicResist);

            mBluntAtk.SetValue(bluntStats.Attack);
            mBluntDef.SetValue(bluntStats.Defense);

            mSlashAtk.SetValue(slashStats.Attack);
            mSlashDef.SetValue(slashStats.Defense);

            mPierceAtk.SetValue(pierceStats.Attack);
            mPierceDef.SetValue(pierceStats.Defense);

            mMagicAtk.SetValue(magicStats.Attack);
            mMagicDef.SetValue(magicStats.Defense);
        }

        private void PopulateEquipment()
        {
            for (var i = 0; i < Options.EquipmentSlots.Count; i++)
            {
                if (Globals.Me.MyEquipment[i] > -1 && Globals.Me.MyEquipment[i] < Options.MaxInvItems)
                {
                    if (Globals.Me.Inventory[Globals.Me.MyEquipment[i]].ItemId != Guid.Empty)
                    {
                        Items[i]
                            .Update(
                                Globals.Me.Inventory[Globals.Me.MyEquipment[i]].ItemId,
                                Globals.Me.Inventory[Globals.Me.MyEquipment[i]].ItemProperties
                            );
                    }
                    else
                    {
                        Items[i].Update(Guid.Empty, new ItemProperties());
                    }
                }
                else
                {
                    Items[i].Update(Guid.Empty, new ItemProperties());
                }
            }
        }

        private void PopulateCharacterPortrait()
        {
            if (Globals.Me == null || string.IsNullOrEmpty(Globals.Me.MySprite))
            {
                return;
            }

            var entityTex = Globals.ContentManager.GetTexture(
                GameContentManager.TextureType.Entity, Globals.Me.MySprite
            );

            if (entityTex == null)
            {
                mCharacter.IsHidden = true;
                for (var i = 0; i < Options.EquipmentSlots.Count + Options.DecorSlots.Count; i++)
                {
                    PaperdollPanels[i].Hide();
                }
                return;
            }

            // Figure out if there are decors we shouldn't draw
            var hideHair = false;
            var hideBeard = false;
            var hideExtra = false;
            var shortHair = false;
            if (Globals.Me.Cosmetics[Options.HelmetIndex] != Guid.Empty)
            {
                var helmet = ItemBase.Get(Globals.Me.Cosmetics[Options.HelmetIndex]);
                if (helmet != null)
                {
                    hideHair = helmet.HideHair;
                    hideBeard = helmet.HideBeard;
                    hideExtra = helmet.HideExtra;
                    shortHair = helmet.ShortHair;
                }
            }
            else if (Globals.Me.MyEquipment[Options.HelmetIndex] > -1)
            {
                var helmet = ItemBase.Get(Globals.Me.Inventory[Globals.Me.MyEquipment[Options.HelmetIndex]].ItemId);
                if (helmet != null)
                {
                    hideHair = helmet.HideHair;
                    hideBeard = helmet.HideBeard;
                    hideExtra = helmet.HideExtra;
                    shortHair = helmet.ShortHair;
                }
            }

            for (var z = 0; z < Options.PaperdollOrder[1].Count; z++)
            {
                var paperdoll = "";
                var textureGroup = GameContentManager.TextureType.Paperdoll;
                var equipmentSlot = Options.EquipmentSlots.IndexOf(Options.PaperdollOrder[1][z]);

                if (Options.EquipmentSlots.IndexOf(Options.PaperdollOrder[1][z]) > -1)
                {
                    var equipment = Globals.Me.MyEquipment;
                    var cosmetics = Globals.Me.Cosmetics;
                    if (equipment[equipmentSlot] > -1 && equipment[equipmentSlot] < Options.MaxInvItems)
                    {
                        var itemNum = Globals.Me
                            .Inventory[equipment[equipmentSlot]]
                            .ItemId;

                        // Override the equipped item?
                        var cosmeticSlot = Globals.Me.Cosmetics.ElementAtOrDefault(equipmentSlot);
                        if (cosmeticSlot != default && cosmeticSlot != Guid.Empty)
                        {
                            itemNum = cosmeticSlot;
                        }

                        if (ItemBase.Get(itemNum) != null)
                        {
                            var itemdata = ItemBase.Get(itemNum);
                            if (Globals.Me.Gender == 0)
                            {
                                paperdoll = itemdata.MalePaperdoll;
                            }
                            else
                            {
                                paperdoll = itemdata.FemalePaperdoll;
                            }
                        }
                    }
                    // Do we have a cosmetic to display?
                    else if (cosmetics.ElementAtOrDefault(equipmentSlot) != default)
                    {
                        var itemNum = cosmetics[equipmentSlot];

                        if (ItemBase.Get(itemNum) != null)
                        {
                            var itemdata = ItemBase.Get(itemNum);
                            if (Globals.Me.Gender == 0)
                            {
                                paperdoll = itemdata.MalePaperdoll;
                            }
                            else
                            {
                                paperdoll = itemdata.FemalePaperdoll;
                            }
                        }
                    }

                }
                else if (Options.DecorSlots.IndexOf(Options.PaperdollOrder[1][z]) > -1)
                {
                    var slotToDraw = Options.DecorSlots.IndexOf(Options.PaperdollOrder[1][z]);
                    if ((slotToDraw == Options.HairSlot && hideHair && !shortHair)
                        || slotToDraw == Options.BeardSlot && hideBeard
                        || slotToDraw == Options.ExtraSlot && hideExtra)
                    {
                        paperdoll = "";
                    }
                    else
                    {
                        if (slotToDraw == Options.HairSlot
                            && shortHair
                            && !string.IsNullOrEmpty(Globals.Me.MyDecors[slotToDraw])
                            && Options.Instance.PlayerOpts.ShortHairMappings.TryGetValue(Globals.Me.MyDecors[slotToDraw], out var hairText))
                        {
                            paperdoll = hairText;
                        }
                        else
                        {
                            paperdoll = Globals.Me.MyDecors[slotToDraw];
                        }
                    }

                    textureGroup = GameContentManager.TextureType.Decor;
                }
                else if (Options.PaperdollOrder[1][z] == "Player")
                {
                    PaperdollPanels[z].Show();
                    PaperdollPanels[z].Texture = entityTex;
                    PaperdollPanels[z].SetTextureRect(0, 0, entityTex.GetWidth() / Options.Instance.Sprites.NormalFrames, entityTex.GetHeight() / Options.Instance.Sprites.Directions);
                    PaperdollPanels[z].SizeToContentsScaled();
                    PaperdollPanels[z].RenderColor = Globals.Me.Color;
                    Align.Center(PaperdollPanels[z]);
                }

                if (string.IsNullOrWhiteSpace(paperdoll) && !string.IsNullOrWhiteSpace(PaperdollTextures[z]) && Options.PaperdollOrder[1][z] != "Player")
                {
                    PaperdollPanels[z].Texture = null;
                    PaperdollPanels[z].Hide();
                    PaperdollTextures[z] = "";
                }
                else if (!string.IsNullOrEmpty(paperdoll) && paperdoll != PaperdollTextures[z])
                {
                    var paperdollTex = Globals.ContentManager.GetTexture(textureGroup, paperdoll);

                    PaperdollPanels[z].Texture = paperdollTex;
                    if (paperdollTex != null)
                    {
                        PaperdollPanels[z]
                            .SetTextureRect(
                                0, 0, PaperdollPanels[z].Texture.GetWidth() / Options.Instance.Sprites.NormalFrames,
                                PaperdollPanels[z].Texture.GetHeight() / Options.Instance.Sprites.Directions
                            );

                        PaperdollPanels[z]
                            .SetSize(
                                PaperdollPanels[z].Texture.ScaledWidth / Options.Instance.Sprites.NormalFrames,
                                PaperdollPanels[z].Texture.ScaledHeight / Options.Instance.Sprites.Directions
                            );

                        PaperdollPanels[z]
                            .SetPosition(
                                mCharacterBackground.Width / 2 - PaperdollPanels[z].Width / 2,
                                mCharacterBackground.Height / 2 - PaperdollPanels[z].Height / 2
                            );
                    }

                    PaperdollPanels[z].Show();
                    PaperdollTextures[z] = paperdoll;
                }
            }
        }

        private void InitializeCharacterInfo()
        {
            mName = GenerateLabel("CharacterName", Globals.Me?.Name ?? string.Empty);
            mLevelAndClass = GenerateLabel("CharacterLevelAndClass");
            mClassRank = GenerateLabel("ClassRank");
        }

        private void InitializeEquipmentStatContainers()
        {
            ContainerComponents = new ComponentList<IGwenComponent>();
            
            mBluntAtk = new NumberContainerComponent(mBackground, "BluntAttackContainer", AtkLabelColor, StatColor, "ATK", "Blunt Attack Damage", ContainerComponents);
            mBluntDef = new NumberContainerComponent(mBackground, "BluntDefenseContainer", DefLabelColor, StatColor, "DEF", "Blunt Attack Resistance", ContainerComponents);

            mSlashAtk = new NumberContainerComponent(mBackground, "SlashAttackContainer", AtkLabelColor, StatColor, "ATK", "Slash Attack Damage", ContainerComponents);
            mSlashDef = new NumberContainerComponent(mBackground, "SlashDefenseContainer", DefLabelColor, StatColor, "DEF", "Slash Attack Resistance", ContainerComponents);

            mPierceAtk = new NumberContainerComponent(mBackground, "PierceAttackContainer", AtkLabelColor, StatColor, "ATK", "Pierce Attack Damage", ContainerComponents);
            mPierceDef = new NumberContainerComponent(mBackground, "PierceDefenseContainer", DefLabelColor, StatColor, "DEF", "Pierce Attack Resistance", ContainerComponents);

            mMagicAtk = new NumberContainerComponent(mBackground, "MagicAttackContainer", AtkLabelColor, StatColor, "ATK", "Magic Attack Damage", ContainerComponents);
            mMagicDef = new NumberContainerComponent(mBackground, "MagicDefenseContainer", DefLabelColor, StatColor, "DEF", "Magic Attack Resistance", ContainerComponents);
        }

        private void InitializeCharacterDisplay()
        {
            mCharacterBackground = new ImagePanel(mBackground, "CharacterContainer");
            mCharacter = new ImagePanel(mCharacterBackground);
            var paperdollSlots = Options.EquipmentSlots.Count + Options.DecorSlots.Count;
            PaperdollPanels = new ImagePanel[paperdollSlots + 1];
            PaperdollTextures = new string[paperdollSlots + 1];
            for (var i = 0; i <= paperdollSlots; i++)
            {
                PaperdollPanels[i] = new ImagePanel(mCharacterBackground);
                PaperdollTextures[i] = "";
                PaperdollPanels[i].Hide();
            }

            for (var i = 0; i < Options.EquipmentSlots.Count; i++)
            {
                Items.Add(new EquipmentItem(i, mParent.Window));
                Items[i].Pnl = new ImagePanel(mBackground, "EquipmentItem" + i);
                Items[i].Setup();
            }
        }

        private void SetCharacterBackground()
        {
            if (Globals.Me == null)
            {
                return;
            }

            if (Globals.Me.MapInstance?.IsIndoors ?? false)
            {
                mCharacterBackground.Texture = InteriorBgTexture;
                return;
            }

            if ((Graphics.BrightnessLevel / 255) * 100 > 70)
            {
                mCharacterBackground.Texture = DayBgTexture;
                return;
            }

            mCharacterBackground.Texture = NightBgTexture;
            return;
        }

        struct EquipRow
        {
            public EquipRow(Player player, Stats attackStat, Stats defenseStat)
            {
                if (player == null)
                {
                    throw new ArgumentNullException(nameof(player));
                }

                Attack = player.Stat[(int)attackStat].ToString();
                Defense = player.Stat[(int)defenseStat].ToString();
            }

            public string Attack { get; set; }
            public string Defense { get; set; }
        }
    }
}
