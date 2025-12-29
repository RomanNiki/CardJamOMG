using Scellecs.Morpeh;

namespace App.Scripts.Infrastructure.WorldExtesions.Systems
{
    public class InitializerBase : IInitializer
    {
        public World World { get; set; }
        
        public virtual void OnAwake()
        {
        }
        
        public virtual void Dispose()
        {
        }
    }
}