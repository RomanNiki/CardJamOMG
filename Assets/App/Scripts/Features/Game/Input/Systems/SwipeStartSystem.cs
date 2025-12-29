using App.Scripts.Features.Game.Input.Components;
using App.Scripts.Features.Game.Views;
using App.Scripts.Infrastructure.WorldExtesions.Systems;
using Scellecs.Morpeh;
using UnityEngine;

namespace App.Scripts.Features.Game.Input.Systems
{
    public class SwipeStartSystem : SystemBase
    {
        private readonly ViewInputZone _inputZone;
        private Stash<SwipeData> _swipeStash;

        public SwipeStartSystem(ViewInputZone inputZone)
        {
            _inputZone = inputZone;
        }

        public override void OnAwake()
        {
            _swipeStash = World.GetStash<SwipeData>();
        }

        public override void OnUpdate(float dt)
        {
            if (!_inputZone.rectTransform.rect.Contains(UnityEngine.Input.mousePosition))
            {
                return;
            }

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
