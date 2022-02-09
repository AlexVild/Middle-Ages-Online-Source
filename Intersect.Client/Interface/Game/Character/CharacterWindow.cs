using System;
using System.Collections.Generic;
using System.Text;
using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Enums;
using Intersect.GameObjects;

namespace Intersect.Client.Interface.Game.Character
{

    public class CharacterWindow
    {

        //Equipment List
        public List<EquipmentItem> Items = new List<EquipmentItem>();

        Label mAbilityPwrLabel;

        Button mAddAbilityPwrBtn;

        Button mAddAttackBtn;

        Button mAddDefenseBtn;

        Button mAddMagicResistBtn;

        Button mAddSpeedBtn;

        //Stats
        Label mAttackLabel;

        private ImagePanel mCharacterContainer;

        private Label mCharacterLevelAndClass;

        private ImagePanel mCharacterPortrait;

        private string mCharacterPortraitImg = "";

        private CheckBox mCalcStatCheckbox;

        //Controls
        private WindowControl mCharacterWindow;

        private string mCurrentSprite = "";

        Label mDefenseLabel;

        private int[] mEmptyStatBoost = new int[(int)Stats.StatCount];

        Label mMagicRstLabel;

        Label mPointsLabel;

        Label mSpeedLabel;

        public ImagePanel[] PaperdollPanels;

        public string[] PaperdollTextures;

        // Crafting
        Label mCraftingLabel;

        Label mMiningTierLabel;

        Label mFishingTierLabel;

        Label mWoodcuttingTierLabel;

        // NPC Guild
        Label mNpcGuildLabel;

        //Location
        public int X;

        public int Y;

        //Init
        public CharacterWindow(Canvas gameCanvas)
        {
            mCharacterWindow = new WindowControl(gameCanvas, Globals.Me.Name, false, "CharacterWindow");
            mCharacterWindow.DisableResizing();

            mCharacterLevelAndClass = new Label(mCharacterWindow, "ChatacterInfoLabel");
            mCharacterLevelAndClass.SetText("");

            mCharacterContainer = new ImagePanel(mCharacterWindow, "CharacterContainer");

            mCharacterPortrait = new ImagePanel(mCharacterContainer);

            var paperdollSlots = Options.EquipmentSlots.Count + Options.DecorSlots.Count;
            PaperdollPanels = new ImagePanel[paperdollSlots + 1];
            PaperdollTextures = new string[paperdollSlots + 1];
            for (var i = 0; i <= paperdollSlots; i++)
            {
                PaperdollPanels[i] = new ImagePanel(mCharacterContainer);
                PaperdollTextures[i] = "";
                PaperdollPanels[i].Hide();
            }

            var equipmentLabel = new Label(mCharacterWindow, "EquipmentLabel");
            equipmentLabel.SetText(Strings.Character.equipment);

            var statsLabel = new Label(mCharacterWindow, "StatsLabel");
            statsLabel.SetText(Strings.Character.stats);

            mAttackLabel = new Label(mCharacterWindow, "AttackLabel");
            mAttackLabel.SetToolTipText(Strings.Character.attacktip);
            mAddAttackBtn = new Button(mCharacterWindow, "IncreaseAttackButton");
            mAddAttackBtn.Clicked += _addAttackBtn_Clicked;
            mAddAttackBtn.SetToolTipText(Strings.Character.addattacktip);

            mDefenseLabel = new Label(mCharacterWindow, "DefenseLabel");
            mDefenseLabel.SetToolTipText(Strings.Character.defensetip);
            mAddDefenseBtn = new Button(mCharacterWindow, "IncreaseDefenseButton");
            mAddDefenseBtn.Clicked += _addDefenseBtn_Clicked;
            mAddDefenseBtn.SetToolTipText(Strings.Character.addphysicaldefensetip);

            mSpeedLabel = new Label(mCharacterWindow, "SpeedLabel");
            mSpeedLabel.SetToolTipText(Strings.Character.agilitytip);
            mAddSpeedBtn = new Button(mCharacterWindow, "IncreaseSpeedButton");
            mAddSpeedBtn.Clicked += _addSpeedBtn_Clicked;
            mAddSpeedBtn.SetToolTipText(Strings.Character.addagilitytip);

            mAbilityPwrLabel = new Label(mCharacterWindow, "AbilityPowerLabel");
            mAbilityPwrLabel.SetToolTipText(Strings.Character.abilitypowertip);
            mAddAbilityPwrBtn = new Button(mCharacterWindow, "IncreaseAbilityPowerButton");
            mAddAbilityPwrBtn.Clicked += _addAbilityPwrBtn_Clicked;
            mAddAbilityPwrBtn.SetToolTipText(Strings.Character.addabilitypowertip);

            mMagicRstLabel = new Label(mCharacterWindow, "MagicResistLabel");
            mMagicRstLabel.SetToolTipText(Strings.Character.resisttip);
            mAddMagicResistBtn = new Button(mCharacterWindow, "IncreaseMagicResistButton");
            mAddMagicResistBtn.Clicked += _addMagicResistBtn_Clicked;
            mAddMagicResistBtn.SetToolTipText(Strings.Character.addmagicdefense);

            mPointsLabel = new Label(mCharacterWindow, "PointsLabel");

            mCraftingLabel = new Label(mCharacterWindow, "CraftingLabel");
            mMiningTierLabel = new Label(mCharacterWindow, "MiningTierLabel");
            mFishingTierLabel = new Label(mCharacterWindow, "FishingTierLabel");
            mWoodcuttingTierLabel = new Label(mCharacterWindow, "WoodcuttingTierLabel");

            mCalcStatCheckbox = new CheckBox(mCharacterWindow, "CalcStatsCheckBox");
            mCalcStatCheckbox.SetToolTipText(Strings.Character.calculatestats);

            mNpcGuildLabel = new Label(mCharacterWindow, "NPCGuildLabel");
            mNpcGuildLabel.SetToolTipText(Strings.Character.classranktip);

            for (var i = 0; i < Options.EquipmentSlots.Count; i++)
            {
                Items.Add(new EquipmentItem(i, mCharacterWindow));
                Items[i].Pnl = new ImagePanel(mCharacterWindow, "EquipmentItem" + i);
                Items[i].Setup();
            }

            mCharacterWindow.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        }

        //Update Button Event Handlers
        void _addMagicResistBtn_Clicked(Base sender, ClickedEventArgs arguments)
        {
            PacketSender.SendUpgradeStat((int) Stats.MagicResist);
        }

        void _addAbilityPwrBtn_Clicked(Base sender, ClickedEventArgs arguments)
        {
            PacketSender.SendUpgradeStat((int) Stats.AbilityPower);
        }

        void _addSpeedBtn_Clicked(Base sender, ClickedEventArgs arguments)
        {
            PacketSender.SendUpgradeStat((int) Stats.Speed);
        }

        void _addDefenseBtn_Clicked(Base sender, ClickedEventArgs arguments)
        {
            PacketSender.SendUpgradeStat((int) Stats.Defense);
        }

        void _addAttackBtn_Clicked(Base sender, ClickedEventArgs arguments)
        {
            PacketSender.SendUpgradeStat((int) Stats.Attack);
        }

        //Methods
        public void Update()
        {
            if (mCharacterWindow.IsHidden)
            {
                return;
            }

            mCharacterLevelAndClass.Text = Strings.Character.levelandclass.ToString(
                Globals.Me.Level, ClassBase.GetName(Globals.Me.Class)
            );

            //Load Portrait
            //UNCOMMENT THIS LINE IF YOU'D RATHER HAVE A FACE HERE GameTexture faceTex = Globals.ContentManager.GetTexture(GameContentManager.TextureType.Face, Globals.Me.Face);
            var entityTex = Globals.ContentManager.GetTexture(
                GameContentManager.TextureType.Entity, Globals.Me.MySprite
            );

            /* UNCOMMENT THIS BLOCK IF YOU"D RATHER HAVE A FACE HERE if (Globals.Me.Face != "" && Globals.Me.Face != _currentSprite && faceTex != null)
             {
                 _characterPortrait.Texture = faceTex;
                 _characterPortrait.SetTextureRect(0, 0, faceTex.GetWidth(), faceTex.GetHeight());
                 _characterPortrait.SizeToContents();
                 Align.Center(_characterPortrait);
                 _characterPortrait.IsHidden = false;
                 for (int i = 0; i < Options.EquipmentSlots.Count; i++)
                 {
                     _paperdollPanels[i].Hide();
                 }
             }
             else */
            if (Globals.Me.MySprite != "" && Globals.Me.MySprite != mCurrentSprite && entityTex != null)
            {
                // Figure out if there are decors we shouldn't draw
                var hideHair = false;
                var hideBeard = false;
                var hideExtra = false;
                if (Globals.Me.MyEquipment[Options.HelmetIndex] > -1)
                {
                    var helmet = ItemBase.Get(Globals.Me.Inventory[Globals.Me.MyEquipment[Options.HelmetIndex]].ItemId);
                    if (helmet != null)
                    {
                        hideHair = helmet.HideHair;
                        hideBeard = helmet.HideBeard;
                        hideExtra = helmet.HideExtra;
                    }
                }

                for (var z = 0; z < Options.PaperdollOrder[1].Count; z++)
                {
                    var paperdoll = "";
                    var textureGroup = GameContentManager.TextureType.Paperdoll;
                    if (Options.EquipmentSlots.IndexOf(Options.PaperdollOrder[1][z]) > -1)
                    {
                        var equipment = Globals.Me.MyEquipment;
                        if (equipment[Options.EquipmentSlots.IndexOf(Options.PaperdollOrder[1][z])] > -1 &&
                            equipment[Options.EquipmentSlots.IndexOf(Options.PaperdollOrder[1][z])] <
                            Options.MaxInvItems)
                        {
                            var itemNum = Globals.Me
                                .Inventory[equipment[Options.EquipmentSlots.IndexOf(Options.PaperdollOrder[1][z])]]
                                .ItemId;

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
                        if (slotToDraw == Options.HairSlot && hideHair
                            || slotToDraw == Options.BeardSlot && hideBeard
                            || slotToDraw == Options.ExtraSlot && hideExtra)
                        {
                            paperdoll = "";
                        } else
                        {
                            paperdoll = Globals.Me.MyDecors[slotToDraw];
                        }
                        
                        textureGroup = GameContentManager.TextureType.Decor;
                    }
                    else if (Options.PaperdollOrder[1][z] == "Player")
                    {
                        PaperdollPanels[z].Show();
                        PaperdollPanels[z].Texture = entityTex;
                        PaperdollPanels[z].SetTextureRect(0, 0, entityTex.GetWidth() / Options.Instance.Sprites.NormalFrames, entityTex.GetHeight() / Options.Instance.Sprites.Directions);
                        PaperdollPanels[z].SizeToContents();
                        PaperdollPanels[z].RenderColor = Globals.Me.Color;
                        Align.Center(PaperdollPanels[z]);
                    }

                    if (string.IsNullOrWhiteSpace(paperdoll) && !string.IsNullOrWhiteSpace(PaperdollTextures[z]) && Options.PaperdollOrder[1][z] != "Player")
                    {
                        PaperdollPanels[z].Texture = null;
                        PaperdollPanels[z].Hide();
                        PaperdollTextures[z] = "";
                    }
                    else if (paperdoll != "" && paperdoll != PaperdollTextures[z])
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
                                    PaperdollPanels[z].Texture.GetWidth() / Options.Instance.Sprites.NormalFrames,
                                    PaperdollPanels[z].Texture.GetHeight() / Options.Instance.Sprites.Directions
                                );

                            PaperdollPanels[z]
                                .SetPosition(
                                    mCharacterContainer.Width / 2 - PaperdollPanels[z].Width / 2,
                                    mCharacterContainer.Height / 2 - PaperdollPanels[z].Height / 2
                                );
                        }

                        PaperdollPanels[z].Show();
                        PaperdollTextures[z] = paperdoll;
                    }
                }
            }
            else if (Globals.Me.MySprite != mCurrentSprite && Globals.Me.Face != mCurrentSprite)
            {
                mCharacterPortrait.IsHidden = true;
                for (var i = 0; i < Options.EquipmentSlots.Count + Options.DecorSlots.Count; i++)
                {
                    PaperdollPanels[i].Hide();
                }
            }

            mAttackLabel.SetText(
                Strings.Character.stat0.ToString(Strings.Combat.stat0, GetStatDisplayString(Stats.Attack, mCalcStatCheckbox.IsChecked))
            );

            mDefenseLabel.SetText(
                Strings.Character.stat2.ToString(Strings.Combat.stat2, GetStatDisplayString(Stats.Defense, mCalcStatCheckbox.IsChecked))
            );

            mSpeedLabel.SetText(
                Strings.Character.stat4.ToString(Strings.Combat.stat4, GetStatDisplayString(Stats.Speed, mCalcStatCheckbox.IsChecked))
            );

            mAbilityPwrLabel.SetText(
                Strings.Character.stat1.ToString(Strings.Combat.stat1, GetStatDisplayString(Stats.AbilityPower, mCalcStatCheckbox.IsChecked))
            );

            mMagicRstLabel.SetText(
                Strings.Character.stat3.ToString(Strings.Combat.stat3, GetStatDisplayString(Stats.MagicResist, mCalcStatCheckbox.IsChecked))
            );

            mPointsLabel.SetText(Strings.Character.points.ToString(Globals.Me.StatPoints));
            mPointsLabel.IsHidden = Globals.Me.StatPoints == 0;
            mAddAbilityPwrBtn.IsHidden = Globals.Me.StatPoints == 0 ||
                                         Globals.Me.TrueStats[(int) Stats.AbilityPower] == Options.MaxStatValue;

            mAddAttackBtn.IsHidden =
                Globals.Me.StatPoints == 0 || Globals.Me.TrueStats[(int) Stats.Attack] == Options.MaxStatValue;

            mAddDefenseBtn.IsHidden = Globals.Me.StatPoints == 0 ||
                                      Globals.Me.TrueStats[(int) Stats.Defense] == Options.MaxStatValue;

            mAddMagicResistBtn.IsHidden = Globals.Me.StatPoints == 0 ||
                                          Globals.Me.TrueStats[(int) Stats.MagicResist] == Options.MaxStatValue;

            mAddSpeedBtn.IsHidden =
                Globals.Me.StatPoints == 0 || Globals.Me.TrueStats[(int) Stats.Speed] == Options.MaxStatValue;

            mCraftingLabel.Text = Strings.Character.crafting;
            mMiningTierLabel.SetText(Strings.Character.miningtier.ToString(Globals.Me.MiningTier));
            mFishingTierLabel.SetText(Strings.Character.fishingtier.ToString(Globals.Me.FishingTier));
            mWoodcuttingTierLabel.SetText(Strings.Character.woodcuttingtier.ToString(Globals.Me.WoodcutTier));

            InitializeClassRankLabel(ref mNpcGuildLabel);

            for (var i = 0; i < Options.EquipmentSlots.Count; i++)
            {
                if (Globals.Me.MyEquipment[i] > -1 && Globals.Me.MyEquipment[i] < Options.MaxInvItems)
                {
                    if (Globals.Me.Inventory[Globals.Me.MyEquipment[i]].ItemId != Guid.Empty)
                    {
                        Items[i]
                            .Update(
                                Globals.Me.Inventory[Globals.Me.MyEquipment[i]].ItemId,
                                Globals.Me.Inventory[Globals.Me.MyEquipment[i]].StatBuffs
                            );
                    }
                    else
                    {
                        Items[i].Update(Guid.Empty, mEmptyStatBoost);
                    }
                }
                else
                {
                    Items[i].Update(Guid.Empty, mEmptyStatBoost);
                }
            }
        }

        public void Show()
        {
            mCharacterWindow.IsHidden = false;
        }

        public bool IsVisible()
        {
            return !mCharacterWindow.IsHidden;
        }

        public void Hide()
        {
            mCharacterWindow.IsHidden = true;
        }

        private static void InitializeClassRankLabel(ref Label label)
        {
            StringBuilder classRankString = new StringBuilder();
            if (Globals.Me.ClassRanks != null)
            {
                foreach (var cls in ClassBase.GetNameList())
                {
                    if (Globals.Me.ClassRanks.TryGetValue(cls, out var classRank))
                    {
                        if (classRank > 0)
                        {
                            if (classRankString.Length == 0)
                            {
                                classRankString.Append(Strings.Character.classrank.ToString(ClassBase.GetName(Globals.Me.Class), classRank));
                            }
                            else
                            {
                                classRankString.Append(", " + Strings.Character.classrank.ToString(ClassBase.GetName(Globals.Me.Class), classRank));
                            }
                        }
                    }
                }
            }

            label.Text = classRankString.ToString();
        }
        
        /// <summary>
        /// Generates a string that displays the difference between the player's true stat and their equipment/spell modified stat
        /// </summary>
        /// <param name="stat">The stat to generate a string for</param>
        /// <param name="calculate">Whether or not to calculate the value
        /// <returns>The display string</returns>
        private static string GetStatDisplayString(Stats stat, bool calculate)
        {
            if (calculate)
            {
                return Globals.Me.Stat[(int)stat].ToString();
            }
            else
            {
                var statDiff = Globals.Me.Stat[(int)stat] - Globals.Me.TrueStats[(int)stat];
                if (Math.Sign(statDiff) >= 0)
                {
                    return Globals.Me.TrueStats[(int)stat].ToString() + " (+" + statDiff + ")";
                }
                else
                {
                    return Globals.Me.TrueStats[(int)stat].ToString() + " (" + statDiff + ")";
                }
            }
            
        }
    }
}
