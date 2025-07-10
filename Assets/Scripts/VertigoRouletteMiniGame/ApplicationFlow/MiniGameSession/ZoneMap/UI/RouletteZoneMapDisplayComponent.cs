using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;
using Utility.UI;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap.UI
{
    public class RouletteZoneMapDisplayComponent : MonoBehaviour
    {
        [SerializeField]private Canvas _canvas;
        [SerializeField]private int _numberOfZonesToDisplay = 5;
        [SerializeField] private GameObject _zoneDisplayPrefab = null;
        [SerializeField] private RectTransform _zoneDisplayParent = null;
        private int currentActiveZoneIndex = -1;



        List<RouletteZoneUIElement> zoneDisplays = new List<RouletteZoneUIElement>();
        List<GameObject > zoneDisplaysInPool = new List<GameObject>();

        private List<Vector3> zoneDisplayPositions = null;
        private float zoneDisplaySpacing = -1;
        
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

        public int ConvertZoneIndexToZonesToDisplayIndex(int zoneIndex)
        {
            int currentActiveZoneToDisplayIndex = CurrentActiveZoneToDisplayIndex;
            if(zoneIndex == currentActiveZoneToDisplayIndex)
                return currentActiveZoneToDisplayIndex;
            else if (zoneIndex > currentActiveZoneIndex)
                return currentActiveZoneToDisplayIndex + (zoneIndex - currentActiveZoneIndex);
            else //if(zoneIndex < currentActiveZoneIndex)
                return currentActiveZoneToDisplayIndex - (zoneIndex - currentActiveZoneIndex);
        }

        public void Initialize()
        {
            float canvasWidth = _canvas.pixelRect.width;
            (zoneDisplayPositions,zoneDisplaySpacing)=CanvasPointDivider.GetUIHorizontalPoints(_canvas.GetComponent<RectTransform>(), _numberOfZonesToDisplay,true);
            
            
        }

        public IEnumerator Cleanup()
        {
            
        }

        public IEnumerator InitializeZones(List<ZoneInstance> zones)
        {
            
            currentActiveZoneIndex = 0;

            int leftMostZoneDisplayIndex = Mathf.Max(0, currentActiveZoneIndex - ((_numberOfZonesToDisplay / 2) + 1));
            int rightMostZoneDisplayIndex = Mathf.Min(zones.Count -1,currentActiveZoneIndex + ((_numberOfZonesToDisplay / 2) + 1) );
            
            for (int i = leftMostZoneDisplayIndex; i < rightMostZoneDisplayIndex; i++)
            {
                int zoneDisplayIndex = ConvertZoneIndexToZonesToDisplayIndex(i);
                
                GameObject zoneDisplayGameObject = Instantiate(_zoneDisplayPrefab, _zoneDisplayParent);
                RouletteZoneUIElement zoneDisplay = zoneDisplayGameObject.GetComponent<RouletteZoneUIElement>();
               // zoneDisplay.Initialize()

                
                
            }
            
        }

        private RouletteZoneUIElement CreateZoneDisplay(ZoneInstance zone)
        {
            
        }

        protected void SpawnZoneUIElementAtZoneIndex(int zoneIndex)
        {
            
        }

        public IEnumerator UpdateActiveZone(int activeZoneIndex, List<ZoneInstance> zones)
        {
            
            if (currentActiveZoneIndex != activeZoneIndex)
            {
                if (zoneDisplayPrefab == null)
                {
                    var loadZoneUIElementPrefab = _zoneDisplayPrefabAssetReference.LoadAssetAsync<GameObject>();
                    yield return loadZoneUIElementPrefab;
                    zoneDisplayPrefab = loadZoneUIElementPrefab.Result;
                }    
            }
            
            
            
            
            yield return null;
        }
        
        
    }
}