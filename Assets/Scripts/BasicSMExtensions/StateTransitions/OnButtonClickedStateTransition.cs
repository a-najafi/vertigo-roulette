using System;
using System.Collections;
using BasicSM;
using UnityEngine;
using UnityEngine.UI;

namespace BasicSMExtensions
{
    [Serializable]
    public class OnButtonClickedStateTransition : StateTransitionBase
    {
        [SerializeField]private Button _button;
        private bool clicked = false;
        
        public override IEnumerator Initialize(IStateMachine stateMachine)
        {
            if(_button == null)
                throw new Exception("OnButtonClickedStateTransition: Button is null");
            _button.onClick.AddListener(OnButtonClicked);
            yield return null;
        }

        private void OnButtonClicked()
        {
            clicked = true;
        }

        public override IEnumerator CleanUp()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
            clicked = false;
            yield return null;
        }

        public override bool ShouldTransition(IStateMachine stateMachine)
        {
            bool temp = clicked;
            clicked = false;
            return temp;
        }
    }
}