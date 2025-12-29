using App.Scripts.Features.Game.Input.Components;
using App.Scripts.Features.Game.Moving.Components;
using Scellecs.Morpeh;
using UnityEngine;

namespace App.Scripts.Features.Game.Moving.Aspects.Initializers
{
    public static class AspectsInitializer
    {
        public static void SetTransformComponents(this Entity entity, Vector2 position = new(),
            float rotation = 0f, float scale = 1f)
        {
            entity.SetComponent(new Position() { Value = position });
            entity.SetComponent(new Rotation() { Value = rotation });
            entity.SetComponent(new Scale() { Value = scale });
        }

        public static void SetVelocityComponents(this Entity entity, Vector2 velocity = new(), float angularVelocity = 0f,
            float scaleVelocity = 0f)
        {
            entity.SetComponent(new Velocity() { Value = velocity });
            entity.SetComponent(new AngularVelocity() { Value = angularVelocity });
            entity.SetComponent(new ScaleVelocity() { Value = scaleVelocity });
        }

        public static void SetInertiaComponent(this Entity entity, float friction = 1f)
        {
            entity.SetComponent(new Inertia() { Friction = friction });
        }

        public static void SetCollisionComponents(this Entity entity, Vector2 size)
        {
            entity.SetComponent(new RectCollider() { Size = size });
        }

        public static void SetBoundsComponents(this Entity entity, Rect bounds)
        {
            entity.SetComponent(new BoundsCollider() { Bounds = bounds });
        }
    }
}