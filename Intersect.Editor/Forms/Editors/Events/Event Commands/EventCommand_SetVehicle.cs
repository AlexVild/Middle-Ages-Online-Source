using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using Intersect.Editor.Localization;
using Intersect.Editor.Content;
using Intersect.GameObjects;
using Intersect.GameObjects.Events.Commands;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    public partial class EventCommand_SetVehicle : UserControl
    {
        private readonly FrmEvent mEventEditor;

        private SetVehicleCommand mMyCommand;

        public EventCommand_SetVehicle(SetVehicleCommand refCommand, FrmEvent editor)
        {
            InitializeComponent();

            mMyCommand = refCommand;
            mEventEditor = editor;

            chkInVehicle.Checked = refCommand.InVehicle;
            InitSpriteSelection();

            InitLocalization();
            UpdatePriveleges();
            
        }

        private void InitLocalization()
        {
            grpSetVehicle.Text = Strings.EventSetVehicle.title;
            lblSprite.Text = Strings.EventSetVehicle.spritelabel;
            lblSpeed.Text = Strings.EventSetVehicle.speedlabel;
            chkInVehicle.Text = Strings.EventSetVehicle.checkbox;
            btnSave.Text = Strings.EventSetVehicle.confirm;
            btnCancel.Text = Strings.EventSetVehicle.cancel;
        }

        private void InitSpriteSelection()
        {
            cmbSprites.Items.Clear();
            cmbSprites.Items.Add(Strings.General.none);
            cmbSprites.Items.AddRange(
                GameContentManager.GetSmartSortedTextureNames(GameContentManager.TextureType.Entity)
            );

            if (mMyCommand.VehicleSprite != null && cmbSprites.Items.IndexOf(mMyCommand.VehicleSprite) > 0)
            {
                cmbSprites.SelectedIndex = cmbSprites.Items.IndexOf(mMyCommand.VehicleSprite) - 1;
            }
            else
            {
                cmbSprites.SelectedIndex = 0;
            }

            UpdatePreview();
        }

        private void UpdatePriveleges()
        {
            cmbSprites.Enabled = chkInVehicle.Checked;
            nudSpeed.Enabled = chkInVehicle.Checked;
        }

        private void UpdatePreview()
        {
            if (cmbSprites.SelectedIndex == 0)
            {
                pnlPreview.BackgroundImage = null;
            }

            var destBitmap = new Bitmap(pnlPreview.Width, pnlPreview.Height);
            var g = Graphics.FromImage(destBitmap);
            g.Clear(System.Drawing.Color.Black);
            if (File.Exists("resources/entities/" + cmbSprites.Text))
            {
                var sourceBitmap = new Bitmap("resources/entities/" + cmbSprites.Text);
                g.DrawImage(
                    sourceBitmap,
                    new Rectangle(
                        pnlPreview.Width / 2 - sourceBitmap.Width / (Options.Instance.Sprites.NormalFrames * 2), pnlPreview.Height / 2 - sourceBitmap.Height / (Options.Instance.Sprites.Directions * 2),
                        sourceBitmap.Width / Options.Instance.Sprites.NormalFrames, sourceBitmap.Height / Options.Instance.Sprites.Directions
                    ), new Rectangle(0, 0, sourceBitmap.Width / Options.Instance.Sprites.NormalFrames, sourceBitmap.Height / Options.Instance.Sprites.Directions), GraphicsUnit.Pixel
                );
            }

            g.Dispose();
            pnlPreview.BackgroundImage = destBitmap;
        }

        private void chkInVehicle_CheckedChanged(object sender, EventArgs e)
        {
            mMyCommand.InVehicle = chkInVehicle.Checked;
            UpdatePriveleges();
        }

        private void cmbSprites_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePreview();
        }

        private void nudSpeed_ValueChanged(object sender, EventArgs e)
        {
            mMyCommand.VehicleSpeed = (long) nudSpeed.Value;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            mMyCommand.VehicleSprite = cmbSprites.Text;
            mEventEditor.FinishCommandEdit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mEventEditor.CancelCommandEdit();
        }
    }
}
