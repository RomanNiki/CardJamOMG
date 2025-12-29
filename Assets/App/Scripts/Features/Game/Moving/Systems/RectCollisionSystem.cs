using App.Scripts.Features.Game.Level.Components;
using App.Scripts.Features.Game.Moving.Aspects;
using App.Scripts.Features.Game.Moving.Components;
using App.Scripts.Features.Game.Moving.Events;
using App.Scripts.Features.Game.Player.Components;
using App.Scripts.Infrastructure.WorldExtesions.Systems;
using Scellecs.Morpeh;
using UnityEngine;

namespace App.Scripts.Features.Game.Moving.Systems
{
    public class RectCollisionSystem : SystemBase
    {
        private Filter _filter;
        private AspectFactory<CollisionAspect> _aspectFactory;
        private Filter _filterFieldCard;

        public override void OnAwake()
        {
            _filter = World.Filter.Extend<CollisionAspect>().Without<OnField>().Build();
            _filterFieldCard = World.Filter.Extend<CollisionAspect>().With<OnField>().Build();
            _aspectFactory = World.GetAspectFactory<CollisionAspect>();
        }

        public override void OnUpdate(float dt)
        {
            foreach (var entityA in _filter)
            {
                foreach (var entityB in _filterFieldCard)
                {
                    if (entityA == entityB) continue;

                    CollisionAspect aspectA = _aspectFactory.Get(entityA);
                    CollisionAspect aspectB = _aspectFactory.Get(entityB);
                    Vector2 aspectAPosition = aspectA.Position.Value;
                    Vector2 colliderSize = aspectA.Collider.Size;
                    Rect rectA = new Rect(aspectAPosition.x, aspectAPosition.y, colliderSize.x, colliderSize.y);
                    Vector2 aspectBPosition = aspectB.Position.Value;
                    Vector2 colliderSizeB = aspectB.Collider.Size;
                    Rect rectB = new Rect(aspectBPosition.x, aspectBPosition.y,  colliderSizeB.x, colliderSizeB.y);

                    if (rectA.Overlaps(rectB, true))
                    {
                        ResolveCollision(ref aspectA, ref aspectB);
                    }
                }
            }
        }

        private void ResolveCollision(ref CollisionAspect a, ref CollisionAspect b)
        {
            Debug.Log("Collided " + a.Entity + " " + b.Entity);
            World.SendMassage(new EventCollided(a.Entity, b.Entity));
        }
    }
}