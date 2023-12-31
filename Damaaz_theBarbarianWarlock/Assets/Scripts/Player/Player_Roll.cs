using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Roll : MonoBehaviour
{
    //This script controls the rolling mechanic and movement of the player character using the CharacterController component.
    //Uses the new input system for inputs.
    //This has two IEnumerators for rolling and for cooldown.
    //Communicates with Player_Movement and Player_AnimationController for rolling animation.

    //Set this InputActionReference in the editor.

    [SerializeField] InputActionReference _doRoll = null;

    CharacterController controller;
    Player_AnimationController animController;
    Player_Movement movement;

    [SerializeField] float rollSpeed = 15f;
    [SerializeField] float rollTime = 0.2f;
    [SerializeField] float coolDownTime = 2f;

    [SerializeField] bool canRoll = true;

    TrailRenderer[] trails;

    void Start()
    {
        trails = transform.Find("Player_Sprite").Find("Player_Trail").GetComponentsInChildren<TrailRenderer>();

        foreach (TrailRenderer trail in trails)
        {
            trail.emitting = false;
        }

        controller = GetComponent<CharacterController>();
        animController = GetComponent<Player_AnimationController>();
        movement = GetComponent<Player_Movement>();

        _doRoll.action.Enable();

        //Binding an event to DoRoll() without the CallbackContext.

        _doRoll.action.started += _ => DoRoll();
    }

    void DoRoll() 
    {
        if (canRoll)
        {
            StartCoroutine(Roll());
        }
    }

    IEnumerator Roll()
    {
        //Takes over the movement of the player character from the Player_Movement and moves in the direction of the Player_Movement.lookDirection vector.

        canRoll = false;

        movement.DisableMovement();

        foreach (TrailRenderer trail in trails)
        {
            trail.emitting = true;
            trail.endColor = new Color(255, 0, 0, 255);
            trail.startColor = new Color(0, 0, 0, 255);
        }

        animController.PlayRollAnimation(movement.lookDirection);

        //Timer for the duration of the roll.

        float currentTime = 0f;
        
        while (rollTime >= currentTime)
        {
            //Moves the character a certain distance every frame during timer.

            controller.Move(movement.movementVector * rollSpeed * Time.deltaTime);

            currentTime += Time.deltaTime;
            
            yield return null;
        }

        //Gives back control to the Player_Movement.

        movement.EnableMovement();

        foreach (TrailRenderer trail in trails)
        {
            trail.emitting = false;
            trail.endColor = new Color(255, 0, 0, 0);
            trail.startColor = new Color(0, 0, 0, 0);
        }

        StartCoroutine(RollCoolDown());
    }

    IEnumerator RollCoolDown()
    {
        //A timer that disables and enables a bool.
        //Bool used for if the player can roll.

        float currentTime = 0f;

        while (coolDownTime >= currentTime)
        {
            currentTime += Time.deltaTime;

                yield return null;
        }

        canRoll = true;
    }
}
