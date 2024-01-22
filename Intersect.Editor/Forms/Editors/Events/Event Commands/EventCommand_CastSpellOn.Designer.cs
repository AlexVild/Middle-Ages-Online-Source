using System.Drawing;
using System.Windows.Forms;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommand_CastSpellOn
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
            this.grpCastSpellOn = new DarkUI.Controls.DarkGroupBox();
            this.grpTargets = new DarkUI.Controls.DarkGroupBox();
            this.chkApplyToSelf = new DarkUI.Controls.DarkCheckBox();
            this.darkButton1 = new DarkUI.Controls.DarkButton();
            this.chkApplyToGuildies = new DarkUI.Controls.DarkCheckBox();
            this.darkButton2 = new DarkUI.Controls.DarkButton();
            this.chkApplyToParty = new DarkUI.Controls.DarkCheckBox();
            this.cmbSpell = new DarkUI.Controls.DarkComboBox();
            this.lblSpell = new System.Windows.Forms.Label();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.chkInstanceMembers = new DarkUI.Controls.DarkCheckBox();
            this.grpCastSpellOn.SuspendLayout();
            this.grpTargets.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpCastSpellOn
            // 
            this.grpCastSpellOn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpCastSpellOn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpCastSpellOn.Controls.Add(this.grpTargets);
            this.grpCastSpellOn.Controls.Add(this.cmbSpell);
            this.grpCastSpellOn.Controls.Add(this.lblSpell);
            this.grpCastSpellOn.Controls.Add(this.btnCancel);
            this.grpCastSpellOn.Controls.Add(this.btnSave);
            this.grpCastSpellOn.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpCastSpellOn.Location = new System.Drawing.Point(3, 3);
            this.grpCastSpellOn.Name = "grpCastSpellOn";
            this.grpCastSpellOn.Size = new System.Drawing.Size(249, 210);
            this.grpCastSpellOn.TabIndex = 18;
            this.grpCastSpellOn.TabStop = false;
            this.grpCastSpellOn.Text = "Cast Spell On";
            // 
            // grpTargets
            // 
            this.grpTargets.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpTargets.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpTargets.Controls.Add(this.chkInstanceMembers);
            this.grpTargets.Controls.Add(this.chkApplyToSelf);
            this.grpTargets.Controls.Add(this.darkButton1);
            this.grpTargets.Controls.Add(this.chkApplyToGuildies);
            this.grpTargets.Controls.Add(this.darkButton2);
            this.grpTargets.Controls.Add(this.chkApplyToParty);
            this.grpTargets.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpTargets.Location = new System.Drawing.Point(9, 58);
            this.grpTargets.Name = "grpTargets";
            this.grpTargets.Size = new System.Drawing.Size(234, 117);
            this.grpTargets.TabIndex = 43;
            this.grpTargets.TabStop = false;
            this.grpTargets.Text = "Targets";
            // 
            // chkApplyToSelf
            // 
            this.chkApplyToSelf.AutoSize = true;
            this.chkApplyToSelf.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.chkApplyToSelf.Location = new System.Drawing.Point(7, 19);
            this.chkApplyToSelf.Name = "chkApplyToSelf";
            this.chkApplyToSelf.Size = new System.Drawing.Size(44, 17);
            this.chkApplyToSelf.TabIndex = 43;
            this.chkApplyToSelf.Text = "Self";
            // 
            // darkButton1
            // 
            this.darkButton1.Location = new System.Drawing.Point(167, 135);
            this.darkButton1.Name = "darkButton1";
            this.darkButton1.Padding = new System.Windows.Forms.Padding(5);
            this.darkButton1.Size = new System.Drawing.Size(75, 23);
            this.darkButton1.TabIndex = 20;
            this.darkButton1.Text = "Cancel";
            // 
            // chkApplyToGuildies
            // 
            this.chkApplyToGuildies.AutoSize = true;
            this.chkApplyToGuildies.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.chkApplyToGuildies.Location = new System.Drawing.Point(7, 62);
            this.chkApplyToGuildies.Name = "chkApplyToGuildies";
            this.chkApplyToGuildies.Size = new System.Drawing.Size(129, 17);
            this.chkApplyToGuildies.TabIndex = 42;
            this.chkApplyToGuildies.Text = "Online Guild Members";
            // 
            // darkButton2
            // 
            this.darkButton2.Location = new System.Drawing.Point(84, 135);
            this.darkButton2.Name = "darkButton2";
            this.darkButton2.Padding = new System.Windows.Forms.Padding(5);
            this.darkButton2.Size = new System.Drawing.Size(75, 23);
            this.darkButton2.TabIndex = 19;
            this.darkButton2.Text = "Ok";
            // 
            // chkApplyToParty
            // 
            this.chkApplyToParty.AutoSize = true;
            this.chkApplyToParty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.chkApplyToParty.Location = new System.Drawing.Point(7, 41);
            this.chkApplyToParty.Name = "chkApplyToParty";
            this.chkApplyToParty.Size = new System.Drawing.Size(96, 17);
            this.chkApplyToParty.TabIndex = 41;
            this.chkApplyToParty.Text = "Party Members";
            // 
            // cmbSpell
            // 
            this.cmbSpell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbSpell.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbSpell.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbSpell.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbSpell.DrawDropdownHoverOutline = false;
            this.cmbSpell.DrawFocusRectangle = false;
            this.cmbSpell.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSpell.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpell.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSpell.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbSpell.FormattingEnabled = true;
            this.cmbSpell.Location = new System.Drawing.Point(9, 32);
            this.cmbSpell.Name = "cmbSpell";
            this.cmbSpell.Size = new System.Drawing.Size(235, 21);
            this.cmbSpell.TabIndex = 24;
            this.cmbSpell.Text = null;
            this.cmbSpell.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblSpell
            // 
            this.lblSpell.AutoSize = true;
            this.lblSpell.Location = new System.Drawing.Point(7, 16);
            this.lblSpell.Name = "lblSpell";
            this.lblSpell.Size = new System.Drawing.Size(30, 13);
            this.lblSpell.TabIndex = 23;
            this.lblSpell.Text = "Spell";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(168, 181);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(87, 181);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Ok";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // chkInstanceMembers
            // 
            this.chkInstanceMembers.AutoSize = true;
            this.chkInstanceMembers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.chkInstanceMembers.Location = new System.Drawing.Point(7, 85);
            this.chkInstanceMembers.Name = "chkInstanceMembers";
            this.chkInstanceMembers.Size = new System.Drawing.Size(113, 17);
            this.chkInstanceMembers.TabIndex = 44;
            this.chkInstanceMembers.Text = "Instance Members";
            this.chkInstanceMembers.CheckedChanged += new System.EventHandler(this.chkInstanceMembers_CheckedChanged);
            // 
            // EventCommand_CastSpellOn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.grpCastSpellOn);
            this.Name = "EventCommand_CastSpellOn";
            this.Size = new System.Drawing.Size(260, 216);
            this.grpCastSpellOn.ResumeLayout(false);
            this.grpCastSpellOn.PerformLayout();
            this.grpTargets.ResumeLayout(false);
            this.grpTargets.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkGroupBox grpCastSpellOn;
        private DarkUI.Controls.DarkCheckBox chkApplyToGuildies;
        private DarkUI.Controls.DarkCheckBox chkApplyToParty;
        private DarkUI.Controls.DarkComboBox cmbSpell;
        private Label lblSpell;
        private DarkUI.Controls.DarkButton btnCancel;
        private DarkUI.Controls.DarkButton btnSave;
        private DarkUI.Controls.DarkGroupBox grpTargets;
        private DarkUI.Controls.DarkCheckBox chkApplyToSelf;
        private DarkUI.Controls.DarkButton darkButton1;
        private DarkUI.Controls.DarkButton darkButton2;
        private DarkUI.Controls.DarkCheckBox chkInstanceMembers;
    }
}
