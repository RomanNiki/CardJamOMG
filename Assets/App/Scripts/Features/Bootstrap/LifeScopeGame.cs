using App.Scripts.Features.Game.Bootstrap;
using App.Scripts.Features.Game.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace App.Scripts.Features.Bootstrap
{
    public class LifeScopeGame : LifetimeScope
    {
        [SerializeField] private ViewGame viewGame;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(viewGame).As<ViewGame>();
            InstallerGameLoop.Configure(builder);
        }
    }
}