using System.Collections.Generic;
using App.Scripts.Infrastructure.Blueprints.Core.Containers.Components;
using Scellecs.Morpeh;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace App.Scripts.Infrastructure.Blueprints.Core.Containers.Entities
{
    [CreateAssetMenu(menuName = "Infrastructure/EntityBlueprint/Entity", fileName = "Entity")]
    public class ContainerBlueprintEntityScriptable : SerializedScriptableObject, IContainerBlueprintEntity
    {
        [OdinSerialize] private List<IContainerBlueprintComponent> _containers = new();

        public void BuildEntity(World world)
        {
            Entity entity = world.CreateEntity();

            foreach (IContainerBlueprintComponent containerComponents in _containers)
            {
                containerComponents.ApplyToEntity(entity);
            }
        }

        public void SetComponent<T>(T component) where T : struct, IComponent
        {
            var container = new ContainerBlueprintComponent<T>();
            container.SetComponent(component);
            _containers.Add(container);
        }

        public bool HasComponent<T>() where T : struct, IComponent
        {
            for (var i = 0; i < _containers.Count; i++)
            {
                IContainerBlueprintComponent container = _containers[i];

                if (container is ContainerBlueprintComponent<T>)
                {
                    return true;
                }
            }

            return false;
        }

        public T GetComponent<T>() where T : struct, IComponent
        {
            for (var i = 0; i < _containers.Count; i++)
            {
                IContainerBlueprintComponent container = _containers[i];

                if (container is ContainerBlueprintComponent<T> containerT)
                {
                    return containerT.GetComponent();
                }
            }

            return default;
        }

        public IEnumerable<IContainerBlueprintComponent> GetComponents()
        {
            return _containers;
        }
    }
}