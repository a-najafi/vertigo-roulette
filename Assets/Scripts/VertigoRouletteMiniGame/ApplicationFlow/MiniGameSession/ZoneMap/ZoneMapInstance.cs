using System;
using System.Collections.Generic;
using VertigoRouletteMiniGame.ApplicationFlow.RouletteZoneMap;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap
{
    public class ZoneMapInstance
    {
        private ZoneMapConfigurationBase zoneMapConfiguration;
        
        private List<ZoneInstance> zoneInstances =  new List<ZoneInstance>();

        private int activeZoneIndex = 0;
        private int maxProgression = -1;

        public int MaxProgression => maxProgression;

        public int ActiveZoneIndex => activeZoneIndex;

        public ZoneMapInstance(ZoneMapConfigurationBase zoneMapConfiguration, int maxProgression = -1)
        {
            this.zoneMapConfiguration = zoneMapConfiguration;
            this.maxProgression = maxProgression;
        }

 
        
        public ZoneMapInstance(List<ZoneInstance> zoneInstances)
        {
            this.zoneInstances = zoneInstances;
            activeZoneIndex = 0;
        }

        public bool TryMakeNextZoneActive()
        {
            
            
            if (zoneInstances[activeZoneIndex].ZoneState != EZoneState.Win)
                return false;
            
            int newActiveZoneIndex = activeZoneIndex + 1;
            
            
            if (newActiveZoneIndex >= zoneInstances.Count)
            {
                ZoneInstance zoneInstance = GetZoneInstance(newActiveZoneIndex);
            }
            activeZoneIndex = newActiveZoneIndex;
            return true;
        }

        

        public ZoneInstance GetActiveZoneInstance()
        {
            return GetZoneInstance(activeZoneIndex);
        }

        public ZoneInstance GetZoneInstance(int zoneIndex)
        {
            if(maxProgression > 0 && zoneIndex > maxProgression)
                return null;
            
            if(zoneIndex >= zoneInstances.Count)
            {
                zoneMapConfiguration.GetZoneConfigurations( zoneIndex ,zoneIndex - zoneInstances.Count + 1,out List<ZoneConfiguration> zoneConfigurations);
                for (int i = 0; i < zoneConfigurations.Count; i++)
                {
                    ZoneInstance zoneInstance = new ZoneInstance(zoneInstances.Count + i, zoneConfigurations[i]);
                    zoneInstances.Add(zoneInstance);
                }
            }
            return zoneInstances[zoneIndex];
        }
        
        
        


    }
}