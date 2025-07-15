using System.Collections.Generic;
using UnityEngine;
using VertigoRouletteMiniGame.ApplicationFlow.RouletteZoneMap;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap
{
    /// <summary>
    /// Provides a fixed set of zone configurations. Can loop when more zones are requested than available.
    /// </summary>
    [CreateAssetMenu(fileName = "ZoneMapConfigurationFixed", menuName = "MiniGame/ZoneMapConfigurationFixed")]
    public class ZoneMapConfigurationFixed : ZoneMapConfigurationBase
    {
        #region Serialized Parameters

        [SerializeField] private bool _loop = true;
        [SerializeField] private List<ZoneConfiguration> _zoneConfigurations;

        #endregion

        #region Public Methods

        /// <summary>
        /// Retrieves a sequence of zone configurations, optionally looping if requested.
        /// </summary>
        /// <param name="index">Start index in the configuration list.</param>
        /// <param name="count">How many configurations to fetch.</param>
        /// <param name="zoneConfigurations">Output list of ZoneConfiguration assets.</param>
        public override void GetZoneConfigurations(int index, int count, out List<ZoneConfiguration> zoneConfigurations)
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
            else if (index < _zoneConfigurations.Count)
            {
                count = Mathf.Min(index + count, _zoneConfigurations.Count);
                for (int i = index; i < index + count; i++)
                {
                    zoneConfigurations.Add(_zoneConfigurations[i]);
                }
            }
            else
            {
                throw new System.Exception("Provided progression is out of range of this zone map.");
            }
        }

        /// <summary>
        /// Retrieves a single zone configuration at the given index, supporting looping if enabled.
        /// </summary>
        /// <param name="index">Index in the configuration list.</param>
        /// <returns>The ZoneConfiguration at the specified index.</returns>
        public override ZoneConfiguration GetZoneConfiguration(int index)
        {
            if (_loop)
            {
                int nonLoopIndex = index % _zoneConfigurations.Count;
                return _zoneConfigurations[nonLoopIndex];
            }
            else if (index < _zoneConfigurations.Count)
            {
                return _zoneConfigurations[index];
            }
            else
            {
                throw new System.Exception("Provided progression is out of range of this zone map.");
            }
        }

        #endregion
    }
}
