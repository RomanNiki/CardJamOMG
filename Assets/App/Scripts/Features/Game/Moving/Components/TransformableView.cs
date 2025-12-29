using System;
using App.Scripts.Infrastructure.BaseView;
using Scellecs.Morpeh;

namespace App.Scripts.Features.Game.Moving.Components
{
    [Serializable]
    public struct TransformableView : IComponent, IDisposable
    {
        public MonoView Value;

        public void Dispose()
        {
            if (Value)
            {
                Value.Remove();
            }
        }
    }
}