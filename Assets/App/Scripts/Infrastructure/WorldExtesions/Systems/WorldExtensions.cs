using Scellecs.Morpeh;

namespace App.Scripts.Infrastructure.WorldExtesions.Systems
{
    public static class WorldExtensions
    {
        public static Entity SendMassage<T>(this World world, T message) where T : struct, IComponent
        {
            var entity = world.CreateEntity();
            entity.SetComponent(message);
            return entity;
        }
    }
}