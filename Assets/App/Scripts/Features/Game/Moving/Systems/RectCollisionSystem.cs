using App.Scripts.Features.Game.Level.Components;
using App.Scripts.Features.Game.Moving.Aspects;
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
                    
                    var aspectA = _aspectFactory.Get(entityA);
                    var aspectB = _aspectFactory.Get(entityB);

                    var posA = aspectA.Position.Value;
                    var posB = aspectB.Position.Value;
                    var sizeA = aspectA.Collider.Size;
                    var sizeB = aspectB.Collider.Size;

                    var rectA = new Rect(posA - sizeA * 0.5f, sizeA);
                    var rectB = new Rect(posB - sizeB * 0.5f, sizeB);

                    if (rectA.Overlaps(rectB))
                    {
                        ResolveCollision(ref aspectA, ref aspectB);
                    }
                }
            }
        }

        private void ResolveCollision(ref CollisionAspect a, ref CollisionAspect b)
        {
            World.SendMassage(new EventCollided(a.Entity, b.Entity));
        }
    }
}
