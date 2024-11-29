using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSquare : Square
{
    List<Square> PressedSquares;

    void Start()
    {
        Level.AddSquare(this);
        PressedSquares = new List<Square>();
    }

    public override IEnumerator Press()
    {
        var squares = FindObjectsOfType<Square>();
        foreach (var sq in squares)
        {
            // Si no es un area square y es adyacente
            if (Vector3.Distance(sq.transform.position, transform.position) <= 3.1f && sq.GetType() != typeof(AreaSquare) && sq.GetType() != typeof(HClearSquare) && sq.GetType() != typeof(VClearSquare))
            {
                bool press = false;
                if (sq.GetType() == typeof(SquareSequence))
                {
                    if (((SquareSequence)sq).Index == SquareSequence.NextPressable)
                    {
                        press = true;
                    }
                }
                else if (sq.GetType() == typeof(AlternatingSquare))
                {
                    if (((AlternatingSquare)sq).Pressable)
                    {
                        press = true;
                    }
                }
                else
                {
                    press = true;
                }

                if (press)
                {
                    // Guardo la referencia y lo pulso
                    PressedSquares.Add(sq);
                    sq.StartCoroutine(sq.Press());
                }
            }
        }

        return base.Press();
    }

    public override void StopPress(bool _skipGameAction)
    {
        foreach (Square sq in PressedSquares)
            if (sq != null)
                sq.StopPress(true);

        base.StopPress(_skipGameAction);
    }
}
