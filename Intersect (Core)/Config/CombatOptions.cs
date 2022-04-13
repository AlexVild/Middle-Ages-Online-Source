using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Intersect.Config
{

    public class CombatOptions
    {

        public int BlockingSlow = 30; //Slow when moving with a shield. Default 30%

        public float AgilityMovementSpeedModifier = 0.5f; // how much to divide the speed modification by

        public float SpeedModifier = 0.95f; // Just a baseline hook into speed

        public int CombatTime = 10000; //10 seconds

        public int BaseComboTime = 3000; //3 seconds

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

        public string PlayerDeathAnimationId = "a306ad8b-c58c-4d27-b94b-86dd7173dfd8";

        public bool HideResourceHealthBars = true;
        
        public float DefaultBackstabMultiplier = 1.25f;
        
        public float SneakAttackMultiplier = 1.5f;
        
        public float SwiftAttackSpeedMod = 0.75f;
        
        public int AccurateCritChanceMultiplier = 3;

        public float HasteModifier = 1.2f;
        
        public float SlowedModifier = 1.6f;
        
        public int ConfusionMissPercent = 50;

        public bool TurnWhileCasting = true;
        //Combat
        public int RegenTime = 5000; //5 seconds

        public bool EnableCombatChatMessages = false; // Enables or disables combat chat messages.

        public long ProjectileSpellMovementDelay = 250;

        public List<int> HarvestBonusIntervals = new List<int>()
        {
            30, 90, 250, 500, 1000
        };

        public List<double> HarvestBonuses = new List<double>()
        {
            0.05f, 0.1f, 0.25f, 0.33f, 0.5f
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