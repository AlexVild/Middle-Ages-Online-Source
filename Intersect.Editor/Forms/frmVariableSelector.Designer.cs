using Microsoft.Xna.Framework;
using System.Drawing;
using System.Windows.Forms;

namespace Intersect.Editor.Forms
{
    partial class FrmVariableSelector
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVariableSelector));
            this.btnOk = new DarkUI.Controls.DarkButton();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.grpSelection = new DarkUI.Controls.DarkGroupBox();
            this.grpVariable = new DarkUI.Controls.DarkGroupBox();
            this.cmbVariables = new DarkUI.Controls.DarkComboBox();
            this.grpVariableType = new DarkUI.Controls.DarkGroupBox();
            this.cmbVariableType = new DarkUI.Controls.DarkComboBox();
            this.grpSelection.SuspendLayout();
            this.grpVariable.SuspendLayout();
            this.grpVariableType.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(77, 176);
            this.btnOk.Name = "btnOk";
            this.btnOk.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.btnOk.Size = new System.Drawing.Size(82, 23);
            this.btnOk.TabIndex = 19;
            this.btnOk.Text = "Ok";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(166, 176);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.btnCancel.Size = new System.Drawing.Size(82, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            // 
            // grpSelection
            // 
            this.grpSelection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grpSelection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpSelection.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpSelection.Controls.Add(this.grpVariable);
            this.grpSelection.Controls.Add(this.grpVariableType);
            this.grpSelection.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpSelection.Location = new System.Drawing.Point(11, 10);
            this.grpSelection.Name = "grpSelection";
            this.grpSelection.Size = new System.Drawing.Size(249, 149);
            this.grpSelection.TabIndex = 21;
            this.grpSelection.TabStop = false;
            this.grpSelection.Text = "Select a Variable";
            // 
            // grpVariable
            // 
            this.grpVariable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grpVariable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpVariable.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpVariable.Controls.Add(this.cmbVariables);
            this.grpVariable.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpVariable.Location = new System.Drawing.Point(9, 79);
            this.grpVariable.Name = "grpVariable";
            this.grpVariable.Size = new System.Drawing.Size(230, 51);
            this.grpVariable.TabIndex = 18;
            this.grpVariable.TabStop = false;
            this.grpVariable.Text = "Variable";
            // 
            // cmbVariables
            // 
            this.cmbVariables.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbVariables.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbVariables.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbVariables.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbVariables.DrawDropdownHoverOutline = false;
            this.cmbVariables.DrawFocusRectangle = false;
            this.cmbVariables.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbVariables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVariables.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbVariables.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbVariables.FormattingEnabled = true;
            this.cmbVariables.Location = new System.Drawing.Point(8, 19);
            this.cmbVariables.Name = "cmbVariables";
            this.cmbVariables.Size = new System.Drawing.Size(217, 21);
            this.cmbVariables.TabIndex = 16;
            this.cmbVariables.Text = null;
            this.cmbVariables.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // grpVariableType
            // 
            this.grpVariableType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grpVariableType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpVariableType.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpVariableType.Controls.Add(this.cmbVariableType);
            this.grpVariableType.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpVariableType.Location = new System.Drawing.Point(9, 19);
            this.grpVariableType.Name = "grpVariableType";
            this.grpVariableType.Size = new System.Drawing.Size(230, 55);
            this.grpVariableType.TabIndex = 9;
            this.grpVariableType.TabStop = false;
            this.grpVariableType.Text = "Variable Type";
            // 
            // cmbVariableType
            // 
            this.cmbVariableType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbVariableType.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbVariableType.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbVariableType.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbVariableType.DrawDropdownHoverOutline = false;
            this.cmbVariableType.DrawFocusRectangle = false;
            this.cmbVariableType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbVariableType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVariableType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbVariableType.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbVariableType.FormattingEnabled = true;
            this.cmbVariableType.Location = new System.Drawing.Point(8, 19);
            this.cmbVariableType.Name = "cmbVariableType";
            this.cmbVariableType.Size = new System.Drawing.Size(217, 21);
            this.cmbVariableType.TabIndex = 16;
            this.cmbVariableType.Text = null;
            this.cmbVariableType.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // FrmVariableSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(271, 210);
            this.Controls.Add(this.grpSelection);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmVariableSelector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Variable Selector";
            this.grpSelection.ResumeLayout(false);
            this.grpVariable.ResumeLayout(false);
            this.grpVariableType.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DarkUI.Controls.DarkButton btnOk;
        private DarkUI.Controls.DarkButton btnCancel;
        private DarkUI.Controls.DarkGroupBox grpSelection;
        private DarkUI.Controls.DarkGroupBox grpVariable;
        private DarkUI.Controls.DarkComboBox cmbVariables;
        private DarkUI.Controls.DarkGroupBox grpVariableType;
        private DarkUI.Controls.DarkComboBox cmbVariableType;
    }
}