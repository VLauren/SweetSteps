using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        print("Enter");
        // other.transform.Translate(Vector3.down * 0.1f);
        transform.Translate(Vector3.down * 0.01f);
    }

    private void OnTriggerExit(Collider other)
    {
        print("Exit");
        Destroy(gameObject);
    }

    IEnumerator Press()
    {
        HACER QUE BAJE POCO A POCO
        yield return null;
    }
}
