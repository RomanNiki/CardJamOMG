using System;
using Scellecs.Morpeh;
using UnityEngine;

namespace App.Scripts.Features.Game.Moving.Components
{
    [Serializable]
    public struct RectCollider : IComponent
    {
        public Vector2 Size;
    }
}