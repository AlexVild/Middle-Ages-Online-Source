using Intersect.Client.Core;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.General.Bestiary;
using Intersect.Client.Interface.Components;
using Intersect.Client.Interface.Game.Components;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Client.Utilities;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using Intersect.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Interface.Game.BestiaryUi
{
    public class BestiaryWindow
    {
        private readonly Color BeastCompletionHoverColor = new Color(255, 105, 158, 252);
        private readonly Color BeastCompletionColor = new Color(255, 30, 74, 157);
        private readonly Color BeastListTextColor = new Color(255, 50, 19, 0);
        private readonly Color BeastListTextHoverColor = new Color(255, 111, 63, 0);
        private readonly Color BeastListLockedTextColor = new Color(255, 100, 100, 100);
        private readonly string UnknownString = Strings.Bestiary.Unknown.ToString();
        private const int ComponentPadding = 16;
        
        private Canvas GameCanvas;
        private WindowControl Window { get; set; }
        
        private ImagePanel ListContainer { get; set; }
        private ImagePanel SearchContainer { get; set; }
        private Label SearchLabel { get; set; }
        private ImagePanel SearchBg { get; set; }
        private TextBox SearchBar { get; set; }
        private string SearchTerm
        {
            get => SearchBar.Text;
            set => SearchBar.SetText(value);
        }
        private Button SearchClearButton { get; set; }
        private ListBox BeastList { get; set; }
        private LabeledCheckBox HideCheckbox { get; set; }
        private Label CompletionRateLabel { get; set; }

        private ScrollControl BeastContainer { get; set; }
        private ImagePanel BeastInfo { get; set; }
        private ImagePanel BeastInfoBelowImage { get; set; }

        private Label NoBeastFoundTemplate { get; set; }
        private RichLabel NoBeastFoundLabel { get; set; }
        
        private Label NameLabel { get; set; }
        private Label KillCountLabel { get; set; }
        private ImagePanel BeastImage { get; set; }
        private ImagePanel DescriptionBg { get; set; }
        private ScrollControl DescriptionContainer { get; set; }
        private Label DescriptionTemplate { get; set; }
        private RichLabel Description { get; set; }

        private ImagePanel VitalsContainer { get; set; }
        private BestiaryVitalComponent HealthComponent { get; set; }
        private BestiaryVitalComponent ManaComponent { get; set; }

        private BestiaryStatsComponent StatsComponent { get; set; }

        private BestiaryMagicComponent MagicComponent { get; set; }
        
        private BestiarySpellCombatComponent SpellCombatComponent { get; set; }
        
        private BestiaryLootComponent LootComponent { get; set; }

        public int X => Window.X;
        public int Y => Window.Y;
        public int Width => Window.Width;
        public int Height => Window.Height;
        public bool IsOpen => Window.IsVisible;
        private bool HideUnknown;

        private int BeastLabelWidth => BeastContainer.Width - BeastContainer.Padding.Left - BeastContainer.Padding.Bottom - BeastContainer.GetVerticalScrollBar().Width;

        private int SpriteFrame = 0;
        private long NextSpriteUpdate = 0;

        private ComponentList<GwenComponent> UnlockableComponents = new ComponentList<GwenComponent>();

        public BestiaryWindow(Canvas gameCanvas)
        {
            GameCanvas = gameCanvas;

            Window = new WindowControl(GameCanvas, "BESTIARY", false, "BestiaryWindow", Hide);

            ListContainer = new ImagePanel(Window, "ListContainer");

            SearchContainer = new ImagePanel(ListContainer, "SearchContainer");
            SearchLabel = new Label(SearchContainer, "SearchLabel")
            {
                Text = "Search:"
            };
            SearchBg = new ImagePanel(SearchContainer, "SearchBg");
            SearchBar = new TextBox(SearchBg, "SearchBar");
            SearchBar.TextChanged += SearchBar_TextChanged; ;
            SearchClearButton = new Button(SearchContainer, "ClearButton")
            {
                Text = "CLEAR"
            };
            SearchClearButton.Clicked += SearchClearButton_Clicked;

            BeastList = new ListBox(ListContainer, "BeastList");

            HideCheckbox = new LabeledCheckBox(ListContainer, "HideCheckbox");
            HideCheckbox.CheckChanged += HideCheckbox_CheckChanged;
            HideCheckbox.Text = "Show/Hide \"???\"";
            CompletionRateLabel = new Label(ListContainer, "CompletionRateLabel");

            BeastContainer = new ScrollControl(Window, "BeastContainer");
            NoBeastFoundTemplate = new Label(BeastContainer, "BeastLockedLabel");
            NoBeastFoundLabel = new RichLabel(BeastContainer);

            BeastInfo = new ImagePanel(BeastContainer, "BeastInfo");

            NameLabel = new Label(BeastInfo, "BeastName");
            KillCountLabel = new Label(BeastInfo, "KillCount");
            BeastImage = new ImagePanel(BeastInfo, "BeastImage");

            BeastInfoBelowImage = new ImagePanel(BeastInfo, "BeastInfoContinued");

            DescriptionBg = new ImagePanel(BeastInfoBelowImage, "DescriptionBg");
            DescriptionContainer = new ScrollControl(DescriptionBg, "DescriptionContainer");
            DescriptionTemplate = new Label(DescriptionContainer, "Description");
            Description = new RichLabel(DescriptionContainer);

            VitalsContainer = new ImagePanel(BeastInfoBelowImage, "VitalsContainer");
            HealthComponent = new BestiaryVitalComponent(VitalsContainer, "Health", "character_stats_health.png", "HEALTH", "MAX", "The enemy's total hit points.", UnlockableComponents);
            ManaComponent = new BestiaryVitalComponent(VitalsContainer, "Mana", "character_stats_mana.png", "MANA", "MAX", "The enemy's total mana pool.", UnlockableComponents);

            StatsComponent = new BestiaryStatsComponent(BeastInfoBelowImage, "StatsContainer", UnlockableComponents);
            
            MagicComponent = new BestiaryMagicComponent(BeastInfoBelowImage, "MagicContainer", UnlockableComponents);
            
            SpellCombatComponent = new BestiarySpellCombatComponent(BeastInfoBelowImage, "SpellCombatContainer", UnlockableComponents);
            
            LootComponent = new BestiaryLootComponent(BeastInfoBelowImage, "LootContainer", UnlockableComponents);

            Window.LoadJsonUi(Framework.File_Management.GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

            BeastInfoBelowImage.SetPosition(0, BeastImage.Y);
            BeastInfoBelowImage.SetSize(BeastContainer.GetContentWidth(true), 1);
            BeastInfoBelowImage.SizeToChildren(false, true);

            HealthComponent.Initialize();
            ManaComponent.Initialize();

            StatsComponent.Initialize();

            MagicComponent.Initialize();

            SpellCombatComponent.Initialize();

            LootComponent.Initialize();

            BeastInfo.SetPosition(BeastContainer.X, BeastContainer.Y);
            BeastInfo.SetSize(BeastContainer.GetContentWidth(true), 1);
            BeastInfo.SizeToChildren(false, true);

            NoBeastFoundLabel.SetText(Strings.Bestiary.BeastNotFound, NoBeastFoundTemplate, BeastLabelWidth);

            NoBeastFoundLabel.Hide();
            BeastInfo.Hide();
        }

        private void HideCheckbox_CheckChanged(Base sender, EventArgs arguments)
        {
            HideCheckbox.IsChecked = !HideUnknown;
            HideUnknown = HideCheckbox.IsChecked;
            LoadBeasts();
        }

        private void ShowBeastInfo()
        {
            BeastInfo.SetPosition(0, 0);
            BeastInfo.SetSize(BeastContainer.GetContentWidth(true), 1);

            BeastInfo.SizeToChildren(false, true);
            BeastInfo.ProcessAlignments();

            BeastInfo.Show();
        }

        public void Update(long timeMs)
        {
            if (!Window.IsVisible)
            {
                return;
            }

            if (BeastImage.IsVisible)
            {
                AnimateImage(timeMs);
            }

            LootComponent.Update();
        }

        private void LoadBeasts()
        {
            BeastList.ScrollToTop();
            BeastList.Clear();
            var amountCompleted = 0;
            foreach (var beast in BestiaryController.CachedBeasts.Values)
            {
                var complete = BeastComplete(beast);
                if (complete)
                {
                    amountCompleted++;
                }

                var nameUnlocked = NameUnlocked(beast.Id);
                var name = nameUnlocked ? beast.Name : UnknownString;

                if (!nameUnlocked && HideUnknown)
                {
                    continue;
                }

                if (!string.IsNullOrEmpty(SearchTerm) && !SearchHelper.IsSearchable(name, SearchTerm))
                {
                    continue;
                }

                var beastRow = BeastList.AddRow(name);
                if (beastRow == null)
                {
                    continue;
                }

                beastRow.UserData = beast.Id;
                beastRow.Clicked += BeastRow_Clicked;
                beastRow.SetTextColor(nameUnlocked ? BeastListTextColor : BeastListLockedTextColor);

                beastRow.RenderColor = new Color(100, 232, 208, 170);

                if (complete)
                {
                    beastRow.SetTextColor(BeastCompletionColor);
                }
            }

            var completionRate = ((float)amountCompleted / BestiaryController.CachedBeasts.Count) * 100;
            CompletionRateLabel.Text = $"{completionRate.ToString("N1")}% Complete";
            if (completionRate >= 100f)
            {
                CompletionRateLabel.SetTextColor(BeastCompletionColor, Label.ControlState.Normal);
                CompletionRateLabel.SetTextColor(BeastCompletionHoverColor, Label.ControlState.Hovered);
            }
            else
            {
                CompletionRateLabel.SetTextColor(BeastListTextColor, Label.ControlState.Normal);
                CompletionRateLabel.SetTextColor(BeastListTextHoverColor, Label.ControlState.Hovered);
            }
            CompletionRateLabel.SetToolTipText($"{amountCompleted} / {BestiaryController.CachedBeasts.Count} entries completed.");
        }

        private bool BeastComplete(NpcBase beast)
        {
            return beast.BestiaryUnlocks.Keys.All(unlock => BestiaryController.MyBestiary.HasUnlock(beast.Id, (BestiaryUnlock)unlock));
        }

        private void BeastRow_Clicked(Base sender, Framework.Gwen.Control.EventArguments.ClickedEventArgs arguments)
        {
            try
            {
                BeastContainer.Show();
                BeastContainer.ScrollToTop();
                var listBoxRow = (ListBoxRow)sender;
                var npcId = (Guid)listBoxRow.UserData;

                if (BestiaryController.KnownKillCounts.TryGetValue(npcId, out var playerKc))
                {
                    KillCountLabel.Show();
                    KillCountLabel.SetText($"Kill count: {playerKc.ToString("N0")}");
                }
                else
                {
                    KillCountLabel.Hide();
                }

                if (!BestiaryController.CachedBeasts.TryGetValue(npcId, out var beast))
                {
                    NoBeastFoundLabel.Show();
                    return;
                }
                NoBeastFoundLabel.Hide();

                var nameUnlocked = NameUnlocked(npcId);

                var name = nameUnlocked ? beast.Name : Strings.Bestiary.Unknown.ToString();

                NameLabel.SetText($"{name} - Tier {beast.Level}");

                ResetImage();

                var sprite = nameUnlocked ? beast.Sprite : "8bit_unknown.png";
                var spriteColor = nameUnlocked ? beast.Color : Color.White;
                DrawBeastImage(sprite, spriteColor);

                BeastInfoBelowImage.SetPosition(0, BeastImage.Bottom + ComponentPadding);

                var description = nameUnlocked ? beast.Description : Strings.Bestiary.BeastLocked.ToString();
                Description.SetText(description.Trim(), DescriptionTemplate, DescriptionContainer.Width - 32);

                // If the player hasn't unlocked _anything_ for this beast yet, hide everything else
                if (!BestiaryController.MyBestiary.Unlocks.TryGetValue(npcId, out var playerUnlocks)) 
                {
                    HideAllComponents();
                    ShowBeastInfo();
                    return;
                }
                if (playerUnlocks.Values.All(val => val == false))
                {
                    HideAllComponents();
                    ShowBeastInfo();
                    return;
                }

                ShowAllComponents();
                
                InitializeBeastVitals(beast);
                InitializeBeastStats(beast);
                InitializeBeastMagic(beast);
                InitializeBeastSpellCombat(beast);
                InitializeBeastLoot(beast);
                
                BeastInfoBelowImage.SizeToChildren(false, true);
                ShowBeastInfo();
                // Load the appropriate bestiary page
            } catch (InvalidCastException)
            {
                return;
            }
        }

        private void ShowAllComponents()
        {
            UnlockableComponents.ShowAll();
        }

        /// <summary>
        /// Hides components and resizes the beast info panel such that it only shows up to the descrption panel.
        /// </summary>
        private void HideAllComponents()
        {
            UnlockableComponents.HideAll();
            BeastInfoBelowImage.SetSize(BeastInfo.Width, 124);
        }

        private void InitializeBeastVitals(NpcBase beast)
        {
            // HP
            if (!beast.BestiaryUnlocks.TryGetValue((int)BestiaryUnlock.HP, out var reqKc))
            {
                reqKc = 0;
            }
            HealthComponent.SetValues(beast.MaxVital[(int)Vitals.Health], reqKc, HPUnlocked(beast.Id));

            // MP
            if (!beast.BestiaryUnlocks.TryGetValue((int)BestiaryUnlock.MP, out reqKc))
            {
                reqKc = 0;
            }
            ManaComponent.SetValues(beast.MaxVital[(int)Vitals.Mana], reqKc, MPUnlocked(beast.Id));
            
            // Don't bother showing if the enemy doesn't have mana
            if (beast.MaxVital[(int)Vitals.Mana] <= 0)
            {
                ManaComponent.Hide();
            }

            // Resize
            VitalsContainer.SetPosition(0, DescriptionBg.Bottom + ComponentPadding);
        }

        private void InitializeBeastStats(NpcBase beast)
        {
            if (!beast.BestiaryUnlocks.TryGetValue((int)BestiaryUnlock.Stats, out var reqKc))
            {
                reqKc = 0;
            }
            StatsComponent.SetUnlockStatus(StatsUnlocked(beast.Id));
            StatsComponent.SetBeast(beast, reqKc);
            StatsComponent.SetPosition(0, VitalsContainer.Bottom + ComponentPadding);
        }

        private void InitializeBeastMagic(NpcBase beast)
        {
            if (beast.Spells.Count <= 0)
            {
                MagicComponent.Hide();
                MagicComponent.SetPosition(0, 0);
                return;
            }
            MagicComponent.Show();
            
            if (!beast.BestiaryUnlocks.TryGetValue((int)BestiaryUnlock.Spells, out var reqKc))
            {
                reqKc = 0;
            }
            MagicComponent.SetUnlockStatus(SpellsUnlocked(beast.Id));
            MagicComponent.SetBeast(beast, reqKc);
            MagicComponent.SetPosition(0, StatsComponent.Bottom + ComponentPadding);
            MagicComponent.ProcessAlignments();
        }

        private void InitializeBeastSpellCombat(NpcBase beast)
        {
            if (beast.Spells.Count <= 0)
            {
                SpellCombatComponent.Hide();
                SpellCombatComponent.SetPosition(0, 0);
                return;
            }
            SpellCombatComponent.Show();

            if (!beast.BestiaryUnlocks.TryGetValue((int)BestiaryUnlock.SpellCombatInfo, out var reqKc))
            {
                reqKc = 0;
            }
            SpellCombatComponent.SetUnlockStatus(SpellCombatUnlocked(beast.Id));
            SpellCombatComponent.SetBeast(beast, reqKc);
            SpellCombatComponent.SetPosition(0, MagicComponent.Bottom + ComponentPadding);
            SpellCombatComponent.ProcessAlignments();
        }

        private void InitializeBeastLoot(NpcBase beast)
        {
            if (beast.Drops.Count == 0 && beast.SecondaryDrops.Count == 0 && beast.TertiaryDrops.Count == 0)
            {
                LootComponent.Hide();
                LootComponent.SetPosition(0, 0);
                return;
            }
            LootComponent.Show();

            if (!beast.BestiaryUnlocks.TryGetValue((int)BestiaryUnlock.Loot, out var reqKc))
            {
                reqKc = 0;
            }
            LootComponent.SetUnlockStatus(LootUnlocked(beast.Id));
            LootComponent.SetBeast(beast, reqKc);

            if (beast.Spells.Count > 0)
            {
                LootComponent.SetPosition(0, SpellCombatComponent.Bottom + ComponentPadding);
            }
            else
            {
                LootComponent.SetPosition(0, StatsComponent.Bottom + ComponentPadding);
            }
            LootComponent.ProcessAlignments();
        }

        private void AnimateImage(long timeMs)
        {
            if (timeMs < NextSpriteUpdate)
            {
                return;
            }

            NextSpriteUpdate = Timing.Global.Milliseconds + Options.Instance.Sprites.MovingFrameDuration;

            SpriteFrame++;
            if (SpriteFrame > Options.Instance.Sprites.NormalFrames - 1)
            {
                SpriteFrame = 0;
            }

            var texture = BeastImage.Texture;
            var animationWidth = texture.Width / 4;
            var animationHeight = texture.Height / 4;

            BeastImage.SetTextureRect(SpriteFrame * animationWidth, 0, animationWidth, animationHeight);
        }

        private void ResetImage()
        {
            SpriteFrame = 0;
            NextSpriteUpdate = Timing.Global.Milliseconds + Options.Instance.Sprites.MovingFrameDuration;
        }

        private void DrawBeastImage(string sprite, Color color)
        {
            if (string.IsNullOrEmpty(sprite) || sprite == Strings.General.none)
            {
                sprite = "8bit_unknown.png";
            }

            var beastImageTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Entity, sprite);
            BeastImage.Texture = beastImageTexture;
            var directions = Options.Instance.Sprites.NormalFrames;

            BeastImage.SetTextureRect(0, 0, beastImageTexture.Width / directions, beastImageTexture.Height / directions);
            BeastImage.SetSize(beastImageTexture.ScaledWidth / directions, beastImageTexture.ScaledHeight / directions);
            BeastImage.RenderColor = color;
            BeastImage.ProcessAlignments();
        }

        public void Show()
        {
            PacketSender.SendRequestKillCounts();
            SearchTerm = string.Empty;
            Interface.InputBlockingElements.Add(SearchBar);
            Window.Show();
            LoadBeasts();
        }

        public void Hide()
        {
            BeastList.Clear();
            BeastContainer.Hide();
            Interface.InputBlockingElements.Remove(SearchBar);
            Window.Hide();
        }

        public void SetPosition(float x, float y)
        {
            Window.SetPosition(x, y);
        }

        private void SearchBar_TextChanged(Base sender, EventArgs arguments)
        {
            LoadBeasts();
            // blank
        }

        private void SearchClearButton_Clicked(Base sender, Framework.Gwen.Control.EventArguments.ClickedEventArgs arguments)
        {
            SearchTerm = string.Empty;
        }

        #region Unlock helpers
        private bool NameUnlocked(Guid npcId)
        {
            return BestiaryController.MyBestiary.HasUnlock(npcId, BestiaryUnlock.NameAndDescription);
        }

        private bool HPUnlocked(Guid npcId)
        {
            return BestiaryController.MyBestiary.HasUnlock(npcId, BestiaryUnlock.HP);
        }

        private bool MPUnlocked(Guid npcId)
        {
            return BestiaryController.MyBestiary.HasUnlock(npcId, BestiaryUnlock.MP);
        }

        private bool LootUnlocked(Guid npcId)
        {
            return BestiaryController.MyBestiary.HasUnlock(npcId, BestiaryUnlock.Loot);
        }

        private bool SpellCombatUnlocked(Guid npcId)
        {
            return BestiaryController.MyBestiary.HasUnlock(npcId, BestiaryUnlock.SpellCombatInfo);
        }

        private bool SpellsUnlocked(Guid npcId)
        {
            return BestiaryController.MyBestiary.HasUnlock(npcId, BestiaryUnlock.Spells);
        }

        private bool StatsUnlocked(Guid npcId)
        {
            return BestiaryController.MyBestiary.HasUnlock(npcId, BestiaryUnlock.Stats);
        }
        #endregion
    }
}
