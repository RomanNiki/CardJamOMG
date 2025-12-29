using App.Scripts.Features.Game.Bootstrap;
using App.Scripts.Features.Game.Configs;
using App.Scripts.Features.Game.Controllers;
using App.Scripts.Features.Game.Views;
using App.Scripts.Infrastructure.Factory;
using App.Scripts.Infrastructure.Pool;
using App.Scripts.Infrastructure.WorldExtesions.Containers;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace App.Scripts.Features.Bootstrap
{
    public class LifeScopeGame : LifetimeScope
    {
        public ViewGrid viewGrid;
        public ViewInputZone viewInputZone;
        public CardConfig cardConfig;
        public RectTransform parent;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(viewGrid).As<ViewGrid>();
            builder.RegisterInstance(viewInputZone).As<ViewInputZone>();
            builder.RegisterInstance(cardConfig).As<CardConfig>();

            builder.Register<FactoryView<ViewCard>>(Lifetime.Singleton).AsSelf()
                .WithParameter(cardConfig.prefab)
                .WithParameter(viewGrid.root.transform);

            builder.Register<PoolFactoryView<ViewCard>>(Lifetime.Singleton).As<IFactory<ViewCard>>()
                .WithParameter(parent.transform);
            
            builder.Register<ContainerWorld>(Lifetime.Singleton).As<IContainerWorld>();
            
            InstallerGameLoop.Configure(builder);

            builder.RegisterEntryPoint<ControllerGameLoop>();
        }
    }
}