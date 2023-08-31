using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Cultist_AttackState : AI_Cultist_StateBase
{
    //Incomplete


    // Start is called before the first frame update
    void Start()
    {
        stateMachine.health.onHealthChange += (int health) => CheckHealth(health);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void CheckHealth(int health)
    {
        if (health <= 0)
        {
            ChangeState(dead);
            ChangeState(damaged);
        }
        else
        {
            ChangeState(damaged);
        }
    }
}
