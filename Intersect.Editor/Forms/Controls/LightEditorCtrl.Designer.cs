using System;
using DarkUI.Controls;

namespace Intersect.Editor.Forms.Controls
{
    partial class LightEditorCtrl
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
            this.pnlLight = new System.Windows.Forms.Panel();
            this.grpLightEditor = new DarkUI.Controls.DarkGroupBox();
            this.nudOffsetY = new DarkUI.Controls.DarkNumericUpDown();
            this.nudOffsetX = new DarkUI.Controls.DarkNumericUpDown();
            this.nudIntensity = new DarkUI.Controls.DarkNumericUpDown();
            this.nudSize = new DarkUI.Controls.DarkNumericUpDown();
            this.nudExpand = new DarkUI.Controls.DarkNumericUpDown();
            this.lblExpandAmt = new System.Windows.Forms.Label();
            this.pnlLightColor = new System.Windows.Forms.Panel();
            this.btnOkay = new DarkUI.Controls.DarkButton();
            this.lblColor = new System.Windows.Forms.Label();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.btnSelectLightColor = new DarkUI.Controls.DarkButton();
            this.lblOffsetX = new System.Windows.Forms.Label();
            this.lblOffsetY = new System.Windows.Forms.Label();
            this.lblSize = new System.Windows.Forms.Label();
            this.lblIntensity = new System.Windows.Forms.Label();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.pnlLight.SuspendLayout();
            this.grpLightEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudOffsetY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOffsetX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudIntensity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudExpand)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlLight
            // 
            this.pnlLight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.pnlLight.Controls.Add(this.grpLightEditor);
            this.pnlLight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLight.Location = new System.Drawing.Point(0, 0);
            this.pnlLight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlLight.Name = "pnlLight";
            this.pnlLight.Size = new System.Drawing.Size(357, 445);
            this.pnlLight.TabIndex = 4;
            // 
            // grpLightEditor
            // 
            this.grpLightEditor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpLightEditor.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpLightEditor.Controls.Add(this.nudOffsetY);
            this.grpLightEditor.Controls.Add(this.nudOffsetX);
            this.grpLightEditor.Controls.Add(this.nudIntensity);
            this.grpLightEditor.Controls.Add(this.nudSize);
            this.grpLightEditor.Controls.Add(this.nudExpand);
            this.grpLightEditor.Controls.Add(this.lblExpandAmt);
            this.grpLightEditor.Controls.Add(this.pnlLightColor);
            this.grpLightEditor.Controls.Add(this.btnOkay);
            this.grpLightEditor.Controls.Add(this.lblColor);
            this.grpLightEditor.Controls.Add(this.btnCancel);
            this.grpLightEditor.Controls.Add(this.btnSelectLightColor);
            this.grpLightEditor.Controls.Add(this.lblOffsetX);
            this.grpLightEditor.Controls.Add(this.lblOffsetY);
            this.grpLightEditor.Controls.Add(this.lblSize);
            this.grpLightEditor.Controls.Add(this.lblIntensity);
            this.grpLightEditor.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpLightEditor.Location = new System.Drawing.Point(6, 6);
            this.grpLightEditor.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpLightEditor.Name = "grpLightEditor";
            this.grpLightEditor.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpLightEditor.Size = new System.Drawing.Size(343, 409);
            this.grpLightEditor.TabIndex = 39;
            this.grpLightEditor.TabStop = false;
            this.grpLightEditor.Text = "Light Editor";
            // 
            // nudOffsetY
            // 
            this.nudOffsetY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudOffsetY.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudOffsetY.Location = new System.Drawing.Point(14, 118);
            this.nudOffsetY.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudOffsetY.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.nudOffsetY.Minimum = new decimal(new int[] {
            128,
            0,
            0,
            -2147483648});
            this.nudOffsetY.Name = "nudOffsetY";
            this.nudOffsetY.Size = new System.Drawing.Size(180, 26);
            this.nudOffsetY.TabIndex = 45;
            this.nudOffsetY.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudOffsetY.ValueChanged += new System.EventHandler(this.nudOffsetY_ValueChanged);
            // 
            // nudOffsetX
            // 
            this.nudOffsetX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudOffsetX.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudOffsetX.Location = new System.Drawing.Point(14, 58);
            this.nudOffsetX.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudOffsetX.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.nudOffsetX.Minimum = new decimal(new int[] {
            128,
            0,
            0,
            -2147483648});
            this.nudOffsetX.Name = "nudOffsetX";
            this.nudOffsetX.Size = new System.Drawing.Size(180, 26);
            this.nudOffsetX.TabIndex = 44;
            this.nudOffsetX.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudOffsetX.ValueChanged += new System.EventHandler(this.nudOffsetX_ValueChanged);
            // 
            // nudIntensity
            // 
            this.nudIntensity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudIntensity.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudIntensity.Location = new System.Drawing.Point(14, 178);
            this.nudIntensity.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudIntensity.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudIntensity.Name = "nudIntensity";
            this.nudIntensity.Size = new System.Drawing.Size(291, 26);
            this.nudIntensity.TabIndex = 43;
            this.nudIntensity.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudIntensity.ValueChanged += new System.EventHandler(this.nudIntensity_ValueChanged);
            // 
            // nudSize
            // 
            this.nudSize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudSize.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudSize.Location = new System.Drawing.Point(14, 238);
            this.nudSize.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudSize.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudSize.Name = "nudSize";
            this.nudSize.Size = new System.Drawing.Size(291, 26);
            this.nudSize.TabIndex = 42;
            this.nudSize.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudSize.ValueChanged += new System.EventHandler(this.nudSize_ValueChanged);
            // 
            // nudExpand
            // 
            this.nudExpand.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudExpand.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudExpand.Location = new System.Drawing.Point(14, 300);
            this.nudExpand.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudExpand.Name = "nudExpand";
            this.nudExpand.Size = new System.Drawing.Size(291, 26);
            this.nudExpand.TabIndex = 41;
            this.nudExpand.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudExpand.ValueChanged += new System.EventHandler(this.nudExpand_ValueChanged);
            // 
            // lblExpandAmt
            // 
            this.lblExpandAmt.AutoSize = true;
            this.lblExpandAmt.Location = new System.Drawing.Point(9, 274);
            this.lblExpandAmt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblExpandAmt.Name = "lblExpandAmt";
            this.lblExpandAmt.Size = new System.Drawing.Size(100, 20);
            this.lblExpandAmt.TabIndex = 40;
            this.lblExpandAmt.Text = "Expand Amt:";
            // 
            // pnlLightColor
            // 
            this.pnlLightColor.BackColor = System.Drawing.Color.White;
            this.pnlLightColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLightColor.Location = new System.Drawing.Point(216, 58);
            this.pnlLightColor.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlLightColor.Name = "pnlLightColor";
            this.pnlLightColor.Size = new System.Drawing.Size(46, 44);
            this.pnlLightColor.TabIndex = 38;
            // 
            // btnOkay
            // 
            this.btnOkay.Location = new System.Drawing.Point(14, 357);
            this.btnOkay.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOkay.Name = "btnOkay";
            this.btnOkay.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.btnOkay.Size = new System.Drawing.Size(112, 35);
            this.btnOkay.TabIndex = 24;
            this.btnOkay.Text = "Save";
            this.btnOkay.Click += new System.EventHandler(this.btnLightEditorClose_Click);
            // 
            // lblColor
            // 
            this.lblColor.AutoSize = true;
            this.lblColor.Location = new System.Drawing.Point(212, 34);
            this.lblColor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(50, 20);
            this.lblColor.TabIndex = 37;
            this.lblColor.Text = "Color:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(193, 357);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.btnCancel.Size = new System.Drawing.Size(112, 35);
            this.btnCancel.TabIndex = 25;
            this.btnCancel.Text = "Revert";
            this.btnCancel.Click += new System.EventHandler(this.btnLightEditorRevert_Click);
            // 
            // btnSelectLightColor
            // 
            this.btnSelectLightColor.Location = new System.Drawing.Point(216, 115);
            this.btnSelectLightColor.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSelectLightColor.Name = "btnSelectLightColor";
            this.btnSelectLightColor.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.btnSelectLightColor.Size = new System.Drawing.Size(115, 35);
            this.btnSelectLightColor.TabIndex = 36;
            this.btnSelectLightColor.Text = "Select Color";
            this.btnSelectLightColor.Click += new System.EventHandler(this.btnSelectLightColor_Click);
            // 
            // lblOffsetX
            // 
            this.lblOffsetX.AutoSize = true;
            this.lblOffsetX.Location = new System.Drawing.Point(9, 34);
            this.lblOffsetX.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOffsetX.Name = "lblOffsetX";
            this.lblOffsetX.Size = new System.Drawing.Size(72, 20);
            this.lblOffsetX.TabIndex = 26;
            this.lblOffsetX.Text = "Offset X:";
            // 
            // lblOffsetY
            // 
            this.lblOffsetY.AutoSize = true;
            this.lblOffsetY.Location = new System.Drawing.Point(9, 94);
            this.lblOffsetY.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOffsetY.Name = "lblOffsetY";
            this.lblOffsetY.Size = new System.Drawing.Size(72, 20);
            this.lblOffsetY.TabIndex = 27;
            this.lblOffsetY.Text = "Offset Y:";
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point(9, 214);
            this.lblSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(44, 20);
            this.lblSize.TabIndex = 34;
            this.lblSize.Text = "Size:";
            // 
            // lblIntensity
            // 
            this.lblIntensity.AutoSize = true;
            this.lblIntensity.Location = new System.Drawing.Point(9, 154);
            this.lblIntensity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIntensity.Name = "lblIntensity";
            this.lblIntensity.Size = new System.Drawing.Size(73, 20);
            this.lblIntensity.TabIndex = 31;
            this.lblIntensity.Text = "Intensity:";
            // 
            // LightEditorCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlLight);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "LightEditorCtrl";
            this.Size = new System.Drawing.Size(357, 445);
            this.pnlLight.ResumeLayout(false);
            this.grpLightEditor.ResumeLayout(false);
            this.grpLightEditor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudOffsetY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOffsetX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudIntensity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudExpand)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel pnlLight;
        public System.Windows.Forms.Panel pnlLightColor;
        private System.Windows.Forms.Label lblColor;
        private DarkButton btnSelectLightColor;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label lblIntensity;
        private System.Windows.Forms.Label lblOffsetY;
        private System.Windows.Forms.Label lblOffsetX;
        private DarkButton btnCancel;
        private DarkButton btnOkay;
        private System.Windows.Forms.ColorDialog colorDialog;
        private DarkGroupBox grpLightEditor;
        private System.Windows.Forms.Label lblExpandAmt;
        private DarkNumericUpDown nudOffsetY;
        private DarkNumericUpDown nudOffsetX;
        private DarkNumericUpDown nudIntensity;
        private DarkNumericUpDown nudSize;
        private DarkNumericUpDown nudExpand;
    }
}
