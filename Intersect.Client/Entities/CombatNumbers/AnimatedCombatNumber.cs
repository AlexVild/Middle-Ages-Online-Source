using Intersect.GameObjects.Events;
using Intersect.Utilities;
using System;

namespace Intersect.Client.Entities.CombatNumbers
{
    public abstract class AnimatedCombatNumber : CombatNumber
    {
        protected bool Looping { get; set; }

        public AnimatedCombatNumber(Guid target, 
            int value, 
            CombatNumberType type, 
            int fallbackX,
            int fallbackY,
            Guid fallbackMapId, 
            Entity visibleTo = null) : base(target, value, type, fallbackX, fallbackY, fallbackMapId, visibleTo)
        {
            Animated = true;
        }

        protected abstract void InitializeAnimation();

        protected override void AdvanceAnimation()
        {
            var framesSinceStart = (int)Math.Floor((double)(Timing.Global.MillisecondsUtcUnsynced - CreatedAt) / FrameRate);
            if (Looping)
            {
                CurrentFrame = framesSinceStart % FramesInAnimaton;
                return;
            }
            CurrentFrame = framesSinceStart;
        }

        protected override void Animate()
        {
            throw new NotImplementedException();
        }

        protected override void Draw()
        {
            throw new NotImplementedException();
        }
    }
}
