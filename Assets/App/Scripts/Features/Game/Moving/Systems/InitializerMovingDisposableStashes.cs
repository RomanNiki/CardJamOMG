using App.Scripts.Features.Game.Moving.Components;
using App.Scripts.Infrastructure.WorldExtesions.Systems;
using Scellecs.Morpeh;

namespace App.Scripts.Features.Game.Moving.Systems
{
    public class InitializerMovingDisposableStashes : InitializerBase
    {
        public override void OnAwake()
        {
            World.GetStash<TransformableView>().AsDisposable();
        }
    }
}