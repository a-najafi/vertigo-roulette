using System;
using System.Collections;
using BasicSM;
using UnityEngine;

namespace VertigoRouletteMiniGame.ApplicationFlow.PlayerSession.StateMachine
{
    public class PlayerSessionStateMachine : StateMachineComponentBase
    {
        [SerializeField,ReadOnly]private PlayerSessionComponent _playerSessionComponent;
        
        public PlayerSessionComponent PlayerSession => _playerSessionComponent;
#if UNITY_EDITOR

        private void OnValidate()
        {
            _playerSessionComponent = GetComponent<PlayerSessionComponent>();
        }

#endif
    }
}