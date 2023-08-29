using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    CharacterController controller;

    [SerializeField] float movementSpeedForward = 5;
    [SerializeField] float movementSpeedSide = 4;

    [SerializeField] InputActionReference _moveLeft = null;
    [SerializeField] InputActionReference _moveRight = null;
    [SerializeField] InputActionReference _moveUp = null;
    [SerializeField] InputActionReference _moveDown = null;

    // Start is called before the first frame update
    void Start()
    {
        enableActions();
        controller = GetComponent<CharacterController>();
    }

    void enableActions()
    {
        _moveLeft.action.Enable();
        _moveRight.action.Enable();
        _moveUp.action.Enable();
        _moveDown.action.Enable();

        /*
        _moveLeft.action.started += _ => MoveLeft();
        _moveRight.action.started += _ => MoveRight();
        _moveUp.action.started += _ => MoveUp();
        _moveDown.action.started += _ => MoveDown();
        */
        _moveDown.action.IsPressed();
    }

    // Update is called once per frame
    void Update()
    {
        MoveLeft();
        MoveRight();
        MoveUp();
        MoveDown();
    }

    void MoveLeft()
    {
        if (_moveLeft.action.IsPressed())
        {
            controller.Move(-transform.right * movementSpeedForward * Time.deltaTime);
        }
    }
    void MoveRight()
    {
        if (_moveRight.action.IsPressed())
        {
            controller.Move(transform.right * movementSpeedForward * Time.deltaTime);
        }
    }
    void MoveUp()
    {
        if (_moveUp.action.IsPressed())
        {
            controller.Move(transform.forward * movementSpeedSide * Time.deltaTime);
        }
    }
    void MoveDown()
    {
        if (_moveDown.action.IsPressed())
        {
            controller.Move(-transform.forward * movementSpeedSide * Time.deltaTime);
        }
    }
}
