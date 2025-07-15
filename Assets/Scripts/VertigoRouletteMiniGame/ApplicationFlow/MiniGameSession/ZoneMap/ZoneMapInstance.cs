using System;
using System.Collections.Generic;
using VertigoRouletteMiniGame.ApplicationFlow.RouletteZoneMap;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap
{
    /// <summary>
    /// Manages a dynamic list of ZoneInstances, based on a ZoneMapConfiguration.
    /// Handles progression logic and zone instantiation on demand.
    /// </summary>
    public class ZoneMapInstance
    {
        #region Non-Serialized Fields

        private ZoneMapConfigurationBase zoneMapConfiguration;
        private List<ZoneInstance> zoneInstances = new List<ZoneInstance>();
        private int activeZoneIndex = 0;
        private int maxProgression = -1;

        #endregion

        #region Properties

        /// <summary>
        /// The highest progression index allowed for this map (-1 means unlimited).
        /// </summary>
        public int MaxProgression => maxProgression;

        /// <summary>
        /// The currently active zone index.
        /// </summary>
        public int ActiveZoneIndex => activeZoneIndex;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a ZoneMapInstance from configuration and an optional max progression.
        /// </summary>
        public ZoneMapInstance(ZoneMapConfigurationBase zoneMapConfiguration, int maxProgression = -1)
        {
            this.zoneMapConfiguration = zoneMapConfiguration;
            this.maxProgression = maxProgression;
        }

        /// <summary>
        /// Create a ZoneMapInstance from a predefined list of ZoneInstances.
        /// </summary>
        public ZoneMapInstance(List<ZoneInstance> zoneInstances)
        {
            this.zoneInstances = zoneInstances;
            activeZoneIndex = 0;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Advances to the next zone if the current active zone has been won.
        /// Returns true if the active zone advanced.
        /// </summary>
        public bool TryMakeNextZoneActive()
        {
            if (zoneInstances[activeZoneIndex].ZoneState != EZoneState.Win)
                return false;

            int newActiveZoneIndex = activeZoneIndex + 1;

            // If we've reached beyond the known zones, try to instantiate more
            if (newActiveZoneIndex >= zoneInstances.Count)
            {
                ZoneInstance _ = GetZoneInstance(newActiveZoneIndex);
            }

            activeZoneIndex = newActiveZoneIndex;
            return true;
        }

        /// <summary>
        /// Returns the currently active ZoneInstance.
        /// </summary>
        public ZoneInstance GetActiveZoneInstance()
        {
            return GetZoneInstance(activeZoneIndex);
        }

        /// <summary>
        /// Gets the ZoneInstance at a specific index, creating it if necessary.
        /// </summary>
        public ZoneInstance GetZoneInstance(int zoneIndex)
        {
            if (maxProgression > 0 && zoneIndex > maxProgression)
                return null;

            // Instantiate additional ZoneInstances if requested index is beyond current count
            if (zoneIndex >= zoneInstances.Count)
            {
                zoneMapConfiguration.GetZoneConfigurations(
                    zoneIndex, 
                    zoneIndex - zoneInstances.Count + 1, 
                    out List<ZoneConfiguration> zoneConfigurations);

                for (int i = 0; i < zoneConfigurations.Count; i++)
                {
                    ZoneInstance zoneInstance = new ZoneInstance(zoneInstances.Count + i, zoneConfigurations[i]);
                    zoneInstances.Add(zoneInstance);
                }
            }
            return zoneInstances[zoneIndex];
        }

        #endregion
    }
}
