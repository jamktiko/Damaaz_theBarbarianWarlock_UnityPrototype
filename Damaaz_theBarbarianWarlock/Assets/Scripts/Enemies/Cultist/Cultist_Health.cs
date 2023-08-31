using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cultist_Health : MonoBehaviour, IEnemies_Health
{
    //This script keeps track of the health of Cultist enemy.
    //Implements IEnemies_Health interface for the damage dealing purposes.

    public int health = 100;

    //Setting up an event "onHealthChange" that delivers an int.

    public delegate void OnHealthChange(int currentHealth);
    public event OnHealthChange onHealthChange;

    public void ChangeHealth(int changeAmount)
    {
        //This method is called by the damage dealer script.

        health += changeAmount;

        //Calling the formentioned event.

        onHealthChange(health);
    }
}
