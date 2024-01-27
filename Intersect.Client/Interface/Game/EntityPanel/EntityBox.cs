using System;
using System.Collections.Generic;
using System.Linq;

using Intersect.Client.Core;
using Intersect.Client.Entities;
using Intersect.Client.Entities.Events;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.General.Bestiary;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using Intersect.Logging;
using Intersect.Utilities;

namespace Intersect.Client.Interface.Game.EntityPanel
{

    public class EntityBox
    {

        private static int sStatusXPadding = 2;

        private static int sStatusYPadding = 2;

        public readonly Framework.Gwen.Control.Label EntityLevel;

        public readonly Framework.Gwen.Control.Label EntityMap;

        public readonly Framework.Gwen.Control.Label EntityName;

        public readonly Framework.Gwen.Control.Label EntityNameAndLevel;

        //Controls
        public readonly ImagePanel EntityWindow;

        public float CurExpWidth = -1;

        public float CurHpWidth = -1;

        public float CurMpWidth = -1;

        public float CurShieldWidth = -1;

        public ImagePanel EntityFace;

        public ImagePanel EntityFaceContainer;

        public ImagePanel EntityInfoPanel;

        public ImagePanel EntityStatusPanel;

        public EntityTypes EntityType;

        public RichLabel EventDesc;

        public ImagePanel ExpBackground;

        public ImagePanel ExpBar;

        public Framework.Gwen.Control.Label ExpLbl;

        public Framework.Gwen.Control.Label ExpTitle;

        public Button FriendLabel;

        public ImagePanel HpBackground;

        public ImagePanel HpBar;

        public Framework.Gwen.Control.Label HpLbl;

        public Framework.Gwen.Control.Label HpTitle;

        private Dictionary<Guid, SpellStatus> mActiveStatuses = new Dictionary<Guid, SpellStatus>();

        private string mCurrentSprite = "";

        private long mLastUpdateTime;

        public ImagePanel MpBackground;

        public ImagePanel MpBar;

        public Framework.Gwen.Control.Label MpLbl;

        public Framework.Gwen.Control.Label MpTitle;

        public Entity MyEntity;

        public ImagePanel[] PaperdollPanels;

        public string[] PaperdollTextures;

        public Button PartyLabel;

        public bool PlayerBox;

        public ImagePanel ShieldBar;

        public Button TradeLabel;

        public bool UpdateStatuses;

        public bool IsHidden;

        public Button GuildLabel;

        private Framework.Gwen.Control.Label ThreatLevelLabel;
        
        private Framework.Gwen.Control.Label ThreatLevelText;
        
        private ImagePanel ThreatLevelContainer;

        private static Bestiary MyBestiary => BestiaryController.MyBestiary;

        private Framework.Gwen.Control.Label ChanceToHitLabel;

        private ImagePanel ChanceToHitIcon { get; set; }

        //Init
        public EntityBox(Canvas gameCanvas, EntityTypes entityType, Entity myEntity, bool playerBox = false)
        {
            MyEntity = myEntity;
            EntityType = entityType;
            PlayerBox = playerBox;
            EntityWindow =
                playerBox ? new ImagePanel(gameCanvas, "PlayerBox") : new ImagePanel(gameCanvas, "TargetBox");

            EntityInfoPanel = new ImagePanel(EntityWindow, "EntityInfoPanel");

            EntityName = new Framework.Gwen.Control.Label(EntityInfoPanel, "EntityNameLabel") {Text = myEntity?.Name.ToUpper()};
            EntityLevel = new Framework.Gwen.Control.Label(EntityInfoPanel, "EntityLevelLabel");
            EntityNameAndLevel = new Framework.Gwen.Control.Label(EntityInfoPanel, "NameAndLevelLabel")
                {IsHidden = true};

            EntityMap = new Framework.Gwen.Control.Label(EntityInfoPanel, "EntityMapLabel");


            var totalPaperdolls = Options.EquipmentSlots.Count + Options.DecorSlots.Count;
            PaperdollPanels = new ImagePanel[totalPaperdolls];
            PaperdollTextures = new string[totalPaperdolls];
            var i = 0;
            for (var z = 0; z < Options.PaperdollOrder[1].Count; z++)
            {
                if (Options.PaperdollOrder[1][z] == "Player")
                {
                    EntityFaceContainer = new ImagePanel(EntityInfoPanel, "EntityGraphicContainer");

                    EntityFace = new ImagePanel(EntityFaceContainer);
                    EntityFace.SetSize(64, 64);
                    EntityFace.AddAlignment(Alignments.Center);
                }
                else
                {
                    PaperdollPanels[i] = new ImagePanel(EntityFaceContainer);
                    PaperdollTextures[i] = "";
                    PaperdollPanels[i].Hide();
                    i++;
                }
            }

            EventDesc = new RichLabel(EntityInfoPanel, "EventDescLabel");

            HpBackground = new ImagePanel(EntityInfoPanel, "HPBarBackground");
            HpBar = new ImagePanel(EntityInfoPanel, "HPBar");
            ShieldBar = new ImagePanel(EntityInfoPanel, "ShieldBar");
            HpTitle = new Framework.Gwen.Control.Label(EntityInfoPanel, "HPTitle");
            HpTitle.SetText(Strings.EntityBox.vital0);
            HpLbl = new Framework.Gwen.Control.Label(EntityInfoPanel, "HPLabel");

            MpBackground = new ImagePanel(EntityInfoPanel, "MPBackground");
            MpBar = new ImagePanel(EntityInfoPanel, "MPBar");
            MpTitle = new Framework.Gwen.Control.Label(EntityInfoPanel, "MPTitle");
            MpTitle.SetText(Strings.EntityBox.vital1);
            MpLbl = new Framework.Gwen.Control.Label(EntityInfoPanel, "MPLabel");

            ThreatLevelContainer = new ImagePanel(EntityInfoPanel, "ThreatLevelContainer");
            ThreatLevelLabel = new Framework.Gwen.Control.Label(ThreatLevelContainer, "ThreatLevelLabel")
            {
                Text = "Threat Lvl:"
            };
            ThreatLevelText = new Framework.Gwen.Control.Label(ThreatLevelContainer, "ThreatLevel");

            ExpBackground = new ImagePanel(EntityInfoPanel, "EXPBackground");
            ExpBar = new ImagePanel(EntityInfoPanel, "EXPBar");
            ExpTitle = new Framework.Gwen.Control.Label(EntityInfoPanel, "EXPTitle");
            ExpTitle.SetText(Strings.EntityBox.exp);
            ExpLbl = new Framework.Gwen.Control.Label(EntityInfoPanel, "EXPLabel");

            TradeLabel = new Button(EntityWindow, "TradeButton");
            TradeLabel.SetText(Strings.EntityBox.trade);
            TradeLabel.SetToolTipText(Strings.EntityBox.tradetip.ToString(MyEntity?.Name));
            TradeLabel.Clicked += tradeRequest_Clicked;

            PartyLabel = new Button(EntityWindow, "PartyButton");
            PartyLabel.SetText(Strings.EntityBox.party);
            PartyLabel.SetToolTipText(Strings.EntityBox.partytip.ToString(MyEntity?.Name));
            PartyLabel.Clicked += invite_Clicked;

            FriendLabel = new Button(EntityWindow, "FriendButton");
            FriendLabel.SetText(Strings.EntityBox.friend);
            FriendLabel.SetToolTipText(Strings.EntityBox.friendtip.ToString(MyEntity?.Name));
            FriendLabel.Clicked += friendRequest_Clicked;
            FriendLabel.IsHidden = true;

            GuildLabel = new Button(EntityWindow, "GuildButton");
            GuildLabel.SetText(Strings.Guilds.Guild);
            GuildLabel.SetToolTipText(Strings.Guilds.guildtip.ToString(MyEntity?.Name));
            GuildLabel.Clicked += guildRequest_Clicked;
            GuildLabel.IsHidden = true;

            ChanceToHitLabel = new Framework.Gwen.Control.Label(EntityWindow, "ChanceToHitLabel");
            ChanceToHitIcon = new ImagePanel(EntityWindow, "ChanceToHitIcon");

            EntityStatusPanel = new ImagePanel(EntityWindow, "StatusArea");

            SetEntity(myEntity);

            EntityWindow.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

            UpdateSpellStatus();

            i = 0;
            for (var z = 0; z < Options.PaperdollOrder[1].Count; z++)
            {
                if (Options.PaperdollOrder[1][z] == "Player")
                {
                    EntityFace.RenderColor = EntityFaceContainer.RenderColor;
                }
                else
                {
                    PaperdollPanels[i].RenderColor = EntityFaceContainer.RenderColor;
                    i++;
                }
            }

            EntityWindow.Hide();

            mLastUpdateTime = Timing.Global.Milliseconds;
        }

        public void SetEntity(Entity entity)
        {
            MyEntity = entity;
            if (MyEntity != null)
            {
                RefreshEntity();
            }
        }

        public void SetEntity(Entity entity, EntityTypes type)
        {
            MyEntity = entity;
            EntityType = type;
            if (MyEntity != null)
            {
                RefreshEntity();
            }
        }

        public void RefreshEntity()
        {
            UpdateThreatLevel();
            SetupEntityElements();
            UpdateSpellStatus();
            if (EntityType == EntityTypes.Event)
            {
                EventDesc.ClearText();
                EventDesc.AddText(((Event)MyEntity).Desc, Color.White);
                EventDesc.SizeToChildren(false, true);
            }
        }

        public void ShowAllElements()
        {
            TradeLabel.Show();
            PartyLabel.Show();
            FriendLabel.Show();
            ExpBackground.Show();
            ExpBar.Show();
            ExpLbl.Show();
            ExpTitle.Show();
            EntityMap.Show();
            EventDesc.Show();
            MpBackground.Show();
            MpBar.Show();
            MpTitle.Show();
            MpLbl.Show();
            HpBackground.Show();
            HpBar.Show();
            HpLbl.Show();
            HpTitle.Show();

            TryShowGuildButton();
        }

        public void SetupEntityElements()
        {
            ShowAllElements();

            //Update Bars
            CurHpWidth = -1;
            CurShieldWidth = -1;
            CurMpWidth = -1;
            CurExpWidth = -1;
            ShieldBar.Hide();
            UpdateHpBar(0, true);
            UpdateMpBar(0, true);
            if (MyEntity is Entities.Player)
            {
                UpdateXpBar(0, true);
            }

            switch (EntityType)
            {
                case EntityTypes.Player:
                    if (Globals.Me != null && Globals.Me == MyEntity)
                    {
                        TradeLabel.Hide();
                        PartyLabel.Hide();
                        FriendLabel.Hide();
                        GuildLabel.Hide();

                        if (!PlayerBox)
                        {
                            ExpBackground.Hide();
                            ExpBar.Hide();
                            ExpLbl.Hide();
                            ExpTitle.Hide();
                            EntityMap.Hide();
                        }
                    }
                    else
                    {
                        if (MyEntity is Player player)
                        {
                            TryShowPartyButton();
                            TryShowGuildButton();
                            TryShowFriendButton();
                        }
                        
                        ExpBackground.Hide();
                        ExpBar.Hide();
                        ExpLbl.Hide();
                        ExpTitle.Hide();
                        EntityMap.Hide();
                    }

                    EventDesc.Hide();

                    break;
                case EntityTypes.GlobalEntity:
                    EventDesc.Hide();
                    ExpBackground.Hide();
                    ExpBar.Hide();
                    ExpLbl.Hide();
                    ExpTitle.Hide();
                    TradeLabel.Hide();
                    PartyLabel.Hide();
                    GuildLabel.Hide();
                    FriendLabel.Hide();
                    EntityMap.Hide();

                    break;
                case EntityTypes.Event:
                    EventDesc.Show();
                    ExpBackground.Hide();
                    ExpBar.Hide();
                    ExpLbl.Hide();
                    ExpTitle.Hide();
                    MpBackground.Hide();
                    MpBar.Hide();
                    MpTitle.Hide();
                    MpLbl.Hide();
                    HpBackground.Hide();
                    HpBar.Hide();
                    HpLbl.Hide();
                    HpTitle.Hide();
                    TradeLabel.Hide();
                    PartyLabel.Hide();
                    FriendLabel.Hide();
                    GuildLabel.Hide();
                    EntityMap.Hide();

                    break;
            }
            ShieldBar.Hide();
        }

        private void TryShowPartyButton()
        {
            if (Globals.Me == null)
            {
                return;
            }

            if (MyEntity is Player player)
            {
                PartyLabel.IsHidden = Globals.Me.IsInMyParty(player) || player.IsInMyParty(Globals.Me.Id);
            }
        }

        private void TryShowFriendButton()
        {
            if (Globals.Me == null)
            {
                return;
            }

            if (MyEntity is Player player)
            {
                FriendLabel.IsHidden = Globals.Me.Friends.Select(f => f.Name).Contains(player.Name);
            }
        }

        //Update
        public void Update()
        {
            if (MyEntity == null || MyEntity.IsDisposed())
            {
                if (!EntityWindow.IsHidden)
                {
                    EntityWindow.Hide();
                }

                return;
            }
            else
            {
                if (EntityWindow.IsHidden)
                {
                    EntityWindow.Show();
                }
            }

            if (PlayerBox)
            {
                EntityWindow.Show();

                if (MyEntity.IsDisposed())
                {
                    Dispose();
                }
            }

            UpdateSpellStatus();

            //Time since this window was last updated (for bar animations)
            var elapsedTime = (Timing.Global.Milliseconds - mLastUpdateTime) / 1000.0f;

            //Update the event/entity face.
            UpdateImage();

            if (Globals.Me.InCutscene())
            {
                EntityWindow.Hide();
            }
            else
            {
                EntityWindow.Show();
            }

            IsHidden = true;
            if (EntityType != EntityTypes.Event)
            {
                if (MyEntity.NpcId != default)
                {
                    var text = MyBestiary.HasUnlock(MyEntity.NpcId, BestiaryUnlock.NameAndDescription) ? MyEntity.Name : "???";
                    var npcBase = NpcBase.Get(MyEntity.NpcId);
                    if (npcBase == default)
                    {
                        EntityName.SetText(text.ToUpper());
                    }
                    else
                    {
                        if (npcBase.HideName)
                        {
                            text = "???";
                        }
                        EntityName.SetText($"{text.ToUpper()} [Tr. {npcBase.Level}]");
                    }
                    
                }
                else
                {
                    EntityName.SetText(MyEntity.Name.ToUpper());
                }
                UpdateLevel();
                UpdateMap();
                UpdateHpBar(elapsedTime);
                UpdateMpBar(elapsedTime);
                
                IsHidden = false;
            }
            else
            {
                if (!EntityNameAndLevel.IsHidden)
                {
                    EntityNameAndLevel.Text = MyEntity.Name.ToUpper();
                }
            }

            //If player draw exp bar
            if (PlayerBox && MyEntity == Globals.Me)
            {
                UpdateXpBar(elapsedTime);
            }

            if (MyEntity.GetEntityType() == EntityTypes.Player && MyEntity != Globals.Me)
            {
                if (MyEntity.Vital[(int)Vitals.Health] <= 0 || !MyEntity.IsAllyOf(Globals.Me))
                {
                    TradeLabel.Hide();
                    PartyLabel.Hide();
                    FriendLabel.Hide();
                    GuildLabel.Hide();
                }
                else if (MyEntity.IsAllyOf(Globals.Me))
                {
                    TradeLabel.Show();
                    TryShowPartyButton();
                    TryShowFriendButton();
                    TryShowGuildButton();
                }
            }

            if (UpdateStatuses)
            {
                UpdateSpellStatus();
                UpdateStatuses = false;
            }

            foreach (var itm in mActiveStatuses)
            {
                itm.Value.Update();
            }
            RefreshAccuracy();

            mLastUpdateTime = Timing.Global.Milliseconds;
        }

        private void ShowAccuracy()
        {
            ChanceToHitLabel.Show();
            ChanceToHitIcon.Show();
        }

        private void HideAccuracy()
        {
            ChanceToHitLabel.Hide();
            ChanceToHitIcon.Hide();
        }

        private void RefreshAccuracy()
        {
            var show = false;

            if (BestiaryController.MyBestiary.HasUnlock(MyEntity.NpcId, BestiaryUnlock.Stats) && MyEntity.GetEntityType() != EntityTypes.Player)
            {
                show = true;
            }

            if (MyEntity.GetEntityType() == EntityTypes.Player && !TradeLabel.IsVisible)
            {
                show = true;
            }

            if (MyEntity.GetEntityType() == EntityTypes.Event)
            {
                show = false;
            }

            if (show)
            {
                ShowAccuracy();
                var missChance = CombatUtilities.MissChance(CombatUtilities.CalculateMissFactor(Globals.Me.Stat[(int)Stats.Accuracy], MyEntity.Stat[(int)Stats.Evasion]));
                var hitChance = MathHelper.Clamp(100 - (missChance * 100), 0, 100);
                ChanceToHitLabel.SetText($"{hitChance.ToString("N1")}%");
            }
            else
            {
                HideAccuracy();
            }
        }

        private void UpdateThreatLevel()
        {
            if (MyEntity.NpcId != default && BestiaryController.CachedBeasts.TryGetValue(MyEntity.NpcId, out var npc))
            {
                ThreatLevelContainer.Show();
                var playerMelee = new List<AttackTypes> { AttackTypes.Blunt };
                
                if (Globals.Me.TryGetEquippedWeaponDescriptor(out var weapon))
                {
                    playerMelee = weapon.AttackTypes;
                }

                var threatLevel = ThreatLevel.Trivial;

                var npcDamageScalar = 100;
                if (npc.IsSpellcaster)
                {
                    Globals.CachedNpcSpellScalar.TryGetValue(npc.Id, out npcDamageScalar);
                }

                threatLevel = ThreatLevelUtilities.DetermineNpcThreatLevel(Globals.Me.MaxVital,
                       Globals.Me.Stat,
                       npc.MaxVital,
                       npc.Stats,
                       playerMelee,
                       npc.AttackTypes,
                       Globals.Me.AttackSpeed(),
                       npc.AttackSpeedValue,
                       Globals.Me.TryGetEquippedWeaponDescriptor(out weapon) ? weapon.ProjectileId != Guid.Empty : false,
                       npc.IsSpellcaster,
                       npcDamageScalar);

                ThreatLevelText.SetText(threatLevel.GetDescription());
                if (!ThreatLevelUtilities.ColorMapping.TryGetValue(threatLevel, out var color))
                {
                    color = Color.White; // color not found
                }

                ThreatLevelText.SetTextColor(color, Framework.Gwen.Control.Label.ControlState.Normal);
            }
            else
            {
                ThreatLevelContainer.Hide();
            }
        }

        public void UpdateSpellStatus()
        {
            if (MyEntity == null)
            {
                return;
            }

            if (!PlayerBox)
            {
                EntityStatusPanel.Y = MyEntity is Player && MyEntity.Id != Globals.Me?.Id ? 172 : 160;
            }

            //Remove 'Dead' Statuses
            var statuses = mActiveStatuses.Keys.ToArray();
            foreach (var status in statuses)
            {
                if (!MyEntity.StatusActive(status))
                {
                    var s = mActiveStatuses[status];
                    s.Pnl.Texture = null;
                    s.Container.Hide();
                    s.Container.Texture = null;
                    EntityStatusPanel.RemoveChild(s.Container, true);
                    s.pnl_HoverLeave(null, null);
                    mActiveStatuses.Remove(status);
                }
                else
                {
                    mActiveStatuses[status].UpdateStatus(MyEntity.GetStatus(status));
                }
            }

            //Add all of the spell status effects
            for (var i = 0; i < MyEntity.Status.Count; i++)
            {
                var id = MyEntity.Status[i].SpellId;
                SpellStatus itm = null;
                if (!mActiveStatuses.ContainsKey(id))
                {
                    itm = new SpellStatus(this, MyEntity.Status[i]);
                    if (PlayerBox)
                    {
                        itm.Container = new ImagePanel(EntityStatusPanel, "PlayerStatusIcon");
                    }
                    else
                    {
                        itm.Container = new ImagePanel(EntityStatusPanel, "TargetStatusIcon");
                    }

                    itm.Setup();

                    itm.Container.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
                    itm.Container.Name = "";
                    mActiveStatuses.Add(id, itm);
                }
                else
                {
                    itm = mActiveStatuses[id];
                }

                var xPadding = itm.Container.Margin.Left + itm.Container.Margin.Right;
                var yPadding = itm.Container.Margin.Top + itm.Container.Margin.Bottom;

                itm.Container.SetPosition(
                    i %
                    (EntityStatusPanel.Width /
                     Math.Max(1, EntityStatusPanel.Width / (itm.Container.Width + xPadding))) *
                    (itm.Container.Width + xPadding) +
                    xPadding,
                    i /
                    Math.Max(1, EntityStatusPanel.Width / (itm.Container.Width + xPadding)) *
                    (itm.Container.Height + yPadding) +
                    yPadding
                );
            }
        }

        private void UpdateLevel()
        {
            var levelString = Strings.EntityBox.level.ToString(MyEntity.Level);
            if (!EntityLevel.IsHidden)
            {
                EntityLevel.Text = levelString;
            }

            if (!EntityNameAndLevel.IsHidden)
            {
                EntityNameAndLevel.Text = Strings.EntityBox.NameAndLevel.ToString(MyEntity.Name.ToUpper(), levelString);
            }
        }

        private void UpdateMap()
        {
            if (Globals.Me.MapInstance != null)
            {
                EntityMap.SetText(Strings.EntityBox.map.ToString(Globals.Me.MapInstance.Name));
                updateMapLabelColor();
            }
            else
            {
                EntityMap.SetText(Strings.EntityBox.map.ToString(""));
            }
        }

        private void updateMapLabelColor()
        {
            if (Globals.Me.MapInstance.ZoneType == MapZones.Normal)
            {
                EntityMap.SetTextColor(Color.Red, Framework.Gwen.Control.Label.ControlState.Normal);
            }
            else if (Globals.Me.MapInstance.ZoneType == MapZones.Arena)
            {
                EntityMap.SetTextColor(Color.Yellow, Framework.Gwen.Control.Label.ControlState.Normal);
            }
            else
            {
                EntityMap.SetTextColor(Color.Gray, Framework.Gwen.Control.Label.ControlState.Normal);
            }
        }

        private void UpdateHpBar(float elapsedTime, bool instant = false)
        {
            if (EntityInfoPanel.IsHidden)
            {
                return;
            }

            var targetHpWidth = 0f;
            var targetShieldWidth = 0f;
            var maxWidth = HpBar?.Texture?.GetWidth() ?? 0;
            if (MyEntity.MaxVital[(int) Vitals.Health] > 0)
            {
                var maxVital = MyEntity.MaxVital[(int) Vitals.Health];
                var shieldSize = MyEntity.GetShieldSize();

                if (shieldSize + MyEntity.Vital[(int)Vitals.Health] > maxVital)
                {
                    maxVital = shieldSize + MyEntity.Vital[(int)Vitals.Health];
                }

                var hpfillRatio = (float) MyEntity.Vital[(int) Vitals.Health] / maxVital;
                hpfillRatio = (float) MathHelper.Clamp(hpfillRatio, 0f, 1f);
                targetHpWidth = MathHelper.RoundNearestMultiple((int) (hpfillRatio * maxWidth), 4);

                var shieldfillRatio = (float) shieldSize / maxVital;
                shieldfillRatio = (float) MathHelper.Clamp(shieldfillRatio, 0f, 1f);
                targetShieldWidth = MathHelper.RoundNearestMultiple((int)(shieldfillRatio * maxWidth), 4);

                //Fix the Labels
                HpLbl.Text = Strings.EntityBox.vital0val.ToString(
                    MyEntity.Vital[(int) Vitals.Health], MyEntity.MaxVital[(int) Vitals.Health]
                );
            }
            else
            {
                HpLbl.Text = Strings.EntityBox.vital0val.ToString(0, 0);
                targetHpWidth = 0;
            }
            
            HpBar.SetTextureRect(0, 0, (int)targetHpWidth, HpBar.Height);
            HpBar.Width = (int)targetHpWidth;
            HpBar.IsHidden = targetHpWidth == 0 || !BestiaryController.MyBestiary.HasUnlock(MyEntity.NpcId, BestiaryUnlock.HP);
            HpBackground.IsHidden = !BestiaryController.MyBestiary.HasUnlock(MyEntity.NpcId, BestiaryUnlock.HP);
            HpLbl.IsHidden = !BestiaryController.MyBestiary.HasUnlock(MyEntity.NpcId, BestiaryUnlock.HP);
            HpTitle.IsHidden = !BestiaryController.MyBestiary.HasUnlock(MyEntity.NpcId, BestiaryUnlock.HP);

            ShieldBar.IsHidden = (int)targetShieldWidth == 0 || HpBackground.IsHidden;
            ShieldBar.Width = (int)targetShieldWidth;
            ShieldBar.SetBounds(targetHpWidth + HpBar.X, HpBar.Y, targetShieldWidth, ShieldBar.Height);
            ShieldBar.SetTextureRect(
                (int)(HpBar.Width - targetShieldWidth), 0, (int)targetShieldWidth, ShieldBar.Height
            );
        }

        private void UpdateMpBar(float elapsedTime, bool instant = false)
        {
            if (EntityInfoPanel.IsHidden)
            {
                return;
            }

            if (MyEntity.MaxVital[(int)Vitals.Mana] <= 0 || !BestiaryController.MyBestiary.HasUnlock(MyEntity.NpcId, GameObjects.Events.BestiaryUnlock.MP))
            {
                MpBar.Hide();
                MpBackground.Hide();
                MpTitle.Hide();
                MpLbl.Hide();
                return;
            }

            var targetMpWidth = 0f;
            var width = MpBar?.Texture?.GetWidth() ?? 0;

            var mpRatio = MyEntity.Vital[(int)Vitals.Mana] / (float)MyEntity.MaxVital[(int)Vitals.Mana];
            mpRatio = (float)MathHelper.Clamp(mpRatio, 0f, 1f);
            MpLbl.Text = Strings.EntityBox.vital1val.ToString(
                MyEntity.Vital[(int)Vitals.Mana], MyEntity.MaxVital[(int)Vitals.Mana]
            );

            targetMpWidth = MathHelper.RoundNearestMultiple((int)(mpRatio * width), 4);

            MpBar.Width = (int)targetMpWidth;
            MpBar.SetTextureRect(0, 0, (int)targetMpWidth, MpBar.Height);
        }

        private void UpdateXpBar(float elapsedTime, bool instant = false)
        {
            if (EntityInfoPanel.IsHidden)
            {
                return;
            }

            float targetExpWidth = 1;
            if (((Player) MyEntity).GetNextLevelExperience() >= 0 && MyEntity.Level != Options.MaxLevel)
            {
                targetExpWidth = (float) ((Player) MyEntity).Experience /
                                 (float) ((Player) MyEntity).GetNextLevelExperience();

                ExpLbl.Text = Strings.EntityBox.expval.ToString(
                    ((Player) MyEntity)?.Experience, ((Player) MyEntity)?.GetNextLevelExperience()
                );
            }
            else if (MyEntity.Level == Options.MaxLevel)
            {
                targetExpWidth = 1f;
                ExpLbl.Text = Strings.EntityBox.maxlevel;
            }

            targetExpWidth *= ExpBackground.Width;
            if (Math.Abs((int) targetExpWidth - CurExpWidth) < 0.01)
            {
                return;
            }

            if (!instant)
            {
                if ((int)targetExpWidth > CurExpWidth)
                {
                    CurExpWidth += 100f * elapsedTime;
                    if (CurExpWidth > (int)targetExpWidth)
                    {
                        CurExpWidth = targetExpWidth;
                    }
                }
                else
                {
                    CurExpWidth -= 100f * elapsedTime;
                    if (CurExpWidth < targetExpWidth)
                    {
                        CurExpWidth = targetExpWidth;
                    }
                }
            }
            else
            {
                CurExpWidth = (int)targetExpWidth;
            }

            if (CurExpWidth == 0)
            {
                ExpBar.IsHidden = true;
            }
            else
            {
                ExpBar.Width = (int) CurExpWidth;
                ExpBar.SetTextureRect(0, 0, (int) CurExpWidth, ExpBar.Height);
                ExpBar.IsHidden = false;
            }
        }

        private void UpdateImage()
        {
            if (EntityInfoPanel.IsHidden)
            {
                return;
            }

            var sprite = EntityType == EntityTypes.Event ? MyEntity?.Face ?? string.Empty : MyEntity?.MySprite ?? string.Empty;

            var entityTex = EntityType == EntityTypes.Event ? 
                Globals.ContentManager.GetTexture(GameContentManager.TextureType.Face, sprite) : 
                MyEntity.Texture;

            if (entityTex != null)
            {   
                if (entityTex != EntityFace.Texture)
                {
                    EntityFace.Texture = entityTex;
                    EntityFace.RenderColor = MyEntity.Color ?? new Color(255, 255, 255, 255);
                    if (EntityType != EntityTypes.Event)
                    {
                        EntityFace.SetTextureRect(0, 0, entityTex.GetWidth() / Options.Instance.Sprites.NormalFrames, entityTex.GetHeight() / Options.Instance.Sprites.Directions);
                    }
                    else
                    {
                        EntityFace.SetTextureRect(0, 0, entityTex.GetWidth(), entityTex.GetHeight());
                    }
                    EntityFace.SizeToContentsScaled();
                    Align.Center(EntityFace);
                    mCurrentSprite = sprite;
                    EntityFace.IsHidden = false;
                }

                var equipment = MyEntity.Equipment;
                var decor = MyEntity.MyDecors;

                bool inVehicle = false;
                if (MyEntity is Player player)
                {
                    inVehicle = player.InVehicle;
                }

                if (MyEntity == Globals.Me)
                {
                    for (var i = 0; i < MyEntity.MyEquipment.Length; i++)
                    {
                        var eqp = MyEntity.MyEquipment[i];
                        if (eqp > -1 && eqp < Options.MaxInvItems)
                        {
                            equipment[i] = MyEntity.Inventory[eqp].ItemId;
                        }
                        else
                        {
                            equipment[i] = Guid.Empty;
                        }
                    }

                    for (var i = 0; i < MyEntity.MyDecors.Length; i++)
                    {
                        decor[i] = MyEntity.MyDecors[i];
                    }
                }

                // Determine decors that need hidden
                var hideHair = false;
                var hideBeard = false;
                var hideExtra = false;
                var shortHair = false;
                if (MyEntity is Player cosmeticOvr && cosmeticOvr.Cosmetics[Options.HelmetIndex] != Guid.Empty)
                {
                    var helmet = ItemBase.Get(cosmeticOvr.Cosmetics[Options.HelmetIndex]);
                    if (helmet != null)
                    {
                        hideHair = helmet.HideHair;
                        hideBeard = helmet.HideBeard;
                        hideExtra = helmet.HideExtra;
                        shortHair = helmet.ShortHair;
                    }
                }
                else if (equipment[Options.HelmetIndex] != Guid.Empty)
                {
                    var helmet = ItemBase.Get(equipment[Options.HelmetIndex]);
                    if (helmet != null)
                    {
                        hideHair = helmet.HideHair;
                        hideBeard = helmet.HideBeard;
                        hideExtra = helmet.HideExtra;
                        shortHair = helmet.ShortHair;
                    }
                }

                var n = 0;
                for (var z = 0; z < Options.PaperdollOrder[(int)Directions.Down].Count; z++)
                {
                    var paperdoll = "";
                    var paperdollSlot = Options.PaperdollOrder[(int)Directions.Down][z];
                    var textureType = GameContentManager.TextureType.Paperdoll;

                    var equipSlot = Options.EquipmentSlots.IndexOf(paperdollSlot);

                    if (equipSlot > -1 &&
                        equipment.Length == Options.EquipmentSlots.Count && !inVehicle)
                    {
                        if (equipment[equipSlot] != Guid.Empty)
                        {
                            var itemId = equipment[equipSlot];

                            // Cosmetic override
                            if (MyEntity is Player pl && pl.Cosmetics.ElementAtOrDefault(equipSlot) != default)
                            {
                                itemId = pl.Cosmetics[equipSlot];
                            }
                            if (ItemBase.Get(itemId) != null)
                            {
                                var itemdata = ItemBase.Get(itemId);
                                if (MyEntity.Gender == 0)
                                {
                                    paperdoll = itemdata.MalePaperdoll;
                                }
                                else
                                {
                                    paperdoll = itemdata.FemalePaperdoll;
                                }
                            }
                        }
                        // Is there a cosmetic though?
                        else if (MyEntity is Player ply && ply.Cosmetics.ElementAtOrDefault(equipSlot) != default)
                        {
                            var itemId = ply.Cosmetics[equipSlot];
                            if (ItemBase.Get(itemId) != null)
                            {
                                var itemdata = ItemBase.Get(itemId);
                                if (MyEntity.Gender == 0)
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

                    if (Options.DecorSlots.IndexOf(paperdollSlot) > -1 && !inVehicle)
                    {
                        var slotToDraw = Options.DecorSlots.IndexOf(paperdollSlot);

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
                                && Options.Instance.PlayerOpts.ShortHairMappings.TryGetValue(decor[slotToDraw] ?? string.Empty, out var hairText))
                            {
                                paperdoll = hairText;
                            }
                            else
                            {
                                paperdoll = decor[slotToDraw];
                            }
                        }
                        textureType = GameContentManager.TextureType.Decor;
                    }

                    //Check for Player layer
                    if (Options.PaperdollOrder[1][z] == "Player")
                    {
                        continue;
                    }

                    if (paperdoll == "" && PaperdollTextures[n] != "")
                    {
                        PaperdollPanels[n].Texture = null;
                        PaperdollPanels[n].Hide();
                        PaperdollTextures[n] = "";
                    }
                    else if (paperdoll != "" && paperdoll != PaperdollTextures[n])
                    {
                        var paperdollTex = Globals.ContentManager.GetTexture(textureType, paperdoll);

                        PaperdollPanels[n].Texture = paperdollTex;
                        if (paperdollTex != null)
                        {
                            PaperdollPanels[n]
                                .SetTextureRect(
                                    0, 0, PaperdollPanels[n].Texture.GetWidth() / Options.Instance.Sprites.NormalFrames,
                                    PaperdollPanels[n].Texture.GetHeight() / Options.Instance.Sprites.Directions
                                );

                            PaperdollPanels[n]
                                .SetSize(
                                    PaperdollPanels[n].Texture.ScaledWidth / Options.Instance.Sprites.NormalFrames,
                                    PaperdollPanels[n].Texture.ScaledHeight / Options.Instance.Sprites.Directions
                                );

                            PaperdollPanels[n]
                                .SetPosition(
                                    EntityFaceContainer.Width / 2 - PaperdollPanels[n].Width / 2,
                                    EntityFaceContainer.Height / 2 - PaperdollPanels[n].Height / 2
                                );
                        }

                        PaperdollPanels[n].Show();
                        PaperdollTextures[n] = paperdoll;
                    }

                    //Check for Player layer
                    if (Options.PaperdollOrder[1][z] != "Player")
                    {
                        n++;
                    }
                }
            }
            else if (MyEntity.MySprite != mCurrentSprite && MyEntity.Face != mCurrentSprite)
            {
                EntityFace.IsHidden = true;
                for (var i = 0; i < Options.EquipmentSlots.Count; i++)
                {
                    PaperdollPanels[i]?.Hide();
                }
            }

            if (EntityFace.RenderColor != MyEntity.Color)
            {
                EntityFace.RenderColor = MyEntity.Color;
            }
        }

        public void Dispose()
        {
            EntityWindow.Hide();
            Interface.GameUi.GameCanvas.RemoveChild(EntityWindow, false);
            EntityWindow.Dispose();
        }

        //Input Handlers
        void invite_Clicked(Base sender, ClickedEventArgs arguments)
        {
            if (Globals.Me.TargetIndex != Guid.Empty && Globals.Me.TargetIndex != Globals.Me.Id)
            {
                if (Globals.Me.CombatTimer < Timing.Global.Milliseconds)
                {
                    PacketSender.SendPartyInvite(Globals.Me.TargetIndex);
                }
                else
                {
                    PacketSender.SendChatMsg(Strings.Parties.infight.ToString(), 4);
                }
            }
        }

        //Input Handlers
        void tradeRequest_Clicked(Base sender, ClickedEventArgs arguments)
        {
            if (Globals.Me.TargetIndex != Guid.Empty && Globals.Me.TargetIndex != Globals.Me.Id)
            {
                if (Globals.Me.CombatTimer < Timing.Global.Milliseconds)
                {
                    PacketSender.SendTradeRequest(Globals.Me.TargetIndex);
                }
                else
                {
                    PacketSender.SendChatMsg(Strings.Trading.infight.ToString(), 4);
                }
            }
        }

        //Input Handlers
        void friendRequest_Clicked(Base sender, ClickedEventArgs arguments)
        {
            if (Globals.Me.TargetIndex != Guid.Empty && Globals.Me.TargetIndex != Globals.Me.Id)
            {
                if (Globals.Me.CombatTimer < Timing.Global.Milliseconds)
                {
                    PacketSender.SendAddFriend(MyEntity.Name);
                }
                else
                {
                    PacketSender.SendChatMsg(Strings.Friends.infight.ToString(), 4);
                }
            }
        }


        void guildRequest_Clicked(Base sender, ClickedEventArgs arguments)
        {
            if (MyEntity is Entities.Player plyr && MyEntity != Globals.Me)
            {
                if (string.IsNullOrWhiteSpace(plyr.Guild))
                {
                    if (Globals.Me?.GuildRank?.Permissions?.Invite ?? false)
                    {
                        if (Globals.Me.CombatTimer < Timing.Global.Milliseconds)
                        {
                            PacketSender.SendInviteGuild(MyEntity.Name);
                        }
                        else
                        {
                            PacketSender.SendChatMsg(Strings.Friends.infight.ToString(), 4);
                        }
                    }
                }
                else
                {
                    Chat.ChatboxMsg.AddMessage(new Chat.ChatboxMsg(Strings.Guilds.InviteAlreadyInGuild, CustomColors.Alerts.Declined, ChatMessageType.Guild));
                }
            }
        }

        void TryShowGuildButton()
        {
            var show = false;
            if (MyEntity is Entities.Player plyr && MyEntity != Globals.Me && string.IsNullOrWhiteSpace(plyr.Guild))
            {
                if (Globals.Me?.GuildRank?.Permissions?.Invite ?? false)
                {
                    show = true;
                }
            }

            GuildLabel.IsHidden = !show;
        }


        public void Hide()
        {
            EntityWindow.Hide();
        }

        public void Show()
        {
            EntityWindow.Show();
            RefreshEntity();
        }

    }

}
