using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Utility.Coroutine;
using Utility.UI;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap.UI
{
    /// <summary>
    /// Handles creation, display, and animation of zone displays on the zone map UI.
    /// </summary>
    public class ZoneMapDisplayComponent : MonoBehaviour
    {
        #region Serialized Parameters

        [SerializeField] private Canvas _canvas;
        [SerializeField] private int _numberOfZonesToDisplay = 5;
        [SerializeField] private GameObject _zoneDisplayPrefab = null;
        [SerializeField] private RectTransform _zoneDisplayParent = null;

        #endregion

        #region Non-Serialized Parameters

        private Dictionary<int, ZoneDisplayComponent> zoneDisplays = new Dictionary<int, ZoneDisplayComponent>();
        private List<Vector2> zoneDisplayPositions = null;
        private float zoneDisplaySpacing = -1;

        #endregion

        #region Properties

        /// <summary>
        /// The display index considered 'active' (usually center).
        /// </summary>
        private int CurrentActiveZoneToDisplayIndex
        {
            get
            {
                if (_numberOfZonesToDisplay <= 0)
                    return -1;
                if (_numberOfZonesToDisplay == 1)
                    return 1;
                return (_numberOfZonesToDisplay / 2) + 1;
            }
        }

        /// <summary>
        /// Maximum offset to left/right of center.
        /// </summary>
        protected int MaxOffset => ((_numberOfZonesToDisplay / 2) + 1);

        #endregion

        #region Public Methods

        /// <summary>
        /// Converts a zone index in the model to a display index in the UI.
        /// </summary>
        public int ConvertZoneIndexToZonesToDisplayIndex(int zoneIndex, int activeZoneIndex)
        {
            int currentActiveZoneToDisplayIndex = CurrentActiveZoneToDisplayIndex;
            if (zoneIndex == currentActiveZoneToDisplayIndex)
                return currentActiveZoneToDisplayIndex;
            else if (zoneIndex > activeZoneIndex)
                return currentActiveZoneToDisplayIndex + (zoneIndex - activeZoneIndex);
            else // zoneIndex < activeZoneIndex
                return currentActiveZoneToDisplayIndex - (zoneIndex - activeZoneIndex);
        }

        /// <summary>
        /// Removes or disables all zone displays (cleanup for pooling or scene unload).
        /// </summary>
        public IEnumerator Cleanup()
        {
            // Future implementation: pool/cleanup
            yield return null;
        }

        /// <summary>
        /// Instantiates and positions zone displays for the given zone map state.
        /// </summary>
        public IEnumerator InitializeZones(ZoneMapInstance zoneMapInstance, int activeZoneIndex = 0)
        {
            CanvasPointDivider.DivideHorizontally(
                _canvas.GetComponent<RectTransform>(),
                _numberOfZonesToDisplay,
                out zoneDisplayPositions,
                out zoneDisplaySpacing,
                true);

            int leftMostZoneDisplayIndex = Mathf.Max(0, activeZoneIndex - MaxOffset);
            int rightMostZoneDisplayIndex = zoneMapInstance.MaxProgression > 0
                ? Mathf.Min(zoneMapInstance.MaxProgression, activeZoneIndex + MaxOffset)
                : activeZoneIndex + MaxOffset;

            List<IEnumerator> initZoneDisplayRoutines = new List<IEnumerator>();

            for (int i = leftMostZoneDisplayIndex; i < rightMostZoneDisplayIndex; i++)
            {
                int zoneDisplayIndex = ConvertZoneIndexToZonesToDisplayIndex(i, activeZoneIndex);
                ZoneDisplayComponent zoneDisplay = AddDisplayZone(zoneDisplayIndex);
                zoneDisplays.Add(zoneDisplayIndex, zoneDisplay);
                initZoneDisplayRoutines.Add(zoneDisplay.Initialize(zoneMapInstance.GetZoneInstance(i)));
            }
            yield return this.WaitForAll(initZoneDisplayRoutines);
        }

        /// <summary>
        /// Instantiates a display at the desired index, positions and sizes it.
        /// </summary>
        public ZoneDisplayComponent AddDisplayZone(int zoneDisplayIndex)
        {
            GameObject zoneDisplayGameObject = Instantiate(_zoneDisplayPrefab, _zoneDisplayParent);
            zoneDisplayGameObject.name = _zoneDisplayPrefab.name + "_" + _zoneDisplayParent.childCount.ToString();
            RectTransform zoneDisplayRectTransform = zoneDisplayGameObject.GetComponent<RectTransform>();

            zoneDisplayRectTransform.anchoredPosition = zoneDisplayPositions[zoneDisplayIndex];
            float width = Mathf.Min(zoneDisplaySpacing * 0.9f, zoneDisplayRectTransform.rect.width);
            zoneDisplayRectTransform.sizeDelta = new Vector2(width, width);
            return zoneDisplayGameObject.GetComponent<ZoneDisplayComponent>();
        }

        /// <summary>
        /// Moves all displays to the next zone in the map, animating them.
        /// </summary>
        public IEnumerator MoveZonesNextByOne(ZoneMapInstance zoneMapInstance)
        {
            Sequence parallelSequence = DOTween.Sequence();
            int lastActiveZoneIndex = GetActiveZoneDisplay().ZoneInstance.ZoneIndex;
            int newActiveZoneIndex = lastActiveZoneIndex + 1;

            bool leavingZoneIsReplacing = false;
            for (int i = 0; i < _numberOfZonesToDisplay + 2; i++)
            {
                if (zoneDisplays.ContainsKey(i))
                {
                    if (i == 0)
                    {
                        // Move cached zone to end
                        zoneDisplays[i].GetComponent<RectTransform>().anchoredPosition = zoneDisplayPositions[_numberOfZonesToDisplay + 1];
                        yield return zoneDisplays[i].Initialize(zoneMapInstance.GetZoneInstance(lastActiveZoneIndex + MaxOffset));

                        parallelSequence.Join(zoneDisplays[i].GetComponent<RectTransform>()
                            .DOAnchorPos(zoneDisplayPositions[_numberOfZonesToDisplay], 1f).SetEase(Ease.OutBounce));

                        leavingZoneIsReplacing = true;
                    }
                    else
                    {
                        parallelSequence.Join(zoneDisplays[i].GetComponent<RectTransform>()
                            .DOAnchorPos(zoneDisplayPositions[i - 1], 1f).SetEase(Ease.OutBounce));
                    }
                }
                else if (i - 1 > CurrentActiveZoneToDisplayIndex)
                {
                    if (!leavingZoneIsReplacing)
                    {
                        ZoneDisplayComponent zoneDisplay = AddDisplayZone(i);
                        yield return zoneDisplay.Initialize(
                            zoneMapInstance.GetZoneInstance(zoneMapInstance.ActiveZoneIndex + (i - CurrentActiveZoneToDisplayIndex - 1)));
                        zoneDisplays.Add(0, zoneDisplay);
                        parallelSequence.Join(zoneDisplays[0].GetComponent<RectTransform>()
                            .DOAnchorPos(zoneDisplayPositions[i - 1], 1f).SetEase(Ease.OutBounce));
                    }
                }
            }

            yield return parallelSequence.WaitForCompletion();

            zoneDisplays.TryGetValue(0, out ZoneDisplayComponent lastZoneDisplay);
            int finalIndex = _numberOfZonesToDisplay + 1;

            for (int i = 0; i < _numberOfZonesToDisplay; i++)
            {
                if (zoneDisplays.ContainsKey(i + 1))
                {
                    zoneDisplays[i] = zoneDisplays[i + 1];
                }
                else
                {
                    zoneDisplays.Remove(i);
                }
            }
            zoneDisplays.Remove(finalIndex);

            // Assign the recycled zone display to the final slot
            if (lastZoneDisplay != null)
            {
                zoneDisplays[_numberOfZonesToDisplay] = lastZoneDisplay;
            }
            else
            {
                zoneDisplays.Remove(_numberOfZonesToDisplay);
            }
        }

        /// <summary>
        /// Returns the ZoneDisplayComponent considered currently active (center of display).
        /// </summary>
        public ZoneDisplayComponent GetActiveZoneDisplay()
        {
            int activeZoneDisplayIndex = CurrentActiveZoneToDisplayIndex;
            if (activeZoneDisplayIndex < 0 || activeZoneDisplayIndex >= _numberOfZonesToDisplay)
                return null;
            if (!zoneDisplays.ContainsKey(CurrentActiveZoneToDisplayIndex))
                return null;
            return zoneDisplays[CurrentActiveZoneToDisplayIndex];
        }

        /// <summary>
        /// Displays the final reward on the active zone display.
        /// </summary>
        public IEnumerator DisplayRewardOnActiveZone()
        {
            yield return zoneDisplays[CurrentActiveZoneToDisplayIndex].DisplayFinalReward();
        }

        #endregion
    }
}
