using System.Collections.Generic;
using System.Runtime.Serialization;
using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Intersect.Config
{

    public class PlayerOptions
    {
        /// <summary>
        /// Intersect default for initial player bank slots
        /// </summary>
        public const int DefaultInitialBankSlots = 100;

        /// <summary>
        /// A percentage between 0 and 100 which determines the chance in which they will lose any given item in their inventory when killed.
        /// </summary>
        public int ItemDropChance = 0;

        /// <summary>
        /// Number of bank slots a player has.
        /// </summary>
        public int InitialBankslots { get; set; } = DefaultInitialBankSlots;

        /// <summary>
        /// Number of characters an account may create.
        /// </summary>
        public int MaxCharacters = 1;

        /// <summary>
        /// Number of inventory slots a player has.
        /// </summary>
        public int MaxInventory = 35;

        /// <summary>
        /// Max level a player can achieve.
        /// </summary>
        public int MaxLevel = 100;

        /// <summary>
        /// Number of spell slots a player has.
        /// </summary>
        public int MaxSpells = 35;

        /// <summary>
        /// The highest value a single stat can be for a player.
        /// </summary>
        public int MaxStat = 255;

        /// <summary>
        /// The highest value a single vital can be for a player.
        /// </summary>
        public int MaxVital = 200;

        public int BaseVitalPointIncrease = 2;

        public int BaseStatSkillIncrease = 1;

        /// <summary>
        /// The highest amount of SP a player can have
        /// </summary>
        public int MaxSkillPoint = 50;

        /// <summary>
        /// How long a player must wait before sending a trade/party/friend request after the first as been denied.
        /// </summary>
        public int RequestTimeout = 300000;

        /// <summary>
        /// Distance (in tiles) between players in which a trade offer can be sent and accepted.
        /// </summary>
        public int TradeRange = 6;

        /// <summary>
        /// Unlinks the timers for combat and movement to facilitate complex combat (e.g. kiting)
        /// </summary>
        public bool AllowCombatMovement = true;

        /// <summary>
        /// Configures whether or not the level of a player is shown next to their name.
        /// </summary>
        public bool ShowLevelByName = false;

        /// <summary>
        /// First thanks for letting me share an idea, of losing XP when dying <3
        /// </summary>
        public int ExpLossOnDeathPercent = 0;

        /// <summary>
        /// If true, it will remove the associated exp, otherwise you will lose the exp based on the exp required to level up.
        /// </summary>
        public bool ExpLossFromCurrentExp = true;

        /// <summary>
        /// The amount to divide strength by when determining its HP bonus 
        /// </summary>
        public int AttackHealthDivider = 5;

        /// <summary>
        /// The amount to divide AP by when determining its HP bonus 
        /// </summary>
        public int AbilityPowerManaDivider = 5;

        public float AmmoRetrieveChance = 30.0f;

        public float PvPRPBonus = .15f;

        /// <summary>
        /// Contains the slots used during character creation - instead of relating these with equipment.
        /// </summary>
        public List<string> DecorSlots = new List<string>()
        {
            "Hair",
            "Eyes",
            "Shirt",
            "Extra",
            "Beard",
        };

        public Dictionary<string, string> ShortHairMappings = new Dictionary<string, string>()
        {
            { "hair_f_buns_blue.png", "hair_u_short_blue.png" },
            { "hair_f_buns_brown.png", "hair_u_short_red.png" },
            { "hair_f_buns_pink.png", "hair_u_short_pink.png" },
            { "hair_f_ponytail_blonde.png", "hair_u_short_blonde.png" },
            { "hair_f_ponytail_green.png", "hair_u_short_green.png" },
            { "hair_f_ponytail_red.png", "hair_u_short_red.png" },
            { "hair_u_afro_black.png", "hair_u_short_red.png" },
            { "hair_u_afro_brown.png", "hair_u_short_red.png" },
            { "hair_u_afro_pink.png", "hair_u_short_pink.png" },
        };

        public int HairSlot = 0;
        
        public int EyeSlot = 1;
        
        public int ShirtSlot = 2;

        public int BeardSlot = 4;

        public int ExtraSlot = 3;

        public string ClassRankVarGuid = "df280bcf-f0f4-448a-9430-45d1022b9aa8";

        public string TasksCompletedVarGuid = "cd6f7e2b-7f2e-477b-9425-70c66d2d5dd6";
        
        public string TasksRemainingVarGuid = "5b18da07-f676-44f5-9050-3a66f1ed0a23";

        public string SpecialAssignmentAvailableGuid = "21267b3b-1c6b-434a-ac87-dd77724fe70a";
        
        public string OnSpecialAssignmentVarGuid = "a1c38f8f-919f-4dda-94cf-88551abbbc81";
        
        public string OnTaskVarGuid = "8cd211a9-b27e-460d-b82a-cf620dfe63a3";

        public string InGuildVarGuid = "ed1040cb-7018-4862-b0b5-1f9f081bb562";
        
        public string TaskCompletedVarGuid = "546e9490-f152-4318-9cc6-2c26555fea78";

        public string GoldGuid = "22d762f3-a3e0-4314-8c89-2e54d781b509";

        /// <summary>
        /// How often, in ms, a player will send a dir change packet
        /// </summary>
        public long DirectionChangeLimiter = 100;

        [OnDeserializing]
        internal void OnDeserializingMethod(StreamingContext context)
        {
            DecorSlots.Clear();
        }

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            DecorSlots = new List<string>(DecorSlots.Distinct());
        }
    }

}
