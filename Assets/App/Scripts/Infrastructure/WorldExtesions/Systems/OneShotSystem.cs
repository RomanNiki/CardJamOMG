using Scellecs.Morpeh;

namespace App.Scripts.Infrastructure.WorldExtesions.Systems
{
    public class OneShotSystem<T> : ISystem
        where T : struct, IComponent
    {
        private Stash<T> _oneShotStash;
        public World World { get; set; }

        public void OnAwake()
        {
            _oneShotStash = World.GetStash<T>();
        }

        public void OnUpdate(float deltaTime)
        {
            _oneShotStash.RemoveAll();
        }

        public void Dispose()
        {
        }
    }
}