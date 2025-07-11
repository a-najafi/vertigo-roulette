using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.RouletteSession.Rewards
{
    
    
    [Serializable]
    public class RouletteRewardConfiguration
    {
        [SerializeField] private AssetReference _itemDefinition;
        [SerializeField] private int _amount = -1;
        [SerializeField] private float _probabilityModifier = 1;
        [SerializeField] private bool _isBomb;
        
        public float ProbabilityModifier => _probabilityModifier;
        public int Amount => _amount;
        public AssetReference ItemDefinition => _itemDefinition;
        public bool IsBomb => _isBomb;
    }
}