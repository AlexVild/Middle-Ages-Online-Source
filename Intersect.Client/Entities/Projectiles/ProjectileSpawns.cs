using System;

using Intersect.GameObjects;
using Intersect.Utilities;

namespace Intersect.Client.Entities.Projectiles
{

    public partial class ProjectileSpawns
    {

        public Animation Anim;

        public bool AutoRotate;

        public int Dir;

        public int Distance;

        public Guid MapId;

        //Clientside variables
        public float OffsetX;

        public float OffsetY;

        public ProjectileBase ProjectileBase;

        public Guid SpawnMapId;

        public long SpawnTime;

        public int SpawnX;

        public int SpawnY;

        public long TransmissionTimer = Timing.Global.Milliseconds;

        public int X;

        public int Y;

        public int Z;

        public Projectile Parent;

        public ProjectileSpawns(
            int dir,
            int x,
            int y,
            int z,
            Guid mapId,
            AnimationBase animBase,
            bool autoRotate,
            ProjectileBase projectileBase,
            Projectile parent
        )
        {
            X = x;
            Y = y;
            SpawnX = X;
            SpawnY = Y;
            Z = z;
            MapId = mapId;
            SpawnMapId = MapId;
            Dir = dir;
            Anim = new Animation(animBase, true, autoRotate, Z, parent);
            AutoRotate = autoRotate;
            ProjectileBase = projectileBase;
            Parent = parent;
            TransmissionTimer = Timing.Global.Milliseconds +
                                (long) ((float) parent?.GetSpeedWithModifier() / ProjectileBase.Range);
            SpawnTime = Timing.Global.Milliseconds +
                                (long)((float)parent?.GetSpeedWithModifier() / ProjectileBase.Range);
        }

        public void Dispose()
        {
            Anim.DisposeNextDraw();
        }

    }

}
