using App.Scripts.Features.Game.Moving.Components;
using App.Scripts.Infrastructure.WorldExtesions.Systems;
using Scellecs.Morpeh;

namespace App.Scripts.Features.Game.Moving.Systems
{
    public class ForceSystem : FixedSystemBase
    {
        private Filter _filter;
        private Stash<ForceRequest> _forceStash;
        private Stash<Velocity> _velocityStash;

        public override void OnAwake()
        {
            _filter = World.Filter.With<ForceRequest>().Build();
            _forceStash = World.GetStash<ForceRequest>();
            _velocityStash = World.GetStash<Velocity>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var forceRequest = ref _forceStash.Get(entity);
                ref var forceEntity = ref forceRequest.Entity;
                
                if (forceEntity.IsNullOrDisposed())
                {
                    continue;
                }

                ref var velocity = ref _velocityStash.Get(forceEntity);
                velocity.Value += forceRequest.Value * deltaTime;
            }

            _forceStash.RemoveAll();
        }
    }
}