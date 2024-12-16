using System;
using Intersect.Client.Entities.Events;
using Intersect.Client.General;
using Intersect.Client.Maps;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Utilities;

namespace Intersect.Client.Core
{

    public static partial class Graphics
    {
        private static void DrawMapPanoramas()
        {
            foreach (var kv in ActiveMapGrid.ToArray())
            {
                var mapId = kv.Key;
                DrawMapPanorama(mapId);
            }
        }

        private static void DrawMapsOnLayer(int layer)
        {
            foreach (var kv in ActiveMapGrid.ToArray())
            {
                var mapId = kv.Key;
                DrawMap(mapId, layer);
            }
        }

        private static void DrawTerritories()
        {
            foreach (var kv in ActiveMapGrid.ToArray())
            {
                var map = kv.Value;
                foreach (var territory in map.Territories.Values)
                {
                    territory.Draw();
                }
            }
        }

        private static void DrawSpellMarkers()
        {
            foreach (var entity in ActiveEntities.ToArray())
            {
                if (entity.SpellCast == default || entity.CurrentMap == default)
                {
                    continue;
                }

                var castSpell = SpellBase.Get(entity.SpellCast);
                var map = MapInstance.Get(entity.CurrentMap);
                switch (castSpell.Combat.TargetType)
                {
                    case SpellTargetTypes.AoE:
                        entity.DrawAoe(castSpell, SpellTargetTypes.AoE, map, entity.X, entity.Y, entity.IsAllyOf(Globals.Me), castSpell.Combat.HitRadius);
                        break;
                    
                    case SpellTargetTypes.Single:
                        if (!Globals.Entities.TryGetValue(entity.EntityTarget, out var target) || target.CurrentMap == default)
                        {
                            break;
                        }

                        // Either you're the target or you're the caster
                        if (target.Id == Globals.Me.Id || entity.Id == Globals.Me.Id)
                        {
                            entity.DrawAoe(castSpell, SpellTargetTypes.Single, map, entity.X, entity.Y, entity.IsAllyOf(Globals.Me), castSpell.Combat.CastRange, entity.Id != Globals.Me.Id);
                        }
                        if (castSpell.Combat.HitRadius <= 0)
                        {
                            break;
                        }

                        var entityX = entity.GetCurrentTileRectangle().CenterX;
                        var entityY = entity.GetCurrentTileRectangle().CenterY;
                        var targetX = target.GetCurrentTileRectangle().CenterX;
                        var targetY = target.GetCurrentTileRectangle().CenterY;

                        var distance = Math.Floor(MathHelper.CalculateDistanceToPoint(entityX, entityY, targetX, targetY) / Options.TileWidth);
                        var minRange = castSpell.Combat.MinRange;
                        if (distance > castSpell.Combat.CastRange || (minRange > 0 && distance < minRange))
                        {
                            break;
                        }

                        var targetMap = MapInstance.Get(target.CurrentMap);
                        target.DrawAoe(castSpell, SpellTargetTypes.AoE, targetMap, target.X, target.Y, entity.IsAllyOf(Globals.Me), castSpell.Combat.HitRadius);
                        break;
                    
                    case SpellTargetTypes.Projectile:
                        entity.DrawProjectileSpawns(castSpell, map, entity.X, entity.Y, entity.IsAllyOf(Globals.Me));
                        break;
                }
            }
        }

        private static void DisposeOrphanedAnimations()
        {
            foreach (var animInstance in LiveAnimations.ToArray())
            {
                if (animInstance.ParentGone())
                {
                    animInstance.Dispose();
                }
            }
        }

        private static void DrawLowerAnimations()
        {
            foreach (var animInstance in LiveAnimations.ToArray())
            {
                animInstance.Draw(false);
            }
        }

        private static void DrawMiddleAnimations()
        {
            foreach (var animInstance in LiveAnimations.ToArray())
            {
                animInstance.Draw(false, true);
                animInstance.Draw(true, true);
            }
        }

        private static void DrawUpperAnimations()
        {
            foreach (var animInstance in LiveAnimations.ToArray())
            {
                animInstance.Draw(true);
            }
        }

        private static void EndAnimationDraw()
        {
            foreach (var animInstance in LiveAnimations.ToArray())
            {
                animInstance.EndDraw();
            }
        }

        private static void DrawEntities()
        {
            foreach (var entity in ActiveEntities.ToArray())
            {
                entity.Draw();
                EntitiesDrawn++;
            }
        }

        private static void DrawMapItemsAndLights()
        {
            foreach (var kv in ActiveMapGrid.ToArray())
            {
                var map = kv.Value;
                map.DrawItemsAndLights();
            }
        }

        private static void DrawMapExtras()
        {
            foreach (var kv in ActiveMapGrid.ToArray())
            {
                var map = kv.Value;
                map.DrawWeather();
                map.DrawFog();
                map.DrawOverlayGraphic();
                map.DrawItemNames();
            }
        }

        private static void DrawEntityExtras()
        {
            foreach (var entity in ActiveEntities.ToArray())
            {
                entity.DrawName(null);
                entity.DrawStatuses();
                if (entity.GetType() != typeof(Event))
                {
                    entity.DrawHpBar();
                    entity.DrawCastingBar();
                    entity.DrawAggroIndicator(12, entity.IsAllyOf(Globals.Me));
                    entity.DrawExhaustionIndicator();
                }

                entity.DrawChatBubbles();
            }
        }

        public static void DrawActionMsgs()
        {
            foreach (var kv in ActiveMapGrid.ToArray())
            {
                var map = kv.Value;
                map.DrawActionMsgs();
            }
        }
    }
}
