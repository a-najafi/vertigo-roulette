using UnityEngine;
using VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession.ZoneMap;
using VertigoRouletteMiniGame.ApplicationFlow.PlayerSession.Inventory;

namespace VertigoRouletteMiniGame.ApplicationFlow.MiniGameSession
{
    public enum EVictoryStatus
    {
        None = 0,
        LeaveWithPrize = 1,
        LoseEverything = 2,
    }
    public class MiniGameSessionComponent : MonoBehaviour
    {
        private PlayerInventory obtainedRewardInventory = new PlayerInventory();
        private ZoneMapInstance zoneMap;
        private EVictoryStatus status = EVictoryStatus.None;
        
        
        
    }
}