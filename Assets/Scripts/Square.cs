using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public Color PressedEmission;

    protected bool Pressed;
    protected bool TryPress;

    void Start()
    {
        Level.AddSquare(this);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MainChar>() != null)
        {
            // Pulso si no hay nada pulsado
            if (!Level.Instance.IsASquarePressed && other.GetComponent<CharacterController>().isGrounded)
                StartCoroutine(Press());
            else
                TryPress = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (Pressed)
        {
            // Si estaba pulsado, despulso y desaparezco
            StopPress();
        }
        else
        {
            // Si no estaba pulsado, dejo de intentar pulsar
            TryPress = false;
        }
    }

    protected virtual void StopPress()
    {
        Level.OnSquareUnpressed(this);
        Level.RemoveSquare(this);
        Destroy(gameObject);
    }

    void Update()
    {
        // Pulso si no hay nada pulsado
        if(TryPress && !Level.Instance.IsASquarePressed && MainChar.Instance.GetComponent<CharacterController>().isGrounded)
        {
            TryPress = false;
            StartCoroutine(Press());
        }
    }

    protected virtual IEnumerator Press()
    {
        Pressed = true;
        Level.OnSquarePressed(this);

        GetComponent<Renderer>().material.SetColor("_EmissionColor", PressedEmission);
        GetComponent<Renderer>().material.EnableKeyword("_EMISSION");

        Vector3 targetPosition = transform.position + Vector3.down * 0.1f;
        while(transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime);
            yield return null;
        }
    }
}
