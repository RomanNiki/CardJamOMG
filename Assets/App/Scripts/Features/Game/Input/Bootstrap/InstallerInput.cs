using App.Scripts.Features.Game.Input.Events;
using App.Scripts.Features.Game.Input.Systems;
using App.Scripts.Infrastructure.Extensions;
using App.Scripts.Infrastructure.VContainer.Extensions;
using App.Scripts.Infrastructure.WorldExtesions.Systems;
using VContainer;

namespace App.Scripts.Features.Game.Input.Bootstrap
{
    public class InstallerInput : Installer<InstallerInput>
    {
        public override void Install(IContainerBuilder builder)
        {
            builder.RegisterSystem<OneShotSystem<EventSwipe>>();
            builder.RegisterSystem<SwipeStartSystem>();
            builder.RegisterSystem<SwipeProcessSystem>();
            builder.RegisterSystem<SwipeEndSystem>();
            builder.RegisterSystem<InertiaSystem>();
        }
    }
}
