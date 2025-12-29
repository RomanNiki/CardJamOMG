using App.Scripts.Features.Game.Constants;
using App.Scripts.Features.Game.Level.Components;
using App.Scripts.Features.Game.Level.Events;
using App.Scripts.Features.Game.Level.Models;
using App.Scripts.Features.Game.Views;
using App.Scripts.Infrastructure.WorldExtesions.Systems;
using Scellecs.Morpeh;
using VContainer;

namespace App.Scripts.Features.Game.Level.Systems
{
    public class InitializableLevel : InitializerBase
    {
        [Inject] private ViewGrid _viewGrid;

        public override void OnAwake()
        {
            var filter = World.Filter.With<ModelLevel>().Build();
            var modelLevelEntity = filter.First();
            if (modelLevelEntity == null) return;

            var model = modelLevelEntity.GetComponent<ModelLevel>();

            Entity entity = World.CreateEntity();
            entity.SetComponent(new Field(_viewGrid));

            foreach (var cardData in model.listCardsPos)
            {
                CreateCard(cardData);
            }
        }

        private void CreateCard(ModelCard cardData)
        {
            var requestEntity = World.CreateEntity();
            var rect = _viewGrid.GetCellRect(cardData.position);
            
            requestEntity.SetComponent(new RequestSpawnCard
            {
                card = new Card
                {
                    type = CardType.GetRandom(),
                    number = GameConstants.GetRandomValue()
                },
                position = rect.position,
                velocity = cardData.moveDir,
                size = rect.size,
                isCurrentCard = false
            });
        }
    }
}