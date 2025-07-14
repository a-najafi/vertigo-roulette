using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VertigoRouletteMiniGame.ApplicationFlow.RouletteZoneMap;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap
{
    
    public abstract class ZoneMapConfigurationBase : ScriptableObject
    {
        public virtual void GetZoneConfigurations(int index, int count,
            out List<ZoneConfiguration> zoneConfigurations)
        {
            throw new System.NotImplementedException();
        }

        public virtual ZoneConfiguration GetZoneConfiguration(int index)
        {
            throw new System.NotImplementedException();
        }

    }
}