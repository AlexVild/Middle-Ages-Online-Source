namespace Intersect.Editor.Forms
{
    partial class frmMapCopier
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMapCopier));
            this.grpCopier = new DarkUI.Controls.DarkGroupBox();
            this.lblMap = new System.Windows.Forms.Label();
            this.cmbMaps = new DarkUI.Controls.DarkComboBox();
            this.btnApply = new DarkUI.Controls.DarkButton();
            this.btnClose = new DarkUI.Controls.DarkButton();
            this.grpCopier.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpCopier
            // 
            this.grpCopier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpCopier.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpCopier.Controls.Add(this.lblMap);
            this.grpCopier.Controls.Add(this.cmbMaps);
            this.grpCopier.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpCopier.Location = new System.Drawing.Point(12, 3);
            this.grpCopier.Name = "grpCopier";
            this.grpCopier.Size = new System.Drawing.Size(352, 47);
            this.grpCopier.TabIndex = 127;
            this.grpCopier.TabStop = false;
            this.grpCopier.Text = "Properties";
            // 
            // lblMap
            // 
            this.lblMap.AutoSize = true;
            this.lblMap.Location = new System.Drawing.Point(6, 16);
            this.lblMap.Name = "lblMap";
            this.lblMap.Size = new System.Drawing.Size(67, 13);
            this.lblMap.TabIndex = 125;
            this.lblMap.Text = "Map to Copy";
            // 
            // cmbMaps
            // 
            this.cmbMaps.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbMaps.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbMaps.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbMaps.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbMaps.DrawDropdownHoverOutline = false;
            this.cmbMaps.DrawFocusRectangle = false;
            this.cmbMaps.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbMaps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMaps.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbMaps.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbMaps.FormattingEnabled = true;
            this.cmbMaps.Location = new System.Drawing.Point(92, 13);
            this.cmbMaps.Name = "cmbMaps";
            this.cmbMaps.Size = new System.Drawing.Size(254, 21);
            this.cmbMaps.TabIndex = 124;
            this.cmbMaps.Text = null;
            this.cmbMaps.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(136, 58);
            this.btnApply.Name = "btnApply";
            this.btnApply.Padding = new System.Windows.Forms.Padding(5);
            this.btnApply.Size = new System.Drawing.Size(111, 27);
            this.btnApply.TabIndex = 128;
            this.btnApply.Text = "Apply";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(253, 58);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(5);
            this.btnClose.Size = new System.Drawing.Size(111, 27);
            this.btnClose.TabIndex = 129;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmMapCopier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(379, 97);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.grpCopier);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMapCopier";
            this.Text = "frmMapCopier";
            this.grpCopier.ResumeLayout(false);
            this.grpCopier.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkGroupBox grpCopier;
        private System.Windows.Forms.Label lblMap;
        private DarkUI.Controls.DarkComboBox cmbMaps;
        private DarkUI.Controls.DarkButton btnApply;
        private DarkUI.Controls.DarkButton btnClose;
    }
}