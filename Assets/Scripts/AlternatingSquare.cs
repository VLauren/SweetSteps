using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternatingSquare : Square
{

    protected override void StopPress()
    {
        base.StopPress();
    }

    protected override IEnumerator Press()
    {
        yield return base.Press();
    }

    protected override void OnGameAction()
    {
        Debug.Log("Alternating square OnGameAction");
        base.OnGameAction();
    }
}
