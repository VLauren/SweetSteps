using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareSpawn : Square
{
    public GameObject SquarePrefab;
    public override IEnumerator Press()
    {
        // TODO Spawn squares

        Instantiate(SquarePrefab, transform.position + new Vector3(3, 0, 0), Quaternion.identity);
        Instantiate(SquarePrefab, transform.position + new Vector3(-3, 0, 0), Quaternion.identity);
        Instantiate(SquarePrefab, transform.position + new Vector3(0, 0, 3), Quaternion.identity);
        Instantiate(SquarePrefab, transform.position + new Vector3(0, 0, -3), Quaternion.identity);

        yield return base.Press();
    }
}
