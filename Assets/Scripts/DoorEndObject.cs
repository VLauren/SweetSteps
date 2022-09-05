using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorEndObject : MonoBehaviour
{
    void Update()
    {
        transform.Find("Model").Rotate(0, Time.deltaTime * 90, 0, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MainChar>() != null)
        {
            // Cargar escena de hub
            SceneManager.LoadScene("HubScene");
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;

        Gizmos.color = new Color(0.75f, 0.75f, 1, 0.5f);
        Gizmos.DrawCube(Vector3.zero, Vector3.one);
        Gizmos.color = new Color(0.5f, 0.5f, 1, 0.7f);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
}
