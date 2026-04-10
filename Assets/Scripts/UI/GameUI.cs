using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    void Start()
    {
        if (!SceneManager.GetActiveScene().name.Equals("LevelPlayScene"))
        {
            transform.Find("LevelNumber").gameObject.SetActive(false);
        }
        else
        {
            int level = GameData.CurrentLevel;
            int total = LevelsData.DoorLevelCount(GameData.CurrentWorld, GameData.CurrentDoor);
            transform.Find("LevelNumber").GetComponent<Text>().text = level + " / " + total;
        }
    }
}
