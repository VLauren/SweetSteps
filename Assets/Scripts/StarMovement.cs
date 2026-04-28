using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class StarMovement : MonoBehaviour
{
    float RotationSpeed = 0.2f;
    float OscSpeed = 0.3f;
    float Oscilation = 0.3f;

    Vector3 StartPos;
    float StartOsc;

    void Start()
    {
        StartPos = transform.position;
        StartOsc = Random.value;
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, Time.deltaTime * RotationSpeed * 360, 0));
        transform.position = StartPos + transform.up * Oscilation * Mathf.Sin(Mathf.PI * 2 * (OscSpeed * Time.time + StartOsc));
    }
}

