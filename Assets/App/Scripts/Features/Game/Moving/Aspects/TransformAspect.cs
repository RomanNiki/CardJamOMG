using App.Scripts.Features.Game.Moving.Components;
using Scellecs.Morpeh;
using UnityEngine;

namespace App.Scripts.Features.Game.Moving.Aspects
{
    public struct TransformAspect : IAspect, IFilterExtension
    {
        private Stash<Position> _position;
        private Stash<Rotation> _rotation;
        private Stash<Scale> _scale;

        public ref Position Position => ref _position.Get(Entity);
        public ref Rotation Rotation => ref _rotation.Get(Entity);
        public ref Scale Scale => ref _scale.Get(Entity);

        public Entity Entity { get; set; }

        public void Rotate(float delta)
        {
            Rotation.Value += delta;
            Rotation.Value = Mathf.Repeat(Rotation.Value, 360f);
        }
        
        public void Translate(Vector2 translation)
        {
            Position.Value += translation;
        }
        
        public void SetSize(float scale)
        {
            Scale.Value = scale;
        }
        
        public void AddSize(float scale)
        {
            Scale.Value += scale;
        }
        
        public void OnGetAspectFactory(World world)
        {
            _position = world.GetStash<Position>();
            _rotation = world.GetStash<Rotation>();
            _scale = world.GetStash<Scale>();
        }

        public FilterBuilder Extend(FilterBuilder rootFilter)
        {
            return rootFilter.With<Position>().With<Rotation>().With<Scale>();
        }
    }
}