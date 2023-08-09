using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternatingSquare : Square
{
    public bool Pressable;

    public Color NonPressableColor;

    void Start()
    {
        if (!Pressable)
            GetComponent<Renderer>().material.color = NonPressableColor;
    }

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
