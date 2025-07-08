using System;
using UnityEngine;
using VertigoRouletteMiniGame.ApplicationFlow.PlayerSession.Inventory;

namespace VertigoRouletteMiniGame.ApplicationFlow.PlayerSession
{
    [Serializable]
    public class PlayerSession : IPlayerSession
    {
        [SerializeField,ReadOnly]private string name = "DefaultPlayerSession";
        [SerializeField,ReadOnly]private PlayerInventory inventory = new PlayerInventory();
        
        public string Name => name;
        public PlayerInventory Inventory => inventory;
        
        
    }
}