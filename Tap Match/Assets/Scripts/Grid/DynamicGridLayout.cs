using UnityEngine;
using UnityEngine.UI;

namespace JGM.Game
{
    public class DynamicGridLayout
    {
        public void SetupGridLayout(GameSettings settings, GridLayoutGroup layout, ContentSizeFitter fitter)
        {
            layout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            layout.constraintCount = settings.columns;

            fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            fitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            CalculateCellSizeToFitLayoutWidth(settings, layout);

            float totalCellHeight = (layout.cellSize.y + layout.spacing.y) * settings.rows;
            float totalHeight = totalCellHeight + layout.padding.top + layout.padding.bottom;
            bool cellsOverflowLayoutHeight = totalHeight > (layout.transform as RectTransform).rect.height;

            // If the content size fitter cannot expand height because of overflow recalculate cell size
            if (cellsOverflowLayoutHeight)
            {
                fitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
                fitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                RecalculateCellSizeToFitLayoutHeight(settings, layout);
            }
        }

        private void CalculateCellSizeToFitLayoutWidth(GameSettings settings, GridLayoutGroup layout)
        {
            var rectTransform = (layout.transform as RectTransform);
            float availableWidth = rectTransform.rect.width - (layout.padding.left + layout.padding.right) - (layout.spacing.x * (settings.columns - 1));
            int totalColumns = Mathf.Max(1, settings.columns);
            float idealCellSize = availableWidth / totalColumns;
            float cellSize = Mathf.Max(idealCellSize, settings.minCellSize);
            layout.cellSize = new Vector2(cellSize, cellSize);
        }

        private void RecalculateCellSizeToFitLayoutHeight(GameSettings settings, GridLayoutGroup layout)
        {
            var rectTransform = (layout.transform as RectTransform);
            float availableHeight = rectTransform.rect.height - (layout.padding.top + layout.padding.bottom) - (layout.spacing.y * (settings.rows - 1));
            int totalRows = Mathf.Max(1, settings.rows);
            float cellSize = availableHeight / totalRows;
            layout.cellSize = new Vector2(cellSize, cellSize);
        }
    }
}
