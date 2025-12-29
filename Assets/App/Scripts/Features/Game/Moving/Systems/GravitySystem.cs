using App.Scripts.Features.Game.Moving.Components;
using App.Scripts.Infrastructure.WorldExtesions.Systems;
using Scellecs.Morpeh;
using UnityEngine;

namespace App.Scripts.Features.Game.Moving.Systems
{
    public class GravitySystem : SystemBase
    {
        private Filter _velocityFilter;
        private const float Gravity = -9.8f;

        public override void OnAwake()
        {
            _velocityFilter = World.Filter.With<Velocity>().Without<TagIgnoreGravity>().Build();
        }

        public override void OnUpdate(float dt)
        {
            foreach (var entity in _velocityFilter)
            {
                World.SendMassage(new ForceRequest()
                {
                    Entity = entity,
                    Value = Vector2.up * Gravity
                });
            }
        }
    }
}