using App.Scripts.Features.Game.Input.Components;
using App.Scripts.Features.Game.Moving.Components;
using App.Scripts.Features.Game.Player.Components;
using App.Scripts.Infrastructure.WorldExtesions.Systems;
using Scellecs.Morpeh;
using UnityEngine;

namespace App.Scripts.Features.Game.Input.Systems
{
    public class SwipeStartSystem : SystemBase
    {
        private Stash<SwipeData> _swipeStash;
        private Filter _currentCardFilter;
        private Stash<Position> _positionStash;

        public override void OnAwake()
        {
            _swipeStash = World.GetStash<SwipeData>();
            _currentCardFilter = World.Filter.With<TagCurrentCard>().With<Position>().Build();
            _positionStash = World.GetStash<Position>();
        }

        public override void OnUpdate(float dt)
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                var entity = World.CreateEntity();
                entity.AddComponent<SwipeData>();
                ref var swipeData = ref _swipeStash.Get(entity);
                swipeData.StartPosition = UnityEngine.Input.mousePosition;
                swipeData.LastPosition = swipeData.StartPosition;
                swipeData.StartTime = Time.time;
                swipeData.AccumulatedTwist = 0f;
                swipeData.Offset = Vector2.zero;

                var currentCardEntity = _currentCardFilter.First();
                if (currentCardEntity != null)
                {
                    var cardPos = _positionStash.Get(currentCardEntity).Value;
                    swipeData.Offset = swipeData.StartPosition - cardPos;
                }
            }
        }
    }
}
