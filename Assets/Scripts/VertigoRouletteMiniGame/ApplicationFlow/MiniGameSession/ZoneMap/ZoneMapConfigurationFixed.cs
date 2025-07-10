using System.Collections.Generic;
using UnityEngine;
using VertigoRouletteMiniGame.ApplicationFlow.RouletteZoneMap;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap
{
    public class ZoneMapConfigurationFixed : ZoneMapConfigurationBase
    {
        [SerializeField] private bool _loop = true;
        [SerializeField]private List<ZoneConfiguration> _zoneConfigurations;
        
        public override void ProvideZoneConfiguration(int currentProgression, int maxProgression,
            ref List<ZoneConfiguration> zoneConfigurations)
        {
            if (currentProgression > maxProgression)
                throw new System.Exception("Provided progression is out of range.");
            
            if (_loop)
            {
                int loopedCount = _zoneConfigurations.Count;
                int nonLoopIndex = currentProgression % _zoneConfigurations.Count;

                for (int i = nonLoopIndex; i < loopedCount; i++)
                {
                    zoneConfigurations.Add(_zoneConfigurations[i]);
                }
            }
            else if(currentProgression < _zoneConfigurations.Count)
            {
                maxProgression = Mathf.Min(maxProgression, _zoneConfigurations.Count);
                for (int i = currentProgression; i < maxProgression; i++)
                {
                    zoneConfigurations.Add(_zoneConfigurations[i]);
                }
            }
            else
                throw new System.Exception("Provided progression is out of range of this zone map. ");
        }
    }
}