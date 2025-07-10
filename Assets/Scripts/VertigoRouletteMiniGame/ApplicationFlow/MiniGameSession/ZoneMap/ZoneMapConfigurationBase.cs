using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VertigoRouletteMiniGame.ApplicationFlow.RouletteZoneMap;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap
{
    
    public abstract class ZoneMapConfigurationBase : ScriptableObject
    {
        
        public virtual void ProvideZoneConfiguration(int currentProgression, int maxProgression,
            ref List<ZoneConfiguration> zoneConfigurations)
        {
            
        }

    }
}