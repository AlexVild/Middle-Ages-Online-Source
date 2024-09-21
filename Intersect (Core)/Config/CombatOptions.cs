using Intersect.GameObjects.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Intersect.Config
{

    public class CombatOptions
    {

        public int CombatTime = 10000; //10 seconds

        public int BaseComboTime = 3000; //3 seconds

        public int BasePartyComboTime = 5000; //5 seconds

        public int MaxNpcStat = 500;

        public string MissSound = "";

        public string BlockSound = "";

        public long MPWarningDisplayTime = 2000; //2 seconds

        public long HPWarningFadeTime = 2000; //2 seconds

        public long WarningFlashRate = 250; //250 ms

        public float BaseComboExpModifier = 0.25f;

        public float MaxComboExpModifier = 2.0f;

        public float HPWarningThreshold = 0.2f;

        public float PartyComboModifier = 0.5f;

        public int MinComboExpLvlDiff = 5;

        public long DirChangeTimer = 90;

        public int MaxAttackRate = 200; //5 attacks per second

        public int MaxDashSpeed = 200;

        public int MinAttackRate = 500; //2 attacks per second

        public bool CombatFlashes = true; // Whether or not to flash screen on critical hits

        public float CriticalHitFlashIntensity = 80f;

        public float HitFlashDuration = 600f; // ms

        public string CriticalHitReceivedSound = null;

        public string CriticalHitDealtSound = null;

        public float DamageTakenFlashIntensity = 35f;

        public float DamageTakenShakeAmount = 2.5f;

        public float ResourceDestroyedShakeAmount = 2.5f;

        public float DamageGivenShakeAmount = 4.0f;

        public float MaxDamageShakeDistance = 6.0f;

        public float ShakeDeltaDurationDivider = 150f;

        public string GenericDamageGivenSound = "al_give_damage.wav";

        public string GenericDamageReceivedSound = "al_take_damage.wav";

        public long FaceTargetPredictionTime = 650;

        public long ActionMessageTime = 1500;

        public double ThreatLevelDeductionPerPartyMember = 0.015;

        public string PlayerDeathAnimationId = "a306ad8b-c58c-4d27-b94b-86dd7173dfd8";

        public string BloodshedItemId = "c4b9e542-9ce2-43a0-bcbd-f2e007058823";

        public bool HideResourceHealthBars = true;

        public float DefaultBackstabMultiplier = 1.25f;

        public float SneakAttackMultiplier = 1.5f;

        public float SwiftAttackSpeedMod = 0.75f;

        public int AccurateCritChanceMultiplier = 3;

        public float HasteModifier = 1.2f;

        public float SlowedModifier = 1.6f;

        public float CombatModeBackModifier = 1.33f;

        public float CombatModeStrafeModifier = 1.15f;

        public int ConfusionMissPercent = 50;

        public bool TurnWhileCasting = true;
        //Combat
        public int RegenTime = 5000; //5 seconds
        
        public int ManaRegenTime = 2000;

        public bool EnableCombatChatMessages = false; // Enables or disables combat chat messages.

        public long ProjectileSpellMovementDelay = 250;

        public bool InvulnerableNpcsAffectedByNonDamaging = true;

        public bool CleanseThruStun = false;

        public int PartyTargetDistance = 10;

        // How many ms before cancel cast can't happen
        public int CancelCastLeeway = 250;

        // Multiply by an NPC's combat level to determine overworld hostility
        public float DefaultAggroLevelMultiplier = 1.35f;

        public string DungeonInstanceVarGuid = "df755027-8588-46c6-820b-d314f7825730";

        public string InspiredSpellId { get; set; } = "";

        public bool StunPreventsItems { get; set; } = false;
        
        public bool UseGeneratedMobExp { get; set; } = false;

        public long ResetThreatLevelAggroTime { get; set; } = 30000;

        public long NpcDirChangeTimer { get; set; } = 1200;

        public long NpcDirChangeSpeedMult { get; set; } = 5;

        public int BestiaryCompletionExpMultiplier { get; set; } = 10;

        public float AttunedStatusDividend { get; set; } = 2.0f;

        public int ExpBoostStatusPercent { get; set; } = 10;
        
        public int LumberjackStatusPercent { get; set; } = 10;
        public int ProspectorStatusPercent { get; set; } = 10;
        public int AnglerStatusPercent { get; set; } = 10;

        public string SpellLearnedAnimGuid { get; set; } = "1a2a103a-8d39-4ecb-a323-ed8882a0a057";

        public List<int> HarvestBonusIntervals = new List<int>()
        {
            30, 90, 250, 500, 1000
        };

        public List<double> HarvestBonuses = new List<double>()
        {
            0.05f, 0.1f, 0.25f, 0.33f, 0.5f
        };

        public float SniperDamageDivider = 2.0f;

        public float BerzerkDamageDivider = 2.0f;

        // How long after a heal/buff some entity can be considered an ally
        public long AllyTimer = 10000;

        public float NpcScalePerAggressor = 0.1f;

        public long ChampionDespawnTimeSeconds { get; set; } = 600;

        public long BackstabCooldown { get; set; } = 5000;

        // Threat levels

        public Dictionary<ThreatLevel, double> ThreatLevelThresholds = new Dictionary<ThreatLevel, double>()
        {
            { ThreatLevel.Midnight, 16 },
            { ThreatLevel.Extreme, 8 },
            { ThreatLevel.Deadly, 3 },
            { ThreatLevel.Threatening, 0.35 },
            { ThreatLevel.Fair, 0.15 },
            { ThreatLevel.Wimpy, 0.05 },
            { ThreatLevel.Trivial, 0 },
        };

        public Dictionary<ThreatLevel, double> ThreatLevelPartyThresholds = new Dictionary<ThreatLevel, double>()
        {
            { ThreatLevel.Midnight, 16 },
            { ThreatLevel.Extreme, 8 },
            { ThreatLevel.Deadly, 3 },
            { ThreatLevel.Threatening, 0.35 },
            { ThreatLevel.Fair, 0.15 },
            { ThreatLevel.Wimpy, 0.05 },
            { ThreatLevel.Trivial, 0 },
        };

        public Dictionary<ThreatLevel, double> ThreatLevelExpRates = new Dictionary<ThreatLevel, double>()
        {
            { ThreatLevel.Midnight, 2.0 },
            { ThreatLevel.Extreme, 1.5 },
            { ThreatLevel.Deadly, 1.2 },
            { ThreatLevel.Threatening, 1.05 },
            { ThreatLevel.Fair, 1.0 },
            { ThreatLevel.Wimpy, 0.75 },
            { ThreatLevel.Trivial, .2 },
        };

        //Spells

        /// <summary>
        /// If enabled this allows spell casts to stop/be canceled if the player tries to move around (WASD)
        /// </summary>
        public bool MovementCancelsCast = false;
        
        // Cooldowns

        /// <summary>
        /// Configures whether cooldowns within cooldown groups should match.
        /// </summary>
        public bool MatchGroupCooldowns = true;

        /// <summary>
        /// Only used when <seealso cref="MatchGroupCooldowns"/> is enabled!
        /// Configures whether cooldowns are being matched to the highest cooldown within a cooldown group when true, or are matched to the current item or spell being used when false.
        /// </summary>
        public bool MatchGroupCooldownHighest = true;

        /// <summary>
        /// Only used when <seealso cref="MatchGroupCooldowns"/> is enabled!
        /// Configures whether cooldown groups between items and spells are shared.
        /// </summary>
        public bool LinkSpellAndItemCooldowns = true;

        /// <summary>
        /// Configures whether or not using a spell or item should trigger a global cooldown.
        /// </summary>
        public bool EnableGlobalCooldowns = false;

        /// <summary>
        /// Configures the duration (in milliseconds) which the global cooldown lasts after each ability.
        /// Only used when <seealso cref="EnableGlobalCooldowns"/> is enabled!
        /// </summary>
        public int GlobalCooldownDuration = 1500;

        /// <summary>
        /// Configures the maximum distance a target is allowed to be from the player when auto targetting.
        /// </summary>
        public int MaxPlayerAutoTargetRadius = 15;

        /// <summary>
        /// Configures the amount of time a previously-applied CC is considered stale for.
        /// </summary>
        public int CCStalenessTimer = 15000;

        /// <summary>
        /// Configures the maximum amount of tenacity awarded for stale CC
        /// </summary>
        public int MaxStaleTenacityBonus = 85;

        [OnDeserializing]
        internal void OnDeserializingMethod(StreamingContext context)
        {
            HarvestBonuses.Clear();
            HarvestBonusIntervals.Clear();
        }

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            HarvestBonuses = new List<double>(HarvestBonuses.Distinct());
            HarvestBonusIntervals = new List<int>(HarvestBonusIntervals.Distinct());
        }
    }

}