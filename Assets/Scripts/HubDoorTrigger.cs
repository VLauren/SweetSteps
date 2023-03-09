using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HubDoorTrigger : MonoBehaviour
{
    public int World;
    public int Door;

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<MainChar>() != null)
        {
            StartCoroutine(DoorEnter());
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;

        Gizmos.color = new Color(0.5f, 1f, 1, 0.5f);
        Gizmos.DrawCube(Vector3.zero, Vector3.one);
        Gizmos.color = new Color(0, 1, 1, 0.7f);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }

    IEnumerator DoorEnter()
    {
        FadeUI.FadeOut(0.5f);

        MainChar.DisableControl();

        AudioManager.Play("door_powerUp8", false);
        AudioManager.Play("fade_FXSweeper1", false, 0.8f);

        yield return new WaitForSeconds(0.5f);

        MainChar.EnableControl();

        GameData.CurrentWorld = World;
        GameData.CurrentDoor = Door;
        GameData.CurrentLevel = 1;

        Level.LevelToSpawn = LevelsData.GetLevelData(World, Door, 1);
        SceneManager.LoadScene("LevelPlayScene");
    }
}
