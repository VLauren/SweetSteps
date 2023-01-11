using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    public static LevelEnd Instance { get; private set; }

    public Renderer Door;
    public Material MatToSet;

    void Awake()
    {
        Instance = this;
    }

    public void OpenExit()
    {
        transform.Find("ExitDoor").gameObject.SetActive(false);

        if (Door != null)
        {
            var mats = Door.materials;
            mats[2] = MatToSet;
            Door.materials = mats;
        }
    }
}
