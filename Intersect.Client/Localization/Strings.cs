using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Intersect.Enums;
using Intersect.Localization;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Intersect.Client.Localization
{

    public static partial class Strings
    {

        private static char[] mQuantityTrimChars = new char[] {'.', '0'};

        public static string FormatQuantityAbbreviated(long value)
        {
            if (value == 0)
            {
                return "";
            }
            else
            {
                double returnVal = 0;
                var postfix = "";

                // hundreds
                if (value <= 999)
                {
                    returnVal = value;
                }

                // thousands
                else if (value >= 1000 && value <= 999999)
                {
                    returnVal = value / 1000.0;
                    postfix = Strings.Numbers.thousands;
                }

                // millions
                else if (value >= 1000000 && value <= 999999999)
                {
                    returnVal = value / 1000000.0;
                    postfix = Strings.Numbers.millions;
                }

                // billions
                else if (value >= 1000000000 && value <= 999999999999)
                {
                    returnVal = value / 1000000000.0;
                    postfix = Strings.Numbers.billions;
                }
                else
                {
                    return "OOB";
                }

                if (returnVal >= 10)
                {
                    returnVal = Math.Floor(returnVal);

                    return returnVal.ToString() + postfix;
                }
                else
                {
                    returnVal = Math.Floor(returnVal * 10) / 10.0;

                    return returnVal.ToString("F1")
                               .TrimEnd(mQuantityTrimChars)
                               .ToString()
                               .Replace(".", Strings.Numbers.dec) +
                           postfix;
                }
            }
        }

        public static void Load()
        {
            if (File.Exists(Path.Combine("resources", "client_strings.json")))
            {
                var strings = new Dictionary<string, Dictionary<string, object>>();
                strings = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(
                    File.ReadAllText(Path.Combine("resources", "client_strings.json"))
                );

                var type = typeof(Strings);

                var fields = new List<Type>();
                fields.AddRange(
                    type.GetNestedTypes(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public)
                );

                foreach (var p in fields)
                {
                    if (!strings.ContainsKey(p.Name))
                    {
                        continue;
                    }

                    var dict = strings[p.Name];
                    foreach (var fieldInfo in p.GetFields(
                        System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public
                    ))
                    {
                        var fieldValue = fieldInfo.GetValue(null);
                        if (!dict.ContainsKey(fieldInfo.Name.ToLower()))
                        {
                            continue;
                        }

                        if (fieldValue is LocalizedString)
                        {
                            fieldInfo.SetValue(null, new LocalizedString((string) dict[fieldInfo.Name.ToLower()]));
                        }
                        else if (fieldValue is Dictionary<int, LocalizedString>)
                        {
                            var existingDict = (Dictionary<int, LocalizedString>) fieldInfo.GetValue(null);
                            var values = ((JObject) dict[fieldInfo.Name]).ToObject<Dictionary<int, string>>();
                            var dic = values.ToDictionary<KeyValuePair<int, string>, int, LocalizedString>(
                                val => val.Key, val => val.Value
                            );

                            foreach (var val in dic)
                            {
                                existingDict[val.Key] = val.Value;
                            }
                        }
                        else if (fieldValue is Dictionary<string, LocalizedString>)
                        {
                            var existingDict = (Dictionary<string, LocalizedString>) fieldInfo.GetValue(null);
                            var pairs = ((JObject) dict[fieldInfo.Name])?.ToObject<Dictionary<string, string>>() ??
                                        new Dictionary<string, string>();

                            foreach (var pair in pairs)
                            {
                                if (pair.Key == null)
                                {
                                    continue;
                                }

                                existingDict[pair.Key.ToLower()] = pair.Value;
                            }
                        }
                    }
                }
            }

            Program.OpenGLLink = Errors.opengllink.ToString();
            Program.OpenALLink = Errors.openallink.ToString();

            Save();
        }

        public static void Save()
        {
            var strings = new Dictionary<string, Dictionary<string, object>>();
            var type = typeof(Strings);
            var fields = type.GetNestedTypes(
                System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public
            );

            foreach (var p in fields)
            {
                var dict = new Dictionary<string, object>();
                foreach (var p1 in p.GetFields(
                    System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public
                ))
                {
                    if (p1.GetValue(null).GetType() == typeof(LocalizedString))
                    {
                        dict.Add(p1.Name.ToLower(), ((LocalizedString) p1.GetValue(null)).ToString());
                    }
                    else if (p1.GetValue(null).GetType() == typeof(Dictionary<int, LocalizedString>))
                    {
                        var dic = new Dictionary<int, string>();
                        foreach (var val in (Dictionary<int, LocalizedString>) p1.GetValue(null))
                        {
                            dic.Add(val.Key, val.Value.ToString());
                        }

                        dict.Add(p1.Name, dic);
                    }
                    else if (p1.GetValue(null).GetType() == typeof(Dictionary<string, LocalizedString>))
                    {
                        var dic = new Dictionary<string, string>();
                        foreach (var val in (Dictionary<string, LocalizedString>) p1.GetValue(null))
                        {
                            dic.Add(val.Key.ToLower(), val.Value.ToString());
                        }

                        dict.Add(p1.Name, dic);
                    }
                }

                strings.Add(p.Name, dict);
            }

            var languageDirectory = Path.Combine("resources");
            if (Directory.Exists(languageDirectory))
            {
                File.WriteAllText(
                    Path.Combine(languageDirectory, "client_strings.json"),
                    JsonConvert.SerializeObject(strings, Formatting.Indented)
                );
            }
        }

        public struct Admin
        {

            public static LocalizedString access = @"Access:";

            public static LocalizedString access0 = @"None";

            public static LocalizedString access1 = @"Moderator";

            public static LocalizedString access2 = @"Admin";

            public static LocalizedString ban = @"Ban";

            public static LocalizedString bancaption = @"Ban {00}";

            public static LocalizedString banprompt =
                @"Banning {00} will not allow them to access this game for the duration you set!";

            public static LocalizedString chronological = @"123...";

            public static LocalizedString chronologicaltip = @"Order maps chronologically.";

            public static LocalizedString face = @"Face:";

            public static LocalizedString kick = @"Kick";

            public static LocalizedString kill = @"Kill";

            public static LocalizedString maplist = @"Map List:";

            public static LocalizedString mute = @"Mute";

            public static LocalizedString mutecaption = @"Mute {00}";

            public static LocalizedString muteprompt =
                @"Muting {00} will not allow them to chat in game for the duration you set!";

            public static LocalizedString name = @"Name:";

            public static LocalizedString noclip = @"No Clip:";

            public static LocalizedString nocliptip = @"Check to walk through obstacles.";

            public static LocalizedString none = @"None";

            public static LocalizedString setface = @"Set Face";

            public static LocalizedString setpower = @"Set Power";

            public static LocalizedString setsprite = @"Set Sprite";

            public static LocalizedString sprite = @"Sprite:";

            public static LocalizedString title = @"Administration";

            public static LocalizedString unban = @"Unban";

            public static LocalizedString unbancaption = @"Unban {00}";

            public static LocalizedString unbanprompt = @"Are you sure that you want to unban {00}?";

            public static LocalizedString unmute = @"Unmute";

            public static LocalizedString unmutecaption = @"Unute {00}";

            public static LocalizedString unmuteprompt = @"Are you sure that you want to unmute {00}?";

            public static LocalizedString warp2me = @"Warp To Me";

            public static LocalizedString warpme2 = @"Warp Me To";
            
            public static LocalizedString overworldreturn = @"Leave Instance";

        }

        public struct Bags
        {

            public static LocalizedString retreiveitem = @"Retreive Item";

            public static LocalizedString retreiveitemprompt = @"How many/much {00} would you like to retreive?";

            public static LocalizedString storeitem = @"Store Item";

            public static LocalizedString storeitemprompt = @"How many/much {00} would you like to store?";

            public static LocalizedString title = @"Bag";

        }

        public struct Bank
        {

            public static LocalizedString deposititem = @"Deposit Item";

            public static LocalizedString deposititemprompt = @"How many/much {00} would you like to deposit?";

            public static LocalizedString title = @"Bank";

            public static LocalizedString withdrawitem = @"Withdraw Item";

            public static LocalizedString withdrawitemprompt = @"How many/much {00} would you like to withdraw?";

            public static LocalizedString sort = @"Sort";

            public static LocalizedString bankvalue = @"Bank Value: {00}";

            public static LocalizedString bankvaluefull = @"{00} Gold Coins";

        }

        public struct BanMute
        {

            public static LocalizedString oneday = @"1 day";

            public static LocalizedString onemonth = @"1 month";

            public static LocalizedString oneweek = @"1 week";

            public static LocalizedString oneyear = @"1 year";

            public static LocalizedString twodays = @"2 days";

            public static LocalizedString twomonths = @"2 months";

            public static LocalizedString twoweeks = @"2 weeks";

            public static LocalizedString threedays = @"3 days";

            public static LocalizedString fourdays = @"4 days";

            public static LocalizedString fivedays = @"5 days";

            public static LocalizedString sixmonths = @"6 months";

            public static LocalizedString cancel = @"Cancel:";

            public static LocalizedString duration = @"Duration:";

            public static LocalizedString forever = @"Indefinitely";

            public static LocalizedString ip = @"Include IP:";

            public static LocalizedString ok = @"Okay:";

            public static LocalizedString reason = @"Reason:";

        }

        public struct Character
        {

            public static LocalizedString equipment = @"Equipment:";

            public static LocalizedString levelandclass = @"Level: {00} {01}";

            public static LocalizedString name = @"{00}";

            public static LocalizedString points = @"Points: {00}";

            public static LocalizedString stat0 = @"{00}: {01}";

            public static LocalizedString stat1 = @"{00}: {01}";

            public static LocalizedString stat2 = @"{00}: {01}";

            public static LocalizedString stat3 = @"{00}: {01}";

            public static LocalizedString stat4 = @"{00}: {01}";

            public static LocalizedString stats = @"Stats:";

            public static LocalizedString title = @"Character";

            public static LocalizedString crafting = @"Crafting:";
            
            public static LocalizedString miningtier = @"Mining Tier: {00}";
            
            public static LocalizedString fishingtier = @"Fishing Tier: {00}";
            
            public static LocalizedString woodcuttingtier = @"Woodcutting Tier: {00}";

            public static LocalizedString classrank = @"{00} Class Rank: {01}";

            public static LocalizedString attacktip = @"Attack-scaling damage, max HP, physical accuracy";

            public static LocalizedString defensetip = @"Physical defense, physical evasion";

            public static LocalizedString agilitytip = @"Movement speed, agility-scaling damage, physical evasion, attunement, resistance & accuracy";

            public static LocalizedString resisttip = @"Magical defense, magical resistance";
            
            public static LocalizedString abilitypowertip = @"Ability power-scaling damage, attunement, maximum MP";

            public static LocalizedString addattacktip = @"Add Attack";

            public static LocalizedString addphysicaldefensetip = @"Add Physical Defense";

            public static LocalizedString addmagicdefense = @"Add Magical Defense";

            public static LocalizedString addagilitytip = @"Add Agility";

            public static LocalizedString addabilitypowertip = @"Add Ability Power";
            
            public static LocalizedString classranktip = @"Your Class Rank(s). This can be increased at a Class Guild Hall.";
            
            public static LocalizedString calculatestats = @"Calculate Stats?";
        }

        public partial struct CharacterCreation
        {

            public static LocalizedString back = @"Back";

            public static LocalizedString Class = @"Class:";

            public static LocalizedString create = @"Create";

            public static LocalizedString female = @"Female";

            public static LocalizedString gender = @"Gender:";

            public static LocalizedString hint = @"Customize";

            public static LocalizedString hint2 = @"Your Character";

            public static LocalizedString invalidname =
                @"Character name is invalid. Please use alphanumeric characters with a length between 2 and 20.";

            public static LocalizedString male = @"Male";

            public static LocalizedString name = @"Char Name:";

            public static LocalizedString title = @"Create Character";

            public static LocalizedString skincolor = @"Skin Color";

            public static LocalizedString hair = @"Hair";

            public static LocalizedString eyes = @"Eyes";
            
            public static LocalizedString clothes = @"Clothes";

            public static LocalizedString extra = @"Extra";

            public static LocalizedString beard = @"Beard";

        }

        public struct CharacterSelection
        {

            public static LocalizedString delete = @"Delete";

            public static LocalizedString deleteprompt =
                @"Are you sure you want to delete {00}? This action is irreversible!";

            public static LocalizedString deletetitle = @"Delete {00}";

            public static LocalizedString empty = @"Empty Character Slot";

            public static LocalizedString info = @"Level {00} {01}";

            public static LocalizedString logout = @"Logout";

            public static LocalizedString name = @"{00}";

            public static LocalizedString New = @"New";

            public static LocalizedString play = @"Use";

            public static LocalizedString title = @"Select a Character";

        }

        public struct Chatbox
        {

            public static LocalizedString channel = @"Channel:";

            public static Dictionary<int, LocalizedString> channels = new Dictionary<int, LocalizedString>
            {
                {0, @"local"},
                {1, @"global"},
                {2, @"party"},
                {3, @"guild"}
            };

            public static LocalizedString channeladmin = @"admin";

            public static LocalizedString enterchat = @"Click here to chat.";

            public static LocalizedString enterchat1 = @"Press {00} to chat.";

            public static LocalizedString enterchat2 = @"Press {00} or {01} to chat.";

            public static LocalizedString send = @"Send";

            public static LocalizedString hide = @"Hide chat";

            public static LocalizedString show = @"Show chat";

            public static LocalizedString title = @"Chat";

            public static LocalizedString toofast = @"You are chatting too fast!";

            public static Dictionary<ChatboxTab, LocalizedString> ChatTabButtons = new Dictionary<Enums.ChatboxTab, LocalizedString>() {
                { ChatboxTab.All, @"All" },
                { ChatboxTab.Local, @"Local" },
                { ChatboxTab.Party, @"Party" },
                { ChatboxTab.Global, @"Global" },
                { ChatboxTab.Guild, @"Clan" },
                { ChatboxTab.System, @"System" },
            };

            public static LocalizedString UnableToCopy = @"It appears you are not able to copy/paste on this platform. Please make sure you have either the 'xclip' or 'wl-clipboard' packages installed if you are running Linux.";

        }

        public struct Colors
        {

            public static Dictionary<int, LocalizedString> presets = new Dictionary<int, LocalizedString>
            {
                {0, @"Black"},
                {1, @"White"},
                {2, @"Pink"},
                {3, @"Blue"},
                {4, @"Red"},
                {5, @"Green"},
                {6, @"Yellow"},
                {7, @"Orange"},
                {8, @"Purple"},
                {9, @"Gray"},
                {10, @"Cyan"}
            };

        }

        public struct Combat
        {

            public static LocalizedString exp = @"Experience";

            public static LocalizedString stat0 = @"Attack";

            public static LocalizedString stat1 = @"Ability Power";

            public static LocalizedString stat2 = @"Defense";

            public static LocalizedString stat3 = @"Magic Resist";

            public static LocalizedString stat4 = @"Speed";

            public static LocalizedString targetoutsiderange = @"Target too far away!";

            public static LocalizedString vital0 = @"Health";

            public static LocalizedString vital1 = @"Mana";

            public static LocalizedString targetneeded = @"Needs Target!";
            
            public static LocalizedString needsrunes = @"Needs Runes!";

            public static LocalizedString warningtitle = @"Combat Warning!";

            public static LocalizedString warningforceclose =
                @"Game was closed while in combat! Your character will remain logged in until combat has concluded!";

            public static LocalizedString warninglogout =
                @"You are about to logout while in combat! Your character will remain in-game until combat has ended! Are you sure you want to logout now?";

            public static LocalizedString warningcharacterselect =
                @"You are about to logout while in combat! Your character will remain in-game until combat has ended! Are you sure you want to logout now?";

            public static LocalizedString warningexitdesktop =
                @"You are about to exit while in combat! Your character will remain in-game until combat has ended! Are you sure you want to quit now?";

            public static LocalizedString combo = @"Combo";

            public static LocalizedString notenoughmp = @"Not enough MP!";

            public static LocalizedString silenced = @"Silenced!";
            public static LocalizedString stunned = @"Stunned!";
            public static LocalizedString snared = @"Snared!";
            public static LocalizedString sleep = @"Asleep!";
            public static LocalizedString blind = @"Blind!";
            public static LocalizedString confused = @"Confused!";

            public static LocalizedString lowhealth = @"HP LOW!";
        }

        public struct Controls
        {

            public static Dictionary<string, LocalizedString> controldict = new Dictionary<string, LocalizedString>
            {
                {"attackinteract", @"Attack/Interact:"},
                {"block", @"Block:"},
                {"autotarget", @"Auto Target:"},
                {"enter", @"Enter:"},
                {"hotkey0", @"Hot Key 0:"},
                {"hotkey1", @"Hot Key 1:"},
                {"hotkey2", @"Hot Key 2:"},
                {"hotkey3", @"Hot Key 3:"},
                {"hotkey4", @"Hot Key 4:"},
                {"hotkey5", @"Hot Key 5:"},
                {"hotkey6", @"Hot Key 6:"},
                {"hotkey7", @"Hot Key 7:"},
                {"hotkey8", @"Hot Key 8:"},
                {"hotkey9", @"Hot Key 9:"},
                {"movedown", @"Down:"},
                {"moveleft", @"Left:"},
                {"moveright", @"Right:"},
                {"moveup", @"Up:"},
                {"pickup", @"Pick Up:"},
                {"screenshot", @"Screenshot:"},
                {"openmenu", @"Open Menu:"},
                {"openinventory", @"Open Inventory:"},
                {"openquests", @"Open Quests:"},
                {"opencharacterinfo", @"Open Character Info:"},
                {"openparties", @"Open Parties:"},
                {"openspells", @"Open Spells:"},
                {"openfriends", @"Open Friends:"},
                {"openguild", @"Open Guild:"},
                {"opensettings", @"Open Settings:"},
                {"opendebugger", @"Open Debugger:"},
                {"openadminpanel", @"Open Admin Panel:"},
                {"togglegui", @"Toggle Interface:"},
                {"turnclockwise", @"Turn Clockwise:"},
                {"turncounterclockwise", @"Turn Counter Clockwise:"},
                {"facetarget", @"Face Target:"},
                {"targetparty1", @"Target Self/Party 1:"},
                {"targetparty2", @"Target Party 2:"},
                {"targetparty3", @"Target Party 3:"},
                {"targetparty4", @"Target Party 4:"},
                {"openoverworldmap", @"Overworld Map:"},
                {"openbestiary", @"Open Bestiary:"},
                {"togglezoomin", @"Zoom In:"},
                {"togglezoomout", @"Zoom Out:"},
            };

            public static LocalizedString CombatMode = @"Enter Combat Mode";

            public static LocalizedString FaceTarget = @"Face Target";

            public static LocalizedString listening = @"Listening";

        }

        public struct Crafting
        {

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString craft = @"Craft 1";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString craftall = @"Craft {00}";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString craftstop = "Stop";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString incorrectresources =
                @"You do not have the correct resources to craft this item.";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString ingredients = @"Requires:";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString product = @"Crafting:";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString recipes = @"Recipes:";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString title = @"Crafting Table";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString recipe = @"{01}";

        }

        public struct Credits
        {

            public static LocalizedString back = @"Main Menu";

            public static LocalizedString title = @"Credits";

        }

        public struct Debug
        {

            public static LocalizedString draws = @"Draws: {00}";

            public static LocalizedString entitiesdrawn = @"Entities Drawn: {00}";

            public static LocalizedString fps = @"FPS: {00}";

            public static LocalizedString knownentities = @"Known Entities: {00}";

            public static LocalizedString knownmaps = @"Known Maps: {00}";

            public static LocalizedString lightsdrawn = @"Lights Drawn: {00}";

            public static LocalizedString map = @"Map: {00}";

            public static LocalizedString mapsdrawn = @"Maps Drawn: {00}";

            public static LocalizedString ping = @"Ping: {00}";

            public static LocalizedString time = @"Time: {00}";

            public static LocalizedString title = @"Debug";

            public static LocalizedString x = @"X: {00}";

            public static LocalizedString y = @"Y: {00}";

            public static LocalizedString z = @"Z: {00}";

            public static LocalizedString interfaceobjects = @"Interface Objects: {00}";

        }

        public struct EntityBox
        {

            public static LocalizedString NameAndLevel = @"{00}    {01}";

            public static LocalizedString cooldown = "{00}s";

            public static LocalizedString exp = @"EXP:";

            public static LocalizedString expval = @"{00} / {01}";

            public static LocalizedString friend = "Befriend";

            public static LocalizedString friendtip = "Send {00} a friend request.";

            public static LocalizedString level = @"Lv. {00}";

            public static LocalizedString map = @"{00}";

            public static LocalizedString maxlevel = @"Max Level";

            public static LocalizedString party = @"Party";

            public static LocalizedString partytip = @"Invite {00} to your party.";

            public static LocalizedString trade = @"Trade";

            public static LocalizedString tradetip = @"Request to trade with {00}.";

            public static LocalizedString vital0 = @"HP:";

            public static LocalizedString vital0val = @"{00} / {01}";

            public static LocalizedString vital1 = @"MP:";

            public static LocalizedString vital1val = @"{00} / {01}";

        }

        public struct Errors
        {

            public static LocalizedString displaynotsupported = @"Invalid Display Configuration!";

            public static LocalizedString displaynotsupportederror =
                @"Fullscreen {00} resolution is not supported on this device!";

            public static LocalizedString errorencountered =
                @"The Intersect Client has encountered an error and must close. Error information can be found in logs/errors.log";

            public static LocalizedString notconnected = @"Not connected to the game server. Is it online?";

            public static LocalizedString notsupported = @"Not Supported!";

            public static LocalizedString openallink = @"https://goo.gl/Nbx6hx";

            public static LocalizedString opengllink = @"https://goo.gl/RSP3ts";

            public static LocalizedString passwordinvalid =
                @"Password is invalid. Please use alphanumeric characters with a length between 4 and 20.";

            public static LocalizedString resourcesnotfound =
                @"The resources directory could not be found! Intersect will now close.";

            public static LocalizedString title = @"Error!";

            public static LocalizedString usernameinvalid =
                @"Username is invalid. Please use alphanumeric characters with a length between 2 and 20.";

            public static LocalizedString LoadFile =
                @"Failed to load a {00}. Please send the game administrator a copy of your errors log file in the logs directory.";

            public static LocalizedString lostconnection =
                @"Lost connection to the game server. Please make sure you're connected to the internet and try again!";

        }

        public struct Words
        {

            public static LocalizedString lcase_sound = @"sound";

            public static LocalizedString lcase_music = @"soundtrack";

            public static LocalizedString lcase_sprite = @"sprite";

            public static LocalizedString lcase_animation = @"animation";

        }

        public struct EventWindow
        {

            public static LocalizedString Continue = @"Continue";

        }

        public struct ForgotPass
        {

            public static LocalizedString back = @"Back";

            public static LocalizedString hint =
                @"If your account exists we will send you a temporary password reset code.";

            public static LocalizedString label = @"Enter your username or email below:";

            public static LocalizedString submit = @"Submit";

            public static LocalizedString title = @"Password Reset";

        }

        public struct Friends
        {

            public static LocalizedString addfriend = @"Add Friend";

            public static LocalizedString addfriendtitle = @"Add Friend";

            public static LocalizedString addfriendprompt = @"Who would you like to add as a friend?";

            public static LocalizedString infight = @"You are currently fighting!";

            public static LocalizedString removefriend = @"Remove Friend";

            public static LocalizedString removefriendprompt = @"Do you wish to remove {00} from your friends list?";

            public static LocalizedString request = @"Friend Request";

            public static LocalizedString requestprompt = @"{00} has sent you a friend request. Do you accept?";

            public static LocalizedString title = @"Friends";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Pm = @"Send PM";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString PartyInvite = @"Invite to party";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString PartyInviteTitle = @"PARTY INVITE";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString PartyInvitePrompt = @"Would you like to invite {00} to your party?";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString GuildInviteTitle = @"CLAN INVITE";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString GuildInvitePrompt = @"Would you like to invite {00} to the clan: {01}?";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString GuildInvite = @"Invite to clan";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Remove = @"Remove from friends";
        }

        public partial struct GameMenu
        {

            public static LocalizedString character = @"Character Info";

            public static LocalizedString Menu = @"Open Menu";

            public static LocalizedString friends = @"Friends";

            public static LocalizedString items = @"Inventory";

            public static LocalizedString party = @"Party";

            public static LocalizedString quest = @"Quest Log";

            public static LocalizedString spells = @"Spell Book";

        }

        public struct General
        {

            public static LocalizedString none = @"None";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString MapItemStackable = @"{01} {00}";

        }

        public struct Guilds
        {
            public static LocalizedString Guild = @"Guild";

            public static LocalizedString guildtip = "Send {00} an invite to your guild.";

            public static LocalizedString Invite = @"Invite";

            public static LocalizedString NotInGuild = @"You are not currently in a guild!";

            public static LocalizedString InviteMemberTitle = @"Invite Player";

            public static LocalizedString InviteMemberPrompt = @"Who would you like to invite to {00}?";

            public static LocalizedString InviteRequestTitle = @"Guild Invite";

            public static LocalizedString InviteRequestPrompt = @"{00} would like to invite you to join their guild, {01}. Do you accept?";

            public static LocalizedString Leave = "Leave";

            public static LocalizedString LeaveTitle = @"Leave Guild";

            public static LocalizedString LeavePrompt = @"Are you sure you would like to leave your guild?";

            public static LocalizedString Promote = @"Promote to {00}";

            public static LocalizedString Demote = @"Demote to {00}";

            public static LocalizedString Kick = @"Kick";

            public static LocalizedString PM = @"PM";

            public static LocalizedString Transfer = @"Transfer";

            public static LocalizedString OnlineListEntry = @"[{00}] {01} - {02}";

            public static LocalizedString OfflineListEntry = @"[{00}] {01} - {02}";

            public static LocalizedString Tooltip = @"Lv. {00} {01}";

            public static LocalizedString KickTitle = @"Kick Guild Member";

            public static LocalizedString KickPrompt = @"Are you sure you would like to kick {00}?";

            public static LocalizedString PromoteTitle = @"Promote Guild Member";

            public static LocalizedString PromotePrompt = @"Are you sure you would like promote {00} to rank {01}?";

            public static LocalizedString DemoteTitle = @"Demote Guild Member";

            public static LocalizedString DemotePrompt = @"Are you sure you would like to demote {00} to rank {01}?";

            public static LocalizedString TransferTitle = @"Transfer Guild";

            public static LocalizedString TransferPrompt = @"This action will completely transfer all ownership of your guild to {00} and you will lose your rank of {01}. If you are sure you want to hand over your guild enter '{02}' below.";

            public static LocalizedString Bank = @"{00} Guild Bank";

            public static LocalizedString NotAllowedWithdraw = @"You do not have permission to withdraw from {00}'s guild bank!";

            public static LocalizedString NotAllowedDeposit = @"You do not have permission to deposit items into {00}'s guild bank!";

            public static LocalizedString NotAllowedSwap = @"You do not have permission to swap items around within {00}'s guild bank!";

            public static LocalizedString NotInGuildInstructions = @"You are not in a clan. To start a clan, visit any Inn and speak to the clan ambassador therein.";

            public static LocalizedString InviteAlreadyInGuild = @"The player you're trying to invite is already in a guild or has a pending invite.";
        }

        public struct InputBox
        {

            public static LocalizedString cancel = @"Cancel";

            public static LocalizedString no = @"No";

            public static LocalizedString okay = @"Okay";

            public static LocalizedString yes = @"Yes";

        }

        public struct MapItemWindow
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Title = @"Loot";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString LootButton = @"Loot All";

        }

        public struct Inventory
        {

            public static LocalizedString cooldown = "{00}s";

            public static LocalizedString dropitem = @"Drop Item";

            public static LocalizedString dropitemprompt = @"How many/much {00} do you want to drop?";

            public static LocalizedString dropprompt = @"Do you wish to drop the item: {00}?";
            
            public static LocalizedString cannotdrop = @"You cannot drop this item!";

            public static LocalizedString destroyitem = @"Destroy Item";

            public static LocalizedString AddFuel = @"Add as Fuel";

            public static LocalizedString AddFuelPrompt = @"How many/much {00} do you want to use as fuel?";

            public static LocalizedString destroyitemprompt = @"How many/much {00} do you want to DESTROY? (Note: Item destruction is permanent)";

            public static LocalizedString destroyprompt = @"Do you wish to DESTROY the item: {00}? (Note: Item destruction is permanent)";

            public static LocalizedString equippedicon = "E";

            public static LocalizedString title = @"Inventory";

        }

        public struct ItemDescription
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString BackstepSpeedBonus = @"Backstep Speed Bonus:";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString StrafeSpeedBonus = @"Strafe Speed Bonus:";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString BackstabMultiplier = @"Backstab Multiplier:";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString BaseDamageType = @"Damage Type:";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static Dictionary<int, LocalizedString> DamageTypes = new Dictionary<int, LocalizedString>()
            {
                { 0, @"Physical" },
                { 1, @"Magic" },
                { 2, @"True" }
            };

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString BaseDamage = @"Base Damage:";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString CosmeticDesc = @"Use to unlock a new cosmetic.";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString EnhancementDesc = @"Use to unlock a new weapon enhancement.";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString PermabuffDesc = @"Permanently adds:";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString PermabuffSkillPoints = @"Skill Point(s):";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString PermabuffUsed = @"Already used!";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString EnhancementKnown = @"Already learned!";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString PermabuffUnused = @"Can be used once";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString CritChance = @"Critical Chance:";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString CritMultiplier = @"Critical Multiplier:";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString AttackSpeed = @"Base Atk Speed:";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString EnhancementThreshold = @"Enhance. Pts.";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString StudyOpportunity = @"Study Opportunity";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString CosmeticOpportunity = @"Cosmetic Unlock %";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Studied = @"Studied!";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString CosmeticKnown = @"Cosmetic unlocked!";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString StudyOpportunityText = @"{00} ({01}%)";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString CosmeticChance = @"{00}%";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString AttackSpeedReal = @"Real Atk Speed:";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Seconds = @"{00}s";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Percentage = @"{00}%";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Multiplier = @"{00}x";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString RegularAndPercentage = @"{00} + {01}%";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static Dictionary<int, LocalizedString> Stats = new Dictionary<int, LocalizedString>
            {
                {0, @"Blunt Attack"},
                {1, @"Magic Attack"},
                {2, @"Blunt Resistance"},
                {3, @"Magic Resistance"},
                {4, @"Speed"},
                {5, @"Slash Attack"},
                {6, @"Slash Resistance"},
                {7, @"Pierce Attack"},
                {8, @"Pierce Resistance"},
                {9, @"Evasion"},
                {10, @"Accuracy"},
            };

            public static Dictionary<int, LocalizedString> StatPercentages = new Dictionary<int, LocalizedString>
            {
                {0, @"Blunt Attack (%)"},
                {1, @"Magic Attack (%)"},
                {2, @"Blunt Resistance (%)"},
                {3, @"Magic Resistance (%)"},
                {4, @"Speed (%)"},
                {5, @"Slash Attack (%)"},
                {6, @"Slash Resistance (%)"},
                {7, @"Pierce Attack (%)"},
                {8, @"Pierce Resistance (%)"},
                {9, @"Evasion (%)"},
                {10, @"Accuracy (%)"},
            };

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static Dictionary<int, LocalizedString> StatCounts = new Dictionary<int, LocalizedString>
            {
                {0, @"Blunt Atk:"},
                {1, @"Magic Atk:"},
                {2, @"Blunt Def:"},
                {3, @"Magic Def:"},
                {4, @"Speed:"},
                {5, @"Slash Atk:"},
                {6, @"Slash Def:"},
                {7, @"Pierce Atk:"},
                {8, @"Pierce Def:"},
                {9, @"Evasion:"},
                {10, @"Accuracy:"},
            };
            
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString ScalingStat = @"Scaling Stat:";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString ScalingPercentage = @"Scaling Percentage:";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static Dictionary<int, LocalizedString> Vitals = new Dictionary<int, LocalizedString>
            {
                {0, @"HP"},
                {1, @"MP"}
            };

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static Dictionary<int, LocalizedString> VitalPercentages = new Dictionary<int, LocalizedString>
            {
                {0, @"HP (%)"},
                {1, @"MP (%)"}
            };

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static Dictionary<int, LocalizedString> VitalRegens = new Dictionary<int, LocalizedString>
            {
                {0, @"OoC HP Regen"},
                {1, @"MP Regen"}
            };

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static Dictionary<int, LocalizedString> ConsumableTypes = new Dictionary<int, LocalizedString>()
            {
                {0, "Restores HP:" },
                {1, "Restores MP:" },
                {2, "Grants Experience:" },

            };

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static Dictionary<int, LocalizedString> BonusEffects = new Dictionary<int, LocalizedString>
            {
                {0, @""},
                {1, @"Cooldown Reduction:"},
                {2, @"Lifesteal:"},
                {3, @"Tenacity:"},
                {4, @"Luck:"},
                {5, @"Bonus Experience:"},
                {6, @"Affinity:"},
                {7, @"Critical Bonus:"},
                {8, @"Swiftness:"},
                {9, @"Prospector:"},
                {10, @"Angler:"},
                {11, @"Lumberjack:"},
                {12, @"Assassin:"},
                {13, @"Sniper:"},
                {14, @"Berzerk:"},
                {15, @"Manasteal:"},
                {16, @"Phantom:"},
                {17, @"Vampire:"},
                {18, @"Junkrat:"},
                {19, @"Block:"},
            };

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString TwoHand = @"2H";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static Dictionary<int, LocalizedString> ItemTypes = new Dictionary<int, LocalizedString>
            {
                {0, @"Item"},
                {1, @"Equipment"},
                {2, @"Consumable"},
                {3, @"Currency"},
                {4, @"Spell"},
                {5, @"Special"},
                {6, @"Bag"},
                {7, @"Cosmetic"},
                {8, @"Weapon Enhancement"},
                {9, @"Permabuff"},
            };

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static Dictionary<int, LocalizedString> Rarity = new Dictionary<int, LocalizedString>
            {
                {0, @"None"},
                {1, @"Tier 0"},
                {2, @"Tier 1"},
                {3, @"Tier 2"},
                {4, @"Tier 3"},
                {5, @"Tier 4"},
                {6, @"Tier 5"},
                {7, @"Tier 6"},
                {8, @"Tier 7"},
                {9, @"Tier 8"},
            };

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Description = @"{00}";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString CastSpell = @"Casts Spell: {00}";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString ComponentsNeeded = @"Components Needed:";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString TeachSpell = @"Teaches Spell: {00}";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString SingleUse = @"Single use";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString BagSlots = @"Bag Slots:";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString ItemLimits = @"Can not be {00}";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Banked = @"Banked";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString GuildBanked = @"Guild Banked";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Sold = @"Sold";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Dropped = @"Dropped";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Bagged = @"Bagged";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Traded = @"Traded";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Amount = @"Amount:";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString DropOnDeath = @"Death Drop %:";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString BestiaryDropChance = @"Drop chance:";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString BestiaryDropQuantity = @"Drop quantity:";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString BestiaryDropChanceTable = @"Drop Chance:";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString BestiaryTableChance = @"Drop Table Chance:";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Restriction = @"Requirements:";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString RestrictionOr = @"OR {00}";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString DestroyOnInstance = @"Instance Item";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString StatAndBuff = @"{00}{01}{02}";
        }

        public struct Keys
        {

            public static Dictionary<string, LocalizedString> keydict = new Dictionary<string, LocalizedString>()
            {
                {"a", @"A"},
                {"add", @"Add"},
                {"alt", @"Alt"},
                {"apps", @"Apps"},
                {"attn", @"Attn"},
                {"b", @"B"},
                {"back", @"Back"},
                {"browserback", @"BrowserBack"},
                {"browserfavorites", @"BrowserFavorites"},
                {"browserforward", @"BrowserForward"},
                {"browserhome", @"BrowserHome"},
                {"browserrefresh", @"BrowserRefresh"},
                {"browsersearch", @"BrowserSearch"},
                {"browserstop", @"BrowserStop"},
                {"c", @"C"},
                {"cancel", @"Cancel"},
                {"capital", @"Capital"},
                {"capslock", @"CapsLock"},
                {"clear", @"Clear"},
                {"control", @"Control"},
                {"controlkey", @"ControlKey"},
                {"crsel", @"Crsel"},
                {"d", @"D"},
                {"d0", @"0"},
                {"d1", @"1"},
                {"d2", @"2"},
                {"d3", @"3"},
                {"d4", @"4"},
                {"d5", @"5"},
                {"d6", @"6"},
                {"d7", @"7"},
                {"d8", @"8"},
                {"d9", @"9"},
                {"decimal", @"Decimal"},
                {"delete", @"Delete"},
                {"divide", @"Divide"},
                {"down", @"Down"},
                {"e", @"E"},
                {"end", @"End"},
                {"enter", @"Enter"},
                {"eraseeof", @"EraseEof"},
                {"escape", @"Escape"},
                {"execute", @"Execute"},
                {"exsel", @"Exsel"},
                {"f", @"F"},
                {"f1", @"F1"},
                {"f10", @"F10"},
                {"f11", @"F11"},
                {"f12", @"F12"},
                {"f13", @"F13"},
                {"f14", @"F14"},
                {"f15", @"F15"},
                {"f16", @"F16"},
                {"f17", @"F17"},
                {"f18", @"F18"},
                {"f19", @"F19"},
                {"f2", @"F2"},
                {"f20", @"F20"},
                {"f21", @"F21"},
                {"f22", @"F22"},
                {"f23", @"F23"},
                {"f24", @"F24"},
                {"f3", @"F3"},
                {"f4", @"F4"},
                {"f5", @"F5"},
                {"f6", @"F6"},
                {"f7", @"F7"},
                {"f8", @"F8"},
                {"f9", @"F9"},
                {"finalmode", @"FinalMode"},
                {"g", @"G"},
                {"h", @"H"},
                {"hanguelmode", @"HanguelMode"},
                {"hangulmode", @"HangulMode"},
                {"hanjamode", @"HanjaMode"},
                {"help", @"Help"},
                {"home", @"Home"},
                {"i", @"I"},
                {"imeaccept", @"IMEAccept"},
                {"imeaceept", @"IMEAceept"},
                {"imeconvert", @"IMEConvert"},
                {"imemodechange", @"IMEModeChange"},
                {"imenonconvert", @"IMENonconvert"},
                {"insert", @"Insert"},
                {"j", @"J"},
                {"junjamode", @"JunjaMode"},
                {"k", @"K"},
                {"kanamode", @"KanaMode"},
                {"kanjimode", @"KanjiMode"},
                {"keycode", @"KeyCode"},
                {"l", @"L"},
                {"launchapplication1", @"LaunchApplication1"},
                {"launchapplication2", @"LaunchApplication2"},
                {"launchmail", @"LaunchMail"},
                {"lbutton", @"Left Mouse"},
                {"lcontrolkey", @"L Control"},
                {"left", @"Left"},
                {"linefeed", @"LineFeed"},
                {"lmenu", @"L Menu"},
                {"lshiftkey", @"L Shift"},
                {"lwin", @"LWin"},
                {"m", @"M"},
                {"mbutton", @"Middle Mouse"},
                {"medianexttrack", @"MediaNextTrack"},
                {"mediaplaypause", @"MediaPlayPause"},
                {"mediaprevioustrack", @"MediaPreviousTrack"},
                {"mediastop", @"MediaStop"},
                {"menu", @"Menu"},
                {"modifiers", @"Modifiers"},
                {"multiply", @"Multiply"},
                {"n", @"N"},
                {"next", @"Next"},
                {"noname", @"NoName"},
                {"none", @"None"},
                {"numlock", @"NumLock"},
                {"numpad0", @"NumPad 0"},
                {"numpad1", @"NumPad 1"},
                {"numpad2", @"NumPad 2"},
                {"numpad3", @"NumPad 3"},
                {"numpad4", @"NumPad 4"},
                {"numpad5", @"NumPad 5"},
                {"numpad6", @"NumPad 6"},
                {"numpad7", @"NumPad 7"},
                {"numpad8", @"NumPad 8"},
                {"numpad9", @"NumPad 9"},
                {"o", @"O"},
                {"oem1", @"1"},
                {"oem102", @"0"},
                {"oem2", @"2"},
                {"oem3", @"3"},
                {"oem4", @"4"},
                {"oem5", @"5"},
                {"oem6", @"6"},
                {"oem7", @"7"},
                {"oem8", @"8"},
                {"oembackslash", @"\"},
                {"oemclear", @"OemClear"},
                {"oemclosebrackets", @"]"},
                {"oemcomma", @","},
                {"oemminus", @"-"},
                {"oemopenbrackets", @"["},
                {"oemperiod", @"."},
                {"oempipe", @"|"},
                {"oemplus", @"+"},
                {"oemquestion", @"?"},
                {"oemquotes", @"'"},
                {"oemsemicolon", @"OemSemicolon"},
                {"oemtilde", @"`"},
                {"p", @"P"},
                {"pa1", @"Pa1"},
                {"packet", @"Packet"},
                {"pagedown", @"Page Down"},
                {"pageup", @"Page Up"},
                {"pause", @"Pause"},
                {"play", @"Play"},
                {"print", @"Print"},
                {"printscreen", @"PrintScreen"},
                {"prior", @"Prior"},
                {"processkey", @"ProcessKey"},
                {"q", @"Q"},
                {"r", @"R"},
                {"rbutton", @"Right Mouse"},
                {"rcontrolkey", @"R Control"},
                {"return", @"Return"},
                {"right", @"Right"},
                {"rmenu", @"R Menu"},
                {"rshiftkey", @"R Shift"},
                {"rwin", @"RWin"},
                {"s", @"S"},
                {"scroll", @"Scroll"},
                {"select", @"Select"},
                {"selectmedia", @"SelectMedia"},
                {"separator", @"Separator"},
                {"shift", @"Shift"},
                {"shiftkey", @"ShiftKey"},
                {"sleep", @"Sleep"},
                {"snapshot", @"Snapshot"},
                {"space", @"Space"},
                {"subtract", @"Subtract"},
                {"t", @"T"},
                {"tab", @"Tab"},
                {"u", @"U"},
                {"up", @"Up"},
                {"v", @"V"},
                {"volumedown", @"VolumeDown"},
                {"volumemute", @"VolumeMute"},
                {"volumeup", @"VolumeUp"},
                {"w", @"W"},
                {"x", @"X"},
                {"xbutton1", @"XButton1"},
                {"xbutton2", @"XButton2"},
                {"y", @"Y"},
                {"z", @"Z"},
                {"zoom", @"Zoom"},
            };

        }

        public struct Login
        {

            public static LocalizedString back = @"Back";

            public static LocalizedString forgot = @"Forgot Password?";

            public static LocalizedString login = @"Login";

            public static LocalizedString password = @"Password:";

            public static LocalizedString savepass = @"Save Password";

            public static LocalizedString title = @"Login";

            public static LocalizedString username = @"Username:";

        }

        public struct Main
        {

            public static LocalizedString gamename = @"Intersect Client";

        }

        public struct MainMenu
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Credits = @"Credits";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Exit = @"Exit";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Login = @"Login";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Register = @"Register";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Settings = @"Settings";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString SettingsTooltip = @"";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Title = @"Main Menu";
        }

        public struct Settings
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Apply = @"Apply";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString AudioSettingsTab = @"Audio";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString AutoCloseWindows = @"Auto-close Windows";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Cancel = @"Cancel";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Fps120 = @"120";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Fps30 = @"30";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Fps60 = @"60";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Fps90 = @"90";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Fullscreen = @"Fullscreen";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString GameSettingsTab = @"Game";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString KeyBindingSettingsTab = @"Controls";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString MusicVolume = @"Music Volume: {00}%";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Resolution = @"Resolution:";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString ResolutionCustom = @"Custom Resolution";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Restore = @"Restore Defaults";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString SoundVolume = @"Sound Volume: {00}%";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString TargetFps = @"Target FPS:";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Title = @"Settings";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString UnlimitedFps = @"No Limit";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString VideoSettingsTab = @"Video";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Vsync = @"V-Sync";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString TapToTurn = @"Tap-to-Turn";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString EnableScanlines = @"Enable Scanlines";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString EnterCombatOnTarget = @"Combat Mode on Target";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString ClassicMode = @"Classic Controls";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString FaceOnLock = @"Face Target on Lock";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString LeftClickTarget = @"Left Click Target";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString CombatFlash = @"Combat Screen Flashes";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString CombatShake = @"Combat Shake";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString PlayerNames = @"Display Player Names";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString StatusMarkers = @"Display Entity Statuses";
            
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString SelfStatusMarkers = @"Display Self Statuses";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString NPCNames = @"Display NPC Names";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString ChangeTarget = @"Change Target on Death";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString PartyMembers = @"Highlight Party Members";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString ClanMembers = @"Highlight Clan Members";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Fade = @"Fade Instead of Wipe";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString AttackCancelsCast = @"Attack Cancels Cast";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString CastingIndicators = @"Casting Indicator";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString HostileTileMarkers = @"Hostile Tile Markers";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString PartyTileMarkers = @"Party Tile Markers";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString SelfTileMakers = @"Self Tile Markers";
            
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString PartyInfo = @"Show Party Info";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString TypewriterText = @"Typewriter Text";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString NameFading = @"Name Fading";
        }

        public struct Parties
        {
            public static LocalizedString infight = @"You are currently fighting!";

            public static LocalizedString inviteprompt = @"{00} has invited you to their party. Do you accept?";

            public static LocalizedString kick = @"Kick {00}";

            public static LocalizedString kicklbl = @"Kick";

            public static LocalizedString leader = @"Leader";

            public static LocalizedString leadertip = @"Party Leader";

            public static LocalizedString leave = @"Leave Party";

            public static LocalizedString leavetip = @"Leave Tip";

            public static LocalizedString name = @"{00} - Lv. {01}";

            public static LocalizedString partyinvite = @"Party Invite";

            public static LocalizedString title = @"Party";

            public static LocalizedString vital0 = @"HP:";

            public static LocalizedString vital0val = @"{00} / {01}";

            public static LocalizedString vital1 = @"MP:";

            public static LocalizedString vital1val = @"{00} / {01}";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Invite = @"Invite";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString InviteTitle = @"INVITE TO PARTY";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString InvitePopupPrompt = @"Who would you like to invite to your party?";
        }

        public struct QuestLog
        {

            public static LocalizedString abandon = @"Abandon";

            public static LocalizedString abandonprompt = @"Are you sure that you want to quit the quest ""{00}""?";

            public static LocalizedString abandontitle = @"Abandon Quest: {00}";

            public static LocalizedString back = @"Back";

            public static LocalizedString completed = @"Quest Completed";

            public static LocalizedString currenttask = @"Current Task:";

            public static LocalizedString inprogress = @"Quest In Progress";

            public static LocalizedString notstarted = @"Quest Not Started";

            public static LocalizedString taskitem = @"{00}/{01} {02}(s) gathered.";

            public static LocalizedString tasknpc = @"{00}/{01} {02}(s) slain.";

            public static LocalizedString title = @"Quest Log";

            public static LocalizedString questpoints = @"Unspent Quest Points: {00}";

            public static LocalizedString lifetimequestpoints = @"Quest Points: {00}";

        }

        public struct QuestOffer
        {

            public static LocalizedString accept = @"Accept";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Completed = @"(Completed!)";

            public static LocalizedString decline = @"Decline";

            public static LocalizedString title = @"Quest Offer";

            public static LocalizedString backtoboard = @"Back";
            
            public static LocalizedString questamount = @"{00} / {01}";

        }

        public struct QuestBoard
        {
            public static LocalizedString cancel = @"Cancel";
        }

        public struct Regex
        {

            public static LocalizedString email =
                @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

            public static LocalizedString password = @"^[-_=\+`~!@#\$%\^&\*()\[\]{}\\|;\:'"",<\.>/\?a-zA-Z0-9]{4,64}$";

            public static LocalizedString username = @"^[a-zA-Z0-9]{2,20}$";

        }

        public struct Registration
        {

            public static LocalizedString back = @"Back";

            public static LocalizedString confirmpass = @"Confirm Password:";

            public static LocalizedString email = @"Email:";

            public static LocalizedString emailinvalid = @"Email is invalid.";

            public static LocalizedString password = @"Password:";

            public static LocalizedString passwordmatch = @"Passwords didn't match!";

            public static LocalizedString register = @"Register";

            public static LocalizedString title = @"Register";

            public static LocalizedString username = @"Username:";

        }

        public struct ResetPass
        {

            public static LocalizedString back = @"Cancel";

            public static LocalizedString code = @"Enter the reset code that was sent to you:";

            public static LocalizedString fail = @"Error!";

            public static LocalizedString failmsg =
                @"The reset code was not valid, has expired, or the account does not exist!";

            public static LocalizedString inputcode = @"Please enter your password reset code.";

            public static LocalizedString password = @"New Password:";

            public static LocalizedString password2 = @"Confirm Password:";

            public static LocalizedString submit = @"Submit";

            public static LocalizedString success = @"Success!";

            public static LocalizedString successmsg = @"Your password has been reset!";

            public static LocalizedString title = @"Password Reset";

        }

        public struct Resources
        {

            public static LocalizedString cancelled = @"Download was Cancelled!";

            public static LocalizedString failedtoload = @"Failed to load Resources!";

            public static LocalizedString resourceexception =
                @"Failed to download client resources.\n\nException Info: {00}\n\nWould you like to try again?";

            public static LocalizedString resourcesfatal =
                @"Failed to load resources from client directory and Ascension Game Dev server. Cannot launch game!";

        }

        public struct Server
        {

            public static LocalizedString StatusLabel = @"Server Status: {00}";

            public static LocalizedString Online = @"Online";

            public static LocalizedString Offline = @"Offline";

            public static LocalizedString Failed = @"Network Error";

            public static LocalizedString Connecting = @"Connecting...";

            public static LocalizedString Unknown = @"Unknown";

            public static LocalizedString VersionMismatch = @"Bad Version";

            public static LocalizedString ServerFull = @"Full";

            public static LocalizedString HandshakeFailure = @"Handshake Error";

        }

        public struct Shop
        {

            public static LocalizedString buyitem = @"Buy Item";

            public static LocalizedString buyitemprompt = @"How many/much {00} would you like to buy?";

            public static LocalizedString cannotsell = @"This shop does not accept that item!";

            public static LocalizedString costs = @"Costs {00} {01}(s)";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString QuickSell = "(Shift + Right Click - quick sell)";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString QuickSellAll = "(Shift + Right Click - quick sell all)";

            public static LocalizedString sellitem = @"Sell Item";

            public static LocalizedString sellitemprompt = @"How many/much {00} would you like to sell?";

            public static LocalizedString sellprompt = @"Do you wish to sell the item: {00}?";

            public static LocalizedString sellsfor = @"Sells for {00} {01}(s)";

            public static LocalizedString wontbuy = @"Shop Will Not Buy This Item";

        }

        public struct SpellDescription
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static Dictionary<int, LocalizedString> SpellTypes = new Dictionary<int, LocalizedString>
            {
                {0, @"Combat Spell"},
                {1, @"Warp to Map"},
                {2, @"Warp to Target"},
                {3, @"Dash"},
                {4, @"Special"},
            };

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Description = @"{00}";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString CastTime = @"Cast Time:";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Instant = @"Instant";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Seconds = @"{00}s";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Percentage = @"{00}%";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Multiplier = @"{00}x";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static Dictionary<int, LocalizedString> VitalCosts = new Dictionary<int, LocalizedString>
            {
                {0, @"HP Cost:"},
                {1, @"MP Cost:"},
            };

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static Dictionary<int, LocalizedString> VitalDamage = new Dictionary<int, LocalizedString>
            {
                {0, @"HP Damage:"},
                {1, @"MP Damage:"},
            };

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static Dictionary<int, LocalizedString> VitalRecovery = new Dictionary<int, LocalizedString>
            {
                {0, @"HP Recovery:"},
                {1, @"MP Recovery:"},
            };

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Cooldown = @"Cooldown:";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString CooldownGroup = @"Cooldown Group:";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString SpellGroup = @"Spell Group:";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString IgnoreGlobalCooldown = @"Ignores Global Cooldown";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString IgnoreCooldownReduction = @"Ignores Cooldown Reduction";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static Dictionary<int, LocalizedString> TargetTypes = new Dictionary<int, LocalizedString>
            {
                {0, @"Self Cast"},
                {1, @"Targetted - Range: {00} Tiles"},
                {2, @"AOE"},
                {3, @"Projectile - Range: {00} Tiles"},
                {4, @"On Hit"},
                {5, @"Trap"},
            };

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Bound = @"Can not be unlearned.";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Distance = @"Distance:";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString IgnoreMapBlock = @"Ignores Map Blocks";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString IgnoreResourceBlock = @"Ignores Active Resources";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString IgnoreZDimension = @"Ignores Height Differences";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString IgnoreConsumedResourceBlock = @"Ignores Consumed Resources";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Tiles = @"{00} Tiles";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Friendly = @"Support Spell";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Unfriendly = @"Damaging Spell";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString DamageType = @"Damage Type:";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static Dictionary<int, LocalizedString> DamageTypes = new Dictionary<int, LocalizedString>()
            {
                { 0, @"Physical" },
                { 1, @"Magic" },
                { 2, @"True" }
            };

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString CritChance = @"Critical Chance:";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString CritMultiplier = @"Critical Multiplier:";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static Dictionary<int, LocalizedString> Stats = new Dictionary<int, LocalizedString>
            {
                {0, @"Blunt Attack"},
                {1, @"Magic Attack"},
                {2, @"Blunt Resistance"},
                {3, @"Magic Resistance"},
                {4, @"Speed"},
                {5, @"Slash Attack"},
                {6, @"Slash Resistance"},
                {7, @"Pierce Attack"},
                {8, @"Pierce Resistance"},
                {9, @"Evasion"},
                {10, @"Accuracy"},
            };

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString ScalingStat = @"Scaling Stat:";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString ScalingPercentage = @"Scaling Percentage:";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString WeaponSkill = @"+ WEAPON STATS";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString HoT = @"Heals over Time";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString DoT = @"Damages over Time";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Tick = @"Ticks every:";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static Dictionary<int, LocalizedString> Effects = new Dictionary<int, LocalizedString>
            {
                {0, @""},
                {1, @"Silence"},
                {2, @"Stun"},
                {3, @"Snare"},
                {4, @"Blind"},
                {5, @"Stealth"},
                {6, @"Transforms"},
                {7, @"Cleanse"},
                {8, @"Invulnerable"},
                {9, @"Shield"},
                {10, @"Sleep"},
                {11, @"On-Hit"},
                {12, @"Taunt"},
                {13, @"Swift"},
                {14, @"Accurate (stackable)"},
                {15, @"Haste"},
                {16, @"Slowed"},
                {17, @"Confused"},
                {18, @"Steady"},
                {19, @"Attuned (stackable)"},
                {20, @"Enfeebled"},
                {21, @"EXP Boost (stackable)"},
                {22, @"Angler (stackable)"},
                {23, @"Lumberjack (stackable)"},
                {24, @"Prospector (stackable)"},
            };

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Effect = @"Effect:";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Duration = @"Duration:";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString ShieldSize = @"Shield Size:";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static Dictionary<int, LocalizedString> StatCounts = new Dictionary<int, LocalizedString>
            {
                {0, @"Blunt Atk:"},
                {1, @"Magic Atk:"},
                {2, @"Blunt Def:"},
                {3, @"Magic Def:"},
                {4, @"Speed:"},
                {5, @"Slash Atk:"},
                {6, @"Slash Def:"},
                {7, @"Pierce Atk:"},
                {8, @"Pierce Def:"},
                {9, @"Evasion:"},
                {10, @"Accuracy:"},
            };

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString RegularAndPercentage = @"{00} + {01}%";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString StatBuff = @"Stat Buff";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString HitRadius = @"Hit Radius:";

            public static LocalizedString addsymbol = @"+";

            public static Dictionary<int, LocalizedString> effectlist = new Dictionary<int, LocalizedString>
            {
                {0, @""},
                {1, @"Silences Target"},
                {2, @"Stuns Target"},
                {3, @"Snares Target"},
                {4, @"Blinds Target"},
                {5, @"Stealths Target"},
                {6, @"Transforms Target"},
                {7, @"Cleanses Target"},
                {8, @"Target becomes Invulnerable"},
                {9, @"Shields Target"},
                {10, @"Makes the target fall asleep"},
                {11, @"Applies an On Hit effect to the target"},
                {12, @"Taunts Target"},
                {13, @"Swiftens Target"},
                {14, @"Increases Target Crit Chance"},
                {15, @"Increases target movement speed"},
                {16, @"Decreases target movement speed"},
                {17, @"Confuses target"},
                {18, @"Nullifies knockback"},
                {19, @"Decreases mana consumption"},
                {20, @"Removes mana regen"},
                {21, @"Increases EXP given from killing enemies"},
                {22, @"Increases harvest speed when using a fishing rod"},
                {23, @"Increases harvest speed when using an axe"},
                {24, @"Increases harvest speed when using a pickaxe"},
            };

        }

        public struct Spells
        {

            public static LocalizedString cooldown = "{00}s";

            public static LocalizedString forgetspell = @"Forget Spell";

            public static LocalizedString forgetspellprompt = @"Are you sure you want to forget {00}?";

            public static LocalizedString title = @"Spells";
            
            public static LocalizedString targetneeded = @"You must have a target selected to cast this spell.";

            public static LocalizedString norunes = @"You don't have the casting components necessary to cast this spell!";

            public static LocalizedString silenced = @"You're silenced and can't cast spells!";
            
            public static LocalizedString stunned = @"You're stunned and can not move or attack!";
            
            public static LocalizedString sleep = @"You're asleep and can not move, use items, or attack!";
            
            public static LocalizedString blind = @"You're blinded, and your attack misses!";

            public static LocalizedString confused = @"You're confused, and your accuracy is affected!";

            public static LocalizedString snared = @"You're snared and can not move!";
        }

        public struct Trading
        {

            public static LocalizedString accept = @"Accept";

            public static LocalizedString infight = @"You are currently fighting!";

            public static LocalizedString offeritem = @"Offer Item";

            public static LocalizedString offeritemprompt = @"How many/much {00} would you like to offer?";

            public static LocalizedString pending = @"Pending";

            public static LocalizedString requestprompt =
                @"{00} has invited you to trade items with them. Do you accept?";

            public static LocalizedString revokeitem = @"Revoke Item";

            public static LocalizedString revokeitemprompt = @"How many/much {00} would you like to revoke?";

            public static LocalizedString theiroffer = @"Their Offer:";

            public static LocalizedString title = @"Trading with {00}";

            public static LocalizedString traderequest = @"Trading Invite";

            public static LocalizedString value = @"Value: {00}";

            public static LocalizedString youroffer = @"Your Offer:";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString OtherPlayer = @"The other player has accepted the trade.";

        }

        public partial struct EscapeMenu
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString CharacterSelect = @"Characters";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Close = @"Close";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString ExitToDesktop = @"Desktop";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Logout = @"Logout";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Settings = @"Settings";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Title = @"Menu";
        }

        public struct Numbers
        {

            public static LocalizedString thousands = "k";

            public static LocalizedString millions = "m";

            public static LocalizedString billions = "b";

            public static LocalizedString dec = ".";

            public static LocalizedString comma = ",";

        }

        public struct Update
        {

            public static LocalizedString Checking = @"Checking for updates, please wait!";

            public static LocalizedString Updating = @"Downloading updates, please wait!";

            public static LocalizedString Restart = @"Update complete! Relaunch {00} to play!";

            public static LocalizedString Done = @"Update complete! Launching game!";

            public static LocalizedString Error = @"Update Error! Check logs for more info!";

            public static LocalizedString Files = @"{00} Files Remaining";

            public static LocalizedString Size = @"{00} Left";

            public static LocalizedString Percent = @"{00}%";

        }

        public struct HarvestBonus
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Bonus = @"Current Harvest Speed Bonus: {00}%";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Remaining = @"Required harvests until next bonus: {00}";
        }

        public struct GameWindow
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString EntityNameAndLevel = @"{00} [Lv. {01}]";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString NpcNameAndLevel = @"{00} [Tr. {01}]";
        }

    }

    public static partial class Strings
    {
        public struct LifeCounterWindow
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Label = @"";
        }

        public struct TimerWindow
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString ElapsedMinutes = @"{0:D1}:{1:D2}.{2:D1}";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString ElapsedHours = @"{0:D1}:{1:D2}:{2:D2}.{3:D1}";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString ElapsedDays = @"{0:D1}D {1:D2}:{2:D2}";
        }

        public partial struct GameMenu
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Bestiary = @"Bestiary";
            
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Map = @"Overworld Map";
        }

        public partial struct EscapeMenu
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString DesktopWarningTitle = @"EXIT TO DESKTOP";
            
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString DesktopWarningPrompt = @"Are you sure you want to exit to desktop?";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString LogoutTitle = @"LOGOUT";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString LogoutPrompt = @"Are you sure you want to logout?";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString CharSelectTitle = @"CHARACTER CHANGE";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString CharSelectPrompt = @"Are you sure you want switch characters?";
        }

        public partial struct CharacterCreation
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString ClassInfoMage = @"The mage excels at ranged combat, preferring to keep enemies at a distance as they are susceptible to receiving a lot of damage should an enemy get close.\nThey can play both a high damage role and a high support role depending on their staff selection.";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString ClassInfoRogue = @"The rogue is all about high damage-per-second and mobility.\nA rogue with a dagger can stack backstab damage with critical hits or stealth attacks to deal incredible burst damage.\nA rogue with a bow can keep enemies at bay while staying safe from harm.";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString ClassInfoWarrior = @"A warrior is built to withstand damage, and deal it back with incredible blows.\nA warrior wielding a shield can provide boosts to surrounding players, and learn ranged and crowd control magic pwoers.\nA warrior wielding a 2-handed weapon can deal massive damage to a crowd with the sword's multi-target swipe.";

        }
        
        public partial struct Leaderboard
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString CacheWarning = @"Results may take up to {00} minutes to show accurately due to caching.";
            
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString ElapsedMinutes = @"{0:D1}:{1:D2}.{2:D3}";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString ElapsedHours = @"{0:D1}:{1:D2}:{2:D2}.{3:D3}";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString ElapsedDays = @"{0:D1} Day(s) and {1:D2}:{2:D2}:{3:D2}.{4:D3}";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString First = @"First";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Loading = @"Fetching records...";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Next = @"Next";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString NoRecords = @"No records found!";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Page = @"Page {00}";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Prev = @"Prev";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Record = @"Record";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString RecordHolder = @"Record Holder";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString SearchButton = @"Search";
        }

        public partial struct LootRoll
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString BankAll = @"Bank All";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString BankItem = @"Send to Bank";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString DismissPrompt = @"Are you sure you wish to dismiss the remaining loot? All remaining items will be permanently lost.";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString DismissTitle = @"Dismiss All";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString DismissItem = @"Dismiss Item";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString DismissItemPrompt = @"Are you sure you wish to dismiss the item: {00}? The item will be permanently lost.";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString DismissItemTitle = @"Dismiss Item";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString DismissRemaining = @"Dismiss";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString TakeAll = @"Take All";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString TakeItem = @"Take Item";
        }

        public partial struct RespawnWindow
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString DeathDungeon = @"You've lost a life! Your party has {00} remaining.";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString DeathDungeonSolo = @"You've lost a life! You have {00} remaining.";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString DeathDungeonFinal = @"You've lost your last life! You can no longer respawn in this instance, and must reset.";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString DeathItems = @"You've died! You've lost {00} experience points and some of your items...";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString DeathPvE = @"You've died! You've lost {00} experience points...";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString DeathDuel = @"You have lost your duel!";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString DeathSafe = @"You've died!";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString InstanceRespawnButton = @"Respawn";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString LeaveInstanceButton = @"Leave";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString LeaveInstancePrompt = @"Are you sure you want to leave this instance? You will not be able to come back.";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString LeaveInstanceTitle = @"Leave Instance";


            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString RespawnButton = @"Respawn";
        }

        public partial struct Bestiary
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString BeastNotFound = @"This beast can not be found. Try reloading the bestiary.";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Unknown = @"???";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString BeastLocked = @"You haven't slain this beast, or haven't yet found any information about them.";
        }

        public partial struct EnhancementWindow
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Enhancements = "Enhancement";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString EPRemaining = "Enhancement Points Remaining: {00}";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString EP = "EP";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString AppliedEnhancemnets = "Applied Enhancements";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString Title = "WEAPON ENHANCEMENT";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString NoWeaponEquipped = "NO EQUIPPED WEAPON";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString NoWeaponEquippedPrompt = "You must have a weapon equipped to use the weapon enhancement station.";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString NoEnhancements = "No enhancements available!";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString RemoveEnhancements = "RESET ITEM";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString RemoveEnhancementsPrompt = "Removing all enhancements from this weapon will cost {00} {01}. Would you like to proceed?";
        }

        public partial struct UpgradeStation
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString NoWeaponEquipped = "NO EQUIPPED WEAPON";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString NoWeaponEquippedPrompt = "You must have a weapon equipped to use the weapon upgrade station.";
        }

        public partial struct Loadouts
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString NewLoadoutTitle = "NEW LOADOUT";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString NewLoadoutPrompt = "What name would you like to give your current assignment of skills?";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString OverwriteLoadoutTitle = "OVERWRITE LOADOUT";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString OverwriteLoadoutPrompt = "Are you sure you want to overwrite the loadout: {00}?";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString OverwriteLoadoutPromptAlreadyExists = "A loadout with the name {00} already exists. Would you like to overwrite it?";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString RemoveLoadout = "REMOVE LOADOUT";

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public static LocalizedString RemoveLoadoutPrompt = "Are you sure you want to remove the loadout: {00}?";
        }
    }
}
