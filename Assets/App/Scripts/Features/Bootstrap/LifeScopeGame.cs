using App.Scripts.Features.Game.Bootstrap;
using App.Scripts.Features.Game.Views;
using VContainer;
using VContainer.Unity;

namespace App.Scripts.Features.Bootstrap
{
    public class LifeScopeGame : LifetimeScope
    {
        public ViewGrid viewGrid;
        public ViewInputZone viewInputZone;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(viewGrid).As<ViewGrid>();
            builder.RegisterInstance(viewInputZone).As<ViewInputZone>();
            InstallerGameLoop.Configure(builder);
        }
    }
}