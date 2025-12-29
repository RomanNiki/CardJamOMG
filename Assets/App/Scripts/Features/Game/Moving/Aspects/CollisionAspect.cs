using App.Scripts.Features.Game.Moving.Components;
using Scellecs.Morpeh;

namespace App.Scripts.Features.Game.Moving.Aspects
{
    public struct CollisionAspect : IAspect, IFilterExtension
    {
        private Stash<Position> _position;
        private Stash<RectCollider> _collider;
        private Stash<Velocity> _velocity;
        
        public ref Position Position => ref _position.Get(Entity);
        public ref RectCollider Collider => ref _collider.Get(Entity);
        public ref Velocity Velocity => ref _velocity.Get(Entity);
        
        public Entity Entity { get; set; }
        
        public void OnGetAspectFactory(World world)
        {
            _position = world.GetStash<Position>();
            _collider = world.GetStash<RectCollider>();
            _velocity = world.GetStash<Velocity>();
        }
        
        public FilterBuilder Extend(FilterBuilder rootFilter)
        {
            return rootFilter.With<Position>().With<RectCollider>().With<Velocity>();
        }
    }
}
