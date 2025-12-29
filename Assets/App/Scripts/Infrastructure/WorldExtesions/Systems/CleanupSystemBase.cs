using Scellecs.Morpeh;

namespace App.Scripts.Infrastructure.WorldExtesions.Systems
{
    public class CleanupSystemBase : ICleanupSystem
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