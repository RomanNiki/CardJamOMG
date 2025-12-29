using System;
using UnityEngine;

namespace App.Scripts.Features.Game.Level.Models
{
    [Serializable]
    public struct ModelCard
    {
        public Vector2Int position;
        public Vector2 moveDir;
    }
}