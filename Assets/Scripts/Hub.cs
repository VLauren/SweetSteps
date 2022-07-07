using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hub : MonoBehaviour
{
    // Hub system
    //
    // 4 mundos de 3 puertas con x niveles (entre 5 y 10)
    //
    // TODO
    // - csv parser
    // - parsear world, door, level, level data
    // - Clase LevelsData con string getleveldata(w, d, l)

    // - clase estatica game data
    //      - grupo de 3 bools por puerta
    //      - calcular IsWorldUnlocked según bools
    //      - metodo UpdateShardValue(int world, int door, bool value) o algo así
    // - movimiento entre mundos, con teclas de debug
    // - mostrar Shards o como lo llame, con teclas de debug
    // - permitir o no movimiento entre mundos segun shards
    // - cargar niveles de datos pasando por puerta
    // - debloquear shard de puerta tras nivel final
    // - test full sistema de unlock con niveles de relleno
    // - area especial a final de puerta de recoger shard

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
