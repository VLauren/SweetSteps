using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorEndObject : MonoBehaviour
{
    void Start()
    {
        FadeUI.FadeIn(0.5f);
    }

    void Update()
    {
        transform.Find("Model").Rotate(0, Time.deltaTime * 90, 0, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MainChar>() != null)
        {
            StartCoroutine(BackToHub());
        }
    }

    IEnumerator BackToHub()
    {
        FadeUI.FadeOut(0.5f);

        MainChar.DisableControl();

        yield return new WaitForSeconds(0.5f);

        MainChar.EnableControl();

        // Cargar escena de hub
        SceneManager.LoadScene("HubScene" + GameData.CurrentWorld);
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
