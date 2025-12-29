using Scellecs.Morpeh;

namespace App.Scripts.Infrastructure.WorldExtesions.Systems
{
    public abstract class SystemBase : ISystem
    {
        public World World { get; set; }
        
        public virtual void OnAwake()
        {
        }

        public virtual void OnUpdate(float deltaTime)
        {
        }

        public virtual void Dispose()
        {
        }
    }
}