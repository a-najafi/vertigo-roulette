using System;
using System.Collections.Generic;
using UnityEngine;
using VertigoRouletteMiniGame.ApplicationFlow.RouletteZoneMap;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap
{
    /// <summary>
    /// Associates a zone configuration with a hopping index (modulus logic).
    /// Example: A hopping index of 1 applies to all, 30 is "special".
    /// </summary>
    [Serializable]
    public struct HoppingZone
    {
        #region Serialized Parameters

        [SerializeField] private int _hoppingIndex;
        [SerializeField] private ZoneConfiguration _zoneConfiguration;

        #endregion

        #region Properties

        public int HoppingIndex => _hoppingIndex;
        public ZoneConfiguration ZoneConfiguration => _zoneConfiguration;

        #endregion
    }

    /// <summary>
    /// A hopping zone map configuration that selects zone configurations by modulus pattern.
    /// </summary>
    [CreateAssetMenu(fileName = "ZoneMapConfigurationHopping", menuName = "MiniGame/ZoneMapConfigurationHopping")]
    public class ZoneMapConfigurationHopping : ZoneMapConfigurationBase
    {
        #region Serialized Parameters

        [SerializeField] private List<HoppingZone> _hoppingZoneConfigurations;

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets a sequence of zone configurations based on their hopping logic.
        /// </summary>
        /// <param name="index">Starting index in progression.</param>
        /// <param name="count">Number of zones to return.</param>
        /// <param name="zoneConfigurations">Output list of configurations.</param>
        /// <exception cref="Exception">
        /// Throws if no hopping zone with index 1 is set as default fallback.
        /// </exception>
        public override void GetZoneConfigurations(int index, int count, out List<ZoneConfiguration> zoneConfigurations)
        {
            zoneConfigurations = new List<ZoneConfiguration>();

            // Always sort for predictable results
            _hoppingZoneConfigurations.Sort(CompareHoppingZones);

            for (int i = index; i < index + count; i++)
            {
                if (i == 0)
                {
                    // Always start with the first hopping zone
                    zoneConfigurations.Add(_hoppingZoneConfigurations[0].ZoneConfiguration);
                    continue;
                }

                bool foundZoneConfiguration = false;
                // Try more specific zones first (higher hopping index)
                for (int j = _hoppingZoneConfigurations.Count - 1; j >= 0; j--)
                {
                    if (i % _hoppingZoneConfigurations[j].HoppingIndex == 0)
                    {
                        zoneConfigurations.Add(_hoppingZoneConfigurations[j].ZoneConfiguration);
                        foundZoneConfiguration = true;
                        break;
                    }
                }
                if (!foundZoneConfiguration)
                {
                    throw new Exception("No suitable HoppingZone found. To fix, add a hopping zone with hopping index of 1 as default.");
                }
            }
        }

        /// <summary>
        /// Gets a single zone configuration at a given index, using hopping modulus.
        /// </summary>
        /// <param name="index">Progression index.</param>
        /// <returns>ZoneConfiguration at this index.</returns>
        public override ZoneConfiguration GetZoneConfiguration(int index)
        {
            if (index == 0)
                return _hoppingZoneConfigurations[0].ZoneConfiguration;

            for (int j = _hoppingZoneConfigurations.Count - 1; j >= 0; j--)
            {
                if (index % _hoppingZoneConfigurations[j].HoppingIndex == 0)
                    return _hoppingZoneConfigurations[j].ZoneConfiguration;
            }
            throw new Exception("No suitable HoppingZone found. Add a hopping zone with hopping index of 1 as default.");
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Sorts hopping zones by their hopping index ascending.
        /// </summary>
        private int CompareHoppingZones(HoppingZone x, HoppingZone y)
        {
            return x.HoppingIndex - y.HoppingIndex;
        }

        #endregion
    }
}
