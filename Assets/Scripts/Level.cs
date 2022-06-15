using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public static Level Instance { get; private set; }

    [Header("Instantiation Prefabs")]
    public GameObject SquarePrefab;
    public GameObject LevelStartPrefab;
    public GameObject LevelEndPrefab;

    [Space()]
    public bool GenerateRandomLevel;
    [TextArea(15,20)]
    public string LevelToSpawn;

    public Vector3 LevelCenter { get; private set; }

    List<Square> Squares;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // SpawnLevel(
            // "3x3\n" +
            // "0,0,1\n" +
            // "1,0,1\n" +
            // "1,1,1");

        if(GenerateRandomLevel)
        {
            int xSize = Random.Range(1, 6);
            int ySize = Random.Range(1, 6);

            LevelToSpawn = xSize + "x" + ySize;

            for (int i = 0; i < ySize; i++)
            {
                LevelToSpawn += "\n";
                for (int j = 0; j < xSize; j++)
                {
                    LevelToSpawn += (Random.value < 0.65f) ? "1" : "0";
                }
            }
        }

        SpawnLevel(LevelToSpawn);
    }

    public static void AddSquare(Square _square)
    {
        if (Instance.Squares == null)
            Instance.Squares = new List<Square>();

        Instance.Squares.Add(_square);
    }

    public static void RemoveSquare(Square _square)
    {
        Instance.Squares.Remove(_square);

        // Condicion de victoria provisional
        if (Instance.Squares.Count == 0)
        {
            // SceneManager.LoadScene(0);
            LevelEnd.Instance.OpenExit();
        }
    }

    void SpawnLevel(string _levelData)
    {
        // Debug.Log("Spawn level " + _levelData);

        string[] lines = _levelData.Split("\n"[0]);

        int xSize = int.Parse(lines[0].Split('x')[0]);
        int ySize = int.Parse(lines[0].Split('x')[1]);

        char[,] grid = new char[xSize, ySize];

        for (int i = 0; i < ySize; i++)
        {
            string lineWithoutComma = lines[ySize - i].Replace(",", "");

            for (int j = 0; j < xSize; j++)
                grid[j, i] = lineWithoutComma[j];
        }

        for (int i = 0; i < ySize; i++)
        {
            for (int j = 0; j < xSize; j++)
            {
                // Si es 1, casilla de suelo
                if (grid[j, i] == '1')
                    Instantiate(SquarePrefab, new Vector3(-1.5f + j * 3, 0, -1.5f + i * 3), Quaternion.identity);
            }
        }

        GameObject levelStart = Instantiate(LevelStartPrefab, new Vector3(-3f + xSize * 1.5f, 0, -4.5f), Quaternion.identity);
        GameObject levelEnd = Instantiate(LevelEndPrefab, new Vector3(-3f + xSize * 1.5f, 0, -4.5f + 3f * (ySize + 1)), Quaternion.identity);

        levelStart.transform.localScale = new Vector3(3 * xSize - 0.2f, levelStart.transform.localScale.y, levelStart.transform.localScale.z);
        levelEnd.transform.Find("Floor").localScale = new Vector3(3 * xSize - 0.2f, levelEnd.transform.Find("Floor").localScale.y, levelEnd.transform.Find("Floor").localScale.z);

        LevelCenter = new Vector3(-3 + 1.5f * xSize, 0, -3 + 1.5f * ySize);
    }
}
