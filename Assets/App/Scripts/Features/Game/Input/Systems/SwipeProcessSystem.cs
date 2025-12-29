using App.Scripts.Features.Game.Input.Components;
using App.Scripts.Infrastructure.WorldExtesions.Systems;
using Scellecs.Morpeh;
using UnityEngine;

namespace App.Scripts.Features.Game.Input.Systems
{
    public class SwipeProcessSystem : SystemBase
    {
        private Filter _swipeFilter;
        private Stash<SwipeData> _swipeStash;

        public override void OnAwake()
        {
            _swipeFilter = World.Filter.With<SwipeData>().Build();
            _swipeStash = World.GetStash<SwipeData>();
        }

        public override void OnUpdate(float dt)
        {
            foreach (var entity in _swipeFilter)
            {
                ref var swipeData = ref _swipeStash.Get(entity);
                Vector2 currentPosition = UnityEngine.Input.mousePosition;

                if (UnityEngine.Input.GetMouseButton(0))
                {
                    Vector2 toLast = swipeData.LastPosition - swipeData.StartPosition;
                    Vector2 toCurrent = currentPosition - swipeData.StartPosition;

                    if (toLast.sqrMagnitude > 100f && toCurrent.sqrMagnitude > 100f) // Минимальный порог в 10 пикселей
                    {
                        float angle = Vector2.SignedAngle(toLast, toCurrent);
                        swipeData.AccumulatedTwist += angle;
                    }
                    swipeData.LastPosition = currentPosition;
                }
            }
        }
    }
}
