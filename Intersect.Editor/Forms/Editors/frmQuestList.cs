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
using Intersect.GameObjects.QuestList;
using Intersect.Models;

namespace Intersect.Editor.Forms.Editors
{
    public partial class FrmQuestList : Form
    {
        private List<QuestListBase> mChanged = new List<QuestListBase>();

        private QuestListBase mEditorItem;

        private List<string> mKnownFolders = new List<string>();

        public FrmQuestList()
        {
            InitializeComponent();
        }

        public void InitEditor()
        {
            //Collect folders
            var mFolders = new List<string>();
            foreach (var itm in QuestListBase.Lookup)
            {
                if (!string.IsNullOrEmpty(((QuestListBase)itm.Value).Folder) &&
                    !mFolders.Contains(((QuestListBase)itm.Value).Folder))
                {
                    mFolders.Add(((QuestListBase)itm.Value).Folder);
                    if (!mKnownFolders.Contains(((QuestListBase)itm.Value).Folder))
                    {
                        mKnownFolders.Add(((QuestListBase)itm.Value).Folder);
                    }
                }
            }

            mFolders.Sort();
            mKnownFolders.Sort();
            cmbFolder.Items.Clear();
            cmbFolder.Items.Add("");
            cmbFolder.Items.AddRange(mKnownFolders.ToArray());

            var items = QuestListBase.Lookup.OrderBy(p => p.Value?.Name).Select(pair => new KeyValuePair<Guid, KeyValuePair<string, string>>(pair.Key,
                new KeyValuePair<string, string>(((QuestListBase)pair.Value)?.Name ?? Models.DatabaseObject<CraftingTableBase>.Deleted, ((QuestListBase)pair.Value)?.Folder ?? ""))).ToArray();
            lstGameObjects.Repopulate(items, mFolders, false, CustomSearch(), txtSearch.Text);
        }

        private bool CustomSearch()
        {
            return !string.IsNullOrWhiteSpace(txtSearch.Text) &&
                   txtSearch.Text != Strings.CraftingTableEditor.searchplaceholder;
        }
    }
}
