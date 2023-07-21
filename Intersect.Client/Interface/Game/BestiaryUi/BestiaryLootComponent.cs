using Intersect.Client.Framework.Graphics;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Interface.Components;
using Intersect.Client.Interface.Game.Components;
using Intersect.GameObjects;
using Intersect.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Interface.Game.BestiaryUi
{
    public class BestiaryLootComponent : BestiaryComponent
    {
        NpcBase Beast;

        private ImagePanel LootImage { get; set; }

        private ImagePanel LootBg { get; set; }

        private ScrollControl LootContainer { get; set; }

        List<BeastLootItem> LootItems { get; set; } = new List<BeastLootItem>();

        private Label8Bit ChampionLabel { get; set; }
        private Label8Bit ChampionInfo { get; set; }

        public BestiaryLootComponent(Base parent, string containerName, ComponentList<GwenComponent> referenceList = null) : base(parent, containerName, "BestiaryLootComponent", referenceList)
        {
        }

        public override void Initialize()
        {
            SelfContainer = new ImagePanel(ParentContainer, ComponentName);
            LootImage = new ImagePanel(SelfContainer, "LootImage");
            LootBg = new ImagePanel(SelfContainer, "LootBg");
            LootContainer = new ScrollControl(LootBg, "LootContainer");
            ChampionLabel = new Label8Bit(LootBg, 12, "ChampionLabel", interactive: true, tooltip: "A special version of this monster can spawn after being killed at a % chance");
            ChampionInfo = new Label8Bit(LootBg, 12, "ChampionInfoLabel");

            base.Initialize();
            FitParentToComponent();
        }

        public override void Dispose()
        {
            ClearLoot();
            base.Dispose();
        }

        private void ClearLoot()
        {
            LootContainer.ClearCreatedChildren();
            LootItems.Clear();
        }

        public void Update()
        {
            if (IsHidden)
            {
                return;
            }

            foreach (var lootItem in LootItems)
            {
                lootItem.Update();
            }
        }

        public void SetBeast(NpcBase beast, int requiredKc)
        {
            if (beast == null)
            {
                return;
            }

            LootItems.Clear();
            // setup
            Beast = beast;
            RequiredKillCount = requiredKc;
            LockLabel.SetText(RequirementString);
            ClearLoot();

            if (!Unlocked)
            {
                LootContainer.Hide();
                ChampionLabel.Hide();
                ChampionInfo.Hide();
                return;
            }

            LootContainer.Show();
            ChampionLabel.Show();
            ChampionInfo.Show();

            var idx = 0;
            var totalWeight = LootTableHelpers.GetTotalWeight(Beast.Drops, true);

            var primaryDrops = LootTableHelpers.UnfoldDrops(Beast.Drops);
            foreach (var drop in primaryDrops)
            {
                if (drop.ItemId == Guid.Empty)
                {
                    continue;
                }

                var chance = LootTableHelpers.GetPrettyChance(drop.Chance, totalWeight);

                var lootItem = new BeastLootItem(idx, LootContainer, drop.ItemId, chance, quantity: drop.Quantity);
                lootItem.Setup();

                var xPadding = lootItem.Pnl.Margin.Left + lootItem.Pnl.Margin.Right;
                var yPadding = lootItem.Pnl.Margin.Top + lootItem.Pnl.Margin.Bottom;

                lootItem.SetPosition(
                    idx %
                    (LootContainer.GetContentWidth() / (lootItem.Pnl.Width + xPadding)) *
                    (lootItem.Pnl.Width + xPadding) +
                    xPadding,
                    idx /
                    (LootContainer.GetContentWidth() / (lootItem.Pnl.Width + xPadding)) *
                    (lootItem.Pnl.Height + yPadding) +
                    yPadding
                );

                LootItems.Add(lootItem);

                idx++;
            }

            if (beast.SecondaryChance > 0)
            {
                // Secondary table
                totalWeight = LootTableHelpers.GetTotalWeight(Beast.SecondaryDrops, true);

                var secondaryDrops = LootTableHelpers.UnfoldDrops(Beast.SecondaryDrops);
                foreach (var drop in secondaryDrops)
                {
                    if (drop.ItemId == Guid.Empty)
                    {
                        continue;
                    }

                    var chance = LootTableHelpers.GetPrettyChance(drop.Chance, totalWeight);

                    var lootItem = new BeastLootItem(idx, LootContainer, drop.ItemId, chance, beast.SecondaryChance, drop.Quantity);
                    lootItem.Setup();

                    var xPadding = lootItem.Pnl.Margin.Left + lootItem.Pnl.Margin.Right;
                    var yPadding = lootItem.Pnl.Margin.Top + lootItem.Pnl.Margin.Bottom;

                    lootItem.SetPosition(
                        idx %
                        (LootContainer.GetContentWidth() / (lootItem.Pnl.Width + xPadding)) *
                        (lootItem.Pnl.Width + xPadding) +
                        xPadding,
                        idx /
                        (LootContainer.GetContentWidth() / (lootItem.Pnl.Width + xPadding)) *
                        (lootItem.Pnl.Height + yPadding) +
                        yPadding
                    );

                    LootItems.Add(lootItem);
                    idx++;
                }
            }

            if (beast.TertiaryChance > 0)
            {
                // Tertiary table
                totalWeight = LootTableHelpers.GetTotalWeight(Beast.TertiaryDrops, true);

                var tertiaryDrops = LootTableHelpers.UnfoldDrops(Beast.TertiaryDrops);
                foreach (var drop in tertiaryDrops)
                {
                    if (drop.ItemId == Guid.Empty)
                    {
                        continue;
                    }

                    var chance = LootTableHelpers.GetPrettyChance(drop.Chance, totalWeight);

                    var lootItem = new BeastLootItem(idx, LootContainer, drop.ItemId, chance, beast.TertiaryChance, drop.Quantity);
                    lootItem.Setup();

                    var xPadding = lootItem.Pnl.Margin.Left + lootItem.Pnl.Margin.Right;
                    var yPadding = lootItem.Pnl.Margin.Top + lootItem.Pnl.Margin.Bottom;

                    lootItem.SetPosition(
                        idx %
                        (LootContainer.GetContentWidth() / (lootItem.Pnl.Width + xPadding)) *
                        (lootItem.Pnl.Width + xPadding) +
                        xPadding,
                        idx /
                        (LootContainer.GetContentWidth() / (lootItem.Pnl.Width + xPadding)) *
                        (lootItem.Pnl.Height + yPadding) +
                        yPadding
                    );

                    LootItems.Add(lootItem);
                    idx++;
                }
            }

            var champion = NpcBase.Get(beast.ChampionId);
            if (champion == default)
            {
                ChampionLabel.SetText("This beast does not have a champion variant.");
                ChampionLabel.SetToolTipText("");
                ChampionInfo.Hide();
            }
            else
            {
                ChampionLabel.SetText($"CHAMPION: {champion.Name}");
                ChampionLabel.SetToolTipText("A special version of this monster can spawn after being killed at a % chance");
                ChampionInfo.Show();
                ChampionInfo.SetText($"{beast.ChampionSpawnChance.ToString("N1")}% chance, {beast.ChampionCooldownSeconds} second cooldown");
            }
            ChampionLabel.PlaceBelow(LootContainer);
            ChampionInfo.PlaceBelow(ChampionLabel);
        }

        public override void SetUnlockStatus(bool unlocked)
        {
            if (unlocked)
            {
                LootImage.RenderColor = Color.White;
            }
            else
            {
                LootImage.RenderColor = Color.Black;
            }

            base.SetUnlockStatus(unlocked);

            SelfContainer.Texture = null;
            if (Unlocked)
            {
                LootBg.Texture = UnlockedBg;
            }
            else
            {
                LootBg.Texture = LockedBg;
            }
        }

        public override GameTexture UnlockedBg => Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "bestiary_loot_bg.png");

        public override GameTexture LockedBg => Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "bestiary_loot_bg_locked.png");
    }
}
