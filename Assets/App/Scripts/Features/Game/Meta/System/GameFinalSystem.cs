using App.Scripts.Features.Game.Cards.Systems;
using App.Scripts.Features.Game.Level.Components;
using App.Scripts.Features.Game.Player.Components;
using App.Scripts.Infrastructure.WorldExtesions.Systems;
using Scellecs.Morpeh;

namespace App.Scripts.Features.Game.Meta.System
{
    public class GameFinalSystem : SystemBase
    {
        private Filter _filterEventMathced;
        private Filter _filterOnField;
        private Filter _filterCurrentCard;
        private Filter _inventory;

        public override void OnAwake()
        {
            _filterEventMathced = World.Filter.With<EventDestroed>().Build();
            _filterOnField = World.Filter.With<OnField>().Build();
            _filterCurrentCard = World.Filter.With<TagCurrentCard>().Build();
            _inventory = World.Filter.With<Inventory>().Build();
        }

        public override void OnUpdate(float deltaTime)
        {
            if (_filterEventMathced.IsEmpty())
            {
                return;
            }
            int cardsCount = _inventory.First().GetComponent<Inventory>().cards.Count;
           
            if (!_filterCurrentCard.IsEmpty() || cardsCount != 0)
            {
                return;
            }

            World.SendMassage(new EndGame(_filterOnField.IsEmpty()));
        }
    }

    public struct EndGame : IComponent
    {
        public bool win;

        public EndGame(bool b)
        {
            win = b;
        }
    }
}