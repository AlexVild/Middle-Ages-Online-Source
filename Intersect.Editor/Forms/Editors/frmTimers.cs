using Intersect.Editor.Localization;
using Intersect.GameObjects;
using Intersect.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Intersect.Editor.Forms.Editors
{
    public partial class frmTimers : EditorForm
    {
        /*private List<IDatabaseObject> mChanged = new List<IDatabaseObject>();

        private IDatabaseObject mEditorItem;

        private List<string> mPlayerExpandedFolders = new List<string>();

        private List<string> mPlayerKnownFolders = new List<string>();

        private List<string> mInstanceExpandedFolders = new List<string>();

        private List<string> mInstanceKnownFolders = new List<string>();*/

        public frmTimers()
        {
           /* ApplyHooks();*/
            InitializeComponent();
            //InitLocalization();

            //lstGameObjects.Init(UpdateToolStripItems, AssignEditorItem, toolStripItemNew_Click, toolStripItemCopy_Click, toolStripItemUndo_Click, toolStripItemPaste_Click, toolStripItemDelete_Click);
        }

       /* private void AssignEditorItem(Guid id)
        {
            if (id != Guid.Empty)
            {
                IDatabaseObject obj = null;
                if (rdoPlayerTimers.Checked)
                {
                    obj = PlayerTimerBase.Get(id);
                }
                else if (rdoInstanceTimers.Checked)
                {
                    obj = InstanceTimerBase.Get(id);
                }

                if (obj != null)
                {
                    mEditorItem = obj;
                    if (!mChanged.Contains(obj))
                    {
                        mChanged.Add(obj);
                        obj.MakeBackup();
                    }
                }
            }
            UpdateEditor();
        }

        private void UpdateEditor()
        {
            if (mEditorItem != null)
            {
                pnlTimerSettings.Show();
                if (rdoPlayerTimers.Checked)
                {
                    lblObject.Text = Strings.VariableEditor.playervariable;
                    txtObjectName.Text = ((PlayerVariableBase)mEditorItem).Name;
                    txtId.Text = ((PlayerVariableBase)mEditorItem).TextId;
                    cmbFolder.Text = ((PlayerVariableBase)mEditorItem).Folder;
                    cmbVariableType.SelectedIndex = (int)(((PlayerVariableBase)mEditorItem).Type - 1);
                    grpValue.Hide();
                }
                else if (rdoInstanceTimers.Checked)
                {
                    lblObject.Text = Strings.VariableEditor.globalvariable;
                    txtObjectName.Text = ((ServerVariableBase)mEditorItem).Name;
                    txtId.Text = ((ServerVariableBase)mEditorItem).TextId;
                    cmbFolder.Text = ((ServerVariableBase)mEditorItem).Folder;
                    cmbVariableType.SelectedIndex = (int)(((ServerVariableBase)mEditorItem).Type - 1);
                    grpValue.Show();
                }

                InitValueGroup();
            }
            else
            {
                pnlTimerSettings.Hide();
            }

            UpdateToolStripItems();
        }*/
    }
}
