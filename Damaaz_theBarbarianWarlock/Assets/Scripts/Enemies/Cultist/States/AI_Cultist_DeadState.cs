using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Cultist_DeadState : AI_Cultist_StateBase
{
    //Trash code, dont use
    //Incomplete

    [SerializeField] float knockBackAmount = 0.1f;
    [SerializeField] float knockBackTime = 0.5f;

    [SerializeField] GameObject blood;

    Animator animator;

    bool isDead = false;

    private void Start()
    {
        animator = thisParentTransform.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        if (agentThis != null && !isDead)
        {
            animator.SetBool("dead", true);
            
            agentThis.ResetPath();

            StartCoroutine(KnockBack());

            thisParentTransform.GetComponent<Collider>().enabled = false;

            SpriteRenderer spriteRenderer = thisParentTransform.Find("Sprite").GetComponent<SpriteRenderer>();

            spriteRenderer.sortingOrder = 4;

            Instantiate(blood, thisParentTransform.position, blood.transform.rotation);

            isDead = true;
        }
    }

    IEnumerator KnockBack()
    {
        float currentTime = 0f;

        while (knockBackTime >= currentTime)
        {
            currentTime += Time.fixedDeltaTime;

            agentThis.Move((thisParentTransform.position - stateMachine.playerCharacter.transform.position).normalized * knockBackAmount);

            yield return null;
        }

        agentThis.enabled = false;
    }
}
