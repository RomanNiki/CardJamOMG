using System.Collections.Generic;
using App.Scripts.Features.Game.Level.Models;

namespace App.Scripts.Features.Game.Level.Providers
{
    public interface IProviderLevels
    {
        List<ModelLevel> GetLevels();
    }
}