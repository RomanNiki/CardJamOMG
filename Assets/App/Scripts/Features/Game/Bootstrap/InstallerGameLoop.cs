using App.Scripts.Features.Game.Cards.Systems;
using App.Scripts.Features.Game.Input.Bootstrap;
using App.Scripts.Features.Game.Level.Systems;
using App.Scripts.Features.Game.Meta.System;
using App.Scripts.Features.Game.Moving.Bootstrap;
using App.Scripts.Features.Game.Player.Systems;
using App.Scripts.Infrastructure.Extensions;
using App.Scripts.Infrastructure.VContainer.Extensions;
using App.Scripts.Infrastructure.WorldExtesions.Systems;
using VContainer;

namespace App.Scripts.Features.Game.Bootstrap
{
    public class InstallerGameLoop : Installer<InstallerGameLoop>
    {
        public override void Install(IContainerBuilder builder)
        {
            builder.RegisterSystem<OneShotSystem<EventDestroed>>();
            builder.RegisterInitializer<InitializableLevel>();
            builder.RegisterInitializer<InitializerPlayerInventory>();
            builder.RegisterSystem<SpawnCardSystem>();
            builder.RegisterSystem<SpawnViewCardSystem>();
            builder.RegisterSystem<SelectCurrentCardSystem>();
            builder.RegisterSystem<MatchSystem>();
            builder.RegisterSystem<DeadZoneSystem>();
            InstallerInput.Configure(builder);
            InstallerMoving.Configure(builder);
            builder.RegisterSystem<DestroyCardSystem>();
            builder.RegisterSystem<GameFinalSystem>();
        }
    }
}