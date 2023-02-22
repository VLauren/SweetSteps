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
    static Dictionary<int, Dictionary<int, GameObject>> PlacedHWalls;
    static Dictionary<int, Dictionary<int, GameObject>> PlacedVWalls;
    static int highestX, highestY;

    void Start()
    {
        // EditorCursor = Instantiate(CursorPrefab).transform;
        EditorCursor = Instantiate(SquarePrefab).transform;

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

        int xPos = 0;
        int yPos = 0;

        Debug.DrawRay(Camera.main.transform.position, ray.direction * 300, Color.green);
        Debug.DrawLine(hitPt, hitPt + Vector3.up, Color.blue);


                        // if (PlacedItems[i][j].GetComponent<MultiplePressSquare>() != null)
                            // res += "2";
                        // else if (PlacedItems[i][j].GetComponent<Square>() != null)
        if (EditorCursor.GetComponent<Square>() != null)
        {
            hitPt.x = 3 * Mathf.RoundToInt((hitPt.x - 1.5f) / 3) + 1.5f;
            hitPt.z = 3 * Mathf.RoundToInt((hitPt.z - 1.5f) / 3) + 1.5f;

            hitPt = new Vector3(Mathf.Clamp(hitPt.x, -1.5f, 30), 0, Mathf.Clamp(hitPt.z, -1.5f, 30));

            xPos = Mathf.RoundToInt((hitPt.x + 1.5f) / 3);
            yPos = Mathf.RoundToInt((hitPt.z + 1.5f) / 3);
        }
        else if(EditorCursor.rotation == Quaternion.identity)
        {
            hitPt.x = 3 * Mathf.RoundToInt((hitPt.x - 1.5f) / 3) + 1.5f;
            hitPt.z = 3 * Mathf.RoundToInt((hitPt.z - 0) / 3) + 0;

            Debug.DrawLine(hitPt, hitPt + Vector3.up, Color.black);

            hitPt = new Vector3(Mathf.Clamp(hitPt.x, -1.5f, 30), 0.9f, Mathf.Clamp(hitPt.z, -3f, 30));

            Debug.DrawLine(hitPt, hitPt + Vector3.up, Color.red);

            xPos = Mathf.RoundToInt((hitPt.x + 1.5f) / 3);
            yPos = Mathf.RoundToInt((hitPt.z) / 3) + 1;
        }
        else
        {
            hitPt.x = 3 * Mathf.RoundToInt((hitPt.x - 0) / 3) + 0;
            hitPt.z = 3 * Mathf.RoundToInt((hitPt.z - 1.5f) / 3) + 1.5f;

            Debug.DrawLine(hitPt, hitPt + Vector3.up, Color.black);

            hitPt = new Vector3(Mathf.Clamp(hitPt.x, -3f, 30), 0.9f, Mathf.Clamp(hitPt.z, -1.5f, 30));

            Debug.DrawLine(hitPt, hitPt + Vector3.up, Color.red);

            xPos = Mathf.RoundToInt((hitPt.x) / 3) + 1;
            yPos = Mathf.RoundToInt((hitPt.z + 1.5f) / 3);

        }

        EditorCursor.position = hitPt;

        Keyboard kb = Keyboard.current;
        Mouse ms = Mouse.current;

        // Selecciar qué colocar
        if(kb.digit1Key.wasPressedThisFrame)
        {
            Destroy(EditorCursor.gameObject);
            EditorCursor = Instantiate(SquarePrefab, EditorCursor.transform.position, Quaternion.identity).transform;
        }
        if(kb.digit2Key.wasPressedThisFrame)
        {
            Destroy(EditorCursor.gameObject);
            EditorCursor = Instantiate(TwoPressSquarePrefab, EditorCursor.transform.position, Quaternion.identity).transform;
        }
        if(kb.digit3Key.wasPressedThisFrame)
        {
            Destroy(EditorCursor.gameObject);
            EditorCursor = Instantiate(WallPrefab, EditorCursor.transform.position, Quaternion.identity).transform;
        }
        if(kb.digit4Key.wasPressedThisFrame)
        {
            Destroy(EditorCursor.gameObject);
            EditorCursor = Instantiate(WallPrefab, EditorCursor.transform.position, Quaternion.Euler(0, 90, 0)).transform;
        }

        // Colocar
        if (ms.leftButton.wasPressedThisFrame)
        {
            PlaceItem(EditorCursor.gameObject, hitPt, EditorCursor.rotation ,xPos, yPos);
        }

        // Borrar
        if(ms.rightButton.wasPressedThisFrame)
        {
            print(EditorCursor.transform.position.x + " " + EditorCursor.transform.position.z);

            if(EditorCursor.transform.position.x % 3 == 0)
            {
                if (PlacedVWalls != null && PlacedVWalls.ContainsKey(xPos) && PlacedVWalls[xPos].ContainsKey(yPos))
                {
                    Destroy(PlacedVWalls[xPos][yPos]);
                    PlacedVWalls[xPos].Remove(yPos);
                }
            }
            else if(EditorCursor.transform.position.z % 3 == 0)
            {
                if (PlacedHWalls != null && PlacedHWalls.ContainsKey(xPos) && PlacedHWalls[xPos].ContainsKey(yPos))
                {
                    Destroy(PlacedHWalls[xPos][yPos]);
                    PlacedHWalls[xPos].Remove(yPos);
                }

            }
            else
            {
                if (PlacedItems != null && PlacedItems.ContainsKey(xPos) && PlacedItems[xPos].ContainsKey(yPos))
                {
                    Destroy(PlacedItems[xPos][yPos]);
                    PlacedItems[xPos].Remove(yPos);
                }
            }

        }

        if (kb.pKey.wasPressedThisFrame)
        {
            Level.LevelToSpawn = GetCurrentLevelString();

            string path = Application.dataPath + "/Data/Resources/LastPlayedLevel.txt";
            System.IO.File.WriteAllText(path, GetCurrentLevelString());

            foreach (var column in PlacedItems)
                foreach (var element in column.Value)
                    element.Value.SetActive(false);

            SceneManager.LoadScene("LevelPlayScene");
        }

        if (kb.sKey.wasPressedThisFrame)
        {
            Debug.Log(Application.dataPath + "/Resources");
        }

    }

    void PlaceItem(GameObject _prefab, Vector3 _position, Quaternion _rotation, int _xIndex, int _yIndex)
    {

        GameObject newItem = Instantiate(_prefab, _position, _rotation);

        if (EditorCursor.GetComponent<Square>() != null)
        {
            if (PlacedItems == null)
                PlacedItems = new Dictionary<int, Dictionary<int, GameObject>>();

            if (!PlacedItems.ContainsKey(_xIndex))
                PlacedItems.Add(_xIndex, new Dictionary<int, GameObject>());
            if (!PlacedItems[_xIndex].ContainsKey(_yIndex))
                PlacedItems[_xIndex].Add(_yIndex, null);

            if (PlacedItems[_xIndex][_yIndex] != null)
                Destroy(PlacedItems[_xIndex][_yIndex]);

            PlacedItems[_xIndex][_yIndex] = newItem;

            if (_xIndex > highestX)
                highestX = _xIndex;
            if (_yIndex > highestY)
                highestY = _yIndex;
        }
        else if (_rotation == Quaternion.identity)
        {
            if (PlacedHWalls == null)
                PlacedHWalls = new Dictionary<int, Dictionary<int, GameObject>>();

            if (!PlacedHWalls.ContainsKey(_xIndex))
                PlacedHWalls.Add(_xIndex, new Dictionary<int, GameObject>());
            if (!PlacedHWalls[_xIndex].ContainsKey(_yIndex))
                PlacedHWalls[_xIndex].Add(_yIndex, null);

            if (PlacedHWalls[_xIndex][_yIndex] != null)
                Destroy(PlacedHWalls[_xIndex][_yIndex]);

            PlacedHWalls[_xIndex][_yIndex] = newItem;

            if (_xIndex > highestX)
                highestX = _xIndex;
            if (_yIndex - 1 > highestY)
                highestY = _yIndex - 1;
        }
        else
        {
            if (PlacedVWalls == null)
                PlacedVWalls = new Dictionary<int, Dictionary<int, GameObject>>();

            if (!PlacedVWalls.ContainsKey(_xIndex))
                PlacedVWalls.Add(_xIndex, new Dictionary<int, GameObject>());
            if (!PlacedVWalls[_xIndex].ContainsKey(_yIndex))
                PlacedVWalls[_xIndex].Add(_yIndex, null);

            if (PlacedVWalls[_xIndex][_yIndex] != null)
                Destroy(PlacedVWalls[_xIndex][_yIndex]);

            PlacedVWalls[_xIndex][_yIndex] = newItem;

            if (_xIndex - 1 > highestX)
                highestX = _xIndex - 1;
            if (_yIndex > highestY)
                highestY = _yIndex;
        }


        DontDestroyOnLoad(newItem);

    }

    string GetCurrentLevelString()
    {
        string res = (highestX + 1) + "x" + (highestY + 1);

        // Objetos de cuadricula
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

        if (PlacedHWalls == null && PlacedVWalls == null)
            return res;

        // --------------------------------------------------------------

        if (PlacedVWalls == null)
            PlacedVWalls = new Dictionary<int, Dictionary<int, GameObject>>();
        if (PlacedHWalls == null)
            PlacedHWalls = new Dictionary<int, Dictionary<int, GameObject>>();

        // Paredes horizontales
        for (int j = highestY + 1; j >= 0; j--)
        {
            res += ":";
            for (int i = 0; i < highestX + 1; i++)
            {
                if (PlacedHWalls.ContainsKey(i) && PlacedHWalls[i].ContainsKey(j))
                    res += "w";
                else
                    res += "-";
            }
        }


        // Paredes verticales
        for (int j = highestY; j >= 0; j--)
        {
            res += ":";
            for (int i = 0; i < highestX + 2; i++)
            {
                if (PlacedVWalls.ContainsKey(i) && PlacedVWalls[i].ContainsKey(j))
                    res += "w";
                else
                    res += "-";
            }
        }


        return res;
    }
}
