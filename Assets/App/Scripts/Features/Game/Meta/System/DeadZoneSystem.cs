using App.Scripts.Features.Game.Cards.Requests;
using App.Scripts.Features.Game.Moving.Components;
using App.Scripts.Infrastructure.WorldExtesions.Systems;
using Scellecs.Morpeh;

namespace App.Scripts.Features.Game.Meta.System
{
    public class DeadZoneSystem : SystemBase
    {
        private Filter _filter;

        public override void OnAwake()
        {
            _filter = World.Filter.With<Position>().Build();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                if (entity.GetComponent<Position>().Value.y <= -100)
                {
                    World.SendMassage(new RequestRemoveCard(entity));
                }
            }
        }
    }
}