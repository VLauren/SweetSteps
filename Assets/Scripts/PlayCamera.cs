using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCamera : MonoBehaviour
{
    [SerializeField] float CameraDampTime;
    [SerializeField] float PositionMultiplier;

    [Space()]
    [SerializeField] Vector3 Offset;

    Vector3 CamVelocity;

    private void Start()
    {
        StartCoroutine(DelayedCameraStartup());
    }

    IEnumerator DelayedCameraStartup()
    {
        // Espero un frame para asegurarme de que el personaje está colocado en su posicion inicial
        yield return null;

        UpdateCameraPosition(0);
    }

    void Update()
    {
        UpdateCameraPosition(CameraDampTime);
    }

    void UpdateCameraPosition(float _dampTime)
    {

        if (Level.Instance != null)
        {
            Vector3 charFloorPos = MainChar.Instance.transform.position - Level.Instance.LevelCenter;
            charFloorPos.y = 0;

            transform.position = Vector3.SmoothDamp(transform.position, Level.Instance.LevelCenter + charFloorPos * PositionMultiplier + Offset, ref CamVelocity, _dampTime);
            // Debug.DrawLine(Level.Instance.LevelCenter, Level.Instance.LevelCenter + Vector3.up, Color.red); 
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, MainChar.Instance.transform.position + Offset, ref CamVelocity, _dampTime);
        }
    }
}
