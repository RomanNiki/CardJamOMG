using System.Collections.Generic;
using App.Scripts.Infrastructure.Blueprints.Core.Containers.Entities;
using Scellecs.Morpeh;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace App.Scripts.Infrastructure.Blueprints.Core.Containers.BlueprintWorld
{
    [CreateAssetMenu(menuName = "Infrastructure/EntityBlueprint/World", fileName = "World")]
    public class ContainerBlueprintWorldScriptable : SerializedScriptableObject, IContainerBlueprintWorld
    {
        [OdinSerialize] private List<IContainerBlueprintEntity> _entities = new();

        public IReadOnlyList<IContainerBlueprintEntity> GetEntities()
        {
            return _entities;
        }

        public void BuildWorld(World world)
        {
            foreach (IContainerBlueprintEntity containerEntity in _entities)
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
                IContainerBlueprintEntity containerBlueprintEntity = _entities[i];

                if (containerBlueprintEntity.HasComponent<T>())
                {
                    return containerBlueprintEntity;
                }
            }

            return default;
        }
    }
}