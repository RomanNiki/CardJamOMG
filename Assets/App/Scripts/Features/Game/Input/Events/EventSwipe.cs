using Scellecs.Morpeh;
using UnityEngine;

namespace App.Scripts.Features.Game.Input.Events
{
    public struct EventSwipe : IComponent
    {
        public readonly Vector2 SwipeVelocity;
        public readonly float SwipeAngularVelocity;

        public EventSwipe(Vector2 swipeVelocity, float swipeAngularVelocity)
        {
            SwipeVelocity = swipeVelocity;
            SwipeAngularVelocity = swipeAngularVelocity;
        }
    }
}