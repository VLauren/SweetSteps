using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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

        GameObject DoorToSpawnPlayer = null;
        if (GameData.CurrentDoor == 1)
            DoorToSpawnPlayer = DoorTrigger1.gameObject;
        if (GameData.CurrentDoor == 2)
            DoorToSpawnPlayer = DoorTrigger2.gameObject;
        if (GameData.CurrentDoor == 3)
            DoorToSpawnPlayer = DoorTrigger3.gameObject;

        if(DoorToSpawnPlayer != null)
        {
            MainChar.Instance.GetComponent<CharacterController>().enabled = false;
            MainChar.Instance.transform.position = DoorToSpawnPlayer.transform.position + new Vector3(0.25f, 0.15f, -4.78f);
            MainChar.Instance.transform.rotation = Quaternion.Euler(0, 140, 0);
            MainChar.Instance.GetComponent<CharacterController>().enabled = true;
        }

        FadeUI.FadeIn();

        if (GameData.ShowDoorCompleteAnim == 1)
            StartCoroutine(DoorCompleteAnimation(DoorTrigger1.gameObject));
        if(GameData.ShowDoorCompleteAnim == 2)
            StartCoroutine(DoorCompleteAnimation(DoorTrigger2.gameObject));
        if(GameData.ShowDoorCompleteAnim == 3)
            StartCoroutine(DoorCompleteAnimation(DoorTrigger3.gameObject));


        // ==================
        // Hack para builds, arrancar en nivel 1

        if(false && !GameData.IsDoorUnlocked(1, 2))
        {
            GameData.CurrentWorld = 1;
            GameData.CurrentDoor = 1;
            GameData.CurrentLevel = 1;

            Level.LevelToSpawn = LevelsData.GetLevelData(1, 1, 1);
            SceneManager.LoadScene("LevelPlayScene");
        }
    }

    private void Update()
    {
        Keyboard kbrd = Keyboard.current;
        if(kbrd.numpadPlusKey.wasPressedThisFrame)
        {
            GameData.SetWorld(GameData.CurrentWorld + 1);
            SceneManager.LoadScene("HubScene" + (GameData.CurrentWorld == 1 ? 1 : 2));
        }
        if (kbrd.numpadMinusKey.wasPressedThisFrame)
        {
            GameData.SetWorld(GameData.CurrentWorld - 1);
            SceneManager.LoadScene("HubScene" + (GameData.CurrentWorld == 1 ? 1 : 2));
        }
    }

    IEnumerator DoorCompleteAnimation(GameObject _doorTrigger)
    {
        GameData.ShowDoorCompleteAnim = 0;
        DoorCompleteIndicator dci = _doorTrigger.transform.parent.Find("DoorCompleteIndicator").GetComponent<DoorCompleteIndicator>();

        yield return null;

        dci.GetComponent<Renderer>().material = dci.OGMaterial;
        MainChar.DisableControl();

        yield return new WaitForSeconds(1);

        Effects.SpawnEffect(2, dci.transform.position);

        dci.GetComponent<Renderer>().material = dci.CompletedMaterial;

        yield return new WaitForSeconds(.5f);

        MainChar.EnableControl();
    }
}
