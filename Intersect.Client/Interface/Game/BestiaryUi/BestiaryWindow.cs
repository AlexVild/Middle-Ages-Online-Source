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
        private readonly Color BeastListTextColor = new Color(255, 50, 19, 0);
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

        private ScrollControl BeastContainer { get; set; }
        private ImagePanel BeastInfo { get; set; }
        private ImagePanel BeastInfoBelowImage { get; set; }

        private Label NoBeastFoundTemplate { get; set; }
        private RichLabel NoBeastFoundLabel { get; set; }
        
        private Label NameLabel { get; set; }
        private Label KillCountLabel { get; set; }
        private ImagePanel BeastImage { get; set; }
        private ImagePanel DescriptionBg { get; set; }
        private ImagePanel DescriptionContainer { get; set; }
        private Label DescriptionTemplate { get; set; }
        private RichLabel Description { get; set; }

        private ImagePanel VitalsContainer { get; set; }
        private BestiaryVitalComponent HealthComponent { get; set; }
        private BestiaryVitalComponent ManaComponent { get; set; }

        private BestiaryStatsComponent StatsComponent { get; set; }

        private BestiaryMagicComponent MagicComponent { get; set; }

        public int X => Window.X;
        public int Y => Window.Y;
        public int Width => Window.Width;
        public int Height => Window.Height;
        public bool IsOpen => Window.IsVisible;

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

            BeastContainer = new ScrollControl(Window, "BeastContainer");
            NoBeastFoundTemplate = new Label(BeastContainer, "BeastLockedLabel");
            NoBeastFoundLabel = new RichLabel(BeastContainer);

            BeastInfo = new ImagePanel(BeastContainer, "BeastInfo");

            NameLabel = new Label(BeastInfo, "BeastName");
            KillCountLabel = new Label(BeastInfo, "KillCount");
            BeastImage = new ImagePanel(BeastInfo, "BeastImage");

            BeastInfoBelowImage = new ImagePanel(BeastInfo, "BeastInfoContinued");

            DescriptionBg = new ImagePanel(BeastInfoBelowImage, "DescriptionBg");
            DescriptionContainer = new ImagePanel(DescriptionBg, "DescriptionContainer");
            DescriptionTemplate = new Label(DescriptionContainer, "Description");
            Description = new RichLabel(DescriptionContainer);

            VitalsContainer = new ImagePanel(BeastInfoBelowImage, "VitalsContainer");
            HealthComponent = new BestiaryVitalComponent(VitalsContainer, "Health", "character_stats_health.png", "HEALTH", "MAX", "The enemy's total hit points.", UnlockableComponents);
            ManaComponent = new BestiaryVitalComponent(VitalsContainer, "Mana", "character_stats_mana.png", "MANA", "MAX", "The enemy's total mana pool.", UnlockableComponents);

            StatsComponent = new BestiaryStatsComponent(BeastInfoBelowImage, "StatsContainer", UnlockableComponents);
            
            MagicComponent = new BestiaryMagicComponent(BeastInfoBelowImage, "MagicContainer", UnlockableComponents);

            Window.LoadJsonUi(Framework.File_Management.GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

            BeastInfoBelowImage.SetPosition(0, BeastImage.Y);
            BeastInfoBelowImage.SetSize(BeastContainer.GetContentWidth(true), 1);
            BeastInfoBelowImage.SizeToChildren(false, true);

            HealthComponent.Initialize();
            ManaComponent.Initialize();

            StatsComponent.Initialize();

            MagicComponent.Initialize();

            BeastInfo.SetPosition(BeastContainer.X, BeastContainer.Y);
            BeastInfo.SetSize(BeastContainer.GetContentWidth(true), 1);
            BeastInfo.SizeToChildren(false, true);

            NoBeastFoundLabel.SetText(Strings.Bestiary.BeastNotFound, NoBeastFoundTemplate, BeastLabelWidth);

            NoBeastFoundLabel.Hide();
            BeastInfo.Hide();
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
        }

        private void LoadBeasts()
        {
            BeastList.ScrollToTop();
            BeastList.Clear();
            foreach (var beast in BestiaryController.CachedBeasts.Values)
            {
                var nameUnlocked = NameUnlocked(beast.Id);
                var name = nameUnlocked ? beast.Name : UnknownString;

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
            }
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
                    KillCountLabel.SetText($"Kill count: {playerKc}");
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

                NameLabel.SetText(name);

                ResetImage();

                var sprite = nameUnlocked ? beast.Sprite : "8bit_unknown.png";
                var spriteColor = nameUnlocked ? beast.Color : Color.White;
                DrawBeastImage(sprite, spriteColor);

                BeastInfoBelowImage.SetPosition(0, BeastImage.Bottom + ComponentPadding);

                var description = nameUnlocked ? beast.Description : Strings.Bestiary.BeastLocked.ToString();
                Description.SetText(description, DescriptionTemplate, DescriptionContainer.Width);

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

                BeastInfoBelowImage.SizeToChildren(false, true);
                BeastContainer.UpdateScrollBars();
                ShowBeastInfo();
                // Load the appropriate bestiary page
            } catch (InvalidCastException)
            {
                return;
            }
        }

        private void ShowAllComponents()
        {
            UnlockableComponents.ForEach(comp => comp.Show());
        }

        private void HideAllComponents()
        {
            UnlockableComponents.ForEach(comp => comp.Hide());
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
            var beastImageTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Entity, sprite);
            BeastImage.Texture = beastImageTexture;
            BeastImage.SetTextureRect(0, 0, beastImageTexture.Width / 4, beastImageTexture.Height / 4);
            BeastImage.SetSize(beastImageTexture.Width / 4, beastImageTexture.Height / 4);
            BeastImage.RenderColor = color;
            BeastImage.ProcessAlignments();
        }

        public void Show()
        {
            PacketSender.SendRequestKillCounts();
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
