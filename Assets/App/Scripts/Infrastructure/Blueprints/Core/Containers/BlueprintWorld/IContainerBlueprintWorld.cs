using System.Collections.Generic;
using App.Scripts.Infrastructure.Blueprints.Core.Containers.Entities;
using Scellecs.Morpeh;

namespace App.Scripts.Infrastructure.Blueprints.Core.Containers.BlueprintWorld
{
    public interface IContainerBlueprintWorld
    {
        IReadOnlyList<IContainerBlueprintEntity> GetEntities();
        void BuildWorld(World world);
        void AddEntity(IContainerBlueprintEntity containerBlueprintEntity);
        IContainerBlueprintEntity FindEntityWith<T>() where T : struct, IComponent;
    }
}