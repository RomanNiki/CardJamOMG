using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace App.Scripts.Infrastructure.VContainer.Extensions
{
    public class ProjectContext : LifetimeScope
    {
        [SerializeField] private List<MonoInstallerBase> _monoInstallers = new();

        protected override void Configure(IContainerBuilder builder)
        {
            foreach (var installer in _monoInstallers)
            {
                installer.Install(builder);
            }
        }
    }
}