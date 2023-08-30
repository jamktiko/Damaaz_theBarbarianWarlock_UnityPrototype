using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Attack_BaseCombo : MonoBehaviour
{
    //This script handles the base attack of the player character that has an attack combo system.
    //It uses the new input system for input.
    //Contains a void for checking what combo we are at and also an IEnumirator for cool downs and combo rhythm.

    //Set this InputActionReference in the editor.

    [SerializeField] InputActionReference _doAttack = null;

    [SerializeField] Player_Enum_BaseCombo comboState = Player_Enum_BaseCombo.first;

    [SerializeField] float attackDamage = 20f;
    [SerializeField] float attackDamageMultiplier = 2f;
    [SerializeField] float attackDamageFinal = 100f;
    [SerializeField] float attackTimeWindow = 0.5f;
    [SerializeField] float comboTimeWindow = 0.5f;
    [SerializeField] float comboCoolDown = 1f;
    [SerializeField] float attackScaleMultiplier = 1.5f;
    [SerializeField] bool canAttack = true;

    [SerializeField] Vector3 originalScale = Vector3.one;

    GameObject attackObject;

    void Start()
    {
        //enable input and bind the event to DoAttack() with out CallbackContext.
        _doAttack.action.Enable();
        _doAttack.action.started += _ => DoAttack();

        //Attack object is the object with the attack collider.
        attackObject = transform.Find("Player_Sprite").Find("Player_Attack").gameObject;
        attackObject.transform.localScale = originalScale;
        attackObject.SetActive(false);
    }

    void DoAttack()
    {   
        //We are keeping track of which strike we are at. Could have just used an int.
        //It applies different damage and scale of the hitbox based on what strike we are at.

        if (canAttack)
        {
            switch (comboState)
            {
                case Player_Enum_BaseCombo.first:
                    StartCoroutine(Attack(originalScale, attackDamage, true));
                    comboState++;
                    break;

                case Player_Enum_BaseCombo.second:
                    StartCoroutine(Attack(originalScale * attackScaleMultiplier * 1, attackDamage * attackDamageMultiplier * 1, true));
                    comboState++;
                    break;

                case Player_Enum_BaseCombo.third:
                    StartCoroutine(Attack(originalScale * attackScaleMultiplier * 2, attackDamageFinal, false));
                    comboState = Player_Enum_BaseCombo.first;

                    
                    break;
            }

            //We want the player to start an attack only once so bool here.

            canAttack = false;
        }
        else
        {
            //If player tries to initialize another attack, we break combo.

            comboState = Player_Enum_BaseCombo.first;
        }
    }

    IEnumerator Attack(Vector3 scale, float damage, bool combo)
    {
        //We input the scale of the hitbox, the damage amount and if this should be able to combo.

        //Attack duration, a duration the player can't start another attack and the hitbox is on.

        attackObject.SetActive(true);
        attackObject.transform.localScale = scale;

        yield return new WaitForSeconds(attackTimeWindow);

        //Attack duration ends and the player can attack again.

        attackObject.SetActive(false);
        
        if (combo)
        {
            canAttack = true;
        }

        //Using a "while" for the combo window, the duration in which the player has to strike to continue combo.

        float currentTime = 0f;

        while (comboTimeWindow >= currentTime)
        {
            currentTime += Time.deltaTime;

            if (_doAttack.action.IsPressed() && combo)
            {
                //We want to break this IEnumerator if an input is detected.
                //This input doesn't do anything else expect check it every frame.
                //Could have used a bool and a method linked to the actual input event-
                //like in Start and we would just check that bool here.

                yield break;
            }

            yield return null;
        }

        //if above "while" doesn't break, we start combo cooldown.

        if (!combo)
        {
            yield return new WaitForSeconds(comboCoolDown);

            canAttack = true;
        }

        //If there is no break, we want to set the enum to "first" in any case.

        comboState = Player_Enum_BaseCombo.first;
    }
}
