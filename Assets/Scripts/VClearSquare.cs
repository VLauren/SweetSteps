using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VClearSquare : Square
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
            if (Mathf.Abs(sq.transform.position.x - transform.position.x) <= 0.1f && sq.GetType() != typeof(AreaSquare) && sq.GetType() != typeof(HClearSquare) && sq.GetType() != typeof(VClearSquare))
            {
                // Guardo la referencia y lo pulso
                PressedSquares.Add(sq);
                sq.StartCoroutine(sq.Press());
            }
        }

        return base.Press();
    }

    public override void StopPress(bool _special)
    {
        foreach (Square sq in PressedSquares)
            if (sq != null)
                sq.StopPress(true);

        base.StopPress();
    }
}
