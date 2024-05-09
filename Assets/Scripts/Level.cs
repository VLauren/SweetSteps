using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public static Level Instance { get; private set; }

    static int LevelListIndex = 0;
    public bool IsASquarePressed { get; private set; }
    public Square LastPressedSquare { get; private set; }

    [Header("Instantiation Prefabs")]
    public GameObject SquarePrefab;
    public GameObject TwoPressSquarePrefab;
    public GameObject AlternatingSquarePrefab;
    public GameObject LevelStartPrefab;
    public GameObject LevelEndPrefab;
    public List<GameObject> LevelStartPrefabs;
    public List<GameObject> LevelEndPrefabs;
    public List<GameObject> LevelStartPrefabs2;
    public List<GameObject> LevelEndPrefabs2;
    public List<GameObject> LevelStartPrefabs3;
    public List<GameObject> LevelEndPrefabs3;
    public List<GameObject> LevelStartPrefabs4;
    public List<GameObject> LevelEndPrefabs4;
    public GameObject WallPrefab;
    public GameObject GhostPowerupPrefab;
    public GameObject AreaSquarePrefab;
    public List<Color> BGColors;

    [Space()]
    public string DoorOpenSound;

    [Space()]
    public bool GenerateRandomLevel;
    public bool UseLevelList;
    public bool DontSpawn;

    [Space()]
    [TextArea(15,20)]
    public static string LevelToSpawn;

    [Space()]
    [TextArea(15,10)]
    public List<string> LevelList;

    public Vector3 LevelCenter { get; private set; }


    List<Square> Squares;

    public static void NextLevel(float _transitionTime = 0.5f)
    {
        Instance.StartCoroutine(Instance.NextLevelRoutine(_transitionTime));
    }

    IEnumerator NextLevelRoutine(float _transitionTime)
    {
        FadeUI.FadeOut(_transitionTime);
        AudioManager.Play("fade_FXSweeper1", false, 0.8f);

        MainChar.DisableControl();

        yield return new WaitForSeconds(_transitionTime);

        MainChar.EnableControl();

        if(LevelEditor.EditorMode)
        {
            SceneManager.LoadScene("EditorScene");
        }
        else if (GameData.CurrentLevel >= LevelsData.DoorLevelCount(GameData.CurrentWorld, GameData.CurrentDoor))
        {
            GameData.SetDoorComplete(GameData.CurrentWorld, GameData.CurrentDoor);
            SceneManager.LoadScene("DoorEndScene");
        }
        else
        {
            GameData.CurrentLevel++;
            Level.LevelToSpawn = LevelsData.GetLevelData(GameData.CurrentWorld, GameData.CurrentDoor, GameData.CurrentLevel);

            print(GameData.CurrentWorld + " " + GameData.CurrentDoor + " " + GameData.CurrentLevel);

            SceneManager.LoadScene("LevelPlayScene");
        }
    }

    void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard.escapeKey.wasPressedThisFrame)
            SceneManager.LoadScene("HubScene" + GameData.CurrentWorld);

        // DEBUG
        if (keyboard.nKey.wasPressedThisFrame && keyboard.ctrlKey.isPressed)
            NextLevel(0.1f);
        if (keyboard.mKey.wasPressedThisFrame && keyboard.ctrlKey.isPressed)
        {
            GameData.CurrentLevel = LevelsData.DoorLevelCount(GameData.CurrentWorld, GameData.CurrentDoor);
            NextLevel(0.1f);
        }
    }

    void Start()
    {
        if (GenerateRandomLevel)
        {
            int xSize = Random.Range(1, 6);
            int ySize = Random.Range(1, 6);

            LevelToSpawn = xSize + "x" + ySize;

            // Casillas
            for (int i = 0; i < ySize; i++)
            {
                LevelToSpawn += "\n";
                for (int j = 0; j < xSize; j++)
                {
                    LevelToSpawn += (Random.value < 0.65f) ? (Random.value < 0.3f ? "2" : "1") : "0";
                }
            }

            // Muros
            for (int i = 0; i < ySize + 1; i++)
            {
                LevelToSpawn += "\n";
                for (int j = 0; j < xSize; j++)
                {
                    LevelToSpawn += (Random.value < 0.1f) ? "w" : "-";
                }
            }
            for (int i = 0; i < ySize; i++)
            {
                LevelToSpawn += "\n";
                for (int j = 0; j < xSize + 1; j++)
                {
                    LevelToSpawn += (Random.value < 0.1f) ? "w" : "-";
                }
            }
        }
        else if(UseLevelList)
        {
            LevelToSpawn = LevelList[LevelListIndex];
        }

        if (!DontSpawn)
            SpawnLevel(LevelToSpawn);

        // Background color
        Camera.main.transform.GetChild(0).GetComponent<Camera>().backgroundColor = BGColors[GameData.CurrentWorld - 1];

        FadeUI.FadeIn(0.5f);
    }

    public static void AddSquare(Square _square)
    {
        if (Instance == null)
            return;

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
            LevelEnd.Instance.OpenExit();

            AudioManager.Play(Instance.DoorOpenSound, false);
        }
    }

    public void SpawnLevel(string _levelData)
    {
        Debug.Log("Spawn level " + _levelData);

        string[] lines = _levelData.Split("\n"[0], ';', ':');

        // for (int i = 0; i < lines.Length; i++)
            // print("line: " + i + " " + lines[i]);

        // ---------------------------
        // Casillas

        int xSize = int.Parse(lines[0].Split('x')[0]);
        int ySize = int.Parse(lines[0].Split('x')[1]);

        char[,] grid = new char[xSize, ySize];

        // Parsear datos y guardar en array bidimensional
        for (int i = 0; i < ySize; i++)
        {
            string line = lines[ySize - i];

            for (int j = 0; j < xSize; j++)
                grid[j, i] = line[j];
        }

        // Instanciar casillas
        for (int i = 0; i < ySize; i++)
        {
            for (int j = 0; j < xSize; j++)
            {
                // Si es 1, casilla de suelo
                if (grid[j, i] == '1')
                    Instantiate(SquarePrefab, new Vector3(-1.5f + j * 3, 0, -1.5f + i * 3), Quaternion.identity);
                // Si es 2, casilla de pulsar dos veces
                if (grid[j, i] == '2')
                    Instantiate(TwoPressSquarePrefab, new Vector3(-1.5f + j * 3, 0, -1.5f + i * 3), Quaternion.identity);
                // Si es a, casilla que alterna A
                if (grid[j, i] == 'a')
                    Instantiate(AlternatingSquarePrefab, new Vector3(-1.5f + j * 3, 0, -1.5f + i * 3), Quaternion.identity).GetComponent<AlternatingSquare>().Pressable = true;
                // Si es b, casilla que alterna B
                if (grid[j, i] == 'b')
                    Instantiate(AlternatingSquarePrefab, new Vector3(-1.5f + j * 3, 0, -1.5f + i * 3), Quaternion.identity).GetComponent<AlternatingSquare>().Pressable = false;
                if (grid[j, i] == 'x')
                    Instantiate(AreaSquarePrefab, new Vector3(-1.5f + j * 3, 0, -1.5f + i * 3), Quaternion.identity);
            }
        }

        // Instanciar inicio y fin de nivel según ancho del nivel
        // GameObject levelStart = Instantiate(LevelStartPrefab, new Vector3(-3f + xSize * 1.5f, 0, -4.5f), Quaternion.identity);
        // GameObject levelEnd = Instantiate(LevelEndPrefab, new Vector3(-3f + xSize * 1.5f, 0, -4.5f + 3f * (ySize + 1)), Quaternion.identity);


        List<GameObject> levelStartPrefabsToUse;
        List<GameObject> levelEndPrefabsToUse;
        switch(GameData.CurrentWorld)
        {
            case 2:
                levelStartPrefabsToUse = LevelStartPrefabs2;
                levelEndPrefabsToUse = LevelEndPrefabs2;
                break;
            case 3:
                levelStartPrefabsToUse = LevelStartPrefabs3;
                levelEndPrefabsToUse = LevelEndPrefabs3;
                break;
            case 4:
                levelStartPrefabsToUse = LevelStartPrefabs4;
                levelEndPrefabsToUse = LevelEndPrefabs4;
                break;
            default:
                levelStartPrefabsToUse = LevelStartPrefabs;
                levelEndPrefabsToUse = LevelEndPrefabs;
                break;
        }

        GameObject levelStart = Instantiate(levelStartPrefabsToUse[xSize-1], new Vector3(-3f + xSize * 1.5f, 0, -4.5f), Quaternion.identity);
        GameObject levelEnd = Instantiate(levelEndPrefabsToUse[xSize-1], new Vector3(-3f + xSize * 1.5f, 0, -4.5f + 3f * (ySize + 1)), Quaternion.identity);

        // levelStart.transform.localScale = new Vector3(3 * xSize - 0.2f, levelStart.transform.localScale.y, levelStart.transform.localScale.z);
        // levelEnd.transform.Find("Floor").localScale = new Vector3(3 * xSize - 0.2f, levelEnd.transform.Find("Floor").localScale.y, levelEnd.transform.Find("Floor").localScale.z);

        LevelCenter = new Vector3(-3 + 1.5f * xSize, 0, -3 + 1.5f * ySize);

        // ---------------------------
        // Muros

        char[,] hWallGrid = new char[xSize, ySize + 1];
        char[,] wWallGrid = new char[xSize + 1, ySize];

        if (lines.Length > ySize + 1)
        {
            char[,] hWallsGrid = new char[xSize, ySize + 1];
            char[,] vWallsGrid = new char[xSize + 1, ySize];

            // Parsear datos y guardar en array bidimensional
            for (int i = ySize + 1; i < ySize + 1 + ySize + 1; i++)
            {
                int lineIndex = 3 * (ySize + 1) - i - 1;
                int gridYIndex = i - ySize - 1;
                string line = lines[lineIndex];

                for (int j = 0; j < xSize; j++)
                {
                    hWallGrid[j, gridYIndex] = line[j];
                    // Debug.Log("hWallGrid(" + j + " " + gridYIndex + "):" + hWallGrid[j, gridYIndex] + " ");
                }
            }
            for (int i = 2 * ySize + 2; i < 2 * ySize + 2 + ySize; i++)
            {
                int lineIndex = 2 * ySize + 2 + ySize + 1 + 2 * ySize - i;
                int gridYIndex = i - (2 * ySize + 2);
                string line = lines[lineIndex];

                for (int j = 0; j < xSize + 1; j++)
                {
                    wWallGrid[j, gridYIndex] = line[j];
                    // Debug.Log("wWallGrid(" + j + " " + gridYIndex + "):" + wWallGrid[j, gridYIndex]);
                }
            }

            // Instanciar casillas
            for (int i = 0; i < ySize + 1; i++)
            {
                for (int j = 0; j < xSize; j++)
                {
                    // Si es 1, casilla de suelo
                    if (hWallGrid[j, i] == 'w')
                        Instantiate(WallPrefab, new Vector3(-1.5f + j * 3, 0.9f, -3f + i * 3), Quaternion.identity);
                }
            }
            for (int i = 0; i < ySize; i++)
            {
                for (int j = 0; j < xSize + 1; j++)
                {
                    // Si es 1, casilla de suelo
                    if (wWallGrid[j, i] == 'w')
                        Instantiate(WallPrefab, new Vector3(-3f + j * 3, 0.9f, -1.5f + i * 3), Quaternion.Euler(0, 90, 0));
                }
            }
        }
        else
        {
            // Debug.Log("No hay muros");
        }

        // ---------------------------
        // Powerups

        char[,] powGrid = new char[xSize, ySize];


        if (lines.Length > 3 * ySize + 1)
        {
            // Parsear datos y guardar en array bidimensional
            for (int i = 3 * ySize + 2; i < 4 * ySize + 2; i++)
            {
                string line = lines[i];

                for (int j = 0; j < xSize; j++)
                {
                    powGrid[j, (i - (3 * ySize + 2))] = line[j];
                }
            }

            // Instanciar powerups
            for (int i = 0; i < ySize; i++)
            {
                for (int j = 0; j < xSize; j++)
                {
                    // Si es g, powerup de ghost
                    if (powGrid[j, i] == 'g')
                    {
                        Instantiate(GhostPowerupPrefab, new Vector3(-1.5f + j * 3, 0, -1.5f + (ySize - i - 1) * 3), Quaternion.identity);
                    }
                }
            }
        }
    }

    public static void OnSquarePressed(Square _square)
    {
        Instance.IsASquarePressed = true;
        Instance.LastPressedSquare = _square;
    }

    public static void OnSquareUnpressed(Square _square)
    {
        Instance.IsASquarePressed = false;
        OnGameAction();
    }

    public static void OnGameAction()
    {
        // Debug.Log("Level: OnGameAction");
        foreach (var sq in Instance.Squares)
            sq.SendMessage("OnGameAction");
    }
}
