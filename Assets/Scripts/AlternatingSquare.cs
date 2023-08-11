using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternatingSquare : Square
{
    public bool Pressable;

    public Color NonPressableColor;

    Color PressableColor;

    void Start()
    {
        PressableColor = GetComponent<Renderer>().material.color;
        if (!Pressable)
            GetComponent<Renderer>().material.color = NonPressableColor;

        Level.AddSquare(this);
    }

    protected override void StopPress()
    {
        base.StopPress();
    }

    protected override IEnumerator Press()
    {
        if (Pressable)
            yield return base.Press();
    }

    protected override void OnGameAction()
    {
        Pressable = !Pressable;
        GetComponent<Renderer>().material.color = Pressable ? PressableColor : NonPressableColor;

        base.OnGameAction();
    }
}
