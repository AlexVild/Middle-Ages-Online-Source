using System;
using System.Collections.Generic;

using Intersect.Enums;
using Intersect.GameObjects.Conditions;
using Intersect.GameObjects.Timers;
using Newtonsoft.Json;

namespace Intersect.GameObjects.Events.Commands
{

    public abstract class EventCommand
    {

        public abstract EventCommandType Type { get; }

        public virtual string GetCopyData(
            Dictionary<Guid, List<EventCommand>> commandLists,
            Dictionary<Guid, List<EventCommand>> copyLists
        )
        {
            return JsonConvert.SerializeObject(
                this, typeof(EventCommand),
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.None, TypeNameHandling = TypeNameHandling.Auto,
                    DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                }
            );
        }

        public virtual void FixBranchIds(Dictionary<Guid, Guid> idDict)
        {
        }

    }

    public partial class ShowTextCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.ShowText;

        public string Text { get; set; } = "";

        public string Face { get; set; } = "";

    }

    public partial class ShowOptionsCommand : EventCommand
    {

        //For Json Deserialization
        public ShowOptionsCommand()
        {
        }

        public ShowOptionsCommand(Dictionary<Guid, List<EventCommand>> commandLists)
        {
            for (var i = 0; i < BranchIds.Length; i++)
            {
                BranchIds[i] = Guid.NewGuid();
                commandLists.Add(BranchIds[i], new List<EventCommand>());
            }
        }

        public override EventCommandType Type { get; } = EventCommandType.ShowOptions;

        public string Text { get; set; } = "";

        public string[] Options { get; set; } = new string[4];

        //Id of the command list(s) you follow when a particular option is selected
        public Guid[] BranchIds { get; set; } = new Guid[4];

        public string Face { get; set; } = "";

        public override string GetCopyData(
            Dictionary<Guid, List<EventCommand>> commandLists,
            Dictionary<Guid, List<EventCommand>> copyLists
        )
        {
            foreach (var branch in BranchIds)
            {
                if (branch != Guid.Empty && commandLists.ContainsKey(branch))
                {
                    copyLists.Add(branch, commandLists[branch]);
                    foreach (var cmd in commandLists[branch])
                    {
                        cmd.GetCopyData(commandLists, copyLists);
                    }
                }
            }

            return base.GetCopyData(commandLists, copyLists);
        }

        public override void FixBranchIds(Dictionary<Guid, Guid> idDict)
        {
            for (var i = 0; i < BranchIds.Length; i++)
            {
                if (idDict.ContainsKey(BranchIds[i]))
                {
                    BranchIds[i] = idDict[BranchIds[i]];
                }
            }
        }

    }

    public class InputVariableCommand : EventCommand
    {

        //For Json Deserialization
        public InputVariableCommand()
        {
        }

        public InputVariableCommand(Dictionary<Guid, List<EventCommand>> commandLists)
        {
            for (var i = 0; i < BranchIds.Length; i++)
            {
                BranchIds[i] = Guid.NewGuid();
                commandLists.Add(BranchIds[i], new List<EventCommand>());
            }
        }

        public override EventCommandType Type { get; } = EventCommandType.InputVariable;

        public string Title { get; set; }

        public string Text { get; set; } = "";

        public VariableTypes VariableType { get; set; } = VariableTypes.PlayerVariable;

        public Guid VariableId { get; set; } = new Guid();

        public long Minimum { get; set; } = 0;

        public long Maximum { get; set; } = 0;

        public Guid[] BranchIds { get; set; } =
            new Guid[2]; //Branch[0] is the event commands to execute when the condition is met, Branch[1] is for when it's not.

        public override string GetCopyData(
            Dictionary<Guid, List<EventCommand>> commandLists,
            Dictionary<Guid, List<EventCommand>> copyLists
        )
        {
            foreach (var branch in BranchIds)
            {
                if (branch != Guid.Empty && commandLists.ContainsKey(branch))
                {
                    copyLists.Add(branch, commandLists[branch]);
                    foreach (var cmd in commandLists[branch])
                    {
                        cmd.GetCopyData(commandLists, copyLists);
                    }
                }
            }

            return base.GetCopyData(commandLists, copyLists);
        }

        public override void FixBranchIds(Dictionary<Guid, Guid> idDict)
        {
            for (var i = 0; i < BranchIds.Length; i++)
            {
                if (idDict.ContainsKey(BranchIds[i]))
                {
                    BranchIds[i] = idDict[BranchIds[i]];
                }
            }
        }

    }

    public class AddChatboxTextCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.AddChatboxText;

        public string Text { get; set; } = "";

        // TODO: Expose this option to the user?
        public ChatMessageType MessageType { get; set; } = ChatMessageType.Notice;

        public string Color { get; set; } = "";

        public ChatboxChannel Channel { get; set; } = ChatboxChannel.Player;

        public bool SendToast { get; set; } = false;

    }

    public partial class SetVariableCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.SetVariable;

        public VariableTypes VariableType { get; set; } = VariableTypes.PlayerVariable;

        public Guid VariableId { get; set; }

        public bool SyncParty { get; set; }

        public VariableMod Modification { get; set; }

    }

    public class SetSelfSwitchCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.SetSelfSwitch;

        public int SwitchId { get; set; } //0 through 3

        public bool Value { get; set; }

    }

    public class ConditionalBranchCommand : EventCommand
    {

        //For Json Deserialization
        public ConditionalBranchCommand()
        {
        }

        public ConditionalBranchCommand(Dictionary<Guid, List<EventCommand>> commandLists)
        {
            for (var i = 0; i < BranchIds.Length; i++)
            {
                BranchIds[i] = Guid.NewGuid();
                commandLists.Add(BranchIds[i], new List<EventCommand>());
            }
        }

        public override EventCommandType Type { get; } = EventCommandType.ConditionalBranch;

        public Condition Condition { get; set; }

        public Guid[] BranchIds { get; set; } =
            new Guid[2]; //Branch[0] is the event commands to execute when the condition is met, Branch[1] is for when it's not.

        public override string GetCopyData(
            Dictionary<Guid, List<EventCommand>> commandLists,
            Dictionary<Guid, List<EventCommand>> copyLists
        )
        {
            foreach (var branch in BranchIds)
            {
                if (branch != Guid.Empty && commandLists.ContainsKey(branch))
                {
                    copyLists.Add(branch, commandLists[branch]);
                    foreach (var cmd in commandLists[branch])
                    {
                        cmd.GetCopyData(commandLists, copyLists);
                    }
                }
            }

            return base.GetCopyData(commandLists, copyLists);
        }

        public override void FixBranchIds(Dictionary<Guid, Guid> idDict)
        {
            for (var i = 0; i < BranchIds.Length; i++)
            {
                if (idDict.ContainsKey(BranchIds[i]))
                {
                    BranchIds[i] = idDict[BranchIds[i]];
                }
            }
        }

    }

    public class ExitEventProcessingCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.ExitEventProcess;

    }

    public class LabelCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.Label;

        public string Label { get; set; }

    }

    public class GoToLabelCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.GoToLabel;

        public string Label { get; set; }

    }

    public class StartCommmonEventCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.StartCommonEvent;

        public Guid EventId { get; set; }

    }

    public class RestoreHpCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.RestoreHp;

        public int Amount { get; set; }

    }

    public class RestoreMpCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.RestoreMp;

        public int Amount { get; set; }

    }

    public class LevelUpCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.LevelUp;

    }

    public class GiveExperienceCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.GiveExperience;

        public long Exp { get; set; }

        /// <summary>
        /// Defines whether this event command will use a variable for processing or not.
        /// </summary>
        public bool UseVariable { get; set; } = false;

        /// <summary>
        /// Defines whether the variable used is a Player or Global variable.
        /// </summary>
        public VariableTypes VariableType { get; set; } = VariableTypes.PlayerVariable;

        /// <summary>
        /// The Variable Id to use.
        /// </summary>
        public Guid VariableId { get; set; }

    }

    public class ChangeLevelCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.ChangeLevel;

        public int Level { get; set; }

    }

    public class ChangeSpellsCommand : EventCommand
    {

        //For Json Deserialization
        public ChangeSpellsCommand()
        {
        }

        public ChangeSpellsCommand(Dictionary<Guid, List<EventCommand>> commandLists)
        {
            for (var i = 0; i < BranchIds.Length; i++)
            {
                BranchIds[i] = Guid.NewGuid();
                commandLists.Add(BranchIds[i], new List<EventCommand>());
            }
        }

        public override EventCommandType Type { get; } = EventCommandType.ChangeSpells;

        public Guid SpellId { get; set; }

        public bool Add { get; set; } //If !Add then Remove

        public Guid[] BranchIds { get; set; } =
            new Guid[2]; //Branch[0] is the event commands to execute when taught/removed successfully, Branch[1] is for when it's not.

        public override string GetCopyData(
            Dictionary<Guid, List<EventCommand>> commandLists,
            Dictionary<Guid, List<EventCommand>> copyLists
        )
        {
            foreach (var branch in BranchIds)
            {
                if (branch != Guid.Empty && commandLists.ContainsKey(branch))
                {
                    copyLists.Add(branch, commandLists[branch]);
                    foreach (var cmd in commandLists[branch])
                    {
                        cmd.GetCopyData(commandLists, copyLists);
                    }
                }
            }

            return base.GetCopyData(commandLists, copyLists);
        }

        public override void FixBranchIds(Dictionary<Guid, Guid> idDict)
        {
            for (var i = 0; i < BranchIds.Length; i++)
            {
                if (idDict.ContainsKey(BranchIds[i]))
                {
                    BranchIds[i] = idDict[BranchIds[i]];
                }
            }
        }

    }

    public partial class ChangeItemsCommand : EventCommand
    {

        //For Json Deserialization
        public ChangeItemsCommand()
        {
        }

        public ChangeItemsCommand(Dictionary<Guid, List<EventCommand>> commandLists)
        {
            for (var i = 0; i < BranchIds.Length; i++)
            {
                BranchIds[i] = Guid.NewGuid();
                commandLists.Add(BranchIds[i], new List<EventCommand>());
            }
        }

        public override EventCommandType Type { get; } = EventCommandType.ChangeItems;

        public Guid ItemId { get; set; }

        public bool Add { get; set; } //If !Add then Remove

        /// <summary>
        /// Defines how the server is supposed to handle changing the items of this request.
        /// </summary>
        public ItemHandling ItemHandling { get; set; } = ItemHandling.Normal;

        /// <summary>
        /// Defines whether this event command will use a variable for processing or not.
        /// </summary>
        public bool UseVariable { get; set; } = false;

        /// <summary>
        /// Defines whether the variable used is a Player or Global variable.
        /// </summary>
        public VariableTypes VariableType { get; set; } = VariableTypes.PlayerVariable;

        /// <summary>
        /// The Variable Id to use.
        /// </summary>
        public Guid VariableId { get; set; }

        public int Quantity { get; set; }

        public Guid[] BranchIds { get; set; } =
            new Guid[2]; //Branch[0] is the event commands to execute when given/taken successfully, Branch[1] is for when they're not.

        public override string GetCopyData(
            Dictionary<Guid, List<EventCommand>> commandLists,
            Dictionary<Guid, List<EventCommand>> copyLists
        )
        {
            foreach (var branch in BranchIds)
            {
                if (branch != Guid.Empty && commandLists.ContainsKey(branch))
                {
                    copyLists.Add(branch, commandLists[branch]);
                    foreach (var cmd in commandLists[branch])
                    {
                        cmd.GetCopyData(commandLists, copyLists);
                    }
                }
            }

            return base.GetCopyData(commandLists, copyLists);
        }

        public override void FixBranchIds(Dictionary<Guid, Guid> idDict)
        {
            for (var i = 0; i < BranchIds.Length; i++)
            {
                if (idDict.ContainsKey(BranchIds[i]))
                {
                    BranchIds[i] = idDict[BranchIds[i]];
                }
            }
        }

    }

    public class EquipItemCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.EquipItem;

        public Guid ItemId { get; set; }

        public bool Unequip { get; set; }

    }

    public class ChangeSpriteCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.ChangeSprite;

        public string Sprite { get; set; } = "";

    }

    public class ChangeNameColorCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.ChangeNameColor;

        public Color Color { get; set; }

        public bool Override { get; set; }

        public bool Remove { get; set; }

    }

    public class ChangePlayerLabelCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.PlayerLabel;

        public string Value { get; set; }

        public int Position { get; set; } //0 = Above Player Name, 1 = Below Player Name

        public Color Color { get; set; }

        public bool MatchNameColor { get; set; }

    }

    public class ChangeFaceCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.ChangeFace;

        public string Face { get; set; } = "";

    }

    public class ChangeGenderCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.ChangeGender;

        public Gender Gender { get; set; } = Gender.Male;

    }

    public class SetAccessCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.SetAccess;

        public Access Access { get; set; }

    }

    public class WarpCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.WarpPlayer;

        public Guid MapId { get; set; }

        public byte X { get; set; }

        public byte Y { get; set; }

        public WarpDirection Direction { get; set; } = WarpDirection.Retain;

        public bool FadeOnWarp { get; set; } = false;

        /// <summary>
        /// Whether or not the warp event will change a player's map instance settings
        /// </summary>
        public bool ChangeInstance { get; set; } = false;

        /// <summary>
        /// The <see cref="MapInstanceType"/> we are going to be warping to
        /// </summary>
        public MapInstanceType InstanceType { get; set; } = MapInstanceType.Overworld;

        public Guid DungeonId { get; set; } = Guid.Empty;
    }

    public class SetMoveRouteCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.SetMoveRoute;

        public EventMoveRoute Route { get; set; } = new EventMoveRoute();

    }

    public class WaitForRouteCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.WaitForRouteCompletion;

        public Guid TargetId { get; set; }

    }

    public class SpawnNpcCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.SpawnNpc;

        public Guid NpcId { get; set; }

        public byte Dir { get; set; }

        //Tile Spawn Variables  (Will spawn on map tile if mapid is not empty)
        public Guid MapId { get; set; }

        //Entity Spawn Variables (Will spawn on/around entity if entityId is not empty)
        public Guid EntityId { get; set; }

        //Map Coords or Coords Centered around player to spawn at
        public sbyte X { get; set; }

        public sbyte Y { get; set; }

    }

    public class DespawnNpcCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.DespawnNpc;

        //No parameters, only despawns npcs that have been spawned via events for the player

    }

    public class PlayAnimationCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.PlayAnimation;

        public Guid AnimationId { get; set; }

        public byte Dir { get; set; }

        //Tile Spawn Variables  (Will spawn on map tile if mapid is not empty)
        public Guid MapId { get; set; }

        //Entity Spawn Variables (Will spawn on/around entity if entityId is not empty)
        public Guid EntityId { get; set; }

        //Map Coords or Coords Centered around player to spawn at
        public sbyte X { get; set; }

        public sbyte Y { get; set; }

        public bool Instanced { get; set; }

    }

    public class HoldPlayerCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.HoldPlayer;

    }

    public class ReleasePlayerCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.ReleasePlayer;

    }

    public class HidePlayerCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.HidePlayer;

    }

    public class ShowPlayerCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.ShowPlayer;

    }

    public class PlayBgmCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.PlayBgm;

        public string File { get; set; } = "";

    }

    public class FadeoutBgmCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.FadeoutBgm;

    }

    public class PlaySoundCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.PlaySound;

        public string File { get; set; } = "";

    }

    public class StopSoundsCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.StopSounds;

    }

    public class ShowPictureCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.ShowPicture;

        /// <summary>
        /// Picture filename to show.
        /// </summary>
        public string File { get; set; } = "";

        /// <summary>
        /// How the picture is rendered on the screen.
        /// </summary>
        public int Size { get; set; } //Original = 0, Full Screen, Half Screen, Stretch To Fit  //TODO Enum this?

        /// <summary>
        /// If true the picture will close upon being clicked
        /// </summary>
        public bool Clickable { get; set; }

        /// <summary>
        /// If not 0 the picture will go away after shown for the time below
        /// </summary>
        public int HideTime { get; set; } = 0;

        /// <summary>
        /// If true this event won't continue with commands until this picture is closed.
        /// </summary>
        public bool WaitUntilClosed { get; set; }

    }

    public class HidePictureCommmand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.HidePicture;

    }


    public class FlashScreenCommand : EventCommand
    {
        public override EventCommandType Type { get; } = EventCommandType.FlashScreen;

        public float Duration { get; set; } = 200f;

        public float Intensity { get; set; } = 255f;
        
        public Color FlashColor { get; set; }
    }

    public class WaitCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.Wait;

        public int Time { get; set; }

    }

    public class OpenBankCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.OpenBank;

    }

    public class OpenShopCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.OpenShop;

        public Guid ShopId { get; set; }

    }

    public class OpenCraftingTableCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.OpenCraftingTable;

        public Guid CraftingTableId { get; set; }

    }

    public class SetClassCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.SetClass;

        public Guid ClassId { get; set; }

    }

    public class StartQuestCommand : EventCommand
    {

        //For Json Deserialization
        public StartQuestCommand()
        {
        }

        public StartQuestCommand(Dictionary<Guid, List<EventCommand>> commandLists)
        {
            for (var i = 0; i < BranchIds.Length; i++)
            {
                BranchIds[i] = Guid.NewGuid();
                commandLists.Add(BranchIds[i], new List<EventCommand>());
            }
        }

        public override EventCommandType Type { get; } = EventCommandType.StartQuest;

        public Guid QuestId { get; set; }

        public bool Offer { get; set; } //Show the offer screen and give the player a chance to decline the quest

        public Guid[] BranchIds { get; set; } =
            new Guid[2]; //Branch[0] is the event commands to execute when quest is started successfully, Branch[1] is for when it's not.

        public override string GetCopyData(
            Dictionary<Guid, List<EventCommand>> commandLists,
            Dictionary<Guid, List<EventCommand>> copyLists
        )
        {
            foreach (var branch in BranchIds)
            {
                if (branch != Guid.Empty && commandLists.ContainsKey(branch))
                {
                    copyLists.Add(branch, commandLists[branch]);
                    foreach (var cmd in commandLists[branch])
                    {
                        cmd.GetCopyData(commandLists, copyLists);
                    }
                }
            }

            return base.GetCopyData(commandLists, copyLists);
        }

        public override void FixBranchIds(Dictionary<Guid, Guid> idDict)
        {
            for (var i = 0; i < BranchIds.Length; i++)
            {
                if (idDict.ContainsKey(BranchIds[i]))
                {
                    BranchIds[i] = idDict[BranchIds[i]];
                }
            }
        }

    }

    public class CompleteQuestTaskCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.CompleteQuestTask;

        public Guid QuestId { get; set; }

        public Guid TaskId { get; set; }
        
        public bool SkipCompletionEvent { get; set; }
        
        public bool DoNotNotify { get; set; }

    }

    public class EndQuestCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.EndQuest;

        public Guid QuestId { get; set; }

        public bool SkipCompletionEvent { get; set; }
        
        public bool ResetQuest { get; set; }
        
        public bool AgnosticStop { get; set; }

    }

    /// <summary>
    /// Defines the Event command class for the Change Player Color command.
    /// </summary>
    public class ChangePlayerColorCommand : EventCommand
    {

        /// <summary>
        /// The <see cref="EventCommandType"/> of this command.
        /// </summary>
        public override EventCommandType Type { get; } = EventCommandType.ChangePlayerColor;

        /// <summary>
        /// The <see cref="Color"/> to apply to the player.
        /// </summary>
        public Color Color { get; set; } = new Color(255, 255, 255, 255);

    }

    public class ChangeNameCommand : EventCommand
    {
        //For Json Deserialization
        public ChangeNameCommand()
        {
        }

        public ChangeNameCommand(Dictionary<Guid, List<EventCommand>> commandLists)
        {
            for (var i = 0; i < BranchIds.Length; i++)
            {
                BranchIds[i] = Guid.NewGuid();
                commandLists.Add(BranchIds[i], new List<EventCommand>());
            }
        }

        public override EventCommandType Type { get; } = EventCommandType.ChangeName;

        public Guid VariableId { get; set; }

        public Guid[] BranchIds { get; set; } =
            new Guid[2]; //Branch[0] is the event commands to execute when given/taken successfully, Branch[1] is for when they're not.

        public override string GetCopyData(
            Dictionary<Guid, List<EventCommand>> commandLists,
            Dictionary<Guid, List<EventCommand>> copyLists
        )
        {
            foreach (var branch in BranchIds)
            {
                if (branch != Guid.Empty && commandLists.ContainsKey(branch))
                {
                    copyLists.Add(branch, commandLists[branch]);
                    foreach (var cmd in commandLists[branch])
                    {
                        cmd.GetCopyData(commandLists, copyLists);
                    }
                }
            }

            return base.GetCopyData(commandLists, copyLists);
        }


        public override void FixBranchIds(Dictionary<Guid, Guid> idDict)
        {
            for (var i = 0; i < BranchIds.Length; i++)
            {
                if (idDict.ContainsKey(BranchIds[i]))
                {
                    BranchIds[i] = idDict[BranchIds[i]];
                }
            }
        }
    }

    public class CreateGuildCommand : EventCommand
    {

        //For Json Deserialization
        public CreateGuildCommand()
        {
        }

        public CreateGuildCommand(Dictionary<Guid, List<EventCommand>> commandLists)
        {
            for (var i = 0; i < BranchIds.Length; i++)
            {
                BranchIds[i] = Guid.NewGuid();
                commandLists.Add(BranchIds[i], new List<EventCommand>());
            }
        }

        public override EventCommandType Type { get; } = EventCommandType.CreateGuild;

        public Guid VariableId { get; set; }

        public Guid[] BranchIds { get; set; } =
            new Guid[2]; //Branch[0] is the event commands to execute when quest is started successfully, Branch[1] is for when it's not.

        public override string GetCopyData(
            Dictionary<Guid, List<EventCommand>> commandLists,
            Dictionary<Guid, List<EventCommand>> copyLists
        )
        {
            foreach (var branch in BranchIds)
            {
                if (branch != Guid.Empty && commandLists.ContainsKey(branch))
                {
                    copyLists.Add(branch, commandLists[branch]);
                    foreach (var cmd in commandLists[branch])
                    {
                        cmd.GetCopyData(commandLists, copyLists);
                    }
                }
            }

            return base.GetCopyData(commandLists, copyLists);
        }
    }

    public class DisbandGuildCommand : EventCommand
    {

        //For Json Deserialization
        public DisbandGuildCommand()
        {
        }

        public DisbandGuildCommand(Dictionary<Guid, List<EventCommand>> commandLists)
        {
            for (var i = 0; i < BranchIds.Length; i++)
            {
                BranchIds[i] = Guid.NewGuid();
                commandLists.Add(BranchIds[i], new List<EventCommand>());
            }
        }

        public override EventCommandType Type { get; } = EventCommandType.DisbandGuild;

        public Guid[] BranchIds { get; set; } =
            new Guid[2]; //Branch[0] is the event commands to execute when quest is started successfully, Branch[1] is for when it's not.

        public override string GetCopyData(
            Dictionary<Guid, List<EventCommand>> commandLists,
            Dictionary<Guid, List<EventCommand>> copyLists
        )
        {
            foreach (var branch in BranchIds)
            {
                if (branch != Guid.Empty && commandLists.ContainsKey(branch))
                {
                    copyLists.Add(branch, commandLists[branch]);
                    foreach (var cmd in commandLists[branch])
                    {
                        cmd.GetCopyData(commandLists, copyLists);
                    }
                }
            }

            return base.GetCopyData(commandLists, copyLists);
        }

        public override void FixBranchIds(Dictionary<Guid, Guid> idDict)
        {
            for (var i = 0; i < BranchIds.Length; i++)
            {
                if (idDict.ContainsKey(BranchIds[i]))
                {
                    BranchIds[i] = idDict[BranchIds[i]];
                }
            }
        }

    }

    public class OpenGuildBankCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.OpenGuildBank;

    }

    public class SetGuildBankSlotsCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.SetGuildBankSlots;

        public VariableTypes VariableType { get; set; }

        public Guid VariableId { get; set; }

    }

    public class ResetStatPointAllocationsCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.ResetStatPointAllocations;

    }

    public class RandomQuestCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.RandomQuest;

        public Guid QuestListId { get; set; }
    }

    public class OpenQuestBoardCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.OpenQuestBoard;

        public Guid QuestBoardId { get; set; }
    }

    public class SetVehicleCommand : EventCommand
    {
        public override EventCommandType Type { get; } = EventCommandType.SetVehicle;

        public bool InVehicle { get; set; }

        public string VehicleSprite { get; set; }

        public long VehicleSpeed { get; set; }
    }

    public class NPCGuildManagementCommand : EventCommand
    {
        public override EventCommandType Type { get; } = EventCommandType.NPCGuildManagement;

        public Guid ClassId { get; set; }

        public NPCGuildManagementSelection Selection { get; set; }

        public bool SelectionValue { get; set; }
        
        public int Value { get; set; }
    }

    public class AddInspirationCommand : EventCommand
    {
        public override EventCommandType Type { get; } = EventCommandType.AddInspiration;

        public long Seconds { get; set; }
    }

    public abstract class TimerCommand : EventCommand
    {
        public Guid DescriptorId { get; set; }
    }

    public class StartTimerCommand : TimerCommand
    {
        public override EventCommandType Type { get; } = EventCommandType.StartTimer;   
    }

    public class ModifyTimerCommand : TimerCommand
    {
        public override EventCommandType Type { get; } = EventCommandType.ModifyTimer;

        public TimerOperator Operator { get; set; }

        public int Amount { get; set; } = 1;

        public bool IsStatic { get; set; } = true;

        public VariableTypes VariableType { get; set; }
        
        public Guid VariableDescriptorId { get; set; }
    }

    public class StopTimerCommand : TimerCommand
    {
        public override EventCommandType Type { get; } = EventCommandType.StopTimer;

        public TimerStopType StopType { get; set; }
    }

    public enum ChangeSpawnOperator
    {
        SET,
        ADD,
        SUBTRACT
    }

    public class ChangeSpawnGroupCommand : EventCommand
    {
        public override EventCommandType Type { get; } = EventCommandType.ChangeSpawnGroup;

        public Guid MapId { get; set; }

        public bool UsePlayerMap { get; set; }

        public bool SurroundingMaps { get; set; }

        public int SpawnGroup { get; set; }

        public bool ResetNpcs { get; set; }

        public bool PersistCleanup { get; set; }

        public bool ResetInvalidNpcsOnly { get; set; }

        public ChangeSpawnOperator Operator { get; set; } = ChangeSpawnOperator.SET;
    }

    public partial class ChangeItemsCommand : EventCommand
    {
        public bool Stacks { get; set; }
    }

    public partial class SetVariableCommand : EventCommand
    {
        public RecordScoring scoreType { get; set; }
        
        public bool InstanceSync { get; set; }
    }

    public enum LeaderboardDisplayMode
    {
        Value = 0,
        Time,
    }

    public class OpenLeaderboardCommand : EventCommand
    {
        public override EventCommandType Type { get; } = EventCommandType.OpenLeaderboard;

        public RecordType RecordType { get; set; }

        public Guid RecordId { get; set; }

        public RecordScoring ScoreType { get; set; }

        public string DisplayName { get; set; }

        public LeaderboardDisplayMode DisplayMode { get; set; }
    }

    public class ClearRecordCommand : EventCommand
    {
        public override EventCommandType Type { get; } = EventCommandType.ClearRecord;

        public RecordType RecordType { get; set; }

        public Guid RecordId { get; set; }

        public bool ClearAll { get; set; }
    }

    public class RollLootCommand : EventCommand
    {
        public override EventCommandType Type { get; } = EventCommandType.RollLoot;

        public List<LootRoll> LootTables { get; set; }

        public string Title { get; set; }
    }
    
    public class UnlockLabelCommand : EventCommand
    {
        public enum LabelUnlockStatus
        {
            Unlock = 0,
            Remove
        }

        public override EventCommandType Type { get; } = EventCommandType.UnlockLabel;

        public Guid LabelId { get; set; }

        public LabelUnlockStatus Status { get; set; }
    }

    public partial class ShowTextCommand : EventCommand
    {
        public enum ShowTextTemplate
        {
            ItemObtained = 0,
            QuestCompleted,
            NoSpace,
            Locked,
            Exp,
            GiveItem,
        }

        public ShowTextTemplate Template;

        public bool UseTemplate = false;
        
        public bool SendToChatbox = false;

        public Guid ItemId;

        public Guid QuestId;

        public string Color { get; set; } = "";

        public bool PlaySound { get; set; }

        public string Sound { get; set; }

        public string TemplateParam { get; set; }

        public long TemplateQuantity { get; set; }

        public ChatboxChannel Channel { get; set; } = ChatboxChannel.Player;
    }

    public partial class ResetVariableCommand : EventCommand
    {
        public override EventCommandType Type { get; } = EventCommandType.VarGroupReset;

        public string Group;

        public bool FireCommonEvents;

        public bool SyncParty;
    }

    public partial class ResetPermadeadNpcsCommand : EventCommand
    {
        public override EventCommandType Type { get; } = EventCommandType.ResetPermadeadNpcs;

        public Guid MapId;

        public bool UsePlayerMap;

        public bool RefreshSpawns;
    }

    public partial class FadeInCommand : EventCommand
    {
        public override EventCommandType Type { get; } = EventCommandType.FadeIn;
    }

    public partial class FadeOutCommand : EventCommand
    {
        public override EventCommandType Type { get; } = EventCommandType.FadeOut;
    }

    public partial class ShakeScreenCommand : EventCommand
    {
        public override EventCommandType Type { get; } = EventCommandType.ShakeScreen;

        public float Intensity { get; set; }
    }

    public partial class ChangeSpawnCommand : EventCommand
    {
        public override EventCommandType Type { get; } = EventCommandType.ChangeSpawn;

        public RespawnChangeType RespawnType { get; set; } = RespawnChangeType.Default;

        public Guid MapId { get; set; }

        public byte X { get; set; }

        public byte Y { get; set; }
        
        public Directions Direction { get; set; } = Directions.Down;

        public bool Reset { get; set; }
    }

    public partial class ShowOptionsCommand : EventCommand
    {
        public ConditionLists Opt1Conditions = new ConditionLists();
        
        public ConditionLists Opt2Conditions = new ConditionLists();

        public ConditionLists Opt3Conditions = new ConditionLists();

        public ConditionLists Opt4Conditions = new ConditionLists();
    }

    public partial class ChangeRecipeStatusCommand : EventCommand
    {
        public override EventCommandType Type { get; } = EventCommandType.ChangeRecipeStatus;

        public Guid RecipeId { get; set; }

        public bool IsUnlocked { get; set; }
    }

    public partial class ChangeBestiaryCommand : EventCommand
    {
        public override EventCommandType Type { get; } = EventCommandType.ChangeBestiary;

        public Guid NpcId { get; set; }

        public BestiaryUnlock UnlockType { get; set; }

        public bool UnlockStatus { get; set; } = true;
    }

    public partial class ChangeWeaponTrackCommand : EventCommand
    {
        public override EventCommandType Type { get; } = EventCommandType.ChangeWeaponTrack;

        public Guid WeaponTypeId { get; set; }

        public WeaponTrackUpdate ChangeType { get; set; } = WeaponTrackUpdate.SetLevel;

        public long Amount { get; set; } = 0L;
    }

    public partial class ChangeChallengeCommand : EventCommand
    {
        public override EventCommandType Type { get; } = EventCommandType.ChangeChallenge;

        public Guid ChallengeId { get; set; }

        public ChallengeUpdate ChangeType { get; set; } = ChallengeUpdate.ChangeSets;

        public int Amount { get; set; } = 0;
    }

    public partial class ChangeDungeonCommand : EventCommand
    {
        public override EventCommandType Type { get; } = EventCommandType.ChangeDungeon;

        public DungeonState State { get; set; } = DungeonState.Inactive;
    }

    public partial class ObtainTreasureGnomeCommand : EventCommand
    {
        public override EventCommandType Type { get; } = EventCommandType.ObtainGnome;
    }

    public partial class RollDungeonLoot : EventCommand
    {
        public override EventCommandType Type { get; } = EventCommandType.RollDungeonLoot;
    }

    public partial class OpenDeconstructorCommand : EventCommand
    {
        public override EventCommandType Type { get; } = EventCommandType.OpenDeconstructor;

        public float FuelCostMultiplier { get; set; } = 1.00f;

        public bool DisableBank { get; set; } = false;
    }

    public partial class ChangeEnhancementsCommand : EventCommand
    {
        public override EventCommandType Type { get; } = EventCommandType.ChangeEnhancements;

        public Guid EnhancementId { get; set; } = Guid.Empty;

        public bool Forget { get; set; } = false;
    }

    public partial class OpenEnhancementWindow : EventCommand
    {
        public override EventCommandType Type { get; } = EventCommandType.OpenEnhancementWindow;

        public Guid CurrencyItemId { get; set; } = Guid.Empty;

        public float CostMultiplier { get; set; } = 1.0f;
    }

    public partial class OpenUpgradeStation : EventCommand
    {
        public override EventCommandType Type { get; } = EventCommandType.OpenUpgradeStation;

        public Guid CurrencyItemId { get; set; } = Guid.Empty;

        public float CostMultiplier { get; set; } = 1.0f;
    }

    public partial class RemovePermabuffCommand : EventCommand
    {
        public override EventCommandType Type { get; } = EventCommandType.RemovePermabuff;

        public Guid ItemId { get; set; } = Guid.Empty;
    }

    public partial class ResetGlobalEventPositions : EventCommand
    {
        public override EventCommandType Type { get; } = EventCommandType.ResetGlobalEventPositions;

        public Guid MapId { get; set; } = Guid.Empty;
    }

    public class MeleeSignupCommand : EventCommand
    {

        public override EventCommandType Type { get; } = EventCommandType.MeleeSignup;

        public Guid DuelMapId { get; set; }
        
        public Guid RespawnMapId { get; set; }

        public byte Duelist1X { get; set; }

        public byte Duelist1Y { get; set; }
        
        public byte Duelist2X { get; set; }

        public byte Duelist2Y { get; set; }

        public byte RespawnX { get; set; }

        public byte RespawnY { get; set; }

        public byte Duelist1Dir { get; set; }
        
        public byte Duelist2Dir { get; set; }

        public byte RespawnDir { get; set; }
    }

    public class MeleeWithdrawCommand : EventCommand
    {
        public override EventCommandType Type { get; } = EventCommandType.MeleeWithdraw;
    }
    
    public class ChangeChampSettingsCommand : EventCommand
    {
        public override EventCommandType Type { get; } = EventCommandType.ChangeChampSettings;

        public bool DisableSpawns { get; set; }
    }

    public class HideEventCommand : EventCommand
    {
        public override EventCommandType Type { get; } = EventCommandType.HideEvent;

        public Guid EventId { get; set; }
    }

    public class ShowEventCommand : EventCommand
    {
        public override EventCommandType Type { get; } = EventCommandType.ShowEvent;

        public Guid EventId { get; set; }
    }
}
