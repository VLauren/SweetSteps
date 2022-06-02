using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCamera : MonoBehaviour
{
    [SerializeField] float CameraDampTime;
    [SerializeField] float PositionMultiplier;

    Vector3 Offset;
    Vector3 CamVelocity;

    void Start()
    {
        Offset = transform.position;
    }

    void Update()
    {
        Vector3 charFloorPos = MainChar.Instance.transform.position;
        charFloorPos.y = 0;

        transform.position = Vector3.SmoothDamp(transform.position, charFloorPos * PositionMultiplier + Offset, ref CamVelocity, CameraDampTime);
    }
}
