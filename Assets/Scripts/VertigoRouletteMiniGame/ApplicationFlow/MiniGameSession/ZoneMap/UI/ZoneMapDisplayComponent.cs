using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;
using Utility.Couroutine;
using Utility.UI;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap.UI
{
    public class ZoneMapDisplayComponent : MonoBehaviour
    {
        [SerializeField]private Canvas _canvas;
        [SerializeField]private int _numberOfZonesToDisplay = 5;
        [SerializeField] private GameObject _zoneDisplayPrefab = null;
        [SerializeField] private RectTransform _zoneDisplayParent = null;



        Dictionary<int,ZoneDisplayComponent> zoneDisplays = new Dictionary<int,ZoneDisplayComponent>();
        List<GameObject > zoneDisplaysInPool = new List<GameObject>();

        private List<Vector2> zoneDisplayPositions = null;
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

        protected int MaxOffset => ((_numberOfZonesToDisplay / 2) + 1);

        public int ConvertZoneIndexToZonesToDisplayIndex(int zoneIndex, int activeZoneIndex)
        {
            int currentActiveZoneToDisplayIndex = CurrentActiveZoneToDisplayIndex;
            if(zoneIndex == currentActiveZoneToDisplayIndex)
                return currentActiveZoneToDisplayIndex;
            else if (zoneIndex > activeZoneIndex)
                return currentActiveZoneToDisplayIndex + (zoneIndex - activeZoneIndex);
            else //if(zoneIndex < currentActiveZoneIndex)
                return currentActiveZoneToDisplayIndex - (zoneIndex - activeZoneIndex);
        }



        public IEnumerator Cleanup()
        {
            yield return null;
        }

        public IEnumerator InitializeZones(ZoneMapInstance zoneMapInstance, int activeZoneIndex = 0)
        {
            CanvasPointDivider.DivideHorizontally(_canvas.GetComponent<RectTransform>(), _numberOfZonesToDisplay,out zoneDisplayPositions,out zoneDisplaySpacing,true);
            

            //group of zone instances we want to display
            int leftMostZoneDisplayIndex = Mathf.Max(0, activeZoneIndex - MaxOffset);
            int rightMostZoneDisplayIndex = zoneMapInstance.MaxProgression > 0 ? Mathf.Min(zoneMapInstance.MaxProgression,activeZoneIndex + MaxOffset ) : activeZoneIndex + MaxOffset;

            List<IEnumerator> initZoneDisplayRoutines = new List<IEnumerator>();
            
           // yield return WaitForAll()
            
            for (int i = leftMostZoneDisplayIndex; i < rightMostZoneDisplayIndex; i++)
            {
                int zoneDisplayIndex = ConvertZoneIndexToZonesToDisplayIndex(i, activeZoneIndex);
                ZoneDisplayComponent zoneDisplay = AddDisplayZone(zoneDisplayIndex);
                zoneDisplays.Add(zoneDisplayIndex, zoneDisplay);
                //instead of yield return zoneDisplay.Initialize(zones[i],zoneDisplayIndex);
                initZoneDisplayRoutines.Add(zoneDisplay.Initialize(zoneMapInstance.GetZoneInstance(i)));
                
                
            }
            yield return this.WaitForAll(initZoneDisplayRoutines);
            
            
            
            
        }

        public ZoneDisplayComponent AddDisplayZone(int zoneDisplayIndex)
        {
            GameObject zoneDisplayGameObject = Instantiate(_zoneDisplayPrefab, _zoneDisplayParent);
            zoneDisplayGameObject.name = _zoneDisplayPrefab.name +"_"+_zoneDisplayParent.childCount.ToString();
            RectTransform zoneDisplayRectTransform = zoneDisplayGameObject.GetComponent<RectTransform>();
                
            zoneDisplayRectTransform.anchoredPosition = zoneDisplayPositions[zoneDisplayIndex];
            return zoneDisplayGameObject.GetComponent<ZoneDisplayComponent>();
        }

        public IEnumerator MoveZonesNextByOne(ZoneMapInstance zoneMapInstance)
        {
            Sequence parallelSequence = DOTween.Sequence();
            int lastActiveZoneIndex = GetActiveZoneDisplay().ZoneInstance.ZoneIndex; //error at 4
            int newActiveZoneIndex = lastActiveZoneIndex + 1; // at 5   
            
            bool leavingZoneIsReplacing = false;
            for (int i = 0; i < _numberOfZonesToDisplay + 2; i++)
            {
                if (zoneDisplays.ContainsKey(i))
                {
                    if (i == 0)
                    {
                        //move cached 
                        zoneDisplays[i].GetComponent<RectTransform>().anchoredPosition = zoneDisplayPositions[_numberOfZonesToDisplay+1];
                        yield return zoneDisplays[i].Initialize(zoneMapInstance.GetZoneInstance(lastActiveZoneIndex + MaxOffset ));
                        
                        parallelSequence.Join(zoneDisplays[i].GetComponent<RectTransform>()
                            .DOAnchorPos(zoneDisplayPositions[_numberOfZonesToDisplay], 1f).SetEase(Ease.OutBounce));  
                        
                        leavingZoneIsReplacing = true;
                    }
                    else
                    {
                        parallelSequence.Join(zoneDisplays[i].GetComponent<RectTransform>()
                            .DOAnchorPos(zoneDisplayPositions[i -1], 1f).SetEase(Ease.OutBounce));    
                    }
                }
                else if (i - 1 > CurrentActiveZoneToDisplayIndex)
                {
                    if (!leavingZoneIsReplacing)
                    {
                        ZoneDisplayComponent zoneDisplay = AddDisplayZone(i);
                        yield return zoneDisplay.Initialize(zoneMapInstance.GetZoneInstance(zoneMapInstance.ActiveZoneIndex + (i - CurrentActiveZoneToDisplayIndex - 1)));
                        zoneDisplays.Add(0, zoneDisplay);
                        parallelSequence.Join(zoneDisplays[0].GetComponent<RectTransform>()
                            .DOAnchorPos(zoneDisplayPositions[i -1], 1f).SetEase(Ease.OutBounce));    
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


            

            //update zoneDisplays so all zone displays match their key index with their position on zoneDisplayPositions
            
            


        }
        

        public ZoneDisplayComponent GetActiveZoneDisplay()
        {
            int activeZoneDisplayIndex = CurrentActiveZoneToDisplayIndex;
            if (activeZoneDisplayIndex < 0 || activeZoneDisplayIndex >= _numberOfZonesToDisplay)
                return null;
            if (!zoneDisplays.ContainsKey(CurrentActiveZoneToDisplayIndex))
                return null;
            return zoneDisplays[CurrentActiveZoneToDisplayIndex];
        }
        

        public IEnumerator DisplayRewardOnActiveZone()
        {
            yield return zoneDisplays[CurrentActiveZoneToDisplayIndex].DisplayFinalReward();
        }

        // public IEnumerator UpdateActiveZone(int newZoneIndex)
        // {
        //     if(newZoneIndex < 0 || newZoneIndex >= zoneDisplays.Count)
        //         return null;
        //     
        //     
        //     
        //     
        // }

     
        
        
    }
}