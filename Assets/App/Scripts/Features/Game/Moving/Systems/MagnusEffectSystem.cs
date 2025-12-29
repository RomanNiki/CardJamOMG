using App.Scripts.Features.Game.Moving.Components;
using App.Scripts.Infrastructure.WorldExtesions.Systems;
using Scellecs.Morpeh;
using UnityEngine;

namespace App.Scripts.Features.Game.Moving.Systems
{
    public class MagnusEffectSystem : SystemBase
    {
        private Filter _filter;
        private Stash<Velocity> _velocity;
        private Stash<AngularVelocity> _angular;

        public override void OnAwake()
        {
            _filter = World.Filter
                .With<Velocity>()
                .With<AngularVelocity>()
                .Build();

            _velocity = World.GetStash<Velocity>();
            _angular = World.GetStash<AngularVelocity>();
        }

        public override void OnUpdate(float dt)
        {
            foreach (var entity in _filter)
            {
                ref var velocity = ref _velocity.Get(entity);
                ref var angular = ref _angular.Get(entity);

                Vector2 v = velocity.Value;

                if (v.sqrMagnitude < 0.001f)
                    continue;

                Vector2 curve = new Vector2(-v.y, v.x) * angular.Value * 0.005f;
                velocity.Value += curve * dt;
            }
        }
    }

}