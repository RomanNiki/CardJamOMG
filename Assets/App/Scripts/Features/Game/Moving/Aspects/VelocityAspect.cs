using App.Scripts.Features.Game.Moving.Components;
using Scellecs.Morpeh;

namespace App.Scripts.Features.Game.Moving.Aspects
{
    public struct VelocityAspect : IAspect, IFilterExtension
    {
        private Stash<Velocity> _velocity;
        private Stash<AngularVelocity> _angularVelocity;
        private Stash<ScaleVelocity> _scaleVelocity;
        
        public ref Velocity Velocity => ref _velocity.Get(Entity);
        public ref AngularVelocity AngularVelocity => ref _angularVelocity.Get(Entity);
        public ref ScaleVelocity ScaleVelocity => ref _scaleVelocity.Get(Entity);
        
        public Entity Entity { get; set; }
        
        public void OnGetAspectFactory(World world)
        {
            _velocity = world.GetStash<Velocity>();
            _angularVelocity = world.GetStash<AngularVelocity>();
            _scaleVelocity = world.GetStash<ScaleVelocity>();
        }
        
        public FilterBuilder Extend(FilterBuilder rootFilter)
        {
            return rootFilter.With<Velocity>().With<AngularVelocity>().With<ScaleVelocity>();
        }
    }
}