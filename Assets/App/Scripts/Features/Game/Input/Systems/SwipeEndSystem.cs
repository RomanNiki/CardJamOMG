using App.Scripts.Features.Game.Input.Components;
using App.Scripts.Features.Game.Input.Events;
using App.Scripts.Features.Game.Moving.Components;
using App.Scripts.Features.Game.Player.Components;
using App.Scripts.Infrastructure.WorldExtesions.Systems;
using Scellecs.Morpeh;
using UnityEngine;

namespace App.Scripts.Features.Game.Input.Systems
{
    public class SwipeEndSystem : SystemBase
    {
        private Filter _swipeFilter;
        private Stash<SwipeData> _swipeStash;
        private Stash<Velocity> _velocityStash;
        private Stash<AngularVelocity> _angularVelocityStash;
        private Filter _velocityFilter;

        public override void OnAwake()
        {
            _swipeFilter = World.Filter.With<SwipeData>().Build();
            _swipeStash = World.GetStash<SwipeData>();
            _velocityStash = World.GetStash<Velocity>();
            _angularVelocityStash = World.GetStash<AngularVelocity>();
            _velocityFilter = World.Filter.With<Velocity>().With<AngularVelocity>().With<TagCurrentCard>().Build();
        }

        public override void OnUpdate(float dt)
        {
            foreach (var entity in _swipeFilter)
            {
                ref var swipeData = ref _swipeStash.Get(entity);
                Vector2 currentPosition = UnityEngine.Input.mousePosition;

                if (UnityEngine.Input.GetMouseButtonUp(0))
                {
                    float duration = Time.time - swipeData.StartTime;
                    Vector2 direction = currentPosition - swipeData.StartPosition;
                    
                    // Если свайп слишком короткий по времени, фиксируем его как 0.1с для избежания деления на 0
                    float velocityFactor = duration > 0.01f ? 1f / duration : 100f;
                    Vector2 swipeVelocity = direction * (velocityFactor * 0.01f); // Коэффициент 0.01 для нормализации экранных координат
                    float swipeAngularVelocity = (swipeData.AccumulatedTwist / duration) * 0.1f; // Коэффициент для нормализации

                    World.SendMassage(new EventSwipe(swipeVelocity, swipeAngularVelocity));
                    
                    foreach (var movableEntity in _velocityFilter)
                    {
                        ref var velocity = ref _velocityStash.Get(movableEntity);
                        velocity.Value = swipeVelocity;

                        ref var angularVelocity = ref _angularVelocityStash.Get(movableEntity);
                        angularVelocity.Value = -swipeAngularVelocity; // Инвертируем, чтобы поворот соответствовал направлению движения мыши
                    }

                    World.RemoveEntity(entity);
                }
            }
        }
    }
}
