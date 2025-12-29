using System.Collections.Generic;
using App.Scripts.Features.Game.Level.Containers;
using App.Scripts.Features.Game.Level.Models;
using Newtonsoft.Json;
using UnityEngine;

namespace App.Scripts.Features.Game.Level.Providers
{
    public class ProviderLevels : IProviderLevels
    {
        public const string path = "Levels/ConfigLevels";

        public List<ModelLevel> GetLevels()
        {
            string text = Resources.Load<TextAsset>(path).text;

            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<ContainerLevels>(text).levels;
        }
    }
}