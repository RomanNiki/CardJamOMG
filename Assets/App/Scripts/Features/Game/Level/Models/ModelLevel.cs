using System;
using System.Collections.Generic;
using Scellecs.Morpeh;

namespace App.Scripts.Features.Game.Level.Models
{
    [Serializable]
    public struct ModelLevel : IComponent
    {
        public List<ModelCard> listCardsPos;
        public int countAdditional;
    }
}