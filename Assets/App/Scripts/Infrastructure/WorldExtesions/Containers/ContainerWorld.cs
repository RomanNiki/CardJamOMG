using System.Collections.Generic;
using Scellecs.Morpeh;
using UnityEngine;

namespace App.Scripts.Infrastructure.WorldExtesions.Containers
{
    public class ContainerWorld : IContainerWorld
    {
        private readonly IEnumerable<ISystem> _systems;
        private readonly IEnumerable<IInitializer> _initializers;
        private Scellecs.Morpeh.World _world;

        public ContainerWorld(IEnumerable<ISystem> systems, IEnumerable<IInitializer> initializers)
        {
            _systems = systems;
            _initializers = initializers;
        }

        public Scellecs.Morpeh.World World => _world;

        public void Initialize()
        {
            CreateWorldWithSystems(_systems, _initializers);
        }

        public void Update()
        {
            _world?.Update(Time.deltaTime);
        }
        
        public void LateUpdate()
        {
            _world?.LateUpdate(Time.deltaTime);
            _world?.CleanupUpdate(Time.deltaTime);
        }

        public void Destroy()
        {
            _world?.Dispose();
            _world = null;
        }

        private void CreateWorldWithSystems(IEnumerable<ISystem> systems, IEnumerable<IInitializer> initializers)
        {
            if (_world != null)
                return;
            
            _world = Scellecs.Morpeh.World.Create();
            var systemsGroup = _world.CreateSystemsGroup();

            foreach (var initializer in initializers)
            {
                systemsGroup.AddInitializer(initializer);
            }
            
            foreach (var system in systems)
            {
                systemsGroup.AddSystem(system);
            }

            _world.AddSystemsGroup(0, systemsGroup);
        }
    }
}