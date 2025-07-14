using System;
using System.Collections.Generic;
using UnityEngine;
using VertigoRouletteMiniGame.ApplicationFlow.RouletteZoneMap;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap
{

    //most common HoppingZone would use _hoppingIndex = 1
    //super special would be 30

    [Serializable]
    public struct HoppingZone
    {
        [SerializeField] private int _hoppingIndex;
        [SerializeField] private ZoneConfiguration _zoneConfiguration;
        
        public int HoppingIndex => _hoppingIndex;

        public ZoneConfiguration ZoneConfiguration => _zoneConfiguration;

        
    }
    
    
    
    [CreateAssetMenu(fileName = "ZoneMapConfigurationHopping", menuName = "MiniGame/ZoneMapConfigurationHopping")]
    public class ZoneMapConfigurationHopping : ZoneMapConfigurationBase
    {
        [SerializeField]private List<HoppingZone> _hoppingZoneConfigurations;


        public override void GetZoneConfigurations(int index, int count,
            out List<ZoneConfiguration> zoneConfigurations)
        {
            
            zoneConfigurations = new List<ZoneConfiguration>();
            
            _hoppingZoneConfigurations.Sort(CompareHoppingZones);

            for (int i = index; i < index + count ; i++)
            {
                if (i == 0)
                {
                    zoneConfigurations.Add(_hoppingZoneConfigurations[0].ZoneConfiguration);
                    continue;
                }
                else
                {
                    bool foundZoneConfiguration = false;
                    for (int j = _hoppingZoneConfigurations.Count - 1; j >= 0; j--)
                    {
                        if (i % _hoppingZoneConfigurations[j].HoppingIndex == 0)
                        {
                            zoneConfigurations.Add(_hoppingZoneConfigurations[j].ZoneConfiguration);
                            foundZoneConfiguration = true;
                            break;
                        }
                    }    
                    if(!foundZoneConfiguration)
                        throw new Exception("Current Hopping Zone Configuration is not supported. To fix this issue easily add hopping zone with hopping index of 1 as default.");
                }
                
            }
        }
        

        public override ZoneConfiguration GetZoneConfiguration(int index)
        {
            if (index == 0)
                return _hoppingZoneConfigurations[0].ZoneConfiguration;
            else
            {
                for (int j = _hoppingZoneConfigurations.Count - 1; j >= 0; j--)
                {
                    if (index % _hoppingZoneConfigurations[j].HoppingIndex == 0)
                    {
                        return _hoppingZoneConfigurations[j].ZoneConfiguration;
                    }
                }    
                throw new Exception("Current Hopping Zone Configuration is not supported. To fix this issue easily add hopping zone with hopping index of 1 as default.");

            }
        }

        private int CompareHoppingZones(HoppingZone x, HoppingZone y)
        {
            return x.HoppingIndex - y.HoppingIndex;
        }
    }
}
