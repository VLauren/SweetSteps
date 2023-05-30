using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    CharacterController CharCC;
    GameObject ExtraCollision;

    void Start()
    {
        CharCC = FindObjectOfType<MainChar>().GetComponent<CharacterController>();
        ExtraCollision = transform.Find("ExtraCollision").gameObject;
    }

    void Update()
    {
        ExtraCollision.SetActive(!CharCC.isGrounded);
    }

    // void OnTriggerEnter(Collider other)
    // {
        // print("Trigger!");
    // }
}
