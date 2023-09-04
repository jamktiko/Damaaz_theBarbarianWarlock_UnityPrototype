using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AttackEventManager : MonoBehaviour
{
    public delegate void OnDamagedEnemy(float amount);
    public event OnDamagedEnemy onDamagedEnemy;

    public void CallOnDamaged_Event(float damageAmount)
    {
        onDamagedEnemy(damageAmount);
    }
}
