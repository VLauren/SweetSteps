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

        for (int i = 0; i < 3; i++)
        {
            SaveSlotData data;
            if (SaveLoadManager.LoadSlotData(i, out data))
            {

                transform.Find("Play/Slot" + (i + 1) + "/Game").gameObject.SetActive(true);
                transform.Find("Play/Slot" + (i + 1) + "/Empty").gameObject.SetActive(false);
            }
            else
            {
                print("Play/Slot" + (i + 1) + "/Game");
                transform.Find("Play/Slot" + (i + 1) + "/Game").gameObject.SetActive(false);
                transform.Find("Play/Slot" + (i + 1) + "/Empty").gameObject.SetActive(true);
            }
        }
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
