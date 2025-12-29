using App.Scripts.Features.Game.Level.Components;
using App.Scripts.Features.Game.Level.Models;
using App.Scripts.Features.Game.Moving.Aspects.Initializers;
using App.Scripts.Features.Game.Moving.Components;
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

            foreach (var cardData in model.listCardsPos)
            {
                CreateCard(cardData);
            }
        }

        private void CreateCard(ModelCard cardData)
        {
            var entity = World.CreateEntity();
            
            var rect = _viewGrid.GetCellRect(cardData.position);
            
            entity.SetTransformComponents(rect.center);
            entity.SetVelocityComponents(cardData.moveDir);
            entity.SetCollisionComponents(rect.size);
            entity.SetComponent(new TagIgnoreGravity());
            entity.SetComponent(new OnField());
            
            entity.SetComponent(new Card
            {
                type = CardType.GetRandom(),
                number = GameConstants.GetRandomValue()
            });
        }
    }
}