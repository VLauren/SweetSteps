using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// TODO
// - 
// - 
// - 
// - 
// - 

public class MainChar : MonoBehaviour
{
    [SerializeField] float MovementSpeed = 4;
    [SerializeField] float RotationSpeed = 360;

    protected Vector3 InputDirection;
    protected Vector3 ControlMovement;
    protected Quaternion TargetRotation;

    void Start()
    {
        
    }

    void Update()
    {

        ControlMovement = Vector3.zero;
        ControlMovement = InputDirection * Time.deltaTime * MovementSpeed;

        float rCam = Camera.main.transform.eulerAngles.y;

        Debug.Log(ControlMovement);
        // ControlMovement = new Vector3(1, 0, 1) * Time.deltaTime * MovementSpeed;

        // Direccion relativa a camara
        ControlMovement = Quaternion.Euler(0, rCam, 0) * ControlMovement;
        if (ControlMovement != Vector3.zero)
        {
            TargetRotation = Quaternion.LookRotation(ControlMovement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, TargetRotation, Time.deltaTime * RotationSpeed);
        }

        GetComponent<CharacterController>().Move(ControlMovement + new Vector3(0, -Time.deltaTime, 0));

        // ----------------------------------
        var keyboard = Keyboard.current;
        if (keyboard.oKey.wasPressedThisFrame)
            Debug.Log("Test");
    }

    void OnMove(InputValue _value)
    {
        InputDirection = new Vector3(_value.Get<Vector2>().x, 0, _value.Get<Vector2>().y);
    }

}
