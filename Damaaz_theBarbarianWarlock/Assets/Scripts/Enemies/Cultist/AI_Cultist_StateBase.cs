using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Cultist_StateBase : MonoBehaviour
{
    //This class is to be inherited by cultist ai states.

    //Owning state machine

    public AI_Cultist_StateMachine stateMachine;
    public Transform thisParentTransform;
    public NavMeshAgent agentThis;

    //States with names for easier switching, for example "ChangeState(chase);".

    private protected AI_Cultist_StateMachine.States dead = AI_Cultist_StateMachine.States.dead;
    private protected AI_Cultist_StateMachine.States chase = AI_Cultist_StateMachine.States.chase;
    private protected AI_Cultist_StateMachine.States attack = AI_Cultist_StateMachine.States.attack;
    private protected AI_Cultist_StateMachine.States damaged = AI_Cultist_StateMachine.States.damaged;

    private protected void ChangeState(AI_Cultist_StateMachine.States state)
    {
        //Used by the states that inherit this class to call a switch from state to state.

        stateMachine.ChangeState(state);
    }


}
