using App.Scripts.Features.Game.Level.Components;
using App.Scripts.Features.Game.Player.Components;
using App.Scripts.Infrastructure.WorldExtesions.Systems;
using Scellecs.Morpeh;

namespace App.Scripts.Features.Game.Player.Systems
{
    public class SelectCurrentCardSystem : SystemBase
    {
        private Filter _currentCardFilter;
        private Filter _inventoryFilter;

        public override void OnAwake()
        {
            _currentCardFilter = World.Filter.With<TagCurrentCard>().Build();
            _inventoryFilter = World.Filter.With<Inventory>().Build();
        }

        public override void OnUpdate(float dt)
        {
            if (!_currentCardFilter.IsEmpty())
            {
                return;
            }

            var inventoryEntity = _inventoryFilter.First();
            if (inventoryEntity == null)
            {
                return;
            }

            ref var inventory = ref inventoryEntity.GetComponent<Inventory>();
            if (inventory.cards == null || inventory.cards.Count == 0)
            {
                return;
            }

            var cardData = inventory.cards[0];
            inventory.cards.RemoveAt(0);

            var currentCardEntity = World.CreateEntity();
            currentCardEntity.SetComponent(cardData);
            currentCardEntity.SetComponent(new TagCurrentCard());
        }
    }
}
