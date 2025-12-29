using App.Scripts.Features.Game.Views;
using Scellecs.Morpeh;

namespace App.Scripts.Features.Game.Level.Components
{
    public struct Field : IComponent
    {
        public ViewGrid ViewGrid;

        public Field(ViewGrid viewGrid)
        {
            ViewGrid = viewGrid;
        }
    }
}