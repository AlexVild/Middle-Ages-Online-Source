using Intersect.Client.General;
using Intersect.Configuration;
using Intersect.Enums;
using Intersect.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Intersect.Client.Interface.Game.Chat
{
    public class SizedQueue<T>: Queue<T>
    {
        public int MaxSize = 100;

        public SizedQueue(int maxSize): base()
        {
            MaxSize = maxSize;
        }

        public void EnqueueAndDequeue(T item)
        {
            if (Count >= MaxSize)
            {
                Dequeue();
            }
            
            Enqueue(item);
        }
    }


    public class ChatboxMsg
    {
        private static SizedQueue<ChatboxMsg> AllMessages = new SizedQueue<ChatboxMsg>(ClientConfiguration.Instance.ChatLines);
        private static SizedQueue<ChatboxMsg> LocalMessages = new SizedQueue<ChatboxMsg>(ClientConfiguration.Instance.ChatLines);
        private static SizedQueue<ChatboxMsg> PartyMessages = new SizedQueue<ChatboxMsg>(ClientConfiguration.Instance.ChatLines);
        private static SizedQueue<ChatboxMsg> GlobalMessages = new SizedQueue<ChatboxMsg>(ClientConfiguration.Instance.ChatLines);
        private static SizedQueue<ChatboxMsg> GuildMessages = new SizedQueue<ChatboxMsg>(ClientConfiguration.Instance.ChatLines);
        private static SizedQueue<ChatboxMsg> SystemMessages = new SizedQueue<ChatboxMsg>(ClientConfiguration.Instance.ChatLines);

        public static bool NewMessage = false;

        // TODO: Move to a configuration file to make this player configurable?
        /// <summary>
        /// Contains the configuration of which message types to display in each chat tab.
        /// </summary>
        private static Dictionary<ChatboxTab, ChatMessageType[]> sTabMessageTypes = new Dictionary<ChatboxTab, ChatMessageType[]>() {
            // All has ALL tabs unlocked, so really we don't have to worry about that one.
            { ChatboxTab.Local, new ChatMessageType[] { ChatMessageType.Local, ChatMessageType.PM, ChatMessageType.Admin } },
            { ChatboxTab.Party, new ChatMessageType[] { ChatMessageType.Party, ChatMessageType.PM, ChatMessageType.Admin } },
            { ChatboxTab.Global, new ChatMessageType[] { ChatMessageType.Global, ChatMessageType.PM, ChatMessageType.Admin } },
            { ChatboxTab.Guild, new ChatMessageType[] { ChatMessageType.Guild, ChatMessageType.PM, ChatMessageType.Admin } },
            { ChatboxTab.System, new ChatMessageType[] { 
                ChatMessageType.Experience, ChatMessageType.Loot, ChatMessageType.Inventory, ChatMessageType.Bank, 
                ChatMessageType.Combat, ChatMessageType.Quest, ChatMessageType.Crafting, ChatMessageType.Trading, 
                ChatMessageType.Friend, ChatMessageType.Spells, ChatMessageType.Notice, ChatMessageType.Error,
                ChatMessageType.Admin } },
        };

        private string mMsg = "";

        private Color mMsgColor;

        private string mTarget = "";

        private ChatMessageType mType;

        /// <summary>
        /// Creates a new instance of the <see cref="ChatboxMsg"/> class.
        /// </summary>
        /// <param name="msg">The message to add.</param>
        /// <param name="clr">The color of the message.</param>
        /// <param name="type">The type of the message.</param>
        /// <param name="target">The target of the message.</param>
        public ChatboxMsg(string msg, Color clr, ChatMessageType type, string target = "")
        {
            mMsg = msg;
            mMsgColor = clr;
            mTarget = target;
            mType = type;
        }

        /// <summary>
        /// The contents of this message.
        /// </summary>
        public string Message => mMsg;

        /// <summary>
        /// The color of this message.
        /// </summary>
        public Color Color => mMsgColor;

        // The target of this message.
        public string Target => mTarget;

        /// <summary>
        /// The type of this message.
        /// </summary>
        public ChatMessageType Type => mType;

        /// <summary>
        /// Adds a new chat message to the stored list.
        /// </summary>
        /// <param name="msg">The message to add.</param>
        public static void AddMessage(ChatboxMsg msg)
        {
            var toTab = ChatboxTab.All;
            foreach (var tab in sTabMessageTypes)
            {
                if (!tab.Value.Contains(msg.Type))
                {
                    continue;
                }

                toTab = tab.Key;
            }

            AllMessages.EnqueueAndDequeue(msg);
            
            switch (toTab)
            {
                case ChatboxTab.Local:
                    LocalMessages.EnqueueAndDequeue(msg);
                    break;
                case ChatboxTab.Party:
                    PartyMessages.EnqueueAndDequeue(msg);
                    break;
                case ChatboxTab.System:
                    SystemMessages.EnqueueAndDequeue(msg);
                    break;
                case ChatboxTab.Guild:
                    GuildMessages.EnqueueAndDequeue(msg);
                    break;
                case ChatboxTab.Global:
                    GlobalMessages.EnqueueAndDequeue(msg);
                    break;
            }
            
            NewMessage = true;
        }

        public static void DebugMessage(string message)
        {
#if DEBUG
            var msg = new ChatboxMsg($"DEBUG: {message}", Color.Gray, ChatMessageType.Admin);
            AddMessage(msg);
#else
            Logging.Log.Debug($"DEBUG: {message}");
#endif
        }

        /// <summary>
        /// Retrieves all messages that should be displayed in the provided tab.
        /// </summary>
        /// <param name="tab">The tab for which to retrieve all messages.</param>
        /// <returns>Returns a list of chat messages.</returns>
        public static ChatboxMsg[] GetMessages(ChatboxTab tab)
        {
            switch (tab)
            {
                case ChatboxTab.Local:
                    return LocalMessages.ToArray();
                case ChatboxTab.Guild:
                    return GuildMessages.ToArray();
                case ChatboxTab.Party:
                    return PartyMessages.ToArray();
                case ChatboxTab.Global:
                    return GlobalMessages.ToArray();
                case ChatboxTab.System:
                    return SystemMessages.ToArray();
                default:
                    return AllMessages.ToArray();
            }           
        }

        /// <summary>
        /// Clears all stored messages.
        /// </summary>
        public static void ClearMessages()
        {
            LocalMessages.Clear();
            GuildMessages.Clear();
            PartyMessages.Clear();
            GlobalMessages.Clear();
            SystemMessages.Clear();
            AllMessages.Clear();
        }
    }

}
