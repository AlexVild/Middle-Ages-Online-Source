using Intersect.Client.Core;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.General.Deconstructor;
using Intersect.Client.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Interface.Game
{
    public sealed class DeconstructorWindow : GameWindow
    {
        Label ItemsLabel { get; set; }
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

        public override void UpdateShown()
        {
            if (Deconstructor == default)
            {
                return;
            }
        }

        protected override void PreInitialization()
        {
            CancelButton = new Button(Background, "CancelButton")
            {
                Text = "CANCEL"
            };
            CancelButton.Clicked += CancelButton_Clicked;

            DeconstructButton = new Button(Background, "DeconstructButton")
            {
                Text = "DECONSTRUCT"
            };

            AddFuelButton = new Button(Background, "AddFuelButton")
            {
                Text = "ADD FUEL"
            };
        }

        private void CancelButton_Clicked(Base sender, Framework.Gwen.Control.EventArguments.ClickedEventArgs arguments)
        {
            Close();
        }

        protected override void PostInitialization()
        {
            // empty
        }

        protected override void Close()
        {
            base.Close();

            Deconstructor.Close();
        }
    }
}
