using Intersect.Client.Framework.Gwen.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Interface.Game.Character
{
    public abstract class SearchableCharacterWindowPanel : CharacterWindowPanel
    {
        protected Label SearchLabel { get; set; }
        protected ImagePanel SearchContainer { get; set; }
        protected ImagePanel SearchBg { get; set; }
        protected TextBox SearchBar { get; set; }
        protected string SearchTerm
        {
            get => SearchBar.Text;
            set => SearchBar.SetText(value);
        }
        protected Button SearchClearButton { get; set; }

        public override void Show()
        {
            SearchTerm = string.Empty;
            Interface.InputBlockingElements.Add(SearchBar);
            base.Show();
        }

        public override void Hide()
        {
            Interface.InputBlockingElements.Remove(SearchBar);
            base.Hide();
        }

        private void SearchBar_TextChanged(Base sender, EventArgs arguments)
        {
            if (SearchTerm.Length >= 3 || string.IsNullOrEmpty(SearchTerm))
            {
                SearchTextChanged();
            }
        }

        private void SearchClearButton_Clicked(Base sender, Framework.Gwen.Control.EventArguments.ClickedEventArgs arguments)
        {
            SearchTerm = string.Empty;
            ClearSearchClicked();
        }

        protected abstract void SearchTextChanged();
        protected abstract void ClearSearchClicked();

        protected virtual void Initialize()
        {
            SearchContainer = new ImagePanel(mBackground, "SearchContainer");
            SearchLabel = new Label(SearchContainer, "SearchLabel")
            {
                Text = "Search:"
            };
            SearchBg = new ImagePanel(SearchContainer, "SearchBg");
            SearchBar = new TextBox(SearchBg, "SearchBar");
            SearchBar.TextChanged += SearchBar_TextChanged;

            SearchClearButton = new Button(SearchContainer, "ClearButton")
            {
                Text = "CLEAR"
            };
            SearchClearButton.Clicked += SearchClearButton_Clicked;
        }
    }
}
