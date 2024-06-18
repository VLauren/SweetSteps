using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareSpawn : Square
{
    public GameObject SquarePrefab;
    public override IEnumerator Press()
    {
        bool spawnNorth = true;
        bool spawnSouth = true;
        bool spawnEast = true;
        bool spawnWest = true;

        var squares = FindObjectsOfType<Square>();
        foreach (var sq in squares)
        {
            if (sq != this)
            {
                if (Vector3.Distance(sq.transform.position, transform.position + new Vector3(0, 0, 3)) <= 0.1f)
                {
                    spawnNorth = false;
                    print(sq.transform.position + " north");
                }
                if (Vector3.Distance(sq.transform.position, transform.position + new Vector3(0, 0, -3)) <= 0.1f)
                {
                    spawnSouth = false;
                    print(sq.transform.position + " south");
                }
                if (Vector3.Distance(sq.transform.position, transform.position + new Vector3(3, 0, 0)) <= 0.1f)
                {
                    spawnEast = false;
                    print(sq.transform.position + " east");
                }
                if (Vector3.Distance(sq.transform.position, transform.position + new Vector3(-3, 0, 0)) <= 0.1f)
                {
                    spawnWest = false;
                    print(sq.transform.position + " west");
                }
            }
        }

        List<GameObject> spawnedSquares = new List<GameObject>();

        if (spawnNorth && transform.position.z < -1.4f + (Level.LevelSizeY - 2) * 3)
            spawnedSquares.Add(Instantiate(SquarePrefab, transform.position + new Vector3(0, 0, 3), Quaternion.identity));
        if (spawnSouth && transform.position.z > 1.4f)
            spawnedSquares.Add(Instantiate(SquarePrefab, transform.position + new Vector3(0, 0, -3), Quaternion.identity));
        if(spawnEast)
            spawnedSquares.Add(Instantiate(SquarePrefab, transform.position + new Vector3(3, 0, 0), Quaternion.identity));
        if (spawnWest)
            spawnedSquares.Add(Instantiate(SquarePrefab, transform.position + new Vector3(-3, 0, 0), Quaternion.identity));

        foreach (var sq in spawnedSquares)
        {
            Color ogColor = sq.GetComponent<Renderer>().material.color;
            sq.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0);
            StartCoroutine(ChangeColorOverTime(sq.GetComponent<Renderer>(), ogColor, 0.1f));
        }

        yield return base.Press();
    }

    IEnumerator ChangeColorOverTime(Renderer _renderer, Color _color, float _time)
    {
        float elapsed = 0;
        Color startColor = _renderer.material.color;

        while(elapsed < _time)
        {
            elapsed += Time.deltaTime;

            _renderer.material.color = Color.Lerp(startColor, _color, elapsed / _time);

            yield return null;
        }

        _renderer.material.color = _color;
    }
}
