using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Cultist_DeadState : AI_Cultist_StateBase
{
    //Incomplete


    private void OnEnable()
    {
        transform.parent.GetComponent<NavMeshAgent>().ResetPath();
    }
}
