using UnityEngine;

namespace App.Scripts.Infrastructure.Extensions
{
    public static class MinMaxExtensions
    {
        public static float GetRandomInRange(this MinMax<float> minMax)
        {
            return Random.Range(minMax.Min, minMax.Max);
        }
    }
}