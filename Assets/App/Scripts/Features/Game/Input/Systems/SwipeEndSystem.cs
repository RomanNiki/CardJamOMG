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
        private Filter _velocityFilter;

        public override void OnAwake()
        {
            _swipeFilter = World.Filter.With<SwipeData>().Build();
            _swipeStash = World.GetStash<SwipeData>();
            _velocityFilter = World.Filter.With<TagCurrentCard>().Build();
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

                    if (duration <= 0.2)
                    {
                        World.RemoveEntity(entity);
                        return;
                    }

                    // Если свайп слишком короткий по времени, фиксируем его как 0.1с для избежания деления на 0
                    float velocityFactor = duration > 0.01f ? 1f / duration : 0.1f;
                    Vector2 swipeVelocity =
                        direction * (velocityFactor); // Коэффициент 0.01 для нормализации экранных координат

                    // Расчет угловой скорости на основе смещения от центра (рычаг)
                    // Torque = r x F, в 2D это r.x * F.y - r.y * F.x
                    float torque = (swipeData.Offset.x * swipeVelocity.y - swipeData.Offset.y * swipeVelocity.x) *
                                   0.01f;

                    float swipeDataAccumulatedTwist = (swipeData.AccumulatedTwist / duration) + torque;
                    float swipeAngularVelocity = Mathf.Clamp(swipeDataAccumulatedTwist, -720f, 720f);

                    Debug.Log(
                        $"Swipe velocity: {swipeVelocity}, torque: {torque}, angular velocity: {swipeAngularVelocity}");

                    World.SendMassage(new EventSwipe(swipeVelocity, swipeAngularVelocity));

                    foreach (var movableEntity in _velocityFilter)
                    {
                        World.SendMassage(new ForceRequest()
                        {
                            Entity = movableEntity,
                            Value = swipeVelocity
                        });

                        World.SendMassage(new AngularForceRequest()
                        {
                            Entity = movableEntity,
                            Value = swipeAngularVelocity
                        });

                        movableEntity.RemoveComponent<TagCurrentCard>();
                        movableEntity.SetComponent(new TagThrownCard());
                        movableEntity.RemoveComponent<TagIgnoreGravity>();
                    }

                    World.RemoveEntity(entity);
                }
            }
        }
    }
}