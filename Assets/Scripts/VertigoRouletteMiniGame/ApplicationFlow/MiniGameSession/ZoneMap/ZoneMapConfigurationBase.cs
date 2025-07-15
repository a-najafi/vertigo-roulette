using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VertigoRouletteMiniGame.ApplicationFlow.RouletteZoneMap;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap
{
    /// <summary>
    /// Abstract base class for zone map configurations. Used to provide zone data to zone map instances.
    /// </summary>
    public abstract class ZoneMapConfigurationBase : ScriptableObject
    {
        #region Public Methods

        /// <summary>
        /// Retrieves a list of zone configurations starting from the specified index.
        /// </summary>
        /// <param name="index">Start index in the sequence of zones.</param>
        /// <param name="count">Number of zone configurations to retrieve.</param>
        /// <param name="zoneConfigurations">Output list of zone configurations.</param>
        public virtual void GetZoneConfigurations(int index, int count, out List<ZoneConfiguration> zoneConfigurations)
        {
            // Should be overridden by derived classes.
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Returns the zone configuration at a specific index.
        /// </summary>
        /// <param name="index">Index of the zone configuration.</param>
        /// <returns>ZoneConfiguration asset.</returns>
        public virtual ZoneConfiguration GetZoneConfiguration(int index)
        {
            // Should be overridden by derived classes.
            throw new System.NotImplementedException();
        }

        #endregion
    }
}