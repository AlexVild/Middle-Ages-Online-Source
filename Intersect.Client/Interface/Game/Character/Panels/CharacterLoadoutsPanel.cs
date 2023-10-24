using Intersect.Client.Core;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Interface.Components;
using Intersect.Client.Interface.Game.Components;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Network.Packets.Server;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intersect.Client.Interface.Game.Character.Panels
{
    public class CharacterLoadoutsPanel : CharacterWindowPanel
    {
        public CharacterPanelType Type = CharacterPanelType.Loadouts;

        public bool RefreshUi
        {
            get => CharacterLoadoutsController.RefreshUi;
            set => CharacterLoadoutsController.RefreshUi = value;
        }

        public List<Loadout> Loadouts
        {
            get => CharacterLoadoutsController.Loadouts;
        }

        private Label TitleLabel { get; set; }

        private Button NewLoadoutButton { get; set; }

        private Label NoLoadoutsLabel { get; set; }

        private ImagePanel LoadoutsBackground { get; set; }
        private ListBox LoadoutsList { get; set; }

        private LoadoutRowComponent LoadoutSelectors { get; set; }

        private Loadout SelectedLoadout { get; set; }

        public CharacterLoadoutsPanel(ImagePanel panelBackground)
        {
            mParentContainer = panelBackground;
            mBackground = new ImagePanel(mParentContainer, "CharacterWindowMAO_Loadouts");

            TitleLabel = new Label(mBackground, "TitleLabel")
            {
                Text = "Skill Loadouts"
            };

            NewLoadoutButton = new Button(mBackground, "NewLoadoutButton")
            {
                Text = "CREATE NEW"
            };
            NewLoadoutButton.Clicked += NewLoadoutButton_Clicked;

            LoadoutsBackground = new ImagePanel(mBackground, "LoadoutsBackground");
            NoLoadoutsLabel = new Label(LoadoutsBackground, "NoLoadoutsLabel")
            {
                Text = "No loadouts have been saved!"
            };
            LoadoutsList = new ListBox(LoadoutsBackground, "LoadoutsList");

            LoadoutSelectors = new LoadoutRowComponent(mBackground, "LoadoutSelectors");

            mBackground.LoadJsonUi(Framework.File_Management.GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
            
            LoadoutSelectors.Initialize();
        }

        public override void Show()
        {
            PacketSender.SendRequestLoadouts();
            base.Show();
        }

        public override void Hide()
        {
            ClearLoadouts();
            base.Hide();
        }

        public override void Update()
        {
            if (RefreshUi)
            {
                RefreshLoadouts();
                RefreshUi = false;
            }


            if (SelectedLoadout == null)
            {
                LoadoutSelectors.Hide();
            }
            else
            {
                LoadoutSelectors.Show();
            }

            return;
        }

        private void RefreshLoadouts()
        {
            var idx = 0;

            LoadoutsList?.ScrollToTop();
            ClearLoadouts();
            foreach (var loadout in Loadouts.OrderBy(l => l.Name).ToArray())
            {
                var loadoutRow = LoadoutsList.AddRowMAO(loadout.Name.Trim());
                loadoutRow.UserData = loadout;

                loadoutRow.Clicked += LoadoutRow_Clicked;
            }

            if (Loadouts.Count <= 0)
            {
                NoLoadoutsLabel.Show();
                LoadoutsList.Hide();
            }
            else
            {
                NoLoadoutsLabel.Hide();
                LoadoutsList.Show();
            }
        }

        private void LoadoutRow_Clicked(Base sender, Framework.Gwen.Control.EventArguments.ClickedEventArgs arguments)
        {
            Loadout selectedLoadout = (Loadout)sender.UserData;
            if (selectedLoadout == default || !Loadouts.Select(l => l.Id).Contains(selectedLoadout.Id))
            {
                return;
            }
            
            SelectedLoadout = selectedLoadout;

            LoadoutSelectors.SetSelectedLoadout(SelectedLoadout);
        }

        private void ClearLoadouts()
        {
            LoadoutsList?.Clear();
            LoadoutsList?.UnselectAll();
            SelectedLoadout = null;
        }

        private void NewLoadoutButton_Clicked(Base sender, Framework.Gwen.Control.EventArguments.ClickedEventArgs arguments)
        {
#pragma warning disable CA2000 // Dispose objects before losing scope
            _ = new InputBox(
                Strings.Loadouts.NewLoadoutTitle, Strings.Loadouts.NewLoadoutPrompt, true, InputBox.InputType.TextInput,
                CharacterLoadoutsController.RequestNewLoadoutSavePrompt, null, 0
            );
#pragma warning restore CA2000 // Dispose objects before losing scope
        }
    }

    public class LoadoutRowComponent : GwenComponent
    {
        private Label NameLabel { get; set; }

        private Button ApplyButton { get; set; }

        private Button OverwriteButton { get; set; }
        
        private Button RemoveButton { get; set; }

        Loadout Loadout { get; set; }

        public int X => ParentContainer.X;
        public int Y => ParentContainer.Y;

        public int Height => ParentContainer.Height;
        public int Width => ParentContainer.Width;

        public LoadoutRowComponent(Base parent,
            string containerName, 
            ComponentList<GwenComponent> referenceList = null) : base(parent, containerName, "LoadoutRowComponent", referenceList)
        {
        }

        public override void Initialize()
        {
            SelfContainer = new ImagePanel(ParentContainer, ComponentName);

            NameLabel = new Label(SelfContainer, "NameLabel")
            {
                Text = !string.IsNullOrWhiteSpace(Loadout?.Name ?? "") ? Loadout.Name : "NOT FOUND"
            };
            ApplyButton = new Button(SelfContainer, "ApplyButton")
            {
                Text = "APPLY"
            };
            OverwriteButton = new Button(SelfContainer, "OverwriteButton")
            {
                Text = "OVERWRITE"
            };
            RemoveButton = new Button(SelfContainer, "RemoveLoadoutButton")
            {
                Text = "REMOVE"
            };

            base.Initialize();
            FitParentToComponent();

            ApplyButton.Clicked += ApplyButton_Clicked;
            OverwriteButton.Clicked += OverwriteButton_Clicked;
            RemoveButton.Clicked += RemoveButton_Clicked;
        }

        public void SetSelectedLoadout(Loadout loadout)
        {
            Loadout = loadout;
        }

        private void RemoveButton_Clicked(Base sender, Framework.Gwen.Control.EventArguments.ClickedEventArgs arguments)
        {
#pragma warning disable CA2000 // Dispose objects before losing scope
            _ = new InputBox(
                Strings.Loadouts.OverwriteLoadoutTitle, Strings.Loadouts.RemoveLoadoutPrompt.ToString(Loadout.Name), true, InputBox.InputType.YesNo,
                CharacterLoadoutsController.RequestLoadoutRemovePrompt, null, Loadout.Id
            );
#pragma warning restore CA2000 // Dispose objects before losing scope
        }

        private void OverwriteButton_Clicked(Base sender, Framework.Gwen.Control.EventArguments.ClickedEventArgs arguments)
        {
#pragma warning disable CA2000 // Dispose objects before losing scope
            _ = new InputBox(
                Strings.Loadouts.OverwriteLoadoutTitle, Strings.Loadouts.OverwriteLoadoutPrompt.ToString(Loadout.Name), true, InputBox.InputType.YesNo,
                CharacterLoadoutsController.RequestLoadoutOverwritePrompt, null, Loadout.Id
            );
#pragma warning restore CA2000 // Dispose objects before losing scope
        }

        private void ApplyButton_Clicked(Base sender, Framework.Gwen.Control.EventArguments.ClickedEventArgs arguments)
        {
            CharacterLoadoutsController.RequestLoadoutSelect(Loadout.Id);
        }
    }

    public static class CharacterLoadoutsController
    {
        public static List<Loadout> Loadouts { get; set; } = new List<Loadout>();

        public static bool RefreshUi { get; set; }

        public static void LoadLoadouts(Loadout[] loadouts)
        {
            Loadouts.Clear();
            Loadouts = loadouts.ToList();
            RefreshUi = true;
        }

        public static bool TryGetLoadout(Guid loadoutId, out Loadout loadout)
        {
            loadout = Loadouts.Find(l => l.Id == loadoutId);

            return loadout != default;
        }

        public static void RequestLoadoutOverwrite(Guid loadoutId)
        {
            PacketSender.SendOverwriteLoadout(loadoutId);
        }

        public static void RequestLoadoutOverwritePrompt(object sender, EventArgs e)
        {
            var input = (InputBox)sender;
            var loadoutId = (Guid)input.UserData;
            RequestLoadoutOverwrite(loadoutId);
        }

        public static void RequestNewLoadoutSave(string name)
        {
            PacketSender.SendSaveLoadout(name);
        }

        public static void RequestNewLoadoutSavePrompt(object sender, EventArgs e)
        {
            var ibox = (InputBox)sender;
            if (ibox.TextValue.Trim().Length >= 1) //Don't bother sending a packet less than the char limit
            {
                RequestNewLoadoutSave(ibox.TextValue);
            }
        }

        public static void RequestLoadoutRemove(Guid loadoutId)
        {
            PacketSender.SendRemoveLoadout(loadoutId);
        }

        public static void RequestLoadoutRemovePrompt(object sender, EventArgs e)
        {
            var input = (InputBox)sender;
            var loadoutId = (Guid)input.UserData;
            RequestLoadoutRemove(loadoutId);
        }

        public static void RequestLoadoutSelect(Guid loadoutId)
        {
            PacketSender.SendSelectLoadout(loadoutId);
        }
    }
}
