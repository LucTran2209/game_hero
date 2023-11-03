using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBoolBehavior : StateMachineBehaviour
{
    // Start is called before the first frame update
    public string boolName;
    public bool UpdateOnState;
    public bool UpdateOnStateMachine;
    public bool valueOnEnter, valueOnExit;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (UpdateOnState)
        {
            animator.SetBool(boolName, valueOnEnter);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (UpdateOnState)
        {
            animator.SetBool(boolName, valueOnExit);
        }
    }

    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if (UpdateOnStateMachine)
        {
            animator.SetBool(boolName, valueOnEnter);
            Debug.Log("not Move");
        }
    }

    public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if (UpdateOnStateMachine)
        {
            animator.SetBool(boolName, valueOnExit);
            Debug.Log("Move");
        }
    }
}
