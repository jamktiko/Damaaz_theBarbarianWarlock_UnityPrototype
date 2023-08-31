using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DamageDealer : MonoBehaviour
{
    //This script deals damage to an enemy using a trigger collider.

    public IPlayer_DamageOwner owner;

    private void OnTriggerEnter(Collider other)
    {
        //Checks if it implements an IEnemies_Health interface used by enemy health scripts.

        if (other.TryGetComponent<IEnemies_Health>(out IEnemies_Health enemyHealth))
        {
            //If the collision gameobject contains a script that implements IEnemies_Health interface,--
            //change health using a definition in that interface.

            //This is built this way because we want to attack enemies without hardcoding.

            //The attack script is giving us itself and we are getting the damage amount, the attack script--
            //wants to do using the IPlayer_DamageOwner inteface GetDamageAmount() definition.

            //This is built this way so we can re-use scripts like this.

            enemyHealth.ChangeHealth(-owner.GetDamageAmount());
        }
    }
}
