using App.Scripts.Features.Game.Moving.Components;
using App.Scripts.Features.Game.Moving.Systems;
using App.Scripts.Infrastructure.Extensions;
using App.Scripts.Infrastructure.VContainer.Extensions;
using App.Scripts.Infrastructure.WorldExtesions.Systems;
using VContainer;

namespace App.Scripts.Features.Game.Moving.Bootstrap
{
    public class InstallerMoving : Installer<InstallerMoving>
    {
        public override void Install(IContainerBuilder builder)
        {
            builder.RegisterInitializer<InitializerDisposableStashes>();
            builder.RegisterSystem<GravitySystem>();
            builder.RegisterSystem<ForceSystem>();
            builder.RegisterSystem<VelocitySystem>();
            builder.RegisterSystem<ExecuteTransformSystem>();
            builder.RegisterSystem<OneShotSystem<ForceRequest>>();
        }
    }
}