using System;

using Intersect.Admin.Actions;
using Intersect.Client.Entities.Events;
using Intersect.Client.General;
using Intersect.Client.Interface.Game;
using Intersect.Client.Maps;
using Intersect.Network.Packets.Client;
using Intersect.Enums;
using Intersect.Utilities;
using Intersect.Client.Entities;
using Intersect.Client.General.Leaderboards;
using Intersect.Client.Interface.Game.Character.Panels;
using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using System.Collections.Generic;

namespace Intersect.Client.Networking
{

    public static partial class PacketSender
    {

        public static void SendPing()
        {
            Network.SendPacket(new PingPacket { Responding = true });
        }

        public static void SendLogin(string username, string password)
        {
            Network.SendPacket(new LoginPacket(username, password));
        }

        public static void SendLogout(bool characterSelect = false)
        {
            Network.SendPacket(new LogoutPacket(characterSelect));
        }

        public static void SendNeedMap(Guid mapId)
        {
            if (mapId == default || MapInstance.Get(mapId) != null || !MapInstance.MapNotRequested(mapId))
            {
                return;
            }
            Network.SendPacket(new NeedMapPacket(mapId));
            MapInstance.UpdateMapRequestTime(mapId);
        }

        public static void SendMove()
        {
            if (Globals.Me.CombatMode)
            {
                Network.SendPacket(new MovePacket(Globals.Me.CurrentMap, Globals.Me.X, Globals.Me.Y, Globals.Me.Dir, Globals.Me.FaceDirection));
            }
            else
            {
                Network.SendPacket(new MovePacket(Globals.Me.CurrentMap, Globals.Me.X, Globals.Me.Y, Globals.Me.Dir));
            }
        }

        public static void SendChatMsg(string msg, byte channel)
        {
            Network.SendPacket(new ChatMsgPacket(msg, channel));
        }

        public static void SendAttack(Guid targetId)
        {
            Network.SendPacket(new AttackPacket(targetId));
        }

        public static void SendBlock(bool blocking)
        {
            Network.SendPacket(new BlockPacket(blocking));
        }

        public static void SendDirection(byte dir, bool force = false)
        {
            var me = Globals.Me;
            if (me == null)
            {
                return;
            }

            // Clear any stale requests
            var now = Timing.Global.Milliseconds;
            while (me.DirRequestTimes.Count > 0 && me.DirRequestTimes.Peek() + Options.Instance.PlayerOpts.DirectionChangeLimiter <= now)
            {
                me.DirRequestTimes.Pop();
            }
            me.DirRequestTimes.Push(now);
            var lastRequest = me.DirRequestTimes.Peek();

            // > 2 because we want to give a little leeway for turning 180 in combat mode
            if (!force && lastRequest + Options.Instance.PlayerOpts.DirectionChangeLimiter > now && me.DirRequestTimes.Count > 2)
            {
                // We're sending too many packets, ignore the request and wait for the player to chill in the Player#Update() method
                return;
            }
            else if (force)
            {
                me.DirRequestTimes.Clear();
            }
            Network.SendPacket(new DirectionPacket(dir));
        }

        public static void SendEnterGame()
        {
            Network.SendPacket(new EnterGamePacket());
        }

        public static void SendActivateEvent(Guid eventId)
        {
            Network.SendPacket(new ActivateEventPacket(eventId));
        }

        public static void SendEventResponse(byte response, Dialog ed)
        {
            Globals.EventDialogs.Remove(ed);
            Globals.LastDialogClosed = Timing.Global.Milliseconds + 100;
            Network.SendPacket(new EventResponsePacket(ed.EventId, response));
        }

        public static void SendEventInputVariable(object sender, EventArgs e)
        {
            Network.SendPacket(
                new EventInputVariablePacket(
                    (Guid) ((InputBox) sender).UserData, (int) ((InputBox) sender).Value, ((InputBox) sender).TextValue
                )
            );
        }

        public static void SendEventInputVariableCancel(object sender, EventArgs e)
        {
            Network.SendPacket(
                new EventInputVariablePacket(
                    (Guid) ((InputBox) sender).UserData, (int) ((InputBox) sender).Value, ((InputBox) sender).TextValue,
                    true
                )
            );
        }

        public static void SendCreateAccount(string username, string password, string email)
        {
            Network.SendPacket(new CreateAccountPacket(username.Trim(), password.Trim(), email.Trim()));
        }

        public static void SendCreateCharacter(string name, Guid classId, int sprite, string[] decors)
        {
            Network.SendPacket(new CreateCharacterPacket(name, classId, sprite, decors));
        }

        public static void SendPickupItem(Guid mapId, int tileIndex, Guid uniqueId)
        {
            Network.SendPacket(new PickupItemPacket(mapId, tileIndex, uniqueId));
        }

        public static void SendSwapInvItems(int item1, int item2)
        {
            Network.SendPacket(new SwapInvItemsPacket(item1, item2));
        }

        public static void SendDropItem(int slot, int amount)
        {
            Network.SendPacket(new DropItemPacket(slot, amount));
        }

        public static void SendDestroyItem(int slot, bool checkCanDrop, int amount = 1)
        {
            Network.SendPacket(new DestroyItemPacket(slot, amount, checkCanDrop));
        }

        public static void SendUseItem(int slot, Guid targetId)
        {
            Network.SendPacket(new UseItemPacket(slot, targetId));
        }

        public static void SendSwapSpells(int spell1, int spell2)
        {
            Network.SendPacket(new SwapSpellsPacket(spell1, spell2));
        }

        public static void SendForgetSpell(int slot)
        {
            Network.SendPacket(new ForgetSpellPacket(slot));
        }

        public static void SendUseSpell(int slot, Guid targetId)
        {
            // TODO Alex: Trying this to avoid multi-casting problems? I think they're due to latency but I really don't know
            if (Globals.Me.LastAttemptedCast > Timing.Global.Milliseconds)
            {
                return;
            }

            Globals.Me.LastAttemptedCast = Timing.Global.Milliseconds + 100;
            Network.SendPacket(new UseSpellPacket(slot, targetId));
        }

        public static void SendUnequipItem(int slot)
        {
            Network.SendPacket(new UnequipItemPacket(slot));
        }

        public static void SendUpgradeStat(byte stat)
        {
            Network.SendPacket(new UpgradeStatPacket(stat));
        }

        public static void SendHotbarUpdate(byte hotbarSlot, sbyte type, int itemIndex)
        {
            Network.SendPacket(new HotbarUpdatePacket(hotbarSlot, type, itemIndex));
        }

        public static void SendHotbarSwap(byte index, byte swapIndex)
        {
            Network.SendPacket(new HotbarSwapPacket(index, swapIndex));
        }

        public static void SendOpenAdminWindow()
        {
            Network.SendPacket(new OpenAdminWindowPacket());
        }

        public static void SendCloseQuestBoard()
        {
            Network.SendPacket(new CloseQuestBoardPacket());
        }

        //Admin Action Packet Should be Here

        public static void SendSellItem(int slot, int amount)
        {
            Network.SendPacket(new SellItemPacket(slot, amount));
        }

        public static void SendBuyItem(int slot, int amount)
        {
            Network.SendPacket(new BuyItemPacket(slot, amount));
        }

        public static void SendCloseShop()
        {
            Network.SendPacket(new CloseShopPacket());
        }

        public static void SendDepositItem(int slot, int amount)
        {
            Network.SendPacket(new DepositItemPacket(slot, amount));
        }

        public static void SendWithdrawItem(int slot, int amount)
        {
            Network.SendPacket(new WithdrawItemPacket(slot, amount));
        }

        public static void SendCloseBank()
        {
            Network.SendPacket(new CloseBankPacket());
        }

        public static void SendCloseCrafting()
        {
            Network.SendPacket(new CloseCraftingPacket());
        }

        public static void SendMoveBankItems(int slot1, int slot2)
        {
            Network.SendPacket(new SwapBankItemsPacket(slot1, slot2));
        }

        public static void SendCraftItem(Guid id, int amount)
        {
            Network.SendPacket(new CraftItemPacket(id, amount));
        }

        public static void SendPartyInvite(Guid targetId)
        {
            Network.SendPacket(new PartyInvitePacket(targetId));
        }

        public static void SendPartyKick(Guid targetId)
        {
            Network.SendPacket(new PartyKickPacket(targetId));
        }

        public static void SendPartyLeave()
        {
            Network.SendPacket(new PartyLeavePacket());
        }

        public static void SendPartyAccept(object sender, EventArgs e)
        {
            Network.SendPacket(new PartyInviteResponsePacket((Guid) ((InputBox) sender).UserData, true));
        }

        public static void SendPartyDecline(object sender, EventArgs e)
        {
            Network.SendPacket(new PartyInviteResponsePacket((Guid) ((InputBox) sender).UserData, false));
        }

        public static void SendAcceptQuest(Guid questId, bool fromQuestBoard)
        {
            Network.SendPacket(new QuestResponsePacket(questId, true, fromQuestBoard));
        }

        public static void SendDeclineQuest(Guid questId, bool fromQuestBoard)
        {
            Network.SendPacket(new QuestResponsePacket(questId, false, fromQuestBoard));
        }

        public static void SendAbandonQuest(Guid questId)
        {
            Network.SendPacket(new AbandonQuestPacket(questId));
        }

        public static void SendTradeRequest(Guid targetId)
        {
            Network.SendPacket(new TradeRequestPacket(targetId));
        }

        public static void SendOfferTradeItem(int slot, int amount)
        {
            Network.SendPacket(new OfferTradeItemPacket(slot, amount));
        }

        public static void SendRevokeTradeItem(int slot, int amount)
        {
            Network.SendPacket(new RevokeTradeItemPacket(slot, amount));
        }

        public static void SendAcceptTrade()
        {
            Network.SendPacket(new AcceptTradePacket());
        }

        public static void SendDeclineTrade()
        {
            Network.SendPacket(new DeclineTradePacket());
        }

        public static void SendTradeRequestAccept(object sender, EventArgs e)
        {
            Network.SendPacket(new TradeRequestResponsePacket((Guid) ((InputBox) sender).UserData, true));
        }

        public static void SendTradeRequestDecline(object sender, EventArgs e)
        {
            Network.SendPacket(new TradeRequestResponsePacket((Guid) ((InputBox) sender).UserData, false));
        }

        public static void SendStoreBagItem(int invSlot, int amount, int bagSlot)
        {
            Network.SendPacket(new StoreBagItemPacket(invSlot, amount, bagSlot));
        }

        public static void SendRetrieveBagItem(int bagSlot, int amount, int invSlot)
        {
            Network.SendPacket(new RetrieveBagItemPacket(bagSlot, amount, invSlot));
        }

        public static void SendCloseBag()
        {
            Network.SendPacket(new CloseBagPacket());
        }

        public static void SendMoveBagItems(int slot1, int slot2)
        {
            Network.SendPacket(new SwapBagItemsPacket(slot1, slot2));
        }

        public static void SendRequestFriends()
        {
            Network.SendPacket(new RequestFriendsPacket());
        }

        public static void SendAddFriend(string name)
        {
            Network.SendPacket(new UpdateFriendsPacket(name, true));
        }

        public static void SendRemoveFriend(string name)
        {
            Network.SendPacket(new UpdateFriendsPacket(name, false));
        }

        public static void SendFriendRequestAccept(Object sender, EventArgs e)
        {
            Network.SendPacket(new FriendRequestResponsePacket((Guid) ((InputBox) sender).UserData, true));
        }

        public static void SendFriendRequestDecline(Object sender, EventArgs e)
        {
            Network.SendPacket(new FriendRequestResponsePacket((Guid) ((InputBox) sender).UserData, false));
        }

        public static void SendSelectCharacter(Guid charId)
        {
            Network.SendPacket(new SelectCharacterPacket(charId));
        }

        public static void SendDeleteCharacter(Guid charId)
        {
            Network.SendPacket(new DeleteCharacterPacket(charId));
        }

        public static void SendNewCharacter()
        {
            Network.SendPacket(new NewCharacterPacket());
        }

        public static void SendRequestPasswordReset(string nameEmail)
        {
            Network.SendPacket(new RequestPasswordResetPacket(nameEmail));
        }

        public static void SendResetPassword(string nameEmail, string code, string hashedPass)
        {
            Network.SendPacket(new ResetPasswordPacket(nameEmail, code, hashedPass));
        }

        public static void SendAdminAction(AdminAction action)
        {
            Network.SendPacket(new AdminActionPacket(action));
        }

        public static void SendBumpEvent(Guid mapId, Guid eventId)
        {
            Network.SendPacket(new BumpPacket(mapId, eventId));
        }

        public static void SendRequestGuild()
        {
            Network.SendPacket(new RequestGuildPacket());
        }

        public static void SendGuildInviteAccept(Object sender, EventArgs e)
        {
            Network.SendPacket(new GuildInviteAcceptPacket());
        }

        public static void SendGuildInviteDecline(Object sender, EventArgs e)
        {
            Network.SendPacket(new GuildInviteDeclinePacket());
        }

        public static void SendInviteGuild(string name)
        {
            Network.SendPacket(new UpdateGuildMemberPacket(Guid.Empty, name, Enums.GuildMemberUpdateActions.Invite));
        }

        public static void SendLeaveGuild()
        {
            Network.SendPacket(new GuildLeavePacket());
        }

        public static void SendKickGuildMember(Guid id)
        {
            Network.SendPacket(new UpdateGuildMemberPacket(id, null, Enums.GuildMemberUpdateActions.Remove));
        }
        public static void SendPromoteGuildMember(Guid id, int rank)
        {
            Network.SendPacket(new UpdateGuildMemberPacket(id, null, Enums.GuildMemberUpdateActions.Promote, rank));
        }

        public static void SendDemoteGuildMember(Guid id, int rank)
        {
            Network.SendPacket(new UpdateGuildMemberPacket(id, null, Enums.GuildMemberUpdateActions.Demote, rank));
        }

        public static void SendTransferGuild(Guid id)
        {
            Network.SendPacket(new UpdateGuildMemberPacket(id, null, Enums.GuildMemberUpdateActions.Transfer));
        }
      
        public static void SendClosePicture(Guid eventId)
        {
            if (eventId != Guid.Empty)
            {
                Network.SendPacket(new PictureClosedPacket(eventId));
            }
        }

        public static void SendMapTransitionReady(Guid newMapId, float x, float y, byte dir, MapInstanceType instanceType, Guid dungeonId)
        {
            Network.SendPacket(new MapTransitionReadyPacket(newMapId, x, y, dir, instanceType, dungeonId));
        }

        public static void SendCraftingInfoPacket()
        {
            Network.SendPacket(new ClassInfoPacket());
        }

        public static void SendQuestPointRequestPacket()
        {
            Network.SendPacket(new SendQuestPointRequestPacket());
        }

        public static void SendBankSortPacket()
        {
            Network.SendPacket(new BankSortPacket());
        }

        public static void RequestQuestsFromList(Guid questList)
        {
            Network.SendPacket(new RequestQuestsFromListPacket(questList));
        }
        
        public static void CancelPlayerCast(Guid playerId)
        {
            Network.SendPacket(new CancelPlayerCastPacket(playerId));
        }
    }

    public static partial class PacketSender
    {
        public static void SendPartyInviteName(string playerName)
        {
            Network.SendPacket(new PartyInviteNamePacket(playerName));
        }

        public static void SendLeaderboardRequest(Leaderboard leaderboard, int page)
        {
            if (Globals.Me == null || Globals.Me.Leaderboard == null)
            {
                return;
            }

            Network.SendPacket(new RequestLeaderboardPacket(page, leaderboard.Type, leaderboard.ScoreType, leaderboard.RecordId, string.Empty, leaderboard.DisplayMode));
        }

        public static void SendLeaderboardRequestForPlayer(Leaderboard leaderboard)
        {
            if (Globals.Me == null || Globals.Me.Leaderboard == null)
            {
                return;
            }

            // a page of -1 indicates we want whatever page the player is on, or 0 if not found
            Network.SendPacket(new RequestLeaderboardPacket(-1, leaderboard.Type, leaderboard.ScoreType, leaderboard.RecordId, string.Empty, leaderboard.DisplayMode));
        }

        public static void SendLeaderboardRequestSearchTerm(Leaderboard leaderboard)
        {
            if (Globals.Me == null || Globals.Me.Leaderboard == null)
            {
                return;
            }

            Network.SendPacket(new RequestLeaderboardPacket(-1, leaderboard.Type, leaderboard.ScoreType, leaderboard.RecordId, leaderboard.SearchTerm, leaderboard.DisplayMode));
        }

        public static void SendCloseLeaderboardPacket()
        {
            Network.SendPacket(new CloseLeaderboardPacket());
        }

        public static void SendLootUpdateRequest(LootUpdateType type, int idx = -1)
        {
            Network.SendPacket(new RequestLootUpdatePacket(type, idx));
        }

        public static void SendFadeFinishPacket()
        {
            Globals.WaitFade = false;
            Network.SendPacket(new FinishedFadePacket());
        }

        public static void SendRequestRespawn()
        {
            Network.SendPacket(new RequestRespawnPacket());
        }

        public static void SendRequestInstanceLeave()
        {
            Network.SendPacket(new RequestInstanceLeavePacket());
        }

        public static void SendRequestResourceInfo(int tool)
        {
            CharacterHarvestingWindowController.WaitingOnServer = true;
            Network.SendPacket(new ResourceInfoRequestPacket(tool));
        }

        public static void SendRequestLabelInfo()
        {
            Network.SendPacket(new RequestLabelsPacket());
        }

        public static void SendSetLabelPacket(Guid descriptorId)
        {
            if (Globals.Me != null)
            {
                Globals.Me.LabelDescriptorId = descriptorId;
            }

            Network.SendPacket(new SetLabelPacket(descriptorId));
        }

        public static void SendRequestCosmetics()
        {
            Network.SendPacket(new RequestCosmeticsPacket());
        }

        public static void SendCosmeticChange(Guid itemId, int slot)
        {
            if (slot >= Options.CosmeticSlots.Count || slot < 0)
            {
                return;
            }

            Network.SendPacket(new CosmeticChangePacket(itemId, Options.CosmeticSlots[slot]));
        }

        public static void SendRequestRecipes(RecipeCraftType craftType, bool withTutorial = false)
        {
            Network.SendPacket(new RequestRecipesPacket(craftType, withTutorial));
        }

        public static void SendRequestUnlockedRecipes()
        {
            Network.SendPacket(new RequestUnlockedRecipesPacket());
        }

        public static void SendRequestRecipeRequirements(Guid recipeId)
        {
            Network.SendPacket(new RequestRecipeRequirementsPacket(recipeId));
        }

        public static void SendRequestKillCounts()
        {
            Network.SendPacket(new RequestKillCountsPacket());
        }

        public static void SendRequestKillCountFor(Guid npcId)
        {
            Network.SendPacket(new RequestKillCountPacket(npcId));
        }

        public static void SendSkillPreparationChange(Guid spellId, bool prepared)
        {
            Network.SendPacket(new SkillPreparationPacket(spellId, prepared));
        }

        public static void SendRequestSkillbook()
        {
            Network.SendPacket(new RequestSkillbookPacket());
        }

        public static void SendRequestChallenges()
        {
            Network.SendPacket(new RequestChallengeProgressPacket());
        }

        public static void SendTrackWeaponProgress(Guid weaponTypeId)
        {
            Network.SendPacket(new TrackWeaponTypePacket(weaponTypeId));
        }

        public static void SendUpdgradeStatsPacket(Dictionary<LevelUpAssignments, int> assignments)
        {
            Network.SendPacket(new UpgradeStatsPacket(assignments));
        }

        public static void SendCloseDeconstructorPacket()
        {
            Network.SendPacket(new CloseDeconstructorPacket());
        }

        public static void SendRequestCreateFuelPacket(Dictionary<int, int> invQuantities)
        {
            Network.SendPacket(new AddFuelPacket(invQuantities));
        }

        public static void SendDeconstructItems(int[] slots)
        {
            Network.SendPacket(new DeconstructItemsPacket(slots));
        }

        public static void SendCloseEnhancementPacket()
        {
            Network.SendPacket(new CloseEnhancementPacket());
        }

        public static void SendApplyEnhancementPacket(Guid[] enhancementIds)
        {
            Network.SendPacket(new ApplyEnhancementsPacket(enhancementIds));
        }

        public static void SendRequestWeaponEnhancementRemoval()
        {
            Network.SendPacket(new RequestWeaponEnhancementRemovalPacket());
        }

        public static void SendCloseUpgradeStation()
        {
            Network.SendPacket(new CloseUpgradeStationPacket());
        }

        public static void SendRequestUpgrade(Guid craftId)
        {
            Network.SendPacket(new RequestUpgradePacket(craftId));
        }

        public static void SendChallengeContractRequest(Guid challengeId)
        {
            Network.SendPacket(new RequestChallengeContractPacket(challengeId));
        }

        public static void SendRequestLoadouts()
        {
            Network.SendPacket(new RequestLoadoutsPacket());
        }

        public static void SendSelectLoadout(Guid loadoutId)
        {
            Network.SendPacket(new SelectLoadoutPacket(loadoutId));
        }

        public static void SendSaveLoadout(string loadoutName)
        {
            Network.SendPacket(new SaveNewLoadoutPacket(loadoutName));
        }

        public static void SendRemoveLoadout(Guid loadoutId)
        {
            Network.SendPacket(new RemoveLoadoutPacket(loadoutId));
        }

        public static void SendOverwriteLoadout(Guid loadoutId)
        {
            Network.SendPacket(new OverwriteLoadoutPacket(loadoutId));
        }

        public static void SendRequestKnownEnhancements()
        {
            Network.SendPacket(new RequestKnownEnhancementsPacket());
        }

        public static void SendWeaponPickerResponse(int invSlot)
        {
            Network.SendPacket(new WeaponPickerResponsePacket(invSlot));
            Interface.Interface.GameUi.HideWeaponPicker();
        }
    }
}
