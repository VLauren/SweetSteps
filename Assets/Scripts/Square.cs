using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public Color PressedEmission;
    void Start()
    {
        Level.AddSquare(this);
    }

    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Press());
    }

    void OnTriggerExit(Collider other)
    {
        Level.RemoveSquare(this);
        Destroy(gameObject);
    }

    IEnumerator Press()
    {
        // GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.2f, 0.2f, 0.2f));
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
