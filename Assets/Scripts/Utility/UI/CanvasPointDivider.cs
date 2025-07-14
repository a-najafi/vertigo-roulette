namespace Utility.UI
{
    using System.Collections.Generic;
    using UnityEngine;
    public static class CanvasPointDivider
    {
        /// <summary>
        /// Divides the width of a RectTransform into equal horizontal points.
        /// </summary>
        /// <param name="canvasRect">RectTransform to divide</param>
        /// <param name="pointCount">Must be odd for a center point</param>
        /// <param name="points">Output list of anchored positions (centered)</param>
        /// <param name="spacing">Output spacing between each point</param>
        /// <param name="includeOutsidePoints">If true, adds one point before and after the main range</param>
        public static void DivideHorizontally(
            RectTransform canvasRect,
            int pointCount,
            out List<Vector2> points,
            out float spacing,
            bool includeOutsidePoints = false)
        {
            points = new List<Vector2>();

            float width = canvasRect.rect.width;

            if (pointCount % 2 == 0)
            {
                Debug.LogWarning("CanvasPointDivider: pointCount should be odd for a center point.");
                pointCount += 1;
            }

            spacing = width / (pointCount - 1);

            int start = includeOutsidePoints ? -1 : 0;
            int end = includeOutsidePoints ? pointCount + 1 : pointCount;

            for (int i = start; i < end; i++)
            {
                float x = (i - (pointCount - 1) / 2f) * spacing;
                points.Add(new Vector2(x, 0));
            }
        }
    }
}