using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCompleteIndicator : MonoBehaviour
{
    public int World;
    public int Door;

    public bool ThisWorld;

    public Material CompletedMaterial;

    void Start()
    {
        if (ThisWorld)
            World = GameData.CurrentWorld;

        if (GameData.IsDoorCompleted(World, Door))
            GetComponent<Renderer>().material = CompletedMaterial;
    }
}
