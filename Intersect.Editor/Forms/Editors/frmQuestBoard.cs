using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using DarkUI.Forms;

using Intersect.Editor.General;
using Intersect.Editor.Localization;
using Intersect.Editor.Networking;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.QuestBoard;
using Intersect.Models;

namespace Intersect.Editor.Forms.Editors
{
    public partial class FrmQuestBoard : Form
    {
        private List<QuestBoardBase> mChanged = new List<QuestBoardBase>();

        private QuestBoardBase mEditorItem;

        private List<string> mKnownFolders = new List<string>();

        public FrmQuestBoard()
        {
            InitializeComponent();
        }

        public void InitEditor()
        {
            //Collect folders
            var mFolders = new List<string>();
            foreach (var itm in QuestBoardBase.Lookup)
            {
                if (!string.IsNullOrEmpty(((QuestBoardBase)itm.Value).Folder) &&
                    !mFolders.Contains(((QuestBoardBase)itm.Value).Folder))
                {
                    mFolders.Add(((QuestBoardBase)itm.Value).Folder);
                    if (!mKnownFolders.Contains(((QuestBoardBase)itm.Value).Folder))
                    {
                        mKnownFolders.Add(((QuestBoardBase)itm.Value).Folder);
                    }
                }
            }

            mFolders.Sort();
            mKnownFolders.Sort();
            cmbFolder.Items.Clear();
            cmbFolder.Items.Add("");
            cmbFolder.Items.AddRange(mKnownFolders.ToArray());

            var items = QuestBoardBase.Lookup.OrderBy(p => p.Value?.Name).Select(pair => new KeyValuePair<Guid, KeyValuePair<string, string>>(pair.Key,
                new KeyValuePair<string, string>(((QuestBoardBase)pair.Value)?.Name ?? Models.DatabaseObject<CraftingTableBase>.Deleted, ((QuestBoardBase)pair.Value)?.Folder ?? ""))).ToArray();
            lstGameObjects.Repopulate(items, mFolders, false, CustomSearch(), txtSearch.Text);
        }

        private bool CustomSearch()
        {
            return !string.IsNullOrWhiteSpace(txtSearch.Text) &&
                   txtSearch.Text != Strings.CraftingTableEditor.searchplaceholder;
        }
    }
}
