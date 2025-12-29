using System;
using Newtonsoft.Json;
using Scellecs.Morpeh;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace App.Scripts.Infrastructure.Blueprints.Core.Containers.Components
{
    [Serializable]
    [InlineProperty]
    public class ContainerBlueprintComponent<T> : IContainerBlueprintComponent where T : struct, IComponent
    {
        [OdinSerialize]
        [HideLabel]
        [InlineProperty]
        [JsonProperty("c", TypeNameHandling = TypeNameHandling.All)]
        private T _component;

        public void ApplyToEntity(Entity entity)
        {
            entity.SetComponent(_component);
        }

        public void SetComponent(T component)
        {
            _component = component;
        }

        public T GetComponent()
        {
            return _component;
        }
    }
}