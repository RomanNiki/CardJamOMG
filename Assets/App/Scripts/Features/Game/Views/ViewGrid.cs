using App.Scripts.Infrastructure.Extensions;
using UnityEngine;

namespace App.Scripts.Features.Game.Views
{
    public class ViewGrid : MonoBehaviour
    {
        public Vector2Int size;
        public RectTransform rectTransform;
        public RectTransform root;

        public Rect GetCellRect(Vector2Int pos)
        {
            if (pos.x < 0 || pos.y < 0 || pos.x >= size.x || pos.y >= size.y)
                return Rect.zero;

            Rect rootRect = rectTransform.GetWorldRect();

            float cellWidth = rootRect.width / size.x;
            float cellHeight = rootRect.height / size.y;

            float x = rootRect.xMin + cellWidth * pos.x - 1;
            float y = rootRect.yMax - cellHeight * pos.y;

            var cellRect = new Rect(x, y, cellWidth, cellHeight);
            Debug.DrawLine(new Vector3(cellRect.xMin, cellRect.yMax), new Vector3(cellRect.xMax, cellRect.yMin),
                Color.red, 50);
            return cellRect;
        }
    }
}