using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Cultist_ChaseState : AI_Cultist_StateBase
{
    //Trash code, dont use
    //Incomplete

    public Vector3 relativePoint;

    [SerializeField] float attackDistance;
    public float distanceToPlayer;

    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        thisParentTransform = transform.parent;
        spriteRenderer = thisParentTransform.Find("Sprite").GetComponent<SpriteRenderer>();
        stateMachine.health.onHealthChange += (int health) => CheckHealth(health);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //distanceToPlayer = Vector3.Distance(thisParentTransform.position, stateMachine.playerCharacter.transform.position);

        agentThis.SetDestination(stateMachine.playerCharacter.transform.position);

        relativePoint = thisParentTransform.InverseTransformPoint(stateMachine.playerCharacter.transform.position);

        if (relativePoint.x < 0f && Mathf.Abs(relativePoint.x) > Mathf.Abs(relativePoint.z))
        {
            spriteRenderer.flipX = false;
        }
        if (relativePoint.x > 0f && Mathf.Abs(relativePoint.x) > Mathf.Abs(relativePoint.z))
        {
            spriteRenderer.flipX = true;
        }
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
