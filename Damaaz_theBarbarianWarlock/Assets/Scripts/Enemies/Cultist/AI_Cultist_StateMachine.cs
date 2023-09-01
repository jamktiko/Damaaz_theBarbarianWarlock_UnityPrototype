using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AI_Cultist_StateMachine : MonoBehaviour
{
    //This class holds information of the states, spawns them, and switches between them.
    //States are otherwise empty prefabs with a state script on it. Each state is its own prefab.

    //names for different states stored in an enum.

    public enum States
    {
        dead,
        chase,
        attack,
        damaged
    }

    //assinged in editor to determine which state this statemachine should be on when loaded.

    [SerializeField] States initialState;

    //Creating a dictionary from the enum states and AI_Cultist_StateBase which all the states, like AI_Cultist_DeadState inherit.

    Dictionary<States, AI_Cultist_StateBase> statesDic = new Dictionary<States, AI_Cultist_StateBase>();

    MonoBehaviour currentState;

    //Drag in editor the correct prefabs of the states to their places.

    [SerializeField] AI_Cultist_DeadState deadState;
    [SerializeField] AI_Cultist_ChaseState chaseState;
    [SerializeField] AI_Cultist_AttackState attackState;
    [SerializeField] AI_Cultist_DamagedState damagedState;

    //thisTransfrom used by states for keeping track of the parent and also used for spawning

    public Transform thisTransfrom;

    //playerCharacter used by states so they don't have to find and store this themselves.

    public GameObject playerCharacter;

    //health used by states so they don't have to find and store this themselves.

    public Cultist_Health health;

    public NavMeshAgent agent;

    void Awake()
    {
        playerCharacter = GameObject.Find("Player").transform.Find("Player_Character").gameObject;
        health = GetComponent<Cultist_Health>();
        agent = GetComponent<NavMeshAgent>();
        thisTransfrom = GetComponent<Transform>();

        //making dictionary enteries with the enum as the key. Also in this stage, we instantiate all the class prefabs and as a child of this object.

        statesDic.Add(States.dead, Instantiate(deadState, thisTransfrom));
        statesDic.Add(States.chase, Instantiate(chaseState, thisTransfrom));
        statesDic.Add(States.attack, Instantiate(attackState, thisTransfrom));
        statesDic.Add(States.damaged, Instantiate(damagedState, thisTransfrom));

        //Telling the states who is their owner. Need to do this in a loop.

        foreach (var state in statesDic)
        {
            state.Value.stateMachine = this;
            state.Value.thisParentTransform = this.gameObject.transform;
            state.Value.agentThis = agent;
        }

        Invoke("LateStart", 0.1f);
    }

    void LateStart()
    {
        //Setting all states off so they don't run.

        foreach (var state in statesDic)
        {
            state.Value.gameObject.SetActive(false);
        }

        //Setting only the current state, which at awake is the initialState, active.

        currentState = statesDic[initialState];
        currentState.gameObject.SetActive(true);
    }

    public void ChangeState(States state)
    {
        //Used by the states to switch from state to state.
        if (statesDic[state] != currentState)
        {
            currentState.gameObject.SetActive(false);
            currentState = statesDic[state];
            currentState.gameObject.SetActive(true);
        }
    }
}
