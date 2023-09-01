using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Cultist_ChaseState : AI_Cultist_StateBase
{
    //Trash code, dont use
    //Incomplete

    [SerializeField] float attackDistance;
    public float distanceToPlayer;

    // Start is called before the first frame update
    void Start()
    {
        thisParentTransform = transform.parent;
        stateMachine.health.onHealthChange += (int health) => CheckHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        //distanceToPlayer = Vector3.Distance(thisParentTransform.position, stateMachine.playerCharacter.transform.position);

        agentThis.SetDestination(stateMachine.playerCharacter.transform.position);
    }

    void CheckHealth(int health)
    {

        if (health <= 0)
        {
            ChangeState(dead);
        }
        else
        {
            ChangeState(damaged);
        }
    }
}
