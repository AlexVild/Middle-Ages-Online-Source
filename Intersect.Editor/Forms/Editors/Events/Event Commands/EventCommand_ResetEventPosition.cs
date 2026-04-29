using Intersect.Editor.Localization;
using Intersect.GameObjects.Events;
using Intersect.GameObjects.Events.Commands;
using Intersect.GameObjects.Maps;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    public partial class EventCommand_ResetEventPosition : UserControl
    {
        private readonly EventBase mEditingEvent;

        private MapBase mCurrentMap;

        private ResetEventPositionCommand mEditingCommand;

        private FrmEvent mEventEditor;

        public EventCommand_ResetEventPosition(
            ResetEventPositionCommand refCommand,
            FrmEvent eventEditor,
            MapBase currentMap,
            EventBase currentEvent
        )
        {
            InitializeComponent();
            mEventEditor = eventEditor;
            mEditingEvent = currentEvent;
            mEditingCommand = refCommand;
            mCurrentMap = currentMap;

            cmbEntities.Items.Clear();

            if (!mEditingEvent.CommonEvent)
            {
                foreach (var evt in mCurrentMap.LocalEvents)
                {
                    cmbEntities.Items.Add(
                        evt.Key == mEditingEvent.Id
                            ? Strings.EventWaitForRouteCompletion.This + " "
                            : "" + evt.Value.Name
                    );

                    if (mEditingCommand.Target == evt.Key)
                    {
                        cmbEntities.SelectedIndex = cmbEntities.Items.Count - 1;
                    }
                }
            }

            if (cmbEntities.SelectedIndex == -1 && cmbEntities.Items.Count > 0)
            {
                cmbEntities.SelectedIndex = 0;
            }

            mEditingCommand = refCommand;
            mEventEditor = eventEditor;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!mEditingEvent.CommonEvent)
            {
                mEditingCommand.Target = mCurrentMap.LocalEvents.Keys.ToList()[cmbEntities.SelectedIndex];
            }

            mEventEditor.FinishCommandEdit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mEventEditor.CancelCommandEdit();
        }
    }
}
