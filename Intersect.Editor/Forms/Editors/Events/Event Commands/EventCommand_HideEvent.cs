using Intersect.Editor.Localization;
using Intersect.GameObjects.Events;
using Intersect.GameObjects.Events.Commands;
using Intersect.GameObjects.Maps;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    public partial class EventCommand_HideEvent : UserControl
    {
        private FrmEvent mEventEditor;

        private HideEventCommand mMyCommand;

        private EventBase mEditingEvent;

        private MapBase mCurrentMap;

        public EventCommand_HideEvent(HideEventCommand refCommand,
            FrmEvent editor, 
            MapBase currentMap,
            EventBase currentEvent
        )
        {
            mMyCommand = refCommand;
            mEventEditor = editor;
            mEditingEvent = currentEvent;
            mCurrentMap = currentMap;

            InitializeComponent();

            cmbTarget.Items.Clear();
            if (!mEditingEvent.CommonEvent)
            {
                foreach (var evt in mCurrentMap.LocalEvents)
                {
                    cmbTarget.Items.Add(
                        evt.Key == mEditingEvent.Id ? Strings.EventMoveRoute.thisevent.ToString() : "" + evt.Value.Name
                    );

                    if (mMyCommand.EventId == evt.Key)
                    {
                        cmbTarget.SelectedIndex = cmbTarget.Items.Count - 1;
                    }
                }
            }

            if (cmbTarget.SelectedIndex == -1 && cmbTarget.Items.Count > 0)
            {
                cmbTarget.SelectedIndex = 0;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            mMyCommand.EventId = mCurrentMap.LocalEvents.Keys.ToList()[cmbTarget.SelectedIndex];
            mEventEditor.FinishCommandEdit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mEventEditor.CancelCommandEdit();
        }
    }
}
