using System;
using Scellecs.Morpeh;
using UnityEngine;

namespace App.Scripts.Features.Game.Moving.Components
{
    [Serializable]
    public struct ForceRequest : IComponent
    {
        public Entity Entity;
        public Vector2 Value;
    }
}