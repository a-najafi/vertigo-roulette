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
        
        
        public override void GetZoneConfigurations(int index, int count,
            out List<ZoneConfiguration> zoneConfigurations)
        {
            zoneConfigurations = new List<ZoneConfiguration>();
            if (_loop)
            {
                int loopedCount = _zoneConfigurations.Count;
                for (int i = index; i < index + count; i++)
                {
                    int nonLoopIndex = i % _zoneConfigurations.Count;
                    zoneConfigurations.Add(_zoneConfigurations[nonLoopIndex]);
                }
            }
            else if(index < _zoneConfigurations.Count)
            {
                count = Mathf.Min(index + count, _zoneConfigurations.Count);
                for (int i = index; i < index + count; i++)
                {
                    zoneConfigurations.Add(_zoneConfigurations[i]);
                }
            }
            else
                throw new System.Exception("Provided progression is out of range of this zone map. ");
        }

        public override ZoneConfiguration GetZoneConfiguration(int index)
        {
            if (_loop)
            {
                int nonLoopIndex = index % _zoneConfigurations.Count;
                return _zoneConfigurations[nonLoopIndex];
            }
            else if(index < _zoneConfigurations.Count)
            {
                return _zoneConfigurations[index];
            }
            else
                throw new System.Exception("Provided progression is out of range of this zone map. ");
        }

        
    }
}