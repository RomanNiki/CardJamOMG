using Scellecs.Morpeh;

namespace App.Scripts.Infrastructure.Blueprints.Core.Containers.Components
{
    public interface IContainerBlueprintComponent
    {
        void ApplyToEntity(Entity entity);
    }
}