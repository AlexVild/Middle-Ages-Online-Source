using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

using Intersect.Enums;
using Newtonsoft.Json;

namespace Intersect.Configuration
{

    /// <inheritdoc />
    /// <summary>
    /// Client configuration options
    /// </summary>
    public sealed partial class ClientConfiguration : IConfiguration<ClientConfiguration>
    {

        public const string DefaultPath = @"resources/config.json";

        /// <inheritdoc />
        public ClientConfiguration Load(string filePath = DefaultPath, bool failQuietly = false)
        {
            return ConfigurationHelper.Load(this, filePath, failQuietly);
        }

        /// <inheritdoc />
        public ClientConfiguration Save(string filePath = DefaultPath, bool failQuietly = false)
        {
            return ConfigurationHelper.Save(this, filePath, failQuietly);
        }

        public static ClientConfiguration LoadAndSave(string filePath = null)
        {
            return ConfigurationHelper.LoadSafely(Instance, filePath);
        }

        #region Constants

        public const string DEFAULT_HOST = "localhost";

        public const int DEFAULT_PORT = 5400;

        public const string DEFAULT_FONT = "sourcesansproblack";

        public const string DEFAULT_UI_FONT = "sourcesanspro,8";

        public const int DEFAULT_CHAT_LINES = 100;

        public const DisplayModes DEFAULT_MENU_BACKGROUND_DISPLAY_MODE = DisplayModes.Default;

        public const long DEFAULT_MENU_BACKGROUND_FRAME_INTERVAL = 50;

        public const string DEFAULT_MENU_MUSIC = "RPG-Theme_v001_Looping.ogg";

        public const string DEFAULT_TYPEWRITER_SOUND = "octave-beep-tapped.wav";

        public const int DEFAULT_TYPEWRITER_SOUND_FREQ = 5;

        public const long DEFAULT_TYPEWRITER_SPEED = 6;

        public const long DEFAULT_TYPEWRITER_FULLSTOP_SPEED = 400;

        public const long DEFAULT_TYPEWRITER_PARTIALSTOP_SPEED = 80;

        public const bool DEFAULT_TYPEWRITER_ENABLED = true;

        public const long DEFAULT_TYPEWRITER_RESPONSE_DELAY = 600;

        #endregion

        #region Static Properties and Methods

        public static ClientConfiguration Instance { get; } = new ClientConfiguration();

        public void Validate()
        {
            Host = string.IsNullOrWhiteSpace(Host) ? DEFAULT_HOST : Host.Trim();
            Port = Math.Min(Math.Max(Port, (ushort) 1), ushort.MaxValue);
            GameFont = string.IsNullOrWhiteSpace(GameFont) ? DEFAULT_FONT : GameFont.Trim();
            UIFont = string.IsNullOrWhiteSpace(UIFont) ? DEFAULT_UI_FONT : UIFont.Trim();
            ChatLines = Math.Min(Math.Max(ChatLines, 10), 500);
            MenuBackground = new List<string>(MenuBackground?.Distinct() ?? new List<string> {"background.png"});
            IntroImages = new List<string>(IntroImages?.Distinct() ?? new List<string>());
        }

        #endregion

        #region Options

        /// <summary>
        /// Hostname of the server to connect to
        /// </summary>
        public string Host { get; set; } = DEFAULT_HOST;

        /// <summary>
        /// Port of the server to connect to
        /// </summary>
        public ushort Port { get; set; } = DEFAULT_PORT;

        /// <summary>
        /// The font family to use on misc non-ui rendering
        /// </summary>
        public string GameFont { get; set; } = DEFAULT_FONT;

        /// <summary>
        /// The font family to use on entity names
        /// </summary>
        public string EntityNameFont { get; set; } = DEFAULT_FONT;

        /// <summary>
        /// The font family to use on chat bubbles
        /// </summary>
        public string ChatBubbleFont { get; set; } = DEFAULT_FONT;

        /// <summary>
        /// The font family to use on action messages
        /// </summary>
        public string ActionMsgFont { get; set; } = DEFAULT_FONT;

        /// <summary>
        /// The font family to use on unstyled windows such as the debug menu/admin window
        /// </summary>
        public string UIFont { get; set; } = DEFAULT_UI_FONT;

        /// <summary>
        /// Number of lines to save for chat scrollback
        /// </summary>
        public int ChatLines { get; set; } = DEFAULT_CHAT_LINES;

        /// <summary>
        /// Menu music file name
        /// </summary>
        public string MenuMusic { get; set; } = DEFAULT_MENU_MUSIC;

        /// <summary>
        /// Sets the main menu's background texture, if the the index of the list is bigger than 1,
        /// the background will be animated by sequentially drawing the texture files from the list.
        /// Static background Example: { "background.png" }, 
        /// Animated background Example: { "background_0.png", "background_1.png", "background_2.png" },
        /// </summary>
        public List<string> MenuBackground { get; set; } = new List<string> {"background.png"};

        /// <summary>
        /// Sets the display mode of the main menu's background.
        /// </summary>
        public DisplayModes MenuBackgroundDisplayMode { get; set; } = DEFAULT_MENU_BACKGROUND_DISPLAY_MODE;

        /// <summary>
        /// Sets the frames interval (milliseconds) of the main menu's animated background.
        /// </summary>
        public long MenuBackgroundFrameInterval { get; set; } = DEFAULT_MENU_BACKGROUND_FRAME_INTERVAL;

        /// <summary>
        /// A list from which introductory images are drawn when the game client is launched.
        /// </summary>
        public List<string> IntroImages { get; set; } = new List<string>();

        /// <summary>
        /// The address for the update manifest file generated by the editor.
        /// </summary>
        public string UpdateUrl { get; set; } = "";

        /// <summary>
        /// Sets a custom mouse cursor.
        /// </summary>
        public string MouseCursor { get; set; } = "";

        /// <summary>
        /// Determines the time it takes to fade-in or fade-out a song when no other instructions are given.
        /// </summary>
        public int MusicFadeTimer { get; set; } = 1500;

        /// <summary>
        /// Configures whether or not the context menus are enabled upon right-clicking certain elements.
        /// </summary>
        public bool EnableContextMenus { get; set; } = true;

        /// <summary>
        /// Configures whether the highlighted even rows of list elements should be marked differently or not.
        /// </summary>
        public bool EnableZebraStripedRows { get; set; } = true;

        /// <summary>
        /// Configures the name of the skin or skin texture (must end in .png) to use.
        /// </summary>
        public string UiSkin { get; set; } = "Intersect2021";

        /// <summary>
        /// Configures the rendering direction of entity bars, vitals in their order, then experience
        /// </summary>
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public List<DisplayDirection> EntityBarDirections { get; set; } =
            Enumerable.Range(0, 1 + (int)Vitals.VitalCount).Select(_ => DisplayDirection.StartToEnd).ToList();

        public List<string> TypewriterSounds { get; set; } = new List<string>()
        {
            DEFAULT_TYPEWRITER_SOUND
        };

        public int TypewriterSoundFrequency { get; set; } = DEFAULT_TYPEWRITER_SOUND_FREQ;

        public long TypewriterLetterSpeed { get; set; } = DEFAULT_TYPEWRITER_SPEED;

        public long TypewriterFullstopSpeed { get; set; } = DEFAULT_TYPEWRITER_FULLSTOP_SPEED;

        public long TypewriterPartialstopSpeed { get; set; } = DEFAULT_TYPEWRITER_PARTIALSTOP_SPEED;

        public bool EnableTypewriting { get; set; } = DEFAULT_TYPEWRITER_ENABLED;

        public long TypewriterResponseDelay { get; set; } = DEFAULT_TYPEWRITER_RESPONSE_DELAY;

        public List<char> TypewriterFullstopCharacters { get; set; } = new List<char>()
        {
            '.',
            '!',
            '?',
            ':',
        };

        public List<char> TypewriterPartialstopCharacters { get; set; } = new List<char>()
        {
            ',',
            ';',
            '-',
        };

        #endregion

        #region Serialization Hooks

        [OnDeserializing]
        internal void OnDeserializing(StreamingContext context)
        {
            MenuBackground?.Clear();
            IntroImages?.Clear();
        }

        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {
            Validate();
        }

        #endregion

    }

}
