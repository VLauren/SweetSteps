using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Color[] WorldColors = new Color[4];

    SaveSlotData[] SaveSlotsData = new SaveSlotData[SaveLoadManager.SLOT_COUNT];

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
            // SaveSlotData data;
            // if (SaveLoadManager.LoadSlotData(i, out data))
            if (SaveLoadManager.LoadSlotData(i, out SaveSlotsData[i]))
            {
                transform.Find("Play/Slot" + (i + 1) + "/Game").gameObject.SetActive(true);
                transform.Find("Play/Slot" + (i + 1) + "/Empty").gameObject.SetActive(false);
            }
            else
            {
                transform.Find("Play/Slot" + (i + 1) + "/Game").gameObject.SetActive(false);
                transform.Find("Play/Slot" + (i + 1) + "/Empty").gameObject.SetActive(true);
            }
        }
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void NewGame(int slot)
    {
        SaveLoadManager.SelectedSlot = slot;
        SaveSlotData empty = new SaveSlotData()
        {
            CurrentWorld = 1,
            CompletedDoorsValues = new bool[12]
        };
        GameData.ApplySaveData(empty);
        SaveLoadManager.Save();
        SceneManager.LoadScene("HubScene1");
    }

    public void StartGame(int slot)
    {
        SaveLoadManager.SelectedSlot = slot;
        GameData.ApplySaveData(SaveSlotsData[slot]);

        SceneManager.LoadScene("HubScene" + (slot + 1));
    }
}
