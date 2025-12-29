using Scellecs.Morpeh;

namespace App.Scripts.Infrastructure.WorldExtesions.Containers
{
    public interface IContainerWorld
    {
        Scellecs.Morpeh.World World { get; }
        void Initialize(World world = null);
        void Update();
        void LateUpdate();
        void Destroy();
    }
}