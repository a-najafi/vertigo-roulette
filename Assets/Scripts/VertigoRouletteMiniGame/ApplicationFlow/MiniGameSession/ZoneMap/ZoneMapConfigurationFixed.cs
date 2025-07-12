using System.Collections.Generic;
using UnityEngine;
using VertigoRouletteMiniGame.ApplicationFlow.RouletteZoneMap;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap
{
    [CreateAssetMenu(fileName = "ZoneMapConfigurationFixed", menuName = "MiniGame/ZoneMapConfigurationFixed")]
    public class ZoneMapConfigurationFixed : ZoneMapConfigurationBase
    {
        [SerializeField] private bool _loop = true;
        [SerializeField]private List<ZoneConfiguration> _zoneConfigurations;
        
        public override void ProvideZoneConfiguration(int currentProgression, int maxProgression,
            out List<ZoneConfiguration> zoneConfigurations)
        {
            
            if (maxProgression > 0 && currentProgression > maxProgression)
                throw new System.Exception("Provided progression is out of range.");
            zoneConfigurations = new List<ZoneConfiguration>();
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
                if (maxProgression > 0)
                    maxProgression = Mathf.Min(maxProgression, _zoneConfigurations.Count);
                else
                    maxProgression = _zoneConfigurations.Count;
                
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