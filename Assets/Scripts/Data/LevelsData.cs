using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelsData
{
    public static Dictionary<int, Dictionary<int, Dictionary<int, string>>> AllLevelData;

    static void UpdateLevelsData()
    {
        // Archivo con datos de niveles
        TextAsset attacksCSVData = Resources.Load<TextAsset>("levels");

        // Array bidimensional construido a partir del archivo csv
        string[,] stringsGrid = CSVReader.SplitCsvGrid(attacksCSVData.text);

        // Diccionario con los datos
        AllLevelData = new Dictionary<int, Dictionary<int, Dictionary<int, string>>>();

        for (int j = 1; j < stringsGrid.GetLength(1); j++)
        {
            if (!int.TryParse(stringsGrid[2, j], out int level))
                continue;

            int world = int.Parse(stringsGrid[0, j]);
            int door = int.Parse(stringsGrid[1, j]);
            // int level = int.Parse(stringsGrid[2, j]);
            string data = stringsGrid[3, j];

            if (!AllLevelData.ContainsKey(world))
                AllLevelData.Add(world, new Dictionary<int, Dictionary<int, string>>());
            if (!AllLevelData[world].ContainsKey(door))
                AllLevelData[world].Add(door, new Dictionary<int, string>());

            if (!AllLevelData[world][door].ContainsKey(level))
                AllLevelData[world][door].Add(level, data);
            else
                AllLevelData[world][door][level] = data;

            // Debug.Log(world + " " + door + " " + level + " " + data);
        }
    }

    public static string GetLevelData(int _world, int _door, int _level)
    {
        if (AllLevelData == null)
            UpdateLevelsData();

        return AllLevelData[_world][_door][_level];
    }

    public static int DoorLevelCount(int _world, int _door)
    {
        if (AllLevelData == null)
            UpdateLevelsData();

        return AllLevelData[_world][_door].Count;
    }
}
