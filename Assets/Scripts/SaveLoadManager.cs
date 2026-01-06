using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        WriteSlot(SelectedSlot, json);
    }

    public static bool LoadSlotData(int slot, out SaveSlotData data)
    {
        data = new SaveSlotData();

        string json = ReadSlot(slot);
        if (string.IsNullOrEmpty(json)) 
            return false;

        data = JsonUtility.FromJson<SaveSlotData>(json);
        return true;
    }

    private static string SlotPath(int slot) => Path.Combine(Application.persistentDataPath, "save" + slot + ".json");

    private static void WriteSlot(int slot, string json)
    {
        File.WriteAllText(SlotPath(slot), json);
    }

    private static string ReadSlot(int slot)
    {
        return File.Exists(SlotPath(slot)) ? File.ReadAllText(SlotPath(slot)) : null;
    }
}
