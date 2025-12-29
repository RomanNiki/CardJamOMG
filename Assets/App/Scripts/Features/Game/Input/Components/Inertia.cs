using System;
using Scellecs.Morpeh;

namespace App.Scripts.Features.Game.Input.Components
{
    [Serializable]
    public struct Inertia : IComponent
    {
        public float Friction; // Коэффициент замедления (0..1, где 1 - мгновенная остановка, 0 - отсутствие трения)
    }
}
