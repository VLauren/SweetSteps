using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    static Dictionary<int, bool[]> CompletedDoors;

    public static int CurrentWorld = 1;
    public static int CurrentDoor;
    public static int CurrentLevel;

    public static bool IsWorldUnlocked(int _world)
    {
        // El mundo 1 siempre está debloqueado
        if (_world == 0)
            return true;

        // Un mundo está desbloqueado si las 3 puertas del mundo anterior se han completado
        return CompletedDoors[_world - 1][0] && CompletedDoors[_world - 1][1] && CompletedDoors[_world - 1][2];
    }

    public static bool IsDoorUnlocked(int _world, int _door)
    {
        if (!IsWorldUnlocked(_world))
            return false;

        if (_door == 1)
            return true;

        if (CompletedDoors[_world][_door - 1])
            return true;

        return false;
    }

    public static void SetDoorComplete(int _world, int _door)
    {
        if (CompletedDoors == null)
            CompletedDoors = new Dictionary<int, bool[]>();

        if (!CompletedDoors.ContainsKey(_world))
            CompletedDoors.Add(_world, new bool[3]);

        CompletedDoors[_world][_door] = true;
    }
}
