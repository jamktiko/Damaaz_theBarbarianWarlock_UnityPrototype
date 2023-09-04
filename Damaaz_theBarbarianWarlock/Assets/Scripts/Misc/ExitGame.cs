using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ExitGame : MonoBehaviour
{
    [SerializeField] InputActionReference exitGameAction = null;

    // Start is called before the first frame update
    void Start()
    {
        exitGameAction.action.started += _ => ShutDownGame();
        exitGameAction.action.Enable();
    }

    void ShutDownGame()
    {
        Application.Quit();
    }
}
