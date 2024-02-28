using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplePressSquare : Square
{
    public Color SecondPressColor;

    int pressAmount;

    void Start()
    {
        Level.AddSquare(this);
    }

    public override void StopPress(bool _special)
    {
        pressAmount++;
        Pressed = false;
        if(!_special)
            TryPress = false;

        Level.OnSquareUnpressed(this);

        if(pressAmount > 1)
            base.StopPress();
        else
            StartCoroutine(Unpress());
    }

    public override IEnumerator Press()
    {
        if (pressAmount == 0)
        {
            yield return base.Press();
        }
        else if(Level.Instance.LastPressedSquare != this)
        {
            Pressed = true;
            Level.OnSquarePressed(this);

            Color StartColor = GetComponent<Renderer>().material.color;
            float lerpAlfa = 0;

            Vector3 targetPosition = transform.position + Vector3.down * 0.1f;
            while (transform.position != targetPosition)
            {
                lerpAlfa = Mathf.MoveTowards(lerpAlfa, 1, Time.deltaTime / 0.1f);
                GetComponent<Renderer>().material.color = Color.Lerp(StartColor, SecondPressColor, lerpAlfa);

                transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime);
                yield return null;
            }

            AudioManager.Play(PressSound, false);
        }
    }

    IEnumerator Unpress()
    {
        Vector3 targetPosition = transform.position - Vector3.down * 0.1f;
        while(transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime);
            yield return null;
        }
    }
}
