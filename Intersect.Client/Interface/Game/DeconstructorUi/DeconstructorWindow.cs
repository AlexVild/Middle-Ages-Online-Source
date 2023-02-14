using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.General.Deconstructor;
using Intersect.Client.Networking;
using Intersect.Enums;
using Intersect.GameObjects;
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

        ImagePanel AddFuelBg { get; set; }
        ScrollControl AddFuelContainer { get; set; }
        Button SubmitFuelButton { get; set; }
        Button CancelFuelButton { get; set; }

        Label NoItemsLabelTemplate { get; set; }
        RichLabel NoItemsLabel { get; set; }
        Label FuelLabel { get; set; }
        Label RemainingFuelLabel { get; set; }
        Label RequiredFuelLabel { get; set; }

        Button CancelButton { get; set; }
        Button DeconstructButton { get; set; }
        Button AddFuelButton { get; set; }

        Label PotentialFuel { get; set; }
        Label CurrentFuel { get; set; }
        Label AddFuelExplanationTemplate { get; set; }
        RichLabel AddFuelExplanation { get; set; }

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
            AddFuelButton.Clicked += AddFuelButton_Clicked;

            AddFuelBg = new ImagePanel(Background, "AddFuelBg");
            AddFuelContainer = new ScrollControl(AddFuelBg, "AddFuelContainer");
            SubmitFuelButton = new Button(AddFuelBg, "Submit")
            {
                Text = "SUBMIT"
            };
            CancelFuelButton = new Button(AddFuelBg, "Cancel")
            {
                Text = "CANCEL"
            };
            PotentialFuel = new Label(AddFuelBg, "PotentialFuelLabel");
            CurrentFuel = new Label(AddFuelBg, "CurrentFuelLabel");
            AddFuelExplanationTemplate = new Label(AddFuelBg, "AddFuelExplanation");
            AddFuelExplanation = new RichLabel(AddFuelBg);

            CancelFuelButton.Clicked += CancelFuelButton_Clicked;

            AddFuelBg.IsHidden = true;
        }

        private void CancelFuelButton_Clicked(Base sender, Framework.Gwen.Control.EventArguments.ClickedEventArgs arguments)
        {
            Deconstructor.CloseFuelAddition();
        }

        private void AddFuelButton_Clicked(Base sender, Framework.Gwen.Control.EventArguments.ClickedEventArgs arguments)
        {
            Deconstructor.OpenFuelAddition();
            AddFuelBg.BringToFront();
            SubmitFuelButton.BringToFront();
            CancelFuelButton.BringToFront();
        }

        protected override void PostInitialization()
        {
            NoItemsLabel.SetText("Right-click on equipment in your inventory to add to the deconstructor. You will need the appropriate amount of fuel to deconstruct items.",
                NoItemsLabelTemplate, ItemsContainer.Width - 64);
            NoItemsLabel.ProcessAlignments();

            AddFuelExplanation.SetText("Right-click on fuel sources in your inventory to select them. Pressing 'submit' will consume those items in return for deconstruction fuel.",
                AddFuelExplanationTemplate, AddFuelContainer.Width - 32);
            AddFuelExplanation.ProcessAlignments();
        }

        public override void UpdateShown()
        {
            if (Deconstructor == default || !Deconstructor.Refresh)
            {
                return;
            }

            var itemIndices = Deconstructor.Items.ToArray();

            AddFuelBg.IsHidden = !Deconstructor?.AddingFuel ?? true;

            ClearItems();
            ClearFuel();

            if (AddFuelBg.IsHidden)
            {
                DeconstructButton.Enable();
                CancelButton.Enable();
                AddFuelButton.Enable();

                if (itemIndices.Length <= 0)
                {
                    NoItemsLabel.Show();
                    return;
                }
                else
                {
                    NoItemsLabel.Hide();
                }

                UpdateItemsToDeconstruct();
            }
            else
            {
                DeconstructButton.Disable();
                CancelButton.Disable();
                AddFuelButton.Disable();

                var potentialFuel = Deconstructor.FuelItems
                    .Aggregate(0, (int fuel, int invSlot) => fuel + ItemBase.Get(Globals.Me.Inventory[invSlot].ItemId).Fuel);
                PotentialFuel.SetText($"Potential Fuel: {potentialFuel}");
                PotentialFuel.IsHidden = potentialFuel <= 0;
                CurrentFuel.SetText($"Total Fuel: 100");

                if (Deconstructor.FuelItems.Count <= 0)
                {
                    AddFuelExplanation.Show();
                    return;
                }
                else
                {
                    AddFuelExplanation.Hide();
                }

                UpdateFuelItems();
            }

            Deconstructor.Refresh = false;
        }

        private void UpdateItemsToDeconstruct()
        {
            var itemIndices = Deconstructor.Items.ToArray();

            var idx = 0;
            foreach (var invIdx in itemIndices)
            {
                var deconstructorItem = new DeconstructorItem(idx, ItemsContainer, Deconstructor, invIdx, Background.X + 48, Background.Y + 39);
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
        }

        private void UpdateFuelItems()
        {
            var itemIndices = Deconstructor.FuelItems.ToArray();

            var idx = 0;
            foreach (var invIdx in itemIndices)
            {
                var fuelItem = new FuelItem(idx, AddFuelContainer, Deconstructor, invIdx, Background.X + 202, Background.Y + 114);
                fuelItem.Setup();
                var item = Globals.Me.Inventory[invIdx];
                fuelItem.Update(item.ItemId, item.StatBuffs);

                var xPadding = fuelItem.Pnl.Margin.Left + fuelItem.Pnl.Margin.Right;
                var yPadding = fuelItem.Pnl.Margin.Top + fuelItem.Pnl.Margin.Bottom;

                fuelItem.SetPosition(
                    idx %
                    ((AddFuelContainer.Width - AddFuelContainer.GetVerticalScrollBar().Width) / (fuelItem.Pnl.Width + xPadding)) *
                    (fuelItem.Pnl.Width + xPadding) +
                    xPadding,
                    idx /
                    ((AddFuelContainer.Width - AddFuelContainer.GetVerticalScrollBar().Width) / (fuelItem.Pnl.Width + xPadding)) *
                    (fuelItem.Pnl.Height + yPadding) +
                    yPadding
                );

                idx++;
            }
        }

        private void ClearItems()
        {
            ItemsContainer.ClearCreatedChildren();
        }

        private void ClearFuel()
        {
            AddFuelContainer.ClearCreatedChildren();
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
