using System.Collections.Generic;
using App.Scripts.Features.Game.Level.Components;
using Scellecs.Morpeh;

namespace App.Scripts.Features.Game.Player.Components
{
    public struct Inventory : IComponent
    {
        public List<Card> cards;
    }
}