using App.Scripts.Features.Game.Configs;
using App.Scripts.Features.Game.Level.Components;
using App.Scripts.Features.Game.Level.Events;
using App.Scripts.Features.Game.Player.Components;
using App.Scripts.Features.Game.Views;
using App.Scripts.Infrastructure.Extensions;
using App.Scripts.Infrastructure.Factory;
using App.Scripts.Infrastructure.WorldExtesions.Systems;
using Scellecs.Morpeh;
using UnityEngine;
using VContainer;

namespace App.Scripts.Features.Game.Player.Systems
{
    public class SelectCurrentCardSystem : SystemBase
    {
        [Inject] private ViewInputZone _viewInputZone;
        [Inject]  private CardConfig _cardConfig;
        
        private Filter _currentCardFilter;
        private Filter _inventoryFilter;
        private Filter _field;

        public override void OnAwake()
        {
            _currentCardFilter = World.Filter.With<TagCurrentCard>().Build();
            _inventoryFilter = World.Filter.With<Inventory>().Build();
            _field = World.Filter.With<Field>().Build();
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
            Rect worldRect = _viewInputZone.rectTransform.GetWorldRect();
            var center = worldRect.center;

            Entity entity = _field.First();
            ViewGrid viewGrid = entity.GetComponent<Field>().ViewGrid;

            if (inventory.cards.Count > 0)
            {
                var nextCard = inventory.cards[0];
                ViewCard viewGridNext = viewGrid.next;
                viewGridNext.Show();
                viewGridNext.SetNumber(nextCard.number);
                viewGridNext.SetSprite(_cardConfig.GetSprite(nextCard.type));
                viewGridNext.SetColor(_cardConfig.GetColor(nextCard.type));
            }
            else
            {
                ViewCard viewGridNext = viewGrid.next;
                viewGridNext.Hide();
            }
            
            requestEntity.SetComponent(new RequestSpawnCard
            {
                card = cardData,
                position = center,
                velocity = Vector2.zero,
                isCurrentCard = true
            });
        }
    }
}
