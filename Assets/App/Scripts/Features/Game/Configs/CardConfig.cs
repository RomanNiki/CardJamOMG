using System;
using System.Collections.Generic;
using App.Scripts.Features.Game.Views;
using UnityEngine;

namespace App.Scripts.Features.Game.Configs
{
    [CreateAssetMenu(fileName = "CardConfig", menuName = "Configs/CardConfig")]
    public class CardConfig : ScriptableObject
    {
        public ViewCard prefab;
        public List<CardSpriteData> sprites;

        public Color GetColor(string cardType)
        {
            return sprites.Find(s => s.type == cardType).sprite;
        }
    }

    [Serializable]
    public struct CardSpriteData
    {
        public string type;
        public Color sprite;
    }
}
