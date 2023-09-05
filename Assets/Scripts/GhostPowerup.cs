using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPowerup : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<MainChar>() != null)
        {
            print("fantasmaaaa picked up");
            Destroy(gameObject);
        }
    }
}
