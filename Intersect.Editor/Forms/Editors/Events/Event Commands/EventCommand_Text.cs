using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using Intersect.Editor.Content;
using Intersect.Editor.General;
using Intersect.Editor.Localization;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using Intersect.GameObjects.Events.Commands;
using Intersect.Models;
using Intersect.Utilities;
using static Intersect.GameObjects.Events.Commands.ShowTextCommand;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{

    public partial class EventCommandText : UserControl
    {

        private readonly FrmEvent mEventEditor;

        private ShowTextCommand mMyCommand;

        private bool IsLoading;

        public EventCommandText(ShowTextCommand refCommand, FrmEvent editor)
        {
            IsLoading = true;
            InitializeComponent();
            mMyCommand = refCommand;
            mEventEditor = editor;
            InitLocalization();
            txtShowText.Text = mMyCommand.Text;
            cmbFace.Items.Clear();
            cmbFace.Items.Add(Strings.General.none);
            cmbFace.Items.AddRange(GameContentManager.GetSmartSortedTextureNames(GameContentManager.TextureType.Face));

            cmbItems.Items.Clear();
            cmbItems.Items.Add(Strings.EditorGenerics.UseLast);
            cmbItems.Items.AddRange(ItemBase.Names);
            cmbQuests.Items.Clear();
            cmbQuests.Items.Add(Strings.EditorGenerics.UseLast);
            cmbQuests.Items.AddRange(QuestBase.Names);

            if (cmbFace.Items.IndexOf(TextUtils.NullToNone(mMyCommand.Face)) > -1)
            {
                cmbFace.SelectedIndex = cmbFace.Items.IndexOf(TextUtils.NullToNone(mMyCommand.Face));
            }
            else
            {
                cmbFace.SelectedIndex = 0;
            }

            var itmIdex = ItemBase.ListIndex(mMyCommand.ItemId) + 1;
            cmbItems.SelectedIndex = itmIdex >= 1 && itmIdex < cmbItems.Items.Count + 1 ? itmIdex: 0;
            
            var questIdx = QuestBase.ListIndex(mMyCommand.QuestId) + 1;
            cmbQuests.SelectedIndex = questIdx >= 0 && questIdx < cmbItems.Items.Count + 1 ? questIdx : 0;

            chkTemplate.Checked = mMyCommand.UseTemplate;

            if (chkTemplate.Checked)
            {
                rdoItem.Checked = mMyCommand.Template == ShowTextTemplate.ItemObtained;
                rdoQuest.Checked = mMyCommand.Template == ShowTextTemplate.QuestCompleted;
                rdoInventorySpace.Checked = mMyCommand.Template == ShowTextTemplate.NoSpace;
                rdoLocked.Checked = mMyCommand.Template == ShowTextTemplate.Locked;
                rdoExp.Checked = mMyCommand.Template == ShowTextTemplate.Exp;
                rdoGiveItem.Checked = mMyCommand.Template == ShowTextTemplate.GiveItem;
            }

            chkSendToChatbox.Checked = mMyCommand.SendToChatbox;
            cmbColor.Items.Clear();
            foreach (Color.ChatColor color in Enum.GetValues(typeof(Color.ChatColor)))
            {
                cmbColor.Items.Add(Globals.GetColorName(color));
            }

            if (!string.IsNullOrEmpty(mMyCommand.Color))
            {
                cmbColor.SelectedIndex = cmbColor.Items.IndexOf(mMyCommand.Color);
                if (cmbColor.SelectedIndex == -1)
                {
                    cmbColor.SelectedIndex = 0;
                }
            }
            else
            {
                cmbColor.SelectedIndex = 0;
            }
            
            if (mMyCommand.Channel != null)
            {
                cmbChannel.SelectedIndex = (int)mMyCommand.Channel;
            }
            else
            {
                cmbChannel.SelectedIndex = 0;
            }

            chkPlaySound.Checked = mMyCommand.PlaySound;
            cmbSound.Items.Clear();
            cmbSound.Items.AddRange(GameContentManager.SmartSortedSoundNames);
            if (cmbSound.Items.IndexOf(TextUtils.NullToNone(mMyCommand.Sound)) > -1)
            {
                cmbSound.SelectedIndex = cmbSound.Items.IndexOf(TextUtils.NullToNone(mMyCommand.Sound));
            }
            else
            {
                cmbSound.SelectedIndex = 0;
            }

            if (rdoGiveItem.Checked)
            {
                txtVerb.Text = mMyCommand.TemplateParam;
            }

            if (rdoItem.Checked)
            {
                nudTemplateQuantity.Value = mMyCommand.TemplateQuantity;
            }

            ToggleAvailabilities();

            UpdateFacePreview();
            IsLoading = false;
        }

        private void ToggleAvailabilities()
        {
            grpText.Enabled = !chkTemplate.Checked;
            grpText.Visible = !chkTemplate.Checked;
            grpTemplates.Enabled = chkTemplate.Checked;
            grpTemplates.Visible = chkTemplate.Checked;
            cmbItems.Enabled = rdoItem.Checked;
            cmbQuests.Enabled = rdoQuest.Checked;
            grpChatboxSettings.Enabled = chkSendToChatbox.Checked;
            grpSound.Enabled = chkPlaySound.Checked;
            txtVerb.Enabled = rdoGiveItem.Checked;
        }

        private void InitLocalization()
        {
            grpShowText.Text = Strings.EventShowText.title;
            lblText.Text = Strings.EventShowText.text;
            lblFace.Text = Strings.EventShowText.face;
            lblCommands.Text = Strings.EventShowText.commands;
            btnSave.Text = Strings.EventShowText.okay;
            btnCancel.Text = Strings.EventShowText.cancel;
            cmbChannel.Items.Clear();
            for (var i = 0; i < Strings.EventChatboxText.channels.Count; i++)
            {
                cmbChannel.Items.Add(Strings.EventChatboxText.channels[i]);
            }
            cmbSound.Items.Clear();
            cmbSound.Items.AddRange(Enum.GetNames(typeof(CommonTextSounds)));
        }

        private void UpdateFacePreview()
        {
            if (File.Exists("resources/faces/" + cmbFace.Text))
            {
                pnlFace.BackgroundImage = new Bitmap("resources/faces/" + cmbFace.Text);
            }
            else
            {
                pnlFace.BackgroundImage = null;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            mMyCommand.Text = txtShowText.Text;
            mMyCommand.Face = TextUtils.SanitizeNone(cmbFace?.Text);
            mMyCommand.UseTemplate = chkTemplate.Checked;

            mMyCommand.ItemId = mMyCommand.UseTemplate && rdoItem.Checked ? ItemBase.IdFromList(cmbItems.SelectedIndex - 1) : default;
            mMyCommand.QuestId = mMyCommand.UseTemplate && rdoQuest.Checked ? QuestBase.IdFromList(cmbQuests.SelectedIndex - 1) : default;
            mMyCommand.Template = GetSelectedTemplate();

            mMyCommand.SendToChatbox = chkSendToChatbox.Checked;
            mMyCommand.Color = cmbColor.Text;
            mMyCommand.Channel = (ChatboxChannel)cmbChannel.SelectedIndex;

            mMyCommand.PlaySound = chkPlaySound.Checked;
            mMyCommand.Sound = cmbSound.Text;

            mMyCommand.TemplateParam = string.Empty;
            mMyCommand.TemplateQuantity = 0L;
            if (mMyCommand.Template == ShowTextTemplate.GiveItem)
            {
                mMyCommand.TemplateParam = txtVerb.Text;
            }

            if (mMyCommand.Template == ShowTextTemplate.ItemObtained)
            {
                mMyCommand.TemplateQuantity = (long)nudTemplateQuantity.Value;
            }

            mEventEditor.FinishCommandEdit();
        }

        private ShowTextTemplate GetSelectedTemplate()
        {
            if (rdoItem.Checked) return ShowTextTemplate.ItemObtained;
            if (rdoQuest.Checked) return ShowTextTemplate.QuestCompleted;
            if (rdoInventorySpace.Checked) return ShowTextTemplate.NoSpace;
            if (rdoLocked.Checked) return ShowTextTemplate.Locked;
            if (rdoExp.Checked) return ShowTextTemplate.Exp;
            if (rdoGiveItem.Checked) return ShowTextTemplate.GiveItem;
            // default
            return ShowTextTemplate.ItemObtained;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mEventEditor.CancelCommandEdit();
        }

        private void cmbFace_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFacePreview();
        }

        private void lblCommands_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(
                "http://www.ascensiongamedev.com/community/topic/749-event-text-variables/"
            );
        }

        private void chkTemplate_CheckedChanged(object sender, EventArgs e)
        {
            if (IsLoading)
            {
                return;
            }

            ToggleAvailabilities();
        }

        private void rdoItem_CheckedChanged(object sender, EventArgs e)
        {
            if (IsLoading)
            {
                return;
            }

            ToggleAvailabilities();
        }

        private void rdoQuest_CheckedChanged(object sender, EventArgs e)
        {
            if (IsLoading)
            {
                return;
            }

            ToggleAvailabilities();
        }

        private void chkSendToChatbox_CheckedChanged(object sender, EventArgs e)
        {
            ToggleAvailabilities();
        }

        private void rdoLocked_CheckedChanged(object sender, EventArgs e)
        {
            if (IsLoading)
            {
                return;
            }

            ToggleAvailabilities();
        }

        private void rdoExp_CheckedChanged(object sender, EventArgs e)
        {
            if (IsLoading)
            {
                return;
            }

            ToggleAvailabilities();
        }

        private void chkPlaySound_CheckedChanged(object sender, EventArgs e)
        {
            ToggleAvailabilities();
        }

        private void rdoGiveItem_CheckedChanged(object sender, EventArgs e)
        {
            if (IsLoading)
            {
                return;
            }

            ToggleAvailabilities();
        }

        private void cmbCommonSound_SelectedIndexChanged(object sender, EventArgs e)
        {
            var sound = (CommonTextSounds)cmbCommonSound.SelectedIndex;
            cmbSound.Text = sound.GetDescription();
        }
    }

}
