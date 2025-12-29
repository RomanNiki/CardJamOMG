using System.Runtime.CompilerServices;
using Scellecs.Morpeh;
using VContainer;

namespace App.Scripts.Infrastructure.Extensions
{
    public static class SystemGroupInjections
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSystem CreateSystem<TSystem>(this IObjectResolver container)
            where TSystem : class, ISystem
        {
            var builder = new RegistrationBuilder(typeof(TSystem), Lifetime.Transient);
            var registration = builder.Build();
            return registration.SpawnInstance(container) as TSystem;
        } 
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSystem Create<TSystem>(this IObjectResolver container)
            where TSystem : class
        {
            var builder = new RegistrationBuilder(typeof(TSystem), Lifetime.Transient);
            var registration = builder.Build();
            return registration.SpawnInstance(container) as TSystem;
        }

        public static void RegisterSystem<TSystem>(this IContainerBuilder containerBuilder)
        where TSystem : class, ISystem
        {
            containerBuilder.Register<TSystem>(Lifetime.Transient).As<ISystem>();
        } 
        
        public static void RegisterInitializer<TSystem>(this IContainerBuilder containerBuilder)
        where TSystem : class, IInitializer
        {
            containerBuilder.Register<TSystem>(Lifetime.Transient).As<IInitializer>();
        }
        
    }
}