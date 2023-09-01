using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;

public class AI_Cultist_DamagedState : AI_Cultist_StateBase
{
    //Trash code, dont use
    //Incomplete

    [SerializeField] float knockBackAmount = 0.1f;
    [SerializeField] float knockBackTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        stateMachine.health.onHealthChange += (int health) => CheckHealth(health);
    }

    private void OnEnable()
    {
        if (agentThis != null)
        {
            agentThis.Move((stateMachine.thisTransfrom.position - stateMachine.playerCharacter.transform.position).normalized * knockBackAmount);

            StartCoroutine(KnockBack());
        }
    }

    IEnumerator KnockBack()
    {
        float currentTime = 0f;

        while (knockBackTime >= currentTime) 
        {
            currentTime += Time.deltaTime;

            agentThis.Move((thisParentTransform.position - stateMachine.playerCharacter.transform.position).normalized * knockBackAmount);

            yield return null;
        }

        ChangeState(chase);
    }

    void CheckHealth(int health)
    {
        if (health <= 0)
        {
            ChangeState(dead);
        }
    }
}
