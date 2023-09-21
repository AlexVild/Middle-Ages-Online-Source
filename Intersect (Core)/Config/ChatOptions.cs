namespace Intersect.Config
{

    public class ChatOptions
    {

        //Chat
        public int MaxChatLength = 120; //120 characters

        public int MinIntervalBetweenChats = 400; //400 ms

        /// <summary>
        /// Is the client allowed to show in-game banners for announcements made?
        /// </summary>
        public bool ShowAnnouncementBanners = true;

        /// <summary>
        /// The time (in milliseconds) the announcement banners should display, if enabled.
        /// </summary>
        public int AnnouncementDisplayDuration = 5000;

        public long MenuNotificationFlashInterval = 250;
        
        public string MenuCharacterIcon = "charactericon.png";

        public string MenuCharacterIconFlashed = "charactericon_flash.png";

        public string ChatSendSound = "al_typing_send.wav";

        public string UIDenySound = "al_gui_cancel.wav";
        
        public string LockedSound = "al_locked.wav";
        
        public string GiveItemSound = "al_cloth_heavy.wav";

        public string BankSortSound = "";
    }

}
