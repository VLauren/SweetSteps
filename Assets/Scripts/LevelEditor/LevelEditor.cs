using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelEditor : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject SquarePrefab;
    public GameObject TwoPressSquarePrefab;
    public GameObject LevelStartPrefab;
    public GameObject LevelEndPrefab;
    public GameObject WallPrefab;

    [Space()]
    public GameObject CursorPrefab;

    Transform EditorCursor;

    Dictionary<int, Dictionary<int, GameObject>> PlacedItems;

    void Start()
    {
        EditorCursor = Instantiate(CursorPrefab).transform;
        PlacedItems = new Dictionary<int, Dictionary<int, GameObject>>();
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

        // - generar texto de nivel
        // - guardar en archivo?
        //
        // - probar nivel y vuelta a edit
        //
        // - seleccion y colocar con click
        // - borrar con click derecho
        // - seleccion de pared: cursor en intersecciones + PlaceWall
        // - guardar wall
        // - generacion de texto con paredes

    }

    void PlaceItem(GameObject _prefab, Vector3 _position, int _xIndex, int _yIndex)
    {
        if (!PlacedItems.ContainsKey(_xIndex))
            PlacedItems.Add(_xIndex, new Dictionary<int, GameObject>());
        if (!PlacedItems[_xIndex].ContainsKey(_yIndex))
            PlacedItems[_xIndex].Add(_yIndex, null);

        if (PlacedItems[_xIndex][_yIndex] != null)
            Destroy(PlacedItems[_xIndex][_yIndex]);

        PlacedItems[_xIndex][_yIndex] = Instantiate(_prefab, _position, Quaternion.identity);
    }
}
