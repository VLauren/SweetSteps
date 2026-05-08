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

    public void ShowPauseMenu()
    {
        transform.Find("PauseMenu").gameObject.SetActive(true);

        if (SceneManager.GetActiveScene().name.Equals("LevelPlayScene"))
        {
            transform.Find("PauseMenu/LevelPause").gameObject.SetActive(true);
            transform.Find("PauseMenu/HubPause").gameObject.SetActive(false);
        }
        else
        {
            transform.Find("PauseMenu/LevelPause").gameObject.SetActive(false);
            transform.Find("PauseMenu/HubPause").gameObject.SetActive(true);

        }
    }

    public void HidePauseMenu()
    {
        Time.timeScale = 1;
        transform.Find("PauseMenu").gameObject.SetActive(false);
    }

    public void BackToHub()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("HubScene" + GameData.CurrentWorld);
        print("ews");
    }

    public void BackToMain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
