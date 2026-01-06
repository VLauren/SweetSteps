using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GameData
{
    static Dictionary<int, bool[]> CompletedDoors;

    public static int CurrentWorld = 1;
    public static int CurrentDoor = 1;
    public static int CurrentLevel;

    public static int ShowDoorCompleteAnim = 0;

    public static bool IsWorldUnlocked(int _world)
    {
        // El mundo 1 siempre está debloqueado
        if (_world <= 1)
            return true;

        if (CompletedDoors == null)
            CompletedDoors = new Dictionary<int, bool[]>();
        if (!CompletedDoors.ContainsKey(_world - 1))
            CompletedDoors.Add(_world - 1, new bool[3]);

        // Un mundo está desbloqueado si las 3 puertas del mundo anterior se han completado
        return CompletedDoors[_world - 1][0] && CompletedDoors[_world - 1][1] && CompletedDoors[_world - 1][2];
    }

    public static bool IsDoorCompleted(int _world, int _door)
    {
        if (!IsWorldUnlocked(_world))
            return false;

        if (CompletedDoors == null)
            CompletedDoors = new Dictionary<int, bool[]>();
        if (!CompletedDoors.ContainsKey(_world))
            CompletedDoors.Add(_world, new bool[3]);

        return CompletedDoors[_world][_door - 1];
    }

    public static bool IsDoorUnlocked(int _world, int _door)
    {
        if (!IsWorldUnlocked(_world))
            return false;

        if (_door == 1)
            return true;

        if (CompletedDoors == null)
            CompletedDoors = new Dictionary<int, bool[]>();
        if (!CompletedDoors.ContainsKey(_world))
            CompletedDoors.Add(_world, new bool[3]);

        if (CompletedDoors[_world][_door - 2])
            return true;

        return false;
    }

    public static void SetDoorComplete(int _world, int _door)
    {
        if (CompletedDoors == null)
            CompletedDoors = new Dictionary<int, bool[]>();
        if (!CompletedDoors.ContainsKey(_world))
            CompletedDoors.Add(_world, new bool[3]);

        if (!CompletedDoors[_world][_door - 1])
            ShowDoorCompleteAnim = CurrentDoor;

        CompletedDoors[_world][_door - 1] = true;
    }

    public static void SetWorld(int _world)
    {
        CurrentWorld = _world;
        if (CurrentWorld > 4)
            CurrentWorld = 4;
        if (CurrentWorld < 1)
            CurrentWorld = 1;
    }

    public static void DebugUnlockAll()
    {
        // foreach (var kvp in CompletedDoors)
        // CompletedDoors[kvp.Key] = new bool[] { true, true, true };

        var keys = new HashSet<int>(CompletedDoors.Keys);
        foreach(var k in keys)
            CompletedDoors[k] = new bool[] { true, true, true };

    }
    public static SaveSlotData GetSaveData()
    {
        SaveSlotData data = new SaveSlotData()
        {
            CompletedDoorsValues = new bool[12],
            CurrentWorld = CurrentWorld
        };

        foreach(var kvp in  CompletedDoors)
            for (int i = 0; i < 3; i++)
                data.CompletedDoorsValues[kvp.Key - 1 + i] = CompletedDoors[kvp.Key][i];

        return data;
    }
}
