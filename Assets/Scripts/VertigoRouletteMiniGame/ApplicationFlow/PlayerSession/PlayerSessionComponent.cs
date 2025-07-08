using UnityEngine;

namespace VertigoRouletteMiniGame.ApplicationFlow.PlayerSession
{
    public class PlayerSessionComponent : MonoBehaviour
    {
        [ReadOnly]private PlayerSession _playerSession;
        
        public PlayerSession Session => _playerSession;

        public void Initialize(PlayerSession playerSession)
        {
            _playerSession = playerSession;
        }
        
    }
}