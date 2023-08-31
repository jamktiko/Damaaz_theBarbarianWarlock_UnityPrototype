using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;

public class AI_Cultist_DamagedState : AI_Cultist_StateBase
{
    //Incomplete

    NavMeshAgent agent;

    public Transform thisParentTransform;

    [SerializeField] float knockBackForce = 5f;

    // Start is called before the first frame update
    void Awake()
    {

        stateMachine.health.onHealthChange += (int health) => CheckHealth(health);
        thisParentTransform = transform.parent;
        agent = thisParentTransform.GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        Debug.Log(damaged);

        agent.Move((stateMachine.thisTransfrom.position - stateMachine.playerCharacter.transform.position).normalized * knockBackForce);
    }
    

    void CheckHealth(int health)
    {
        if (health <= 0)
        {
            ChangeState(dead);
        }
    }
}
