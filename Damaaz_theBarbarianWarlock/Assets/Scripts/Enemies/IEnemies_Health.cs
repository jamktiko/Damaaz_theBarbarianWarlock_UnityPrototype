using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemies_Health
{
    //This interface is used by different enemies health scripts for changing the health of an enemy.
    //Used so we can change them without hard coding.

    void ChangeHealth(int health);
}
