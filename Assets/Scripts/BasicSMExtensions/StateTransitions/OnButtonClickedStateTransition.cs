using System;
using System.Collections;
using BasicSM;
using UnityEngine;
using UnityEngine.UI;

namespace BasicSMExtensions.StateTransitions
{
    /// <summary>
    /// State transition that triggers when the assigned Unity UI Button is clicked.
    /// </summary>
    [Serializable]
    public class OnButtonClickedStateTransition : StateTransitionBase
    {
        #region Serialized Parameters

        /// <summary>
        /// The UI Button whose click will trigger the state transition.
        /// </summary>
        [SerializeField]
        private Button _button;

        #endregion

        #region Non Serialized Parameters

        /// <summary>
        /// Tracks whether the button was clicked since last transition check.
        /// </summary>
        private bool clicked = false;

        #endregion

        #region Public Methods

        /// <summary>
        /// Subscribes to the button's onClick event when the transition is initialized.
        /// </summary>
        /// <param name="stateMachine">The parent state machine.</param>
        public override IEnumerator Initialize(IStateMachine stateMachine)
        {
            if (_button == null)
                throw new Exception("OnButtonClickedStateTransition: Button is null");
            _button.onClick.AddListener(OnButtonClicked);
            yield return null;
        }

        /// <summary>
        /// Unsubscribes from the button's onClick event and resets the clicked flag.
        /// </summary>
        public override IEnumerator CleanUp()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
            clicked = false;
            yield return null;
        }

        /// <summary>
        /// Returns true if the button was clicked since the last check.
        /// Resets the clicked flag.
        /// </summary>
        /// <param name="stateMachine">The parent state machine.</param>
        /// <returns>True if clicked, otherwise false.</returns>
        public override bool ShouldTransition(IStateMachine stateMachine)
        {
            bool temp = clicked;
            clicked = false;
            return temp;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Callback when the button is clicked, sets the clicked flag.
        /// </summary>
        private void OnButtonClicked()
        {
            clicked = true;
        }

        #endregion
    }
}
