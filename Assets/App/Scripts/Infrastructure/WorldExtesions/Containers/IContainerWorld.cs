namespace App.Scripts.Infrastructure.WorldExtesions.Containers
{
    public interface IContainerWorld
    {
        Scellecs.Morpeh.World World { get; }
        void Initialize();
        void Update();
        void LateUpdate();
        void Destroy();
    }
}