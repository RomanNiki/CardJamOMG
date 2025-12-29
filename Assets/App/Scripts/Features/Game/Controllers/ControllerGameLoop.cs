using System;
using System.Linq;
using System.Threading;
using App.Scripts.Features.Game.Level.Models;
using App.Scripts.Features.Game.Level.Providers;
using App.Scripts.Infrastructure.WorldExtesions.Containers;
using Cysharp.Threading.Tasks;
using Scellecs.Morpeh;
using VContainer.Unity;

namespace App.Scripts.Features.Game.Controllers
{
    public class ControllerGameLoop : IAsyncStartable, ITickable, IDisposable
    {
        private readonly IContainerWorld _container;
        private readonly IProviderLevels _providerLevels;

        public ControllerGameLoop(IContainerWorld container, IProviderLevels providerLevels)
        {
            _container = container;
            _providerLevels = providerLevels;
        }

        public void Initialize()
        {
            World world = World.Create();
      
            ModelLevel modelLevel = _providerLevels.GetLevels().First(x => x.levelNumber == 7);
          
            world.CreateEntity().SetComponent(modelLevel);
          
            _container.Initialize(world);
        }

        public void Tick()
        {
            _container.Update();
        }

        public void Dispose()
        {
            _container.Destroy();
        }

        public async UniTask StartAsync(CancellationToken cancellation = new())
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: cancellation);
            Initialize();
        }
    }
}