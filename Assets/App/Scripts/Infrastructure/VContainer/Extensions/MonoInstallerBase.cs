using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace App.Scripts.Infrastructure.VContainer.Extensions
{
    public abstract class MonoInstallerBase : MonoBehaviour, IInstaller
    {
        public abstract void Install(IContainerBuilder builder);
    }
}