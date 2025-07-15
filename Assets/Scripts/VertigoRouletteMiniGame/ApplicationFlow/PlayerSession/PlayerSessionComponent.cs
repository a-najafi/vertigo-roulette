using System.Collections;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using Utility.Addressable;

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