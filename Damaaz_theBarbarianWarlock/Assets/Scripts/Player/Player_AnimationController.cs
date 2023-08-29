using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AnimationController : MonoBehaviour
{
    //This script controls the animations for the player character.
    //Contains public voids for animations that need to be played.

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void PlayRollAnimation(Player_Enum_LookDirection lookDirection)
    {
        //Gets an enum as an input that is either right or left.
        //Based on this the player is going to roll clockwise or counter clockwise.

        if (lookDirection == Player_Enum_LookDirection.right)
        {
            StartCoroutine(SetAnimationBool("rollRight", 0, true));
            
            StartCoroutine(SetAnimationBool("rollRight", 0.1f, false));
        }

        if (lookDirection == Player_Enum_LookDirection.left)
        {
            StartCoroutine(SetAnimationBool("rollLeft", 0, true));

            StartCoroutine(SetAnimationBool("rollLeft", 0.1f, false));
        }
    }

    IEnumerator SetAnimationBool(string name, float delay, bool value)
    {
        //This IEnumerator sets an animator bool with a possible delay.
        //Reusable code. Very proud. Hope it won't cause problems.

        float currentTime = 0;

        while (delay >= currentTime)
        {
            currentTime += Time.deltaTime;
            
            yield return null;
        }

        animator.SetBool(name, value);
    }
}