using VContainer;

namespace App.Scripts.Infrastructure.VContainer.Extensions
{
    public abstract class Installer<TDerived> : InstallerBase where TDerived : Installer<TDerived>, new()
    {
        protected Installer()
        {
        }
        
        public static void Configure(IContainerBuilder builder)
        {
            var installer = new TDerived();
            installer.Install(builder);
        }
    }
}