using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplePressSquare : Square
{
    public Color SecondPressEmission;

    int pressAmount;

    protected override void StopPress()
    {
        pressAmount++;
        Pressed = false;
        TryPress = false;

        Level.OnSquareUnpressed(this);

        print("unpress: " + pressAmount);

        if(pressAmount > 1)
            base.StopPress();
        else
            StartCoroutine(Unpress());
    }

    protected override IEnumerator Press()
    {
        if (pressAmount == 0)
        {
            yield return base.Press();
        }
        else
        {
            Pressed = true;
            Level.OnSquarePressed(this);

            GetComponent<Renderer>().material.SetColor("_EmissionColor", SecondPressEmission);
            GetComponent<Renderer>().material.EnableKeyword("_EMISSION");

            Vector3 targetPosition = transform.position + Vector3.down * 0.1f;
            while (transform.position != targetPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime);
                yield return null;
            }
        }
    }

    IEnumerator Unpress()
    {
        // Pressed = true;
        // Level.OnSquarePressed(this);

        // GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.2f, 0.2f, 0.2f));
        // GetComponent<Renderer>().material.SetColor("_EmissionColor", PressedEmission);
        // GetComponent<Renderer>().material.EnableKeyword("_EMISSION");

        Vector3 targetPosition = transform.position - Vector3.down * 0.1f;
        while(transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime);
            yield return null;
        }
    }
}
