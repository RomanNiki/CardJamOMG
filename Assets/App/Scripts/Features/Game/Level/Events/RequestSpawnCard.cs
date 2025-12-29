using App.Scripts.Features.Game.Level.Components;
using Scellecs.Morpeh;
using UnityEngine;

namespace App.Scripts.Features.Game.Level.Events
{
    public struct RequestSpawnCard : IComponent
    {
        public Card card;
        public Vector2 position;
        public Vector2 velocity;
        public bool isCurrentCard;
    }
}
