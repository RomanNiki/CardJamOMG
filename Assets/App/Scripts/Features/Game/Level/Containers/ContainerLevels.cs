using System;
using System.Collections.Generic;
using App.Scripts.Features.Game.Level.Models;

namespace App.Scripts.Features.Game.Level.Containers
{
    [Serializable]
    public struct ContainerLevels
    {
        public List<ModelLevel> levels;
    }
}