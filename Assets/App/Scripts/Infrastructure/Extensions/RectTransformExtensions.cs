using UnityEngine;

namespace App.Scripts.Infrastructure.Extensions
{
    public static class RectTransformExtensions
    {
        public static Rect GetWorldRect(this RectTransform rectTransform)
        {
            Vector3[] corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);
            
            // corners[0] - bottom left
            // corners[1] - top left
            // corners[2] - top right
            // corners[3] - bottom right
            
            Vector2 min = corners[0];
            Vector2 max = corners[2];
            Vector2 size = max - min;
            
            return new Rect(min, size);
        }
    }
}
