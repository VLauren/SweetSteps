using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainChar : MonoBehaviour
{
    public static MainChar Instance { get; private set; }

    [SerializeField] float MovementSpeed = 4;
    [SerializeField] float RotationSpeed = 360;
    [SerializeField] float Gravity = 20;

    protected Vector3 InputDirection;
    protected Vector3 ControlMovement;
    protected Quaternion TargetRotation;

    float VerticalVelocity;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        ControlMovement = Vector3.zero;
        ControlMovement = InputDirection * Time.deltaTime * MovementSpeed;

        float rCam = Camera.main.transform.eulerAngles.y;

        // Direccion relativa a camara
        ControlMovement = Quaternion.Euler(0, rCam, 0) * ControlMovement;
        if (ControlMovement != Vector3.zero)
        {
            TargetRotation = Quaternion.LookRotation(ControlMovement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, TargetRotation, Time.deltaTime * RotationSpeed);
        }

        if (GetComponent<CharacterController>().isGrounded)
            VerticalVelocity = -1;
        else
            VerticalVelocity -= Time.deltaTime * Gravity;

        GetComponent<CharacterController>().Move(ControlMovement + new Vector3(0, VerticalVelocity * Time.deltaTime, 0));

        // Perder por caida
        if(transform.position.y < -5)
            SceneManager.LoadScene(0);

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
