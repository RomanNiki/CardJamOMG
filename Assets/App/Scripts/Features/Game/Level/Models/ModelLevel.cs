using System;
using System.Collections.Generic;

namespace App.Scripts.Features.Game.Level.Models
{
    [Serializable]
    public struct ModelLevel
    {
        public List<ModelCard> listCardsPos;
        public int countAdditional;
    }
}