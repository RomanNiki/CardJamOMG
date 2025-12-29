using System;
using System.Collections.Generic;
using App.Scripts.Infrastructure.Blueprints.Core.Containers.Components;
using Newtonsoft.Json;
using Scellecs.Morpeh;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace App.Scripts.Infrastructure.Blueprints.Core.Containers.Entities
{
    [Serializable]
    [InlineProperty]
    public class ContainerBlueprintEntity : IContainerBlueprintEntity
    {
        [OdinSerialize]
        [JsonProperty("c", ItemTypeNameHandling = TypeNameHandling.All)]
        [ListDrawerSettings(ShowFoldout = false, ShowPaging = false)]
        private List<IContainerBlueprintComponent> components = new();

        public ContainerBlueprintEntity(IEnumerable<IContainerBlueprintComponent> components)
        {
            this.components.AddRange(components);
        }

        public ContainerBlueprintEntity()
        {
        }

        public void BuildEntity(World world)
        {
            Entity entity = world.CreateEntity();

            foreach (IContainerBlueprintComponent containerComponents in components)
            {
                containerComponents.ApplyToEntity(entity);
            }
        }

        public void SetComponent<T>(T component) where T : struct, IComponent
        {
            var container = new ContainerBlueprintComponent<T>();
            container.SetComponent(component);
            components.Add(container);
        }

        public T GetComponent<T>() where T : struct, IComponent
        {
            for (var i = 0; i < components.Count; i++)
            {
                IContainerBlueprintComponent container = components[i];

                if (container is ContainerBlueprintComponent<T> containerT)
                {
                    return containerT.GetComponent();
                }
            }

            return default;
        }

        public IEnumerable<IContainerBlueprintComponent> GetComponents()
        {
            return components;
        }

        public bool HasComponent<T>() where T : struct, IComponent
        {
            for (var i = 0; i < components.Count; i++)
            {
                IContainerBlueprintComponent container = components[i];

                if (container is ContainerBlueprintComponent<T>)
                {
                    return true;
                }
            }

            return false;
        }
    }
}