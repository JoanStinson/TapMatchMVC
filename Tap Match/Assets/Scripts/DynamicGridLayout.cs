using UnityEngine;
using UnityEngine.UI;

namespace JGM.Game
{
    [RequireComponent(typeof(GridLayoutGroup))]
    [RequireComponent(typeof(ContentSizeFitter))]
    public class DynamicGridLayout : MonoBehaviour
    {
        public GridSize gridSize;
        public GameObject prefabToInstantiate; // Assign the prefab in the Inspector

        private GridLayoutGroup gridLayout;
        private ContentSizeFitter contentSizeFitter;

        private void Start()
        {
            gridLayout = GetComponent<GridLayoutGroup>();
            contentSizeFitter = GetComponent<ContentSizeFitter>();

            SetupGridLayout();
        }

        private void SetupGridLayout()
        {
            gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            gridLayout.constraintCount = gridSize.cols;

            CalculateCellSize();
            InstantiatePrefabs();

            float totalCellHeight = (gridLayout.cellSize.y + gridLayout.spacing.y) * gridSize.rows;
            float totalHeight = totalCellHeight + gridLayout.padding.top + gridLayout.padding.bottom;

            if (totalHeight <= GetComponent<RectTransform>().rect.height)
            {
                contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            }
            else
            {
                contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
                contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;

                // Recalculate cell size based on the available height of the RectTransform
                float availableHeight = GetComponent<RectTransform>().rect.height - (gridLayout.padding.top + gridLayout.padding.bottom) - (gridLayout.spacing.y * (gridSize.rows - 1));

                int totalRows = Mathf.Max(1, gridSize.rows);
                float cellSize = availableHeight / totalRows;

                gridLayout.cellSize = new Vector2(cellSize, cellSize);
            }
        }

        private void CalculateCellSize()
        {
            RectTransform rectTransform = GetComponent<RectTransform>();

            float availableWidth = rectTransform.rect.width - (gridLayout.padding.left + gridLayout.padding.right) - (gridLayout.spacing.x * (gridSize.cols - 1));
            float availableHeight = rectTransform.rect.height - (gridLayout.padding.top + gridLayout.padding.bottom);

            int totalColumns = Mathf.Max(1, gridSize.cols); // Ensure at least 1 column

            float idealCellSize = availableWidth / totalColumns;
            float cellSize = Mathf.Max(idealCellSize, gridSize.minCellSize);

            gridLayout.cellSize = new Vector2(cellSize, cellSize);
        }

        private void InstantiatePrefabs()
        {
            int totalElements = gridSize.rows * gridSize.cols;

            for (int i = 0; i < totalElements; i++)
            {
                Instantiate(prefabToInstantiate, transform); // Instantiate the prefab under the DynamicGridLayout
            }
        }
    }
}
