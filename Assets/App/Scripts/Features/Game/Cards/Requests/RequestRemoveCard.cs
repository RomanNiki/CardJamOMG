using Scellecs.Morpeh;

namespace App.Scripts.Features.Game.Cards.Requests
{
    public struct RequestRemoveCard : IComponent
    {
        public Entity card;

        public RequestRemoveCard(Entity cardToRemove)
        {
            card = cardToRemove;
        }
    }
}