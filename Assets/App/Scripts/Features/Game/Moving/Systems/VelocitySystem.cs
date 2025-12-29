using App.Scripts.Features.Game.Moving.Aspects;
using App.Scripts.Features.Game.Moving.Components;
using App.Scripts.Infrastructure.WorldExtesions.Systems;
using Scellecs.Morpeh;

namespace App.Scripts.Features.Game.Moving.Systems
{
    public class VelocitySystem : FixedSystemBase
    {
        private Filter _velocityFilter;
        private AspectFactory<TransformAspect> _transformAspect;
        private AspectFactory<VelocityAspect> _physicsAspect;

        public override void OnAwake()
        {
            _velocityFilter = World.Filter.Extend<TransformAspect>().Extend<VelocityAspect>().Without<StopMoveTag>()
                .Build();
            _transformAspect = World.GetAspectFactory<TransformAspect>();
            _physicsAspect = World.GetAspectFactory<VelocityAspect>();
        }

        public override void OnUpdate(float dt)
        {
            foreach (var entity in _velocityFilter)
            {
                var transform = _transformAspect.Get(entity);
                var physicsBody = _physicsAspect.Get(entity);
                transform.Rotate(physicsBody.AngularVelocity.Value * dt);
                transform.Translate(physicsBody.Velocity.Value * dt);
                transform.AddSize(physicsBody.ScaleVelocity.Value * dt);
            }
        }
    }
}