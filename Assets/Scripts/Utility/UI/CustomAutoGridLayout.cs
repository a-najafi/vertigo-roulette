namespace Utility.UI
{
    using UnityEngine;
    using UnityEngine.UI;

#if UNITY_EDITOR
    [ExecuteAlways]
#endif
    [RequireComponent(typeof(GridLayoutGroup))]
    public class CustomAutoGridLayout : MonoBehaviour
    {
        public int columnCount = 3;
        [SerializeField]private bool _squareAllignment = false;
        private GridLayoutGroup _grid;
        private RectTransform _rect;

        void Awake()
        {
            _grid = GetComponent<GridLayoutGroup>();
            _rect = GetComponent<RectTransform>();

            _grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _grid.constraintCount = columnCount;
        }
#if UNITY_EDITOR
        void Update()
        {
            AutoResizeCells();
        }
#endif
        public void AutoResizeCells()
        {
            // Use the available width for square cells
            float totalWidth = _rect.rect.width - _grid.padding.left - _grid.padding.right -
                               _grid.spacing.x * (columnCount - 1);
            float cellSize = totalWidth / columnCount;

            _grid.cellSize = new Vector2(cellSize, _squareAllignment ? cellSize : _grid.cellSize.y);
        }
    }
}