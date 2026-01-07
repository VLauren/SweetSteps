using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        ShowTitleScreen();
    }

    public void ShowTitleScreen()
    {
        transform.Find("Title").gameObject.SetActive(true);
        transform.Find("Play").gameObject.SetActive(false);
    }

    public void ShowPlayScreen()
    {
        transform.Find("Title").gameObject.SetActive(false);
        transform.Find("Play").gameObject.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void NewGame(int slot)
    {
        SaveLoadManager.SelectedSlot = slot;
        SaveSlotData empty = new SaveSlotData()
        {
            CompletedDoorsValues = new bool[12]
        };
        GameData.ApplySaveData(empty);
        SaveLoadManager.Save();
        SceneManager.LoadScene("HubScene1");
    }
}
