using App.Scripts.Features.Game.Input.Components;
using App.Scripts.Infrastructure.WorldExtesions.Systems;
using Scellecs.Morpeh;
using UnityEngine;

namespace App.Scripts.Features.Game.Input.Systems
{
    public class SwipeStartSystem : SystemBase
    {
        private Stash<SwipeData> _swipeStash;

        public override void OnAwake()
        {
            _swipeStash = World.GetStash<SwipeData>();
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
            }
        }
    }
}
