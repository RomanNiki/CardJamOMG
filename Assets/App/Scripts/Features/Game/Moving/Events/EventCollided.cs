using Scellecs.Morpeh;

namespace App.Scripts.Features.Game.Moving.Events
{
    public struct EventCollided : IComponent
    {
        public Entity FirstEntity;
        public Entity SecondEntity;

        public EventCollided(Entity aEntity, Entity bEntity)
        {
            FirstEntity = aEntity;
            SecondEntity = bEntity;
        }
    }
}