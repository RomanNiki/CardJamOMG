using App.Scripts.Features.Game.Configs;
using App.Scripts.Features.Game.Level.Components;
using App.Scripts.Features.Game.Moving.Components;
using App.Scripts.Features.Game.Views;
using App.Scripts.Infrastructure.WorldExtesions.Systems;
using Scellecs.Morpeh;
using UnityEngine;
using VContainer;

namespace App.Scripts.Features.Game.Level.Systems
{
    public class SpawnViewCardSystem : SystemBase
    {
        [Inject] private CardConfig _config;
        
        private Filter _filter;
        private Stash<Card> _cardStash;
        private Stash<TransformableView> _viewStash;
        private Stash<Position> _positionStash;
        private Filter _fieldStash;

        public override void OnAwake()
        {
            _filter = World.Filter.With<Card>().With<Position>().Without<TransformableView>().Build();
            _cardStash = World.GetStash<Card>();
            _fieldStash = World.Filter.With<Field>().Build();
            _viewStash = World.GetStash<TransformableView>();
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

            Field field = firstOrDefault.GetComponent<Field>();
            ViewGrid viewGrid = field.ViewGrid;

            foreach (var entity in _filter)
            {
                var card = _cardStash.Get(entity);
                var position = _positionStash.Get(entity).Value;
                
                var view = Object.Instantiate(_config.prefab, viewGrid.root);
                view.Move(position);
                view.SetNumber(card.number);
                view.SetColor(_config.GetColor(card.type));
                
                entity.SetComponent(new TransformableView { Value = view });
            }
        }
    }
}
