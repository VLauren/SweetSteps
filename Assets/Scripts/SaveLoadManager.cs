using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[System.Serializable]
public struct SaveSlotData
{
    public bool[] CompletedDoorsValues;
    public int CurrentWorld;
}

public static class SaveLoadManager
{
    public const int SLOT_COUNT = 3;

    public static int SelectedSlot;

    public static void Save()
    {
        SaveSlotData data = GameData.GetSaveData();
        string json = JsonUtility.ToJson(data, true);
        Debug.Log(json);
    }
}
