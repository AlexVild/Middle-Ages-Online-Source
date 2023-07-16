using DarkUI.Controls;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommandCompleteQuestTask
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpCompleteTask = new DarkUI.Controls.DarkGroupBox();
            this.chkNoNotify = new DarkUI.Controls.DarkCheckBox();
            this.chkSkipCompletionEvent = new DarkUI.Controls.DarkCheckBox();
            this.cmbQuestTask = new DarkUI.Controls.DarkComboBox();
            this.lblTask = new System.Windows.Forms.Label();
            this.cmbQuests = new DarkUI.Controls.DarkComboBox();
            this.lblQuest = new System.Windows.Forms.Label();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.grpCompleteTask.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpCompleteTask
            // 
            this.grpCompleteTask.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpCompleteTask.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpCompleteTask.Controls.Add(this.chkNoNotify);
            this.grpCompleteTask.Controls.Add(this.chkSkipCompletionEvent);
            this.grpCompleteTask.Controls.Add(this.cmbQuestTask);
            this.grpCompleteTask.Controls.Add(this.lblTask);
            this.grpCompleteTask.Controls.Add(this.cmbQuests);
            this.grpCompleteTask.Controls.Add(this.lblQuest);
            this.grpCompleteTask.Controls.Add(this.btnCancel);
            this.grpCompleteTask.Controls.Add(this.btnSave);
            this.grpCompleteTask.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpCompleteTask.Location = new System.Drawing.Point(3, 3);
            this.grpCompleteTask.Name = "grpCompleteTask";
            this.grpCompleteTask.Size = new System.Drawing.Size(404, 146);
            this.grpCompleteTask.TabIndex = 17;
            this.grpCompleteTask.TabStop = false;
            this.grpCompleteTask.Text = "Complete Quest Task";
            // 
            // chkNoNotify
            // 
            this.chkNoNotify.AutoSize = true;
            this.chkNoNotify.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.chkNoNotify.Location = new System.Drawing.Point(4, 94);
            this.chkNoNotify.Name = "chkNoNotify";
            this.chkNoNotify.Size = new System.Drawing.Size(168, 17);
            this.chkNoNotify.TabIndex = 25;
            this.chkNoNotify.Text = "Do not send chat notification?";
            // 
            // chkSkipCompletionEvent
            // 
            this.chkSkipCompletionEvent.AutoSize = true;
            this.chkSkipCompletionEvent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.chkSkipCompletionEvent.Location = new System.Drawing.Point(4, 74);
            this.chkSkipCompletionEvent.Name = "chkSkipCompletionEvent";
            this.chkSkipCompletionEvent.Size = new System.Drawing.Size(166, 17);
            this.chkSkipCompletionEvent.TabIndex = 24;
            this.chkSkipCompletionEvent.Text = "Do not run completion event?";
            // 
            // cmbQuestTask
            // 
            this.cmbQuestTask.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbQuestTask.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbQuestTask.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbQuestTask.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbQuestTask.DrawDropdownHoverOutline = false;
            this.cmbQuestTask.DrawFocusRectangle = false;
            this.cmbQuestTask.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbQuestTask.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQuestTask.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbQuestTask.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbQuestTask.FormattingEnabled = true;
            this.cmbQuestTask.Location = new System.Drawing.Point(47, 47);
            this.cmbQuestTask.Name = "cmbQuestTask";
            this.cmbQuestTask.Size = new System.Drawing.Size(351, 21);
            this.cmbQuestTask.TabIndex = 24;
            this.cmbQuestTask.Text = null;
            this.cmbQuestTask.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbQuestTask.Visible = false;
            // 
            // lblTask
            // 
            this.lblTask.AutoSize = true;
            this.lblTask.Location = new System.Drawing.Point(4, 50);
            this.lblTask.Name = "lblTask";
            this.lblTask.Size = new System.Drawing.Size(34, 13);
            this.lblTask.TabIndex = 23;
            this.lblTask.Text = "Task:";
            this.lblTask.Visible = false;
            // 
            // cmbQuests
            // 
            this.cmbQuests.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbQuests.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbQuests.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbQuests.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbQuests.DrawDropdownHoverOutline = false;
            this.cmbQuests.DrawFocusRectangle = false;
            this.cmbQuests.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbQuests.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQuests.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbQuests.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbQuests.FormattingEnabled = true;
            this.cmbQuests.Location = new System.Drawing.Point(47, 19);
            this.cmbQuests.Name = "cmbQuests";
            this.cmbQuests.Size = new System.Drawing.Size(351, 21);
            this.cmbQuests.TabIndex = 22;
            this.cmbQuests.Text = null;
            this.cmbQuests.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbQuests.SelectedIndexChanged += new System.EventHandler(this.cmbQuests_SelectedIndexChanged);
            // 
            // lblQuest
            // 
            this.lblQuest.AutoSize = true;
            this.lblQuest.Location = new System.Drawing.Point(4, 22);
            this.lblQuest.Name = "lblQuest";
            this.lblQuest.Size = new System.Drawing.Size(38, 13);
            this.lblQuest.TabIndex = 21;
            this.lblQuest.Text = "Quest:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(89, 117);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(6, 117);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Ok";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // EventCommandCompleteQuestTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.grpCompleteTask);
            this.Name = "EventCommandCompleteQuestTask";
            this.Size = new System.Drawing.Size(415, 155);
            this.grpCompleteTask.ResumeLayout(false);
            this.grpCompleteTask.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkGroupBox grpCompleteTask;
        private DarkButton btnCancel;
        private DarkButton btnSave;
        private System.Windows.Forms.Label lblQuest;
        private DarkComboBox cmbQuests;
        private DarkComboBox cmbQuestTask;
        private System.Windows.Forms.Label lblTask;
        private DarkCheckBox chkNoNotify;
        private DarkCheckBox chkSkipCompletionEvent;
    }
}
