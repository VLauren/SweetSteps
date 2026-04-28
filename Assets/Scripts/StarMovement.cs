using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMovement : MonoBehaviour
{
    float RotationSpeed = 0.4f;
    float OscSpeed = 0.4f;
    float Oscilation = 0.002f;

    void Update()
    {
        transform.Rotate(new Vector3(0, Time.deltaTime * RotationSpeed * 360, 0));
        transform.Translate(0, Oscilation * Mathf.Sin(OscSpeed * Mathf.PI * Time.time), 0);
    }
}

