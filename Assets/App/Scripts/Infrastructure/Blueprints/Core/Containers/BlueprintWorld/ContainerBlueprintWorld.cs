using System;
using System.Collections.Generic;
using App.Scripts.Infrastructure.Blueprints.Core.Containers.Entities;
using Newtonsoft.Json;
using Scellecs.Morpeh;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace App.Scripts.Infrastructure.Blueprints.Core.Containers.BlueprintWorld
{
    [Serializable]
    public class ContainerBlueprintWorld : IContainerBlueprintWorld
    {
        [OdinSerialize]
        [JsonProperty("e", ItemTypeNameHandling = TypeNameHandling.Objects)]
        [ListDrawerSettings(ShowFoldout = false, ShowPaging = false)]
        private List<IContainerBlueprintEntity> _entities = new();

        public ContainerBlueprintWorld()
        {
        }

        public ContainerBlueprintWorld(IContainerBlueprintWorld containerBlueprintWorld)
        {
            foreach (var containerBlueprintEntity in containerBlueprintWorld.GetEntities())
            {
                _entities.Add(new ContainerBlueprintEntity(containerBlueprintEntity.GetComponents()));
            }
        }

        public IReadOnlyList<IContainerBlueprintEntity> GetEntities()
        {
            return _entities;
        }

        public void BuildWorld(World world)
        {
            foreach (var containerEntity in _entities)
            {
                containerEntity.BuildEntity(world);
            }
        }

        public void AddEntity(IContainerBlueprintEntity containerBlueprintEntity)
        {
            _entities.Add(containerBlueprintEntity);
        }

        public IContainerBlueprintEntity FindEntityWith<T>() where T : struct, IComponent
        {
            for (var i = 0; i < _entities.Count; i++)
            {
                var containerBlueprintEntity = _entities[i];

                if (containerBlueprintEntity.HasComponent<T>())
                {
                    return containerBlueprintEntity;
                }
            }

            return default;
        }
    }
}