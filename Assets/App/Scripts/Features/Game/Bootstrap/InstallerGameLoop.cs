using App.Scripts.Features.Game.Input.Bootstrap;
using App.Scripts.Features.Game.Moving.Bootstrap;
using App.Scripts.Infrastructure.VContainer.Extensions;
using VContainer;

namespace App.Scripts.Features.Game.Bootstrap
{
    public class InstallerGameLoop : Installer<InstallerGameLoop>
    {
        public override void Install(IContainerBuilder builder)
        {
            InstallerInput.Configure(builder);
            InstallerMoving.Configure(builder);
        }
    }
}