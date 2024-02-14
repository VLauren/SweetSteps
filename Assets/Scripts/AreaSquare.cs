using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSquare : Square
{
    void Start()
    {
        Level.AddSquare(this);
    }

    public override IEnumerator Press()
    {
        return base.Press();

        var squares = FindObjectsOfType<Square>();
        foreach (var sq in squares)
            sq.StartCoroutine(sq.Press());
    }

    protected override void StopPress()
    {
        base.StopPress();

        var squares = FindObjectsOfType<Square>();
        foreach (var sq in squares)
            sq.StartCoroutine(sq.Press());
    }
}
