using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.General;
using Intersect.GameObjects.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Entities.CombatNumbers
{
    public static partial class CombatNumberManager
    {
        public static GameTexture DamageHealthTexture { get; set; }
        public static GameTexture DamageHealthFlashTexture { get; set; }
        public static GameTexture DamageHealthTextureLg { get; set; }
        public static GameTexture DamageHealthFlashTextureLg { get; set; }
        public static GameTexture AddHealthTexture { get; set; }
        public static GameTexture AddHealthTextureLg { get; set; }
        public static GameTexture AddManaTexture { get; set; }
        public static GameTexture AddManaTextureLg { get; set; }
        public static GameTexture DamageNeutralTexture { get; set; }
        public static GameTexture DamageNeutralFlashTexture { get; set; }
        public static GameTexture DamageNeutralTextureLg { get; set; }
        public static GameTexture DamageNeutralFlashTextureLg { get; set; }
        public static GameTexture DamageManaTexture { get; set; }
        public static GameTexture DamageManaFlashTexture { get; set; }
        public static GameTexture DamageManaTextureLg { get; set; }
        public static GameTexture DamageManaFlashTextureLg { get; set; }
        public static GameTexture DamageCriticalTexture { get; set; }
        public static GameTexture DamageCriticalFlashTexture { get; set; }
        public static GameTexture DamageCriticalTextureLg { get; set; }
        public static GameTexture DamageCriticalFlashTextureLg { get; set; }
        public static GameTexture InterruptTexture { get; set; }
        public static GameTexture InterruptTextureFlash { get; set; }

        public static void CacheTextureRefs()
        {
            DamageHealthTexture = Globals.ContentManager.GetTexture(
                GameContentManager.TextureType.Gui,
                "combat_damagehealth.png"
            );

            DamageHealthFlashTexture = Globals.ContentManager.GetTexture(
                GameContentManager.TextureType.Gui,
                "combat_damagehealth_flash.png"
            );

            DamageHealthTextureLg = Globals.ContentManager.GetTexture(
                GameContentManager.TextureType.Gui,
                "combat_damagehealth_lg.png"
            );

            DamageHealthFlashTextureLg = Globals.ContentManager.GetTexture(
                GameContentManager.TextureType.Gui,
                "combat_damagehealth_flash.png"
            );

            DamageNeutralTexture = Globals.ContentManager.GetTexture(
                GameContentManager.TextureType.Gui,
                "combat_damageneutral.png"
            );

            DamageNeutralFlashTexture = Globals.ContentManager.GetTexture(
                GameContentManager.TextureType.Gui,
                "combat_damageneutral_flash.png"
            );

            DamageNeutralTextureLg = Globals.ContentManager.GetTexture(
               GameContentManager.TextureType.Gui,
               "combat_damageneutral_lg.png"
           );

            DamageNeutralFlashTextureLg = Globals.ContentManager.GetTexture(
                GameContentManager.TextureType.Gui,
                "combat_damageneutral_flash_lg.png"
            );

            DamageManaTexture = Globals.ContentManager.GetTexture(
                GameContentManager.TextureType.Gui,
                "combat_damagemana.png"
            );

            DamageManaFlashTexture = Globals.ContentManager.GetTexture(
                GameContentManager.TextureType.Gui,
                "combat_damagemana_flash.png"
            );

            DamageManaTextureLg = Globals.ContentManager.GetTexture(
                GameContentManager.TextureType.Gui,
                "combat_damagemana_lg.png"
            );

            DamageManaFlashTextureLg = Globals.ContentManager.GetTexture(
                GameContentManager.TextureType.Gui,
                "combat_damagemana_flash_lg.png"
            );

            DamageCriticalTexture = Globals.ContentManager.GetTexture(
                GameContentManager.TextureType.Gui,
                "combat_damagecrit.png"
            );

            DamageCriticalFlashTexture = Globals.ContentManager.GetTexture(
                GameContentManager.TextureType.Gui,
                "combat_damagecrit_flash.png"
            );

            DamageCriticalTextureLg = Globals.ContentManager.GetTexture(
                GameContentManager.TextureType.Gui,
                "combat_damagecrit_lg.png"
            );

            DamageCriticalFlashTextureLg = Globals.ContentManager.GetTexture(
                GameContentManager.TextureType.Gui,
                "combat_damagecrit_flash_lg.png"
            );

            AddHealthTexture = Globals.ContentManager.GetTexture(
                GameContentManager.TextureType.Gui,
                "combat_addhealth.png"
            );

            AddManaTexture = Globals.ContentManager.GetTexture(
                GameContentManager.TextureType.Gui,
                "combat_addmana.png"
            );

            AddHealthTextureLg = Globals.ContentManager.GetTexture(
                GameContentManager.TextureType.Gui,
                "combat_addhealth_lg.png"
            );

            AddManaTextureLg = Globals.ContentManager.GetTexture(
                GameContentManager.TextureType.Gui,
                "combat_addmana_lg.png"
            );

            InterruptTexture = Globals.ContentManager.GetTexture(
                GameContentManager.TextureType.Gui,
                "combat_interrupt.png"
            );

            InterruptTextureFlash = Globals.ContentManager.GetTexture(
                GameContentManager.TextureType.Gui,
                "combat_interrupt_flash.png"
            );
        }

        public static bool IsDamageType(CombatNumberType type)
        {
            return type == CombatNumberType.DamageHealth
                || type == CombatNumberType.DamageMana
                || type == CombatNumberType.DamageCritical
                || type == CombatNumberType.Neutral;
        }

        public static bool IsHealingType(CombatNumberType type)
        {
            return type == CombatNumberType.HealHealth
                || type == CombatNumberType.HealMana;
        }

        public static CombatNumber CreateNumber(Guid targetId, 
            int value, 
            CombatNumberType type, 
            int mapX,
            int mapY,
            Guid mapId,
            Entity visibleTo
        )
        {
            if (IsDamageType(type))
            {
                return new DamageNumber(targetId, value, type, mapX, mapY, mapId, visibleTo);
            }
            return new HealingNumber(targetId, value, type, mapX, mapY, mapId, visibleTo);
        }

        public static void PopulateTextures(CombatNumber combatNumber)
        {
            if (combatNumber == null)
            {
                return;
            }

            switch(combatNumber.Type)
            {
                case CombatNumberType.DamageHealth:
                    combatNumber.BackgroundTexture = DamageHealthTexture;
                    combatNumber.BackgroundTextureFlash = DamageHealthFlashTexture;
                    combatNumber.FontColor = new Color(63, 9, 4);
                    combatNumber.FontFlashColor = new Color(241, 199, 194);
                    break;

                case CombatNumberType.DamageMana:
                    combatNumber.BackgroundTexture = DamageManaTexture;
                    combatNumber.BackgroundTextureFlash = DamageManaFlashTexture;
                    combatNumber.FontColor = new Color(54, 3, 75);
                    combatNumber.FontFlashColor = new Color(234, 192, 249);
                    break;
                
                case CombatNumberType.DamageCritical:
                    combatNumber.BackgroundTexture = DamageCriticalTexture;
                    combatNumber.BackgroundTextureFlash = DamageCriticalFlashTexture;
                    combatNumber.FontColor = new Color(50, 19, 0);
                    combatNumber.FontFlashColor = new Color(232, 208, 170);
                    break;

                case CombatNumberType.Neutral:
                    combatNumber.BackgroundTexture = DamageNeutralTexture;
                    combatNumber.BackgroundTextureFlash = DamageNeutralFlashTexture;
                    combatNumber.FontColor = new Color(33, 33, 33);
                    combatNumber.FontFlashColor = new Color(194, 194, 194);
                    break;

                case CombatNumberType.HealHealth:
                    combatNumber.BackgroundTexture = AddHealthTexture;
                    combatNumber.BackgroundTextureFlash = AddHealthTexture;
                    combatNumber.FontColor = new Color(11, 42, 0);
                    combatNumber.FontFlashColor = new Color(201, 226, 158);
                    break;

                case CombatNumberType.HealMana:
                    combatNumber.BackgroundTexture = AddManaTexture;
                    combatNumber.BackgroundTextureFlash = AddManaTexture;
                    combatNumber.FontColor = new Color(15, 15, 101);
                    combatNumber.FontFlashColor = new Color(204, 204, 255);
                    break;

                case CombatNumberType.Interrupt:
                    combatNumber.BackgroundTexture = InterruptTexture;
                    combatNumber.BackgroundTextureFlash = InterruptTextureFlash;
                    combatNumber.FontColor = new Color(63, 9, 4);
                    combatNumber.FontFlashColor = new Color(241, 199, 194);
                    break;
            }
        }
    }

    public static partial class CombatNumberManager
    {
        /// <summary>
        /// Mapping of a combat number's key (<see cref="CombatNumber.GenerateKey(Guid, CombatNumberType)"/> and the
        /// associated combat number
        /// </summary>
        private static Dictionary<string, CombatNumber> EntityCombatNumbers { get; set; } = new Dictionary<string, CombatNumber>();

        public static void AddCombatNumber(Guid targetId, int value, CombatNumberType type, int mapX, int mapY, Guid mapId, Entity visibleTo = null)
        {
            lock (EntityCombatNumbers)
            {
                // If interrupt exists, update it
                if (IsDamageType(type) && EntityCombatNumbers.TryGetValue(CombatNumber.GenerateKey(targetId, CombatNumberType.Interrupt), out var interrupt))
                {
                    interrupt.Refresh(value);
                }

                var combatNumber = CreateNumber(targetId, value, type, mapX, mapY, mapId, visibleTo);
                if (!EntityCombatNumbers.TryGetValue(combatNumber.Id, out var existingNumber))
                {
                    EntityCombatNumbers[combatNumber.Id] = combatNumber;
                    return;
                }

                // Otherwise, what do we need to do?
                if (existingNumber.ShouldRefresh())
                {
                    existingNumber.Refresh(value);
                }
                else
                {
                    EntityCombatNumbers[combatNumber.Id] = combatNumber;
                }
            }
        }

        public static void AddInterrupt(Guid targetId, int value, int threshold, int mapX, int mapY, Guid mapId, Entity visibleTo = null)
        {
            lock (EntityCombatNumbers)
            {
                var interruptNumber = new InterruptNumber(targetId, 0, CombatNumberType.Interrupt, mapX, mapY, mapId, threshold, visibleTo);
                if (!EntityCombatNumbers.TryGetValue(interruptNumber.Id, out var existingNumber))
                {
                    EntityCombatNumbers[interruptNumber.Id] = interruptNumber;
                    return;
                }

                existingNumber.Refresh(value);
            }
        }

        public static void UpdateAndDrawCombatNumbers()
        {
            lock (EntityCombatNumbers)
            {
                foreach (var entity in EntityCombatNumbers.Keys.ToList())
                {
                    var combatNumber = EntityCombatNumbers[entity];
                    if (combatNumber.Cleanup())
                    {
                        EntityCombatNumbers.Remove(entity);
                        continue;
                    }
                    combatNumber.UpdateAndDraw();
                }
            }
        }
    }
}
