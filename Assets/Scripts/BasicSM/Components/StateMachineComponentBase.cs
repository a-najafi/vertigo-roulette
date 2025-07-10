using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace BasicSM
{ 
    public class StateMachineComponentBase : MonoBehaviour, IStateMachine
    {
        [SerializeField] private bool _isPersistent = false;
        [SerializeField] private bool _startMachineOnStart = false;
        [SerializeField] private List<StateConfigurationBase> _serializedStateConfigurations = new List<StateConfigurationBase>();
        
        private List<IStateConfiguration> stateConfigurations = null;
        private Dictionary<IState, int> cachedStateIndexes = new Dictionary<IState, int>();

        
        private bool isInTransition = false;
        private bool isInitialized = false;
        [SerializeField,ReadOnly]private int currentStateIndex = 0;
        
        
              
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

        public IStateConfiguration CurrentState => States[currentStateIndex];

        private IEnumerator Start()
        {
            if (_startMachineOnStart)
                yield return Initialize(null);
        }

        

      

        public int GetStateIndex(IState state)
        {
            if(cachedStateIndexes.TryGetValue(state, out var index))
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
        
        public virtual IEnumerator Initialize(IStateMachine stateMachine)
        {
            if(_isPersistent)
                DontDestroyOnLoad(gameObject);
            
            currentStateIndex = 0;
            yield return TransitionState(-1,currentStateIndex);

            isInitialized = true;

            StartCoroutine(UpdateCoroutine());
            StartCoroutine(FixedUpdateCoroutine());
        }

        public IEnumerator UpdateCoroutine()
        {
            while (true)
            {
                if(isInTransition)
                    yield return null;
                else
                {
                    yield return new WaitForEndOfFrame();
                    yield return OnUpdate();    
                }
                
            }
        }
        
        public IEnumerator FixedUpdateCoroutine()
        {
            while (true)
            {
                if(isInTransition)
                    yield return null;
                else
                {
                    yield return new WaitForFixedUpdate();
                    yield return OnFixedUpdate();
                }
            }
        }



        public IEnumerator Terminate()
        {

            if (!isInitialized)
                yield return null;

            while (isInTransition)
            {
                yield return new WaitForEndOfFrame();
            }
            
            if(_startMachineOnStart)
                StopAllCoroutines();
            
            int stateConfigurationsCount = stateConfigurations.Count;
            for (int i = 0; i < stateConfigurationsCount; i++)
            {
                yield return States[i].State.OnCleanUp(this);
            }
            
            
            isInitialized = false;
        }
        
       

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
        
        public virtual IEnumerator OnUpdate()
        {
            yield return States[currentStateIndex].State.OnUpdate(this);
            
            int stateTransitionCount = States[currentStateIndex].Transitions.Count;
            for (int i = 0; i < stateTransitionCount; i++)
            {
                if (States[currentStateIndex].Transitions[i].ShouldTransition(this))
                {
                    int targetStateIndex = GetStateIndex(States[currentStateIndex].Transitions[i].TargetState);
                    if(targetStateIndex >= 0)
                        yield return TransitionState(currentStateIndex,targetStateIndex);
                }
            }
        }
        

        public IEnumerator OnFixedUpdate()
        {
            yield return States[currentStateIndex].State.OnFixedUpdate(this);
        }

 
    }
}