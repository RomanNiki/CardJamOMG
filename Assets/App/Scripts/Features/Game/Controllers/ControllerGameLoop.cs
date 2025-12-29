using System;
using System.Collections.Generic;
using System.Threading;
using App.Scripts.Features.Game.Level.Models;
using App.Scripts.Infrastructure.WorldExtesions.Containers;
using Cysharp.Threading.Tasks;
using Scellecs.Morpeh;
using UnityEngine;
using VContainer.Unity;

namespace App.Scripts.Features.Game.Controllers
{
    public class ControllerGameLoop : IAsyncStartable, ITickable, IDisposable
    {
        private readonly IContainerWorld _container;

        public ControllerGameLoop(IContainerWorld container)
        {
            _container = container;
        }

        public void Initialize()
        {
            World world = World.Create();
            world.CreateEntity().SetComponent(new ModelLevel()
            {
                listCardsPos = new List<ModelCard>()
                {
                    new ModelCard()
                    {
                        position = new Vector2Int(1, 1),
                    },
                    new ModelCard()
                    {
                        position = new Vector2Int(2, 2),
                    },
                    new ModelCard()
                    {
                        position = new Vector2Int(4, 4),
                    }
                },
                countAdditional = 5
            });
          
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

        public async UniTask StartAsync(CancellationToken cancellation = new CancellationToken())
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: cancellation);
            Initialize();
        }
    }
}