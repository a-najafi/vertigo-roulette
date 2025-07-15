using System.Collections;
using System.Collections.Generic;
using BasicSM;
using UnityEngine;
using UnityEngine.Serialization;

namespace BasicSMExtensions.States
{
    public class SimpleEnableState : EmptyState
    {
        [SerializeField] List<MonoBehaviour> _components;
        [SerializeField] private bool _enable = true;

        public override IEnumerator OnEnter(IStateMachine stateMachine)
        {
            yield return base.OnEnter(stateMachine);
            if (_components != null)
            {
                for (int i = 0; i < _components.Count; i++)
                {
                    _components[i].enabled = _enable;    
                }    
            }
            
            
        }

        public override IEnumerator OnExit(IStateMachine stateMachine)
        {
            yield return base.OnExit(stateMachine);
            if (_components != null)
            {
                for (int i = 0; i < _components.Count; i++)
                {
                    _components[i].enabled = !_enable;    
                }    
            }
            
        }
    }
}