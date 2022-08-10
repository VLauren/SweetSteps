using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelEditor : MonoBehaviour
{
    public static bool EditorMode { get; private set; }

    [Header("Prefabs")]
    public GameObject SquarePrefab;
    public GameObject TwoPressSquarePrefab;
    public GameObject LevelStartPrefab;
    public GameObject LevelEndPrefab;
    public GameObject WallPrefab;

    [Space()]
    public GameObject CursorPrefab;

    Transform EditorCursor;

    static Dictionary<int, Dictionary<int, GameObject>> PlacedItems;
    static int highestX, highestY;

    void Start()
    {
        EditorCursor = Instantiate(CursorPrefab).transform;

        EditorMode = true;

        if (PlacedItems != null)
            foreach (var column in PlacedItems)
                foreach (var element in column.Value)
                    element.Value.SetActive(true);
    }

    void Update()
    {
        Mouse mouse = Mouse.current;

        Ray ray = Camera.main.ScreenPointToRay(mouse.position.ReadValue());
        Plane floor = new Plane(Vector3.up, 0);
        float enter = 0;
        Vector3 hitPt = new Vector3();
        if (floor.Raycast(ray, out enter))
            hitPt = ray.GetPoint(enter);

        hitPt.x = 3 * Mathf.RoundToInt((hitPt.x - 1.5f) / 3) + 1.5f;
        hitPt.z = 3 * Mathf.RoundToInt((hitPt.z - 1.5f) / 3) + 1.5f;

        hitPt = new Vector3(Mathf.Clamp(hitPt.x, -1.5f, 30), 0, Mathf.Clamp(hitPt.z, -1.5f, 30));

        int xPos = Mathf.RoundToInt((hitPt.x + 1.5f) / 3);
        int yPos = Mathf.RoundToInt((hitPt.z + 1.5f) / 3);

        EditorCursor.position = hitPt;

        Keyboard kb = Keyboard.current;

        if(kb.digit1Key.wasPressedThisFrame)
            PlaceItem(SquarePrefab, hitPt, xPos, yPos);
        if(kb.digit2Key.wasPressedThisFrame)
            PlaceItem(TwoPressSquarePrefab, hitPt, xPos, yPos);


        if (kb.pKey.wasPressedThisFrame)
        {
            Level.LevelToSpawn = GetCurrentLevelString();

            string path = Application.dataPath + "/Data/Resources/LastPlayedLevel.txt";
            System.IO.File.WriteAllText(path, GetCurrentLevelString());

            foreach (var column in PlacedItems)
                foreach (var element in column.Value)
                    element.Value.SetActive(false);

            SceneManager.LoadScene(0);
        }

        // - guardar en archivo?
        // - probar nivel y vuelta a edit
        //
        // - seleccion y colocar con click
        // - borrar con click derecho
        // - seleccion de pared: cursor en intersecciones + PlaceWall
        // - guardar wall
        // - generacion de texto con paredes

        if (kb.sKey.wasPressedThisFrame)
        {
            Debug.Log(Application.dataPath + "/Resources" );
        }

    }

    void PlaceItem(GameObject _prefab, Vector3 _position, int _xIndex, int _yIndex)
    {
        if (PlacedItems == null)
            PlacedItems = new Dictionary<int, Dictionary<int, GameObject>>();

        if (!PlacedItems.ContainsKey(_xIndex))
            PlacedItems.Add(_xIndex, new Dictionary<int, GameObject>());
        if (!PlacedItems[_xIndex].ContainsKey(_yIndex))
            PlacedItems[_xIndex].Add(_yIndex, null);

        if (PlacedItems[_xIndex][_yIndex] != null)
            Destroy(PlacedItems[_xIndex][_yIndex]);

        GameObject newItem = Instantiate(_prefab, _position, Quaternion.identity);
        PlacedItems[_xIndex][_yIndex] = newItem;
        DontDestroyOnLoad(newItem);

        if (_xIndex > highestX)
            highestX = _xIndex;
        if (_yIndex > highestY)
            highestY = _yIndex;
    }

    string GetCurrentLevelString()
    {
        string res = (highestX + 1) + "x" + (highestY + 1);

        for (int j = highestY; j >= 0; j--)
        {
            res += ":";
            for (int i = 0; i < highestX + 1; i++)
            {
                if(PlacedItems.ContainsKey(i))
                {
                    if (PlacedItems[i].ContainsKey(j))
                    {
                        if (PlacedItems[i][j].GetComponent<MultiplePressSquare>() != null)
                            res += "2";
                        else if (PlacedItems[i][j].GetComponent<Square>() != null)
                            res += "1";
                        else
                            res += "0";
                    }
                    else
                        res += "0";
                }
                else
                    res += "0";
            }
        }

        return res;
    }
}
