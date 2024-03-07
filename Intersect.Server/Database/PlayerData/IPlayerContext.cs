using Intersect.Server.Core.Games.ClanWars;
using Intersect.Server.Database.PlayerData.Api;
using Intersect.Server.Database.PlayerData.Players;
using Intersect.Server.Entities;
using Intersect.Server.Entities.PlayerData;
using Microsoft.EntityFrameworkCore;

namespace Intersect.Server.Database.PlayerData
{
    public interface IPlayerContext : IDbContext
    {
        DbSet<User> Users { get; set; }

        DbSet<Mute> Mutes { get; set; }

        DbSet<Ban> Bans { get; set; }

        DbSet<RefreshToken> RefreshTokens { get; set; }

        DbSet<Player> Players { get; set; }

        DbSet<BankSlot> Player_Bank { get; set; }

        DbSet<Friend> Player_Friends { get; set; }

        DbSet<HotbarSlot> Player_Hotbar { get; set; }

        DbSet<InventorySlot> Player_Items { get; set; }

        DbSet<Quest> Player_Quests { get; set; }

        DbSet<SpellSlot> Player_Spells { get; set; }

        DbSet<PlayerVariable> Player_Variables { get; set; }

        DbSet<Bag> Bags { get; set; }

        DbSet<BagSlot> Bag_Items { get; set; }

        DbSet<Guild> Guilds { get; set; }

        DbSet<GuildBankSlot> Guild_Bank { get; set; }

        DbSet<PlayerRecord> Player_Record { get; set; }
        
        DbSet<TimerInstance> Timers { get; set; }
        
        DbSet<MapExploredInstance> Maps_Explored { get; set; }

        DbSet<RecordTeammateInstance> Record_Teammate { get; set; }

        DbSet<LootRollInstance> Loot_Rolls { get; set; }
        
        DbSet<LabelInstance> Player_Labels { get; set; }
        
        DbSet<CosmeticInstance> Player_Cosmetics { get; set; }
        
        DbSet<PermabuffInstance> Player_Permabuffs { get; set; }
        
        DbSet<RecipeInstance> Player_Recipes { get; set; }
        
        DbSet<BestiaryUnlockInstance> Player_Bestiary_Unlocks { get; set; }
        
        DbSet<PlayerSkillInstance> Player_Unlocked_Skills { get; set; }

        DbSet<PassiveSpell> Player_Passive_Spells { get; set; }

        DbSet<ChallengeInstance> Player_Challenges { get; set; }
        
        DbSet<WeaponMasteryInstance> Player_Weapon_Masteries { get; set; }
        
        DbSet<ItemDiscoveryInstance> Player_Items_Discovered { get; set; }
        
        DbSet<DungeonTrackerInstance> Player_Dungeons_Tracked { get; set; }
        
        DbSet<PlayerEnhancementInstance> Player_Enhancements { get; set; }
        
        DbSet<PlayerLoadout> Player_Loadouts { get; set; }
        
        DbSet<TerritoryInstance> Territories { get; set; }
        
        DbSet<ClanWarInstance> Clan_Wars { get; set; }

        DbSet<ClanWarParticipant> Clan_War_Participants { get; set; }

        DbSet<GuildVariable> Guild_Variables { get; set; }
    }
}
