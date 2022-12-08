﻿using Intersect.Client.Framework.File_Management;
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
        public static GameTexture AddHealthTexture { get; set; }
        public static GameTexture AddManaTexture { get; set; }
        public static GameTexture DamageNeutralTexture { get; set; }
        public static GameTexture DamageNeutralFlashTexture { get; set; }
        public static GameTexture DamageManaTexture { get; set; }
        public static GameTexture DamageManaFlashTexture { get; set; }
        public static GameTexture DamageCriticalTexture { get; set; }
        public static GameTexture DamageCriticalFlashTexture { get; set; }

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

            DamageNeutralTexture = Globals.ContentManager.GetTexture(
                GameContentManager.TextureType.Gui,
                "combat_damageneutral.png"
            );

            DamageNeutralFlashTexture = Globals.ContentManager.GetTexture(
                GameContentManager.TextureType.Gui,
                "combat_damageneutral_flash.png"
            );

            DamageManaTexture = Globals.ContentManager.GetTexture(
                GameContentManager.TextureType.Gui,
                "combat_damagemana.png"
            );

            DamageManaFlashTexture = Globals.ContentManager.GetTexture(
                GameContentManager.TextureType.Gui,
                "combat_damagemana_flash.png"
            );

            DamageCriticalTexture = Globals.ContentManager.GetTexture(
                GameContentManager.TextureType.Gui,
                "combat_damagecrit.png"
            );

            DamageCriticalFlashTexture = Globals.ContentManager.GetTexture(
                GameContentManager.TextureType.Gui,
                "combat_damagecrit_flash.png"
            );

            AddHealthTexture = Globals.ContentManager.GetTexture(
                GameContentManager.TextureType.Gui,
                "combat_addhealth.png"
            );

            AddManaTexture = Globals.ContentManager.GetTexture(
                GameContentManager.TextureType.Gui,
                "combat_addmana.png"
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