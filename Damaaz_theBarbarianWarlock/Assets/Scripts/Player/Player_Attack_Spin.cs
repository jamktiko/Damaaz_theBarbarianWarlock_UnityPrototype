using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Attack_Spin : MonoBehaviour, IPlayer_DamageOwner
{
    [SerializeField] float attackTime = 3f;
    [SerializeField] float attackCoolDown = 1f;
    [SerializeField] int attackDamage = 100;

    [SerializeField] bool canAttack = true;

    GameObject attackObject;

    [SerializeField] InputActionReference _doAttack = null;

    Player_AnimationController animController;


    // Start is called before the first frame update
    void Start()
    {
        animController = GetComponent<Player_AnimationController>();

        _doAttack.action.Enable();
        _doAttack.action.started += _ => DoAttack();

        attackObject = transform.Find("Player_Sprite").Find("Player_Attack_Spin").gameObject;

        attackObject.GetComponent<Player_DamageDealer>().owner = this as IPlayer_DamageOwner;

        attackObject.SetActive(false);
    }

    void DoAttack()
    {
        if (canAttack)
        {
            StartCoroutine(Attack());

            canAttack = false;
        }
    }

    IEnumerator Attack()
    {
        attackObject.SetActive(true);
        animController.SwitchSpinningAttackAnimation(true);

        yield return new WaitForSeconds(attackTime);

        animController.SwitchSpinningAttackAnimation(false);
        attackObject.SetActive(false);

        float currentTime = 0f;

        while (currentTime >= attackCoolDown)
        {
            currentTime += Time.deltaTime;
            
            yield return null;
        }

        canAttack = true;
    }

    public int GetDamageAmount()
    {
        return attackDamage;
    }
}
