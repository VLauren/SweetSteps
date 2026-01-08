using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Color[] WorldColors = new Color[4];
    public Color[] DoorCompletionIndicatorColors = new Color[2];

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
            if (SaveLoadManager.LoadSlotData(i, out SaveSlotsData[i]))
            {
                transform.Find("Play/Slot" + (i + 1) + "/Game").gameObject.SetActive(true);
                transform.Find("Play/Slot" + (i + 1) + "/Empty").gameObject.SetActive(false);

                // Numero de mundo y color de fondo
                int currentWorld = SaveSlotsData[i].CurrentWorld;
                transform.Find("Play/Slot" + (i + 1) + "/Bg").GetComponent<Image>().color = WorldColors[currentWorld - 1];
                transform.Find("Play/Slot" + (i + 1) + "/Game/WorldText").GetComponent<Text>().text = "World " + (currentWorld);

                // Indicadores de puertas completas
                for (int j = 0; j < 12; j++)
                {
                    int doorComplete = SaveSlotsData[i].CompletedDoorsValues[j] ? 1 : 0;
                    transform.Find("Play/Slot" + (i + 1) + "/Game/SquareIndicators/Indicator" + (j + 1)).GetComponent<Image>().color = DoorCompletionIndicatorColors[doorComplete];
                }
            }
            else
            {
                // Si no hay partida, mostrar boton de new game
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
