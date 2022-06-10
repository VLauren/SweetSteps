using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    public static LevelEnd Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    public void OpenExit()
    {
        transform.Find("ExitDoor").gameObject.SetActive(false);
    }
}
