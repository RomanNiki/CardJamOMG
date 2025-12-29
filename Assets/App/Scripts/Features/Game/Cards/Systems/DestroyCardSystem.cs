using App.Scripts.Features.Game.Cards.Requests;
using App.Scripts.Infrastructure.WorldExtesions.Systems;
using Scellecs.Morpeh;

namespace App.Scripts.Features.Game.Cards.Systems
{
    public class DestroyCardSystem : SystemBase
    {
        private Filter _filter;

        public override void OnAwake()
        {
            _filter = World.Filter.With<RequestRemoveCard>().Build();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var entityRemoveCard in _filter)
            {
                RequestRemoveCard requestRemoveCard = entityRemoveCard.GetComponent<RequestRemoveCard>();
                Entity entity = requestRemoveCard.card;
                World.RemoveEntity(entity);
            }
        }
    }
}