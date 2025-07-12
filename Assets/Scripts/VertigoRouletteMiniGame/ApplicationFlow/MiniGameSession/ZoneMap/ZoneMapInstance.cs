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
        
        public List<ZoneInstance> ZoneInstances => zoneInstances;

        public int ActiveZoneIndex => activeZoneIndex;

        public ZoneMapInstance(ZoneMapConfigurationBase zoneMapConfiguration, int maxProgression = -1)
        {
            this.zoneMapConfiguration = zoneMapConfiguration;
            this.maxProgression = maxProgression;
            ResolveZoneInstances();
        }

        protected bool ResolveZoneInstances(int includeFutureZoneNum = 0)
        {
            
            if (maxProgression > 0 && activeZoneIndex >= maxProgression)
                return false;
            
            if (activeZoneIndex + includeFutureZoneNum >= zoneInstances.Count)
            {
                zoneMapConfiguration.ProvideZoneConfiguration(activeZoneIndex,maxProgression,out List<ZoneConfiguration> zoneConfigurations);

                if (zoneConfigurations.Count == 0)
                    return false;
                
                for (int i = 0; i < zoneConfigurations.Count; i++)
                {
                    ZoneInstance zoneInstance = new ZoneInstance(activeZoneIndex + i, zoneConfigurations[i]);
                    zoneInstances.Add(zoneInstance);
                }
            }

            return true;
        }

        public ZoneMapInstance(List<ZoneInstance> zoneInstances)
        {
            this.zoneInstances = zoneInstances;
            activeZoneIndex = 0;
        }

        public bool TryMakeNextZoneActive()
        {
            int newActiveZoneIndex = activeZoneIndex + 1;
            
            if (newActiveZoneIndex < 0 || newActiveZoneIndex >= zoneInstances.Count)
                return false;
            
            if (zoneInstances[activeZoneIndex].ZoneState != EZoneState.Win)
                return false;
            
            activeZoneIndex = newActiveZoneIndex;
            return true;
        }

        

        public ZoneInstance GetActiveZoneInstance()
        {
            return zoneInstances[activeZoneIndex];
        }

        public ZoneInstance GetZoneInstance(int zoneIndex)
        {
            if (zoneIndex >= zoneInstances.Count)
            {
                ResolveZoneInstances(zoneIndex - activeZoneIndex);
            }
            return zoneInstances[zoneIndex];
        }
        


    }
}