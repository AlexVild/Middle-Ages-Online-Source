﻿// <auto-generated />
using System;
using Intersect.Server.Database.PlayerData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Intersect.Server.Migrations
{
    [DbContext(typeof(PlayerContext))]
    partial class PlayerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065");

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Api.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ClientId");

                    b.Property<DateTime>("Expires");

                    b.Property<DateTime>("Issued");

                    b.Property<string>("Subject")
                        .IsRequired();

                    b.Property<string>("Ticket")
                        .IsRequired();

                    b.Property<Guid>("TicketId");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Ban", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Banner");

                    b.Property<DateTime>("EndTime");

                    b.Property<string>("Ip");

                    b.Property<string>("Reason");

                    b.Property<DateTime>("StartTime");

                    b.Property<Guid>("UserId")
                        .HasColumnName("PlayerId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Bans");
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Mute", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndTime");

                    b.Property<string>("Ip");

                    b.Property<string>("Muter");

                    b.Property<string>("Reason");

                    b.Property<DateTime>("StartTime");

                    b.Property<Guid>("UserId")
                        .HasColumnName("PlayerId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Mutes");
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Players.Bag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("SlotCount");

                    b.HasKey("Id");

                    b.ToTable("Bags");
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Players.BagSlot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("BagId");

                    b.Property<Guid>("ItemId");

                    b.Property<Guid>("ParentBagId");

                    b.Property<int>("Quantity");

                    b.Property<int>("Slot");

                    b.Property<string>("StatBuffsJson")
                        .HasColumnName("StatBuffs");

                    b.HasKey("Id");

                    b.HasIndex("BagId");

                    b.HasIndex("ParentBagId");

                    b.ToTable("Bag_Items");
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Players.BankSlot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("BagId");

                    b.Property<Guid>("ItemId");

                    b.Property<Guid>("PlayerId");

                    b.Property<int>("Quantity");

                    b.Property<int>("Slot");

                    b.Property<string>("StatBuffsJson")
                        .HasColumnName("StatBuffs");

                    b.HasKey("Id");

                    b.HasIndex("BagId");

                    b.HasIndex("PlayerId");

                    b.ToTable("Player_Bank");
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Players.BestiaryUnlockInstance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("NpcId");

                    b.Property<Guid>("PlayerId");

                    b.Property<int>("UnlockType");

                    b.Property<bool>("Unlocked");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("Player_Bestiary_Unlocks");
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Players.ChallengeInstance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<Guid>("ChallengeId")
                        .HasColumnName("Challenge");

                    b.Property<bool>("Complete");

                    b.Property<Guid>("PlayerId");

                    b.Property<int>("Progress");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("Player_Challenges");
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Players.CosmeticInstance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ItemId");

                    b.Property<Guid>("PlayerId");

                    b.Property<bool>("Unlocked");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("Player_Cosmetics");
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Players.Friend", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("OwnerId");

                    b.Property<Guid?>("TargetId");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("TargetId");

                    b.ToTable("Player_Friends");
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Players.Guild", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BankSlotsCount");

                    b.Property<DateTime>("FoundingDate");

                    b.Property<Guid>("GuildInstanceId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Guilds");
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Players.GuildBankSlot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("BagId");

                    b.Property<Guid>("GuildId");

                    b.Property<Guid>("ItemId");

                    b.Property<int>("Quantity");

                    b.Property<int>("Slot");

                    b.Property<string>("StatBuffsJson")
                        .HasColumnName("StatBuffs");

                    b.HasKey("Id");

                    b.HasIndex("BagId");

                    b.HasIndex("GuildId");

                    b.ToTable("Guild_Bank");
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Players.HotbarSlot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("BagId");

                    b.Property<Guid>("ItemOrSpellId");

                    b.Property<Guid>("PlayerId");

                    b.Property<int>("Slot");

                    b.Property<string>("StatBuffsJson")
                        .HasColumnName("PreferredStatBuffs");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("Player_Hotbar");
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Players.InventorySlot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("BagId");

                    b.Property<Guid>("ItemId");

                    b.Property<Guid>("PlayerId");

                    b.Property<int>("Quantity");

                    b.Property<int>("Slot");

                    b.Property<string>("StatBuffsJson")
                        .HasColumnName("StatBuffs");

                    b.HasKey("Id");

                    b.HasIndex("BagId");

                    b.HasIndex("PlayerId");

                    b.ToTable("Player_Items");
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Players.LabelInstance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("DescriptorId");

                    b.Property<bool>("IsNew");

                    b.Property<Guid>("PlayerId");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("Player_Labels");
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Players.LootRollInstance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("EventId");

                    b.Property<string>("LootJson")
                        .HasColumnName("Loot");

                    b.Property<Guid>("PlayerId");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("Loot_Rolls");
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Players.MapExploredInstance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("MapId");

                    b.Property<Guid>("PlayerId");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("Maps_Explored");
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Players.PassiveSpell", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsActive");

                    b.Property<Guid>("PlayerId");

                    b.Property<Guid>("SpellId");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("Player_Passive_Spells");
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Players.PlayerRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("Amount");

                    b.Property<Guid>("PlayerId");

                    b.Property<Guid>("RecordId");

                    b.Property<int>("ScoreType");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("Player_Record");
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Players.Quest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Completed");

                    b.Property<Guid>("PlayerId");

                    b.Property<Guid>("QuestId");

                    b.Property<Guid>("TaskId");

                    b.Property<int>("TaskProgress");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("QuestId", "PlayerId")
                        .IsUnique();

                    b.ToTable("Player_Quests");
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Players.RecipeInstance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("DescriptorId");

                    b.Property<Guid>("PlayerId");

                    b.Property<bool>("Unlocked");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("Player_Recipes");
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Players.SpellSlot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("PlayerId");

                    b.Property<int>("Slot");

                    b.Property<long>("SpellCd");

                    b.Property<Guid>("SpellId");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("Player_Spells");
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Players.Variable", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Json")
                        .HasColumnName("Value");

                    b.Property<Guid>("PlayerId");

                    b.Property<Guid>("VariableId");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("VariableId", "PlayerId")
                        .IsUnique();

                    b.ToTable("Player_Variables");
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Players.WeaponMasteryInstance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("ExpRemaining");

                    b.Property<bool>("IsActive");

                    b.Property<int>("Level");

                    b.Property<Guid>("PlayerId");

                    b.Property<Guid>("WeaponTypeId")
                        .HasColumnName("WeaponType");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("Player_Weapon_Masteries");
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.RecordTeammateInstance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("PlayerId");

                    b.Property<Guid>("RecordInstanceId");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("RecordInstanceId");

                    b.ToTable("Record_Teammate");
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.TimerInstance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CompletionCount");

                    b.Property<Guid>("DescriptorId");

                    b.Property<Guid>("OwnerId");

                    b.Property<long>("TimeRemaining");

                    b.HasKey("Id");

                    b.ToTable("Timers");
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("LastIp");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("PasswordResetCode");

                    b.Property<DateTime?>("PasswordResetTime");

                    b.Property<ulong>("PlayTimeSeconds");

                    b.Property<string>("PowerJson")
                        .HasColumnName("Power");

                    b.Property<DateTime?>("RegistrationDate");

                    b.Property<string>("Salt");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Intersect.Server.Entities.Player", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ArenaRespawnDir");

                    b.Property<Guid>("ArenaRespawnMapId")
                        .HasColumnName("ArenaRespawn");

                    b.Property<int>("ArenaRespawnX");

                    b.Property<int>("ArenaRespawnY");

                    b.Property<Guid>("ClassId");

                    b.Property<string>("ClassInfoJson")
                        .HasColumnName("ClassInfo");

                    b.Property<long>("ComboTimestamp");

                    b.Property<string>("CosmeticsJson")
                        .HasColumnName("Cosmetics");

                    b.Property<DateTime?>("CreationDate");

                    b.Property<Guid?>("DbGuildId");

                    b.Property<string>("DecorJson")
                        .HasColumnName("Decor");

                    b.Property<int>("Dir");

                    b.Property<string>("EquipmentJson")
                        .HasColumnName("Equipment");

                    b.Property<long>("Exp");

                    b.Property<string>("Face");

                    b.Property<string>("FooterLabelJson")
                        .HasColumnName("FooterLabel");

                    b.Property<int>("Gender");

                    b.Property<DateTime>("GuildJoinDate");

                    b.Property<int>("GuildRank");

                    b.Property<string>("HeaderLabelJson")
                        .HasColumnName("HeaderLabel");

                    b.Property<bool>("InVehicle");

                    b.Property<long>("InspirationTime");

                    b.Property<int>("InstanceType");

                    b.Property<string>("ItemCooldownsJson")
                        .HasColumnName("ItemCooldowns");

                    b.Property<string>("JsonColor")
                        .HasColumnName("Color");

                    b.Property<DateTime?>("LastOnline");

                    b.Property<Guid>("LastOverworldMapId")
                        .HasColumnName("LastOverworldMapId");

                    b.Property<int>("LastOverworldX");

                    b.Property<int>("LastOverworldY");

                    b.Property<int>("Level");

                    b.Property<Guid>("MapId");

                    b.Property<string>("Name");

                    b.Property<string>("NameColorJson")
                        .HasColumnName("NameColor");

                    b.Property<Guid>("PersonalMapInstanceId");

                    b.Property<ulong>("PlayTimeSeconds");

                    b.Property<bool>("PlayerDead");

                    b.Property<int>("RespawnOverrideDir");

                    b.Property<Guid>("RespawnOverrideMapId")
                        .HasColumnName("RespawnOverride");

                    b.Property<int>("RespawnOverrideX");

                    b.Property<int>("RespawnOverrideY");

                    b.Property<int>("SharedInstanceRespawnDir");

                    b.Property<Guid>("SharedInstanceRespawnId")
                        .HasColumnName("SharedInstanceRespawnId");

                    b.Property<int>("SharedInstanceRespawnX");

                    b.Property<int>("SharedInstanceRespawnY");

                    b.Property<Guid>("SharedMapInstanceId");

                    b.Property<int>("SkillPointTotal");

                    b.Property<string>("SpellCooldownsJson")
                        .HasColumnName("SpellCooldowns");

                    b.Property<string>("Sprite");

                    b.Property<int>("StatPoints");

                    b.Property<string>("StatPointsJson")
                        .HasColumnName("StatPointAllocations");

                    b.Property<string>("StatsJson")
                        .HasColumnName("BaseStats");

                    b.Property<Guid>("UserId");

                    b.Property<long>("VehicleSpeed");

                    b.Property<string>("VehicleSprite");

                    b.Property<string>("VitalsJson")
                        .HasColumnName("Vitals");

                    b.Property<int>("X");

                    b.Property<int>("Y");

                    b.Property<int>("Z");

                    b.HasKey("Id");

                    b.HasIndex("DbGuildId");

                    b.HasIndex("UserId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("Intersect.Server.Entities.PlayerData.PlayerSkillInstance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Equipped");

                    b.Property<Guid>("PlayerId");

                    b.Property<Guid>("SpellId");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("Player_Unlocked_Skills");
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Api.RefreshToken", b =>
                {
                    b.HasOne("Intersect.Server.Database.PlayerData.User", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Ban", b =>
                {
                    b.HasOne("Intersect.Server.Database.PlayerData.User", "User")
                        .WithOne("Ban")
                        .HasForeignKey("Intersect.Server.Database.PlayerData.Ban", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Mute", b =>
                {
                    b.HasOne("Intersect.Server.Database.PlayerData.User", "User")
                        .WithOne("Mute")
                        .HasForeignKey("Intersect.Server.Database.PlayerData.Mute", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Players.BagSlot", b =>
                {
                    b.HasOne("Intersect.Server.Database.PlayerData.Players.Bag", "Bag")
                        .WithMany()
                        .HasForeignKey("BagId");

                    b.HasOne("Intersect.Server.Database.PlayerData.Players.Bag", "ParentBag")
                        .WithMany("Slots")
                        .HasForeignKey("ParentBagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Players.BankSlot", b =>
                {
                    b.HasOne("Intersect.Server.Database.PlayerData.Players.Bag", "Bag")
                        .WithMany()
                        .HasForeignKey("BagId");

                    b.HasOne("Intersect.Server.Entities.Player", "Player")
                        .WithMany("Bank")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Players.BestiaryUnlockInstance", b =>
                {
                    b.HasOne("Intersect.Server.Entities.Player", "Player")
                        .WithMany("BestiaryUnlocks")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Players.ChallengeInstance", b =>
                {
                    b.HasOne("Intersect.Server.Entities.Player", "Player")
                        .WithMany("Challenges")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Players.CosmeticInstance", b =>
                {
                    b.HasOne("Intersect.Server.Entities.Player", "Player")
                        .WithMany("UnlockedCosmetics")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Players.Friend", b =>
                {
                    b.HasOne("Intersect.Server.Entities.Player", "Owner")
                        .WithMany("Friends")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Intersect.Server.Entities.Player", "Target")
                        .WithMany()
                        .HasForeignKey("TargetId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Players.GuildBankSlot", b =>
                {
                    b.HasOne("Intersect.Server.Database.PlayerData.Players.Bag", "Bag")
                        .WithMany()
                        .HasForeignKey("BagId");

                    b.HasOne("Intersect.Server.Database.PlayerData.Players.Guild", "Guild")
                        .WithMany("Bank")
                        .HasForeignKey("GuildId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Players.HotbarSlot", b =>
                {
                    b.HasOne("Intersect.Server.Entities.Player", "Player")
                        .WithMany("Hotbar")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Players.InventorySlot", b =>
                {
                    b.HasOne("Intersect.Server.Database.PlayerData.Players.Bag", "Bag")
                        .WithMany()
                        .HasForeignKey("BagId");

                    b.HasOne("Intersect.Server.Entities.Player", "Player")
                        .WithMany("Items")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Players.LabelInstance", b =>
                {
                    b.HasOne("Intersect.Server.Entities.Player", "Player")
                        .WithMany("UnlockedLabels")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Players.LootRollInstance", b =>
                {
                    b.HasOne("Intersect.Server.Entities.Player", "Player")
                        .WithMany("LootRolls")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Players.MapExploredInstance", b =>
                {
                    b.HasOne("Intersect.Server.Entities.Player", "Player")
                        .WithMany("MapsExplored")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Players.PassiveSpell", b =>
                {
                    b.HasOne("Intersect.Server.Entities.Player", "Player")
                        .WithMany("PassiveSpells")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Players.PlayerRecord", b =>
                {
                    b.HasOne("Intersect.Server.Entities.Player", "Player")
                        .WithMany("PlayerRecords")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Players.Quest", b =>
                {
                    b.HasOne("Intersect.Server.Entities.Player", "Player")
                        .WithMany("Quests")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Players.RecipeInstance", b =>
                {
                    b.HasOne("Intersect.Server.Entities.Player", "Player")
                        .WithMany("UnlockedRecipes")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Players.SpellSlot", b =>
                {
                    b.HasOne("Intersect.Server.Entities.Player", "Player")
                        .WithMany("Spells")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Players.Variable", b =>
                {
                    b.HasOne("Intersect.Server.Entities.Player", "Player")
                        .WithMany("Variables")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.Players.WeaponMasteryInstance", b =>
                {
                    b.HasOne("Intersect.Server.Entities.Player", "Player")
                        .WithMany("WeaponMasteries")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Intersect.Server.Database.PlayerData.RecordTeammateInstance", b =>
                {
                    b.HasOne("Intersect.Server.Entities.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Intersect.Server.Database.PlayerData.Players.PlayerRecord", "Record")
                        .WithMany("Teammates")
                        .HasForeignKey("RecordInstanceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Intersect.Server.Entities.Player", b =>
                {
                    b.HasOne("Intersect.Server.Database.PlayerData.Players.Guild", "DbGuild")
                        .WithMany()
                        .HasForeignKey("DbGuildId");

                    b.HasOne("Intersect.Server.Database.PlayerData.User", "User")
                        .WithMany("Players")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Intersect.Server.Entities.PlayerData.PlayerSkillInstance", b =>
                {
                    b.HasOne("Intersect.Server.Entities.Player", "Player")
                        .WithMany("SkillBook")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
