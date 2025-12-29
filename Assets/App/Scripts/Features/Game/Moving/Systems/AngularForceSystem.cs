using App.Scripts.Features.Game.Moving.Components;
using App.Scripts.Infrastructure.WorldExtesions.Systems;
using Scellecs.Morpeh;

namespace App.Scripts.Features.Game.Moving.Systems
{
    public class AngularForceSystem : SystemBase
    {
        private Filter _filter;
        private Stash<AngularForceRequest> _angularForceStash;
        private Stash<AngularVelocity> _angularVelocityStash;

        public override void OnAwake()
        {
            _filter = World.Filter.With<AngularForceRequest>().Build();
            _angularForceStash = World.GetStash<AngularForceRequest>();
            _angularVelocityStash = World.GetStash<AngularVelocity>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var request = ref _angularForceStash.Get(entity);
                var targetEntity = request.Entity;
                
                if (targetEntity.IsNullOrDisposed())
                {
                    continue;
                }

                if (_angularVelocityStash.Has(targetEntity))
                {
                    ref var angularVelocity = ref _angularVelocityStash.Get(targetEntity);
                    angularVelocity.Value += request.Value;
                }
            }
        }
    }
}
