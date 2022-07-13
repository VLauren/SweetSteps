using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hub : MonoBehaviour
{
    // Hub system
    // 4 mundos de 3 puertas con x niveles (entre 5 y 10)

    [SerializeField] HubDoorTrigger DoorTrigger1;
    [SerializeField] HubDoorTrigger DoorTrigger2;
    [SerializeField] HubDoorTrigger DoorTrigger3;

    [SerializeField] GameObject Door1;
    [SerializeField] GameObject Door2;
    [SerializeField] GameObject Door3;

    void Start()
    {
        DoorTrigger1.Door = 1;
        DoorTrigger2.Door = 2;
        DoorTrigger3.Door = 3;

        DoorTrigger1.World = GameData.CurrentWorld;
        DoorTrigger2.World = GameData.CurrentWorld;
        DoorTrigger3.World = GameData.CurrentWorld;


        Door1.SetActive(!GameData.IsDoorUnlocked(GameData.CurrentWorld, 1));
        Door2.SetActive(!GameData.IsDoorUnlocked(GameData.CurrentWorld, 2));
        Door3.SetActive(!GameData.IsDoorUnlocked(GameData.CurrentWorld, 3));
    }
}
