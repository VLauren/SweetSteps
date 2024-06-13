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

        if (spawnNorth && transform.position.z < -1.4f + (Level.LevelSizeY-2) * 3)
            Instantiate(SquarePrefab, transform.position + new Vector3(0, 0, 3), Quaternion.identity);
        if (spawnSouth && transform.position.z > 1.4f)
            Instantiate(SquarePrefab, transform.position + new Vector3(0, 0, -3), Quaternion.identity);
        if(spawnEast)
            Instantiate(SquarePrefab, transform.position + new Vector3(3, 0, 0), Quaternion.identity);
        if (spawnWest)
            Instantiate(SquarePrefab, transform.position + new Vector3(-3, 0, 0), Quaternion.identity);


        yield return base.Press();
    }
}
