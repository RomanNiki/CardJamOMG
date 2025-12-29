using App.Scripts.Features.Game.Level.Components;
using App.Scripts.Features.Game.Level.Events;
using App.Scripts.Features.Game.Moving.Aspects.Initializers;
using App.Scripts.Features.Game.Moving.Components;
using App.Scripts.Features.Game.Player.Components;
using App.Scripts.Infrastructure.WorldExtesions.Systems;
using Scellecs.Morpeh;

namespace App.Scripts.Features.Game.Level.Systems
{
    public class SpawnCardSystem : SystemBase
    {
        private Filter _requestFilter;

        public override void OnAwake()
        {
            _requestFilter = World.Filter.With<RequestSpawnCard>().Build();
        }

        public override void OnUpdate(float dt)
        {
            foreach (var entity in _requestFilter)
            {
                var request = entity.GetComponent<RequestSpawnCard>();

                var cardEntity = World.CreateEntity();
                cardEntity.SetTransformComponents(request.position);
                cardEntity.SetVelocityComponents(request.velocity);
                cardEntity.SetInertiaComponent(0.4f);
                cardEntity.SetComponent(request.card);
                cardEntity.SetComponent(new TagIgnoreGravity());

                if (request.isCurrentCard)
                {
                    cardEntity.SetComponent(new TagCurrentCard());
                }
                else
                {
                    cardEntity.SetComponent(new OnField());
                }

                World.RemoveEntity(entity);
            }
        }
    }
}