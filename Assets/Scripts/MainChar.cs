using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainChar : MonoBehaviour
{
    public static MainChar Instance { get; private set; }

    static bool ControlEnabled = true;

    [SerializeField] float MovementSpeed = 4;
    [SerializeField] float RotationSpeed = 360;
    [SerializeField] float Gravity = 20;
    [SerializeField] float JumpStrength = 1;

    protected Vector3 InputDirection;
    protected Vector3 ControlMovement;
    protected Quaternion TargetRotation;
    protected bool JumpPressed;

    protected Animator Animator;

    public bool GhostActive { get; private set; }
    public float VerticalVelocity { get; private set; }

    public Square SquareToPressAfterGhost;

    Material[] OGMats;
    Coroutine GhostRoutineRef;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Animator = transform.Find("Model").GetComponent<Animator>();
        OGMats = transform.Find("Model/Cube").GetComponent<Renderer>().materials;
    }

    void Update()
    {
        if(!ControlEnabled)
        {
            InputDirection = Vector3.zero;
            JumpPressed = false;
        }

        ControlMovement = Vector3.zero;
        ControlMovement = InputDirection * Time.deltaTime * MovementSpeed;

        Animator.SetFloat("MovementSpeed", InputDirection.magnitude);

        float rCam = Camera.main.transform.eulerAngles.y;

        // Direccion relativa a camara
        ControlMovement = Quaternion.Euler(0, rCam, 0) * ControlMovement;
        if (ControlMovement != Vector3.zero)
        {
            TargetRotation = Quaternion.LookRotation(ControlMovement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, TargetRotation, Time.deltaTime * RotationSpeed);
        }

        if (GetComponent<CharacterController>().isGrounded)
        {
            VerticalVelocity = -1;
            if (JumpPressed && !GhostActive)
            {
                Animator.SetTrigger("Jump");
                Animator.SetBool("Grounded", false);
                VerticalVelocity = JumpStrength;
                JumpPressed = false;

                Effects.SpawnEffect(1, transform.Find("Model").position);

                AudioManager.Play("footstep_concrete_00" + Random.Range(0, 5), false);
            }
            else
            {
                Animator.SetBool("Grounded", true);
            }
        }
        else
        {
            VerticalVelocity -= Time.deltaTime * Gravity;

            if (VerticalVelocity < -2)
                Animator.SetBool("Grounded", false);

            if(GhostActive && transform.position.y < 0.9f)
            {
                Animator.SetBool("Grounded", true);
                if (VerticalVelocity < 0)
                    VerticalVelocity = 0;
            }
        }

        GetComponent<CharacterController>().Move(ControlMovement + new Vector3(0, VerticalVelocity * Time.deltaTime, 0));

        // Perder por caida
        if (transform.position.y < -5)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            AudioManager.Play("fall_lowRandom", false);
            AudioManager.Play("fall_question_001", false);
        }

        // ----------------------------------

        var keyboard = Keyboard.current;
        if (keyboard.digit3Key.wasPressedThisFrame)
            Time.timeScale = 3;
        if (keyboard.digit1Key.wasPressedThisFrame)
            Time.timeScale = 1;

        if (keyboard.gKey.wasPressedThisFrame)
            Ghost(4);

    }

    void OnMove(InputValue _value)
    {
        InputDirection = new Vector3(_value.Get<Vector2>().x, 0, _value.Get<Vector2>().y);
    }

    void OnJump(InputValue _value)
    {
        JumpPressed = true;
    }

    public static void DisableControl()
    {
        ControlEnabled = false;
    }

    public static void EnableControl()
    {
        ControlEnabled = true;
    }

    public void Ghost(float _time)
    {
        GhostActive = true;

        if(GhostRoutineRef != null)
            StopCoroutine(GhostRoutineRef);
        GhostRoutineRef = StartCoroutine(GhostRoutine(_time));
    }

    IEnumerator GhostRoutine(float _time)
    {
        Material[] ghostMats = { null };
        transform.Find("Model/Cube").GetComponent<Renderer>().materials = ghostMats;

        // TODO cambiar layer pa atravesar paredes

        yield return new WaitForSeconds(_time);

        GhostActive = false;
        transform.Find("Model/Cube").GetComponent<Renderer>().materials = OGMats;

        yield return null;

        if(SquareToPressAfterGhost != null)
        {
            if (!Level.Instance.IsASquarePressed && GetComponent<CharacterController>().isGrounded)
                StartCoroutine(SquareToPressAfterGhost.Press());
            else
                SquareToPressAfterGhost.TryPress = true;

            SquareToPressAfterGhost = null;
        }

        // TODO restablecer layer
    }
}
