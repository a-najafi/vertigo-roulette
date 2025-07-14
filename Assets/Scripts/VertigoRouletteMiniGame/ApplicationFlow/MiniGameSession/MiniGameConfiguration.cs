using UnityEngine;
using UnityEngine.AddressableAssets;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession
{
    [CreateAssetMenu(fileName = "MiniGameConfiguration", menuName = "MiniGame/MiniGameConfiguration")]
    public class MiniGameConfiguration : ScriptableObject
    {
        [SerializeField] private AssetReference _zoneMapConfigurationAssetReference;
        [SerializeField] private int _maximumProgression = -1;

        public int MaximumProgression => _maximumProgression;

        public AssetReference ZoneMapConfiguration => _zoneMapConfigurationAssetReference;
    }
}