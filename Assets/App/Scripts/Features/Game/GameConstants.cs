using System.Linq;
using UnityEngine;

namespace App.Scripts.Features.Game
{
    public static class CardType
    {
        public const string Red = "red";
        public const string Green = "green";
        public const string Blue = "blue";
        public const string Yellow = "yellow";

        public static readonly string[] All = { Red, Green, Blue, Yellow };

        public static string GetRandom()
        {
            return All[Random.Range(0, All.Length)];
        }
    }

    public static class GameConstants
    {
        public const int MinCardValue = 1;
        public const int MaxCardValue = 10;

        public static int GetRandomValue()
        {
            return Random.Range(MinCardValue, MaxCardValue + 1);
        }
    }
}
