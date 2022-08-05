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

    void Start()
    {
        EditorCursor = Instantiate(CursorPrefab).transform;
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

        EditorCursor.position = hitPt;

        // - instanciar algo con teclas
        // - mantener referencias a lo instanciado, array bidimensional
        // - borrar/sustituir
        // - generar texto de nivel
        // - guardar en archivo?
        // - probar nivel y vuelta a edit

    }
}
