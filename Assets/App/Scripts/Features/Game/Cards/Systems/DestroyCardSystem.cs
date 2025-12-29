using App.Scripts.Features.Game.Cards.Requests;
using App.Scripts.Features.Game.Moving.Components;
using App.Scripts.Infrastructure.WorldExtesions.Systems;
using Scellecs.Morpeh;

namespace App.Scripts.Features.Game.Cards.Systems
{
    public class DestroyCardSystem : SystemBase
    {
        private Filter _filter;
        private Stash<RequestRemoveCard> _stash;

        public override void OnAwake()
        {
            _filter = World.Filter.With<RequestRemoveCard>().Build();
            _stash = World.GetStash<RequestRemoveCard>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var entityRemoveCard in _filter)
            {
                RequestRemoveCard requestRemoveCard = entityRemoveCard.GetComponent<RequestRemoveCard>();
                Entity entity = requestRemoveCard.card;
                if (entity.IsNullOrDisposed())
                {
                    continue;
                }
                TransformableView transformableView = entity.GetComponent<TransformableView>();
                transformableView.Dispose();
                World.RemoveEntity(entity);
                World.SendMassage(new EventDestroed());
            }

            _stash.RemoveAll();
        }
    }
}