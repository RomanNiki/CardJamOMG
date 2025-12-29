using App.Scripts.Features.Game.Input.Bootstrap;
using App.Scripts.Features.Game.Level.Systems;
using App.Scripts.Features.Game.Moving.Bootstrap;
using App.Scripts.Features.Game.Player.Systems;
using App.Scripts.Infrastructure.Extensions;
using App.Scripts.Infrastructure.VContainer.Extensions;
using VContainer;

namespace App.Scripts.Features.Game.Bootstrap
{
    public class InstallerGameLoop : Installer<InstallerGameLoop>
    {
        public override void Install(IContainerBuilder builder)
        {
            builder.RegisterInitializer<InitializableLevel>();
            builder.RegisterInitializer<InitializerPlayerInventory>();
            InstallerInput.Configure(builder);
            InstallerMoving.Configure(builder);
        }
    }
}