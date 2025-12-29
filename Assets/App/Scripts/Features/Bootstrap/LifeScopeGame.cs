using App.Scripts.Features.Game.Bootstrap;
using App.Scripts.Features.Game.Configs;
using App.Scripts.Features.Game.Controllers;
using App.Scripts.Features.Game.Views;
using App.Scripts.Infrastructure.WorldExtesions.Containers;
using VContainer;
using VContainer.Unity;

namespace App.Scripts.Features.Bootstrap
{
    public class LifeScopeGame : LifetimeScope
    {
        public ViewGrid viewGrid;
        public ViewInputZone viewInputZone;
        public CardConfig cardConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<ContainerWorld>(Lifetime.Singleton).As<IContainerWorld>();
           
            builder.RegisterInstance(viewGrid).As<ViewGrid>();
            builder.RegisterInstance(viewInputZone).As<ViewInputZone>();
            builder.RegisterInstance(cardConfig).As<CardConfig>();
            InstallerGameLoop.Configure(builder);

            builder.RegisterEntryPoint<ControllerGameLoop>();
        }
    }
}