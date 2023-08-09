using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    CharacterController CharCC;
    GameObject ExtraCollision;

    void Start()
    {
        if (FindObjectOfType<MainChar>() != null)
            CharCC = FindObjectOfType<MainChar>().GetComponent<CharacterController>();
        ExtraCollision = transform.Find("ExtraCollision").gameObject;
    }

    void Update()
    {
        if (CharCC != null && ExtraCollision != null)
            ExtraCollision.SetActive(!CharCC.isGrounded);
    }

    // void OnTriggerEnter(Collider other)
    // {
        // print("Trigger!");
    // }
}
