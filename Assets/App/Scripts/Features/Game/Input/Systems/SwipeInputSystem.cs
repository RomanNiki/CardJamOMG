using App.Scripts.Features.Game.Input.Components;
using App.Scripts.Features.Game.Moving.Components;
using App.Scripts.Features.Game.Views;
using App.Scripts.Infrastructure.WorldExtesions.Systems;
using Scellecs.Morpeh;
using UnityEngine;

namespace App.Scripts.Features.Game.Input.Systems
{
    public class SwipeInputSystem : SystemBase
    {
        private readonly ViewInputZone _inputZone;
        private Filter _swipeFilter;
        private Stash<SwipeData> _swipeStash;
        private Stash<Velocity> _velocityStash;
        private Stash<AngularVelocity> _angularVelocityStash;
        private Filter _velocityFilter;

        public SwipeInputSystem(ViewInputZone inputZone)
        {
            _inputZone = inputZone;
        }

        public override void OnAwake()
        {
            _swipeFilter = World.Filter.With<SwipeData>().Build();
            _swipeStash = World.GetStash<SwipeData>();
            _velocityStash = World.GetStash<Velocity>();
            _angularVelocityStash = World.GetStash<AngularVelocity>();
            _velocityFilter = World.Filter.With<Velocity>().With<AngularVelocity>().Build();
        }

        public override void OnUpdate(float dt)
        {
            if (!_inputZone.rectTransform.rect.Contains(UnityEngine.Input.mousePosition))
            {
                return;
            }

            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                var entity = World.CreateEntity();
                entity.AddComponent<SwipeData>();
                ref var swipeData = ref _swipeStash.Get(entity);
                swipeData.StartPosition = UnityEngine.Input.mousePosition;
                swipeData.LastPosition = swipeData.StartPosition;
                swipeData.StartTime = Time.time;
                swipeData.AccumulatedTwist = 0f;
            }

            foreach (var entity in _swipeFilter)
            {
                ref var swipeData = ref _swipeStash.Get(entity);
                Vector2 currentPosition = UnityEngine.Input.mousePosition;

                if (UnityEngine.Input.GetMouseButton(0))
                {
                    Vector2 toLast = swipeData.LastPosition - swipeData.StartPosition;
                    Vector2 toCurrent = currentPosition - swipeData.StartPosition;

                    if (toLast.sqrMagnitude > 100f && toCurrent.sqrMagnitude > 100f) // Минимальный порог в 10 пикселей
                    {
                        float angle = Vector2.SignedAngle(toLast, toCurrent);
                        swipeData.AccumulatedTwist += angle;
                    }
                    swipeData.LastPosition = currentPosition;
                }

                if (UnityEngine.Input.GetMouseButtonUp(0))
                {
                    float duration = Time.time - swipeData.StartTime;
                    
                    Vector2 direction = currentPosition - swipeData.StartPosition;
                    
                    // Если свайп слишком короткий по времени, фиксируем его как 0.1с для избежания деления на 0
                    float velocityFactor = duration > 0.01f ? 1f / duration : 100f;
                    Vector2 swipeVelocity = direction * (velocityFactor * 0.01f); // Коэффициент 0.01 для нормализации экранных координат
                    float swipeAngularVelocity = (swipeData.AccumulatedTwist / duration) * 0.1f; // Коэффициент для нормализации
                    
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
