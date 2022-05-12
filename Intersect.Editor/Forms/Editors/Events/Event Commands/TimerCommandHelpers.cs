using Intersect.Editor.Localization;
using Intersect.GameObjects.Events.Commands;
using Intersect.GameObjects.Timers;
using System;
using System.Linq;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    public static class TimerCommandHelpers
    {
        /// <summary>
        /// Populates timer type and timer fields according to the values required by the <see cref="TimerCommand"/> coming in.
        /// </summary>
        /// <param name="command">The command that is being edited/created</param>
        /// <param name="timerOwnerBox">The combo box that contains timer owner filters</param>
        /// <param name="timerBox">The combo box that contains timer descriptor names</param>
        public static void InitializeSelectionFields(TimerCommand command, ref DarkUI.Controls.DarkComboBox timerOwnerBox, ref DarkUI.Controls.DarkComboBox timerBox)
        {
            _ = command ?? throw new ArgumentNullException(nameof(command));
            _ = timerOwnerBox ?? throw new ArgumentNullException(nameof(timerOwnerBox));
            _ = timerBox ?? throw new ArgumentNullException(nameof(timerBox));

            TimerDescriptor descriptor = TimerDescriptor.Get(command.DescriptorId);
            if (descriptor == default) // no selection prior
            {
                InitializeTimerOwnerSelector(ref timerOwnerBox, TimerOwnerType.Global);
                RefreshTimerSelector(ref timerBox, TimerOwnerType.Global);
                return;
            }
            // Otherwise load previous selection
            InitializeTimerOwnerSelector(ref timerOwnerBox, descriptor.OwnerType);
            RefreshTimerSelector(ref timerBox, descriptor.OwnerType);
            var index = TimerDescriptor.ListIndex(descriptor.Id, descriptor.OwnerType);
            if (index < timerBox.Items.Count)
            {
                timerBox.SelectedIndex = index;
            }
        }

        /// <summary>
        /// Refreshes a combobox of timer descriptors based on the requested <see cref="TimerOwnerType"/>
        /// </summary>
        /// <param name="timerBox">The combobox to be popualted with timer descriptor names</param>
        /// <param name="ownerType">The <see cref="TimerOwnerType"/> to filter the <see cref="TimerDescriptor"/>s by</param>
        public static void RefreshTimerSelector(ref DarkUI.Controls.DarkComboBox timerBox, TimerOwnerType ownerType)
        {
            _ = timerBox ?? throw new ArgumentNullException(nameof(timerBox));

            timerBox.Items.Clear();

            var validTimers = TimerDescriptor.Lookup
                .OrderBy(p => p.Value?.Name).ToList()
                .Where(kv => ((TimerDescriptor)kv.Value).OwnerType == ownerType)
                .Select(kv => ((TimerDescriptor)kv.Value).Name)
                .ToArray();

            timerBox.Items.AddRange(validTimers);
            if (timerBox.Items.Count > 0)
            {
                timerBox.SelectedIndex = 0;
            }
        }

        private static void InitializeTimerOwnerSelector(ref DarkUI.Controls.DarkComboBox timerOwnerBox, TimerOwnerType ownerType)
        {
            _ = timerOwnerBox ?? throw new ArgumentNullException(nameof(timerOwnerBox));

            timerOwnerBox.Items.Clear();

            timerOwnerBox.Items.AddRange(Strings.TimerEditor.OwnerTypes.Values.ToArray());
            if (timerOwnerBox.Items.Count <= 0)
            {
                return;
            }
            timerOwnerBox.SelectedIndex = (int)ownerType;
        }
    }
}
