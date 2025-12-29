using App.Scripts.Features.Game.Input.Systems;
using App.Scripts.Infrastructure.Extensions;
using App.Scripts.Infrastructure.VContainer.Extensions;
using VContainer;

namespace App.Scripts.Features.Game.Input.Bootstrap
{
    public class InstallerInput : Installer<InstallerInput>
    {
        public override void Install(IContainerBuilder builder)
        {
            builder.RegisterSystem<SwipeInputSystem>();
            builder.RegisterSystem<InertiaSystem>();
        }
    }
}
