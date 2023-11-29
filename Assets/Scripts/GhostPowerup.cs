using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPowerup : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<MainChar>() != null)
        {
            other.GetComponent<MainChar>().Ghost(4);
            Destroy(gameObject);
        }
    }
}
