using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPowerup : MonoBehaviour
{
    public float Time;

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<MainChar>() != null)
        {
            other.GetComponent<MainChar>().Ghost(Time);
            Destroy(gameObject);
        }
    }
}
