using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Infrastructure.Extensions
{
    public static class ListExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            var count = list.Count;
            while (count > 1)
            {
                count--;
                var k = Random.Range(0, count + 1);
                (list[k], list[count]) = (list[count], list[k]);
            }
        }
    }
}
