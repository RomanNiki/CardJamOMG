using Scellecs.Morpeh;
using UnityEngine;

namespace App.Scripts.Features.Game.Input.Components
{
    public struct SwipeData : IComponent
    {
        public Vector2 StartPosition;
        public Vector2 LastPosition;
        public float StartTime;
        public float AccumulatedTwist;
        public Vector2 Offset;
    }
}
