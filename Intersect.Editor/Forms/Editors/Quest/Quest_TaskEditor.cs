using System;
using System.Windows.Forms;

using Intersect.Editor.Forms.Editors.Events;
using Intersect.Editor.General;
using Intersect.Editor.Localization;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.Crafting;
using Intersect.Logging;

namespace Intersect.Editor.Forms.Editors.Quest
{

    public partial class QuestTaskEditor : UserControl
    {

        public bool Cancelled;

        private string mEventBackup = null;

        private QuestBase mMyQuest;

        private QuestBase.QuestTask mMyTask;

        public QuestTaskEditor(QuestBase refQuest, QuestBase.QuestTask refTask)
        {
            if (refQuest == null)
            {
                Log.Warn($@"{nameof(refQuest)} is null.");
            }

            if (refTask == null)
            {
                Log.Warn($@"{nameof(refTask)} is null.");
            }

            InitializeComponent();
            mMyTask = refTask;
            mMyQuest = refQuest;

            if (mMyTask?.EditingEvent == null)
            {
                Log.Warn($@"{nameof(mMyTask.EditingEvent)} is null.");
            }

            mEventBackup = mMyTask?.EditingEvent?.JsonData;
            InitLocalization();
            cmbTaskType.SelectedIndex = mMyTask == null ? -1 : (int) mMyTask.Objective;
            txtStartDesc.Text = mMyTask?.Description;
            UpdateFormElements();
            switch (cmbTaskType.SelectedIndex)
            {
                case 0: //Event Driven
                    break;
                case 1: //Gather Items
                    cmbItem.SelectedIndex = ItemBase.ListIndex(mMyTask?.TargetId ?? Guid.Empty);
                    nudItemAmount.Value = mMyTask?.Quantity ?? 0;

                    break;
                case 2: //Kill NPCS
                    cmbNpc.SelectedIndex = NpcBase.ListIndex(mMyTask?.TargetId ?? Guid.Empty);
                    nudNpcQuantity.Value = mMyTask?.Quantity ?? 0;

                    break;
                case 3: //Craft crafts
                    cmbCraft.SelectedIndex = CraftBase.ListIndex(mMyTask?.TargetId ?? Guid.Empty);
                    nudCraftAmt.Value = mMyTask?.Quantity ?? 0;

                    break;
                case 4: //Harvest Resources
                    cmbResource.SelectedIndex = ResourceBase.ListIndex(mMyTask?.TargetId ?? Guid.Empty);
                    nudResourceAmt.Value = mMyTask?.Quantity ?? 0;
                    
                    break;
            }
        }

        private void InitLocalization()
        {
            grpEditor.Text = Strings.TaskEditor.editor;

            lblType.Text = Strings.TaskEditor.type;
            cmbTaskType.Items.Clear();
            for (var i = 0; i < Strings.TaskEditor.types.Count; i++)
            {
                cmbTaskType.Items.Add(Strings.TaskEditor.types[i]);
            }

            lblDesc.Text = Strings.TaskEditor.desc;

            grpKillNpcs.Text = Strings.TaskEditor.killnpcs;
            lblNpc.Text = Strings.TaskEditor.npc;
            lblNpcQuantity.Text = Strings.TaskEditor.npcamount;

            grpGatherItems.Text = Strings.TaskEditor.gatheritems;
            lblItem.Text = Strings.TaskEditor.item;
            lblItemQuantity.Text = Strings.TaskEditor.gatheramount;

            lblEventDriven.Text = Strings.TaskEditor.eventdriven;

            btnEditTaskEvent.Text = Strings.TaskEditor.editcompletionevent;
            btnSave.Text = Strings.TaskEditor.ok;
            btnCancel.Text = Strings.TaskEditor.cancel;
        }

        private void UpdateFormElements()
        {
            grpGatherItems.Hide();
            grpKillNpcs.Hide();
            switch (cmbTaskType.SelectedIndex)
            {
                case 0: //Event Driven
                    break;
                case 1: //Gather Items
                    grpGatherItems.Show();
                    cmbItem.Items.Clear();
                    cmbItem.Items.AddRange(ItemBase.Names);
                    if (cmbItem.Items.Count > 0)
                    {
                        cmbItem.SelectedIndex = 0;
                    }

                    nudItemAmount.Value = 1;

                    break;
                case 2: //Kill Npcs
                    grpKillNpcs.Show();
                    cmbNpc.Items.Clear();
                    cmbNpc.Items.AddRange(NpcBase.Names);
                    if (cmbNpc.Items.Count > 0)
                    {
                        cmbNpc.SelectedIndex = 0;
                    }

                    nudNpcQuantity.Value = 1;

                    break;
                case 3: //Craft crafts
                    grpCraftCrafts.Show();
                    cmbCraft.Items.Clear();
                    cmbCraft.Items.AddRange(CraftBase.Names);
                    if (cmbCraft.Items.Count > 0)
                    {
                        cmbCraft.SelectedIndex = 0;
                    }

                    nudCraftAmt.Value = 1;

                    break;
                case 4: //Harvest Resources
                    grpResource.Show();
                    cmbResource.Items.Clear();
                    cmbResource.Items.AddRange(ResourceBase.Names);
                    if (cmbResource.Items.Count > 0)
                    {
                        cmbResource.SelectedIndex = 0;
                    }

                    nudResourceAmt.Value = 1;

                    break;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            mMyTask.Objective = (QuestObjective) cmbTaskType.SelectedIndex;
            mMyTask.Description = txtStartDesc.Text;
            switch (mMyTask.Objective)
            {
                case QuestObjective.EventDriven: //Event Driven
                    mMyTask.TargetId = Guid.Empty;
                    mMyTask.Quantity = 1;

                    break;
                case QuestObjective.GatherItems: //Gather Items
                    mMyTask.TargetId = ItemBase.IdFromList(cmbItem.SelectedIndex);
                    mMyTask.Quantity = (int) nudItemAmount.Value;

                    break;
                case QuestObjective.KillNpcs: //Kill Npcs
                    mMyTask.TargetId = NpcBase.IdFromList(cmbNpc.SelectedIndex);
                    mMyTask.Quantity = (int) nudNpcQuantity.Value;

                    break;
                case QuestObjective.CraftItems: //Craft Items
                    mMyTask.TargetId = CraftBase.IdFromList(cmbCraft.SelectedIndex);
                    mMyTask.Quantity = (int)nudCraftAmt.Value;

                    break;
                case QuestObjective.HarvestResources: //Harvest Resources
                    mMyTask.TargetId = ResourceBase.IdFromList(cmbResource.SelectedIndex);
                    mMyTask.Quantity = (int)nudResourceAmt.Value;

                    break;
            }

            ParentForm.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cancelled = true;
            mMyTask.EditingEvent.Load(mEventBackup);
            ParentForm.Close();
        }

        private void cmbConditionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFormElements();
        }

        private void btnEditTaskEvent_Click(object sender, EventArgs e)
        {
            mMyTask.EditingEvent.Name = Strings.TaskEditor.completionevent.ToString(mMyQuest.Name);
            var editor = new FrmEvent(null)
            {
                MyEvent = mMyTask.EditingEvent
            };

            editor.InitEditor(true, true, true);
            editor.ShowDialog();
            Globals.MainForm.BringToFront();
            BringToFront();
        }

    }

}
