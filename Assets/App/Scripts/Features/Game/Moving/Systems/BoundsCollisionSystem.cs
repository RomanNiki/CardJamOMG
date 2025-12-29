using App.Scripts.Features.Game.Moving.Components;
using App.Scripts.Infrastructure.WorldExtesions.Systems;
using Scellecs.Morpeh;

namespace App.Scripts.Features.Game.Moving.Systems
{
    public class BoundsCollisionSystem : SystemBase
    {
        private Filter _filter;
        private Stash<Position> _positionStash;
        private Stash<Velocity> _velocityStash;
        private Stash<RectCollider> _rectColliderStash;
        private Stash<BoundsCollider> _boundsColliderStash;

        public override void OnAwake()
        {
            _filter = World.Filter.With<Position>().With<Velocity>().With<RectCollider>().With<BoundsCollider>().Build();
            _positionStash = World.GetStash<Position>();
            _velocityStash = World.GetStash<Velocity>();
            _rectColliderStash = World.GetStash<RectCollider>();
            _boundsColliderStash = World.GetStash<BoundsCollider>();
        }

        public override void OnUpdate(float dt)
        {
            foreach (var entity in _filter)
            {
                ref var pos = ref _positionStash.Get(entity);
                ref var vel = ref _velocityStash.Get(entity);
                var collider = _rectColliderStash.Get(entity);
                var bounds = _boundsColliderStash.Get(entity).Bounds;

                var halfSize = collider.Size * 0.5f;

                if (pos.Value.x - halfSize.x < bounds.xMin)
                {
                    pos.Value.x = bounds.xMin + halfSize.x;
                    if (vel.Value.x < 0) vel.Value.x *= -0.5f; // Bounce with damping
                }
                else if (pos.Value.x + halfSize.x > bounds.xMax)
                {
                    pos.Value.x = bounds.xMax - halfSize.x;
                    if (vel.Value.x > 0) vel.Value.x *= -0.5f;
                }

                if (pos.Value.y - halfSize.y < bounds.yMin)
                {
                    pos.Value.y = bounds.yMin + halfSize.y;
                    if (vel.Value.y < 0) vel.Value.y *= -0.5f;
                }
                else if (pos.Value.y + halfSize.y > bounds.yMax)
                {
                    pos.Value.y = bounds.yMax - halfSize.y;
                    if (vel.Value.y > 0) vel.Value.y *= -0.5f;
                }
            }
        }
    }
}
