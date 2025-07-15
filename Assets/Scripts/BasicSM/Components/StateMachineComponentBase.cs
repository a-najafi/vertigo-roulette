using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.PropertyAttributes;

namespace BasicSM
{
    /// <summary>
    /// Component representing a state machine, managing states and transitions.
    /// </summary>
    public class StateMachineComponentBase : MonoBehaviour, IStateMachine
    {
        #region Serialized Parameters

        [SerializeField]
        private bool _isPersistent = false;

        [SerializeField]
        private bool _startMachineOnStart = false;

        [SerializeField]
        private List<StateConfigurationBase> _serializedStateConfigurations = new List<StateConfigurationBase>();

        [SerializeField, ReadOnly]
        private int currentStateIndex = 0;

        #endregion

        #region Non Serialized Parameters

        private List<IStateConfiguration> stateConfigurations = null;
        private Dictionary<IState, int> cachedStateIndexes = new Dictionary<IState, int>();
        private bool isInTransition = false;
        private bool isInitialized = false;

        #endregion

        #region Properties

        /// <summary>
        /// List of all state configurations in this state machine.
        /// </summary>
        public List<IStateConfiguration> States
        {
            get
            {
                if (stateConfigurations == null)
                {
                    stateConfigurations = new List<IStateConfiguration>();
                    int stateConfigurationCount = _serializedStateConfigurations.Count;
                    for (int i = 0; i < stateConfigurationCount; i++)
                    {
                        stateConfigurations.Add(_serializedStateConfigurations[i]);
                    }
                }
                return stateConfigurations;
            }
        }

        /// <summary>
        /// The currently active state configuration.
        /// </summary>
        public IStateConfiguration CurrentState => States[currentStateIndex];

        #endregion

        #region Unity Methods

        /// <summary>
        /// Unity Start coroutine. Initializes the state machine if configured to do so.
        /// </summary>
        private IEnumerator Start()
        {
            if (_startMachineOnStart)
                yield return Initialize(null);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Retrieves the index of the given state, caching the result for future lookups.
        /// </summary>
        /// <param name="state">The state to look up.</param>
        /// <returns>The index of the state, or -1 if not found.</returns>
        public int GetStateIndex(IState state)
        {
            if (cachedStateIndexes.TryGetValue(state, out var index))
                return index;

            int stateConfigurationCount = States.Count;
            for (int i = 0; i < stateConfigurationCount; i++)
            {
                if (States[i].State == state)
                {
                    cachedStateIndexes.Add(state, i);
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Initializes the state machine, setting up persistence and starting coroutines.
        /// </summary>
        /// <param name="stateMachine">The parent state machine, if any.</param>
        public virtual IEnumerator Initialize(IStateMachine stateMachine)
        {
            if (_isPersistent)
                DontDestroyOnLoad(gameObject);

            currentStateIndex = 0;
            yield return TransitionState(-1, currentStateIndex);

            isInitialized = true;

            StartCoroutine(UpdateCoroutine());
            StartCoroutine(FixedUpdateCoroutine());
        }

        /// <summary>
        /// Terminates the state machine, cleaning up all states.
        /// </summary>
        public IEnumerator Terminate()
        {
            if (!isInitialized)
                yield return null;

            while (isInTransition)
            {
                yield return new WaitForEndOfFrame();
            }

            if (_startMachineOnStart)
                StopAllCoroutines();

            int stateConfigurationsCount = stateConfigurations.Count;
            for (int i = 0; i < stateConfigurationsCount; i++)
            {
                yield return States[i].State.OnCleanUp(this);
            }

            isInitialized = false;
        }

        /// <summary>
        /// Handles the logic for transitioning between states.
        /// </summary>
        /// <param name="fromStateIndex">The index of the state being exited.</param>
        /// <param name="toStateIndex">The index of the state being entered.</param>
        public IEnumerator TransitionState(int fromStateIndex, int toStateIndex)
        {
            isInTransition = true;
            List<IStateTransition> transitions = null;
            int transitionCount = 0;

            if (fromStateIndex > 0)
            {
                IState from = States[fromStateIndex].State;
                yield return from.OnExit(this);

                transitions = States[fromStateIndex].Transitions;
                transitionCount = transitions.Count;
                for (int i = 0; i < transitionCount; i++)
                {
                    yield return transitions[i].CleanUp();
                }
            }

            IState to = States[toStateIndex].State;
            currentStateIndex = toStateIndex;
            yield return to.OnEnter(this);

            transitions = States[toStateIndex].Transitions;
            transitionCount = transitions.Count;
            for (int i = 0; i < transitionCount; i++)
            {
                yield return transitions[i].Initialize(this);
            }

            isInTransition = false;
        }

        /// <summary>
        /// Coroutine that continually updates the state machine.
        /// </summary>
        public IEnumerator UpdateCoroutine()
        {
            while (true)
            {
                if (isInTransition)
                    yield return null;
                else
                {
                    yield return new WaitForEndOfFrame();
                    yield return OnUpdate();
                }
            }
        }

        /// <summary>
        /// Coroutine that continually handles FixedUpdate logic.
        /// </summary>
        public IEnumerator FixedUpdateCoroutine()
        {
            while (true)
            {
                if (isInTransition)
                    yield return null;
                else
                {
                    yield return new WaitForFixedUpdate();
                    yield return OnFixedUpdate();
                }
            }
        }

        /// <summary>
        /// Called every frame by UpdateCoroutine, processes state logic and handles transitions.
        /// </summary>
        public virtual IEnumerator OnUpdate()
        {
            yield return States[currentStateIndex].State.OnUpdate(this);

            int stateTransitionCount = States[currentStateIndex].Transitions.Count;
            if (stateTransitionCount != States[currentStateIndex].Transitions.Count)
            {
                Debug.Log("WTF");
            }
            for (int i = 0; i < stateTransitionCount; i++)
            {
                if (currentStateIndex >= States.Count)
                {
                    Debug.Log("Transition Failed: index out of range states.");
                }
                else if (i >= States[currentStateIndex].Transitions.Count)
                {
                    Debug.Log("Transition Failed: index out of range transitions.");
                }
                if (States[currentStateIndex].Transitions[i].ShouldTransition(this))
                {
                    int targetStateIndex = GetStateIndex(States[currentStateIndex].Transitions[i].TargetState);
                    if (targetStateIndex >= 0)
                    {
                        yield return TransitionState(currentStateIndex, targetStateIndex);
                        yield break;
                    }
                }
            }
        }

        /// <summary>
        /// Called every fixed frame by FixedUpdateCoroutine, processes physics logic for the current state.
        /// </summary>
        public IEnumerator OnFixedUpdate()
        {
            yield return States[currentStateIndex].State.OnFixedUpdate(this);
        }

        #endregion
    }
}
