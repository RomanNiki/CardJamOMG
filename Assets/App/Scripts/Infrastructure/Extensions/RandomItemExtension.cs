using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace App.Scripts.Infrastructure.Extensions
{
    public static class RandomItemExtension
    {
        public static T GetRandomItemBySpawnChance<T>(this ICollection<T> list, Func<T, float> item)
        {
            var sum = list.Sum(item);

            var randomPoint = Random.Range(0, 1f) * sum;

            foreach (var arg in list)
            {
                var prob = item(arg);
                if (randomPoint < prob)
                {
                    return arg;
                }

                randomPoint -= prob;
            }

            return list.Last();
        }
    }
}