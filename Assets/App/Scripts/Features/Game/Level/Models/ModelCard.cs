using System;
using Scellecs.Morpeh;
using UnityEngine;

namespace App.Scripts.Features.Game.Level.Models
{
    [Serializable]
    public struct ModelCard : IComponent
    {
        public Vector2Int position;
        public Vector2 moveDir;
    }
}