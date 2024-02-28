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

    public override void StopPress(bool _special)
    {
        base.StopPress();
    }

    public override IEnumerator Press()
    {
        if (Pressable)
            yield return base.Press();
    }

    protected override void OnGameAction()
    {
        Pressable = !Pressable;
        StartCoroutine(ChangeColorOverTime(Pressable ? PressableColor : NonPressableColor, 0.1f));
        foreach (var col in GetComponents<Collider>())
            col.enabled = Pressable;

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
}
