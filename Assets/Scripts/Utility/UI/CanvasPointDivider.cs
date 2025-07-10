namespace Utility.UI
{
    using System.Collections.Generic;
    using UnityEngine;

    public static class CanvasPointDivider
    {
        public static (List<Vector3> points, float spacing) GetUIHorizontalPoints(RectTransform canvasRect, int count, bool includeOutside = false)
        {
            if (count % 2 == 0 || count < 1)
                throw new System.ArgumentException("Count must be an odd number greater than 0.");

            float width = canvasRect.rect.width;
            float spacing = width / (count - 1);
            float startX = -width / 2f;

            List<Vector3> positions = new List<Vector3>();
            for (int i = 0; i < (count); i++)
            {
                float x = startX + i * spacing;
                positions.Add(new Vector3(x, 0f, 0f));
            }

            if (includeOutside)
            {
                Vector3 leftOutside = new Vector3(startX - spacing, 0f, 0f);
                
                positions.Insert(0,leftOutside);
                
                Vector3 rightOutside = new Vector3(startX + count * spacing, 0f, 0f);
                
                positions.Add(rightOutside);
                    
            }
            
            return (positions, spacing);
        }
    }

}