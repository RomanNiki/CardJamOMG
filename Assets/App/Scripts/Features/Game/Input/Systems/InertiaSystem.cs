using App.Scripts.Features.Game.Input.Components;
using App.Scripts.Features.Game.Moving.Components;
using App.Scripts.Infrastructure.WorldExtesions.Systems;
using Scellecs.Morpeh;
using UnityEngine;

namespace App.Scripts.Features.Game.Input.Systems
{
    public class InertiaSystem : SystemBase
    {
        private Filter _filter;
        private Stash<Velocity> _velocityStash;
        private Stash<AngularVelocity> _angularVelocityStash;
        private Stash<Inertia> _inertiaStash;

        public override void OnAwake()
        {
            _filter = World.Filter.With<Velocity>().With<Inertia>().Build();
            _velocityStash = World.GetStash<Velocity>();
            _angularVelocityStash = World.GetStash<AngularVelocity>();
            _inertiaStash = World.GetStash<Inertia>();
        }

        public override void OnUpdate(float dt)
        {
            foreach (var entity in _filter)
            {
                ref var velocity = ref _velocityStash.Get(entity);
                ref var inertia = ref _inertiaStash.Get(entity);

                float frictionEffect = Mathf.Clamp01(1f - inertia.Friction * dt);

                if (velocity.Value.sqrMagnitude > 0.001f)
                {
                    // Применяем трение: скорость уменьшается пропорционально Friction и времени
                    // v = v * (1 - friction * dt)
                    velocity.Value *= frictionEffect;
                    
                    // Если скорость стала очень маленькой, обнуляем её
                    if (velocity.Value.sqrMagnitude <= 0.001f)
                    {
                        velocity.Value = Vector2.zero;
                    }
                }

                if (_angularVelocityStash.Has(entity))
                {
                    ref var angularVelocity = ref _angularVelocityStash.Get(entity);
                    if (Mathf.Abs(angularVelocity.Value) > 0.001f)
                    {
                        angularVelocity.Value *= frictionEffect;
                        if (Mathf.Abs(angularVelocity.Value) <= 0.001f)
                        {
                            angularVelocity.Value = 0f;
                        }
                    }
                }
            }
        }
    }
}
