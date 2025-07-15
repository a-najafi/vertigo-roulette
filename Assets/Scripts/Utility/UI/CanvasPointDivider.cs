namespace Utility.UI
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Utility for dividing a RectTransform’s width into evenly spaced points.
    /// </summary>
    public static class CanvasPointDivider
    {
        #region Public Methods

        /// <summary>
        /// Divides the width of a RectTransform into equal horizontal points, optionally including outside edge points.
        /// Anchored so that the central point is always at the horizontal center.
        /// </summary>
        /// <param name="canvasRect">RectTransform to divide</param>
        /// <param name="pointCount">Must be odd for a center point</param>
        /// <param name="points">Output list of anchored positions (centered on X axis)</param>
        /// <param name="spacing">Output: distance between each point</param>
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

            // Ensure odd number for perfect center point.
            if (pointCount % 2 == 0)
            {
                Debug.LogWarning("CanvasPointDivider: pointCount should be odd for a center point. Auto-incrementing.");
                pointCount += 1;
            }

            spacing = width / (pointCount - 1);

            // For outside points, start before 0 and end after the main set.
            int start = includeOutsidePoints ? -1 : 0;
            int end = includeOutsidePoints ? pointCount + 1 : pointCount;

            for (int i = start; i < end; i++)
            {
                // Center the points horizontally by offsetting from the middle.
                float x = (i - (pointCount - 1) / 2f) * spacing;
                points.Add(new Vector2(x, 0));
            }
        }

        #endregion
    }
}
