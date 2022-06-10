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

    List<Square> Squares;

    void Awake()
    {
        Instance = this;
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
}
