using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IPlayer_DamageOwner
{
    //this interface is used by all player attack scrips to communicate to the damage dealers.

    int GetDamageAmount();
}
