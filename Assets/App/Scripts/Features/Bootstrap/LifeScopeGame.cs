using App.Scripts.Features.Game.Bootstrap;
using App.Scripts.Features.Game.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace App.Scripts.Features.Bootstrap
{
    public class LifeScopeGame : LifetimeScope
    {
        [SerializeField] private ViewGrid viewGrid;
        [SerializeField] private ViewInputZone viewInputZone;
        
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(viewGrid).As<ViewGrid>();
            builder.RegisterInstance(viewInputZone).As<ViewInputZone>();
            InstallerGameLoop.Configure(builder);
        }
    }
}