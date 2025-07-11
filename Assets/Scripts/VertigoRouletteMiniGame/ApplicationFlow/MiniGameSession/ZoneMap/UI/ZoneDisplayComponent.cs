using System.Collections;
using UnityEngine;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap.UI
{
    public class ZoneDisplayComponent : MonoBehaviour
    {
        
        private int zoneDisplayIndex;
        private ZoneInstance zoneInstance;

        public virtual IEnumerator Initialize(ZoneInstance zoneInstance, int zoneDisplayIndex)
        {
            this.zoneDisplayIndex = zoneDisplayIndex;
            this.zoneInstance = zoneInstance;
            yield return null;
        }
    }
}