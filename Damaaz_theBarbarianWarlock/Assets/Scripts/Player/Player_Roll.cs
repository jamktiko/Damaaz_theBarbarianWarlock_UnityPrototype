using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Roll : MonoBehaviour
{
    [SerializeField] InputActionReference _doRoll = null;

    // Start is called before the first frame update
    void Start()
    {
        _doRoll.action.Enable();

        _doRoll.action.started += _ => DoRoll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DoRoll() 
    {
    
    }
}
