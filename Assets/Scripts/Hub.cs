using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hub : MonoBehaviour
{
    // Hub system
    // 4 mundos de 3 puertas con x niveles (entre 5 y 10)

    void Start()
    {
        /*
        TextAsset lvlTxt = Resources.Load<TextAsset>("levels");
        string[,] levelData = CSVReader.SplitCsvGrid(lvlTxt.text);

        for (int row = 1; row < levelData.GetLength(1); row++)
        {
            Debug.Log("level " + row + ": " +levelData[3, row]);
        }

        Level.Instance.SpawnLevel(levelData[3, 1]);
        */
        LevelsData.GetLevelData(1, 1, 1);
    }
}
