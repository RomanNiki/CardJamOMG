using App.Scripts.Features.Game.Configs;
using App.Scripts.Features.Game.Level.Components;
using App.Scripts.Features.Game.Moving.Aspects.Initializers;
using App.Scripts.Features.Game.Moving.Components;
using App.Scripts.Features.Game.Views;
using App.Scripts.Infrastructure.Factory;
using App.Scripts.Infrastructure.WorldExtesions.Systems;
using Scellecs.Morpeh;
using UnityEngine;

namespace App.Scripts.Features.Game.Level.Systems
{
    public class SpawnViewCardSystem : SystemBase
    {
        private readonly IFactory<ViewCard> _factory;
        private readonly CardConfig _cardConfig;
        private Filter _filter;
        private Stash<Card> _cardStash;
        private Stash<Position> _positionStash;
        private Filter _fieldStash;

        public SpawnViewCardSystem(IFactory<ViewCard> factory, CardConfig cardConfig)
        {
            _factory = factory;
            _cardConfig = cardConfig;
        }

        public override void OnAwake()
        {
            _filter = World.Filter.With<Card>().With<Position>().Without<TransformableView>().Build();
            _cardStash = World.GetStash<Card>();
            _fieldStash = World.Filter.With<Field>().Build();
            World.GetStash<TransformableView>();
            _positionStash = World.GetStash<Position>();
        }

        public override void OnUpdate(float dt)
        {
            Entity firstOrDefault = _fieldStash.FirstOrDefault();

            if (firstOrDefault.IsNullOrDisposed())
            {
                Debug.LogError("No field entity");
                return;
            }
            
            foreach (var entity in _filter)
            {
                var card = _cardStash.Get(entity);
                var position = _positionStash.Get(entity).Value;

                var view = _factory.Create();
                view.Move(position);
                view.SetNumber(card.number);
                view.SetSprite(_cardConfig.GetSprite(card.type));
                view.SetColor(_cardConfig.GetColor(card.type));
                
                entity.SetCollisionComponents(view.GetRect().size);

                entity.SetComponent(new TransformableView { Value = view });
            }
        }
    }
}