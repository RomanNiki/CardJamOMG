using UnityEngine;

namespace App.Scripts.Features.Game.Views
{
    public class ViewGrid : MonoBehaviour
    {
        public Vector2Int size;
        public RectTransform rectTransform;

        public Rect GetCellRect(Vector2Int pos)
        {
            var rect = rectTransform.rect;
            var cellSize = new Vector2(rect.width / size.x, rect.height / size.y);
            return new Rect(
                rect.xMin + pos.x * cellSize.x,
                rect.yMin + (size.y - 1 - pos.y) * cellSize.y,
                cellSize.x,
                cellSize.y
            );
        }
    }
}