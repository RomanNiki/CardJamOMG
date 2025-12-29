using App.Scripts.Features.Game.Level.Components;
using App.Scripts.Features.Game.Level.Events;
using App.Scripts.Features.Game.Player.Components;
using App.Scripts.Features.Game.Views;
using App.Scripts.Infrastructure.Extensions;
using App.Scripts.Infrastructure.WorldExtesions.Systems;
using Scellecs.Morpeh;
using UnityEngine;
using VContainer;

namespace App.Scripts.Features.Game.Player.Systems
{
    public class SelectCurrentCardSystem : SystemBase
    {
        [Inject] private ViewInputZone _viewInputZone;
        
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

            var requestEntity = World.CreateEntity();
            var center = _viewInputZone.rectTransform.GetWorldRect().center;
            
            requestEntity.SetComponent(new RequestSpawnCard
            {
                card = cardData,
                position = center,
                velocity = Vector2.zero,
                size = new Vector2(100, 150),
                isCurrentCard = true
            });
        }
    }
}
