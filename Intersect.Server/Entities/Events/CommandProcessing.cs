using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using Intersect.GameObjects.Events.Commands;
using Intersect.GameObjects.QuestList;
using Intersect.GameObjects.QuestBoard;
using Intersect.GameObjects.Switches_and_Variables;
using Intersect.Server.Database;
using Intersect.Server.Database.PlayerData.Players;
using Intersect.Server.Database.PlayerData.Security;
using Intersect.Server.General;
using Intersect.Server.Localization;
using Intersect.Server.Maps;
using Intersect.Server.Networking;
using Intersect.Utilities;
using Intersect.GameObjects.Timers;
using Intersect.Server.Database.PlayerData;
using Intersect.Server.Core;
using Intersect.Network.Packets.Server;
using Intersect.Server.Utilities;
using static Intersect.GameObjects.Events.Commands.ShowTextCommand;
using Intersect.Server.Entities.Combat;

namespace Intersect.Server.Entities.Events
{

    public static partial class CommandProcessing
    {

        public static void ProcessCommand(EventCommand command, Player player, Event instance)
        {
            var stackInfo = instance.CallStack.Peek();
            stackInfo.WaitingForResponse = CommandInstance.EventResponse.None;
            stackInfo.WaitingOnCommand = null;
            stackInfo.BranchIds = null;

            ProcessCommand((dynamic) command, player, instance, instance.CallStack.Peek(), instance.CallStack);

            stackInfo.CommandIndex++;
        }

        //Show Text Command
        private static void ProcessCommand(
            ShowTextCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            if (player == null)
            {
                return;
            }

            var text = command.Text;
            if (command.UseTemplate)
            {
                switch(command.Template)
                {
                    case ShowTextTemplate.ItemObtained:
                        var quantity = command.TemplateQuantity;
                        var item = ItemBase.Get(command.ItemId);
                        if (item == default)
                        {
                            item = instance.LastItem;
                            quantity = instance.LastItemQuantity;
                            if (item == default)
                            {
                                return;
                            }
                        }

                        var name = item.Name.Trim();

                        if (quantity <= 1)
                        {
                            text = Strings.TextTemplates.ItemObtained.ToString(name);
                        }
                        else
                        {
                            if (name.EndsWith("s"))
                            {
                                name = name.Substring(0, name.Length - 1);
                            }
                            text = Strings.TextTemplates.ItemObtainedMany.ToString(quantity, name);
                        }

                        if (!string.IsNullOrEmpty(item.Animation?.Sound))
                        {
                            PacketSender.SendPlaySound(player, item.Animation?.Sound);
                        }
                        else
                        {
                            PacketSender.SendPlaySound(player, Options.Instance.LootRollOpts.TakeSound);
                        }

                        PacketSender.SendEventDialog(
                                player, ParseEventText(text, player, instance), item.Icon, instance.PageInstance.Id
                            );
                        break;
                    case ShowTextTemplate.QuestCompleted:
                        var quest = QuestBase.Get(command.QuestId)?.Name;
                        if (quest == default)
                        {
                            quest = player.LastQuestCompleted;
                            return;
                        }
                        text = Strings.TextTemplates.QuestCompleted.ToString(quest);
                        PacketSender.SendEventDialog(
                            player, ParseEventText(text, player, instance), string.Empty, instance.PageInstance.Id
                        );
                        break;
                    case ShowTextTemplate.NoSpace:
                        text = Strings.TextTemplates.NoSpace;
                        PacketSender.SendPlaySound(player, Options.UIDenySound);
                        PacketSender.SendEventDialog(
                            player, ParseEventText(text, player, instance), string.Empty, instance.PageInstance.Id
                        );
                        break;
                    case ShowTextTemplate.Locked:
                        text = Strings.TextTemplates.Locked;
                        PacketSender.SendPlaySound(player, Options.Instance.ChatOpts.LockedSound);
                        PacketSender.SendEventDialog(
                            player, ParseEventText(text, player, instance), string.Empty, instance.PageInstance.Id
                        );
                        break;
                    case ShowTextTemplate.Exp:
                        text = Strings.TextTemplates.Exp.ToString(instance.LastExp);
                        PacketSender.SendEventDialog(
                            player, ParseEventText(text, player, instance), string.Empty, instance.PageInstance.Id
                        );
                        break;
                    case ShowTextTemplate.GiveItem:
                        var itemName = ItemBase.GetName(instance.LastItemId);
                        if (instance.LastItemQuantity > 1)
                        {
                            text = Strings.TextTemplates.GiveItemQuantity.ToString(command.TemplateParam, instance.LastItemQuantity, itemName);
                        }
                        else
                        {
                            text = Strings.TextTemplates.GiveItemSingle.ToString(command.TemplateParam, itemName);
                        }
                        PacketSender.SendEventDialog(
                            player, ParseEventText(text, player, instance), string.Empty, instance.PageInstance.Id
                        );
                        break;
                    default:
                        var errMsg = $"Invalid template for event text command for {player.Name}: {command.Template}";
#if DEBUG
                        throw new NotImplementedException(errMsg);
#else
                        Logging.Log.Error(errMsg);
#endif
                        break;
                }
            }
            else
            {
                text = command.Text;

                PacketSender.SendEventDialog(
                    player, ParseEventText(command.Text, player, instance), command.Face, instance.PageInstance.Id
                );
            }

            if (command.SendToChatbox)
            {
                try
                {
                    ProcessChatboxCommand(text, player, instance, command.Channel, command.Color, false);
                }
                catch (Exception e)
                {
                    Logging.Log.Error($"Failed to send chatbox message with text event for ${player.Name}");
                }
            }

            if (command.PlaySound)
            {
                PacketSender.SendPlaySound(player, command.Sound);
            }

            stackInfo.WaitingForResponse = CommandInstance.EventResponse.Dialogue;
        }

        //Show Options Command
        private static void ProcessCommand(
            ShowOptionsCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            var txt = ParseEventText(command.Text, player, instance);
            
            stackInfo.WaitingForResponse = CommandInstance.EventResponse.Dialogue;
            stackInfo.WaitingOnCommand = command;
            stackInfo.BranchIds = command.BranchIds;

            if (string.IsNullOrEmpty(command.Options[0]))
            {
                PacketSender.SendEventDialog(player, txt, string.Empty, string.Empty, string.Empty, string.Empty, command.Face, instance.PageInstance.Id);
            }
            else
            {
                var opt1 = command.Opt1Conditions == null || Conditions.MeetsConditionLists(command.Opt1Conditions, player, instance) ? ParseEventText(command.Options[0], player, instance) : string.Empty;
                var opt2 = command.Opt2Conditions == null || Conditions.MeetsConditionLists(command.Opt2Conditions, player, instance) ? ParseEventText(command.Options[1], player, instance) : string.Empty;
                var opt3 = command.Opt3Conditions == null || Conditions.MeetsConditionLists(command.Opt3Conditions, player, instance) ? ParseEventText(command.Options[2], player, instance) : string.Empty;
                var opt4 = command.Opt4Conditions == null || Conditions.MeetsConditionLists(command.Opt4Conditions, player, instance) ? ParseEventText(command.Options[3], player, instance) : string.Empty;
                PacketSender.SendEventDialog(player, txt, opt1, opt2, opt3, opt4, command.Face, instance.PageInstance.Id);
            }
        }

        //Input Variable Command
        private static void ProcessCommand(
            InputVariableCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            var title = ParseEventText(command.Title, player, instance);
            var txt = ParseEventText(command.Text, player, instance);
            var type = -1;

            if (command.VariableType == VariableTypes.PlayerVariable)
            {
                var variable = PlayerVariableBase.Get(command.VariableId);
                if (variable != null)
                {
                    type = (int)variable.Type;
                }
            }
            else if (command.VariableType == VariableTypes.ServerVariable)
            {
                var variable = ServerVariableBase.Get(command.VariableId);
                if (variable != null)
                {
                    type = (int)variable.Type;
                }
            }
            else if (command.VariableType == VariableTypes.InstanceVariable && MapController.TryGetInstanceFromMap(player.MapId, player.MapInstanceId, out var mapInstance))
            {
                var variable = mapInstance.GetInstanceVariable(command.VariableId);
                if (variable != null)
                {
                    type = (int)InstanceVariableBase.Get(command.VariableId).Type;
                }
            }

            if (type == -1)
            {
                var tmpStack = new CommandInstance(stackInfo.Page, command.BranchIds[1]);
                callStack.Push(tmpStack);
                return;
            }

            PacketSender.SendInputVariableDialog(player, title, txt, (VariableDataTypes)type, instance.PageInstance.Id);
            stackInfo.WaitingForResponse = CommandInstance.EventResponse.Dialogue;
            stackInfo.WaitingOnCommand = command;
            stackInfo.BranchIds = command.BranchIds;
        }

        //Show Add Chatbox Text Command
        private static void ProcessCommand(
            AddChatboxTextCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            ProcessChatboxCommand(command.Text, player, instance, command.Channel, command.Color, command.SendToast);
        }

        //Set Variable Commands
        private static void ProcessCommand(
            SetVariableCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            ProcessVariableModification(command, (dynamic) command.Modification, player, instance);
        }

        //Set Self Switch Command
        private static void ProcessCommand(
            SetSelfSwitchCommand command,
            Player player,
            Event eventInstance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            if (eventInstance.Global)
            {
                if (MapController.TryGetInstanceFromMap(eventInstance.MapId, player.MapInstanceId, out var instance))
                {
                    var evts = instance.GlobalEventInstances.Values.ToList();
                    for (var i = 0; i < evts.Count; i++)
                    {
                        if (evts[i] != null && evts[i].BaseEvent == eventInstance.BaseEvent)
                        {
                            evts[i].SelfSwitch[command.SwitchId] = command.Value;
                        }
                    }
                }
            }
            else
            {
                eventInstance.SelfSwitch[command.SwitchId] = command.Value;
            }
        }

        //Conditional Branch Command
        private static void ProcessCommand(
            ConditionalBranchCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            var success = Conditions.MeetsCondition(command.Condition, player, instance, null);

            List<EventCommand> newCommandList = null;
            if (success && stackInfo.Page.CommandLists.ContainsKey(command.BranchIds[0]))
            {
                newCommandList = stackInfo.Page.CommandLists[command.BranchIds[0]];
            }

            if (!success && command.Condition.ElseEnabled && stackInfo.Page.CommandLists.ContainsKey(command.BranchIds[1]))
            {
                newCommandList = stackInfo.Page.CommandLists[command.BranchIds[1]];
            }

            if (newCommandList != null)
            {
                var tmpStack = new CommandInstance(stackInfo.Page) {
                    CommandList = newCommandList,
                    CommandIndex = 0,
                };

                callStack.Push(tmpStack);
            }
        }

        //Exit Event Process Command
        private static void ProcessCommand(
            ExitEventProcessingCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            callStack.Clear();
        }

        //Label Command
        private static void ProcessCommand(
            LabelCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            return; //Do Nothing.. just a label
        }

        //Go To Label Command
        private static void ProcessCommand(
            GoToLabelCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            //Recursively search through commands for the label, and create a brand new call stack based on where that label is located.
            var newCallStack = LoadLabelCallstack(command.Label, stackInfo.Page);
            if (newCallStack != null)
            {
                newCallStack.Reverse();
                while (callStack.Peek().CommandListId != Guid.Empty)
                {
                    callStack.Pop();
                }

                //also pop the current event
                callStack.Pop();
                foreach (var itm in newCallStack)
                {
                    callStack.Push(itm);
                }
            }
        }

        //Start Common Event Command
        private static void ProcessCommand(
            StartCommmonEventCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            var commonEvent = EventBase.Get(command.EventId);
            if (commonEvent != null)
            {
                for (var i = 0; i < commonEvent.Pages.Count; i++)
                {
                    if (Conditions.CanSpawnPage(commonEvent.Pages[i], player, instance))
                    {
                        var commonEventStack = new CommandInstance(commonEvent.Pages[i]);
                        callStack.Push(commonEventStack);
                    }
                }
            }

            if (command.AllInInstance && InstanceProcessor.TryGetInstanceController(player.MapInstanceId, out var instanceController)) 
            {
                foreach (var instanceMember in instanceController.Players.Where(pl => pl.Id != player.Id)) 
                {
                    instanceMember.EnqueueStartCommonEvent(commonEvent);
                }
            }
        }

        //Restore Hp Command
        private static void ProcessCommand(
            RestoreHpCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            if (command.Amount > 0)
            {
                player.AddVital(Vitals.Health, command.Amount);
            }
            else if (command.Amount < 0)
            {
                player.SubVital(Vitals.Health, -command.Amount);
                player.CombatTimer = Timing.Global.Milliseconds + Options.CombatTime;
                if (player.GetVital(Vitals.Health) <= 0)
                {
                    lock (player.EntityLock)
                    {
                        player.Die();
                    }
                }
            }
            else
            {
                player.RestoreVital(Vitals.Health);
            }
        }

        //Restore Mp Command
        private static void ProcessCommand(
            RestoreMpCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            if (command.Amount > 0)
            {
                player.AddVital(Vitals.Mana, command.Amount);
            }
            else if (command.Amount < 0)
            {
                player.SubVital(Vitals.Mana, -command.Amount);
                player.CombatTimer = Timing.Global.Milliseconds + Options.CombatTime;
            }
            else
            {
                player.RestoreVital(Vitals.Mana);
            }
        }

        //Level Up Command
        private static void ProcessCommand(
            LevelUpCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            player.LevelUp();
        }

        //Give Experience Command
        private static void ProcessCommand(
            GiveExperienceCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            var quantity = command.Exp;
            if (command.UseVariable)
            {
                switch (command.VariableType)
                {
                    case VariableTypes.PlayerVariable:
                        quantity = (int)player.GetVariableValue(command.VariableId).Integer;

                        break;
                    case VariableTypes.ServerVariable:
                        quantity = (int)ServerVariableBase.Get(command.VariableId)?.Value.Integer;
                        break;
                    case VariableTypes.InstanceVariable:
                        if (MapController.TryGetInstanceFromMap(player.MapId, player.MapInstanceId, out var mapInstance))
                        {
                            quantity = mapInstance.GetInstanceVariable(command.VariableId).Integer;
                        }
                        break;
                }
            }

            instance.LastExp = quantity;
            player.GiveExperience(quantity);
        }

        //Change Level Command
        private static void ProcessCommand(
            ChangeLevelCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            player.SetLevel(command.Level, true);
        }

        //Change Spells Command
        private static void ProcessCommand(
            ChangeSpellsCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            if (player == null)
            {
                return;
            }

            //0 is add, 1 is remove
            var success = false;
            if (command.Add) //Try to add a spell
            {
                success = player.TryAddSkillToBook(command.SpellId);
                if (success)
                {
                    player.SendSkillAddedMessage(command.SpellId);
                }
            }
            else
            {
                success = player.TryRemoveSkillFromSkillbook(command.SpellId);
                if (success)
                {
                    player.SendSkillLostMessage(command.SpellId);
                }
            }

            List<EventCommand> newCommandList = null;
            if (success && stackInfo.Page.CommandLists.ContainsKey(command.BranchIds[0]))
            {
                newCommandList = stackInfo.Page.CommandLists[command.BranchIds[0]];
            }

            if (!success && stackInfo.Page.CommandLists.ContainsKey(command.BranchIds[1]))
            {
                newCommandList = stackInfo.Page.CommandLists[command.BranchIds[1]];
            }

            var tmpStack = new CommandInstance(stackInfo.Page)
            {
                CommandList = newCommandList,
                CommandIndex = 0,
            };

            callStack.Push(tmpStack);
        }

        //Change Items Command
        private static void ProcessCommand(
            ChangeItemsCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            var success = false;
            var skip = false;

            // Use the command quantity, unless we're using a variable for input!
            var quantity = command.Quantity;
            if (command.UseVariable)
            {
                switch (command.VariableType)
                {
                    case VariableTypes.PlayerVariable:
                        quantity = (int)player.GetVariableValue(command.VariableId).Integer;

                        break;
                    case VariableTypes.ServerVariable:
                        quantity = (int)ServerVariableBase.Get(command.VariableId)?.Value.Integer;
                        break;
                    case VariableTypes.InstanceVariable:
                        if (MapController.TryGetInstanceFromMap(player.MapId, player.MapInstanceId, out var mapInstance))
                        {
                            quantity = (int)mapInstance.GetInstanceVariable(command.VariableId).Integer;
                        }
                        break;
                }

                // The code further ahead converts 0 to quantity 1, due to some legacy junk where some editors would (maybe still do?) set quantity to 0 for non-stackable items.
                // but if we want to give a player no items through an event we should listen to that.
                if (quantity <= 0)
                {
                    skip = true;
                }
            }

            if (command.Stacks)
            {
                var item = ItemBase.Get(command.ItemId);
                quantity *= item.MaxInventoryStack;
            }

            if (!skip)
            {
                if (command.Add)
                {
                    success = player.TryGiveItem(command.ItemId, quantity, command.ItemHandling);
                }
                else
                {
                    success = player.TryTakeItem(command.ItemId, quantity, command.ItemHandling);
                }
            }
            else
            {
                // If we're skipping, this always succeeds.
                success = true;
            }

            List<EventCommand> newCommandList = null;
            if (success && stackInfo.Page.CommandLists.ContainsKey(command.BranchIds[0]))
            {
                newCommandList = stackInfo.Page.CommandLists[command.BranchIds[0]];
            }

            if (!success && stackInfo.Page.CommandLists.ContainsKey(command.BranchIds[1]))
            {
                newCommandList = stackInfo.Page.CommandLists[command.BranchIds[1]];
            }

            var tmpStack = new CommandInstance(stackInfo.Page)
            {
                CommandList = newCommandList,
                CommandIndex = 0,
            };

            instance.LastItemId = command.ItemId;
            instance.LastItemQuantity = quantity;
            callStack.Push(tmpStack);
        }

        //Equip Items Command
        private static void ProcessCommand(
            EquipItemCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            var itm = ItemBase.Get(command.ItemId);

            if (itm == null)
            {
                return;
            }

            if (command.Unequip)
            {
                player.UnequipItem(command.ItemId);
            }
            else
            {
                player.EquipItem(ItemBase.Get(command.ItemId));
            }
        }

        //Change Sprite Command
        private static void ProcessCommand(
            ChangeSpriteCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            player.Sprite = command.Sprite;
            PacketSender.SendEntityDataToProximity(player);
        }

        //Change Face Command
        private static void ProcessCommand(
            ChangeFaceCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            player.Face = command.Face;
            PacketSender.SendEntityDataToProximity(player);
        }

        //Change Gender Command
        private static void ProcessCommand(
            ChangeGenderCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            player.Gender = command.Gender;
            PacketSender.SendEntityDataToProximity(player);
        }

        //Change Name Color Command
        private static void ProcessCommand(
            ChangeNameColorCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            if (command.Remove)
            {
                player.NameColor = null;
                PacketSender.SendEntityDataToProximity(player);

                return;
            }

            //Don't set the name color if it doesn't override admin name colors.
            if (player.Power != UserRights.None && !command.Override)
            {
                return;
            }

            player.NameColor = command.Color;
            PacketSender.SendEntityDataToProximity(player);
        }

        //Change Player Label Command
        private static void ProcessCommand(
            ChangePlayerLabelCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            var label = ParseEventText(command.Value, player, instance);

            var color = command.Color;
            if (command.MatchNameColor)
            {
                color = Color.Transparent;
            }

            if (command.Position == 0) // Header
            {
                player.HeaderLabel = new Label(label, color);
            }
            else if (command.Position == 1) // Footer
            {
                player.FooterLabel = new Label(label, color);
            }

            PacketSender.SendEntityDataToProximity(player);
        }

        //Set Access Command (wtf why would we even allow this? lol)
        private static void ProcessCommand(
            SetAccessCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            if (player.Client == null)
            {
                return;
            }

            switch (command.Access)
            {
                case Access.Moderator:
                    player.Client.Power = UserRights.Moderation;

                    break;
                case Access.Admin:
                    player.Client.Power = UserRights.Admin;

                    break;
                default:
                    player.Client.Power = UserRights.None;

                    break;
            }

            PacketSender.SendEntityDataToProximity(player);
            PacketSender.SendChatMsg(player, Strings.Player.powerchanged, ChatMessageType.Notice, CustomColors.Alerts.Declined);
        }

        //Warp Player Command
        private static void ProcessCommand(
            WarpCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            if (player == null)
            {
                return;
            }

            player.NextInstanceLives = command.SharedLives - 1;
            if (command.ChangeInstance)
            {
                player.Warp(
                    command.MapId, command.X, command.Y,
                    command.Direction == WarpDirection.Retain ? (byte)player.Dir : (byte)(command.Direction - 1),
                    false, 0, false, command.FadeOnWarp, command.InstanceType, dungeonId: command.DungeonId
                );
            } else
            {
                player.Warp(
                    command.MapId, command.X, command.Y,
                    command.Direction == WarpDirection.Retain ? (byte)player.Dir : (byte)(command.Direction - 1),
                    false, 0, false, command.FadeOnWarp, dungeonId: command.DungeonId
                );
            }

            if (!string.IsNullOrWhiteSpace(command.Sound))
            {
                PacketSender.SendPlaySound(player, command.Sound);
            }
        }

        //Set Move Route Command
        private static void ProcessCommand(
            SetMoveRouteCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            if (command.Route.Target == Guid.Empty)
            {
                player.MoveRoute = new EventMoveRoute();
                player.MoveRouteSetter = instance.PageInstance;
                player.MoveRoute.CopyFrom(command.Route);
                PacketSender.SendMoveRouteToggle(player, true);
            }
            else
            {
                foreach (var evt in player.EventLookup)
                {
                    if (evt.Value.BaseEvent.Id == command.Route.Target)
                    {
                        if (evt.Value.PageInstance != null)
                        {
                            evt.Value.PageInstance.MoveRoute.CopyFrom(command.Route);
                            evt.Value.PageInstance.MovementType = EventMovementType.MoveRoute;
                            if (evt.Value.PageInstance.GlobalClone != null)
                            {
                                evt.Value.PageInstance.GlobalClone.MovementType = EventMovementType.MoveRoute;
                            }
                        }
                    }
                }
            }
        }

        //Wait for Route Completion Command
        private static void ProcessCommand(
            WaitForRouteCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            if (command.TargetId == Guid.Empty)
            {
                stackInfo.WaitingForRoute = player.Id;
                stackInfo.WaitingForRouteMap = player.MapId;
            }
            else
            {
                foreach (var evt in player.EventLookup)
                {
                    if (evt.Value.BaseEvent.Id == command.TargetId)
                    {
                        stackInfo.WaitingForRoute = evt.Value.BaseEvent.Id;
                        stackInfo.WaitingForRouteMap = evt.Value.MapId;

                        break;
                    }
                }
            }
        }

        //Spawn Npc Command
        private static void ProcessCommand(
            SpawnNpcCommand command,
            Player player,
            Event eventInstance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            var npcId = command.NpcId;
            var mapId = command.MapId;
            var tileX = 0;
            var tileY = 0;
            var direction = (byte) Directions.Up;
            var targetEntity = (Entity) player;
            if (mapId != Guid.Empty)
            {
                tileX = command.X;
                tileY = command.Y;
                direction = command.Dir;
            }
            else
            {
                if (command.EntityId != Guid.Empty)
                {
                    foreach (var evt in player.EventLookup)
                    {
                        if (evt.Value.MapId != eventInstance.MapId)
                        {
                            continue;
                        }

                        if (evt.Value.BaseEvent.Id == command.EntityId)
                        {
                            targetEntity = evt.Value.PageInstance;

                            break;
                        }
                    }
                }

                if (targetEntity != null)
                {
                    int xDiff = command.X;
                    int yDiff = command.Y;
                    if (command.Dir == 1)
                    {
                        var tmp = 0;
                        switch (targetEntity.Dir)
                        {
                            case (int) Directions.Down:
                                yDiff *= -1;
                                xDiff *= -1;

                                break;
                            case (int) Directions.Left:
                                tmp = yDiff;
                                yDiff = xDiff;
                                xDiff = tmp;

                                break;
                            case (int) Directions.Right:
                                tmp = yDiff;
                                yDiff = xDiff;
                                xDiff = -tmp;

                                break;
                        }

                        direction = (byte) targetEntity.Dir;
                    }

                    mapId = targetEntity.MapId;
                    tileX = (byte) (targetEntity.X + xDiff);
                    tileY = (byte) (targetEntity.Y + yDiff);
                }
                else
                {
                    return;
                }
            }

            var tile = new TileHelper(mapId, tileX, tileY);
            if (tile.TryFix() && MapController.TryGetInstanceFromMap(mapId, player.MapInstanceId, out var instance)
                && instance.TrySpawnNpc((byte)tileX, (byte)tileY, direction, npcId, out var npc, true))
            {
                player.SpawnedNpcs.Add(npc);
            }
        }

        //Despawn Npcs Command
        private static void ProcessCommand(
            DespawnNpcCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            var entities = player.SpawnedNpcs.ToArray();
            for (var i = 0; i < entities.Length; i++)
            {
                if (entities[i] != null && entities[i].GetType() == typeof(Npc))
                {
                    if (((Npc) entities[i]).Despawnable == true)
                    {
                        lock (player.EntityLock)
                        {
                            ((Npc)entities[i]).Die();
                        }
                    }
                }
            }

            player.SpawnedNpcs.Clear();
        }

        //Play Animation Command
        private static void ProcessCommand(
            PlayAnimationCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            //Playing an animations requires a target type/target or just a tile.
            //We need an animation number and whether or not it should rotate (and the direction I guess)
            var animId = command.AnimationId;
            var mapId = command.MapId;
            var tileX = 0;
            var tileY = 0;
            var direction = (byte) Directions.Up;
            var targetEntity = (Entity) player;
            if (mapId != Guid.Empty)
            {
                tileX = command.X;
                tileY = command.Y;
                direction = command.Dir;
            }
            else
            {
                if (command.EntityId != Guid.Empty)
                {
                    foreach (var evt in player.EventLookup)
                    {
                        if (evt.Value.MapId != instance.MapId)
                        {
                            continue;
                        }

                        if (evt.Value.BaseEvent.Id == command.EntityId)
                        {
                            targetEntity = evt.Value.PageInstance;

                            break;
                        }
                    }
                }

                if (targetEntity != null)
                {
                    if (command.X == 0 && command.Y == 0 && command.Dir == 0)
                    {
                        //Attach to entity instead of playing on tile
                        if (command.Instanced)
                        {
                            PacketSender.SendAnimationTo(
                                animId, targetEntity.GetEntityType() == EntityTypes.Event ? 2 : 1, targetEntity.Id,
                                targetEntity.MapId, 0, 0, 0, targetEntity.MapInstanceId, player
                            );
                        }
                        else
                        {
                            PacketSender.SendAnimationToProximity(
                                animId, targetEntity.GetEntityType() == EntityTypes.Event ? 2 : 1, targetEntity.Id,
                                targetEntity.MapId, 0, 0, 0, targetEntity.MapInstanceId
                            );
                        }

                        return;
                    }

                    int xDiff = command.X;
                    int yDiff = command.Y;
                    if (command.Dir == 1)
                    {
                        var tmp = 0;
                        switch (targetEntity.Dir)
                        {
                            case (int) Directions.Down:
                                yDiff *= -1;
                                xDiff *= -1;

                                break;
                            case (int) Directions.Left:
                                tmp = yDiff;
                                yDiff = xDiff;
                                xDiff = tmp;

                                break;
                            case (int) Directions.Right:
                                tmp = yDiff;
                                yDiff = xDiff;
                                xDiff = -tmp;

                                break;
                        }

                        direction = (byte) targetEntity.Dir;
                    }

                    mapId = targetEntity.MapId;
                    tileX = targetEntity.X + xDiff;
                    tileY = targetEntity.Y + yDiff;
                }
                else
                {
                    return;
                }
            }

            var tile = new TileHelper(mapId, tileX, tileY);
            if (tile.TryFix())
            {
                if (command.Instanced)
                {
                    PacketSender.SendAnimationTo(
                        animId, -1, Guid.Empty, tile.GetMapId(), tile.GetX(), tile.GetY(), (sbyte)direction, player.MapInstanceId,  player
                    );
                }
                else
                {
                    PacketSender.SendAnimationToProximity(
                        animId, -1, Guid.Empty, tile.GetMapId(), tile.GetX(), tile.GetY(), (sbyte)direction, player.MapInstanceId
                    );
                }
            }
        }

        //Hold Player Command
        private static void ProcessCommand(
            HoldPlayerCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            instance.HoldingPlayer = true;
            PacketSender.SendHoldPlayer(player, instance.BaseEvent.Id, instance.BaseEvent.MapId);
        }

        //Release Player Command
        private static void ProcessCommand(
            ReleasePlayerCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            instance.HoldingPlayer = false;
            PacketSender.SendReleasePlayer(player, instance.BaseEvent.Id);
        }

        //Hide Player Command
        private static void ProcessCommand(
            HidePlayerCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            player.HideEntity = true;
            player.HideName = true;
            PacketSender.SendEntityDataToProximity(player);
        }

        //Show Player Command
        private static void ProcessCommand(
            ShowPlayerCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            player.HideEntity = false;
            player.HideName = false;
            PacketSender.SendEntityDataToProximity(player);
        }

        //Play Bgm Command
        private static void ProcessCommand(
            PlayBgmCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            PacketSender.SendPlayMusic(player, command.File);
        }

        //Fadeout Bgm Command
        private static void ProcessCommand(
            FadeoutBgmCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            PacketSender.SendFadeMusic(player);
        }

        //Play Sound Command
        private static void ProcessCommand(
            PlaySoundCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            PacketSender.SendPlaySound(player, command.File);
        }

        //Stop Sounds Command
        private static void ProcessCommand(
            StopSoundsCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            PacketSender.SendStopSounds(player);
        }

        //Show Picture Command
        private static void ProcessCommand(
            ShowPictureCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            var id = Guid.Empty;
            var shouldWait = command.WaitUntilClosed && (command.Clickable || command.HideTime > 0);
            if (shouldWait)
            {
                id = instance.PageInstance.Id;
                stackInfo.WaitingForResponse = CommandInstance.EventResponse.Picture;
            }
            
            PacketSender.SendShowPicture(player, command.File, command.Size, command.Clickable, command.HideTime, id);
        }

        //Hide Picture Command
        private static void ProcessCommand(
            HidePictureCommmand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            PacketSender.SendHidePicture(player);
        }

        //Flash Screen Command
        private static void ProcessCommand(
            FlashScreenCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            PacketSender.SendFlashScreenPacket(player.Client, command.Duration, command.FlashColor, command.Intensity);
        }

        //Wait Command
        private static void ProcessCommand(
            WaitCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            instance.WaitTimer = Timing.Global.Milliseconds + command.Time;
            callStack.Peek().WaitingForResponse = CommandInstance.EventResponse.Timer;
        }

        //Open Bank Command
        private static void ProcessCommand(
            OpenBankCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            player.OpenBank();
            callStack.Peek().WaitingForResponse = CommandInstance.EventResponse.Bank;
        }

        //Open Shop Command
        private static void ProcessCommand(
            OpenShopCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            player.OpenShop(ShopBase.Get(command.ShopId));
            callStack.Peek().WaitingForResponse = CommandInstance.EventResponse.Shop;
        }

        //Open Crafting Table Command
        private static void ProcessCommand(
            OpenCraftingTableCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            player.OpenCraftingTable(command.CraftingTableId);
            callStack.Peek().WaitingForResponse = CommandInstance.EventResponse.Crafting;
        }

        //Set Class Command
        private static void ProcessCommand(
            SetClassCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            if (ClassBase.Get(command.ClassId) != null)
            {
                player.ClassId = command.ClassId;
                player.RecalculateStatsAndPoints();
                player.UnequipInvalidItems();
            }

            PacketSender.SendEntityDataToProximity(player);
        }

        //Start Quest Command
        private static void ProcessCommand(
            StartQuestCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            var success = false;
            var quest = QuestBase.Get(command.QuestId);
            if (quest != null)
            {
                if (player.CanStartQuest(quest))
                {
                    if (command.Offer)
                    {
                        player.OfferQuest(quest);
                        stackInfo.WaitingForResponse = CommandInstance.EventResponse.Quest;
                        stackInfo.BranchIds = command.BranchIds;
                        stackInfo.WaitingOnCommand = command;

                        return;
                    }
                    else
                    {
                        player.StartQuest(quest);
                        success = true;
                    }
                }
            }

            List<EventCommand> newCommandList = null;
            if (success && stackInfo.Page.CommandLists.ContainsKey(command.BranchIds[0]))
            {
                newCommandList = stackInfo.Page.CommandLists[command.BranchIds[0]];
            }

            if (!success && stackInfo.Page.CommandLists.ContainsKey(command.BranchIds[1]))
            {
                newCommandList = stackInfo.Page.CommandLists[command.BranchIds[1]];
            }

            var tmpStack = new CommandInstance(stackInfo.Page)
            {
                CommandList = newCommandList,
                CommandIndex = 0,
            };

            callStack.Push(tmpStack);
        }

        //Complete Quest Task Command
        private static void ProcessCommand(
            CompleteQuestTaskCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            player.CompleteQuestTask(command.QuestId, command.TaskId);
        }

        //End Quest Command
        private static void ProcessCommand(
            EndQuestCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            if (command.ResetQuest)
            {
                player.ResetQuest(command.QuestId, command.AgnosticStop);
            }
            else
            {
                player.CompleteQuest(command.QuestId, command.SkipCompletionEvent);
            }
        }

        // Change Player Color Command
        private static void ProcessCommand(
            ChangePlayerColorCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            player.Color = command.Color;
            PacketSender.SendEntityDataToProximity(player);
        }

        private static void ProcessCommand(
            ChangeNameCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            var success = false;

            var variable = PlayerVariableBase.Get(command.VariableId);
            if (variable != null)
            {
                if (variable.Type == VariableDataTypes.String)
                {
                    var data = player.GetVariable(variable.Id)?.Value;
                    if (data != null)
                    {
                        success = player.TryChangeName(data.String);
                    }
                }
            }

            List<EventCommand> newCommandList = null;
            if (success && stackInfo.Page.CommandLists.ContainsKey(command.BranchIds[0]))
            {
                newCommandList = stackInfo.Page.CommandLists[command.BranchIds[0]];
            }

            if (!success && stackInfo.Page.CommandLists.ContainsKey(command.BranchIds[1]))
            {
                newCommandList = stackInfo.Page.CommandLists[command.BranchIds[1]];
            }

            var tmpStack = new CommandInstance(stackInfo.Page)
            {
                CommandList = newCommandList,
                CommandIndex = 0,
            };

            callStack.Push(tmpStack);
        }

        //Create Guild Command
        private static void ProcessCommand(
            CreateGuildCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            var success = false;
            var playerVariable = PlayerVariableBase.Get(command.VariableId);

            // We only accept Strings as our Guild Names!
            if (playerVariable.Type == VariableDataTypes.String)
            {
                // Get our intended guild name
                var gname = player.GetVariable(playerVariable.Id)?.Value.String?.Trim();

                // Can we use this name according to our configuration?
                if (gname != null && FieldChecking.IsValidGuildName(gname, Strings.Regex.guildname))
                {
                    // Is the name already in use?
                    if (Guild.GetGuild(gname) == null)
                    {
                        // Is the player already in a guild?
                        if (player.Guild == null)
                        {
                            // Finally, we can actually MAKE this guild happen!
                            var guild = Guild.CreateGuild(player, gname);
                            if (guild != null)
                            {
                                // Send them a welcome message!
                                PacketSender.SendChatMsg(player, Strings.Guilds.Welcome.ToString(gname), ChatMessageType.Guild, CustomColors.Alerts.Success);

                                // Denote that we were successful.
                                success = true;
                            }
                        }
                        else
                        {
                            // This cheeky bugger is already in a guild, tell him so!
                            PacketSender.SendChatMsg(player, Strings.Guilds.AlreadyInGuild, ChatMessageType.Guild, CustomColors.Alerts.Error);
                        }
                    }
                    else
                    {
                        // This name already exists, oh dear!
                        PacketSender.SendChatMsg(player, Strings.Guilds.GuildNameInUse, ChatMessageType.Guild, CustomColors.Alerts.Error);
                    }
                }
                else
                {
                    // Let our player know they need to adjust their name.
                    PacketSender.SendChatMsg(player, Strings.Guilds.VariableInvalid, ChatMessageType.Guild, CustomColors.Alerts.Error);
                }
            }
            else
            {
                // Notify the user that something went wrong, the user really shouldn't see this.. Assuming the creator set up his events properly.
                PacketSender.SendChatMsg(player, Strings.Guilds.VariableNotString, ChatMessageType.Guild, CustomColors.Alerts.Error);
            }

            List<EventCommand> newCommandList = null;
            if (success && stackInfo.Page.CommandLists.ContainsKey(command.BranchIds[0]))
            {
                newCommandList = stackInfo.Page.CommandLists[command.BranchIds[0]];
            }

            if (!success && stackInfo.Page.CommandLists.ContainsKey(command.BranchIds[1]))
            {
                newCommandList = stackInfo.Page.CommandLists[command.BranchIds[1]];
            }

            var tmpStack = new CommandInstance(stackInfo.Page)
            {
                CommandList = newCommandList,
                CommandIndex = 0,
            };

            callStack.Push(tmpStack);
        }

        private static void ProcessCommand(
            DisbandGuildCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            var success = false;

            // Is this player in a guild?
            if (player.Guild != null)
            {
                // Send the members a notification, then start wiping the guild from existence through sheer willpower!
                PacketSender.SendGuildMsg(player, Strings.Guilds.DisbandGuild.ToString(player.Guild.Name), CustomColors.Alerts.Info);
                Guild.DeleteGuild(player.Guild, player);

                // :(
                success = true;
            }
            else
            {
                // They're not in a guild.. tell them?
                PacketSender.SendChatMsg(player, Strings.Guilds.NotInGuild, ChatMessageType.Guild, CustomColors.Alerts.Error);
            }

            List<EventCommand> newCommandList = null;
            if (success && stackInfo.Page.CommandLists.ContainsKey(command.BranchIds[0]))
            {
                newCommandList = stackInfo.Page.CommandLists[command.BranchIds[0]];
            }

            if (!success && stackInfo.Page.CommandLists.ContainsKey(command.BranchIds[1]))
            {
                newCommandList = stackInfo.Page.CommandLists[command.BranchIds[1]];
            }

            var tmpStack = new CommandInstance(stackInfo.Page)
            {
                CommandList = newCommandList,
                CommandIndex = 0,
            };

            callStack.Push(tmpStack);
        }

        //Open Guild Bank Command
        private static void ProcessCommand(
            OpenGuildBankCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            player.OpenBank(true);
            callStack.Peek().WaitingForResponse = CommandInstance.EventResponse.Bank;
        }


        //Open Guild Bank Slots Count Command
        private static void ProcessCommand(
            SetGuildBankSlotsCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            var quantity = 0;
            switch (command.VariableType)
            {
                case VariableTypes.PlayerVariable:
                    quantity = (int)player.GetVariableValue(command.VariableId).Integer;

                    break;
                case VariableTypes.ServerVariable:
                    quantity = (int)ServerVariableBase.Get(command.VariableId)?.Value.Integer;
                    break;
                case VariableTypes.InstanceVariable:
                    if (MapController.TryGetInstanceFromMap(player.MapId, player.MapInstanceId, out var mapInstance))
                    {
                        quantity = (int)mapInstance.GetInstanceVariable(command.VariableId).Integer;
                    }
                    break;
            }
            var guild = player.Guild;
            if (quantity > 0 && guild != null && guild.BankSlotsCount != quantity)
            {
                guild.ExpandBankSlots(quantity);
            }
        }

        //Reset Stat Point Allocations Command
        private static void ProcessCommand(
            ResetStatPointAllocationsCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            for (var i = 0; i < (int)Stats.StatCount; i++)
            {
                player.StatPointAllocations[i] = 0;
            }
            for (var i = 0; i < (int)Vitals.VitalCount; i++)
            {
                player.VitalPointAllocations[i] = 0;
            }
            player.RecalculateStatsAndPoints();
            player.UnequipInvalidItems();
            PacketSender.SendEntityDataToProximity(player);
        }

        private static Stack<CommandInstance> LoadLabelCallstack(string label, EventPage currentPage)
        {
            var newStack = new Stack<CommandInstance>();
            newStack.Push(new CommandInstance(currentPage)); //Start from the top
            if (FindLabelResursive(newStack, currentPage, newStack.Peek().CommandList, label))
            {
                return newStack;
            }

            return null;
        }

        private static bool FindLabelResursive(
            Stack<CommandInstance> stack,
            EventPage page,
            List<EventCommand> commandList,
            string label
        )
        {
            if (page.CommandLists.ContainsValue(commandList))
            {
                while (stack.Peek().CommandIndex < commandList.Count)
                {
                    var command = commandList[stack.Peek().CommandIndex];
                    var branchIds = new List<Guid>();
                    switch (command.Type)
                    {
                        case EventCommandType.ShowOptions:
                            branchIds.AddRange(((ShowOptionsCommand) command).BranchIds);

                            break;
                        case EventCommandType.InputVariable:
                            branchIds.AddRange(((InputVariableCommand) command).BranchIds);

                            break;
                        case EventCommandType.ConditionalBranch:
                            branchIds.AddRange(((ConditionalBranchCommand) command).BranchIds);

                            break;
                        case EventCommandType.ChangeSpells:
                            branchIds.AddRange(((ChangeSpellsCommand) command).BranchIds);

                            break;
                        case EventCommandType.ChangeItems:
                            branchIds.AddRange(((ChangeItemsCommand) command).BranchIds);

                            break;
                        case EventCommandType.StartQuest:
                            branchIds.AddRange(((StartQuestCommand) command).BranchIds);

                            break;
                        case EventCommandType.Label:
                            //See if we found the label!
                            if (((LabelCommand) command).Label == label)
                            {
                                return true;
                            }

                            break;
                    }

                    //Test each branch
                    foreach (var branch in branchIds)
                    {
                        if (page.CommandLists.ContainsKey(branch))
                        {
                            var tmpStack = new CommandInstance(page)
                            {
                                CommandList = page.CommandLists[branch],
                                CommandIndex = 0,
                            };

                            stack.Peek().CommandIndex++;
                            stack.Push(tmpStack);
                            if (FindLabelResursive(stack, page, tmpStack.CommandList, label))
                            {
                                return true;
                            }

                            stack.Peek().CommandIndex--;
                        }
                    }

                    stack.Peek().CommandIndex++;
                }

                stack.Pop(); //We made it through a list
            }

            return false;
        }

        public static string ParseEventText(string input, Player player, Event instance)
        {
            if (input == null)
            {
                input = "";
            }

            if (player != null && input.Contains("\\") && InstanceProcessor.TryGetInstanceController(player.MapInstanceId, out var instanceController))
            {
                var sb = new StringBuilder(input);
                var time = Time.GetTime();
                var replacements = new Dictionary<string, string>()
                {
                    { Strings.Events.playernamecommand, player.Name },
                    { Strings.Events.playerguildcommand, player.Guild?.Name ?? "" },
                    { Strings.Events.timehour, Time.Hour },
                    { Strings.Events.militaryhour, Time.MilitaryHour },
                    { Strings.Events.timeminute, Time.Minute },
                    { Strings.Events.timesecond, Time.Second },
                    { Strings.Events.timeperiod, time.Hour >= 12 ? Strings.Events.periodevening : Strings.Events.periodmorning },
                    { Strings.Events.onlinecountcommand, Player.OnlineCount.ToString() },
                    { Strings.Events.onlinelistcommand, input.Contains(Strings.Events.onlinelistcommand) ? string.Join(", ", Player.OnlineList.Select(p => p.Name).ToList()) : "" },
                    { Strings.Events.eventnamecapscommand, instance?.PageInstance?.Name.ToUpper() ?? "" },
                    { Strings.Events.eventnamecommand, instance?.PageInstance?.Name ?? "" },
                    { Strings.Events.commandparameter, instance?.PageInstance?.Param ?? "" },
                    { Strings.Events.eventparams, (instance != null && input.Contains(Strings.Events.eventparams)) ? instance.FormatParameters(player) : "" },
                    { Strings.Events.MeleePool, instanceController.DuelPool.Count > 0 ? string.Join(", ", instanceController.DuelPool.Select(pl => pl?.Name ?? "").ToArray()) : "No players are currently in the open melee pool" },
                };
                
                if (player.Party?.Count > 1)
                {
                    if (player.Party.Count == 2)
                    {
                        replacements[Strings.Events.playerpartymemberscommand] = String.Join(" and ", player.Party.Select(member => member.Name).ToArray());
                    }
                    else
                    {
                        StringBuilder partyBuilder = new StringBuilder(player.Party[0].Name);
                        for (int i = 1; i < player.Party.Count; i++)
                        {
                            if (i < player.Party.Count - 1)
                            {
                                partyBuilder.Append(", " + player.Party[i].Name);
                            }
                            else
                            {
                                partyBuilder.Append(", and " + player.Party[i].Name);
                            }
                        }

                        replacements[Strings.Events.playerpartymemberscommand] = partyBuilder.ToString();
                    }
                }
                else
                {
                    replacements[Strings.Events.playerpartymemberscommand] = player.Name;
                }


                foreach (var val in replacements)
                {
                    if (input.Contains(val.Key))
                        sb.Replace(val.Key, val.Value);
                }

                foreach (var val in DbInterface.ServerVariableEventTextLookup)
                {
                    if (input.Contains(val.Key))
                        sb.Replace(val.Key, (val.Value).Value.ToString((val.Value).Type));
                }

                foreach (var val in DbInterface.PlayerVariableEventTextLookup)
                {
                    if (input.Contains(val.Key))
                        sb.Replace(val.Key, player.GetVariableValue(val.Value.Id).ToString((val.Value).Type));
                }

                foreach (var val in DbInterface.InstanceVariableEventTextLookup)
                {
                    if (MapController.TryGetInstanceFromMap(player.MapId, player.MapInstanceId, out var mapInstance))
                    {
                        var instanceVarVal = mapInstance.GetInstanceVariable(val.Value.Id);
                        if (instanceVarVal != null && input.Contains(val.Key))
                                sb.Replace(val.Key, instanceVarVal.ToString((val.Value).Type));
                    }
                }
                    
                if (instance != null)
                {
                    var parms = instance.GetParams(player);
                    foreach (var val in parms)
                    {
                        sb.Replace(Strings.Events.eventparam + "{" + val.Key + "}", val.Value);
                    }
                }

                // Time Lapses require variables to already be evaluated in the string, so we make a second string builder.
                var finalSb = new StringBuilder(sb.ToString());
                var elapsedRegexString = $@"\{Strings.Events.Elapsed}\{{\d+\}}";
                Regex elapsedRegex = new Regex(elapsedRegexString);

                foreach (var match in elapsedRegex.Matches(finalSb.ToString()))
                {
                    var digits = Regex.Match(match.ToString(), @"\d+").Value;

                    if (double.TryParse(digits.ToString(), out var ms))
                    {
                        string elapsedString = TextUtils.GetTimeElapsedString((long)ms, Strings.Events.ElapsedMinutes, Strings.Events.ElapsedHours, Strings.Events.ElapsedDays);

                        finalSb.Replace(match.ToString(), elapsedString);
                    }
                }

                return finalSb.ToString();
            }

            return input;
        }

        private static void ProcessVariableModification(
            SetVariableCommand command,
            VariableMod mod,
            Player player,
            Event instance
        )
        {
        }

        private static void ProcessVariableModification(
            SetVariableCommand command,
            BooleanVariableMod mod,
            Player player,
            Event instance
        )
        {
            VariableValue value = null;
            if (command.VariableType == VariableTypes.PlayerVariable)
            {
                value = player.GetVariableValue(command.VariableId);
            }
            else if (command.VariableType == VariableTypes.ServerVariable)
            {
                value = ServerVariableBase.Get(command.VariableId)?.Value;
            }
            else if (command.VariableType == VariableTypes.InstanceVariable && MapController.TryGetInstanceFromMap(player.MapId, player.MapInstanceId, out var mapInstance)) 
            {
                value = mapInstance.GetInstanceVariable(command.VariableId);
            }

            if (value == null)
            {
                value = new VariableValue();
            }

            var originalValue = value.Boolean;

            if (mod.DuplicateVariableId != Guid.Empty)
            {
                if (mod.DupVariableType == VariableTypes.PlayerVariable)
                {
                    value.Boolean = player.GetVariableValue(mod.DuplicateVariableId).Boolean;
                }
                else if (mod.DupVariableType == VariableTypes.ServerVariable)
                {
                    var variable = ServerVariableBase.Get(mod.DuplicateVariableId);
                    if (variable != null)
                    {
                        value.Boolean = ServerVariableBase.Get(mod.DuplicateVariableId).Value.Boolean;
                    }
                }
                else if (mod.DupVariableType == VariableTypes.InstanceVariable && MapController.TryGetInstanceFromMap(player.MapId, player.MapInstanceId, out var mapInstance))
                {
                    var variable = mapInstance.GetInstanceVariable(command.VariableId);
                    if (variable != null)
                    {
                        value.Boolean = variable.Boolean;
                    }
                }
            }
            else
            {
                value.Boolean = mod.Value;
            }

            var changed = value.Boolean != originalValue;

            if (command.VariableType == VariableTypes.PlayerVariable)
            {
                if (changed)
                {
                    player.StartCommonEventsWithTrigger(CommonEventTrigger.PlayerVariableChange, "", command.VariableId.ToString());
                }
                // Set the party member switches too if Sync Party enabled!
                if (command.SyncParty)
                {
                    foreach (var partyMember in player.Party.ToArray())
                    {
                        if (partyMember != player)
                        {
                            partyMember.SetSwitchValue(command.VariableId, mod.Value);
                            partyMember.StartCommonEventsWithTrigger(CommonEventTrigger.PlayerVariableChange, "", command.VariableId.ToString());
                        }
                    }
                }
            }
            else if (command.VariableType == VariableTypes.ServerVariable && changed)
            {
                Player.StartCommonEventsWithTriggerForAll(Enums.CommonEventTrigger.ServerVariableChange, "", command.VariableId.ToString());
                DbInterface.UpdatedServerVariables.AddOrUpdate(command.VariableId, ServerVariableBase.Get(command.VariableId), (key, oldValue) => ServerVariableBase.Get(command.VariableId));
            }
            else if (command.VariableType == VariableTypes.InstanceVariable && changed && MapController.TryGetInstanceFromMap(player.MapId, player.MapInstanceId, out var mapInstance))
            {
                MapInstance.SetInstanceVariable(command.VariableId, value, mapInstance.MapInstanceId);
                Player.StartCommonEventsWithTriggerForAllOnInstance(Enums.CommonEventTrigger.InstanceVariableChange, player.MapInstanceId, "", command.VariableId.ToString());
            }
        }

        private static void ProcessVariableModification(
            SetVariableCommand command,
            IntegerVariableMod mod,
            Player player,
            Event instance
        )
        {
            VariableValue value = null;
            // Get the players instance so we have it for reaching instance variables.
            if (!MapController.TryGetInstanceFromMap(player.MapId, player.MapInstanceId, out var playersInstance))
            {
                return; // A player not having an instance reaalllly is a problem, so just kick out of here if they don't.
            }

            if (command.VariableType == VariableTypes.PlayerVariable)
            {
                value = player.GetVariableValue(command.VariableId);
            }
            else if (command.VariableType == VariableTypes.ServerVariable)
            {
                value = ServerVariableBase.Get(command.VariableId)?.Value;
            }
            else if (command.VariableType == VariableTypes.InstanceVariable)
            {
                value = playersInstance.GetInstanceVariable(command.VariableId);
            }

            if (value == null)
            {
                value = new VariableValue();
            }

            var originalValue = value.Integer;

            switch (mod.ModType)
            {
                case Enums.VariableMods.Set:
                    value.Integer = mod.Value;

                    break;
                case Enums.VariableMods.Add:
                    value.Integer += mod.Value;

                    break;
                case Enums.VariableMods.Subtract:
                    value.Integer -= mod.Value;

                    break;
                case Enums.VariableMods.Multiply:
                    value.Integer *= mod.Value;

                    break;
                case Enums.VariableMods.Divide:
                    if (mod.Value != 0)  //Idiot proofing divide by 0 LOL
                    {
                        value.Integer /= mod.Value;
                    }

                    break;
                case Enums.VariableMods.LeftShift:
                    value.Integer = value.Integer << (int)mod.Value;

                    break;
                case Enums.VariableMods.RightShift:
                    value.Integer = value.Integer >> (int)mod.Value;

                    break;
                case Enums.VariableMods.Random:
                    //TODO: Fix - Random doesnt work with longs lolz
                    value.Integer = Randomization.Next((int) mod.Value, (int) mod.HighValue + 1);

                    break;
                case Enums.VariableMods.SystemTime:
                    var ms = (long) (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
                        .TotalMilliseconds;

                    value.Integer = ms;

                    break;
                case Enums.VariableMods.PlayerLevel:
                    value.Integer = player.Level;

                    break;
                case Enums.VariableMods.PlayerX:
                    value.Integer = player.X;

                    break;
                case Enums.VariableMods.PlayerY:
                    value.Integer = player.Y;

                    break;
                case Enums.VariableMods.EventX:
                case Enums.VariableMods.EventY:
                    if (instance.Global && MapController.TryGetInstanceFromMap(instance.MapId, instance.MapInstanceId, out var mapInstance))
                    {
                        var globalEvent = mapInstance.GetGlobalEventInstance(instance.BaseEvent);
                        if (globalEvent.GlobalPageInstance != null)
                        {
                            if (mod.ModType == Enums.VariableMods.EventX)
                            {
                                value.Integer = globalEvent.GlobalPageInstance[globalEvent.PageIndex].X;
                            }
                            else
                            {
                                value.Integer = globalEvent.GlobalPageInstance[globalEvent.PageIndex].Y;
                            }
                        }
                    }
                    else if (instance.PageInstance != null)
                    {
                        if (mod.ModType == Enums.VariableMods.EventX)
                        {
                            value.Integer = instance.PageInstance.X;
                        }
                        else
                        {
                            value.Integer = instance.PageInstance.Y;
                        }
                    }

                    break;
                case Enums.VariableMods.SpawnGroup:
                    if (MapController.TryGetInstanceFromMap(player.MapId, player.MapInstanceId, out var playerMap))
                    {
                        value.Integer = playerMap.NpcSpawnGroup;
                    }
                    
                    break;
                case Enums.VariableMods.OpenSlots:
                    value.Integer = player.FindOpenInventorySlots().Count;

                    break;
                case Enums.VariableMods.DupPlayerVar:
                    value.Integer = player.GetVariableValue(mod.DuplicateVariableId).Integer;

                    break;
                case Enums.VariableMods.DupGlobalVar:
                    var dupServerVariable = ServerVariableBase.Get(mod.DuplicateVariableId);
                    if (dupServerVariable != null)
                    {
                        value.Integer = dupServerVariable.Value.Integer;
                    }

                    break;
                case Enums.VariableMods.AddPlayerVar:
                    value.Integer += player.GetVariableValue(mod.DuplicateVariableId).Integer;

                    break;
                case Enums.VariableMods.AddGlobalVar:
                    var asv = ServerVariableBase.Get(mod.DuplicateVariableId);
                    if (asv != null)
                    {
                        value.Integer += asv.Value.Integer;
                    }

                    break;
                case Enums.VariableMods.SubtractPlayerVar:
                    value.Integer -= player.GetVariableValue(mod.DuplicateVariableId).Integer;

                    break;
                case Enums.VariableMods.SubtractGlobalVar:
                    var ssv = ServerVariableBase.Get(mod.DuplicateVariableId);
                    if (ssv != null)
                    {
                        value.Integer -= ssv.Value.Integer;
                    }

                    break;
                case Enums.VariableMods.MultiplyPlayerVar:
                    value.Integer *= player.GetVariableValue(mod.DuplicateVariableId).Integer;

                    break;
                case Enums.VariableMods.MultiplyGlobalVar:
                    var msv = ServerVariableBase.Get(mod.DuplicateVariableId);
                    if (msv != null)
                    {
                        value.Integer *= msv.Value.Integer;
                    }

                    break;
                case Enums.VariableMods.DividePlayerVar:
                    if (player.GetVariableValue(mod.DuplicateVariableId).Integer != 0) //Idiot proofing divide by 0 LOL
                    {
                        value.Integer /= player.GetVariableValue(mod.DuplicateVariableId).Integer;
                    }

                    break;
                case Enums.VariableMods.DivideGlobalVar:
                    var dsv = ServerVariableBase.Get(mod.DuplicateVariableId);
                    if (dsv != null)
                    {
                        if (dsv.Value != 0) //Idiot proofing divide by 0 LOL
                        {
                            value.Integer /= dsv.Value.Integer;
                        }
                    }

                    break;
                case Enums.VariableMods.LeftShiftPlayerVar:
                    value.Integer = value.Integer << (int)player.GetVariableValue(mod.DuplicateVariableId).Integer;

                    break;
                case Enums.VariableMods.LeftShiftGlobalVar:
                    var lhsv = ServerVariableBase.Get(mod.DuplicateVariableId);
                    if (lhsv != null)
                    {
                        value.Integer = value.Integer << (int)lhsv.Value.Integer;
                    }

                    break;
                case Enums.VariableMods.RightShiftPlayerVar:
                    value.Integer = value.Integer >> (int)player.GetVariableValue(mod.DuplicateVariableId).Integer;

                    break;
                case Enums.VariableMods.RightShiftGlobalVar:
                    var rhsv = ServerVariableBase.Get(mod.DuplicateVariableId);
                    if (rhsv != null)
                    {
                        value.Integer = value.Integer >> (int)rhsv.Value.Integer;
                    }

                    break;
                case VariableMods.DupInstanceVar:
                    if (playersInstance != null) 
                    {
                        value.Integer = playersInstance.GetInstanceVariable(mod.DuplicateVariableId).Integer;
                    }

                    break;
                case VariableMods.AddInstanceVar:
                    if (playersInstance != null)
                    {
                        value.Integer += playersInstance.GetInstanceVariable(mod.DuplicateVariableId).Integer;
                    }

                    break;
                case VariableMods.SubtractInstanceVar:
                    if (playersInstance != null)
                    {
                        value.Integer -= playersInstance.GetInstanceVariable(mod.DuplicateVariableId).Integer;
                    }

                    break;
                case VariableMods.MultiplyInstanceVar:
                    if (playersInstance != null)
                    {
                        value.Integer *= playersInstance.GetInstanceVariable(mod.DuplicateVariableId).Integer;
                    }

                    break;
                case VariableMods.DivideInstanceVar:
                    if (playersInstance != null)
                    {
                        var div = playersInstance.GetInstanceVariable(mod.DuplicateVariableId);
                        if (div != 0)
                        {
                            value.Integer /= playersInstance.GetInstanceVariable(mod.DuplicateVariableId).Integer;
                        }
                    }

                    break;
                case VariableMods.LeftShiftInstanceVar:
                    if (playersInstance != null)
                    {
                        var div = playersInstance.GetInstanceVariable(mod.DuplicateVariableId);
                        value.Integer = value.Integer << (int)playersInstance.GetInstanceVariable(mod.DuplicateVariableId).Integer;
                    }

                    break;
                case VariableMods.RightShiftInstanceVar:
                    if (playersInstance != null)
                    {
                        var div = playersInstance.GetInstanceVariable(mod.DuplicateVariableId);
                        value.Integer = value.Integer >> (int)playersInstance.GetInstanceVariable(mod.DuplicateVariableId).Integer;
                    }

                    break;
            }

            var changed = value.Integer != originalValue;

            if (command.VariableType == VariableTypes.PlayerVariable)
            {
                var newRecord = false;
                if (changed)
                {
                    var teammates = player.Party?.ToList() ?? default;
                    if (command.InstanceSync)
                    {
                        teammates = teammates.FindAll(tm => tm.MapInstanceId == player.MapInstanceId);
                    }
                    if (player.TrySetRecord(RecordType.PlayerVariable, command.VariableId, value.Integer, teammates))
                    {
                        newRecord = true;

                        PlayerRecord.SendNewVariableRecordMessage(command.VariableId, command.SyncParty && (player.Party?.Count > 0), player);
                    }
                    player.StartCommonEventsWithTrigger(CommonEventTrigger.PlayerVariableChange, "", command.VariableId.ToString());
                }

                // Set the party member switches too if Sync Party enabled!
                if (command.SyncParty)
                {
                    foreach (var partyMember in player.Party.ToArray())
                    {
                        if (command.InstanceSync && partyMember.MapInstanceId != player.MapInstanceId)
                        {
                            continue;
                        }
                        if (partyMember != player)
                        {
                            // We don't want to set a record for this party member - but we want to tell them if
                            // a new record has been set
                            partyMember.SetVariableValue(command.VariableId, value.Integer, false);
                            if (!newRecord)
                            {
                                continue;
                            }
                            PacketSender.SendChatMsg(partyMember, Strings.Records.NewRecordGenericParty, ChatMessageType.Experience, Color.FromName("Blue", Strings.Colors.presets));
                        }
                    }
                }
            }
            else if (command.VariableType == VariableTypes.ServerVariable && changed)
            {
                Player.StartCommonEventsWithTriggerForAll(Enums.CommonEventTrigger.ServerVariableChange, "", command.VariableId.ToString());
                DbInterface.UpdatedServerVariables.AddOrUpdate(command.VariableId, ServerVariableBase.Get(command.VariableId), (key, oldValue) => ServerVariableBase.Get(command.VariableId));
            }
            else if (command.VariableType == VariableTypes.InstanceVariable && changed && MapController.TryGetInstanceFromMap(player.MapId, player.MapInstanceId, out var mapInstance))
            {
                MapInstance.SetInstanceVariable(command.VariableId, value, mapInstance.MapInstanceId);
                Player.StartCommonEventsWithTriggerForAllOnInstance(Enums.CommonEventTrigger.InstanceVariableChange, player.MapInstanceId, "", command.VariableId.ToString());
            }
        }

        private static void ProcessVariableModification(
            SetVariableCommand command,
            StringVariableMod mod,
            Player player,
            Event instance
        )
        {
            VariableValue value = null;
            if (command.VariableType == VariableTypes.PlayerVariable)
            {
                value = player.GetVariableValue(command.VariableId);
            }
            else if (command.VariableType == VariableTypes.ServerVariable)
            {
                value = ServerVariableBase.Get(command.VariableId)?.Value;
            }
            else if (command.VariableType == VariableTypes.InstanceVariable && MapController.TryGetInstanceFromMap(player.MapId, player.MapInstanceId, out var mapInstance))
            {
                value = mapInstance.GetInstanceVariable(command.VariableId);
            }

            if (value == null)
            {
                value = new VariableValue();
            }

            var originalValue = value.String;

            switch (mod.ModType)
            {
                case Enums.VariableMods.Set:
                    value.String = ParseEventText(mod.Value, player, instance);

                    break;
                case Enums.VariableMods.Replace:
                    var find = ParseEventText(mod.Value, player, instance);
                    var replace = ParseEventText(mod.Replace, player, instance);
                    value.String = value.String.Replace(find, replace);

                    break;
            }

            var changed = value.String != originalValue;

            if (command.VariableType == VariableTypes.PlayerVariable)
            {
                if (changed)
                {
                    player.StartCommonEventsWithTrigger(CommonEventTrigger.PlayerVariableChange, "", command.VariableId.ToString());
                }

                // Set the party member switches too if Sync Party enabled!
                if (command.SyncParty)
                {
                    foreach (var partyMember in player.Party.ToArray())
                    {
                        if (partyMember != player)
                        {
                            partyMember.SetVariableValue(command.VariableId, value.String);
                        }
                    }
                }
            }
            else if (command.VariableType == VariableTypes.ServerVariable && changed)
            {
                Player.StartCommonEventsWithTriggerForAll(Enums.CommonEventTrigger.ServerVariableChange, "", command.VariableId.ToString());
                DbInterface.UpdatedServerVariables.AddOrUpdate(command.VariableId, ServerVariableBase.Get(command.VariableId), (key, oldValue) => ServerVariableBase.Get(command.VariableId));
            }
            else if (command.VariableType == VariableTypes.InstanceVariable && changed && MapController.TryGetInstanceFromMap(player.MapId, player.MapInstanceId, out var mapInstance))
            {
                MapInstance.SetInstanceVariable(command.VariableId, value, mapInstance.MapInstanceId);
                Player.StartCommonEventsWithTriggerForAllOnInstance(Enums.CommonEventTrigger.InstanceVariableChange, player.MapInstanceId, "", command.VariableId.ToString());
            }
        }
    }

    public static partial class CommandProcessing
    {
        private static void ProcessCommand(
            RandomQuestCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            var rand = new Random();
            var quests = QuestListBase.Get(command.QuestListId).Quests;

            // Eliminate quests the player can't do based on quest reqs
            List<Guid> viableQuests = new List<Guid>();
            foreach (var quest in quests)
            {
                if (player.CanStartQuest(QuestBase.Get(quest)))
                {
                    viableQuests.Add(quest);
                }
            }

            if (viableQuests.Count <= 0)
            {
                PacketSender.SendChatMsg(player, Strings.Quests.reqsnotmetforlist.ToString(), ChatMessageType.Local, CustomColors.Alerts.Declined);
            }
            else
            {
                var randomQuestIndex = rand.Next(viableQuests.Count);

                var questToOffer = QuestBase.Get(viableQuests[randomQuestIndex]);
                if (questToOffer != null)
                {
                    player.OfferQuest(questToOffer);
                    stackInfo.WaitingForResponse = CommandInstance.EventResponse.RandomQuest;
                    stackInfo.WaitingOnCommand = command;
                }
            }
        }

        private static void ProcessCommand(
            OpenQuestBoardCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            player.OpenQuestBoard(QuestBoardBase.Get(command.QuestBoardId));
            callStack.Peek().WaitingForResponse = CommandInstance.EventResponse.QuestBoard;
        }

        //Set Vehicle Command
        private static void ProcessCommand(
            SetVehicleCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            player.InVehicle = command.InVehicle;
            if (player.InVehicle)
            {
                player.VehicleSpeed = command.VehicleSpeed;
                player.VehicleSprite = command.VehicleSprite;
            }
            else
            {
                player.VehicleSpeed = 0L;
                player.VehicleSprite = string.Empty;
            }

            PacketSender.SendEntityDataToProximity(player);
        }

        //NPC Guild Management Command

        private static void ProcessCommand(
            NPCGuildManagementCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            var cls = ClassBase.Get(command.ClassId);
            if (player == null || !player.ClassInfo.TryGetValue(command.ClassId, out var classInfo))
            {
                return;
            }

            switch (command.Selection)
            {
                case NPCGuildManagementSelection.ChangeComplete:
                    classInfo.TaskCompleted = command.SelectionValue;
                    break;
                case NPCGuildManagementSelection.ChangeGuildStatus:
                    if (command.SelectionValue)
                    {
                        player.JoinNpcGuildOfClass(command.ClassId);
                    }
                    else
                    {
                        player.LeaveNpcGuildOfClass(command.ClassId);
                    }
                    break;
                case NPCGuildManagementSelection.ChangeTasksRemaining:
                    classInfo.TasksRemaining = command.Value;
                    classInfo.AssignmentAvailable = classInfo.TasksRemaining == 0;
                    if (classInfo.AssignmentAvailable)
                    {
                        PacketSender.SendChatMsg(player,
                               Strings.Quests.newspecialassignment.ToString(cls.Name),
                               ChatMessageType.Quest,
                               CustomColors.Quests.Completed, sendToast: true);
                    }
                    else
                    {
                        PacketSender.SendChatMsg(player,
                            Strings.Quests.tasksremaining.ToString(classInfo.TasksRemaining, cls.Name),
                            ChatMessageType.Quest,
                            CustomColors.Quests.TaskUpdated);
                    }
                    break;
                case NPCGuildManagementSelection.ChangeRank:
                    var oldRank = classInfo.Rank;
                    classInfo.Rank = command.Value;
                    if (oldRank < classInfo.Rank)
                    {
                        PacketSender.SendChatMsg(player,
                            Strings.Quests.classrankincreased.ToString(cls.Name, classInfo.Rank.ToString()),
                            ChatMessageType.Quest,
                            CustomColors.Quests.Completed, sendToast: true);
                        RecipeUnlockWatcher.RefreshPlayer(player);
                        classInfo.TasksRemaining = player.TasksRemainingForClassRank(classInfo.Rank);
                    }
                    else if (oldRank > classInfo.Rank)
                    {
                        PacketSender.SendChatMsg(player,
                            Strings.Quests.classrankdecreased.ToString(cls.Name, classInfo.Rank.ToString()),
                            ChatMessageType.Quest,
                            CustomColors.Quests.Abandoned, sendToast: true);
                        RecipeUnlockWatcher.RefreshPlayer(player);
                        classInfo.TasksRemaining = classInfo.Rank == Options.MaxClassRank ? -1 : 0;
                    }
                    break;
                case NPCGuildManagementSelection.ChangeSpecialAssignment:
                    classInfo.AssignmentAvailable = command.SelectionValue;
                    if (command.SelectionValue)
                    {
                        PacketSender.SendChatMsg(player,
                            Strings.Quests.newspecialassignment.ToString(ClassBase.Get(command.ClassId).Name),
                            ChatMessageType.Quest,
                            CustomColors.Quests.Completed, sendToast: true);
                    }
                    break;
                case NPCGuildManagementSelection.ClearCooldown:
                    classInfo.LastTaskStartTime = 0L;
                    break;
            }
        }

        private static void ProcessCommand(
            AddInspirationCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            if (player == null) return;

            var now = Timing.Global.MillisecondsUtc;
            if (player.InspirationTime < now)
            {
                // Add Millis
                player.InspirationTime = now + (command.Seconds * 1000);
            }
            else
            {
                player.InspirationTime += (command.Seconds * 1000);
            }

            player.SendInspirationUpdateText(command.Seconds);
        }

        private static void ProcessCommand(
            StartTimerCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            if (player == null) return;

            var now = Timing.Global.MillisecondsUtc;

            TimerDescriptor descriptor = TimerDescriptor.Get(command.DescriptorId);
            if (descriptor == null)
            {
                return;
            }

            lock (player.EntityLock)
            {
                if (TimerProcessor.TryGetOwnerId(descriptor.OwnerType, command.DescriptorId, player, out var ownerId) && !TimerProcessor.TryGetActiveTimer(command.DescriptorId, ownerId, out _))
                {
                    TimerProcessor.AddTimer(command.DescriptorId, ownerId, now);
                }

            }
        }

        private static void ProcessCommand(
            StopTimerCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            if (player == null) return;

            var now = Timing.Global.MillisecondsUtc;

            TimerDescriptor descriptor = TimerDescriptor.Get(command.DescriptorId);
            if (descriptor == null)
            {
                return;
            }

            lock (player.EntityLock)
            {
                if (TimerProcessor.TryGetOwnerId(descriptor.OwnerType, command.DescriptorId, player, out var ownerId) && TimerProcessor.TryGetActiveTimer(command.DescriptorId, ownerId, out var activeTimer))
                {
                    var players = activeTimer.GetAffectedPlayers();
                    Action<Action<Player>> stopAction = action =>
                    {
                        foreach (var pl in players)
                        {
                            action(pl);
                        }
                    };

                    switch (command.StopType)
                    {
                        case TimerStopType.None:
                            TimerProcessor.RemoveTimer(activeTimer);
                            break;
                        case TimerStopType.Cancel:
                            stopAction((pl) => pl.EnqueueStartCommonEvent(descriptor.CancellationEvent));
                            TimerProcessor.RemoveTimer(activeTimer);
                            break;

                        case TimerStopType.Complete:
                            stopAction((pl) => pl.EnqueueStartCommonEvent(descriptor.CompletionEvent));
                            TimerProcessor.RemoveTimer(activeTimer, true);
                            break;

                        case TimerStopType.Expire:
                            activeTimer.ExpireTimer(now);
                            break;

                        default:
                            throw new NotImplementedException("Invalid timer stop type received for StopTimerCommand");
                    }
                }
            }
        }

        private static void ProcessCommand(
            ModifyTimerCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            if (player == null) return;

            TimerDescriptor descriptor = TimerDescriptor.Get(command.DescriptorId);
            if (descriptor == null)
            {
                return;
            }

            lock (player.EntityLock)
            {
                if (TimerProcessor.TryGetOwnerId(descriptor.OwnerType, command.DescriptorId, player, out var ownerId) && TimerProcessor.TryGetActiveTimer(command.DescriptorId, ownerId, out var activeTimer))
                {
                    long amount = 0;
                    if (command.IsStatic)
                    {
                        amount = command.Amount;
                    }
                    else
                    {
                        switch (command.VariableType)
                        {
                            case VariableTypes.PlayerVariable:
                                amount = player.GetVariableValue(command.VariableDescriptorId).Integer;
                                break;
                            case VariableTypes.ServerVariable:
                                amount = (int)ServerVariableBase.Get(command.VariableDescriptorId)?.Value.Integer;

                                break;
                            case VariableTypes.InstanceVariable:
                                if (MapController.TryGetInstanceFromMap(player.MapId, player.MapInstanceId, out var mapInstance))
                                {
                                    amount = mapInstance.GetInstanceVariable(command.VariableDescriptorId)?.Value;
                                }

                                break;
                        }
                    }

                    // Convert to seconds
                    amount *= 1000;

                    using (var context = DbInterface.CreatePlayerContext(readOnly: false))
                    {
                        switch (command.Operator)
                        {
                            case TimerOperator.Set:
                                activeTimer.TimeRemaining = Timing.Global.MillisecondsUtc + amount;
                                break;
                            case TimerOperator.Add:
                                activeTimer.TimeRemaining += amount;
                                break;
                            case TimerOperator.Subtract:
                                activeTimer.TimeRemaining -= amount;
                                break;
                            default:
                                throw new NotImplementedException("Invalid operator given to modify timer value");
                        }

                        context.Timers.Update(activeTimer);

                        context.ChangeTracker.DetectChanges();
                        context.SaveChanges();
                    }

                    // Re-sort with new timer values
                    TimerProcessor.ActiveTimers.Sort();

                    // Update client values
                    activeTimer.SendTimerPackets();
                }
            }
        }

        private static void ProcessCommand(
            ChangeSpawnGroupCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            if (player == null) return;

            var mapId = player.MapId;
            if (!command.UsePlayerMap)
            {
                mapId = command.MapId;
            }

            // First, check to see if the map we want to affect is currently being processed on the player's instance ID
            var spawnGroup = command.SpawnGroup;
            if (MapController.TryGetInstanceFromMap(mapId, player.MapInstanceId, out var mapInstance))
            {
                // If it IS, change it, and allow the instance the opportunity to refresh, as players might be there right now
                // and need accurate spawn group data
                var mapSpawnGroup = mapInstance.NpcSpawnGroup;
                switch (command.Operator)
                {
                    case ChangeSpawnOperator.ADD:
                        mapSpawnGroup += spawnGroup;
                        break;
                    case ChangeSpawnOperator.SUBTRACT:
                        mapSpawnGroup -= spawnGroup;
                        break;
                    case ChangeSpawnOperator.SET:
                        mapSpawnGroup = spawnGroup;
                        break;
                    default:
                        throw new NotImplementedException($"Invalid change spawn operator for change spawn group command for player {player?.Name}");
                }
                mapInstance.ChangeSpawnGroup(mapSpawnGroup, command.ResetNpcs, command.ResetInvalidNpcsOnly, command.PersistCleanup, command.SurroundingMaps);
            }
            // if we want this spawn group to continue even after the map is swept up (default 3 mins not on overworld)
            else if (command.PersistCleanup && InstanceProcessor.TryGetInstanceController(player.MapInstanceId, out var instanceController))
            {
                // If it's not, then simply _prepare_ that instance for what its spawn group _should_ be, when it gets instantiated
                instanceController.ChangeSpawnGroup(mapId, spawnGroup, command.PersistCleanup, command.SurroundingMaps);
            }

            return;
        }

        private static void ProcessCommand(
            OpenLeaderboardCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            if (player == null) return;

            player.CloseLeaderboard = false;
            player.SendPacket(new OpenLeaderboardPacket(command.RecordType, command.DisplayName, command.ScoreType, command.RecordId, command.DisplayMode));

            callStack.Peek().WaitingForResponse = CommandInstance.EventResponse.Leaderboard;
        }

        private static void ProcessCommand(
            ClearRecordCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            if (player == null) return;

            if (!command.ClearAll)
            {
                player.DeleteRecord(command.RecordType, command.RecordId);
                return;
            }

            PlayerRecord.RemoveAllRecordsOfType(command.RecordId, command.RecordType);
        }

        private static void ProcessCommand(
            RollLootCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            if (player == null) return;

            player.OpenLootRoll(instance.BaseEvent.Id, command.LootTables);
            PacketSender.SendOpenLootPacketTo(player, command.Title, GameObjects.Events.LootAnimType.Chest);

            callStack.Peek().WaitingForResponse = CommandInstance.EventResponse.LootRoll;
        }

        private static void ProcessCommand(
            UnlockLabelCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            if (player == null) return;

            try
            {
                player.ChangeLabelUnlockStatus(command.LabelId, command.Status);
            }
            catch (NotImplementedException e)
            {
                Logging.Log.Error($"Player command processing exception: {e.Message}");
            }
        }

        private static void ProcessChatboxCommand(string text, Player player, Event instance, ChatboxChannel channel, string clr, bool sendToast)
        {
            var txt = ParseEventText(text, player, instance);
            var color = Color.FromName(clr, Strings.Colors.presets);
            switch (channel)
            {
                case ChatboxChannel.Player:
                    PacketSender.SendChatMsg(player, txt, ChatMessageType.Notice, color, sendToast: sendToast);

                    break;
                case ChatboxChannel.Local:
                    PacketSender.SendProximityMsg(txt, ChatMessageType.Local, player.MapId, color);

                    break;
                case ChatboxChannel.Global:
                    PacketSender.SendGlobalMsg(txt, color, string.Empty, ChatMessageType.Global);

                    break;
                case ChatboxChannel.Party:
                    if (player.Party?.Count > 0)
                    {
                        PacketSender.SendPartyMsg(player, txt, color, player.Name);
                    }

                    break;
                case ChatboxChannel.Guild:
                    if (player.Guild != null)
                    {
                        PacketSender.SendGuildMsg(player, txt, color, player.Name);
                    }
                    break;
            }
        }

        private static void ProcessCommand(
            ResetVariableCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            if (player == null) return;

            var players = new List<Player> { player };
            if (command.SyncParty && player.Party?.ToArray().Length > 1)
            {
                foreach (var member in player.Party.Where(mem => mem.Id != player.Id).ToArray())
                {
                    players.Add(member);
                }
            }

            foreach (var validPlayer in players)
            {
                var varsInGroup = validPlayer.Variables
                    .FindAll(playerVar => PlayerVariableBase.Get(playerVar.VariableId)?.VariableGroup == command.Group)
                    .ToArray();

                foreach (var playerVar in varsInGroup)
                {
                    var change = false;
                    switch (playerVar.Value.Type)
                    {
                        case VariableDataTypes.Boolean:
                            change = playerVar.Value.Boolean != default;
                            playerVar.Value.Boolean = default;
                            break;
                        case VariableDataTypes.Integer:
                        case VariableDataTypes.Number:
                            change = playerVar.Value.Integer != default;
                            playerVar.Value.Integer = default;
                            if (validPlayer.TrySetRecord(RecordType.PlayerVariable, playerVar.VariableId, playerVar.Value.Integer))
                            {
                                PlayerRecord.SendNewVariableRecordMessage(playerVar.VariableId, true, validPlayer);
                            }
                            break;
                        case VariableDataTypes.String:
                            change = !string.IsNullOrEmpty(playerVar.Value.String);
                            playerVar.Value.String = string.Empty;
                            break;
                    }
                    if (change && command.FireCommonEvents)
                    {
                        validPlayer.StartCommonEventsWithTrigger(CommonEventTrigger.PlayerVariableChange, "", playerVar.VariableId.ToString());
                    }
                }
            }
        }

        private static void ProcessCommand(
            ResetPermadeadNpcsCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            if (player == null)
            {
                return;
            }

            Guid mapId;

            if (command.UsePlayerMap)
            {
                mapId = player.MapId;
            }
            else
            {
                mapId = command.MapId;
            }

            if (mapId == default)
            {
                return;
            }

            if (!InstanceProcessor.TryGetInstanceController(player.MapInstanceId, out var instanceController))
            {
                return;
            }

            instanceController.ClearPermadeadNpcs(mapId, command.RefreshSpawns);
        }

        private static void ProcessCommand(
            FadeInCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            player.ClientAwaitingFadeCompletion = true;
            player?.SendPacket(new FadePacket(true));

            callStack.Peek().WaitingForResponse = CommandInstance.EventResponse.Fade;
        }

        private static void ProcessCommand(
            FadeOutCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            player.ClientAwaitingFadeCompletion = true;
            player?.SendPacket(new FadePacket(false));

            callStack.Peek().WaitingForResponse = CommandInstance.EventResponse.Fade;
        }

        private static void ProcessCommand(
            ShakeScreenCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            player?.SendPacket(new ShakeScreenPacket(command.Intensity));
        }

        private static void ProcessCommand(
            ChangeSpawnCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            if (player == null)
            {
                return;
            }

            if (command.RespawnType == RespawnChangeType.Default)
            {
                if (command.Reset)
                {
                    player.RespawnOverrideMapId = Guid.Empty;
                    return;
                }

                player.RespawnOverrideMapId = command.MapId;
                player.RespawnOverrideX = command.X;
                player.RespawnOverrideY = command.Y;
                player.RespawnOverrideDir = (byte)command.Direction;
                PacketSender.SendChatMsg(player, Strings.Player.SpawnChanged, ChatMessageType.Notice, CustomColors.General.GeneralPrimary, sendToast: true);
                return;
            }
            else if (command.RespawnType == RespawnChangeType.Arena)
            {
                if (command.Reset)
                {
                    player.ArenaRespawnMapId = Guid.Empty;
                    return;
                }

                player.ArenaRespawnMapId = command.MapId;
                player.ArenaRespawnX = command.X;
                player.ArenaRespawnY = command.Y;
                player.ArenaRespawnDir = (byte)command.Direction;
                return;
            }

            throw new NotImplementedException($"Invalid respawn change type for player {player.Name}");
        }

        private static void ProcessCommand(
            ChangeRecipeStatusCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            if (command.IsUnlocked)
            {
                player?.UnlockRecipe(command.RecipeId);
            }
            else
            {
                player?.RemoveRecipes(command.RecipeId);
            }
        }

        private static void ProcessCommand(
            ChangeBestiaryCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            player?.ChangeBeastUnlockStatus(command.NpcId, command.UnlockType, command.UnlockStatus);
        }

        private static void ProcessCommand(
            ChangeWeaponTrackCommand command,
            Player player,
            Event instance,
            CommandInstance stackInfo,
            Stack<CommandInstance> callStack
        )
        {
            if (player == null || command == null)
            {
                return;
            }

            if (!player.TryGetMastery(command.WeaponTypeId, out var mastery))
            {
                // No need adding the challenge if we're doing a subtractive update
                if (command.ChangeType == WeaponTrackUpdate.Unlearn || command.ChangeType == WeaponTrackUpdate.LoseLevel)
                {
                    return;
                }

                player.WeaponMasteries.Add(new WeaponMasteryInstance(player.Id, command.WeaponTypeId, 0, true));
                player.TryGetMastery(command.WeaponTypeId, out mastery);
            }

            if (mastery == null)
            {
                return;
            }

            var maxLevel = 0;
            if (mastery.WeaponType != default)
            {
                maxLevel = mastery.WeaponType.Unlocks.Keys.OrderByDescending(k => k).FirstOrDefault();
            }

            var prevLevel = mastery.Level;

            switch (command.ChangeType)
            {
                case WeaponTrackUpdate.SetLevel:
                    var newLevel = MathHelper.Clamp((int)command.Amount, 0, maxLevel);
                    mastery.ExpRemaining = 0L;

                    var count = Math.Abs(newLevel - prevLevel);
                    if (prevLevel < newLevel)
                    {
                        while (count > 0)
                        {
                            player.CompleteMasteryChallenges(mastery);
                            player.LevelUpMastery(mastery);
                            count--;
                        }
                    }
                    else if (prevLevel > newLevel)
                    {
                        while (count > 0)
                        {
                            player.RescendMasteryLevel(mastery);
                            count--;
                        }
                    }

                    if (mastery.Level != prevLevel)
                    {
                        PacketSender.SendChatMsg(player,
                            $"Your {mastery.WeaponType.Name} level has changed to {mastery.Level}",
                            ChatMessageType.Experience,
                            sendToast: true);
                    }
                    break;

                case WeaponTrackUpdate.ChangeExp:
                    if (command.Amount > 0)
                    {
                        PacketSender.SendChatMsg(player,
                            $"You've gained {mastery.WeaponType.Name} EXP",
                            ChatMessageType.Experience);
                    }
                    else if (command.Amount < 0)
                    {
                        PacketSender.SendChatMsg(player,
                            $"You've lost {mastery.WeaponType.Name} EXP",
                            ChatMessageType.Experience);
                    }

                    player.TryGainMasteryExp(command.Amount, mastery);
                    break;

                case WeaponTrackUpdate.GainLevel:
                    var x = command.Amount;
                    while (x > 0)
                    {
                        player.CompleteMasteryChallenges(mastery);
                        player.LevelUpMastery(mastery);
                        x--;
                    }

                    if (mastery.Level != prevLevel)
                    {
                        PacketSender.SendChatMsg(player,
                            $"Your {mastery.WeaponType.Name} level has changed to {mastery.Level}",
                            ChatMessageType.Experience,
                            sendToast: true);
                    }
                    break;

                case WeaponTrackUpdate.LoseLevel:
                    var y = command.Amount;
                    while (y > 0)
                    {
                        player.RescendMasteryLevel(mastery);
                        y--;
                    }
                    if (mastery.Level != prevLevel)
                    {
                        PacketSender.SendChatMsg(player,
                            $"Your {mastery.WeaponType.Name} level has changed to {mastery.Level}",
                            ChatMessageType.Experience,
                            sendToast: true);
                    }
                    break;

                case WeaponTrackUpdate.Unlearn:
                    player.WeaponMasteries.Remove(mastery);

                    // Reset all associated challenges
                    var descriptor = mastery.WeaponType;
                    if (descriptor != null)
                    {
                        foreach (var challengeList in descriptor.Unlocks.Values.Select(unlock => unlock.ChallengeIds).ToArray())
                        {
                            foreach (var challenge in challengeList)
                            {
                                player.RemoveChallenge(challenge);
                            }
                        }
                    }

                    // And remove the mastery all together
                    DbInterface.Pool.QueueWorkItem(mastery.RemoveFromDb);

                    PacketSender.SendChatMsg(player,
                            $"Your progress toward {mastery.WeaponType.Name} mastery has been reset",
                            ChatMessageType.Experience,
                            sendToast: true);
                    break;

            }

            // Get a fresh update of current mastery status
            if (player.TrackedWeaponType == command.WeaponTypeId)
            {
                player.TrackWeaponTypeProgress(command.WeaponTypeId);
                PacketSender.SendExperience(player);
            }
            player.SetMasteryProgress();
        }

        private static void ProcessCommand(
           ChangeChallengeCommand command,
           Player player,
           Event instance,
           CommandInstance stackInfo,
           Stack<CommandInstance> callStack
        )
        {
            var descriptor = ChallengeDescriptor.Get(command.ChallengeId);

            if (descriptor == null || player == null)
            {
                return;
            }

            if (!player.TryGetChallenge(command.ChallengeId, out var challenge))
            {
                return;
            }

            switch (command.ChangeType)
            {
                case ChallengeUpdate.ChangeSets:
                    if (challenge.Complete && command.Amount > 0)
                    {
                        break;
                    }

                    if (descriptor.RequiresContract && player.ChallengeContractId != descriptor.Id)
                    {
                        break;
                    }

                    challenge.Progress = MathHelper.Clamp(challenge.Progress + command.Amount, 0, descriptor.Sets);
                    challenge.Complete = challenge.Progress >= descriptor.Sets;
                    if (challenge.Complete)
                    {
                        if (challenge.Challenge.RequiresContract && player.ChallengeContractId == descriptor.Id)
                        {
                            player.ChallengeContractId = Guid.Empty;
                        }

                        if (challenge.Challenge.SpellUnlock != null)
                        {
                            player.TryAddSkillToBook(challenge.Challenge.SpellUnlockId);
                        }

                        PacketSender.SendChatMsg(player,
                            $"Challenge completed: {descriptor.Name}!",
                            ChatMessageType.Experience,
                            sendToast: true);
                    }
                    else
                    {
                        PacketSender.SendChatMsg(player,
                            $"Challenge update: {descriptor.Name} ({challenge.Progress} / {descriptor.Sets})",
                            ChatMessageType.Experience,
                            sendToast: true);
                    }
                    break;

                case ChallengeUpdate.Complete:
                    if (challenge.Complete)
                    {
                        break;
                    }

                    if (challenge.Challenge.RequiresContract && player.ChallengeContractId == challenge.ChallengeId)
                    {
                        player.ChallengeContractId = Guid.Empty;
                        challenge.Complete = true;
                        PacketSender.SendChatMsg(player,
                                $"Challenge completed: {descriptor.Name}!",
                                ChatMessageType.Experience,
                                sendToast: true);
                        if (challenge.Challenge.SpellUnlock != null)
                        {
                            player.TryAddSkillToBook(challenge.Challenge.SpellUnlockId);
                        }
                    }
                    else if (!challenge.Challenge.RequiresContract)
                    {
                        challenge.Complete = true;
                        PacketSender.SendChatMsg(player,
                                $"Challenge completed: {descriptor.Name}!",
                                ChatMessageType.Experience,
                                sendToast: true);
                    }
                    break;

                case ChallengeUpdate.Reset:
                    challenge.Complete = false;
                    challenge.Progress = 0;
                    PacketSender.SendChatMsg(player,
                            $"Challenge reset: {descriptor.Name}!",
                            ChatMessageType.Experience,
                            sendToast: true);
                    break;
            }

            // Refresh mastery progression
            player.SetMasteryProgress();
        }

        private static void ProcessCommand(
           ChangeDungeonCommand command,
           Player player,
           Event instance,
           CommandInstance stackInfo,
           Stack<CommandInstance> callStack
        )
        {
            if (player == null || !InstanceProcessor.TryGetInstanceController(player.MapInstanceId, out var instanceController))
            {
                return;
            }

            switch (command.State)
            {
                case DungeonState.Null:
                    instanceController.RemoveDungeon();
                    break;

                case DungeonState.Inactive:
                    instanceController.ResetDungeon();
                    break;

                case DungeonState.Active:
                    instanceController.StartDungeon(player);
                    break;

                case DungeonState.Complete:
                    instanceController.CompleteDungeon(player);
                    break;

                default:
                    throw new ArgumentException(nameof(command.State));
            }
        }

        private static void ProcessCommand(
           ObtainTreasureGnomeCommand command,
           Player player,
           Event instance,
           CommandInstance stackInfo,
           Stack<CommandInstance> callStack
        )
        {
            if (player == null || !InstanceProcessor.TryGetInstanceController(player.MapInstanceId, out var instanceController))
            {
                return;
            }

            if (instanceController.InstanceIsDungeon && instanceController.Dungeon.Participants.Contains(player))
            {
                instanceController.GetGnome();
            }
            else
            {
                PacketSender.SendChatMsg(player, "You are not a participant of this dungeon!", ChatMessageType.Notice, CustomColors.General.GeneralWarning);
            }
        }

        private static void ProcessCommand(
           RollDungeonLoot command,
           Player player,
           Event instance,
           CommandInstance stackInfo,
           Stack<CommandInstance> callStack
        )
        {
            if (player == null || !InstanceProcessor.TryGetInstanceController(player.MapInstanceId, out var instanceController))
            {
                return;
            }

            if (instanceController.InstanceIsDungeon && instanceController.Dungeon.Participants.Contains(player))
            {
                player.OpenLootRoll(instance.BaseEvent.Id, instanceController.GetDungeonLoot());

                PacketSender.SendOpenLootPacketTo(player, instanceController.DungeonName, GameObjects.Events.LootAnimType.Chest);

                callStack.Peek().WaitingForResponse = CommandInstance.EventResponse.LootRoll;
            }
            else
            {
                PacketSender.SendChatMsg(player, "You did not participate in this dungeon, and are not worthy of its spoils!", ChatMessageType.Notice, CustomColors.General.GeneralWarning);
            }
        }

        private static void ProcessCommand(
           OpenDeconstructorCommand command,
           Player player,
           Event instance,
           CommandInstance stackInfo,
           Stack<CommandInstance> callStack
        )
        {
            if (player == null)
            {
                return;
            }

            var existingDeconRoll = player.LootRolls.Find(roll => roll.EventId == instance.BaseEvent.Id);
            if (existingDeconRoll != default && existingDeconRoll.Loot.Count > 0)
            {
                player.OpenLootRoll(instance.BaseEvent.Id, new List<LootRoll>());
                PacketSender.SendOpenLootPacketTo(player, "Deconstruction", LootAnimType.Deconstruct, command.DisableBank);

                callStack.Peek().WaitingForResponse = CommandInstance.EventResponse.LootRoll;
                return;
            }

            player.OpenDeconstructor(command.FuelCostMultiplier, instance.BaseEvent.Id, command.DisableBank);
            callStack.Peek().WaitingForResponse = CommandInstance.EventResponse.Deconstruction;
        }

        private static void ProcessCommand(
          ChangeEnhancementsCommand command,
          Player player,
          Event instance,
          CommandInstance stackInfo,
          Stack<CommandInstance> callStack
       )
        {
            if (command == default)
            {
                return;
            }

            if (command.Forget)
            {
                player?.TryForgetEnhancement(command.EnhancementId, true);
            }
            else
            {
                player?.TryUnlockEnhancement(command.EnhancementId);
            }
        }

        private static void ProcessCommand(
          OpenEnhancementWindow command,
          Player player,
          Event instance,
          CommandInstance stackInfo,
          Stack<CommandInstance> callStack
       )
        {
            if (player == null)
            {
                return;
            }

            player.OpenWeaponPicker(WeaponPickerResult.Enhancement, command.CurrencyItemId, command.CostMultiplier);
            callStack.Peek().WaitingForResponse = CommandInstance.EventResponse.Enhancement;
        }

        private static void ProcessCommand(
          OpenUpgradeStation command,
          Player player,
          Event instance,
          CommandInstance stackInfo,
          Stack<CommandInstance> callStack
       )
        {
            if (player == null)
            {
                return;
            }

            player.OpenWeaponPicker(WeaponPickerResult.Upgrade, command.CurrencyItemId, command.CostMultiplier);
            callStack.Peek().WaitingForResponse = CommandInstance.EventResponse.Upgrade;
        }

        private static void ProcessCommand(
          RemovePermabuffCommand command,
          Player player,
          Event instance,
          CommandInstance stackInfo,
          Stack<CommandInstance> callStack
       )
        {
            if (player == null)
            {
                return;
            }

            if (player.TryRemovePermabuff(command.ItemId, false))
            {
                player.RecalculateStatsAndPoints();
                PacketSender.SendUsedPermabuffs(player);
            }
        }

        private static void ProcessCommand(
          ResetGlobalEventPositions command,
          Player player,
          Event instance,
          CommandInstance stackInfo,
          Stack<CommandInstance> callStack
       )
        {
            if (player == null)
            {
                return;
            }

            // Currently only works for overworld
            if (!MapController.TryGetInstanceFromMap(command.MapId, Guid.Empty, out var mapInstance))
            {
                return;
            }

            foreach (var evt in mapInstance.GetGlobalEventInstances())
            {
                evt.GlobalPageInstance[evt.PageIndex].ResetPosition();
            }
        }

        private static void ProcessCommand(
          MeleeSignupCommand command,
          Player player,
          Event instance,
          CommandInstance stackInfo,
          Stack<CommandInstance> callStack
       )
        {
            if (player == null || !InstanceProcessor.TryGetInstanceController(player.MapInstanceId, out var instanceController))
            {
                return;
            }

            if (instanceController.DuelPool.Contains(player))
            {
                return;
            }

            player.MeleeSignup(command.DuelMapId, command.RespawnMapId, command.Duelist1X, command.Duelist1Y, command.Duelist1Dir, command.Duelist2X, command.Duelist2Y, command.Duelist2Dir, command.RespawnX, command.RespawnY, command.RespawnDir);
            player.EnterInstanceMeleePool();
        }

        private static void ProcessCommand(
          MeleeWithdrawCommand command,
          Player player,
          Event instance,
          CommandInstance stackInfo,
          Stack<CommandInstance> callStack
       )
        {
            if (player == null || !InstanceProcessor.TryGetInstanceController(player.MapInstanceId, out var instanceController))
            {
                return;
            }

            if (!instanceController.PlayerInPool(player))
            {
                return;
            }

            player.WithdrawFromMelee();
        }

        private static void ProcessCommand(
          ChangeChampSettingsCommand command,
          Player player,
          Event instance,
          CommandInstance stackInfo,
          Stack<CommandInstance> callStack
       )
        {
            if (player == null)
            {
                return;
            }

            player.DisableChampionSpawns = command.DisableSpawns;
            if (player.DisableChampionSpawns)
            {
                PacketSender.SendToast(player, "Champion spawning has been disabled.");
            }
            else
            {
                PacketSender.SendToast(player, "Champion spawning has been enabled.");
            }
        }

        private static void ProcessCommand(
          HideEventCommand command,
          Player player,
          Event instance,
          CommandInstance stackInfo,
          Stack<CommandInstance> callStack
       )
        {
            if (player == null)
            {
                return;
            }

            foreach (var evt in player.EventLookup)
            {
                if (evt.Value.BaseEvent.Id != command.EventId || evt.Value.PageInstance == null)
                {
                    continue;
                }

                if (evt.Value.PageInstance.IsGlobal)
                {
                    evt.Value.PageInstance.GlobalClone.HideEvent();
                }
                else
                {
                    evt.Value.PageInstance.HideEvent();
                }
            }
        }

        private static void ProcessCommand(
          ShowEventCommand command,
          Player player,
          Event instance,
          CommandInstance stackInfo,
          Stack<CommandInstance> callStack
       )
        {
            if (player == null)
            {
                return;
            }

            foreach (var evt in player.EventLookup)
            {
                if (evt.Value.BaseEvent.Id != command.EventId || evt.Value.PageInstance == null)
                {
                    continue;
                }

                if (evt.Value.PageInstance.IsGlobal)
                {
                    evt.Value.PageInstance.GlobalClone.ShowEvent();
                }
                else
                {
                    evt.Value.PageInstance.ShowEvent();
                }
            }
        }

        private static void ProcessCommand(
           CastSpellOn command,
           Player player,
           Event instance,
           CommandInstance stackInfo,
           Stack<CommandInstance> callStack
       )
        {
            if (player == null || !player.Online || !SpellBase.TryGet(command.SpellId, out var spell))
            {
                return;
            }

            List<Player> affectedPlayers = new List<Player>();

            if (command.Self)
            {
                affectedPlayers.Add(player);
            }

            if (command.PartyMembers && player.IsInParty)
            {
                affectedPlayers.AddRange(player.Party.Where(pl => pl.Id != player.Id));
            }

            if (command.GuildMembers && player.IsInGuild)
            {
                affectedPlayers.AddRange(player.Guild.FindOnlineMembers().Where(pl => pl.Id != player.Id));
            }

            foreach (var affectedPlayer in affectedPlayers.GroupBy(pl => pl.Id).Select(pl => pl.FirstOrDefault()))
            {
                if (!affectedPlayer.Online)
                {
                    continue;
                }

                affectedPlayer?.UseSpell(spell, -1, affectedPlayer, ignoreVitals: true, instantCast: true);
            }
        }
    }
}
