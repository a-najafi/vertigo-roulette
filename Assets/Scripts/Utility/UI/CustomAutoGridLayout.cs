namespace Utility.UI
{
    using UnityEngine;
    using UnityEngine.UI;

#if UNITY_EDITOR
    [ExecuteAlways]
#endif
    [RequireComponent(typeof(GridLayoutGroup))]
    /// <summary>
    /// Automatically resizes grid cell size based on a fixed column count and supports optional square alignment.
    /// </summary>
    public class CustomAutoGridLayout : MonoBehaviour
    {
        #region Serialized Parameters

        /// <summary>
        /// The number of columns in the grid.
        /// </summary>
        public int columnCount = 3;

        /// <summary>
        /// If true, cells are forced to be square.
        /// </summary>
        [SerializeField]
        private bool _squareAllignment = false;

        #endregion

        #region Non Serialized Parameters

        private GridLayoutGroup _grid;
        private RectTransform _rect;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _grid = GetComponent<GridLayoutGroup>();
            _rect = GetComponent<RectTransform>();

            _grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _grid.constraintCount = columnCount;
        }

#if UNITY_EDITOR
        private void Update()
        {
            AutoResizeCells();
        }
#endif

        #endregion

        #region Public Methods

        /// <summary>
        /// Automatically resizes the cell size based on current RectTransform width and column count.
        /// Optionally keeps cells square if _squareAllignment is enabled.
        /// </summary>
        public void AutoResizeCells()
        {
            // Calculate the available width for the grid (excluding padding and spacing).
            float totalWidth = _rect.rect.width - _grid.padding.left - _grid.padding.right -
                               _grid.spacing.x * (columnCount - 1);
            float cellSize = totalWidth / columnCount;

            _grid.cellSize = new Vector2(cellSize, _squareAllignment ? cellSize : _grid.cellSize.y);
        }

        #endregion
    }
}
