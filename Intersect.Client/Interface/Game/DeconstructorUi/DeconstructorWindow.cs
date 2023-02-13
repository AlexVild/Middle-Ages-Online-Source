using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.General.Deconstructor;
using Intersect.Client.Networking;
using Intersect.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Interface.Game.DeconstructorUi
{
    public sealed class DeconstructorWindow : GameWindow
    {
        ImagePanel ItemsBg { get; set; }
        ScrollControl ItemsContainer { get; set; }

        ImagePanel FuelBg { get; set; }

        Label NoItemsLabelTemplate { get; set; }
        RichLabel NoItemsLabel { get; set; }
        Label FuelLabel { get; set; }
        Label RemainingFuelLabel { get; set; }
        Label RequiredFuelLabel { get; set; }

        Button CancelButton { get; set; }
        Button DeconstructButton { get; set; }
        Button AddFuelButton { get; set; }

        protected override string FileName => "DeconstructorWindow";
        protected override string Title => "DECONSTRUCTION";

        Deconstructor Deconstructor => Globals.Me?.Deconstructor;

        public DeconstructorWindow(Base gameCanvas) : base(gameCanvas) { }

        protected override void PreInitialization()
        {
            ItemsBg = new ImagePanel(Background, "ItemsBg");
            ItemsContainer = new ScrollControl(ItemsBg, "ItemsContainer");

            NoItemsLabelTemplate = new Label(ItemsBg, "NoItemsLabel");
            NoItemsLabel = new RichLabel(ItemsBg);

            FuelBg = new ImagePanel(Background, "FuelBg");

            CancelButton = new Button(ItemsBg, "CancelButton")
            {
                Text = "CANCEL"
            };
            CancelButton.Clicked += CancelButton_Clicked;

            DeconstructButton = new Button(ItemsBg, "DeconstructButton")
            {
                Text = "DECONSTRUCT"
            };

            AddFuelButton = new Button(FuelBg, "AddFuelButton")
            {
                Text = "ADD FUEL"
            };
        }
        protected override void PostInitialization()
        {
            NoItemsLabel.SetText("Right-click on equipment in your inventory to add to the deconstructor. You will need the appropriate amount of fuel to deconstruct items.",
                NoItemsLabelTemplate, ItemsContainer.Width - 64);
            NoItemsLabel.ProcessAlignments();
        }

        public override void UpdateShown()
        {
            if (Deconstructor == default || !Deconstructor.Refresh)
            {
                return;
            }

            ClearItems();
            var itemIndices = Deconstructor.Items.ToArray();

            if (itemIndices.Length <= 0)
            {
                NoItemsLabel.Show();
                return;
            }
            else
            {
                NoItemsLabel.Hide();
            }

            var idx = 0;
            foreach (var invIdx in itemIndices)
            {
                var deconstructorItem = new DeconstructorItem(idx, ItemsContainer, Deconstructor, invIdx);
                deconstructorItem.Setup();
                var item = Globals.Me.Inventory[invIdx];
                deconstructorItem.Update(item.ItemId, item.StatBuffs);

                var xPadding = deconstructorItem.Pnl.Margin.Left + deconstructorItem.Pnl.Margin.Right;
                var yPadding = deconstructorItem.Pnl.Margin.Top + deconstructorItem.Pnl.Margin.Bottom;

                deconstructorItem.SetPosition(
                    idx %
                    ((ItemsContainer.Width - ItemsContainer.GetVerticalScrollBar().Width) / (deconstructorItem.Pnl.Width + xPadding)) *
                    (deconstructorItem.Pnl.Width + xPadding) +
                    xPadding,
                    idx /
                    ((ItemsContainer.Width - ItemsContainer.GetVerticalScrollBar().Width) / (deconstructorItem.Pnl.Width + xPadding)) *
                    (deconstructorItem.Pnl.Height + yPadding) +
                    yPadding
                );

                idx++;
            }

            // Wait for collection change before bothering
            Deconstructor.Refresh = false;
        }

        private void ClearItems()
        {
            ItemsContainer.ClearCreatedChildren();
        }

        private void CancelButton_Clicked(Base sender, Framework.Gwen.Control.EventArguments.ClickedEventArgs arguments)
        {
            Close();
        }


        protected override void Close()
        {
            base.Close();

            Deconstructor.Close();
        }
    }
}
