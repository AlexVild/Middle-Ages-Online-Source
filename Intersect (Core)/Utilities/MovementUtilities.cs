using System;

namespace Intersect.Utilities
{
    public static class MovementUtilities
    {
        public static int GetMovementTime(int speed, 
            bool combatMode, 
            int moveDir, 
            int faceDir, 
            bool isHasted, 
            bool isSlowed, 
            float backstepBonus, 
            float strafeBonus)
        {
            var minSpeed = Options.Instance.MovementOpts.MinSpeedMs; // Minimum movement speed
            var speedThreshold = Options.Instance.MovementOpts.SpeedStatSoftcap; // Stat limit that denotes its time to soft-cap increases
            var baseMs = Options.Instance.MovementOpts.BaseSpeedPerTileMs; // Speed (in ms) to move 1 tile at 1 speed
            var maxMs = Options.Instance.MovementOpts.CappedSpeedPerTileMs; // Speed (in ms) to move 1 tile at our soft-cap speed
            var diminishFactor = Options.Instance.MovementOpts.SpeedCapDiminishFactor; // Factor of which to soft cap (i.e, it takes 3.5x more speed to see a difference that 1 speed would normally take)
            var backstepSpeedMod = Options.Instance.CombatOpts.CombatModeBackModifier;
            var strafeSpeedMod = Options.Instance.CombatOpts.CombatModeStrafeModifier;

            float time;
            if (speed <= speedThreshold)
            {
                time = baseMs - (baseMs - maxMs) * (speed / speedThreshold);
            }
            else
            {
                float excessSpeed = speed - speedThreshold;
                float diminishedSpeed = (baseMs - maxMs) / diminishFactor;

                time = maxMs - (diminishedSpeed * (excessSpeed / speedThreshold));
            }

            if (combatMode)
            {
                var maximumTime = time;

                // Bonuses apply as percentages of the original speed modifiers - if backstep is 50% slower, and you have 10% backstep bonus, then backstep is now 50 - (.1 * 50) = 45% slower
                var backstepModifier = backstepSpeedMod - ((backstepSpeedMod - 1) * backstepBonus);
                var strafeModifier = strafeSpeedMod - ((strafeSpeedMod - 1) * strafeBonus);

                if (moveDir != faceDir)
                {
                    switch (moveDir)
                    {
                        //up
                        case 0:
                            if (faceDir == 1)
                            {
                                time *= backstepModifier;
                            }
                            else
                            {
                                time *= strafeModifier;
                            }

                            break;
                        //down
                        case 1:
                            if (faceDir == 0)
                            {
                                time *= backstepModifier;
                            }
                            else
                            {
                                time *= strafeModifier;
                            }
                            break;
                        //left
                        case 2:
                            if (faceDir == 3)
                            {
                                time *= backstepModifier;
                            }
                            else
                            {
                                time *= strafeModifier;
                            }
                            break;
                        //right
                        case 3:
                            if (faceDir == 2)
                            {
                                time *= backstepModifier;
                            }
                            else
                            {
                                time *= strafeModifier;
                            }
                            break;
                    }

                    time = (float)MathHelper.Clamp(time, maximumTime, float.MaxValue);
                }
            }

            if (isSlowed)
            {
                time *= Options.Instance.CombatOpts.SlowedModifier;
            }
            else if (isHasted)
            {
                time /= Options.Instance.CombatOpts.HasteModifier;
            }

            return (int)Math.Round(Math.Min(minSpeed, (float)time));
        }
    }
}
