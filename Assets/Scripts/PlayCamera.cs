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
        if (Level.Instance != null)
        {
            Vector3 charFloorPos = MainChar.Instance.transform.position - Level.Instance.LevelCenter;
            charFloorPos.y = 0;
            transform.position = Level.Instance.LevelCenter + charFloorPos * PositionMultiplier + Offset;
        }
        else
        {
            transform.position = MainChar.Instance.transform.position + Offset;
        }
    }

    void Update()
    {
        if (Level.Instance != null)
        {
            Vector3 charFloorPos = MainChar.Instance.transform.position - Level.Instance.LevelCenter;
            charFloorPos.y = 0;

            transform.position = Vector3.SmoothDamp(transform.position, Level.Instance.LevelCenter + charFloorPos * PositionMultiplier + Offset, ref CamVelocity, CameraDampTime);
            // Debug.DrawLine(Level.Instance.LevelCenter, Level.Instance.LevelCenter + Vector3.up, Color.red); 
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, MainChar.Instance.transform.position + Offset, ref CamVelocity, CameraDampTime);
        }

    }
}
