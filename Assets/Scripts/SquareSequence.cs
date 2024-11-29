using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareSequence : Square
{
    static SquareSequence[] OtherSquareOfSequence;
    public static int NextPressable;

    public int Index;
    public Color NonPressableColor;
    Color PressableColor;

    void Start()
    {
        PressableColor = GetComponent<Renderer>().material.color;
        if (Index != 0)
        {
            GetComponent<Renderer>().material.color = NonPressableColor;
            foreach (var col in GetComponents<Collider>())
                col.enabled = false;

            OtherSquareOfSequence = FindObjectsOfType<SquareSequence>();
            NextPressable = 0;
        }

        Level.AddSquare(this);
    }

    public override void StopPress(bool _skipGameAction)
    {
        base.StopPress(_skipGameAction);
    }

    public override IEnumerator Press()
    {
        if (NextPressable == Index)
        {
            yield return base.Press();
            NextPressable++;
        }
    }

    protected override void OnGameAction()
    {
        if (!Pressed && NextPressable == Index)
        {
            StartCoroutine(ChangeColorOverTime(PressableColor, 0.1f));
            foreach (var col in GetComponents<Collider>())
                col.enabled = true;
        }

        base.OnGameAction();
    }

    IEnumerator ChangeColorOverTime(Color _color, float _time)
    {
        float elapsed = 0;
        Color startColor = GetComponent<Renderer>().material.color;

        while(elapsed < _time)
        {
            elapsed += Time.deltaTime;

            GetComponent<Renderer>().material.color = Color.Lerp(startColor, _color, elapsed / _time);

            yield return null;
        }

        GetComponent<Renderer>().material.color = _color;
    }

    public string GetKey()
    {
        if(Index == 1)
            return "w";
        if(Index == 2)
            return "e";
        if(Index == 3)
            return "r";
        if(Index == 4)
            return "t";
        return "q";
    }
}
