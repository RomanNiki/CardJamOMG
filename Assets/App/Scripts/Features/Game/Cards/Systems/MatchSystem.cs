using App.Scripts.Features.Game.Cards.Requests;
using App.Scripts.Features.Game.Level.Components;
using App.Scripts.Features.Game.Moving.Events;
using App.Scripts.Features.Game.Player.Components;
using App.Scripts.Infrastructure.WorldExtesions.Systems;
using Scellecs.Morpeh;

namespace App.Scripts.Features.Game.Cards.Systems
{
    public class MatchSystem : SystemBase
    {
        private Filter _eventFilter;
        private Stash<Card> _cardStash;
        private Stash<TagThrownCard> _thrownStash;
        private Stash<OnField> _onFieldStash;

        public override void OnAwake()
        {
            _eventFilter = World.Filter.With<EventCollided>().Build();
            _cardStash = World.GetStash<Card>();
            _thrownStash = World.GetStash<TagThrownCard>();
            _onFieldStash = World.GetStash<OnField>();
        }

        public override void OnUpdate(float dt)
        {
            foreach (var eventEntity in _eventFilter)
            {
                var evt = eventEntity.GetComponent<EventCollided>();
                ProcessCollision(evt.FirstEntity, evt.SecondEntity);
            }
        }

        private void ProcessCollision(Entity a, Entity b)
        {
            if (!a.IsNullOrDisposed() && !b.IsNullOrDisposed())
            {
                bool aThrown = _thrownStash.Has(a);
                bool bOnField = _onFieldStash.Has(b);

                if (aThrown && bOnField)
                {
                    HandleMatch(a, b);
                    return;
                }

                bool bThrown = _thrownStash.Has(b);
                bool aOnField = _onFieldStash.Has(a);

                if (bThrown && aOnField)
                {
                    HandleMatch(b, a);
                    return;
                }
            }
        }

        private void HandleMatch(Entity thrown, Entity field)
        {
            if (!_cardStash.Has(thrown) || !_cardStash.Has(field))
            {
                World.SendMassage(new RequestRemoveCard(thrown));
                return;
            }

            var thrownCard = _cardStash.Get(thrown);
            var fieldCard = _cardStash.Get(field);

            if (thrownCard.type == fieldCard.type || thrownCard.number == fieldCard.number)
            {
                World.SendMassage(new RequestRemoveCard(thrown));
                World.SendMassage(new RequestRemoveCard(field));
            }
            else
            {
                World.SendMassage(new RequestRemoveCard(thrown));
            }
        }
    }
}
