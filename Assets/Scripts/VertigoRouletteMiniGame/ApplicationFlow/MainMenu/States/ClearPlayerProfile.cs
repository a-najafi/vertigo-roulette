using System.Collections;
using BasicSM;
using BasicSMExtensions.States;
using UnityEngine;
using VertigoRouletteMiniGame.ApplicationFlow.Inventory.UI;
using VertigoRouletteMiniGame.ApplicationFlow.PlayerSession;

namespace VertigoRouletteMiniGame.ApplicationFlow.MainMenu.States
{
    public class ClearPlayerProfile : EmptyState
    {


        public override IEnumerator OnEnter(IStateMachine stateMachine)
        {


            PlayerSessionComponent playerSession = FindObjectOfType<PlayerSessionComponent>();
            playerSession.Session.Inventory.ClearInventory();
            yield return PlayerSession.PlayerSession.SavePlayerSession(playerSession.Session, SavePaths.PlayerSession);
            yield break;
        }

        public override IEnumerator OnExit(IStateMachine stateMachine)
        {
            yield break;
        }
    }
}