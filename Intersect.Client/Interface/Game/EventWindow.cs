using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intersect.Client.Interface.Game
{

    public class EventResponse
    {
        public string Response { get; set; }
        public byte Index { get; set; }
    }

    public class EventWindow : Base
    {

        private ScrollControl mEventDialogArea;

        private ScrollControl mEventDialogAreaNoFace;

        private RichLabel mEventDialogLabel;

        private RichLabel mEventDialogLabelNoFace;

        private Label mEventDialogLabelNoFaceTemplate;

        private Label mEventDialogLabelTemplate;

        //Window Controls
        private ImagePanel mEventDialogWindow;

        private ImagePanel mEventFace;

        private Button mEventResponse1;

        private Button mEventResponse2;

        private Button mEventResponse3;

        private Button mEventResponse4;

        private Label mSpeakerLabel;

        private List<EventResponse> Responses;

        //Init
        public EventWindow(Canvas gameCanvas)
        {
            //Event Dialog Window
            mEventDialogWindow = new ImagePanel(gameCanvas, "EventDialogueWindow");
            mEventDialogWindow.Hide();
            Interface.InputBlockingElements.Add(mEventDialogWindow);

            mEventFace = new ImagePanel(mEventDialogWindow, "EventFacePanel");

            mEventDialogArea = new ScrollControl(mEventDialogWindow, "EventDialogArea");
            mEventDialogLabelTemplate = new Label(mEventDialogArea, "EventDialogLabel");
            mEventDialogLabel = new RichLabel(mEventDialogArea);

            mEventDialogAreaNoFace = new ScrollControl(mEventDialogWindow, "EventDialogAreaNoFace");
            mEventDialogLabelNoFaceTemplate = new Label(mEventDialogAreaNoFace, "EventDialogLabel");
            mEventDialogLabelNoFace = new RichLabel(mEventDialogAreaNoFace);

            mSpeakerLabel = new Label(mEventDialogWindow, "SpeakerLabel");

            mEventResponse1 = new Button(mEventDialogWindow, "EventResponse1");
            mEventResponse1.Clicked += EventResponse1_Clicked;

            mEventResponse2 = new Button(mEventDialogWindow, "EventResponse2");
            mEventResponse2.Clicked += EventResponse2_Clicked;

            mEventResponse3 = new Button(mEventDialogWindow, "EventResponse3");
            mEventResponse3.Clicked += EventResponse3_Clicked;

            mEventResponse4 = new Button(mEventDialogWindow, "EventResponse4");
            mEventResponse4.Clicked += EventResponse4_Clicked;

            Responses = new List<EventResponse>();
        }

        public void SetResponses(string response1, string response2, string response3, string response4)
        {
            var responses = new List<string> {
                            response1, response2,
                            response3, response4
                        };

            Responses.Clear();
            byte idx = 1;
            foreach (var response in responses)
            {
                var eventResponse = new EventResponse();
                eventResponse.Response = response;
                eventResponse.Index = idx;
                Responses.Add(eventResponse);
                idx++;
            }

            Responses = Responses.Where(response => !string.IsNullOrEmpty(response.Response)).ToList();
        }

        //Update
        public void Update()
        {
            if (mEventDialogWindow.IsHidden)
            {
                Interface.InputBlockingElements.Remove(this);
            }
            else
            {
                if (!Interface.InputBlockingElements.Contains(this))
                {
                    Interface.InputBlockingElements.Add(this);
                }
            }

            if (Globals.EventDialogs.Count > 0)
            {
                if (mEventDialogWindow.IsHidden)
                {
                    base.Show();
                    mEventDialogWindow.Show();
                    mEventDialogWindow.MakeModal();
                    mEventDialogArea.ScrollToTop();
                    mEventDialogWindow.BringToFront();
                    var faceTex = Globals.ContentManager.GetTexture(
                        GameContentManager.TextureType.Face, Globals.EventDialogs[0].Face
                    );
                    if (faceTex == null)
                    {
                        // Check for item texture if we can't find a face
                        faceTex = Globals.ContentManager.GetTexture(
                            GameContentManager.TextureType.Item, Globals.EventDialogs[0].Face
                        );
                    }

                    var responseCount = 0;
                    var maxResponse = 1;

                    responseCount = Responses.Count;
                    maxResponse = responseCount;

                    mEventResponse1.Name = "";
                    mEventResponse2.Name = "";
                    mEventResponse3.Name = "";
                    mEventResponse4.Name = "";

                    // Determine whether or not this event is a "dialog", with a speaker, to display the event differently.
                    bool hasSpeaker = false;
                    var prompt = Globals.EventDialogs[0].Prompt;
                    var splitPrompt = prompt.Split(new string[] { ":\r\n" }, StringSplitOptions.None).ToList();
                    if (splitPrompt.Count > 1 && splitPrompt[0].Split(' ').Length <= 4 && splitPrompt[0][0] != '"')
                    {
                        hasSpeaker = true;
                        mSpeakerLabel.Show();
                        mSpeakerLabel.Text = $"{splitPrompt[0].ToUpper()}:";
                        prompt = string.Concat(splitPrompt.Skip(1));
                    }
                    else
                    {
                        mSpeakerLabel.Hide();
                    }

                    switch (maxResponse)
                    {
                        case 1:
                            if (hasSpeaker)
                            {
                                mEventDialogWindow.Name = "EventDialogWindow_1ResponseSpeaker";
                            }
                            else
                            {
                                mEventDialogWindow.Name = "EventDialogWindow_1Response";
                            }
                            
                            mEventResponse1.Name = "Response1Button";

                            break;
                        case 2:
                            if (hasSpeaker)
                            {
                                mEventDialogWindow.Name = "EventDialogWindow_2ResponsesSpeaker";
                            }
                            else
                            {
                                mEventDialogWindow.Name = "EventDialogWindow_2Responses";
                            }
                            
                            mEventResponse1.Name = "Response1Button";
                            mEventResponse2.Name = "Response2Button";

                            break;
                        case 3:
                            if (hasSpeaker)
                            {
                                mEventDialogWindow.Name = "EventDialogWindow_3ResponsesSpeaker";
                            }
                            else
                            {
                                mEventDialogWindow.Name = "EventDialogWindow_3Responses";
                            }
                            
                            mEventResponse1.Name = "Response1Button";
                            mEventResponse2.Name = "Response2Button";
                            mEventResponse3.Name = "Response3Button";

                            break;
                        case 4:
                            if (hasSpeaker)
                            {
                                mEventDialogWindow.Name = "EventDialogWindow_4ResponsesSpeaker";
                            }
                            else
                            {
                                mEventDialogWindow.Name = "EventDialogWindow_4Responses";
                            }
                            
                            mEventResponse1.Name = "Response1Button";
                            mEventResponse2.Name = "Response2Button";
                            mEventResponse3.Name = "Response3Button";
                            mEventResponse4.Name = "Response4Button";

                            break;
                    }

                    mEventDialogWindow.LoadJsonUi(
                        GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString()
                    );

                    if (faceTex != null)
                    {
                        mEventFace.Show();
                        mEventFace.Texture = faceTex;
                        mEventDialogArea.Show();
                        mEventDialogAreaNoFace.Hide();
                    }
                    else
                    {
                        mEventFace.Hide();
                        mEventDialogArea.Hide();
                        mEventDialogAreaNoFace.Show();
                    }

                    if (responseCount == 0)
                    {
                        mEventResponse1.Show();
                        mEventResponse1.SetText(Strings.EventWindow.Continue);
                        mEventResponse2.Hide();
                        mEventResponse3.Hide();
                        mEventResponse4.Hide();
                    }
                    else
                    {
                        if (Responses.Count >= 1 && !string.IsNullOrEmpty(Responses[0].Response))
                        {
                            mEventResponse1.Show();
                            mEventResponse1.SetText(Responses[0].Response);
                            mEventResponse1.UserData = Responses[0].Index;
                        }
                        else
                        {
                            mEventResponse1.Hide();
                        }

                        if (Responses.Count >= 2 && !string.IsNullOrEmpty(Responses[1].Response))
                        {
                            mEventResponse2.Show();
                            mEventResponse2.SetText(Responses[1].Response);
                            mEventResponse2.UserData = Responses[1].Index;
                        }
                        else
                        {
                            mEventResponse2.Hide();
                        }

                        if (Responses.Count >= 3 && !string.IsNullOrEmpty(Responses[2].Response))
                        {
                            mEventResponse3.Show();
                            mEventResponse3.SetText(Responses[2].Response);
                            mEventResponse3.UserData = Responses[2].Index;
                        }
                        else
                        {
                            mEventResponse3.Hide();
                        }

                        if (Responses.Count >= 4 && !string.IsNullOrEmpty(Responses[3].Response))
                        {
                            mEventResponse4.Show();
                            mEventResponse4.SetText(Responses[3].Response);
                            mEventResponse4.UserData = Responses[3].Index;
                        }
                        else
                        {
                            mEventResponse4.Hide();
                        }
                    }

                    mEventDialogWindow.SetSize(
                        mEventDialogWindow.Texture.GetWidth(), mEventDialogWindow.Texture.GetHeight()
                    );

                    if (faceTex != null)
                    {
                        mEventDialogLabel.ClearText();
                        mEventDialogLabel.Width = mEventDialogArea.Width -
                                                  mEventDialogArea.GetVerticalScrollBar().Width;

                        mEventDialogLabel.AddText(
                            prompt, mEventDialogLabelTemplate.TextColor,
                            mEventDialogLabelTemplate.CurAlignments.Count > 0
                                ? mEventDialogLabelTemplate.CurAlignments[0]
                                : Alignments.Left, mEventDialogLabelTemplate.Font
                        );

                        mEventDialogLabel.SizeToChildren(false, true);
                        mEventDialogArea.ScrollToTop();
                    }
                    else
                    {
                        mEventDialogLabelNoFace.ClearText();
                        mEventDialogLabelNoFace.Width = mEventDialogAreaNoFace.Width -
                                                        mEventDialogAreaNoFace.GetVerticalScrollBar().Width;

                        mEventDialogLabelNoFace.AddText(
                            prompt, mEventDialogLabelNoFaceTemplate.TextColor,
                            mEventDialogLabelNoFaceTemplate.CurAlignments.Count > 0
                                ? mEventDialogLabelNoFaceTemplate.CurAlignments[0]
                                : Alignments.Left, mEventDialogLabelNoFaceTemplate.Font
                        );

                        mEventDialogLabelNoFace.SizeToChildren(false, true);
                        mEventDialogAreaNoFace.ScrollToTop();
                    }
                }
            }
        }

        //Input Handlers
        void EventResponse4_Clicked(Base sender, ClickedEventArgs arguments)
        {
            var ed = Globals.EventDialogs[0];
            if (ed.ResponseSent != 0)
            {
                return;
            }

            PacketSender.SendEventResponse(Responses[3].Index, ed);
            mEventDialogWindow.RemoveModal();
            mEventDialogWindow.IsHidden = true;
            ed.ResponseSent = 1;
            base.Hide();
        }

        void EventResponse3_Clicked(Base sender, ClickedEventArgs arguments)
        {
            var ed = Globals.EventDialogs[0];
            if (ed.ResponseSent != 0)
            {
                return;
            }

            PacketSender.SendEventResponse(Responses[2].Index, ed);
            mEventDialogWindow.RemoveModal();
            mEventDialogWindow.IsHidden = true;
            ed.ResponseSent = 1;
            base.Hide();
        }

        void EventResponse2_Clicked(Base sender, ClickedEventArgs arguments)
        {
            var ed = Globals.EventDialogs[0];
            if (ed.ResponseSent != 0)
            {
                return;
            }

            PacketSender.SendEventResponse(Responses[1].Index, ed);
            mEventDialogWindow.RemoveModal();
            mEventDialogWindow.IsHidden = true;
            ed.ResponseSent = 1;
            base.Hide();
        }

        public void EventResponse1_Clicked(Base sender, ClickedEventArgs arguments)
        {
            var ed = Globals.EventDialogs[0];
            if (ed.ResponseSent != 0)
            {
                return;
            }

            if (Responses.Count > 0)
            {
                PacketSender.SendEventResponse(Responses[0].Index, ed);
            }
            else
            {
                PacketSender.SendEventResponse(0, ed);
            }
            mEventDialogWindow.RemoveModal();
            mEventDialogWindow.IsHidden = true;
            ed.ResponseSent = 1;
            base.Hide();
        }
    }
}
