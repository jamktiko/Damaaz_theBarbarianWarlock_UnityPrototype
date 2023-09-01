using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    //This scrip controls the basic movement of the player character using the CharacterController component.
    //It uses the new input system for inputs.
    //Using the inputs, it creates a few vectors for both axii and also a combination of them and tells the CharacterController to move.

    CharacterController controller;

    //Set these InputActionReferences in the editor.

    [SerializeField] InputActionReference _moveLeft = null;
    [SerializeField] InputActionReference _moveRight = null;
    [SerializeField] InputActionReference _moveUp = null;
    [SerializeField] InputActionReference _moveDown = null;

    [SerializeField] float movementSpeedForward = 5;
    [SerializeField] float movementSpeedSideways = 4;

    Vector3 movementVectorSideways = Vector3.zero;
    Vector3 movementVectorForward = Vector3.zero;
    
    public Vector3 movementVector = Vector3.zero;
    [SerializeField] Vector3 zeroingMovementVector = Vector3.zero;

    public bool updateMovement = true;

    public Player_Enum_LookDirection lookDirection = Player_Enum_LookDirection.right;

    Player_AnimationController animController;

    void Start()
    {
        EnableActions();
        controller = GetComponent<CharacterController>();
        animController = GetComponent<Player_AnimationController>();
    }

    public void EnableActions()
    {
        //Enable input

        _moveLeft.action.Enable();
        _moveRight.action.Enable();
        _moveUp.action.Enable();
        _moveDown.action.Enable();
    }

    public void DisableActions()
    {
        //Disable input

        _moveLeft.action.Disable();
        _moveRight.action.Disable();
        _moveUp.action.Disable();
        _moveDown.action.Disable();
    }


    public void EnableMovement()
    {
        //Do we want to move the character or not?

        updateMovement = true;
    }

    public void DisableMovement()
    {
        //Do we want to move the character or not?

        updateMovement = false;
    }

    void Update()
    {
        UpdateMovementVector();

        UpdateRunningAnimation();
    }

    void UpdateMovementVector()
    {
        //Updating the vectors for both axii and also setting an enum for look direction, used by other scripts.

        if (_moveLeft.action.IsPressed())
        {
            lookDirection = Player_Enum_LookDirection.left;
            movementVectorForward += -transform.right;
        }
        if (_moveRight.action.IsPressed())
        {
            lookDirection = Player_Enum_LookDirection.right;
            movementVectorForward += transform.right;
        }
        if (_moveUp.action.IsPressed())
        {
            movementVectorSideways += transform.forward;
        }
        if (_moveDown.action.IsPressed())
        {
            movementVectorSideways += -transform.forward;
        }

        if (_moveLeft.action.IsPressed() || _moveRight.action.IsPressed() || _moveUp.action.IsPressed() || _moveDown.action.IsPressed())
        {
            zeroingMovementVector = movementVector;
        }
        else
        {
            zeroingMovementVector = Vector3.zero;
        }
        
        //Combined vector used by other scripts. Is this bad design? Probably...

        movementVector += movementVectorForward + movementVectorSideways;

        //Normalize so we can control the multiplication

        movementVectorForward.Normalize();
        movementVectorSideways.Normalize();
        movementVector.Normalize();
        
        UpdateMovement();
        
        //Zero axii so if input is not present the next frame, the player will stop 

        movementVectorForward = Vector3.zero;
        movementVectorSideways = Vector3.zero;
    }

    void UpdateMovement()
    {
        //Tells the CharacterController how to move for each axii.

        if (updateMovement)
        {
            controller.Move(movementVectorForward * movementSpeedForward * Time.deltaTime);
            controller.Move(movementVectorSideways * movementSpeedSideways * Time.deltaTime);
        }
    }

    void UpdateRunningAnimation()
    {
        if (zeroingMovementVector != Vector3.zero)
        {
            animController.SwitchRunningAnimationBool(true);
        }
        else
        {
            animController.SwitchRunningAnimationBool(false);
        }
    }
}
