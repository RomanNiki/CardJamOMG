using App.Scripts.Features.Game.Moving.Aspects;
using App.Scripts.Features.Game.Moving.Components;
using App.Scripts.Infrastructure.WorldExtesions.Systems;
using Scellecs.Morpeh;

namespace App.Scripts.Features.Game.Moving.Systems
{
    public class ExecuteTransformSystem : FixedSystemBase
    {
        private Filter _moveableFilter;
        private AspectFactory<TransformAspect> _transformAspect;
        private Stash<TransformableView> _transformableStash;

        public override void OnAwake()
        {
            _moveableFilter = World.Filter.Extend<TransformAspect>().With<TransformableView>().Build();
            _transformAspect = World.GetAspectFactory<TransformAspect>();
            _transformableStash = World.GetStash<TransformableView>();
        }

        public override void OnUpdate(float dt)
        {
            foreach (var entity in _moveableFilter)
            {
                ref var view = ref _transformableStash.Get(entity).Value;
                var transform = _transformAspect.Get(entity);
                view.Move(transform.Position.Value);
                view.Rotate(transform.Rotation.Value);
                view.SetSize(transform.Scale.Value);
            }
        }
    }
}