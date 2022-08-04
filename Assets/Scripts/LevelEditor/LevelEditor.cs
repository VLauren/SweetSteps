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

    void Update()
    {
        Mouse mouse = Mouse.current;

        Ray ray = Camera.main.ScreenPointToRay(mouse.position.ReadValue());

        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.position + ray.direction * 100, Color.red);

        // - interseccion con el plano y debug
        // - redondear a casilla
        // - ver algun marcador de casilla
        // - instanciar algo con teclas
        // - mantener referencias a lo instanciado, array bidimensional
        // - borrar/sustituir
        // - generar texto de nivel
        // - guardar en archivo?
        // - probar nivel y vuelta a edit

    }
}
