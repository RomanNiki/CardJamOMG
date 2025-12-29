using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Features.Game.Level.Models
{
    [Serializable]
    public struct ModelLevel
    {
        public Vector2Int size;
        public ModelCard[,] Field;
        public List<string> cardTypes;
        public int cardsCount;
        public int countQuests;
    }
}