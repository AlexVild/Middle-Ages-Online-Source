using DarkUI.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Intersect.Editor.Extensions
{
    public static class MessageBoxExtensions
    {
        public static MessageBoxButtons ToMessageBoxButton(DarkDialogButton button)
        {
            switch (button)
            {
                case DarkDialogButton.Ok:
                    return MessageBoxButtons.OK;
                case DarkDialogButton.OkCancel:
                    return MessageBoxButtons.OKCancel;
                case DarkDialogButton.YesNo:
                    return MessageBoxButtons.YesNo;
                case DarkDialogButton.YesNoCancel:
                    return MessageBoxButtons.YesNoCancel;
                case DarkDialogButton.AbortRetryIgnore:
                    return MessageBoxButtons.AbortRetryIgnore;
                case DarkDialogButton.RetryCancel:
                    return MessageBoxButtons.RetryCancel;
                default:
                    throw new ArgumentException($"Invalid argument: {nameof(button)}");
            }
        }

        public static DialogResult ShowInformation(string message, string caption, DarkDialogButton button, Icon icon)
        {
            return MessageBox.Show(message, caption, ToMessageBoxButton(button), MessageBoxIcon.Information);
        }

        public static DialogResult ShowError(string message, string caption, DarkDialogButton button, Icon icon)
        {
            return MessageBox.Show(message, caption, ToMessageBoxButton(button), MessageBoxIcon.Error);
        }

        public static DialogResult ShowWarning(string message, string caption, DarkDialogButton button, Icon icon)
        {
            return MessageBox.Show(message, caption, ToMessageBoxButton(button), MessageBoxIcon.Warning);
        }
    }
}
