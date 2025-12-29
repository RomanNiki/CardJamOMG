using System.Collections.Generic;
using App.Scripts.Infrastructure.Blueprints.Core.Containers.Components;
using Scellecs.Morpeh;

namespace App.Scripts.Infrastructure.Blueprints.Core.Containers.Entities
{
    public interface IContainerBlueprintEntity
    {
        void BuildEntity(World world);
        void SetComponent<T>(T component) where T : struct, IComponent;
        bool HasComponent<T>() where T : struct, IComponent;
        T GetComponent<T>() where T : struct, IComponent;
        IEnumerable<IContainerBlueprintComponent> GetComponents();
    }
}