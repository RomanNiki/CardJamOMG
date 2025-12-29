using VContainer;
using VContainer.Unity;

namespace App.Scripts.Infrastructure.VContainer.Extensions
{
    public abstract class InstallerBase : IInstaller
    {
        public abstract void Install(IContainerBuilder builder);
    }
}